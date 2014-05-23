#region File Description
//-----------------------------------------------------------------------------
// MainForm.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System.Windows.Forms;
#endregion

namespace WinFormsGraphicsDevice
{
    // System.Drawing and the XNA Framework both define Color types.
    // To avoid conflicts, we define shortcut names for them both.
    using GdiColor = System.Drawing.Color;
    using XnaColor = Microsoft.Xna.Framework.Color;
    using System.Drawing;
    using Microsoft.Xna.Framework.Graphics;
    using System.IO;
    using System;
    using Newtonsoft.Json;

    
    /// <summary>
    /// Custom form provides the main user interface for the program.
    /// In this sample we used the designer to add a splitter pane to the form,
    /// which contains a SpriteFontControl and a SpinningTriangleControl.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            
        }


        /// <summary>
        /// Event handler updates the spinning triangle control when
        /// one of the three vertex color combo boxes is altered.
        /// </summary>
        void vertexColor_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            /*
            if (sender != null)
                return;
            */
            // Which color was selected?
            ComboBox combo = (ComboBox)sender;

            string colorName = combo.SelectedItem.ToString();

            GdiColor gdiColor = GdiColor.FromName(colorName);

            XnaColor xnaColor = new XnaColor(gdiColor.R, gdiColor.G, gdiColor.B);

            // Update the spinning triangle control with the new color.
            //spinningTriangleControl.Vertices[vertexIndex].Color = xnaColor;
        }

        private void numericUpDown1_ValueChanged(object sender, System.EventArgs e)
        {
            string[,] strarray = new string[ShowMap.map.GetLength(0), ShowMap.map.GetLength(1)];
            for (int i = 0; i < ShowMap.map.GetLength(0); i++)
			    for (int j = 0; j < ShowMap.map.GetLength(1); j++)
                    strarray[i, j] = ShowMap.map[i, j];

            ShowMap.map = new string[(int)numericUpDown1.Value, ShowMap.map.GetLength(1)];
            int max;
            if (ShowMap.map.GetLength(0) > strarray.GetLength(0))
                max = strarray.GetLength(0);
            else
                max = ShowMap.map.GetLength(0);

            for (int i = 0; i < max; i++)
                for (int j = 0; j < ShowMap.map.GetLength(1); j++)
                    ShowMap.map[i, j] = strarray[i, j];
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            string[,] strarray = new string[ShowMap.map.GetLength(0), ShowMap.map.GetLength(1)];
            for (int i = 0; i < ShowMap.map.GetLength(0); i++)
                for (int j = 0; j < ShowMap.map.GetLength(1); j++)
                    strarray[i, j] = ShowMap.map[i, j];

            ShowMap.map = new string[ShowMap.map.GetLength(0), (int)numericUpDown2.Value];
            int max;
            if (ShowMap.map.GetLength(1) > strarray.GetLength(1))
                max = strarray.GetLength(1);
            else
                max = ShowMap.map.GetLength(1);

            for (int i = 0; i < strarray.GetLength(0); i++)
                for (int j = 0; j < max; j++)
                    ShowMap.map[i, j] = strarray[i, j];
        }

        public void AddlistBox1Element(string str)
        {
            listBox1.Items.Add(str);
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            MemoryStream stream = new MemoryStream();
            Texture2D texture = ShowMap.tiles[listBox1.SelectedItem.ToString()];
            texture.SaveAsPng(stream, texture.Bounds.Width, texture.Bounds.Height);
            Bitmap bitmap = new Bitmap(stream);
            pictureBox1.Image = bitmap;
        }


        private void MouseMapAction()
        {
            if ((listBox1.SelectedItem != null) && (ShowMap.tiles.ContainsKey(listBox1.SelectedItem.ToString())))
            {
                Point location = mapEditor.PointToClient(MousePosition);
                float x = location.X, y = location.Y;
                x = x / ShowMap.dimension;
                y = y / ShowMap.dimension;
                if ((x >= 0) && (x < ShowMap.map.GetLength(0)) && (y >= 0) && (y < ShowMap.map.GetLength(1)))
                {
                    ShowMap.map[(int)Math.Floor(x), (int)Math.Floor(y)] = listBox1.SelectedItem.ToString();
                }
            }
        }
        private void spinningTriangleControl_Click(object sender, System.EventArgs e)
        {
            MouseMapAction();
        }
        

        private void spinningTriangleControl_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (checkBox1.Checked || ctrlPressed)
                MouseMapAction();
        }
        private bool ctrlPressed = false; // Ne marche pas et je sais pas pk
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                ctrlPressed = true;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control)
                ctrlPressed = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Stream stream = saveFileDialog1.OpenFile();
            StreamWriter sw = new StreamWriter(stream);
            sw.Write(JsonConvert.SerializeObject(ShowMap.map));
            sw.Flush();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            ShowMap.dimension = (int)numericUpDown3.Value;
        }

    }
}

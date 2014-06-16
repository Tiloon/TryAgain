#region File Description
//-----------------------------------------------------------------------------
// MainForm.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion


using System.Windows.Forms;
using System.IO;
using System.IO.Compression;


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
    using Ionic.Zip;


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

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

            try
            {
                String json;
                StreamReader sr = new StreamReader(openFileDialog1.OpenFile());
                json = sr.ReadToEnd();
                ShowMap.map = JsonConvert.DeserializeObject<String[,]>(json);
            }
            catch (Exception)
            {
                MessageBox.Show("Error : not a map file or other weeblee wobly timy spacy related error.");
            }
        }

        private void mapEditor_MouseUp(object sender, MouseEventArgs e)
        {
            checkBox1.Checked = false;
        }

        private void mapEditor_MouseDown(object sender, MouseEventArgs e)
        {
            checkBox1.Checked = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private int cX = 1, currentX = 0, cY = 1, currentY = 0;
        private bool isLoadingMap = true;

        private void button6_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(Application.StartupPath + @"\tmp");
            progressBar1.Value = 0;
            button7.Enabled = true;
            button6.Enabled = false;
            cX = (int)numericUpDown17.Value;
            cY = (int)numericUpDown16.Value;
            numericUpDown17.Enabled = false;
            numericUpDown16.Enabled = false;
            progressBar1.Maximum = cX * cY + 1;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        String[,] mapArray;
        int arrayX, arrayY;
        private void openFileDialog2_FileOk_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                String json;
                StreamReader sr = new StreamReader(openFileDialog2.OpenFile());
                json = sr.ReadToEnd();
                String[,] array = JsonConvert.DeserializeObject<string[,]>(json);
                if ((currentX == 0) && (currentY == 0))
                {
                    mapArray = new String[array.GetLength(0) * cX, array.GetLength(1) * cY];
                    arrayX = array.GetLength(0);
                    arrayY = array.GetLength(1);
                }

                if ((arrayX != array.GetLength(0)) || (arrayY != array.GetLength(1)))
                    throw new Exception("Maps aren't the same size");

                for (int i = 0; i < arrayX; i++)
                {
                    for (int j = 0; j < arrayY; j++)
                    {
                        mapArray[currentX * arrayX + i, currentY * arrayY + j] = array[i, j]; 
                    }
                }

                currentX++;
                if (currentX >= cX)
                {
                    currentX = 0;
                    currentY++;
                    if (currentY >= cY)
                    {
                        currentY = 0;
                        button7.Enabled = false;
                        button8.Enabled = true;
                    }
                }
                button7.Text = "Load map (" + currentX + ", " + currentY + ")";

                progressBar1.Value++;
            }
            catch (Exception E)
            {
                MessageBox.Show("Error : not a map file or other weeblee wobly timy spacy related error." + E.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(mapArray);
            StreamWriter sw = new StreamWriter(Application.StartupPath + @"\tmp\map.json");
            sw.Write(json);
            sw.Flush();
            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(Application.StartupPath + @"\tmp");
                zip.Save("world.zip");
            }
            MessageBox.Show("File created as \"" + Application.StartupPath + "\\world.zip\".");
            button8.Enabled = false;
            button6.Enabled = true;
            numericUpDown17.Enabled = true;
            numericUpDown16.Enabled = true;
            progressBar1.Value++;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Shared.Stats npcStats = new Shared.Stats();
            npcStats.lvl = (int)numericUpDown4.Value;
            npcStats.lp = (int)numericUpDown4.Value;
            npcStats.lpmax = (int)numericUpDown5.Value;
            npcStats.mh = (int)numericUpDown6.Value;
            npcStats.mhmax = (int)numericUpDown7.Value;
            npcStats.ch = (int)numericUpDown8.Value;
            npcStats.chmax = (int)numericUpDown9.Value;
            npcStats.cbonus = (int)numericUpDown10.Value;
            npcStats.mp = (int)numericUpDown11.Value;
            npcStats.mpmax = (int)numericUpDown12.Value;
            npcStats.force = (int)numericUpDown13.Value;
            npcStats.intelligence = (int)numericUpDown14.Value;
            npcStats.defense = (int)numericUpDown15.Value;
            npcStats.criticalrate = (int)numericUpDown16.Value;
            npcStats.speed = (float)numericUpDown17.Value;

            Shared.Gob npc = new Shared.Gob();
            npc.stats = npcStats;
            npc.name = textBox1.Text;
            npc.commonName = textBox2.Text;
            npc.type = "GameObject,Character,Npc";
            npc.script = textBox3.Text;
            npc.X = Shared.Converter.FloatToString(0);
            npc.Y = Shared.Converter.FloatToString(0);
        }


        private String npcPic = "";
        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();
        }

        private void openFileDialog3_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

    }
}

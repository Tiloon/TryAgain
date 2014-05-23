using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using Newtonsoft.Json;

namespace WinFormsGraphicsDevice
{
    /// <summary>
    /// Example control inherits from GraphicsDeviceControl, which allows it to
    /// render using a GraphicsDevice. This control shows how to draw animating
    /// 3D graphics inside a WinForms application. It hooks the Application.Idle
    /// event, using this to invalidate the control, which will cause the animation
    /// to constantly redraw.
    /// </summary>
    class ShowMap : GraphicsDeviceControl
    {
        public static int paddingX = 0, paddingY = 0;
        public static Dictionary<String, Texture2D> tiles = new Dictionary<string, Texture2D>();
        public static string[,] map = new string[12, 12];
        Tuple<String, String, bool>[] tileList;
        SpriteBatch spriteBatch;
        public static int dimension = 64;

        private static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }

        private static String ReadTextFile(String path)
        {
            return System.Text.Encoding.UTF8.GetString(ReadFile(path));
        }

        private void InitFromJSON(String filename)
        {
            tileList = JsonConvert.DeserializeObject<Tuple<String, String, bool>[]>(ReadTextFile(filename));
            foreach (var tile in tileList)
            {
                if (!tiles.ContainsKey(tile.Item1))
                {
                    FileStream fs = new FileStream(tile.Item2, FileMode.Open);
                    Texture2D t = Texture2D.FromStream(this.GraphicsDevice, fs);
                    tiles.Add(tile.Item1, t);
                    Program.form.AddlistBox1Element(tile.Item1);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// <summary>
        /// Initializes the control.
        /// </summary>
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
            InitFromJSON("TextureList.json");
        }

        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            Texture2D SimpleTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            Int32[] pixel = { 0xFFFFFF };
            SimpleTexture.SetData<Int32>(pixel, 0, SimpleTexture.Width * SimpleTexture.Height);

            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            //Draw
            for (int i = 0; i <= map.GetLength(0); i++)
            {
                for (int j = 0; j <= map.GetLength(1); j++)
                {
                    if ((j < map.GetLength(1)) && (i < map.GetLength(0)))
                        if (map[i, j] != null)
                            spriteBatch.Draw(tiles[map[i, j]], new Rectangle(dimension * i, dimension * j, dimension, dimension), Color.White);

                    if (i < map.GetLength(0))
                    {
                        if ((j == 0) || (j == map.GetLength(1)))
                            spriteBatch.Draw(SimpleTexture, new Rectangle(dimension * i, dimension * j, dimension, 3), Color.Red);
                        else
                            spriteBatch.Draw(SimpleTexture, new Rectangle(dimension * i, dimension * j, dimension, 1), Color.White);
                    }

                    if ((j < map.GetLength(1)))
                    {
                        if ((i == 0) || (i == map.GetLength(0)))
                            spriteBatch.Draw(SimpleTexture, new Rectangle(dimension * i, dimension * j, 3, dimension), Color.Red);
                        else
                            spriteBatch.Draw(SimpleTexture, new Rectangle(dimension * i, dimension * j, 1, dimension), Color.White);
                    }

                }
            }
            spriteBatch.End();
        }
    }
}

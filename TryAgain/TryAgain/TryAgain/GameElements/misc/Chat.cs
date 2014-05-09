using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TryAgain.Online;

namespace TryAgain.GameElements.misc
{
    class Chat
    {
        public volatile static bool isWriting = false;
        private volatile static String[] lastMessages = new String[8];
        private volatile static Color[] lastMessagesColor = new Color[8];
        private static String bufferStr = "";
        private static float opacity = 1.0F;
        private static KeyboardState oldKeyboardState, newState;

        public static void AddMessage(String str, Color color)
        {
            String oldMessage;
            Color oldColor;
            for (int i = 0; i < lastMessages.Length; i++)
            {
                oldMessage = lastMessages[i];
                lastMessages[i] = str;
                str = oldMessage;
                oldColor = lastMessagesColor[i];
                lastMessagesColor[i] = color;
                color = oldColor;
            }
            opacity = 1.0F;
        }

        public static void AddMessage(String str)
        {
            AddMessage(str, Color.DarkGray);
        }

        public static void Draw(SpriteBatch sb)
        {
            Textures.DrawRectangle(sb, new Rectangle(0, 720, 256, 128), Color.Cyan * opacity);
            for (int i = 0; i < lastMessages.Length; i++)
            {
                if((lastMessages[i] != null) && (lastMessages[i] != ""))
                    sb.DrawString(Textures.UIfontSmall, lastMessages[i], new Vector2(0, 720 + 128 - (i + 1) * 15 - 4), lastMessagesColor[i] * (opacity / 2 + 0.5F));
            }
            if (true)
            {
                Textures.DrawRectangle(sb, new Rectangle(0, 720 + 128, 256, 24), Color.DeepSkyBlue * 0.8F);
                sb.DrawString(Textures.UIfontSmall, bufferStr, new Vector2(0, 720 + 128), Color.Black);
            }
            if (opacity > 0)
                opacity -= 0.01F;
        }

        public static void Write()
        {
            if (!isWriting)
            {
                newState = Keyboard.GetState();
                isWriting = true;
                bufferStr = "";
            }
            else
            {
                oldKeyboardState = newState;
                newState = Keyboard.GetState();
                if (bufferStr.Length < 24)
                {
                    if ((newState.IsKeyDown(Keys.RightAlt) && !oldKeyboardState.IsKeyDown(Keys.D9)) &&
                        (newState.IsKeyDown(Keys.D8) && !oldKeyboardState.IsKeyDown(Keys.D8)))
                    {
                        bufferStr += '\\';
                        return;
                    }

                    if (newState.IsKeyDown(Keys.A) && !oldKeyboardState.IsKeyDown(Keys.A))
                        bufferStr += 'a';
                    if (newState.IsKeyDown(Keys.B) && !oldKeyboardState.IsKeyDown(Keys.B))
                        bufferStr += 'b';
                    if (newState.IsKeyDown(Keys.C) && !oldKeyboardState.IsKeyDown(Keys.C))
                        bufferStr += 'c';
                    if (newState.IsKeyDown(Keys.D) && !oldKeyboardState.IsKeyDown(Keys.D))
                        bufferStr += 'd';
                    if (newState.IsKeyDown(Keys.E) && !oldKeyboardState.IsKeyDown(Keys.E))
                        bufferStr += 'e';
                    if (newState.IsKeyDown(Keys.F) && !oldKeyboardState.IsKeyDown(Keys.F))
                        bufferStr += 'f';
                    if (newState.IsKeyDown(Keys.G) && !oldKeyboardState.IsKeyDown(Keys.G))
                        bufferStr += 'g';
                    if (newState.IsKeyDown(Keys.H) && !oldKeyboardState.IsKeyDown(Keys.H))
                        bufferStr += 'h';
                    if (newState.IsKeyDown(Keys.I) && !oldKeyboardState.IsKeyDown(Keys.I))
                        bufferStr += 'i';
                    if (newState.IsKeyDown(Keys.J) && !oldKeyboardState.IsKeyDown(Keys.J))
                        bufferStr += 'j';
                    if (newState.IsKeyDown(Keys.K) && !oldKeyboardState.IsKeyDown(Keys.K))
                        bufferStr += 'k';
                    if (newState.IsKeyDown(Keys.L) && !oldKeyboardState.IsKeyDown(Keys.L))
                        bufferStr += 'l';
                    if (newState.IsKeyDown(Keys.M) && !oldKeyboardState.IsKeyDown(Keys.M))
                        bufferStr += 'm';
                    if (newState.IsKeyDown(Keys.N) && !oldKeyboardState.IsKeyDown(Keys.N))
                        bufferStr += 'n';
                    if (newState.IsKeyDown(Keys.O) && !oldKeyboardState.IsKeyDown(Keys.O))
                        bufferStr += 'o';
                    if (newState.IsKeyDown(Keys.P) && !oldKeyboardState.IsKeyDown(Keys.P))
                        bufferStr += 'p';
                    if (newState.IsKeyDown(Keys.Q) && !oldKeyboardState.IsKeyDown(Keys.Q))
                        bufferStr += 'q';
                    if (newState.IsKeyDown(Keys.R) && !oldKeyboardState.IsKeyDown(Keys.R))
                        bufferStr += 'r';
                    if (newState.IsKeyDown(Keys.S) && !oldKeyboardState.IsKeyDown(Keys.S))
                        bufferStr += 's';
                    if (newState.IsKeyDown(Keys.T) && !oldKeyboardState.IsKeyDown(Keys.T))
                        bufferStr += 't';
                    if (newState.IsKeyDown(Keys.U) && !oldKeyboardState.IsKeyDown(Keys.U))
                        bufferStr += 'u';
                    if (newState.IsKeyDown(Keys.V) && !oldKeyboardState.IsKeyDown(Keys.V))
                        bufferStr += 'v';
                    if (newState.IsKeyDown(Keys.W) && !oldKeyboardState.IsKeyDown(Keys.W))
                        bufferStr += 'w';
                    if (newState.IsKeyDown(Keys.X) && !oldKeyboardState.IsKeyDown(Keys.X))
                        bufferStr += 'x';
                    if (newState.IsKeyDown(Keys.Y) && !oldKeyboardState.IsKeyDown(Keys.Y))
                        bufferStr += 'y';
                    if (newState.IsKeyDown(Keys.Z) && !oldKeyboardState.IsKeyDown(Keys.Z))
                        bufferStr += 'z';
                    if (newState.IsKeyDown(Keys.D0) && !oldKeyboardState.IsKeyDown(Keys.D0))
                        bufferStr += '0';
                    if (newState.IsKeyDown(Keys.D1) && !oldKeyboardState.IsKeyDown(Keys.D1))
                        bufferStr += '1';
                    if (newState.IsKeyDown(Keys.D2) && !oldKeyboardState.IsKeyDown(Keys.D2))
                        bufferStr += '2';
                    if (newState.IsKeyDown(Keys.D3) && !oldKeyboardState.IsKeyDown(Keys.D3))
                        bufferStr += '3';
                    if (newState.IsKeyDown(Keys.D4) && !oldKeyboardState.IsKeyDown(Keys.D4))
                        bufferStr += '4';
                    if (newState.IsKeyDown(Keys.D5) && !oldKeyboardState.IsKeyDown(Keys.D5))
                        bufferStr += '5';
                    if (newState.IsKeyDown(Keys.D6) && !oldKeyboardState.IsKeyDown(Keys.D6))
                        bufferStr += '6';
                    if (newState.IsKeyDown(Keys.D7) && !oldKeyboardState.IsKeyDown(Keys.D7))
                        bufferStr += '7';
                    if (newState.IsKeyDown(Keys.D8) && !oldKeyboardState.IsKeyDown(Keys.D8))
                        bufferStr += '8';
                    if (newState.IsKeyDown(Keys.D9) && !oldKeyboardState.IsKeyDown(Keys.D9))
                        bufferStr += '9';

                    if (newState.IsKeyDown(Keys.Space) && !oldKeyboardState.IsKeyDown(Keys.Space))
                        bufferStr += ' ';
                }

                if (newState.IsKeyDown(Keys.Back) && !oldKeyboardState.IsKeyDown(Keys.Back))
                {
                    if (bufferStr.Length >= 1)
                       bufferStr = bufferStr.Remove(bufferStr.Length - 1);
                }
               
                if (newState.IsKeyDown(Keys.Enter) && !oldKeyboardState.IsKeyDown(Keys.Enter))
                {
                    isWriting = false;
                    Connection.SendMessage(bufferStr);
                    bufferStr = "";
                }
            }
        }

    }
}

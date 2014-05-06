using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Server
{
    class GameObject
    {
        public String X, Y;
        public float x, y;
        public String ID;
        public String spr;
        public String type;
        public String name;
        public bool changed;
        private Rectangle view;
        public Rectangle GetView()
        {
            return view;
        }
        public void SetView(Rectangle view)
        {
            this.view = view;
        }
    }
}

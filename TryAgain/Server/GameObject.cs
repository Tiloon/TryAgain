﻿using System;
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
        private bool toUpdate;
        private List<GameObject> near = new List<GameObject>();
        private GameObject target;

        public void Update()
        {
            if (this.target != null)
            {
                Vector2 direction = new Vector2(this.target.x - this.x, this.target.y - this.y);
                this.x += direction.X * 0.1f;
                this.y += direction.Y * 0.1f;
                this.X = Convert.ToBase64String(BitConverter.GetBytes(this.x));
                this.Y = Convert.ToBase64String(BitConverter.GetBytes(this.y));

                target = null;
            }
        }

        // Bouger ça
        private static float CartesianDistance(float x, float y)
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        public void NewTarget(GameObject gob)
        {
            if ((this.target == null) || (CartesianDistance(this.x - this.target.x, this.y - this.target.y) > CartesianDistance(this.x - gob.x, this.y - gob.y)))
            {
                target = gob;
            }
        }

        public Rectangle GetView()
        {
            return view;
        }
        public void SetView(Rectangle view)
        {
            this.view = view;
        }
        public bool IsToUpdate()
        {
            return this.toUpdate;
        }
        public void ToUpdate(bool b)
        {
            this.toUpdate = b;
        }
    }
}

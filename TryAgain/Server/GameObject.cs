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
        public float speed = 0.09F;
        public String ID;
        public String spr;
        public String type;
        public String name;
        public String datas = "";
        private String script = "";
        public bool changed;
        private Rectangle view;
        private bool toUpdate;
        private List<GameObject> near = new List<GameObject>();
        private GameObject target;
        private Shared.Stats stats;

        public void SetScript(String str)
        {
            this.script = str;
        }

        public void SetStats(Shared.Stats stats)
        {
            this.stats = stats;
        }

        public GameObject()
        {
            stats.lvl = 1;
            stats.lp = 1;
            stats.lpmax = 1;
            stats.mh = 100;
            stats.mhmax = 100;
            stats.ch = 80;
            stats.chmax = 100;
            stats.cbonus = 0;
            stats.mp = 0;
            stats.mpmax = 0;
            stats.force = 10;
            stats.intelligence = 10;
            stats.defense = 10;
            stats.criticalrate = 0;
            stats.speed = 0.15F;
        }

        public void TakeDamages(int points)
        {
            this.stats.lp -= points;
            if (this.stats.lp <= 0)
            {
                if (Server.goblist.ContainsKey(this.ID))
                    Server.goblist.Remove(this.ID);
                if(Server.igIDs.Contains(this.ID))
                    Server.igIDs.Remove(this.ID);
                Program.server.Remove(this.ID);
            }

            Console.WriteLine("Stills " + this.stats.lp + " live points");
        }

        public void SetLp(int lp)
        {
            this.stats.lp = lp;
            this.stats.lpmax = lp;
        }

        public void Update()
        {
            if (this.type == "Monster")
            {
                if (this.target != null)
                {
                    Vector2 direction = new Vector2(this.target.x - this.x, this.target.y - this.y);
                    if((this.name != "bio") || (Server.map[(int)Math.Ceiling(this.x + direction.X * 0.1f), (int)this.y] != "Taqua"))
                        this.x += direction.X * 0.1f;
                    if ((this.name != "bio") || (Server.map[(int)this.x, (int)Math.Ceiling(this.y + direction.Y * 0.1f)] != "Taqua"))
                        this.y += direction.Y * 0.1f;
                    this.X = Convert.ToBase64String(BitConverter.GetBytes(this.x));
                    this.Y = Convert.ToBase64String(BitConverter.GetBytes(this.y));
                    
                        
                    target = null;
                }
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

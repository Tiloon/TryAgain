using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TryAgain.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using TryAgain.GameElements;

namespace TryAgain.GameElements.Characters
{
    class Player: Character
    {
        public Player(String sprite, Vector2 position)
            : base("Player", "Player")
        {
            this.stats.lp = 60;
            this.position = new Vector2(position.X, position.Y);
            this.X = position.X;
            this.Y = position.Y;
            if (Textures.Cache.ContainsKey(sprite))
                this.apparence = Textures.Cache[sprite];
            else
                this.apparence = Textures.Cache["Tminion"];
        }

        public void Collision()
        { }

        public override void update()
        { }

        public override void jsonUpdate(string json)
        {
            base.jsonUpdate(json);
            Player data = JsonConvert.DeserializeObject<Player>(json);
            this.stats = data.stats;
            GameObject gob = data;
            Delete(ref gob);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using TryAgain.Datas;

namespace TryAgain.GameElements.Map___environnement
{
    class Tile
    {
        private Texture2D t;
        private bool walkable = true;
        public bool isBlended = false;
        public string type = "UNDEF";

        public bool IsWalkable()
        {
            return this.walkable;
        }

        public Texture2D getTexture()
        {
            return this.t;
        }

        public Tile(Texture2D texture, bool isWalkable)
        {
            this.walkable = isWalkable;
            this.t = texture;
        }

        public Tile(string name)
        {
            Tuple<bool> props = JsonConvert.DeserializeObject<Tuple<bool>>(Initializer.ReadTextFile(@"elements\textures\" + name + ".json"));
            this.walkable = props.Item1;
            this.t = Textures.Cache[name];
            this.type = name;
        }
    }
}

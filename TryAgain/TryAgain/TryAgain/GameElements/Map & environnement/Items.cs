using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Noesis.Javascript;

namespace TryAgain
{
    enum ItemType
    {
        undef,
        weapon,
        food
    }
    class Items
    {
        public static JavascriptContext jscontext = new JavascriptContext();

        public static Target useOnTarget(string script, Target t)
        {
            jscontext.SetParameter("target", t);
            jscontext.Run(script);
            return (Target)jscontext.GetParameter("target");
        }
    }

    class Item
    {
        private ItemType type;
        private Texture2D icon;
        private string itemID;
        private string itemName;
        private string script;
        private Object data;
        // private delegate Target useItem(Target t) ;
        public Func<Target, Target> useItem { get; set; }

        public Item(string itemID, string itemName, Texture2D icon, string script, ItemType type, Object data)
        {
            this.itemID = itemID;
            this.itemName = itemName;
            this.data = data;
            this.icon = icon;
            this.script = script;
            this.type = type;
            this.itemID = "undefined";
            this.useItem = (t) => { return Items.useOnTarget(script, t); };
        }

        public Item(Texture2D icon, string script) : this("00undef", "Undefined", icon, script, ItemType.undef, null)
        {
        }
    }
}
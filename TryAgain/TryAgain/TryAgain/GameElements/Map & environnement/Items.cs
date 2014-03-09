using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Noesis.Javascript;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TryAgain
{
    enum ItemType
    {
        none,
        undef,
        meleWeapon,
        food
    }
    class Items
    {
        public static JavascriptContext jscontext = new JavascriptContext();

        //
        public static Func<Object, String> msg { get; set; }

        public static Tuple<String, String> useOnTarget(string script, object user, object target)
        {
            jscontext.SetParameter("user", JsonConvert.SerializeObject(user));
            jscontext.SetParameter("target", JsonConvert.SerializeObject(target));
            msg = x => { MessageBox.Show(x.ToString()); return ""; };
            jscontext.SetParameter("msg", msg);
            jscontext.Run(script);
            return new Tuple<String, String>(jscontext.GetParameter("user").ToString(), jscontext.GetParameter("target").ToString());
        }
    }

    class Item
    {
        protected String type;
        protected Texture2D icon;
        protected string itemID;
        protected string itemName;
        protected string script;
        protected Object data;
        // private delegate Target useItem(Target t) ;
        public Func<Object, Object, Tuple<String, String>> useItem { get; set; }

        public Item(string itemID, string itemName, Texture2D icon, string script, String type, Object data)
        {
            this.itemID = itemID;
            this.itemName = itemName;
            this.data = data;
            this.icon = icon;
            this.script = "var target = JSON.parse(target), user = JSON.parse(user);" + script + "target = JSON.stringify(target);user = JSON.stringify(user);";
            this.type = type;
            this.itemID = "undefined";
            this.useItem = (u, t) => { return Items.useOnTarget(this.script, u, t); };
        }

        public Texture2D getIcon()
        {
            return this.icon;
        }

        public Item(Texture2D icon, string script) : this("00undef", "Undefined", icon, script, "undef", null)
        {
        }
        public Item()
            : this(null, "")
        {
        }
    }

    class MeleWeapon : Item
    {
        public MeleWeapon()
            : base(Textures.neige_texture, "target.stats.lp-=user.stats.force;")
        {
            //this.useItem = (t) => { return t; };
        }
    }
}
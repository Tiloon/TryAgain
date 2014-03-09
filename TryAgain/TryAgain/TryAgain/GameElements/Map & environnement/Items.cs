﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Noesis.Javascript;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using TryAgain.Datas;

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
        public static Dictionary<String, Item> itemsBank = new Dictionary<string,Item>();
        public static JavascriptContext jscontext = new JavascriptContext();

        public static void initializeItemBank()
        {
            String[] itemIdList = JsonConvert.DeserializeObject<String[]>(Initializer.ReadTextFile(@"elements\items\itemslist.json"));
            foreach (var id in itemIdList)
            {
                //MessageBox.Show(id);
                itemsBank[id] = Item.mkItemFromFile(id + ".json");
            }
        }

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

    public struct ItemDefinition
    {
        public String itemid;
	    public String itemname;
	    public int online;
        public String onuseScript;
	    public String icon;
	    public String Type;
        public String data;
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

        public Item(Texture2D icon, string script)
            : this("00undef", "Undefined", icon, script, "undef", null)
        {
        }
        public Item()
            : this(null, "")
        {
        }

        public static Item mkItemFromFile(String path)
        {
            ItemDefinition e = JsonConvert.DeserializeObject<ItemDefinition>(Initializer.ReadTextFile(@"elements\items\" + path));
            String script = "";
            script = Initializer.ReadTextFile(@"elements\items\" + e.onuseScript);
            if (!Textures.Cache.ContainsKey(e.icon))
            {
                Textures.Cache[e.icon] = Texture2D.FromStream(Game1.gamegfx.GraphicsDevice, new FileStream(@"elements\items\" + e.icon, FileMode.Open)); 
            }
            return new Item(Textures.Cache[e.icon], script);
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
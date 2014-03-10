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
using System.IO;
using TryAgain.Datas;
using TryAgain.GameElements;
using TryAgain.GameElements.misc;

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
        public static Dictionary<String, Item> itemsBank = new Dictionary<string, Item>();
        public static JavascriptContext jscontext = new JavascriptContext();

        public static void initializeItemBank()
        {
            String[] itemIdList = JsonConvert.DeserializeObject<String[]>(Initializer.ReadTextFile(@"elements\items\itemslist.json"));
            foreach (var id in itemIdList)
            {
                itemsBank[id] = Item.mkItemFromFile(id + ".json");
            }
        }

        public static Tuple<String, String> useOnTarget(Item item, string script, object user, object target)
        {
            jscontext.SetParameter("item_delete", (Func<bool>)(() => item.exists = false));
            jscontext.SetParameter("item", item.data);
            jscontext.SetParameter("user", JsonConvert.SerializeObject(user));
            jscontext.SetParameter("target", JsonConvert.SerializeObject(target));

            JSAPI.setJSAPI(ref jscontext);
            jscontext.Run(script);
            item.data = jscontext.GetParameter("item").ToString();
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
        protected String UID;
        protected String type;
        protected Texture2D icon;
        protected string itemID;
        protected string itemName;
        protected string script;
        public String data = " ";
        public bool exists = true;
        // private delegate Target useItem(Target t) ;
        public Func<Object, Object, Tuple<String, String>> useItem { get; set; }

        public String GetUID()
        {
            return this.UID;
        }

        public String GetItemID()
        {
            return this.itemID;
        }

        public Item(string itemID, string itemName, Texture2D icon, string script, String type, String data)
        {
            this.itemID = itemID;
            this.itemName = itemName;
            this.data = data;
            this.icon = icon;
            this.script = // the ugliest way
                "var target = JSON.parse(target)," +
                    "item = JSON.parse(item)," +
                    "user = JSON.parse(user);" +
                "item.delete = item_delete;" +
                "/*try {*/" +
                    script +
                "/*} catch (err) {msg(JSON.stringify(err));};*/" +
                "target = JSON.stringify(target);" +
                "user = JSON.stringify(user);" +
                "item = JSON.stringify(item);";
            this.type = type;
            this.itemID = "undefined";
            this.useItem = (u, t) =>
            {
                try
                {
                    return Items.useOnTarget(this, this.script, u, t);
                }
                catch (Exception e)
                {
                    Debug.Show(e.ToString());
                    return new Tuple<string, string>(JsonConvert.SerializeObject(u), JsonConvert.SerializeObject(t));
                }
                
            };
        }

        public Texture2D getIcon()
        {
            return this.icon;
        }

        public Item(Texture2D icon, string script)
            : this("00undef", "Undefined", icon, script, "undef", "")
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
            String data = Initializer.ReadTextFile(@"elements\items\" + e.data);
            return new Item(e.itemid, e.itemname, Textures.Cache[e.icon], script, e.Type, data);
        }
    }

    class GObItem : GameObject
    {
        private Item bindedItem;
        public Item item()
        {
            return bindedItem;
        }
        public GObItem(Item item, Vector2 pos)
            : base("GObItem", item.GetItemID() + item.GetUID())
        {
            this.bindedItem = item;
            this.position = pos;
            this.size = new Vector2(64, 64);
        }
        public override void update()
        {

        }
        public override void jsonUpdate(String json)
        {

        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(this.bindedItem.getIcon(), this.position, Color.White);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Noesis.Javascript;
using TryAgain.GameStates;
using TryAgain.GameElements.Map___environnement;
using Microsoft.Xna.Framework;

namespace TryAgain.Datas
{
    class JSAPI
    {
        public static Func<String, int, int, int, int, bool> JSCreateParticle = (name, x1, y1, x2, y2) =>
        {
            GameScreen.GOList.Add(new GobParticle(name, new Vector2(x1, y1), new Vector2(x2, y2)));
            return true;
        };
        public static Func<Object, String> msg = x => { MessageBox.Show(x.ToString()); return ""; };

        public static void setJSAPI(ref JavascriptContext ctx)
        {
            ctx.SetParameter("API_msg", JSAPI.msg);
            ctx.SetParameter("API_CreateParticle", JSAPI.JSCreateParticle);

        }
    }
}

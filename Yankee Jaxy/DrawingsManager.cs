using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;

using static Eclipse.SpellsManager;
using static Eclipse.Menus;

namespace Eclipse
{
    internal class DrawingsManager
    {
        public static void InitializeDrawings()
        {
            Drawing.OnDraw += Drawing_OnDraw;
            Drawing.OnEndScene += Drawing_OnEndScene;
            DamageIndicator.Init();
        }

        /// <summary>
        /// Normal Drawings will not ovewrite any of LOL Sprites
        /// </summary>
        /// <param name="args"></param>
        private static void Drawing_OnDraw(EventArgs args)
        {
            var readyDraw = DrawingsMenu.GetCheckBoxValue("readyDraw");

            if (DrawingsMenu.GetCheckBoxValue("qDraw") && readyDraw ? Q.IsReady() : DrawingsMenu.GetCheckBoxValue("qDraw"))
            {
                Circle.Draw(QColorSlide.GetSharpColor(), Q.Range, 1f, Player.Instance);
            }


        }

        /// <summary>
        /// This one will overwrite LOL sprites like menus, healthbar and etc
        /// </summary>
        /// <param name="args"></param>
        private static void Drawing_OnEndScene(EventArgs args)
        {
           
        }
    }
}

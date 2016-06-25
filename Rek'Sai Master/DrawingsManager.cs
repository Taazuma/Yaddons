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

            if (DrawingsMenu.GetCheckBoxValue("qDraw") && readyDraw ? Q.IsReady() : Q.IsLearned)
            {
                Circle.Draw(QColorSlide.GetSharpColor(), Q.Range, 1f, Player.Instance);
            }

            //if (DrawingsMenu.GetCheckBoxValue("wDraw") && readyDraw ? W.IsReady() : W.IsLearned)
            //{
            //    Circle.Draw(WColorSlide.GetSharpColor(), W.Range, 1f, Player.Instance);
            //}

            //if (DrawingsMenu.GetCheckBoxValue("eDraw") && readyDraw ? E.IsReady() : E.IsLearned)
            //{
            //    Circle.Draw(EColorSlide.GetSharpColor(), E.Range, 1f, Player.Instance);
            //}

            //if (DrawingsMenu.GetCheckBoxValue("rDraw") && readyDraw ? R.IsReady() : R.IsLearned)
            //{
            //    Circle.Draw(RColorSlide.GetSharpColor(), R.Range, 1f, Player.Instance);
            //}

            //if (DrawingsMenu.GetCheckBoxValue("srDraw") && readyDraw ? Smite.IsReady() : Smite.IsLearned)
            //{
            //    Circle.Draw(SColorSlide.GetSharpColor(), Smite.Range, 1f, Player.Instance);
            //}

            //if (MiscMenu.GetKeyBindValue("smitekey") && DrawingsMenu.GetCheckBoxValue("sDraw") && readyDraw ? Smite.IsReady() : Smite.IsLearned)
            //{
            //    Drawing.DrawText(300, 235, System.Drawing.Color.AntiqueWhite, "Smite: Ready");
            //}

            //if (Smite.IsLearned && Smite.IsOnCooldown && DrawingsMenu.GetCheckBoxValue("sDraw") && MiscMenu.GetKeyBindValue("smitekey"))
            //{
            //    Drawing.DrawText(300, 235, System.Drawing.Color.IndianRed, "Smite: Cooldown");
            //}

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

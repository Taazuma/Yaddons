using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;
using static Eclipse.SpellsManager;
using static Eclipse.Menus;
using System.Linq;

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
        public static Obj_AI_Minion Minion;
        public static Text Text1 = new EloBuddy.SDK.Rendering.Text("", new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 20, System.Drawing.FontStyle.Bold));
        private static Text Text = new EloBuddy.SDK.Rendering.Text("", new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 15, System.Drawing.FontStyle.Bold));
        private static Text Text0 = new EloBuddy.SDK.Rendering.Text("", new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 9, System.Drawing.FontStyle.Bold));
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

            if (DrawingsMenu.GetCheckBoxValue("wDraw") && readyDraw ? W.IsReady() : DrawingsMenu.GetCheckBoxValue("wDraw"))
            {
                Circle.Draw(WColorSlide.GetSharpColor(), W.Range, 1f, Player.Instance);
            }

            if (DrawingsMenu.GetCheckBoxValue("eDraw") && readyDraw ? E.IsReady() : DrawingsMenu.GetCheckBoxValue("eDraw"))
            {
                Circle.Draw(EColorSlide.GetSharpColor(), E.Range, 1f, Player.Instance);
            }

            if (DrawingsMenu.GetCheckBoxValue("rDraw") && readyDraw ? R.IsReady() : DrawingsMenu.GetCheckBoxValue("rDraw"))
            {
                Circle.Draw(RColorSlide.GetSharpColor(), R.Range, 1f, Player.Instance);
            }
                Minion = (Obj_AI_Minion)EntityManager.MinionsAndMonsters.Monsters.FirstOrDefault(buff => Program._player.IsInRange(buff, 570) && (buff.Name.StartsWith(buff.BaseSkinName) || Program.BuffsThatActuallyMakeSenseToSmite.Contains(buff.BaseSkinName)) && !buff.Name.Contains("Mini") && !buff.Name.Contains("Spawn"));
                AIHeroClient target = TargetSelector.GetTarget(570, DamageType.Magical);
                var playerPos = Drawing.WorldToScreen(Program._player.Position);
                var enemyPos = Drawing.WorldToScreen(target.Position);
                var MonsterPos = Drawing.WorldToScreen(Minion.Position);
                var smitedraw = DrawingsMenu.GetCheckBoxValue("smitedraw");

                if (Smite.IsReady() && DrawingsMenu.GetCheckBoxValue("smitedraw"))
                {
                    //Drawing.DrawText(playerPos.X - 70, playerPos.Y + 40, Color.GhostWhite, "Smite active");
                    Text0.Position = Drawing.WorldToScreen(Player.Instance.Position) - new Vector2(100, -40);
                    Text0.Color = Color.AliceBlue;
                    Text0.TextValue = "Smite active";
                    Text0.Draw();
                }

                if (Smite.IsOnCooldown && DrawingsMenu.GetCheckBoxValue("smitedraw"))
                {
                    Drawing.DrawText(playerPos.X - 70, playerPos.Y + 40, Color.Red, "Smite cooldown");
                }

                if (target.Health < Program.SmiteDmgHero(target) && MiscMenu.GetCheckBoxValue("sks") && smitedraw)
                {
                    Drawing.DrawText(enemyPos.X - 70, enemyPos.Y + 40, Color.Fuchsia, "Smite killable");
                }

                if (Minion.Health < Program.SmiteDmgMonster(Minion) && MiscMenu.GetCheckBoxValue("sjgl"))
                {
                    Drawing.DrawText(MonsterPos.X - 70, MonsterPos.Y + 40, Color.IndianRed, "Smite killable");
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

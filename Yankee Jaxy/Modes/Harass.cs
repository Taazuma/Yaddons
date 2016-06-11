using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using static Eclipse.SpellsManager;

namespace Eclipse.Modes
{
    internal class Harass
    {
        private static bool IsEUsed => Player.HasBuff("JaxCounterStrike");
        public static void Execute()
        {
            var ttarget = TargetSelector.GetTarget(E.Range + 200, DamageType.Mixed);

            if (ttarget == null) return;

            AIHeroClient target = TargetSelector.GetTarget(700, DamageType.Magical);

            if (E.IsReady() && (Program.getCheckBoxItem(Menus.HarassMenu, "eUse")))
            {
                if ((!IsEUsed && E.IsReady() && target.IsValidTarget(Q.Range)) || (!IsEUsed && Program._player.Distance(target.Position) < 250))
                {
                    E.Cast();
                }

                if (Program.getCheckBoxItem(Menus.HarassMenu, "e2Use") && IsEUsed && (Program._player.Distance(target.Position) < 180))
                {
                    E.Cast();
                }
            }

        }
    }
}

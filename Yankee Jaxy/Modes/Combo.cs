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
using static Eclipse.Menus;

namespace Eclipse.Modes
{
    internal class Combo
    {
        private static bool IsEUsed => Player.HasBuff("JaxCounterStrike");

        public static void Execute()
        {
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Magical);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            var rtarget = TargetSelector.GetTarget(R.Range, DamageType.Mixed);
            AIHeroClient target = TargetSelector.GetTarget(700, DamageType.Magical);

            // ACTUAL COMBO
            if (target != null && !target.IsZombie)
            {
                if (Q.IsReady() && Program.getCheckBoxItem(ComboMenu, "qUse"))
                {
                    if ((Program._player.Distance(target.Position) > Program._player.GetAutoAttackRange(Program._player)) || Program.getCheckBoxItem(ComboMenu, "q2Use"))
                    {
                        Q.Cast(target);
                    }
                }

                if (E.IsReady() && (Program.getCheckBoxItem(ComboMenu, "eUse")))
                {
                    if ((!IsEUsed && Q.IsReady() && target.IsValidTarget(Q.Range)) || (!IsEUsed && Program._player.Distance(target.Position) < 250))
                    {
                        E.Cast();
                    }
                    if (Program.getCheckBoxItem(ComboMenu, "e2Use") && IsEUsed && (Program._player.Distance(target.Position) < 180))
                    {
                        E.Cast();
                    }
                }

                if (target.HealthPercent > 20)
                {
                    if ((Program.getCheckBoxItem(ComboMenu, "rUse") && Q.IsReady() && R.IsReady()) ||
                        (Program.getCheckBoxItem(ComboMenu, "rUse") && R.IsReady() && !Q.IsReady() &&
                         Program._player.Distance(target.Position) < 300)) R.Cast();
                }

            }



        }
    }
}
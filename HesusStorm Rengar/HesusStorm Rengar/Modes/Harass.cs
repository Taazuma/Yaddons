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
    /// <summary>
    /// This mode will run when the key of the orbwalker is pressed
    /// </summary>
    internal class Harass
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }
        public static readonly AIHeroClient Player = ObjectManager.Player;
        public static void Execute()
        {
            var target = TargetSelector.GetTarget(2000, DamageType.Physical);
            var rtarget = TargetSelector.GetTarget(R.Range, DamageType.Magical);
            var useQ = HarassMenu.GetCheckBoxValue("qUse");
            var useW = HarassMenu.GetCheckBoxValue("wUse");
            var useE = HarassMenu.GetCheckBoxValue("eUse");
            var useR = HarassMenu.GetCheckBoxValue("rUse");
            var useEE = HarassMenu.GetCheckBoxValue("UseEEC");
            var tqq = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            var tee = TargetSelector.GetTarget(E.Range, DamageType.Physical);

            if (useQ && tqq.IsValidTarget(SpellsManager.Q.Range) && Q.IsReady())
            {
                Q.Cast();
            }

            if (useE && tee.IsValidTarget(E.Range) && E.IsReady() && E.GetPrediction(tee).HitChance >= Hitch.hitchance(E, FirstMenu))
            {
                E.Cast(tee);
            }

            if (useW && tqq.IsValidTarget(SpellsManager.W.Range) && W.IsReady())
            {
                W.Cast();
            }



        }
    }
}
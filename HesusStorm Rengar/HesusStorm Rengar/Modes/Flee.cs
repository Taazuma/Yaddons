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
using static Eclipse.Menus;
using EloBuddy.SDK.Menu;
using static Eclipse.SpellsManager;

//using Settings = RoninTune.Modes.Flee

namespace Eclipse.Modes
{
    internal class Flee
    {
        private static AIHeroClient _player;
        public static void Execute()
        {
            var ttarget = TargetSelector.GetTarget(1000, DamageType.Physical);
            if (E.IsReady() && Player.Instance.Mana == 5 && MiscMenu.GetCheckBoxValue("eflee") && E.GetPrediction(ttarget).HitChance >= Hitch.hitchance(E, FirstMenu))
            {
                var target = TargetSelector.GetTarget(1000, DamageType.Physical);
                E.Cast(target);
            }

           else if (E.IsReady() && MiscMenu.GetCheckBoxValue("eflee"))
            {
                var target = TargetSelector.GetTarget(1000, DamageType.Physical);
                E.Cast(target);
            }

        }
    }
}




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
using System.Diagnostics;

namespace Eclipse.Modes
{
    internal class Active
    {
        public static Obj_AI_Minion Minion;
        public static void Execute()
        {
            Minion = (Obj_AI_Minion)EntityManager.MinionsAndMonsters.Monsters.FirstOrDefault(buff => Program._player.IsInRange(buff, 570) && (buff.Name.StartsWith(buff.BaseSkinName) || Program.BuffsThatActuallyMakeSenseToSmite.Contains(buff.BaseSkinName)) && !buff.Name.Contains("Mini") && !buff.Name.Contains("Spawn"));
            AIHeroClient target = TargetSelector.GetTarget(570, DamageType.Magical);

            if (Minion.IsValidTarget(570) && Minion.Health < Program.SmiteDmgMonster(Minion) && MiscMenu.GetCheckBoxValue("sjgl"))
            {
                Smite.Cast(Minion);
            }

            if (target.IsValidTarget(570) && target.Health < Program.SmiteDmgMonster(Minion) && MiscMenu.GetCheckBoxValue("sks"))
            {
                Smite.Cast(target);
            }

        }
    }
}

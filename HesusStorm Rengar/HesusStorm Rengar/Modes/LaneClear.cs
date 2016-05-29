using static Eclipse.SpellsManager;
using static Eclipse.Menus;
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
namespace Eclipse.Modes
{
    internal class LaneClear
    {
        public static AIHeroClient _player
        {
            get { return ObjectManager.Player; }
        }
        public static void Execute()
        {
            var qtarget = EntityManager.MinionsAndMonsters.EnemyMinions.FirstOrDefault(x => !x.IsDead && Q.IsInRange(x));
            var wtarget = EntityManager.MinionsAndMonsters.EnemyMinions.FirstOrDefault(x => !x.IsDead && W.IsInRange(x));
            var etarget = EntityManager.MinionsAndMonsters.EnemyMinions.FirstOrDefault(x => !x.IsDead && E.IsInRange(x));

            if (_player.Mana <= 1-4)
            { 
            
                if (LaneClearMenu.GetCheckBoxValue("qUse") && Q.IsReady())
            {
                Program.Items();
                Q.Cast();
                Orbwalker.ForcedTarget = qtarget;
            }

            if (LaneClearMenu.GetCheckBoxValue("wUse") && W.IsReady())
            {
                W.Cast();
            }
            if (LaneClearMenu.GetCheckBoxValue("eUse") && E.IsReady() && E.GetPrediction(etarget).HitChance >= Hitch.hitchance(E, FirstMenu))
            {
                E.Cast(etarget);
            }
            }

            if (_player.Mana <= 5 && LaneClearMenu.GetCheckBoxValue("wUse") && W.IsReady() && LaneClearMenu.GetCheckBoxValue("WClearHeal"))
            {
                W.Cast();
            }

        }
    }
}

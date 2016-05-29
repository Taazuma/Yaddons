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
    internal class JungleClear
    {
        public static AIHeroClient _player
        {
            get { return ObjectManager.Player; }
        }
        public static void Execute()
        {
            var target = EntityManager.MinionsAndMonsters.GetJungleMonsters().OrderByDescending(a => a.MaxHealth).FirstOrDefault(a => a.IsValidTarget(900));

            if (JungleClearMenu.GetCheckBoxValue("qUse") && Q.IsReady())
            {
                Program.Items();
                Q.Cast();
                Orbwalker.ForcedTarget = target;
            }
            if (JungleClearMenu.GetCheckBoxValue("wUse") && W.IsReady())
            {
                W.Cast();
            }
            if (JungleClearMenu.GetCheckBoxValue("eUse") && E.IsReady() && E.GetPrediction(target).HitChance >= Hitch.hitchance(E, FirstMenu))
            {
                E.Cast(target);
            }

            var save = JungleClearMenu.GetCheckBoxValue("JungleSave");
            if (_player.Mana != 5 || save)
            {
                return;
            }

            if (target.IsValidTarget(Q.Range) && Q.IsReady()
                && FirstMenu.GetComboBoxValue("JunglePrio") == 0 && JungleClearMenu.GetCheckBoxValue("qUse"))
            {
                Q.Cast();
            }

            if (target.IsValidTarget(W.Range) && W.IsReady()
            && FirstMenu.GetComboBoxValue("JunglePrio") == 1 && JungleClearMenu.GetCheckBoxValue("wUse")
             && !_player.HasBuff("rengarpassivebuff"))
            {
                W.Cast();
            }

            Program.Items();

            if (target.IsValidTarget(E.Range) && E.IsReady()
            && FirstMenu.GetComboBoxValue("JunglePrio") == 2 && JungleClearMenu.GetCheckBoxValue("eUse"))
            {
                E.Cast(target.ServerPosition);
            }

        }
    }
}

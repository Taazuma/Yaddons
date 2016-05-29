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
        public static AIHeroClient _player
        {
            get { return ObjectManager.Player; }
        }

        public static void Execute()
        {
            var target = TargetSelector.GetTarget(2000, DamageType.Physical);
            var rtarget = TargetSelector.GetTarget(R.Range, DamageType.Magical);
            var useQ = ComboMenu.GetCheckBoxValue("qUse");
            var useW = ComboMenu.GetCheckBoxValue("wUse");
            var useE = ComboMenu.GetCheckBoxValue("eUse");
            var useR = ComboMenu.GetCheckBoxValue("rUse");
            var useEE = ComboMenu.GetCheckBoxValue("UseEEC");
            var tqq = TargetSelector.GetTarget(Q.Range, DamageType.Physical);

            if(_player.HasBuff("RengarR"))
            {
                Program.ItemsYuno();
            }

            if (useR)
            {
                R.Cast();
            }

      if (_player.Mana <= 4)

          { 
                var tqqq = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (tqqq.IsValidTarget(Q.Range) && Q.IsReady() && useQ)
            {
                Q.Cast();
            }

            var tww = TargetSelector.GetTarget(W.Range, DamageType.Magical);
            if (tww.IsValidTarget(W.Range) && W.IsReady() && !_player.HasBuff("rengarpassivebuff") && useW)
            {
                W.Cast();
            }


            var tee = TargetSelector.GetTarget(E.Range, DamageType.Physical);
            var predE = E.GetPrediction(tee);
            if (!_player.HasBuff("rengarpassivebuff") && tee.IsValidTarget(E.Range) && E.IsReady() && E.GetPrediction(tee).HitChance >= Hitch.hitchance(E, FirstMenu) && useE)
            {
                E.Cast(tee);
            }
                Program.Items();
            }

            if (_player.Mana <= 5)
            {
                var tq = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                if (useQ && (FirstMenu.GetComboBoxValue("ComboPrio") == 0
                        || (FirstMenu.GetComboBoxValue("ComboPrio") == 2)))
                    if (tq.IsValidTarget(Q.Range) && Q.IsReady()) Q.Cast();

                if (useW && FirstMenu.GetComboBoxValue("ComboPrio") == 1)
                {
                    var tw = TargetSelector.GetTarget(W.Range, DamageType.Magical);
                    if (tw.IsValidTarget(W.Range) && W.IsReady() && !_player.HasBuff("rengarpassivebuff")) W.Cast();
                }

                if (useE && FirstMenu.GetComboBoxValue("ComboPrio") == 2)
                {
                    var te = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                    if (te.IsValidTarget(E.Range) && E.IsReady() && !_player.HasBuff("rengarpassivebuff") && E.GetPrediction(te).HitChance >= Hitch.hitchance(E, FirstMenu)) E.Cast(te);
                }

                if (useEE && !_player.HasBuff("RengarR")
                     && (FirstMenu.GetComboBoxValue("ComboPrio") == 2
                         || FirstMenu.GetComboBoxValue("ComboPrio") == 0))
                {
                    var te = TargetSelector.GetTarget(E.Range, DamageType.Physical);

                    if (_player.Distance(te) > E.Range + 100f)
                    {
                        if (te.IsValidTarget(E.Range) && E.IsReady() && E.GetPrediction(te).HitChance >= Hitch.hitchance(E, FirstMenu)) E.Cast(te);
                    }
                }
                Program.Items();
            }

            if (MiscMenu.GetCheckBoxValue("AutoW"))
            {
                    var HealthW = MiscMenu["AutoWHP"].Cast<Slider>().CurrentValue;
                    if (W.IsReady() && _player.HealthPercent != HealthW && Player.Instance.Mana == 5)
                    {
                        W.Cast();
                    }
                }
            

        }
    }
}
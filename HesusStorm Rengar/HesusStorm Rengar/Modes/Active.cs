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
    internal class Active
    {
        private static AIHeroClient _player;
        public static void Execute()
        {
            //////////////////// KS Q
            if (KillStealMenu.GetCheckBoxValue("qUse"))
            {
                var qtarget = TargetSelector.GetTarget(W.Range, DamageType.Magical);

                if (qtarget == null) return;

                if (Q.IsReady())
                {
                    var rDamage = qtarget.GetDamage(SpellSlot.Q);

                    var predictedHealth = Prediction.Health.GetPrediction(qtarget, Q.CastDelay + Game.Ping);

                    if (predictedHealth <= rDamage)
                    {
                        var rangi = TargetSelector.GetTarget(_player.GetAutoAttackRange(), DamageType.Physical);
                        if (rangi.IsValidTarget())
                        {
                            Q.Cast();
                        }
                    }
                }
            }            //////////////////// END KS Q

                         //////////////////// KS W
            if (KillStealMenu.GetCheckBoxValue("wUse"))
            {
                var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Magical);

                if (wtarget == null) return;

                if (W.IsReady())
                {
                    var rDamage = wtarget.GetDamage(SpellSlot.W);

                    var predictedHealth = Prediction.Health.GetPrediction(wtarget, W.CastDelay + Game.Ping);

                    if (predictedHealth <= rDamage)
                    {
                            W.Cast();
                        }
                    }
                }
            //////////////////// KS W END


            //////////////////// KS E
            if (KillStealMenu.GetCheckBoxValue("eUse"))
            {
                var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);

                if (etarget == null) return;

                if (E.IsReady())
                {
                    var rDamage = etarget.GetDamage(SpellSlot.E);

                    var predictedHealth = Prediction.Health.GetPrediction(etarget, E.CastDelay + Game.Ping);

                    if (predictedHealth <= rDamage)
                    {
                        var pred = E.GetPrediction(etarget);
                        if (pred.HitChancePercent >= 90)
                        {
                            E.Cast(etarget);
                        }
                    }
                }
            }//////////////////// KS E END

        //    //HEAL W
        //if (MiscMenu.GetCheckBoxValue("AutoW"))
        //{
        //    var health = (100 * (_player.Health / _player.MaxHealth)) < MiscMenu.GetSliderValue("AutoWHP");

        //    if (_player.HasBuff("Recall") || _player.Mana <= 4) return;


        //    if (W.IsReady() && health)
        //    {
        //        W.Cast();
        //    }
        //    }            //HEAL W END

            // ZED ULT HEAL
            if (Player.HasBuff("zedulttargetmark") && MiscMenu.GetCheckBoxValue("ZedHeal4") && Player.Instance.Mana == 4)
            {
                if (W.IsReady())
                {
                    W.Cast();
                }
            }           // ZED ULT HEAL END

                        // ZED ULT HEAL 2
            if (Player.HasBuff("zedulttargetmark") && MiscMenu.GetCheckBoxValue("ZedHeal5") && Player.Instance.Mana == 5)
            {
                if (W.IsReady())
                {
                    W.Cast();
                }
            }           // ZED ULT HEAL 2 END



        }
    }
}

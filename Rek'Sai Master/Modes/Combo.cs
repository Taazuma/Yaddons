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
        public static void Execute()
        {
            var t = TargetSelector.GetTarget(Q2.Range, DamageType.Physical);
            var reksaifury = Equals(Program._player.Mana, Program._player.MaxMana);

          //  if (Program.IsBurrowed())
         //   {
             //   if (Program.getCheckBoxItem(ComboMenu, "UseEBCombo"))
             //   {
             //       var te = TargetSelector.GetTarget(E2.Range + W.Range, DamageType.Physical);
            //        if (E2.IsReady() && te.IsValidTarget(E2.Range + W.Range) && Program._player.Distance(te) > Q.Range && E2.GetPrediction(te).HitChance >= Hitch.hitchance(E2, FirstMenu))
            //        {
            //            E2.Cast(te);
            //        }
            //    }
            //    if (Program.getCheckBoxItem(ComboMenu, "UseQBCombo"))
            //    {
            //        var tbq = TargetSelector.GetTarget(Q2.Range, DamageType.Magical);
            //        var predq = Q2.GetPrediction(tbq).HitChance >= Hitch.hitchance(Q2, FirstMenu);
            //        if (Q2.IsReady() && predq && t.IsValidTarget(Q2.Range)) Q2.Cast(tbq);
            //    }

            //    if (Program.getCheckBoxItem(ComboMenu, "UseWCombo"))
            //    {
            //        var tw = TargetSelector.GetTarget(W.Range, DamageType.Physical);
            //        if (W.IsReady() && tw.IsValidTarget(W.Range) && !Q2.IsReady())
            //        {
            //            W.Cast(t);
            //        }
            //    }
                if (Program.burrowed)
                {
                    var targetW = TargetSelector.GetTarget(Player.Instance.BoundingRadius + 175, DamageType.Physical);
                    var targetQ2 = TargetSelector.GetTarget(850, DamageType.Magical);

                    var targetE = TargetSelector.GetTarget(550, DamageType.Physical);
                    var targetE2 = TargetSelector.GetTarget(E2.Range, DamageType.Physical);
                    var predE2 = E2.GetPrediction(targetE2);

                    if (ComboMenu.GetCheckBoxValue("UseWCombo") && W.IsReady())
                    {

                        if (targetW != null && targetW.IsValidTarget())
                        {
                            W.Cast();
                            return;
                        }
                    }

                   if (Program.getCheckBoxItem(ComboMenu, "UseQBCombo"))
                   {
                       var tbq = TargetSelector.GetTarget(Q2.Range, DamageType.Magical);
                        var predqq = Q2.GetPrediction(tbq).HitChance >= Hitch.hitchance(Q2, FirstMenu);
                       if (Q2.IsReady() && predqq && t.IsValidTarget(Q2.Range)) Q2.Cast(tbq);
                    }

                var predq = Q2.GetPrediction(targetQ2).HitChance >= Hitch.hitchance(Q2, FirstMenu);
                if (ComboMenu.GetCheckBoxValue("UseQBCombo") && Q2.IsReady() && predq)
                    {

                        if (targetQ2 != null && targetQ2.IsValidTarget())
                        {
                            var predictionQ2 = Q2.GetPrediction(targetQ2);
                            if (predictionQ2.HitChance >= HitChance.Medium)
                            {
                                Q2.Cast(predictionQ2.CastPosition);
                                return;
                            }
                        }
                    }

                if (Program.getCheckBoxItem(ComboMenu, "UseEBCombo"))
               {
                   var te = TargetSelector.GetTarget(E2.Range + W.Range, DamageType.Physical);
                    if (E2.IsReady() && te.IsValidTarget(E2.Range + W.Range) && Program._player.Distance(te) > Q.Range && E2.GetPrediction(te).HitChance >= Hitch.hitchance(E2, FirstMenu))
                    {
                        E2.Cast(te.ServerPosition);
                    }
                }
            }

            //}


            //if (!Program.IsBurrowed())
            //{
            //    if (Program.getCheckBoxItem(ComboMenu, "UseQCombo"))
            //    {
            //        var tq = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            //        if (Q.IsReady() && tq.IsValidTarget(Q.Range)) Q.Cast(t);
            //    }

            //    if (Program.getCheckBoxItem(ComboMenu, "UseECombo"))
            //    {
            //        var te = TargetSelector.GetTarget(E.Range, DamageType.Physical);
            //        if (te.IsValidTarget(E.Range) && E.IsReady())
            //        {
            //            if (reksaifury)
            //            {
            //                E.Cast(te);
            //            }
            //            else if (Program._player.Mana < 100)
            //            {
            //                E.Cast(te);
            //            }
            //        }
            //    }

            //    if (Program.getCheckBoxItem(ComboMenu, "UseWCombo") && W.IsReady())
            //    {
            //        var tw = TargetSelector.GetTarget(Q2.Range, DamageType.Physical);
            //        if (!Q.IsReady() && !tw.IsValidTarget(E.Range) && tw.IsValidTarget(Q2.Range))
            //            W.Cast();
            //    }
            if (!Program.burrowed)
                    {
                        var targetE = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                        var lastTarget = Orbwalker.LastTarget;
                        var target = TargetSelector.GetTarget(300, DamageType.Physical);

                        if (ComboMenu.GetCheckBoxValue("UseECombo") && E.IsReady())
                        {

                            if (targetE != null && targetE.IsValidTarget())
                            {
                                E.Cast(targetE);
                                return;
                            }
                        }

                        if (ComboMenu.GetCheckBoxValue("UseWCombo") && W.IsReady())
                        {
                            if (Player.Instance.CountEnemiesInRange(400) == 0)
                            {
                                W.Cast();
                                return;
                            }
                        }

                        if (ComboMenu.GetCheckBoxValue("UseWCombo") && W.IsReady())
                        {
                            if (lastTarget.IsValidTarget(Player.Instance.BoundingRadius + 250) && !target.HasBuff("reksaiknockupimmune"))
                            {
                                W.Cast();
                                return;
                            }
                        }
                    }


            }
        }
    }

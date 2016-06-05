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
using Eclipse.Modes;
using EloBuddy.SDK.Menu;

namespace Eclipse
{
    internal class Program
    {
        public static AIHeroClient _player
        {
            get { return ObjectManager.Player; }
        }
        private static int _lastTick;
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            //Put the name of the champion here
            if (Player.Instance.ChampionName != "Rengar") return;
            SpellsManager.InitializeSpells();
            DrawingsManager.InitializeDrawings();
            Menus.CreateMenu();
            ModeManager.InitializeModes();
            Interrupter.OnInterruptableSpell += Program.Interrupter2_OnInterruptableTarget;
            Orbwalker.OnPreAttack += OnBeforeAttack;
            Orbwalker.OnPostAttack += OnAfterAttack;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
        }

        private static void OnAfterAttack(AttackableUnit target, EventArgs args)
        {
            var combo = Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
            var Q = ComboMenu.GetCheckBoxValue("qUse");
            if (!target.IsMe) return;
            if (combo && Q && target.IsValidTarget(SpellsManager.Q.Range) && SpellsManager.Q.IsReady())
            {
                SpellsManager.Q.Cast();
            }
        }

        private static void OnBeforeAttack(AttackableUnit target, Orbwalker.PreAttackArgs args)
        {
            var combo = Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
            var harass = Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
            var QC = ComboMenu.GetCheckBoxValue("qUse");
            var QH = HarassMenu.GetCheckBoxValue("qUse");
            var mode = FirstMenu.GetComboBoxValue("ComboPrio") == 0
                       || FirstMenu.GetComboBoxValue("ComboPrio") == 2;
            if (!(args.Target is AIHeroClient))
            {
                return;
            }

            if (_player.HasBuff("rengarpassivebuff") || _player.HasBuff("RengarR"))
            {
                return;
            }

            if (_player.Mana <= 4)
            {
                if (combo && QC && Q.IsReady() && args.Target.IsValidTarget(Q.Range))
                {
                    Q.Cast();
                }

                if (harass && QH && Q.IsReady() && args.Target.IsValidTarget(Q.Range))
                {
                    Q.Cast();
                }
            }

            if (_player.Mana == 5)
            {
                if (combo && QC && Q.IsReady() && mode && args.Target.IsValidTarget(Q.Range))
                {
                    Q.Cast();
                }


                if (harass && QH && Q.IsReady() && mode && args.Target.IsValidTarget(Q.Range))
                {
                    Q.Cast();
                }
            }

        }

        public static bool getCheckBoxItem(Menu m, string item)
        {
            return m[item].Cast<CheckBox>().CurrentValue;
        }

        public static int getSliderItem(Menu m, string item)
        {
            return m[item].Cast<Slider>().CurrentValue;
        }

        public static bool getKeyBindItem(Menu m, string item)
        {
            return m[item].Cast<KeyBind>().CurrentValue;
        }

        public static int getBoxItem(Menu m, string item)
        {
            return m[item].Cast<ComboBox>().CurrentValue;
        }

        public static void ChangeComboMode()
        {
            var changetime = Environment.TickCount - _lastTick;


            if (getKeyBindItem(FirstMenu, "Switch"))
            {
                if (getBoxItem(FirstMenu, "ComboPrio") == 0 && _lastTick + 400 < Environment.TickCount)
                {
                    _lastTick = Environment.TickCount;
                    FirstMenu["ComboPrio"].Cast<ComboBox>().CurrentValue = 1;
                }

                if (getBoxItem(FirstMenu, "ComboPrio") == 1 && _lastTick + 400 < Environment.TickCount)
                {
                    _lastTick = Environment.TickCount;
                    FirstMenu["ComboPrio"].Cast<ComboBox>().CurrentValue = 2;
                }
                if (getBoxItem(FirstMenu, "ComboPrio") == 2 && _lastTick + 400 < Environment.TickCount)
                {
                    _lastTick = Environment.TickCount;
                    FirstMenu["ComboPrio"].Cast<ComboBox>().CurrentValue = 0;
                }
            }
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            var spell = args.SData;
            if (!sender.IsMe)
            {
                return;
            }

            if (spell.Name.ToLower().Contains("rengarq") || spell.Name.ToLower().Contains("rengare"))
            {
                Orbwalker.ResetAutoAttack();
            }
        }

        public static void Items()
        {
            if (Item.HasItem(3074) && Item.CanUseItem(3074)) // Hydra
                Item.UseItem(3074);
            if (Item.HasItem(3077) && Item.CanUseItem(3077)) // Tiamat
                Item.UseItem(3077);
            if (Item.HasItem(3748) && Item.CanUseItem(3748)) // Titanic Hydra
                Item.UseItem(3748);
        }
        public static void ItemsYuno()
        {
            if (Item.HasItem(3142) && Item.CanUseItem(3142)) // Youmuu's
                Item.UseItem(3142);
        }

        public static void AutoHeal()
        {
            var health = MiscMenu.GetSliderValue("AutoWHP");

            if (_player.HasBuff("Recall") || _player.Mana <= 5) return;

            if (W.IsReady() && _player.HealthPercent <= health)
            {
                W.Cast();
            }
        }

        private static void Interrupter2_OnInterruptableTarget(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (_player.Mana < 5) return;
            if (E.IsReady() && sender.IsValidTarget(E.Range) && MiscMenu.GetCheckBoxValue("UseEInt"))
            {
                var predE = E.GetPrediction(sender);
                if (E.GetPrediction(sender).HitChance >= Hitch.hitchance(E, FirstMenu) && !_player.HasBuff("rengarpassivebuff")) E.Cast(sender.Position);
            }
        }


    }
}
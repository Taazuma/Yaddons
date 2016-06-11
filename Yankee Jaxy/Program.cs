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
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }
        public static AIHeroClient _player
        {
            get { return ObjectManager.Player; }

        }
        public const float SmiteRange = 570;
        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            //Put the name of the champion here
            if (Player.Instance.ChampionName != "Jax") return;
            SpellsManager.InitializeSpells();
            DrawingsManager.InitializeDrawings();
            Menus.CreateMenu();
            ModeManager.InitializeModes();
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            OnDoCast();
            Killsteal();
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

        public static float SmiteDmgMonster(Obj_AI_Base target)
        {
            return Player.Instance.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Smite);
        }

        public static float SmiteDmgHero(AIHeroClient target)
        {
            return Player.Instance.CalculateDamageOnUnit(target, DamageType.True,
                20.0f + Player.Instance.Level * 8.0f);
        }


              public static readonly string[] BuffsThatActuallyMakeSenseToSmite =
             {
                "SRU_Red", "SRU_Blue", "SRU_Dragon_Water",  "SRU_Dragon_Fire", "SRU_Dragon_Earth", "SRU_Dragon_Air", "SRU_Dragon_Elder",
                "SRU_Baron", "SRU_Gromp", "SRU_Murkwolf",
                "SRU_Razorbeak", "SRU_RiftHerald",
                "SRU_Krug", "Sru_Crab", "TT_Spiderboss",
                "TT_NGolem", "TT_NWolf", "TT_NWraith"
            };

        public static readonly string[] DragonSmite =
            {
                "SRU_Red", "SRU_Blue", "SRU_Dragon_Water",  "SRU_Dragon_Fire", "SRU_Dragon_Earth", "SRU_Dragon_Air", "SRU_Dragon_Elder"
            };

        public static readonly string[] BaronSmite =
          {
                "SRU_Baron"
            };

        public static readonly string[] BlueSmite =
            {
                "SRU_Blue"
            };

        public static readonly string[] RedSmite =
             {
                "SRU_Red"
            };

        public static readonly string[] HeraldSmite =
            {
                "SRU_RiftHerald"
            };

        public static readonly string[] FroschS =
          {
                "SRU_Gromp"
            };

        public static readonly string[] WolfS =
          {
                "SRU_Murkwolf"
            };

        public static readonly string[] SteinS =
             {
                "SRU_Krug"
            };

        public static readonly string[] ChicksS =
             {
                "SRU_Razorbeak"
            };

        public static readonly string[] KrabbeS =
          {
                "Sru_Crab"
            };

        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (args.Target != null && args.Target.IsMe && args.SData.IsAutoAttack() && Program.getCheckBoxItem(Menus.JungleClearMenu, "eUse") && E.IsReady() && (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear)) && sender.Team == GameObjectTeam.Neutral)
            {
                E.Cast();
            }
        }

        static int CanKill(AIHeroClient target, bool useq)
        {
            double damage = 0;
            if (!useq)
                return 0;
            if (Q.IsReady())
            {
                damage += ObjectManager.Player.GetSpellDamage(target, SpellSlot.Q);
            }
            if (damage >= target.Health)
            {
                return 1;
            }
            return damage >= target.Health ? 2 : 0;

        }

        private static void Killsteal()
        {
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            if (qtarget == null || qtarget.HasBuffOfType(BuffType.Invulnerability))
                    return;

                if (CanKill(qtarget, getCheckBoxItem(KillStealMenu, "qUse")) == 1 && qtarget.IsValidTarget(390))
                {
                    Q.Cast(qtarget);
                    return;
                }
        }

        private static void OnDoCast()
        {
            Obj_AI_Base.OnSpellCast += (sender, args) =>
            {
                //if (!sender.IsMe || !Orbwalking.IsAutoAttack((args.SData.Name))) return;
                if (sender.IsMe && args.SData.IsAutoAttack())
                {
                    if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                    {
                        if (getCheckBoxItem(ComboMenu, "wUse") && W.IsReady()) W.Cast();
                    }
                }
            };
        }


    }
}
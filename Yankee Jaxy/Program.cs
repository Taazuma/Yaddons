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

        static float WardTick;

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
            if (!SpellManager.HasSmite())
            {
                Chat.Print("No smite detected - unloading Smite.", System.Drawing.Color.Red);
                return;
            }
            Config.Initialize();
            ModeManagerSmite.Initialize();
            Events.Initialize();
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

        private static readonly Item[] Wards =
             {
                    new Item(ItemId.Warding_Totem_Trinket, 600f), new Item(ItemId.Sightstone, 600f), new Item(ItemId.Ruby_Sightstone, 600f),
                    new Item(ItemId.Eye_of_the_Oasis, 600f), new Item(ItemId.Eye_of_the_Equinox, 600f), new Item(ItemId.Trackers_Knife, 600f),
                    new Item(ItemId.Trackers_Knife_Enchantment_Warrior, 600f), new Item(ItemId.Trackers_Knife_Enchantment_Runic_Echoes, 600f),
                    new Item(ItemId.Trackers_Knife_Enchantment_Sated_Devourer, 600f), new Item(ItemId.Trackers_Knife_Enchantment_Devourer, 600f),
                    new Item(ItemId.Trackers_Knife_Enchantment_Cinderhulk, 600f), new Item(ItemId.Eye_of_the_Watchers, 600f),
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


        public static void WardJumper()
        {
            //----------------------------------------------Ward Jump---------------------------------------
            if (Q.IsReady() && MiscMenu.GetKeyBindValue("wardjump") && Environment.TickCount - WardTick >= 2000)
            {
                var CursorPos = Game.CursorPos;
                float WardTick;
                Obj_AI_Base JumpPlace = EntityManager.Heroes.Allies.FirstOrDefault(it => it.Distance(CursorPos) <= 250 && Q.IsInRange(it));

                if (JumpPlace != default(Obj_AI_Base)) Q.Cast(JumpPlace);
                else
                {
                    JumpPlace = EntityManager.MinionsAndMonsters.Minions.FirstOrDefault(it => it.Distance(CursorPos) <= 250 && Q.IsInRange(it));

                    if (JumpPlace != default(Obj_AI_Base)) Q.Cast(JumpPlace);
                    else if (JumpWard() != default(InventorySlot))
                    {
                        var Ward = JumpWard();
                        CursorPos = _player.Position.Extend(CursorPos, 600).To3D();
                        Ward.Cast(CursorPos);
                        WardTick = Environment.TickCount;
                        Core.DelayAction(() => WardJump(CursorPos), Game.Ping + 100);
                    }
                }

            }
        }


        //------------------------------------|| Methods ||--------------------------------------Credits to WU

        //---------------------------------------------WardJump()-------------------------------------------------Credits to WU

        public static void WardJump(Vector3 cursorpos)
        {
            var Ward = ObjectManager.Get<Obj_AI_Base>().FirstOrDefault(it => it.IsValidTarget(Q.Range) && it.Distance(cursorpos) <= 250);
            if (Ward != null) Q.Cast(Ward);
        }

        //---------------------------------------------JumpWard()-------------------------------------------------- Credits to WU

        public static InventorySlot JumpWard()
        {
            var Inventory = Program._player.InventoryItems;

            if (Item.CanUseItem(3340)) return Inventory.First(it => it.Id == ItemId.Warding_Totem_Trinket);
            if (Item.CanUseItem(2049)) return Inventory.First(it => it.Id == ItemId.Sightstone);
            if (Item.CanUseItem(2045)) return Inventory.First(it => it.Id == ItemId.Ruby_Sightstone);
            if (Item.CanUseItem(3711)) return Inventory.First(it => (int)it.Id == 3711); //Tracker's Knife
            if (Item.CanUseItem(2301)) return Inventory.First(it => (int)it.Id == 2301); //Eye of the Watchers
            if (Item.CanUseItem(2302)) return Inventory.First(it => (int)it.Id == 2302); //Eye of the Oasis
            if (Item.CanUseItem(2303)) return Inventory.First(it => (int)it.Id == 2303); //Eye of the Equinox
            if (Item.CanUseItem(2043)) return Inventory.First(it => it.Id == ItemId.Vision_Ward);

            return default(InventorySlot);
        }
        // Credits END WU


    }
}
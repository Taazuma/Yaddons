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

        //public static void WardJump()
        //{
        //    EloBuddy.Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
        //    if (!Q.IsReady())
        //    {
        //        return;
        //    }
        //    Vector3 wardJumpPosition = (_player.Position.Distance(Game.CursorPos) < 600) ? Game.CursorPos : _player.Position.Distance(Game.CursorPos, 600);
        //    var lstGameObjects = ObjectManager.Get<Obj_AI_Base>().ToArray();
        //    Obj_AI_Base entityToWardJump = lstGameObjects.FirstOrDefault(obj =>
        //        obj.Position.Distance(wardJumpPosition) < 150
        //        && (obj is Obj_AI_Minion || obj is AIHeroClient)
        //        && !obj.IsMe && !obj.IsDead
        //        && obj.Position.Distance(_player.Position) < Q.Range);

        //    if (entityToWardJump != null)
        //    {
        //        Q.Cast(entityToWardJump);
        //    }
        //    else
        //    {
        //        int wardId = GetWardItem();


        //        if (wardId != -1 && !wardJumpPosition.IsWall())
        //        {
        //            PutWard(wardJumpPosition.To2D(), (ItemId)wardId);
        //            lstGameObjects = ObjectManager.Get<Obj_AI_Base>().ToArray();
        //            Q.Cast(
        //                lstGameObjects.FirstOrDefault(obj =>
        //                obj.Position.Distance(wardJumpPosition) < 150 &&
        //                obj is Obj_AI_Minion && obj.Position.Distance(_player.Position) < Q.Range));
        //        }
        //    }
        //}


        //public static int GetWardItem()
        //{
        //    int[] wardItems = { 3340, 3350, 3205, 3207, 2049, 2045, 2044, 3361, 3154, 3362, 3160, 2043 };
        //    foreach (var id in wardItems.Where(id => Items.HasItem(id) && Items.CanUseItem(id)))
        //        return id;
        //    return -1;
        //}

        //public static void PutWard(Vector2 pos, ItemId warditem)
        //{

        //    foreach (var slot in _player.InventoryItems.Where(slot => slot.Id == warditem))
        //    {
        //        ObjectManager.Player.Spellbook.CastSpell(slot.SpellSlot, pos.To3D());
        //        return;
        //    }
        //}


    }
}
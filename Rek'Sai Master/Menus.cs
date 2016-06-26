using System.Drawing;
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
using static Eclipse.SpellsManager;
using static Eclipse.Menus;
using Eclipse.Modes;
using EloBuddy.SDK.Menu;

namespace Eclipse
{
    internal class Menus
    {
        public static Menu FirstMenu;
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu AutoHarassMenu;
        public static Menu LaneClearMenu;
        public static Menu LasthitMenu;
        public static Menu JungleClearMenu;
        public static Menu KillStealMenu;
        public static Menu DrawingsMenu;
        public static Menu MiscMenu;

        public static ColorSlide QColorSlide;
        public static ColorSlide WColorSlide;
        public static ColorSlide EColorSlide;
        public static ColorSlide RColorSlide;
        public static ColorSlide SColorSlide;
        public static ColorSlide DamageIndicatorColorSlide;

        public const string ComboMenuID = "combomenuid";
        public const string HarassMenuID = "harassmenuid";
        public const string AutoHarassMenuID = "autoharassmenuid";
        public const string LaneClearMenuID = "laneclearmenuid";
        public const string LastHitMenuID = "lasthitmenuid";
        public const string JungleClearMenuID = "jungleclearmenuid";
        public const string KillStealMenuID = "killstealmenuid";
        public const string DrawingsMenuID = "drawingsmenuid";
        public const string MiscMenuID = "miscmenuid";

        public static void CreateMenu()
        {
            FirstMenu = MainMenu.AddMenu("Master "+Player.Instance.ChampionName, Player.Instance.ChampionName.ToLower() + "master");
            FirstMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            FirstMenu.Add(Q2.Slot + "hit", new ComboBox("Q2 HitChance", 0, "High", "Medium", "Low"));
            FirstMenu.Add(E2.Slot + "hit", new ComboBox("E2 HitChance", 0, "High", "Medium", "Low"));
            FirstMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            FirstMenu.AddGroupLabel("Addon by Taazuma / Thanks for using it");
            FirstMenu.AddLabel("If you found any bugs report it on my Thread");
            FirstMenu.AddLabel("Have fun with Playing");
            ComboMenu = FirstMenu.AddSubMenu("Combo", ComboMenuID);
            HarassMenu = FirstMenu.AddSubMenu("Harass", HarassMenuID);
            //AutoHarassMenu = FirstMenu.AddSubMenu("AutoHarass", AutoHarassMenuID);
            LaneClearMenu = FirstMenu.AddSubMenu("LaneClear", LaneClearMenuID);
            LasthitMenu = FirstMenu.AddSubMenu("LastHit", LastHitMenuID);
            JungleClearMenu = FirstMenu.AddSubMenu("JungleClear", JungleClearMenuID);
            KillStealMenu = FirstMenu.AddSubMenu("KillSteal", KillStealMenuID);
            MiscMenu = FirstMenu.AddSubMenu("Misc", MiscMenuID);
            DrawingsMenu = FirstMenu.AddSubMenu("Drawings", DrawingsMenuID);

            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.AddSeparator();
            ComboMenu.Add("UseQCombo", new CheckBox("Use Q Unburrowed"));
            ComboMenu.Add("UseQBCombo", new CheckBox("Use Q"));
            ComboMenu.AddSeparator();
            ComboMenu.Add("UseWCombo", new CheckBox("Use W"));
            //ComboMenu.CreateCheckBox("Use R", "rUse", false);
            ComboMenu.AddSeparator();
            ComboMenu.Add("UseECombo", new CheckBox("Use E Unburrowed"));
            ComboMenu.Add("UseEBCombo", new CheckBox("Use E burrowed"));
            ComboMenu.AddSeparator();


            HarassMenu.AddGroupLabel("Harass");
            HarassMenu.CreateCheckBox("Use Q", "qUse", false);
            HarassMenu.CreateCheckBox("Use Q2", "q2Use");
            HarassMenu.CreateCheckBox("Use E", "eUse", false);

            LaneClearMenu.AddGroupLabel("LaneClear");
            LaneClearMenu.CreateCheckBox("Use Q", "qUse");
            LaneClearMenu.CreateCheckBox("Use E", "eUse");

            LasthitMenu.AddGroupLabel("Lasthit");
            LasthitMenu.CreateCheckBox("Use Q", "qUse");

            JungleClearMenu.AddGroupLabel("JungleClear");
            JungleClearMenu.CreateCheckBox("Use Q", "qUse");
            JungleClearMenu.CreateCheckBox("Use Q2", "q2Use");
            JungleClearMenu.CreateCheckBox("Use E", "eUse");

            KillStealMenu.AddGroupLabel("KillSteal");
            KillStealMenu.CreateCheckBox("Use Q2", "qUse");
            KillStealMenu.CreateCheckBox("Use E2", "eUse");

            MiscMenu.AddGroupLabel("Settings");
            MiscMenu.Add("AutoW", new CheckBox("Auto W"));
            MiscMenu.Add("AutoWHP", new Slider("Use W if HP is <= ", 25, 1));
            MiscMenu.Add("AutoWMP", new Slider("Use W if Fury is >= ", 100, 1));
            MiscMenu.Add("Inter_W", new CheckBox("Use W to Interrupter"));
            MiscMenu.Add("turnburrowed", new CheckBox("Turn Burrowed if do nothing"));
            MiscMenu.AddSeparator(12);
            MiscMenu.Add("escapeterino", new KeyBind("Escape|WallJump", false, KeyBind.BindTypes.HoldActive, 'T'));
            MiscMenu.AddGroupLabel("Spell Settings");
            if (SpellsManager.Smite.IsLearned)
            {
                MiscMenu.AddLabel("Smite Spell");
                MiscMenu.CreateCheckBox("Use Smite to KS", "sks");
                MiscMenu.CreateCheckBox("Use Smite in JGL", "sjgl");
                MiscMenu.CreateCheckBox("Use Smite in Fight", "fjgl", false);
                MiscMenu.Add("smitekey", new KeyBind("Smite Activated", false, KeyBind.BindTypes.PressToggle, 'M'));
                MiscMenu.AddSeparator(15);
                MiscMenu.Add("vSmiteDrawSmiteStatus", new CheckBox("Draw Smite Status"));
                MiscMenu.Add("vSmiteDrawSmiteable", new CheckBox("Draw Smiteable Monsters"));
                MiscMenu.Add("vSmiteDrawRange", new CheckBox("Draw Smite Range"));
            }

            DrawingsMenu.AddGroupLabel("Settings");
            DrawingsMenu.CreateCheckBox("Draw spell`s range only if they are ready.", "readyDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator.", "damageDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator percent.", "perDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator statistics.", "statDraw", false);
            DrawingsMenu.AddGroupLabel("Spells");
            //DrawingsMenu.AddLabel("----------------");
            DrawingsMenu.CreateCheckBox("Draw Q.", "qDraw");
            DrawingsMenu.CreateCheckBox("Draw W.", "wDraw", false);
            DrawingsMenu.CreateCheckBox("Draw E.", "eDraw");
            DrawingsMenu.CreateCheckBox("Draw R.", "rDraw", false);
            DrawingsMenu.AddGroupLabel("Drawings Color");
            QColorSlide = new ColorSlide(DrawingsMenu, "qColor", Color.Red, "Q Color:");
            WColorSlide = new ColorSlide(DrawingsMenu, "wColor", Color.Purple, "W Color:");
            EColorSlide = new ColorSlide(DrawingsMenu, "eColor", Color.Orange, "E Color:");
            RColorSlide = new ColorSlide(DrawingsMenu, "rColor", Color.DeepPink, "R Color:");
            DamageIndicatorColorSlide = new ColorSlide(DrawingsMenu, "healthColor", Color.YellowGreen, "DamageIndicator Color:");

        }
    }
}

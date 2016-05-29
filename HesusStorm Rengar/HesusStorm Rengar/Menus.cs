using System.Drawing;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using static Eclipse.SpellsManager;
using static Eclipse.Menus;

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
            FirstMenu = MainMenu.AddMenu("HesusStorm "+Player.Instance.ChampionName, Player.Instance.ChampionName.ToLower() + "taazuma");
            FirstMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            FirstMenu.Add("ComboPrio", new ComboBox("Combo Priority", 0, "Q", "W", "E"));
            FirstMenu.Add(E.Slot + "hit", new ComboBox("E HitChance", 0, "High", "Medium", "Low"));
            FirstMenu.AddLabel("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            FirstMenu.AddSeparator(10);
            FirstMenu.AddGroupLabel("Addon by Taazuma / Thanks for using it");
            FirstMenu.AddLabel("If you found any bugs report it on my Thread");
            FirstMenu.AddLabel("Have fun with Playing");
            ComboMenu = FirstMenu.AddSubMenu("Combo", ComboMenuID);
            //HarassMenu = FirstMenu.AddSubMenu("Harass", HarassMenuID);
            //AutoHarassMenu = FirstMenu.AddSubMenu("AutoHarass", AutoHarassMenuID);
            LaneClearMenu = FirstMenu.AddSubMenu("LaneClear", LaneClearMenuID);
            LasthitMenu = FirstMenu.AddSubMenu("LastHit", LastHitMenuID);
            JungleClearMenu = FirstMenu.AddSubMenu("JungleClear", JungleClearMenuID);
            KillStealMenu = FirstMenu.AddSubMenu("KillSteal", KillStealMenuID);
            MiscMenu = FirstMenu.AddSubMenu("Misc", MiscMenuID);
            DrawingsMenu = FirstMenu.AddSubMenu("Drawings", DrawingsMenuID);

            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.CreateCheckBox("Use Q", "qUse");
            ComboMenu.CreateCheckBox("Use W", "wUse");
            ComboMenu.CreateCheckBox("Use E", "eUse");
            ComboMenu.CreateCheckBox("Use R", "rUse", false);
            ComboMenu.Add("UseEEC", new CheckBox("Use Empower E"));

            //HarassMenu.AddGroupLabel("Harass");
            //HarassMenu.CreateCheckBox("Use Q", "qUse");
            //HarassMenu.CreateCheckBox("Use W", "wUse");
            //HarassMenu.CreateCheckBox("Use E", "eUse");
            //HarassMenu.AddGroupLabel("Settings");

            LaneClearMenu.AddGroupLabel("LaneClear");
            LaneClearMenu.CreateCheckBox("Use Q", "qUse");
            LaneClearMenu.CreateCheckBox("Use W", "wUse");
            LaneClearMenu.CreateCheckBox("Use E", "eUse");
            LaneClearMenu.AddGroupLabel("Settings");
            LaneClearMenu.CreateCheckBox("W Heal on Clear", "WClearHeal");

            LasthitMenu.AddGroupLabel("Lasthit");
            LasthitMenu.CreateCheckBox("Use Q", "qUse");
            LasthitMenu.CreateCheckBox("Use W", "wUse");
            LasthitMenu.CreateCheckBox("Use E", "eUse");
            //LasthitMenu.AddGroupLabel("Settings");

            JungleClearMenu.AddGroupLabel("JungleClear");
            JungleClearMenu.CreateCheckBox("Use Q", "qUse");
            JungleClearMenu.CreateCheckBox("Use W", "wUse");
            JungleClearMenu.CreateCheckBox("Use E", "eUse");
            JungleClearMenu.AddGroupLabel("Settings");
            JungleClearMenu.Add("JungleSave", new CheckBox("Save Ferocity"));
            JungleClearMenu.Add("JunglePrio", new ComboBox("Empowered Priority", 0, "Q", "W", "E"));

            KillStealMenu.AddGroupLabel("KillSteal Beta");
            KillStealMenu.CreateCheckBox("Use Q pref off", "qUse", false);
            KillStealMenu.CreateCheckBox("Use W", "wUse");
            KillStealMenu.CreateCheckBox("Use E", "eUse");
            //KillStealMenu.AddGroupLabel("Settings");

            MiscMenu.AddGroupLabel("Settings");
            MiscMenu.AddLabel("Healer");
            MiscMenu.Add("AutoW", new CheckBox("Use W to Heal"));
            MiscMenu.Add("AutoWHP", new Slider("If Health % <", 35, 1, 100));
            MiscMenu.AddLabel("Misc...");
            MiscMenu.Add("UseEInt", new CheckBox("E to Interrupt"));
            MiscMenu.CreateCheckBox("Use W Heal on Zed Ult -> Mana 4", "ZedHeal4");
            MiscMenu.CreateCheckBox("Use W Heal on Zed Ult -> Mana 5", "ZedHeal5");
            MiscMenu.CreateCheckBox("Use Flee with Stun E && Normal E", "eflee");

            DrawingsMenu.AddGroupLabel("Settings");
            DrawingsMenu.CreateCheckBox("Draw spell`s range only if they are ready.", "readyDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator.", "damageDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator percent.", "perDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator statistics.", "statDraw", false);
            DrawingsMenu.AddGroupLabel("Spells");
            DrawingsMenu.Add("qDraw", new CheckBox("Draw Q."));
            DrawingsMenu.CreateCheckBox("Draw W.", "wDraw");
            DrawingsMenu.CreateCheckBox("Draw E.", "eDraw");
            DrawingsMenu.CreateCheckBox("Draw R.", "rDraw");
            DrawingsMenu.AddGroupLabel("Drawings Color");
            QColorSlide = new ColorSlide(DrawingsMenu, "qColor", Color.Red, "Q Color:");
            WColorSlide = new ColorSlide(DrawingsMenu, "wColor", Color.Purple, "W Color:");
            EColorSlide = new ColorSlide(DrawingsMenu, "eColor", Color.Orange, "E Color:");
            RColorSlide = new ColorSlide(DrawingsMenu, "rColor", Color.DeepPink, "R Color:");
            DamageIndicatorColorSlide = new ColorSlide(DrawingsMenu, "healthColor", Color.YellowGreen, "DamageIndicator Color:");

        }
    }
}

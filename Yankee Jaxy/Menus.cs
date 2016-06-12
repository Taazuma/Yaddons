using System.Drawing;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

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
            FirstMenu = MainMenu.AddMenu("Yankee "+Player.Instance.ChampionName, Player.Instance.ChampionName.ToLower() + "taazuma");
            FirstMenu.AddGroupLabel("Addon by Taazuma / Thanks for using it");
            FirstMenu.AddLabel("If you found any bugs report it on my Thread");
            FirstMenu.AddLabel("Have fun with Playing");
            ComboMenu = FirstMenu.AddSubMenu("Combo", ComboMenuID);
            HarassMenu = FirstMenu.AddSubMenu("Harass", HarassMenuID);
            //AutoHarassMenu = FirstMenu.AddSubMenu("AutoHarass", AutoHarassMenuID);
            LaneClearMenu = FirstMenu.AddSubMenu("LaneClear", LaneClearMenuID);
            //LasthitMenu = FirstMenu.AddSubMenu("LastHit", LastHitMenuID);
            JungleClearMenu = FirstMenu.AddSubMenu("JungleClear", JungleClearMenuID);
            KillStealMenu = FirstMenu.AddSubMenu("KillSteal", KillStealMenuID);
            MiscMenu = FirstMenu.AddSubMenu("Misc", MiscMenuID);
            DrawingsMenu = FirstMenu.AddSubMenu("Drawings", DrawingsMenuID);

            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.CreateCheckBox("Use Q", "qUse");
            ComboMenu.Add("q2Use", new CheckBox("Use Q when enemy is in AA Range", false));
            ComboMenu.AddLabel("----------------");
            ComboMenu.CreateCheckBox("Use W", "wUse");
            ComboMenu.AddLabel("----------------");
            ComboMenu.CreateCheckBox("Use E", "eUse");
            ComboMenu.Add("e2Use", new CheckBox("Use second E"));
            ComboMenu.AddLabel("second E off = manual");
            ComboMenu.AddLabel("----------------");
            ComboMenu.CreateCheckBox("Use R", "rUse");

            HarassMenu.AddGroupLabel("Harass");
            HarassMenu.CreateCheckBox("Use E", "eUse");
            HarassMenu.Add("e2Use", new CheckBox("Use second E"));
            HarassMenu.AddLabel("second E off = manual");
            HarassMenu.AddGroupLabel("Settings");
            HarassMenu.CreateSlider("Mana must be higher than [{0}%] to use Harass spells", "manaSlider", 50);

            LaneClearMenu.AddGroupLabel("LaneClear");
            LaneClearMenu.CreateCheckBox("Use Q", "qUse", false);
            LaneClearMenu.CreateCheckBox("Use W", "wUse");
            LaneClearMenu.AddGroupLabel("Settings");
            LaneClearMenu.CreateSlider("Mana must be higher than [{0}%] to use Harass spells", "manaSlider", 50);

            //LasthitMenu.AddGroupLabel("Lasthit");
            //LasthitMenu.CreateCheckBox("Use Q", "qUse");
            //LasthitMenu.CreateCheckBox("Use E", "eUse");
            //LasthitMenu.AddGroupLabel("Settings");
            //LasthitMenu.CreateSlider("Mana must be higher than [{0}%] to use Harass spells", "manaSlider", 30);

            JungleClearMenu.AddGroupLabel("JungleClear");
            JungleClearMenu.CreateCheckBox("Use Q", "qUse");
            JungleClearMenu.CreateCheckBox("Use W", "wUse");
            JungleClearMenu.CreateCheckBox("Use E", "eUse");
            JungleClearMenu.AddGroupLabel("Settings");
            JungleClearMenu.CreateSlider("Mana must be higher than [{0}%] to use Harass spells", "manaSlider", 20);

            KillStealMenu.AddGroupLabel("KillSteal");
            KillStealMenu.CreateCheckBox("Use Q", "qUse");


            MiscMenu.AddGroupLabel("Spell Settings");
            if (SpellsManager.Smite.IsLearned)
            {
            MiscMenu.AddLabel("Smite Spell");
            MiscMenu.CreateCheckBox("Use Smite to KS", "sks");
            MiscMenu.CreateCheckBox("Use Smite in JGL", "sjgl");
            MiscMenu.Add("smitekey", new KeyBind("Smite Activated", false, KeyBind.BindTypes.PressToggle, 'M'));
            }

            DrawingsMenu.AddGroupLabel("Settings");
            DrawingsMenu.CreateCheckBox("Draw spell`s range only if they are ready.", "readyDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator.", "damageDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator percent.", "perDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator statistics.", "statDraw", false);
            DrawingsMenu.AddGroupLabel("Spells");
            DrawingsMenu.CreateCheckBox("Draw Smite Status", "smitedrawer");
            DrawingsMenu.AddLabel("----------------");
            DrawingsMenu.CreateCheckBox("Draw Q.", "qDraw");
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

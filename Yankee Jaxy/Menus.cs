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
            FirstMenu = MainMenu.AddMenu("Taazuma "+Player.Instance.ChampionName, Player.Instance.ChampionName.ToLower() + "taazuma");
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

            MiscMenu.AddGroupLabel("Settings");
            MiscMenu.AddLabel("Smite");
            MiscMenu.CreateCheckBox("Use Smite to KS", "sks");
            MiscMenu.CreateCheckBox("Use Smite in JGL", "sjgl");
            if (Game.MapId == GameMapId.TwistedTreeline && SpellsManager.Smite.IsLearned)
            {
                MiscMenu.AddGroupLabel("Mobs");
                MiscMenu.Add("TT_Spiderboss", new CheckBox("Vilemaw Enabled"));
                MiscMenu.Add("TT_NGolem", new CheckBox("Golem Enabled"));
                MiscMenu.Add("TT_NWolf", new CheckBox("Wolf Enabled"));
                MiscMenu.Add("TT_NWraith", new CheckBox("Wraith Enabled"));
            }

            if (Game.MapId == GameMapId.SummonersRift && SpellsManager.Smite.IsLearned)
            {
                MiscMenu.AddGroupLabel("Big Mobs");
                MiscMenu.Add("AllDragons", new CheckBox("All Dragons"));
                MiscMenu.Add("SRU_Baron", new CheckBox("Baron"));
                MiscMenu.Add("SRU_Red", new CheckBox("Red buff"));
                MiscMenu.Add("SRU_Blue", new CheckBox("Blue buff"));
                MiscMenu.Add("SRU_RiftHerald", new CheckBox("Rift Herald"));
                MiscMenu.AddSeparator();
                MiscMenu.AddGroupLabel("Small Mobs");
                MiscMenu.Add("SRU_Gromp", new CheckBox("Gromp", false));
                MiscMenu.Add("SRU_Murkwolf", new CheckBox("Wolves", false));
                MiscMenu.Add("SRU_Krug", new CheckBox("Krug", false));
                MiscMenu.Add("SRU_Razorbeak", new CheckBox("Chicken camp", false));
                MiscMenu.Add("Sru_Crab", new CheckBox("Crab", false));
            }

            DrawingsMenu.AddGroupLabel("Settings");
            DrawingsMenu.CreateCheckBox("Draw spell`s range only if they are ready.", "readyDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator.", "damageDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator percent.", "perDraw");
            DrawingsMenu.CreateCheckBox("Draw damage indicator statistics.", "statDraw", false);
            DrawingsMenu.AddGroupLabel("Spells");
            DrawingsMenu.CreateCheckBox("Draw Q", "qDraw");
            DrawingsMenu.AddGroupLabel("Drawings Color");
            QColorSlide = new ColorSlide(DrawingsMenu, "qColor", Color.Red, "Q Color:");
            DamageIndicatorColorSlide = new ColorSlide(DrawingsMenu, "healthColor", Color.YellowGreen, "DamageIndicator Color:");

        }
    }
}

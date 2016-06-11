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
using System.Diagnostics;

namespace Eclipse.Modes
{
    internal class Active
    {
        public static Obj_AI_Minion Minion;
        public static Obj_AI_Minion Dragon;
        public static Obj_AI_Minion BaronSmite;
        public static Obj_AI_Minion RedSmite;
        public static Obj_AI_Minion BlueSmite;
        public static Obj_AI_Minion HeraldSmite;
        public static Obj_AI_Minion FroschS;
        public static Obj_AI_Minion WolfS;
        public static Obj_AI_Minion SteinS;
        public static Obj_AI_Minion ChicksS;
        public static Obj_AI_Minion KrabbeS;
        public static void Execute()
        {
            Minion = (Obj_AI_Minion)EntityManager.MinionsAndMonsters.Monsters.FirstOrDefault(buff => Program._player.IsInRange(buff, 570) && (buff.Name.StartsWith(buff.BaseSkinName) || Program.BuffsThatActuallyMakeSenseToSmite.Contains(buff.BaseSkinName)) && !buff.Name.Contains("Mini") && !buff.Name.Contains("Spawn"));
            AIHeroClient target = TargetSelector.GetTarget(570, DamageType.Magical);
            Dragon = (Obj_AI_Minion)EntityManager.MinionsAndMonsters.Monsters.FirstOrDefault(buff => Program._player.IsInRange(buff, 570) && (buff.Name.StartsWith(buff.BaseSkinName) || Program.DragonSmite.Contains(buff.BaseSkinName)) && !buff.Name.Contains("Mini") && !buff.Name.Contains("Spawn"));
            var Baron = MiscMenu.GetCheckBoxValue("SRU_Baron");
            BaronSmite = (Obj_AI_Minion)EntityManager.MinionsAndMonsters.Monsters.FirstOrDefault(buff => Program._player.IsInRange(buff, 570) && (buff.Name.StartsWith(buff.BaseSkinName) || Program.BaronSmite.Contains(buff.BaseSkinName)) && !buff.Name.Contains("Mini") && !buff.Name.Contains("Spawn"));
            var Red = MiscMenu.GetCheckBoxValue("SRU_Red");
            var Blue = MiscMenu.GetCheckBoxValue("SRU_Blue");
            RedSmite = (Obj_AI_Minion)EntityManager.MinionsAndMonsters.Monsters.FirstOrDefault(buff => Program._player.IsInRange(buff, 570) && (buff.Name.StartsWith(buff.BaseSkinName) || Program.RedSmite.Contains(buff.BaseSkinName)) && !buff.Name.Contains("Mini") && !buff.Name.Contains("Spawn"));
            BlueSmite = (Obj_AI_Minion)EntityManager.MinionsAndMonsters.Monsters.FirstOrDefault(buff => Program._player.IsInRange(buff, 570) && (buff.Name.StartsWith(buff.BaseSkinName) || Program.BlueSmite.Contains(buff.BaseSkinName)) && !buff.Name.Contains("Mini") && !buff.Name.Contains("Spawn"));
            var Heraldi = MiscMenu.GetCheckBoxValue("Rift Herald");
            HeraldSmite = (Obj_AI_Minion)EntityManager.MinionsAndMonsters.Monsters.FirstOrDefault(buff => Program._player.IsInRange(buff, 570) && (buff.Name.StartsWith(buff.BaseSkinName) || Program.HeraldSmite.Contains(buff.BaseSkinName)) && !buff.Name.Contains("Mini") && !buff.Name.Contains("Spawn"));
            var Frosch = MiscMenu.GetCheckBoxValue("SRU_Gromp");
            var Wolf = MiscMenu.GetCheckBoxValue("SRU_Murkwolf");
            var Stein = MiscMenu.GetCheckBoxValue("SRU_Krug");
            var Chicks = MiscMenu.GetCheckBoxValue("SRU_Razorbeak");
            var Krabbe = MiscMenu.GetCheckBoxValue("Sru_Crab");
            FroschS = (Obj_AI_Minion)EntityManager.MinionsAndMonsters.Monsters.FirstOrDefault(buff => Program._player.IsInRange(buff, 570) && (buff.Name.StartsWith(buff.BaseSkinName) || Program.FroschS.Contains(buff.BaseSkinName)) && !buff.Name.Contains("Mini") && !buff.Name.Contains("Spawn"));
            WolfS = (Obj_AI_Minion)EntityManager.MinionsAndMonsters.Monsters.FirstOrDefault(buff => Program._player.IsInRange(buff, 570) && (buff.Name.StartsWith(buff.BaseSkinName) || Program.WolfS.Contains(buff.BaseSkinName)) && !buff.Name.Contains("Mini") && !buff.Name.Contains("Spawn"));
            SteinS = (Obj_AI_Minion)EntityManager.MinionsAndMonsters.Monsters.FirstOrDefault(buff => Program._player.IsInRange(buff, 570) && (buff.Name.StartsWith(buff.BaseSkinName) || Program.SteinS.Contains(buff.BaseSkinName)) && !buff.Name.Contains("Mini") && !buff.Name.Contains("Spawn"));
            ChicksS = (Obj_AI_Minion)EntityManager.MinionsAndMonsters.Monsters.FirstOrDefault(buff => Program._player.IsInRange(buff, 570) && (buff.Name.StartsWith(buff.BaseSkinName) || Program.ChicksS.Contains(buff.BaseSkinName)) && !buff.Name.Contains("Mini") && !buff.Name.Contains("Spawn"));
            KrabbeS = (Obj_AI_Minion)EntityManager.MinionsAndMonsters.Monsters.FirstOrDefault(buff => Program._player.IsInRange(buff, 570) && (buff.Name.StartsWith(buff.BaseSkinName) || Program.KrabbeS.Contains(buff.BaseSkinName)) && !buff.Name.Contains("Mini") && !buff.Name.Contains("Spawn"));


            if (Dragon.IsValidTarget(570) && Dragon.Health < Program.SmiteDmgMonster(Dragon) && MiscMenu.GetCheckBoxValue("sjgl") && MiscMenu.GetCheckBoxValue("AllDragons"))
            {
                Smite.Cast(Dragon);
            }

            if (BaronSmite.IsValidTarget(570) && BaronSmite.Health < Program.SmiteDmgMonster(BaronSmite) && MiscMenu.GetCheckBoxValue("sjgl") && Baron)
            {
                Smite.Cast(BaronSmite);
            }

            if (RedSmite.IsValidTarget(570) && RedSmite.Health < Program.SmiteDmgMonster(RedSmite) && MiscMenu.GetCheckBoxValue("sjgl") && Red)
            {
                Smite.Cast(RedSmite);
            }

            if (BlueSmite.IsValidTarget(570) && BlueSmite.Health < Program.SmiteDmgMonster(BlueSmite) && MiscMenu.GetCheckBoxValue("sjgl") && Blue)
            {
                Smite.Cast(BlueSmite);
            }

            if (HeraldSmite.IsValidTarget(570) && HeraldSmite.Health < Program.SmiteDmgMonster(HeraldSmite) && MiscMenu.GetCheckBoxValue("sjgl") && Heraldi)
            {
                Smite.Cast(HeraldSmite);
            }

            //-------------------- Low Mobs
            if (FroschS.IsValidTarget(570) && FroschS.Health < Program.SmiteDmgMonster(FroschS) && MiscMenu.GetCheckBoxValue("sjgl") && Frosch)
            {
                Smite.Cast(FroschS);
            }

            if (WolfS.IsValidTarget(570) && WolfS.Health < Program.SmiteDmgMonster(WolfS) && MiscMenu.GetCheckBoxValue("sjgl") && Wolf)
            {
                Smite.Cast(WolfS);
            }

            if (SteinS.IsValidTarget(570) && SteinS.Health < Program.SmiteDmgMonster(SteinS) && MiscMenu.GetCheckBoxValue("sjgl") && Stein)
            {
                Smite.Cast(SteinS);
            }

            if (ChicksS.IsValidTarget(570) && ChicksS.Health < Program.SmiteDmgMonster(ChicksS) && MiscMenu.GetCheckBoxValue("sjgl") && Chicks)
            {
                Smite.Cast(ChicksS);
            }

            if (KrabbeS.IsValidTarget(570) && KrabbeS.Health < Program.SmiteDmgMonster(KrabbeS) && MiscMenu.GetCheckBoxValue("sjgl") && Krabbe)
            {
                Smite.Cast(KrabbeS);
            }

            if (target.IsValidTarget(570) && target.Health < Program.SmiteDmgMonster(Minion) && MiscMenu.GetCheckBoxValue("sks"))
            {
                Smite.Cast(target);
            }

        }
    }
}

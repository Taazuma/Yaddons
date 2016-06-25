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
        private const string Activeq = "RekSaiQ";
        public const float SmiteRange = 570;
        public static bool burrowed = false;
        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            //Put the name of the champion here
            if (Player.Instance.ChampionName != "RekSai") return;
            SpellsManager.InitializeSpells();
            DrawingsManager.InitializeDrawings();
            Menus.CreateMenu();
            ModeManager.InitializeModes();
            Interrupter.OnInterruptableSpell += Program.Interrupter2_OnInterruptableTarget;
            Orbwalker.OnPostAttack += OnAfterAttack;
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
                "SRU_RiftHerald",
                "SRU_Krug", "Sru_Crab", "TT_Spiderboss",
                "TT_NGolem", "TT_NWolf", "TT_NWraith"
       };

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
        public static bool IsBurrowed()
        {
            return ObjectManager.Player.HasBuff("RekSaiW");
        }

        private static void Game_OnGameUpdate(EventArgs args)
        {
            Orbwalker.DisableAttacking = false;
            if (getCheckBoxItem(MiscMenu, "AutoW") &&
              (getCheckBoxItem(MiscMenu, "turnburrowed") &&
               !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) ||
               !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass) ||
               !getKeyBindItem(HarassMenu, "harasstoggle") ||
               !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) ||
               !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear) ||
               !getKeyBindItem(MiscMenu, "escapeterino") ||
               !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee)))
            {
                AutoW();
            }

            if ((!Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo) ||
                 !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass) ||
                 !getKeyBindItem(HarassMenu, "harasstoggle") ||
                 !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) ||
                 !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear) ||
                 !getKeyBindItem(MiscMenu, "escapeterino") ||
                 !Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee)) &&
                getCheckBoxItem(MiscMenu, "turnburrowed") && !IsBurrowed())
            {
                autoburrowed();
            }
            if (getKeyBindItem(MiscMenu, "escapeterino"))
            {
                Escapeterino();
            }
        }

        public static void autoburrowed()
        {
            if (!IsBurrowed() && W.IsReady())
            {
                W.Cast();
            }
        }

        private static void Interrupter2_OnInterruptableTarget(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (IsBurrowed() && W.IsReady() && sender.IsValidTarget(Q.Range) && getCheckBoxItem(MiscMenu, "Inter_W"))
                W.Cast(sender);
        }


        private static void AutoW()
        {
            var reksaiHp = _player.MaxHealth * getSliderItem(MiscMenu, "AutoWHP") / 100;
            var reksaiMp = _player.MaxMana * getSliderItem(MiscMenu, "AutoWMP") / 100;
            if (W.IsReady() && _player.Health <= reksaiHp && !IsBurrowed() && _player.Mana >= reksaiMp)
            {
                W.Cast();
            }
        }

        private static void Player_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (!sender.IsMe) return;
            if (sender.IsMe && args.Buff.Name == "RekSaiW")
            {
                burrowed = true;
                Orbwalker.DisableAttacking = true;
            }
        }

        private static void Player_OnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs args)
        {
            if (!sender.IsMe) return;
            if (sender.IsMe && args.Buff.Name == "RekSaiW")
            {
                burrowed = false;
                Orbwalker.DisableAttacking = false;
            }
        }

        public static void OnAfterAttack(AttackableUnit target, EventArgs args)
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                    if (ComboMenu.GetCheckBoxValue("UseQCombo") && SpellsManager.Q.IsReady())
                    {
                        SpellsManager.Q.Cast();
                        return;
                    }
                }

                else if (ComboMenu.GetCheckBoxValue("UseQCombo") && SpellsManager.Q.IsReady())
                {
                    SpellsManager.Q.Cast();
                    return;
                }
            }

        public static Vector2? GetFirstWallPoint(Vector3 from, Vector3 to, float step = 25)
        {
            return GetFirstWallPoint(from.To2D(), to.To2D(), step);
        }

        public static Vector2? GetFirstWallPoint(Vector2 from, Vector2 to, float step = 25)
        {
            var direction = (to - from).Normalized();

            for (float d = 0; d < from.Distance(to); d = d + step)
            {
                var testPoint = from + d * direction;
                var flags = NavMesh.GetCollisionFlags(testPoint.X, testPoint.Y);
                if (flags.HasFlag(CollisionFlags.Wall) || flags.HasFlag(CollisionFlags.Building))
                {
                    return from + (d - step) * direction;
                }
            }

            return null;
        }

        private static void Escapeterino()
        {
            // Walljumper credits to Hellsing

            if (!IsBurrowed() && W.IsReady() && E2.IsReady()) W.Cast();

            // We need to define a new move position since jumping over walls
            // requires you to be close to the specified wall. Therefore we set the move
            // point to be that specific piont. People will need to get used to it,
            // but this is how it works.
            var wallCheck = GetFirstWallPoint(_player.Position, Game.CursorPos);

            // Be more precise
            if (wallCheck != null) wallCheck = GetFirstWallPoint((Vector3)wallCheck, Game.CursorPos, 5);

            // Define more position point
            var movePosition = wallCheck != null ? (Vector3)wallCheck : Game.CursorPos;

            // Update fleeTargetPosition
            var tempGrid = NavMesh.WorldToGrid(movePosition.X, movePosition.Y);

            // Only calculate stuff when our Q is up and there is a wall inbetween
            if (IsBurrowed() && E2.IsReady() && wallCheck != null)
            {
                // Get our wall position to calculate from
                var wallPosition = movePosition;

                // Check 300 units to the cursor position in a 160 degree cone for a valid non-wall spot
                var direction = (Game.CursorPos.To2D() - wallPosition.To2D()).Normalized();
                float maxAngle = 80;
                var step = maxAngle / 20;
                float currentAngle = 0;
                float currentStep = 0;
                var jumpTriggered = false;
                while (true)
                {
                    // Validate the counter, break if no valid spot was found in previous loops
                    if (currentStep > maxAngle && currentAngle < 0) break;

                    // Check next angle
                    if ((currentAngle == 0 || currentAngle < 0) && currentStep != 0)
                    {
                        currentAngle = currentStep * (float)Math.PI / 180;
                        currentStep += step;
                    }

                    else if (currentAngle > 0) currentAngle = -currentAngle;

                    Vector3 checkPoint;

                    // One time only check for direct line of sight without rotating
                    if (currentStep == 0)
                    {
                        currentStep = step;
                        checkPoint = wallPosition + E2.Range * direction.To3D();
                    }
                    // Rotated check
                    else checkPoint = wallPosition + E2.Range * direction.Rotated(currentAngle).To3D();

                    // Check if the point is not a wall
                    if (!checkPoint.IsWall())
                    {
                        // Check if there is a wall between the checkPoint and wallPosition
                        wallCheck = GetFirstWallPoint(checkPoint, wallPosition);
                        if (wallCheck != null)
                        {
                            // There is a wall inbetween, get the closes point to the wall, as precise as possible
                            var wallPositionOpposite =
                                (Vector3)GetFirstWallPoint((Vector3)wallCheck, wallPosition, 5);

                            //// Check if it's worth to jump considering the path length
                            //if (_player.GetPath(wallPositionOpposite).ToList().ToLookup().PathLength()
                            //    - _player.Distance(wallPositionOpposite) > 200) //200
                            //{
                                // Check the distance to the opposite side of the wall
                                if (_player.Distance(wallPositionOpposite, true)
                                    < Math.Pow(E2.Range + 200 - _player.BoundingRadius / 2, 2))
                                {
                                    // Make the jump happen
                                    E2.Cast(wallPositionOpposite);

                                    // Update jumpTriggered value to not orbwalk now since we want to jump
                                    jumpTriggered = true;

                                    break;
                                }
                            //}

                            //else
                            //{
                            //    // yolo
                            //    Render.Circle.DrawCircle(Game.CursorPos, 35, Color.Red, 2);
                            //}
                        }
                    }
                }
                // Check if the loop triggered the jump, if not just orbwalk
                if (!jumpTriggered)
                {
                    Orbwalker.OrbwalkTo(Game.CursorPos);
                }
            }

            // Either no wall or W on cooldown, just move towards to wall then
            else
            {
                Orbwalker.OrbwalkTo(Game.CursorPos);
                if (IsBurrowed() && E2.IsReady()) E2.Cast(Game.CursorPos);
            }
        }

    }
}
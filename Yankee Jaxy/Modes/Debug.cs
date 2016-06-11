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

namespace Eclipse
{
    class Debug
    {
        private static int _lastConsoleMsg = Environment.TickCount; // To prevent msg spam
        private static int _lastChatMsg = Environment.TickCount;
        private static string _lastChatMsgText = "Chat";
        private static string _lastConsoleMsgText = "Console";

        public static void Write(String text)
        {
            WriteChat(text, Color.LightBlue);
            WriteConsole(text, ConsoleColor.Cyan);
        }

        public static void Write(string format, params string[] args)
        {
            WriteChat(String.Format(format, args), Color.LightBlue);
            WriteConsole(String.Format(format, args), ConsoleColor.Cyan);
        }

        public static void WriteChat(string text)
        {
            WriteChat(text, Color.LightBlue);
        }

        public static void WriteChat(string format, params string[] args)
        {
            WriteChat(String.Format(format, args), Color.LightBlue);
        }


        public static void WriteChat(string format, Color color, params string[] args)
        {
            WriteChat(String.Format(format, args), color);
        }


        public static void WriteConsole(string text)
        {
            WriteConsole(text, ConsoleColor.Cyan);
        }

        public static void WriteConsole(string format, params string[] args)
        {
            WriteConsole(String.Format(format, args), ConsoleColor.Cyan);
        }

        public static void WriteConsole(string format, ConsoleColor color, params string[] args)
        {
            WriteConsole(String.Format(format, args), color);
        }

    }
}

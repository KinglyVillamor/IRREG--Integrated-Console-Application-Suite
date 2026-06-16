using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Media;

namespace IRREG_PixelArcade
{
    #region Pixel Art Assets
    static class PixelArt
    {
        // Retro Console Header
        public static readonly string[] BootScreen = {
            "╔════════════════════════════════════════════════════════════════════════════════════════╗",
            "║  ██╗██████╗ ██████╗  ███████╗ ██████╗     ██████╗ ██╗██╗  ██╗███████╗██╗                 ║",
            "║  ██║██╔══██╗██╔══██╗██╔════╝██╔════╝     ██╔══██╗██║╚██╗██╔╝██╔════╝██║                 ║",
            "║  ██║██████╔╝██████╔╝█████╗  ██║  ███╗    ██████╔╝██║ ╚███╔╝ █████╗  ██║                 ║",
            "║  ██║██╔══██╗██╔══██╗██╔══╝  ██║   ██║    ██╔══██╗██║ ██╔██╗ ██╔══╝  ██║                 ║",
            "║  ██║██║  ██║██║  ██║███████╗╚██████╔╝    ██║  ██║██║██╔╝ ██╗███████╗███████╗            ║",
            "║  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝ ╚═════╝     ╚═╝  ╚═╝╚═╝╚═╝  ╚═╝╚══════╝╚══════╝            ║",
            "╠════════════════════════════════════════════════════════════════════════════════════════╣",
            "║                      🎮 PIXEL PERFECT CONSOLE SUITE 🎮                                  ║",
            "║                    Where Retro Gaming Meets Modern Utility                              ║",
            "╚════════════════════════════════════════════════════════════════════════════════════════╝"
        };

        // Pixel Heart Animation
        public static readonly string[] PixelHeart = {
            " ██████╗  █████╗ ███╗   ███╗███████╗",
            "██╔════╝ ██╔══██╗████╗ ████║██╔════╝",
            "██║  ███╗███████║██╔████╔██║█████╗  ",
            "██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  ",
            "╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗",
            " ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝"
        };

        // Loading Bar Frame
        public static string DrawLoadingFrame(int width, string title)
        {
            return $"╔{new string('═', width)}╗\n║{new string(' ', width)}║\n╚{new string('═', width)}╝";
        }
    }
    #endregion

    #region Retro Sound Effects
    static class RetroAudio
    {
        private static ConsoleColor _originalColor;

        public static void PlayBlip()
        {
            try { Console.Beep(800, 50); } catch { }
        }

        public static void PlaySuccess()
        {
            try { Console.Beep(1200, 100); Console.Beep(1500, 100); } catch { }
        }

        public static void PlayError()
        {
            try { Console.Beep(400, 200); } catch { }
        }

        public static void PlayGameOver()
        {
            try { Console.Beep(300, 300); Console.Beep(250, 300); Console.Beep(200, 500); } catch { }
        }

        public static void PlayEat()
        {
            try { Console.Beep(1000, 30); } catch { }
        }

        public static void PlaySelect()
        {
            try { Console.Beep(600, 80); } catch { }
        }
    }
    #endregion

    #region Pixel Animation Engine
    static class PixelEngine
    {
        private static int _frameDelay = 50;

        public static void AnimatePixelText(string text, int delayMs = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }
        }

        public static void DrawPixelBorder(int width, int height, int startX, int startY, string title = "")
        {
            Console.SetCursorPosition(startX, startY);
            Console.Write("╔" + new string('═', width - 2) + "╗");

            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(startX, startY + i);
                Console.Write("║" + new string(' ', width - 2) + "║");
            }

            Console.SetCursorPosition(startX, startY + height - 1);
            Console.Write("╚" + new string('═', width - 2) + "╝");

            if (!string.IsNullOrEmpty(title))
            {
                Console.SetCursorPosition(startX + (width - title.Length) / 2, startY);
                Console.Write(title);
            }
        }

        public static void TypewriterEffect(string[] lines, int delayMs = 40)
        {
            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    Console.Write(c);
                    Thread.Sleep(delayMs / 2);
                }
                Console.WriteLine();
                Thread.Sleep(delayMs);
            }
        }

        public static void ScreenShake(int intensity = 2, int duration = 100)
        {
            int originalLeft = Console.CursorLeft;
            int originalTop = Console.CursorTop;

            for (int i = 0; i < duration / 20; i++)
            {
                Console.SetCursorPosition(originalLeft + (i % 2 == 0 ? intensity : -intensity), originalTop);
                Thread.Sleep(20);
            }
            Console.SetCursorPosition(originalLeft, originalTop);
        }

        public static void DrawProgressBar(int x, int y, int width, int progress, string message)
        {
            int filled = (progress * width) / 100;

            Console.SetCursorPosition(x, y);
            Console.Write("[" + new string('█', filled) + new string('░', width - filled) + $"] {progress}%");

            if (!string.IsNullOrEmpty(message))
            {
                Console.SetCursorPosition(x + (width / 2) - (message.Length / 2), y - 1);
                Console.Write(message);
            }
        }
    }
    #endregion

    class Program
    {
        // Game constants
        static readonly int gridW = 90;
        static readonly int gridH = 20;
        static Cell[,] grid = new Cell[gridH, gridW];
        static Cell currentCell;
        static int direction;
        static readonly int speed = 1;
        static bool Populated = false;
        static bool Lost = false;
        static int snakeLength;
        static int highScore = 0;

        // TicTacToe variables
        static char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static int player = 1;
        static int choice;
        static int flag = 0;

        static Random rng = new Random();

        static void Main(string[] args)
        {
            try
            {
                Console.Title = "🎮 IRREG: Pixel Arcade Suite 🎮";
                Console.SetWindowSize(140, 45);
                Console.SetBufferSize(140, 45);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.CursorVisible = false;

                // Load high score
                LoadHighScore();

                ShowPixelBootScreen();
                ShowPixelLoadingScreen();
                ShowRetroLogin();
            }
            catch (Exception ex)
            {
                PixelEngine.DrawPixelBorder(60, 10, 40, 15, "⚠️ SYSTEM ERROR ⚠️");
                Console.SetCursorPosition(45, 18);
                Console.Write($"ERROR: {ex.Message}");
                Console.SetCursorPosition(45, 20);
                Console.Write("Press any key to exit...");
                Console.ReadKey();
            }
        }

        static void LoadHighScore()
        {
            // Simple high score persistence
            try
            {
                if (System.IO.File.Exists("highscore.dat"))
                {
                    highScore = int.Parse(System.IO.File.ReadAllText("highscore.dat"));
                }
            }
            catch { }
        }

        static void SaveHighScore()
        {
            try
            {
                if (snakeLength > highScore)
                {
                    highScore = snakeLength;
                    System.IO.File.WriteAllText("highscore.dat", highScore.ToString());
                }
            }
            catch { }
        }

        static void ShowPixelBootScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;

            // Animated boot sequence
            for (int i = 0; i < PixelArt.BootScreen.Length; i++)
            {
                Console.SetCursorPosition(5, i + 2);
                foreach (char c in PixelArt.BootScreen[i])
                {
                    Console.Write(c);
                    Thread.Sleep(5);
                }
                Console.WriteLine();
            }

            Console.SetCursorPosition(45, 20);
            Console.ForegroundColor = ConsoleColor.Yellow;
            PixelEngine.AnimatePixelText("▶ PRESS ANY KEY TO BOOT UP ◀");
            Console.ReadKey();
            RetroAudio.PlaySelect();
        }

        static void ShowPixelLoadingScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            // Pixel art loading animation
            string[] loadingFrames = { "█", "▓", "▒", "░" };

            PixelEngine.DrawPixelBorder(60, 12, 40, 10, "LOADING GAME DATA");

            for (int progress = 0; progress <= 100; progress += 5)
            {
                Console.SetCursorPosition(45, 14);
                Console.Write("[");
                int filled = progress / 2;
                Console.Write(new string('█', filled) + new string('░', 50 - filled));
                Console.Write($"] {progress}%");

                // Animated loading text
                Console.SetCursorPosition(55, 16);
                Console.Write($"Team IRREG {loadingFrames[(progress / 5) % 4]}");

                Thread.Sleep(50);
                RetroAudio.PlayBlip();
            }

            Thread.Sleep(500);
            Console.Clear();

            // Show Team IRREG Pixel Art
            foreach (string line in PixelArt.PixelHeart)
            {
                Console.SetCursorPosition(45, Console.CursorTop + 1);
                Console.WriteLine(line);
                Thread.Sleep(50);
            }

            Console.SetCursorPosition(55, 30);
            PixelEngine.AnimatePixelText("✦ TEAM IRREG PRESENTS ✦", 40);
            Thread.Sleep(1000);
        }

        static void ShowRetroLogin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            string un, pw;
            int attempts = 0;

            while (attempts < 3)
            {
                Console.Clear();

                // Retro game-style login screen
                PixelEngine.DrawPixelBorder(70, 18, 35, 8, "🎮 PLAYER AUTHENTICATION 🎮");

                Console.SetCursorPosition(45, 12);
                Console.Write("👤 ENTER USERNAME: ");
                un = Console.ReadLine();

                Console.SetCursorPosition(45, 14);
                Console.Write("🔒 ENTER PASSWORD: ");
                pw = ReadPasswordWithPixel();

                if (un == "UNO" && pw == "UNO")
                {
                    RetroAudio.PlaySuccess();
                    ShowPixelWelcome();
                    ShowMainGameMenu();
                    break;
                }
                else
                {
                    attempts++;
                    RetroAudio.PlayError();
                    Console.SetCursorPosition(45, 17);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"⚠️ ACCESS DENIED! {3 - attempts} attempts remaining ⚠️");
                    Thread.Sleep(1500);
                }

                if (attempts == 3)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    PixelEngine.TypewriterEffect(new[] {
                        "╔════════════════════════════════════════╗",
                        "║         SYSTEM LOCKED - GAME OVER      ║",
                        "║        CONTACT SYSTEM ADMINISTRATOR    ║",
                        "╚════════════════════════════════════════╝"
                    });
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                }
            }
        }

        static string ReadPasswordWithPixel()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("⬤");
                    RetroAudio.PlayBlip();
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[0..^1];
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

        static void ShowPixelWelcome()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            string[] welcomeArt = {
                "╔════════════════════════════════════════════════════════════════════════════╗",
                "║                                                                              ║",
                "║    ██╗██████╗ ██████╗  ███████╗ ██████╗     ██╗██╗  ██╗███████╗██╗          ║",
                "║    ██║██╔══██╗██╔══██╗██╔════╝██╔════╝     ██║╚██╗██╔╝██╔════╝██║          ║",
                "║    ██║██████╔╝██████╔╝█████╗  ██║  ███╗    ██║ ╚███╔╝ █████╗  ██║          ║",
                "║    ██║██╔══██╗██╔══██╗██╔══╝  ██║   ██║    ██║ ██╔██╗ ██╔══╝  ██║          ║",
                "║    ██║██║  ██║██║  ██║███████╗╚██████╔╝    ██║██╔╝ ██╗███████╗███████╗     ║",
                "║    ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝ ╚═════╝     ╚═╝╚═╝  ╚═╝╚══════╝╚══════╝     ║",
                "║                                                                              ║",
                "║                      🎮 WELCOME TO THE ARCADE 🎮                             ║",
                "║                                                                              ║",
                "╚════════════════════════════════════════════════════════════════════════════╝"
            };

            foreach (string line in welcomeArt)
            {
                Console.SetCursorPosition(5, Console.CursorTop);
                PixelEngine.AnimatePixelText(line, 10);
                Console.WriteLine();
            }

            Thread.Sleep(1000);
        }

        static void ShowMainGameMenu()
        {
            string choice;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;

                // Retro game menu
                PixelEngine.DrawPixelBorder(60, 18, 40, 5, "🎮 MAIN MENU 🎮");

                string[] menuItems = {
                    "╔════════════════════════════════════════╗",
                    "║                                        ║",
                    "║    1. 🧮 BASIC OPERATIONS              ║",
                    "║    2. 📚 INTERMEDIATE OPERATIONS       ║",
                    "║    3. 🎮 ENTERTAINMENT                 ║",
                    "║    4. 👾 CREDITS                       ║",
                    "║    5. 🚪 EXIT GAME                     ║",
                    "║                                        ║",
                    "╚════════════════════════════════════════╝"
                };

                Console.SetCursorPosition(45, 9);
                foreach (string item in menuItems)
                {
                    Console.SetCursorPosition(45, Console.CursorTop);
                    Console.WriteLine(item);
                }

                Console.SetCursorPosition(60, 20);
                Console.Write("🎯 SELECT OPTION: ");
                choice = Console.ReadLine();

                RetroAudio.PlaySelect();

                switch (choice)
                {
                    case "1": ShowPixelBasicMenu(); break;
                    case "2": ShowPixelIntermediateMenu(); break;
                    case "3": ShowPixelGameMenu(); break;
                    case "4": ShowPixelCredits(); break;
                    case "5":
                        Console.Clear();
                        PixelEngine.TypewriterEffect(new[] { "GAME OVER", "THANKS FOR PLAYING!" }, 100);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.SetCursorPosition(55, 22);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("❌ INVALID SELECTION! ❌");
                        Thread.Sleep(1000);
                        break;
                }
            } while (true);
        }

        static void ShowPixelBasicMenu()
        {
            string choice;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;

                PixelEngine.DrawPixelBorder(55, 15, 42, 5, "🧮 BASIC OPERATIONS");

                string[] options = {
                    "╔════════════════════════════════════╗",
                    "║                                    ║",
                    "║    A. 🔄 SWAPPING NUMBERS          ║",
                    "║    B. 🧮 MDAS CALCULATOR           ║",
                    "║    C. ⬆️ HIGHEST NUMBER            ║",
                    "║    D. ↩️ BACK TO MENU              ║",
                    "║                                    ║",
                    "╚════════════════════════════════════╝"
                };

                Console.SetCursorPosition(47, 9);
                foreach (string option in options)
                {
                    Console.SetCursorPosition(47, Console.CursorTop);
                    Console.WriteLine(option);
                }

                Console.SetCursorPosition(55, 18);
                Console.Write("🎮 CHOOSE: ");
                choice = Console.ReadLine()?.ToUpper();

                RetroAudio.PlaySelect();

                switch (choice)
                {
                    case "A": PixelSwapping(); break;
                    case "B": PixelMDAS(); break;
                    case "C": PixelHighest(); break;
                    case "D": return;
                    default:
                        Console.SetCursorPosition(55, 20);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("❌ INVALID CHOICE!");
                        Thread.Sleep(1000);
                        break;
                }
            } while (true);
        }

        static void PixelSwapping()
        {
            Console.Clear();
            PixelEngine.DrawPixelBorder(60, 12, 40, 5, "🔄 NUMBER SWAPPER");

            Console.SetCursorPosition(50, 10);
            Console.Write("📝 First Number: ");
            if (int.TryParse(Console.ReadLine(), out int a))
            {
                Console.SetCursorPosition(50, 12);
                Console.Write("📝 Second Number: ");
                if (int.TryParse(Console.ReadLine(), out int b))
                {
                    Console.SetCursorPosition(50, 14);
                    Console.Write($"✨ Before: {a} and {b}");
                    (a, b) = (b, a);
                    Console.SetCursorPosition(50, 16);
                    Console.Write($"🎉 After: {a} and {b}");
                    Console.SetCursorPosition(50, 18);
                    Console.Write("⚡ Press any key to continue...");
                    RetroAudio.PlaySuccess();
                }
            }
            Console.ReadKey();
        }

        static void PixelMDAS()
        {
            Console.Clear();
            PixelEngine.DrawPixelBorder(60, 15, 40, 5, "🧮 MDAS CALCULATOR");

            Console.SetCursorPosition(50, 10);
            Console.Write("🔢 First Number: ");
            if (double.TryParse(Console.ReadLine(), out double num1))
            {
                Console.SetCursorPosition(50, 12);
                Console.Write("🔢 Second Number: ");
                if (double.TryParse(Console.ReadLine(), out double num2))
                {
                    Console.SetCursorPosition(50, 14);
                    Console.Write("🎯 Operator (+ - * /): ");
                    string op = Console.ReadLine();

                    double result = op switch
                    {
                        "+" => num1 + num2,
                        "-" => num1 - num2,
                        "*" => num1 * num2,
                        "/" => num2 != 0 ? num1 / num2 : double.NaN,
                        _ => double.NaN
                    };

                    Console.SetCursorPosition(50, 16);
                    if (!double.IsNaN(result))
                    {
                        Console.Write($"📊 Result: {num1} {op} {num2} = {result:F2}");
                        RetroAudio.PlaySuccess();
                    }
                    else
                        Console.Write("⚠️ ERROR: Invalid operation!");
                }
            }
            Console.SetCursorPosition(50, 18);
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        static void PixelHighest()
        {
            Console.Clear();
            PixelEngine.DrawPixelBorder(60, 14, 40, 5, "⬆️ HIGHEST NUMBER");

            double[] nums = new double[3];
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(50, 8 + i * 2);
                Console.Write($"📌 Number {i + 1}: ");
                while (!double.TryParse(Console.ReadLine(), out nums[i]))
                {
                    Console.SetCursorPosition(50, 8 + i * 2);
                    Console.Write("❌ Invalid! Try again: ");
                }
            }

            double highest = nums.Max();
            Console.SetCursorPosition(50, 16);
            Console.Write($"🏆 HIGHEST: {highest}");
            Console.SetCursorPosition(50, 18);
            Console.Write("Press any key to continue...");
            RetroAudio.PlaySuccess();
            Console.ReadKey();
        }

        static void ShowPixelIntermediateMenu()
        {
            string choice;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;

                PixelEngine.DrawPixelBorder(60, 16, 40, 5, "📚 INTERMEDIATE OPS");

                string[] options = {
                    "╔════════════════════════════════════╗",
                    "║                                    ║",
                    "║    A. 👨‍🎓 STUDENT PROFILE         ║",
                    "║    B. 📊 GRADE COMPUTATION         ║",
                    "║    C. 💰 SALES TRANSACTION         ║",
                    "║    D. ↩️ BACK TO MENU              ║",
                    "║                                    ║",
                    "╚════════════════════════════════════╝"
                };

                Console.SetCursorPosition(47, 9);
                foreach (string option in options)
                {
                    Console.SetCursorPosition(47, Console.CursorTop);
                    Console.WriteLine(option);
                }

                Console.SetCursorPosition(55, 18);
                Console.Write("🎮 CHOOSE: ");
                choice = Console.ReadLine()?.ToUpper();

                RetroAudio.PlaySelect();

                switch (choice)
                {
                    case "A": PixelStudentProfile(); break;
                    case "B": PixelGradeComputation(); break;
                    case "C": PixelSalesTransaction(); break;
                    case "D": return;
                    default:
                        Console.SetCursorPosition(55, 20);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("❌ INVALID CHOICE!");
                        Thread.Sleep(1000);
                        break;
                }
            } while (true);
        }

        static void PixelStudentProfile()
        {
            Console.Clear();
            PixelEngine.DrawPixelBorder(80, 25, 30, 3, "👨‍🎓 STUDENT DATABASE");

            Console.SetCursorPosition(35, 7);
            Console.Write("📊 Number of Students: ");
            if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
            {
                Console.SetCursorPosition(35, 9);
                Console.Write("❌ Invalid number!");
                Thread.Sleep(1000);
                return;
            }

            var students = new List<dynamic>();
            for (int i = 0; i < count; i++)
            {
                Console.Clear();
                PixelEngine.DrawPixelBorder(60, 18, 40, 3, $"👤 STUDENT {i + 1}");

                Console.SetCursorPosition(45, 7);
                Console.Write("🆔 ID: ");
                string id = Console.ReadLine();
                Console.SetCursorPosition(45, 9);
                Console.Write("📝 First Name: ");
                string fn = Console.ReadLine();
                Console.SetCursorPosition(45, 11);
                Console.Write("📝 Last Name: ");
                string ln = Console.ReadLine();
                Console.SetCursorPosition(45, 13);
                Console.Write("🎓 Course: ");
                string course = Console.ReadLine();

                students.Add(new { ID = id, FirstName = fn, LastName = ln, Course = course });
                RetroAudio.PlayBlip();
            }

            Console.Clear();
            PixelEngine.DrawPixelBorder(90, count + 8, 25, 3, "📋 STUDENT DIRECTORY");

            Console.SetCursorPosition(30, 7);
            Console.WriteLine("┌───────┬──────────────────┬──────────────────┬─────────────┐");
            Console.SetCursorPosition(30, 8);
            Console.WriteLine("│  ID   │    FIRST NAME     │    LAST NAME      │   COURSE    │");
            Console.SetCursorPosition(30, 9);
            Console.WriteLine("├───────┼──────────────────┼──────────────────┼─────────────┤");

            for (int i = 0; i < students.Count; i++)
            {
                var s = students[i];
                Console.SetCursorPosition(30, 10 + i);
                Console.WriteLine($"│ {s.ID,-5} │ {s.FirstName,-16} │ {s.LastName,-16} │ {s.Course,-11} │");
            }

            Console.SetCursorPosition(30, 11 + students.Count);
            Console.WriteLine("└───────┴──────────────────┴──────────────────┴─────────────┘");
            Console.SetCursorPosition(40, 13 + students.Count);
            Console.Write("✨ Press any key to continue...");
            Console.ReadKey();
        }

        static void PixelGradeComputation()
        {
            Console.Clear();
            PixelEngine.DrawPixelBorder(70, 20, 35, 3, "📊 GRADE COMPUTATION");

            Console.SetCursorPosition(40, 7);
            Console.Write("👥 Number of Students: ");
            if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
            {
                Console.SetCursorPosition(40, 9);
                Console.Write("❌ Invalid!");
                Thread.Sleep(1000);
                return;
            }

            var grades = new List<dynamic>();
            for (int i = 0; i < count; i++)
            {
                Console.Clear();
                PixelEngine.DrawPixelBorder(60, 15, 40, 3, $"📝 STUDENT {i + 1}");

                Console.SetCursorPosition(45, 7);
                Console.Write("👤 Name: ");
                string name = Console.ReadLine();
                Console.SetCursorPosition(45, 9);
                Console.Write("📖 Prelim: ");
                double prelim = GetPixelGrade();
                Console.SetCursorPosition(45, 11);
                Console.Write("📖 Midterm: ");
                double midterm = GetPixelGrade();
                Console.SetCursorPosition(45, 13);
                Console.Write("📖 Finals: ");
                double finals = GetPixelGrade();

                double avg = (prelim + midterm + finals) / 3;
                grades.Add(new { Name = name, Prelim = prelim, Midterm = midterm, Finals = finals, Average = avg });
                RetroAudio.PlayBlip();
            }

            Console.Clear();
            PixelEngine.DrawPixelBorder(100, count + 8, 20, 3, "🏆 GRADE REPORT");

            Console.SetCursorPosition(25, 7);
            Console.WriteLine("┌──────────────────────┬─────────┬─────────┬─────────┬─────────┬──────────┐");
            Console.SetCursorPosition(25, 8);
            Console.WriteLine("│       STUDENT        │ PRELIM  │ MIDTERM │ FINALS  │ AVERAGE │  STATUS  │");
            Console.SetCursorPosition(25, 9);
            Console.WriteLine("├──────────────────────┼─────────┼─────────┼─────────┼─────────┼──────────┤");

            for (int i = 0; i < grades.Count; i++)
            {
                var g = grades[i];
                string status = g.Average >= 75 ? "✅ PASSED" : "❌ FAILED";
                Console.SetCursorPosition(25, 10 + i);
                Console.WriteLine($"│ {g.Name,-20} │ {g.Prelim,7:F2} │ {g.Midterm,7:F2} │ {g.Finals,7:F2} │ {g.Average,7:F2} │ {status,-8} │");
            }

            Console.SetCursorPosition(25, 11 + grades.Count);
            Console.WriteLine("└──────────────────────┴─────────┴─────────┴─────────┴─────────┴──────────┘");
            Console.SetCursorPosition(40, 13 + grades.Count);
            Console.Write("✨ Press any key to continue...");
            Console.ReadKey();
        }

        static double GetPixelGrade()
        {
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double grade) && grade >= 0 && grade <= 100)
                    return grade;
                Console.Write("❌ Invalid (0-100): ");
                RetroAudio.PlayError();
            }
        }

        static void PixelSalesTransaction()
        {
            Console.Clear();
            PixelEngine.DrawPixelBorder(60, 20, 40, 3, "💰 SALES SYSTEM");

            Console.SetCursorPosition(45, 7);
            Console.Write("🛒 Number of Items: ");
            if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
            {
                Console.SetCursorPosition(45, 9);
                Console.Write("❌ Invalid!");
                Thread.Sleep(1000);
                return;
            }

            var items = new List<dynamic>();
            double total = 0;

            for (int i = 0; i < count; i++)
            {
                Console.Clear();
                PixelEngine.DrawPixelBorder(60, 12, 40, 3, $"📦 ITEM {i + 1}");

                Console.SetCursorPosition(45, 7);
                Console.Write("🏷️ Name: ");
                string name = Console.ReadLine();
                Console.SetCursorPosition(45, 9);
                Console.Write("💰 Price: ₱");

                if (double.TryParse(Console.ReadLine(), out double price) && price > 0)
                {
                    items.Add(new { Name = name, Price = price });
                    total += price;
                    RetroAudio.PlayBlip();
                }
                else
                {
                    Console.SetCursorPosition(45, 11);
                    Console.Write("❌ Invalid price!");
                    Thread.Sleep(1000);
                    i--;
                }
            }

            Console.Clear();
            PixelEngine.DrawPixelBorder(60, count + 12, 40, 3, "🧾 OFFICIAL RECEIPT");

            Console.SetCursorPosition(45, 7);
            Console.WriteLine("╔════════════════════════╦════════════╗");
            Console.SetCursorPosition(45, 8);
            Console.WriteLine("║       ITEM NAME        ║   PRICE    ║");
            Console.SetCursorPosition(45, 9);
            Console.WriteLine("╠════════════════════════╬════════════╣");

            for (int i = 0; i < items.Count; i++)
            {
                Console.SetCursorPosition(45, 10 + i);
                Console.WriteLine($"║ {items[i].Name,-22} ║ ₱{items[i].Price,9:F2} ║");
            }

            Console.SetCursorPosition(45, 11 + items.Count);
            Console.WriteLine("╠════════════════════════╬════════════╣");
            Console.SetCursorPosition(45, 12 + items.Count);
            Console.WriteLine($"║ TOTAL                  ║ ₱{total,9:F2} ║");
            Console.SetCursorPosition(45, 13 + items.Count);
            Console.WriteLine("╚════════════════════════╩════════════╝");

            Console.SetCursorPosition(45, 15 + items.Count);
            Console.Write("💵 CASH: ₱");
            if (double.TryParse(Console.ReadLine(), out double cash) && cash >= total)
            {
                Console.SetCursorPosition(45, 17 + items.Count);
                Console.Write($"💰 CHANGE: ₱{cash - total:F2}");
                RetroAudio.PlaySuccess();
            }
            else
            {
                Console.SetCursorPosition(45, 17 + items.Count);
                Console.Write("❌ INSUFFICIENT PAYMENT!");
                RetroAudio.PlayError();
            }

            Console.SetCursorPosition(45, 19 + items.Count);
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        static void ShowPixelGameMenu()
        {
            string choice;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;

                PixelEngine.DrawPixelBorder(55, 14, 42, 5, "🎮 GAME ARCADE 🎮");

                string[] games = {
                    "╔════════════════════════════════════╗",
                    "║                                    ║",
                    "║    A. 🐍 SNAKE CLASSIC             ║",
                    "║    B. ❌ TIC-TAC-TOE               ║",
                    "║    C. ↩️ BACK TO MENU              ║",
                    "║                                    ║",
                    "╚════════════════════════════════════╝"
                };

                Console.SetCursorPosition(47, 9);
                foreach (string game in games)
                {
                    Console.SetCursorPosition(47, Console.CursorTop);
                    Console.WriteLine(game);
                }

                Console.SetCursorPosition(55, 17);
                Console.Write("🎮 SELECT GAME: ");
                choice = Console.ReadLine()?.ToUpper();

                RetroAudio.PlaySelect();

                switch (choice)
                {
                    case "A": PixelSnakeGame(); break;
                    case "B": PixelTicTacToe(); break;
                    case "C": return;
                    default:
                        Console.SetCursorPosition(55, 19);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("❌ INVALID CHOICE!");
                        Thread.Sleep(1000);
                        break;
                }
            } while (true);
        }

        #region Snake Game - Pixel Edition
        static void PixelSnakeGame()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            // Show game instructions
            PixelEngine.DrawPixelBorder(50, 12, 45, 5, "🐍 SNAKE CONTROLS");
            Console.SetCursorPosition(50, 9);
            Console.Write("  W = ⬆️   S = ⬇️   A = ⬅️   D = ➡️");
            Console.SetCursorPosition(50, 11);
            Console.Write("⚡ HIGH SCORE: {0}", highScore);
            Console.SetCursorPosition(50, 13);
            Console.Write("Press any key to start...");
            Console.ReadKey();

            // Reset game state
            Populated = false;
            Lost = false;
            snakeLength = 5;
            direction = 1;

            if (!Populated)
            {
                PixelPopulateGrid();
                currentCell = grid[gridH / 2, gridW / 2];
                PixelUpdatePos();
                PixelAddFood();
                Populated = true;
            }

            while (!Lost)
            {
                PixelRestart();
            }

            SaveHighScore();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            PixelEngine.DrawPixelBorder(50, 10, 45, 8, "💀 GAME OVER 💀");
            Console.SetCursorPosition(55, 12);
            Console.Write($"🐍 FINAL LENGTH: {snakeLength}");
            Console.SetCursorPosition(55, 14);
            Console.Write($"🏆 HIGH SCORE: {highScore}");
            Console.SetCursorPosition(55, 16);
            Console.Write("Press any key to continue...");
            Console.ReadKey();

            Lost = false;
            Populated = false;
        }

        static void PixelRestart()
        {
            Console.SetCursorPosition(0, 0);
            PixelPrintGrid();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"╔════════════════════════════════════════╗");
            Console.WriteLine($"║  LENGTH: {snakeLength,-5} HIGH SCORE: {highScore,-5}                ║");
            Console.WriteLine($"╚════════════════════════════════════════╝");
            PixelGetInput();
        }

        static void PixelGetInput()
        {
            if (Console.KeyAvailable)
            {
                var input = Console.ReadKey(true);
                PixelDoInput(input.KeyChar);
            }
            else
            {
                PixelMove();
                PixelUpdateScreen();
            }
        }

        static void PixelUpdateScreen()
        {
            Console.SetCursorPosition(0, 0);
            PixelPrintGrid();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"╔════════════════════════════════════════╗");
            Console.WriteLine($"║  LENGTH: {snakeLength,-5} HIGH SCORE: {highScore,-5}                ║");
            Console.WriteLine($"╚════════════════════════════════════════╝");
            Thread.Sleep(speed * 80);
        }

        static void PixelDoInput(char inp)
        {
            switch (inp)
            {
                case 'w': if (direction != 2) direction = 0; break;
                case 's': if (direction != 0) direction = 2; break;
                case 'a': if (direction != 1) direction = 3; break;
                case 'd': if (direction != 3) direction = 1; break;
            }
        }

        static void PixelAddFood()
        {
            Random r = new Random();
            int attempts = 0;
            while (attempts < 100)
            {
                int row = r.Next(gridH);
                int col = r.Next(gridW);
                if (grid[row, col].val == " ")
                {
                    grid[row, col].val = "🍎";
                    return;
                }
                attempts++;
            }
        }

        static void PixelEatFood()
        {
            snakeLength++;
            RetroAudio.PlayEat();
            PixelAddFood();
        }

        static void PixelMove()
        {
            int newRow = currentCell.y;
            int newCol = currentCell.x;

            switch (direction)
            {
                case 0: newRow--; break;
                case 1: newCol++; break;
                case 2: newRow++; break;
                case 3: newCol--; break;
            }

            if (newRow < 0 || newRow >= gridH || newCol < 0 || newCol >= gridW)
            {
                RetroAudio.PlayGameOver();
                Lose();
                return;
            }

            if (grid[newRow, newCol].val == "🧱")
            {
                RetroAudio.PlayGameOver();
                Lose();
                return;
            }

            PixelVisitCell(grid[newRow, newCol]);
        }

        static void PixelCheckCell(Cell cell)
        {
            if (cell.val == "🍎")
            {
                PixelEatFood();
            }
            if (cell.visited)
            {
                RetroAudio.PlayGameOver();
                Lose();
            }
        }

        static void Lose()
        {
            Lost = true;
        }

        static void PixelVisitCell(Cell cell)
        {
            currentCell.val = "⬜";
            currentCell.visited = true;
            currentCell.decay = snakeLength;
            PixelCheckCell(cell);
            currentCell = cell;
            PixelUpdatePos();
        }

        static void PixelUpdatePos()
        {
            switch (direction)
            {
                case 0: currentCell.val = "⬆️"; break;
                case 1: currentCell.val = "➡️"; break;
                case 2: currentCell.val = "⬇️"; break;
                case 3: currentCell.val = "⬅️"; break;
                default: currentCell.val = "🐍"; break;
            }
            currentCell.visited = false;
        }

        static void PixelPopulateGrid()
        {
            for (int row = 0; row < gridH; row++)
            {
                for (int col = 0; col < gridW; col++)
                {
                    Cell cell = new Cell();
                    cell.x = col;
                    cell.y = row;
                    cell.visited = false;
                    cell.decay = 0;

                    if (row == 0 || row == gridH - 1 || col == 0 || col == gridW - 1)
                        cell.Set("🧱");
                    else
                        cell.Clear();

                    grid[row, col] = cell;
                }
            }
        }

        static void PixelPrintGrid()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            for (int row = 0; row < gridH; row++)
            {
                for (int col = 0; col < gridW; col++)
                {
                    grid[row, col].decaySnake();

                    // Color based on cell content
                    if (grid[row, col].val == "🍎")
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (grid[row, col].val == "🧱")
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    else if (grid[row, col].val == "⬆️" || grid[row, col].val == "⬇️" ||
                             grid[row, col].val == "➡️" || grid[row, col].val == "⬅️")
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (grid[row, col].val == "⬜")
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    else
                        Console.ForegroundColor = ConsoleColor.DarkGreen;

                    Console.Write(grid[row, col].val);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        #endregion

        #region Tic-Tac-Toe - Pixel Edition
        static void PixelTicTacToe()
        {
            arr = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            player = 1;
            flag = 0;

            Console.Clear();
            PixelEngine.DrawPixelBorder(60, 12, 40, 5, "❌ TIC-TAC-TOE ⭕");
            Console.SetCursorPosition(45, 10);
            Console.Write("Player 1: ❌   Player 2: ⭕");
            Console.SetCursorPosition(45, 12);
            Console.Write("Press any key to start...");
            Console.ReadKey();

            do
            {
                Console.Clear();
                PixelEngine.DrawPixelBorder(50, 12, 45, 3, "🎮 TIC-TAC-TOE");

                Console.SetCursorPosition(55, 7);
                Console.WriteLine($"Player {(player % 2 == 0 ? "2 (⭕)" : "1 (❌)")}'s turn");
                PixelBoard();

                Console.SetCursorPosition(55, 17);
                Console.Write("📍 POSITION (1-9): ");

                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 9)
                {
                    if (arr[choice] != '❌' && arr[choice] != '⭕')
                    {
                        arr[choice] = (player % 2 == 0) ? '⭕' : '❌';
                        player++;
                        RetroAudio.PlayBlip();
                    }
                    else
                    {
                        Console.SetCursorPosition(55, 19);
                        Console.Write("⚠️ SPACE TAKEN!");
                        RetroAudio.PlayError();
                        Thread.Sleep(1000);
                    }
                    flag = PixelCheckWin();
                }
                else
                {
                    Console.SetCursorPosition(55, 19);
                    Console.Write("❌ INVALID INPUT!");
                    RetroAudio.PlayError();
                    Thread.Sleep(1000);
                }
            }
            while (flag != 1 && flag != -1);

            Console.Clear();
            PixelEngine.DrawPixelBorder(50, 12, 45, 5, flag == 1 ? "🏆 WINNER! 🏆" : "🤝 DRAW! 🤝");
            PixelBoard();

            Console.SetCursorPosition(55, 15);
            if (flag == 1)
                Console.WriteLine($"✨ Player {(player % 2) + 1} WINS! ✨");
            else
                Console.WriteLine($"📊 GAME ENDED IN A DRAW!");

            Console.SetCursorPosition(55, 17);
            Console.Write("Press any key to continue...");
            RetroAudio.PlaySuccess();
            Console.ReadKey();
        }

        static void PixelBoard()
        {
            Console.SetCursorPosition(55, 9);
            Console.WriteLine("┌─────┬─────┬─────┐");
            Console.SetCursorPosition(55, 10);
            Console.WriteLine($"│  {arr[1]}  │  {arr[2]}  │  {arr[3]}  │");
            Console.SetCursorPosition(55, 11);
            Console.WriteLine("├─────┼─────┼─────┤");
            Console.SetCursorPosition(55, 12);
            Console.WriteLine($"│  {arr[4]}  │  {arr[5]}  │  {arr[6]}  │");
            Console.SetCursorPosition(55, 13);
            Console.WriteLine("├─────┼─────┼─────┤");
            Console.SetCursorPosition(55, 14);
            Console.WriteLine($"│  {arr[7]}  │  {arr[8]}  │  {arr[9]}  │");
            Console.SetCursorPosition(55, 15);
            Console.WriteLine("└─────┴─────┴─────┘");
        }

        static int PixelCheckWin()
        {
            for (int i = 1; i <= 7; i += 3)
                if (arr[i] == arr[i + 1] && arr[i + 1] == arr[i + 2])
                    return 1;

            for (int i = 1; i <= 3; i++)
                if (arr[i] == arr[i + 3] && arr[i + 3] == arr[i + 6])
                    return 1;

            if (arr[1] == arr[5] && arr[5] == arr[9]) return 1;
            if (arr[3] == arr[5] && arr[5] == arr[7]) return 1;

            for (int i = 1; i <= 9; i++)
                if (arr[i] != '❌' && arr[i] != '⭕')
                    return 0;

            return -1;
        }
        #endregion

        static void ShowPixelCredits()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;

            string[] creditsArt = {
                "╔══════════════════════════════════════════════════════════════════════╗",
                "║                                                                      ║",
                "║    ████████╗███████╗ █████╗ ███╗   ███╗                           ║",
                "║    ╚══██╔══╝██╔════╝██╔══██╗████╗ ████║    ██╗██████╗ ██████╗ ███████╗║",
                "║       ██║   █████╗  ███████║██╔████╔██║    ██║██╔══██╗██╔══██╗██╔════╝║",
                "║       ██║   ██╔══╝  ██╔══██║██║╚██╔╝██║    ██║██████╔╝██████╔╝█████╗  ║",
                "║       ██║   ███████╗██║  ██║██║ ╚═╝ ██║    ██║██╔══██╗██╔══██╗██╔══╝  ║",
                "║       ╚═╝   ╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝    ╚═╝██║  ██║██║  ██║███████╗║",
                "║                                                                      ║",
                "╚══════════════════════════════════════════════════════════════════════╝"
            };

            foreach (string line in creditsArt)
            {
                Console.SetCursorPosition(10, Console.CursorTop);
                PixelEngine.AnimatePixelText(line, 20);
                Console.WriteLine();
            }

            Thread.Sleep(500);

            string[] team = {
                "",
                "╔══════════════════════════════════════════════════════════════════════╗",
                "║                         👾 DEVELOPER TEAM 👾                         ║",
                "╠══════════════════════════════════════════════════════════════════════╣",
                "║                                                                      ║",
                "║                    🎮 KINGLY VILLAMOR - LEAD DEV                     ║",
                "║                    🎨 JULLIANE KAYE IGNACIO - UI/UX                  ║",
                "║                    🕹️ NOVEM PYAR ROSERO - GAME DEV                   ║",
                "║                    🔧 JOHN GABRIEL FUENTES - QA                      ║",
                "║                                                                      ║",
                "║                    ✨ FIRST YEAR FINAL PROJECT 2022 ✨               ║",
                "║                                                                      ║",
                "╚══════════════════════════════════════════════════════════════════════╝"
            };

            foreach (string line in team)
            {
                Console.SetCursorPosition(10, Console.CursorTop);
                Console.WriteLine(line);
                Thread.Sleep(100);
            }

            Console.SetCursorPosition(45, 35);
            Console.Write("Press any key to return...");
            Console.ReadKey();
        }
    }

    public class Cell
    {
        public string val { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public bool visited { get; set; }
        public int decay { get; set; }

        public void decaySnake()
        {
            decay -= 1;
            if (decay == 2)
            {
                visited = false;
                val = " ";
            }
        }

        public void Clear()
        {
            val = " ";
        }

        public void Set(string newVal)
        {
            val = newVal;
        }
    }
}
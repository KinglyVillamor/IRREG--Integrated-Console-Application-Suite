using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Media;

namespace TRIAL3_withlogindesign
{
    class Program
    {
        // Game constants
        static readonly int gridW = 90;
        static readonly int gridH = 20;
        static Cell[,] grid = new Cell[gridH, gridW];
        static Cell currentCell;
        static int direction; //0=Up 1=Right 2=Down 3=Left
        static readonly int speed = 1;
        static bool Populated = false;
        static bool Lost = false;
        static int snakeLength;

        // TicTacToe variables
        static char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static int player = 1;
        static int choice;
        static int flag = 0;

        static void Main(string[] args)
        {
            try
            {
                Console.Title = "TEAM IRREG - Multi-Purpose System";
                Console.SetWindowSize(120, 40);
                Console.ForegroundColor = ConsoleColor.Red;

                ShowStartScreen();
                Console.CursorVisible = false;
                loadingGraphics();
                login();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        static void ShowStartScreen()
        {
            Console.SetCursorPosition(50, 16);
            Console.WriteLine(@"
                      _.-=-._.-=-._.-=-._.-=-._.-=-._.-=-._.-=-._.-=-._.-=-._.-=-._.-=-._.-=-._
                    .-'---      - ---     --     ---   -----   - --       ----  ----   -     ---`-.
                     )                                                                           (
                    (                                                                             )
                     )                                                                           (
                    (                             The System is starting                          )
                     )                           Press ENTER to continue...                      (
                    (                                                                             )
                     )                                                                           (
                    (                                                                             )
                     )                                                                           (
                    (                                                                             )
                     )                                                                           (
                    (___       _       _       _       _       _       _       _       _       ___)
                        `-._.-' (___ _) (__ _ ) (_   _) (__  _) ( __ _) (__  _) (__ _ ) `-._.-'
                                `-._.-' (  ___) ( _  _) ( _ __) (_  __) (__ __) `-._.-'
                                        `-._.-' (__  _) (__  _) (_ _ _) `-._.-'
                                                `-._.-' (_ ___) `-._.-'
                                                        `-._.-'");
            Console.ReadKey();
        }

        static void loadingGraphics()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@" 

                                             _                  _
                                            | '-.            .-' |
                                            | -. '..\\,.//,.' .- |
                                            |   \  \\\||///  /   |
                                           /|    )M\/%%%%/\/(  . |\
                                          (/\  MM\/%/\||/%\\/MM  /\)
                                          (//M   \%\\\%%//%//   M\\)
                                        (// M________ /\ ________M \\)
                                         (// M\ \(--)|  |(--)/ /M \\) \\\\ 
                                          (\\ M\.  /,\\//,\  ./M //)
                                            / MMmm( \\||// )mmMM \  \\
                                             // MMM\\\||///MMM \\ \\
                                              \//''\)/||\(/''\\/ \\
                                              mrf\\( \oo/ )\\\/\
                                                   \'-..-'\/\\
                                                  #TEAM IRREG

");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;

            Console.SetCursorPosition(35, 16);
            Console.WriteLine("|------------------------------------|");
            Console.SetCursorPosition(35, 15);
            Console.WriteLine("|                                    |");
            Console.SetCursorPosition(35, 14);
            Console.WriteLine("|------------------------------------|");
            Console.SetCursorPosition(50, 20);
            Console.WriteLine(@"
                             _,.---._     ,---.                   .=-.-..-._            _,---.               
                   _.-.    ,-.' , -  `. .--.'  \      _,..---._  /==/_ /==/ \  .-._ _.='.'-,  \              
                 .-,.'|   /==/_,  ,  - \\==\-/\ \   /==/,   -  \|==|, ||==|, \/ /, /==.'-     /              
                |==|, |  |==|   .=.     /==/-|_\ |  |==|   _   _\==|  ||==|-  \|  /==/ -   .-'               
                |==|- |  |==|_ : ;=:  - \==\,   - \ |==|  .=.   |==|- ||==| ,  | -|==|_   /_,-.              
                |==|, |  |==| , '='     /==/ -   ,| |==|,|   | -|==| ,||==| -   _ |==|  , \_.' )             
                |==|- `-._\==\ -    ,_ /==/-  /\ - \|==|  '='   /==|- ||==|  /\ , \==\-  ,    ( .=. .=. .=.  
                /==/ - , ,/'.='. -   .'\==\ _.\=\.-'|==|-,   _`//==/. //==/, | |- |/==/ _  ,  /:=; :=; :=; : 
                `--`-----'   `--`--''   `--`        `-.`.____.' `--`-` `--`./  `--``--`------'  `=` `=` `=`  
");

            for (int i = 0; i < 36; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(36 + i, 15);
                Console.Write("<3");
                Thread.Sleep(50);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(45, 13);
            Console.WriteLine("Loading Complete!");
            Console.SetCursorPosition(35, 16);
            Console.WriteLine("|------------------------------------|");
            Console.SetCursorPosition(35, 15);
            Console.WriteLine("|                                    |");
            Console.SetCursorPosition(35, 14);
            Console.WriteLine("|------------------------------------|");

            for (int i = 0; i < 36; i++)
            {
                Console.SetCursorPosition(36 + i, 15);
                Console.Write(">");
            }
            Thread.Sleep(1000);
        }

        static void login()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@" 
                                              SYSTEM PRESENTATION OF 
                     
    ███        ▄████████    ▄████████   ▄▄▄▄███▄▄▄▄         ▄█     ▄████████    ▄████████    ▄████████    ▄██████▄  
▀█████████▄   ███    ███   ███    ███ ▄██▀▀▀███▀▀▀██▄      ███    ███    ███   ███    ███   ███    ███   ███    ███ 
   ▀███▀▀██   ███    █▀    ███    ███ ███   ███   ███      ███▌   ███    ███   ███    ███   ███    █▀    ███    █▀  
    ███   ▀  ▄███▄▄▄       ███    ███ ███   ███   ███      ███▌  ▄███▄▄▄▄██▀  ▄███▄▄▄▄██▀  ▄███▄▄▄      ▄███        
    ███     ▀▀███▀▀▀     ▀███████████ ███   ███   ███      ███▌ ▀▀███▀▀▀▀▀   ▀▀███▀▀▀▀▀   ▀▀███▀▀▀     ▀▀███ ████▄  
    ███       ███    █▄    ███    ███ ███   ███   ███      ███  ▀███████████ ▀███████████   ███    █▄    ███    ███ 
    ███       ███    ███   ███    ███ ███   ███   ███      ███    ███    ███   ███    ███   ███    ███   ███    ███ 
   ▄████▀     ██████████   ███    █▀   ▀█   ███   █▀       █▀     ███    ███   ███    ███   ██████████   ████████▀  
                                                                  ███    ███   ███    ███                           

         o  o   o  o                                                                                 o  o   o  o  
         |\/ \^/ \/|                                                                                 |\/ \^/ \/|
         |,-------.|                                                                                 |,-------.|
       ,-.(|)   (|),-.                                                                             ,-.(|)   (|),-.
       \_*._ ' '_.* _/                                                                             \_*._ ' '_.* _/
        /`-.`--' .-'\                                                                               /`-.`--' .-'\
   ,--./    `---'    \,--.                 Press ENTER to Log in your account...               ,--./    `---'    \,--.
   \   |(  )     (  )|   /                                                                     \   |(  )     (  )|   /
    \  | ||       || |  /                                                                       \  | ||       || |  /
     \ | /|\     /|\ | /                                                                         \ | /|\     /|\ | /
     /  \-._     _,-/  \                                                                         /  \-._     _,-/  \
    //| \\  `---'  // |\\                                                                       //| \\  `---'  // |\\
   /,-.,-.\       /,-.,-.\                                                                     /,-.,-.\       /,-.,-.\   
  o   o   o      o   o    o                                                                   o   o   o      o   o    o                         
");
            Console.ReadKey();
            Console.Clear();

            string un, pw, letter;
            int ctr = 0, menuChoice;

            do
            {
                DrawLoginBox();

                Console.Write("             \t\t|       |                             | ");
                un = Console.ReadLine();

                Console.Write("             \t\t|       |                             | ");
                pw = ReadPassword();

                DrawLoginBoxBottom();
                Console.ReadLine();
                Console.Clear();

                if (un == "UNO" && pw == "UNO")
                {
                    PlaySound();
                    ShowTeamIrregLogo();

                    string x;
                    do
                    {
                        Console.Clear();
                        DisplayMainMenu();
                        Console.Write("\t\t\t\t\t\tEnter choice: ");

                        if (int.TryParse(Console.ReadLine(), out menuChoice))
                        {
                            switch (menuChoice)
                            {
                                case 1:
                                    ShowBasicMenu(ref x);
                                    break;
                                case 2:
                                    ShowIntermediateMenu(ref x);
                                    break;
                                case 3:
                                    ShowEntertainmentMenu(ref x);
                                    break;
                                case 4:
                                    Credits();
                                    Console.Write("\n\t\t\t\t\t\tDo you want to exit? (Y/N): ");
                                    x = Console.ReadLine();
                                    Console.Clear();
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("INVALID CHOICE");
                                    Console.Write("Do you want to exit? (Y/N): ");
                                    x = Console.ReadLine();
                                    Console.Clear();
                                    break;
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("INVALID CHOICE");
                            Console.Write("Do you want to exit? (Y/N): ");
                            x = Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    while (x?.ToUpper() != "Y");
                    break;
                }
                else
                {
                    ctr++;
                    Console.WriteLine("\t\t\t\t\tWrong Username and Password! Try Again!");
                    Console.ReadLine();
                    Console.Clear();

                    if (ctr == 3)
                    {
                        ShowSystemBlocked();
                        Environment.Exit(0);
                    }
                }
            }
            while (true);
        }

        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
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

        static void DrawLoginBox()
        {
            Console.WriteLine("\n");
            Console.WriteLine("             \t\t+------------------------------------------------------------------------+");
            Console.WriteLine("             \t\t|                                                                        |");
            Console.WriteLine("             \t\t|       +--------------------------------------------------------+       |");
            Console.WriteLine("             \t\t|       |                             +------------------------+ |       |");
            Console.WriteLine("             \t\t|       |   Enter Username:           |                        | |       |");
            Console.WriteLine("             \t\t|       |                             |                        | |       |");
            Console.WriteLine("             \t\t|       |                             +------------------------+ |       |");
            Console.WriteLine("             \t\t|       +--------------------------------------------------------+       |");
            Console.WriteLine("             \t\t|                                                                        |");
            Console.WriteLine("             \t\t|       +--------------------------------------------------------+       |");
            Console.WriteLine("             \t\t|       |                             +------------------------+ |       |");
            Console.WriteLine("             \t\t|       |   Enter Password:           |                        | |       |");
            Console.WriteLine("             \t\t|       |                             |                        | |       |");
        }

        static void DrawLoginBoxBottom()
        {
            Console.WriteLine("             \t\t|       |                             +------------------------+ |       |");
            Console.WriteLine("             \t\t|       +--------------------------------------------------------+       |");
            Console.WriteLine("             \t\t|                                                                        |");
            Console.WriteLine("             \t\t+------------------------------------------------------------------------+");
        }

        static void PlaySound()
        {
            try
            {
                SoundPlayer wav = new SoundPlayer("sounds.wav");
                wav.Load();
                wav.Play();
            }
            catch { }
        }

        static void ShowTeamIrregLogo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@" 

                                             _                  _
                                            | '-.            .-' |
                                            | -. '..\\,.//,.' .- |
                                            |   \  \\\||///  /   |
                                           /|    )M\/%%%%/\/(  . |\
                                          (/\  MM\/%/\||/%\\/MM  /\)
                                          (//M   \%\\\%%//%//   M\\)
                                        (// M________ /\ ________M \\)
                                         (// M\ \(',)|  |(',)/ /M \\) \\\\  
                                          (\\ M\.  /,\\//,\  ./M //)
                                            / MMmm( \\||// )mmMM \  \\
                                             // MMM\\\||///MMM \\ \\
                                              \//''\)/||\(/''\\/ \\
                                              mrf\\( \oo/ )\\\/\
                                                   \'-..-'\/\\
                                                   #TEAM IRREG

");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;

            Console.SetCursorPosition(35, 16);
            Console.WriteLine("|------------------------------------|");
            Console.SetCursorPosition(35, 15);
            Console.WriteLine("|                                    |");
            Console.SetCursorPosition(35, 14);
            Console.WriteLine("|------------------------------------|");
            Console.SetCursorPosition(50, 20);
            Console.WriteLine(@"
                             _,.---._     ,---.                   .=-.-..-._            _,---.               
                   _.-.    ,-.' , -  `. .--.'  \      _,..---._  /==/_ /==/ \  .-._ _.='.'-,  \              
                 .-,.'|   /==/_,  ,  - \\==\-/\ \   /==/,   -  \|==|, ||==|, \/ /, /==.'-     /              
                |==|, |  |==|   .=.     /==/-|_\ |  |==|   _   _\==|  ||==|-  \|  /==/ -   .-'               
                |==|- |  |==|_ : ;=:  - \==\,   - \ |==|  .=.   |==|- ||==| ,  | -|==|_   /_,-.              
                |==|, |  |==| , '='     /==/ -   ,| |==|,|   | -|==| ,||==| -   _ |==|  , \_.' )             
                |==|- `-._\==\ -    ,_ /==/-  /\ - \|==|  '='   /==|- ||==|  /\ , \==\-  ,    ( .=. .=. .=.  
                /==/ - , ,/'.='. -   .'\==\ _.\=\.-'|==|-,   _`//==/. //==/, | |- |/==/ _  ,  /:=; :=; :=; : 
                `--`-----'   `--`--''   `--`        `-.`.____.' `--`-` `--`./  `--``--`------'  `=` `=` `=`  
                                             KINDLY WAIT FOR IT TO LOAD...                                               
");

            for (int i = 0; i < 36; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(36 + i, 15);
                Console.Write("<3");
                Thread.Sleep(50);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(45, 13);
            Console.WriteLine("Loading Complete!");
            Console.SetCursorPosition(35, 16);
            Console.WriteLine("|------------------------------------|");
            Console.SetCursorPosition(35, 15);
            Console.WriteLine("|                                    |");
            Console.SetCursorPosition(35, 14);
            Console.WriteLine("|------------------------------------|");

            for (int i = 0; i < 36; i++)
            {
                Console.SetCursorPosition(36 + i, 15);
                Console.Write("*");
            }
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine(@"
                        ███╗   ███╗ █████╗ ██╗███╗   ██╗    ███╗   ███╗███████╗███╗   ██╗██╗   ██╗
                        ████╗ ████║██╔══██╗██║████╗  ██║    ████╗ ████║██╔════╝████╗  ██║██║   ██║
                        ██╔████╔██║███████║██║██╔██╗ ██║    ██╔████╔██║█████╗  ██╔██╗ ██║██║   ██║
                        ██║╚██╔╝██║██╔══██║██║██║╚██╗██║    ██║╚██╔╝██║██╔══╝  ██║╚██╗██║██║   ██║
                        ██║ ╚═╝ ██║██║  ██║██║██║ ╚████║    ██║ ╚═╝ ██║███████╗██║ ╚████║╚██████╔╝
                        ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝    ╚═╝     ╚═╝╚══════╝╚═╝  ╚═══╝ ╚═════╝ 
                                                                        ");
        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("\t\t\t\t\t\tChoose an option:");
            Console.WriteLine("\t\t\t\t\t\t\t1. Basic");
            Console.WriteLine("\t\t\t\t\t\t\t2. Intermediate");
            Console.WriteLine("\t\t\t\t\t\t\t3. Entertainment");
            Console.WriteLine("\t\t\t\t\t\t\t4. Credits");
            Console.WriteLine("*********************************************************");
        }

        static void ShowBasicMenu(ref string exitChoice)
        {
            string letter;
            do
            {
                Console.Clear();
                Console.WriteLine(@"
                                        ██████╗  █████╗ ███████╗██╗ ██████╗
                                        ██╔══██╗██╔══██╗██╔════╝██║██╔════╝
                                        ██████╔╝███████║███████╗██║██║     
                                        ██╔══██╗██╔══██║╚════██║██║██║     
                                        ██████╔╝██║  ██║███████║██║╚██████╗
                                        ╚═════╝ ╚═╝  ╚═╝╚══════╝╚═╝ ╚═════╝
                                   
");
                Console.WriteLine("\t\t\t\t\t\tSelect an option:");
                Console.WriteLine("\t\t\t\t\t\t\tA. Swapping");
                Console.WriteLine("\t\t\t\t\t\t\tB. MDAS");
                Console.WriteLine("\t\t\t\t\t\t\tC. Highest number");
                Console.Write("\t\t\t\t\t\tEnter choice: ");
                letter = Console.ReadLine()?.ToUpper();
                Console.WriteLine("*********************************************************");

                switch (letter)
                {
                    case "A":
                        Swapping();
                        break;
                    case "B":
                        MDAS();
                        break;
                    case "C":
                        Highest();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("INVALID CHOICE");
                        break;
                }

                Console.Write("Do you want to exit this menu? (Y/N): ");
                exitChoice = Console.ReadLine();
                Console.Clear();
            } while (exitChoice?.ToUpper() != "Y");
        }

        static void ShowIntermediateMenu(ref string exitChoice)
        {
            string letter;
            do
            {
                Console.Clear();
                Console.WriteLine(@"

                ██╗███╗   ██╗████████╗███████╗██████╗ ███╗   ███╗███████╗██████╗ ██╗ █████╗ ████████╗███████╗
                ██║████╗  ██║╚══██╔══╝██╔════╝██╔══██╗████╗ ████║██╔════╝██╔══██╗██║██╔══██╗╚══██╔══╝██╔════╝
                ██║██╔██╗ ██║   ██║   █████╗  ██████╔╝██╔████╔██║█████╗  ██║  ██║██║███████║   ██║   █████╗  
                ██║██║╚██╗██║   ██║   ██╔══╝  ██╔══██╗██║╚██╔╝██║██╔══╝  ██║  ██║██║██╔══██║   ██║   ██╔══╝  
                ██║██║ ╚████║   ██║   ███████╗██║  ██║██║ ╚═╝ ██║███████╗██████╔╝██║██║  ██║   ██║   ███████╗
                ╚═╝╚═╝  ╚═══╝   ╚═╝   ╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝╚═════╝ ╚═╝╚═╝  ╚═╝   ╚═╝   ╚══════╝

");
                Console.WriteLine("\t\t\t\t\t\tSelect an option:");
                Console.WriteLine("\t\t\t\t\t\t\tA. Student Profile");
                Console.WriteLine("\t\t\t\t\t\t\tB. Grade Computation");
                Console.WriteLine("\t\t\t\t\t\t\tC. Sales Transaction");
                Console.Write("\t\t\t\t\t\tEnter choice: ");
                letter = Console.ReadLine()?.ToUpper();
                Console.WriteLine("*********************************************************");

                switch (letter)
                {
                    case "A":
                        Student_Profile();
                        break;
                    case "B":
                        Grade_Computation();
                        break;
                    case "C":
                        Sales_Transaction();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("INVALID CHOICE");
                        break;
                }

                Console.Write("Do you want to exit this menu? (Y/N): ");
                exitChoice = Console.ReadLine();
                Console.Clear();
            } while (exitChoice?.ToUpper() != "Y");
        }

        static void ShowEntertainmentMenu(ref string exitChoice)
        {
            string letter;
            do
            {
                Console.Clear();
                Console.WriteLine(@"

    ███████╗███╗   ██╗████████╗███████╗██████╗ ████████╗ █████╗ ██╗███╗   ██╗███╗   ███╗███████╗███╗   ██╗████████╗
    ██╔════╝████╗  ██║╚══██╔══╝██╔════╝██╔══██╗╚══██╔══╝██╔══██╗██║████╗  ██║████╗ ████║██╔════╝████╗  ██║╚══██╔══╝
    █████╗  ██╔██╗ ██║   ██║   █████╗  ██████╔╝   ██║   ███████║██║██╔██╗ ██║██╔████╔██║█████╗  ██╔██╗ ██║   ██║   
    ██╔══╝  ██║╚██╗██║   ██║   ██╔══╝  ██╔══██╗   ██║   ██╔══██║██║██║╚██╗██║██║╚██╔╝██║██╔══╝  ██║╚██╗██║   ██║   
    ███████╗██║ ╚████║   ██║   ███████╗██║  ██║   ██║   ██║  ██║██║██║ ╚████║██║ ╚═╝ ██║███████╗██║ ╚████║   ██║   
    ╚══════╝╚═╝  ╚═══╝   ╚═╝   ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝╚═╝     ╚═╝╚══════╝╚═╝  ╚═══╝   ╚═╝   
                                                                                                               ");
                Console.WriteLine("\t\t\t\t\t\tSelect an option:");
                Console.WriteLine("\t\t\t\t\t\t\tA. Snake Game");
                Console.WriteLine("\t\t\t\t\t\t\tB. Tic Tac Toe");
                Console.Write("\t\t\t\t\t\tEnter choice: ");
                letter = Console.ReadLine()?.ToUpper();
                Console.WriteLine("*********************************************************");

                switch (letter)
                {
                    case "A":
                        Snake();
                        break;
                    case "B":
                        tictactoe();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("INVALID CHOICE");
                        break;
                }

                Console.Write("Do you want to exit this menu? (Y/N): ");
                exitChoice = Console.ReadLine();
                Console.Clear();
            } while (exitChoice?.ToUpper() != "Y");
        }

        static void ShowSystemBlocked()
        {
            Console.Clear();
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("\t\t\t\t\t\t\tSystem Blocked!");
            }
            Console.ReadKey();
        }

        // Existing functional methods (Swapping, MDAS, Highest, Student_Profile, Grade_Computation, Sales_Transaction remain the same)
        static void Swapping()
        {
            Console.Clear();
            Console.WriteLine(@"
                            ███████╗██╗    ██╗ █████╗ ██████╗ ██████╗ ██╗███╗   ██╗ ██████╗ 
                            ██╔════╝██║    ██║██╔══██╗██╔══██╗██╔══██╗██║████╗  ██║██╔════╝ 
                            ███████╗██║ █╗ ██║███████║██████╔╝██████╔╝██║██╔██╗ ██║██║  ███╗
                            ╚════██║██║███╗██║██╔══██║██╔═══╝ ██╔═══╝ ██║██║╚██╗██║██║   ██║
                            ███████║╚███╔███╔╝██║  ██║██║     ██║     ██║██║ ╚████║╚██████╔╝
                            ╚══════╝ ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝     ╚═╝     ╚═╝╚═╝  ╚═══╝ ╚═════╝ 
                                                                ");
            Console.Write("Enter First number:  ");
            if (int.TryParse(Console.ReadLine(), out int f))
            {
                Console.Write("Enter Second number:  ");
                if (int.TryParse(Console.ReadLine(), out int s))
                {
                    Console.WriteLine("**********************************");
                    Console.WriteLine("Swapping results");
                    Console.WriteLine($"First number: {s}\nSecond number: {f}");
                }
            }
            Console.ReadKey();
        }

        static void MDAS()
        {
            Console.Clear();
            Console.WriteLine(@"
                                    ███╗   ███╗██████╗  █████╗ ███████╗
                                    ████╗ ████║██╔══██╗██╔══██╗██╔════╝
                                    ██╔████╔██║██║  ██║███████║███████╗
                                    ██║╚██╔╝██║██║  ██║██╔══██║╚════██║
                                    ██║ ╚═╝ ██║██████╔╝██║  ██║███████║
                                    ╚═╝     ╚═╝╚═════╝ ╚═╝  ╚═╝╚══════╝
                                   
");
            Console.Write("Enter First number: ");
            if (double.TryParse(Console.ReadLine(), out double num1))
            {
                Console.Write("Enter Second number: ");
                if (double.TryParse(Console.ReadLine(), out double num2))
                {
                    Console.WriteLine("Choose an operator");
                    Console.WriteLine("\t+ : Addition");
                    Console.WriteLine("\t- : Subtraction");
                    Console.WriteLine("\t* : Multiplication");
                    Console.WriteLine("\t/ : Division");
                    Console.Write("Enter an operator: ");

                    switch (Console.ReadLine())
                    {
                        case "+":
                            Console.WriteLine($"The Sum is {num1 + num2}");
                            break;
                        case "-":
                            Console.WriteLine($"The difference is {num1 - num2}");
                            break;
                        case "*":
                            Console.WriteLine($"The product is {num1 * num2}");
                            break;
                        case "/":
                            Console.WriteLine(num2 != 0 ? $"The quotient is {num1 / num2:F2}" : "Cannot divide by zero!");
                            break;
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
                }
            }
            Console.ReadKey();
        }

        static void Highest()
        {
            Console.Clear();
            Console.WriteLine(@"
    ██╗  ██╗██╗ ██████╗ ██╗  ██╗███████╗███████╗████████╗    ███╗   ██╗██╗   ██╗███╗   ███╗██████╗ ███████╗██████╗ 
    ██║  ██║██║██╔════╝ ██║  ██║██╔════╝██╔════╝╚══██╔══╝    ████╗  ██║██║   ██║████╗ ████║██╔══██╗██╔════╝██╔══██╗
    ███████║██║██║  ███╗███████║█████╗  ███████╗   ██║       ██╔██╗ ██║██║   ██║██╔████╔██║██████╔╝█████╗  ██████╔╝
    ██╔══██║██║██║   ██║██╔══██║██╔══╝  ╚════██║   ██║       ██║╚██╗██║██║   ██║██║╚██╔╝██║██╔══██╗██╔══╝  ██╔══██╗
    ██║  ██║██║╚██████╔╝██║  ██║███████╗███████║   ██║       ██║ ╚████║╚██████╔╝██║ ╚═╝ ██║██████╔╝███████╗██║  ██║
    ╚═╝  ╚═╝╚═╝ ╚═════╝ ╚═╝  ╚═╝╚══════╝╚══════╝   ╚═╝       ╚═╝  ╚═══╝ ╚═════╝ ╚═╝     ╚═╝╚═════╝ ╚══════╝╚═╝  ╚═╝                                                                                                                           
");
            Console.Write("Input the 1st number\t:");
            if (double.TryParse(Console.ReadLine(), out double n1))
            {
                Console.Write("Input the 2nd number\t:");
                if (double.TryParse(Console.ReadLine(), out double n2))
                {
                    Console.Write("Input the 3rd number\t:");
                    if (double.TryParse(Console.ReadLine(), out double n3))
                    {
                        double highest = Math.Max(n1, Math.Max(n2, n3));
                        Console.WriteLine($"\n{highest} is the highest number among the three.");
                    }
                }
            }
            Console.ReadKey();
        }

        static void Student_Profile()
        {
            try
            {
                Console.WindowWidth = 155;
                Console.WindowHeight = 30;
            }
            catch { }

            Console.Clear();
            Console.WriteLine(@"
 ______     ______   __  __     _____     ______     __   __     ______      ______   ______     ______     ______   __     __         ______    
/\  ___\   /\__  _\ /\ \/\ \   /\  __-.  /\  ___\   /\ ""-.\ \   /\__  _\    /\  == \ /\  == \   /\  __ \   /\  ___\ /\ \   /\ \       /\  ___\   
\ \___  \  \/_/\ \/ \ \ \_\ \  \ \ \/\ \ \ \  __\   \ \ \-.  \  \/_/\ \/    \ \  _-/ \ \  __<   \ \ \/\ \  \ \  __\ \ \ \  \ \ \____  \ \  __\   
 \/\_____\    \ \_\  \ \_____\  \ \____-  \ \_____\  \ \_\\""\_\    \ \_\     \ \_\    \ \_\ \_\  \ \_____\  \ \_\    \ \_\  \ \_____\  \ \_____\ 
  \/_____/     \/_/   \/_____/   \/____/   \/_____/   \/_/ \/_/     \/_/      \/_/     \/_/ /_/   \/_____/   \/_/     \/_/   \/_____/   \/_____/ 
");
            Console.Write("\t\t\t\t\t\t\tEnter number of Student: ");
            if (!int.TryParse(Console.ReadLine(), out int nos) || nos <= 0)
            {
                Console.WriteLine("Invalid number!");
                Console.ReadKey();
                return;
            }

            string[] fn = new string[nos];
            string[] mn = new string[nos];
            string[] ln = new string[nos];
            string[] sno = new string[nos];
            string[] crs = new string[nos];
            string[] sec = new string[nos];
            string[] bday = new string[nos];
            string[] age = new string[nos];

            for (int i = 0; i < nos; i++)
            {
                Console.Clear();
                Console.WriteLine(@"
 ______     ______   __  __     _____     ______     __   __     ______      ______   ______     ______     ______   __     __         ______    
/\  ___\   /\__  _\ /\ \/\ \   /\  __-.  /\  ___\   /\ ""-.\ \   /\__  _\    /\  == \ /\  == \   /\  __ \   /\  ___\ /\ \   /\ \       /\  ___\   
\ \___  \  \/_/\ \/ \ \ \_\ \  \ \ \/\ \ \ \  __\   \ \ \-.  \  \/_/\ \/    \ \  _-/ \ \  __<   \ \ \/\ \  \ \  __\ \ \ \  \ \ \____  \ \  __\   
 \/\_____\    \ \_\  \ \_____\  \ \____-  \ \_____\  \ \_\\""\_\    \ \_\     \ \_\    \ \_\ \_\  \ \_____\  \ \_\    \ \_\  \ \_____\  \ \_____\ 
  \/_____/     \/_/   \/_____/   \/____/   \/_____/   \/_/ \/_/     \/_/      \/_/     \/_/ /_/   \/_____/   \/_/     \/_/   \/_____/   \/_____/ 
");
                Console.Write("\t\t\t\t\t\t\tEnter Student Number: ");
                sno[i] = Console.ReadLine();
                Console.Write("\t\t\t\t\t\t\tEnter First Name: ");
                fn[i] = Console.ReadLine();
                Console.Write("\t\t\t\t\t\t\tEnter Middle Name: ");
                mn[i] = Console.ReadLine();
                Console.Write("\t\t\t\t\t\t\tEnter Last Name: ");
                ln[i] = Console.ReadLine();
                Console.Write("\t\t\t\t\t\t\tEnter Course: ");
                crs[i] = Console.ReadLine();
                Console.Write("\t\t\t\t\t\t\tEnter Section: ");
                sec[i] = Console.ReadLine();
                Console.Write("\t\t\t\t\t\t\tEnter Birthday (mm/dd/yyyy): ");
                bday[i] = Console.ReadLine();
                Console.Write("\t\t\t\t\t\t\tEnter Age: ");
                age[i] = Console.ReadLine();
            }

            Console.Clear();
            Console.WriteLine(new string('*', 120));
            Console.WriteLine($"{"Student Number",-15} {"Full Name",-35} {"Course",-15} {"Section",-10} {"Birthday",-12} {"Age",-5}");
            Console.WriteLine(new string('*', 120));

            for (int i = 0; i < nos; i++)
            {
                string middleInitial = mn[i].Length > 0 ? mn[i].ToUpper()[0].ToString() : "";
                Console.WriteLine($"{sno[i],-15} {fn[i].ToUpper()} {middleInitial}. {ln[i].ToUpper(),-30} {crs[i].ToUpper(),-15} {sec[i],-10} {bday[i],-12} {age[i],-5}");
            }
            Console.ReadKey();
        }

        static void Grade_Computation()
        {
            Console.Clear();
            Console.WriteLine(@"
         ▄▄ • ▄▄▄   ▄▄▄· ·▄▄▄▄  ▄▄▄ ..▄▄ ·      ▄▄·       • ▌ ▄ ·.  ▄▄▄·▄• ▄▌▄▄▄▄▄ ▄▄▄· ▄▄▄▄▄▪         ▐ ▄ 
        ▐█ ▀ ▪▀▄ █·▐█ ▀█ ██▪ ██ ▀▄.▀·▐█ ▀.     ▐█ ▌▪▪     ·██ ▐███▪▐█ ▄██▪██▌•██  ▐█ ▀█ •██  ██ ▪     •█▌▐█
        ▄█ ▀█▄▐▀▀▄ ▄█▀▀█ ▐█· ▐█▌▐▀▀▪▄▄▀▀▀█▄    ██ ▄▄ ▄█▀▄ ▐█ ▌▐▌▐█· ██▀·█▌▐█▌ ▐█.▪▄█▀▀█  ▐█.▪▐█· ▄█▀▄ ▐█▐▐▌
        ▐█▄▪▐█▐█•█▌▐█ ▪▐▌██. ██ ▐█▄▄▌▐█▄▪▐█    ▐███▌▐█▌.▐▌██ ██▌▐█▌▐█▪·•▐█▄█▌ ▐█▌·▐█ ▪▐▌ ▐█▌·▐█▌▐█▌.▐▌██▐█▌
        ·▀▀▀▀ .▀  ▀ ▀  ▀ ▀▀▀▀▀•  ▀▀▀  ▀▀▀▀     ·▀▀▀  ▀█▄▀▪▀▀  █▪▀▀▀.▀    ▀▀▀  ▀▀▀  ▀  ▀  ▀▀▀ ▀▀▀ ▀█▄▀▪▀▀ █▪
");
            Console.Write("Enter Number of Students: ");
            if (!int.TryParse(Console.ReadLine(), out int nos) || nos <= 0)
            {
                Console.WriteLine("Invalid number!");
                Console.ReadKey();
                return;
            }

            string[] sname = new string[nos];
            double[] p = new double[nos];
            double[] m = new double[nos];
            double[] f = new double[nos];
            double[] ave = new double[nos];

            for (int i = 0; i < nos; i++)
            {
                Console.WriteLine($"\n--- Student {i + 1} ---");
                Console.Write("Enter Student Full Name: ");
                sname[i] = Console.ReadLine();

                Console.Write("Enter Prelim Grade (0-100): ");
                p[i] = GetValidGrade();

                Console.Write("Enter Midterm Grade (0-100): ");
                m[i] = GetValidGrade();

                Console.Write("Enter Finals Grade (0-100): ");
                f[i] = GetValidGrade();

                ave[i] = (p[i] + m[i] + f[i]) / 3;
            }

            Console.Clear();
            Console.WriteLine("GRADES COMPUTATION");
            Console.WriteLine(new string('*', 100));
            Console.WriteLine($"{"Student Name",-30} {"Prelim",10} {"Midterm",10} {"Finals",10} {"Average",10} {"Status",10}");
            Console.WriteLine(new string('*', 100));

            for (int i = 0; i < nos; i++)
            {
                string status = ave[i] >= 75 ? "PASSED" : "FAILED";
                Console.WriteLine($"{sname[i].ToUpper(),-30} {p[i],10:F2} {m[i],10:F2} {f[i],10:F2} {ave[i],10:F2} {status,10}");
            }
            Console.ReadKey();
        }

        static double GetValidGrade()
        {
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double grade) && grade >= 0 && grade <= 100)
                    return grade;
                Console.Write("Invalid! Enter grade (0-100): ");
            }
        }

        static void Sales_Transaction()
        {
            Console.Clear();
            Console.WriteLine("███████╗ █████╗ ██╗     ███████╗███████╗\r\n██╔════╝██╔══██╗██║     ██╔════╝██╔════╝\r\n███████╗███████║██║     █████╗  ███████╗\r\n╚════██║██╔══██║██║     ██╔══╝  ╚════██║\r\n███████║██║  ██║███████╗███████╗███████║\r\n╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝\r\n");
            Console.Write("Input Number of Purchased Items: ");
            if (!int.TryParse(Console.ReadLine(), out int noofitems) || noofitems <= 0)
            {
                Console.WriteLine("Invalid number!");
                Console.ReadKey();
                return;
            }

            string[] items = new string[noofitems];
            double[] price = new double[noofitems];
            double total = 0;

            for (int i = 0; i < noofitems; i++)
            {
                Console.Write($"Item {i + 1} name: ");
                items[i] = Console.ReadLine();
                Console.Write("Price: ₱");
                if (double.TryParse(Console.ReadLine(), out double p))
                {
                    price[i] = p;
                    total += p;
                }
                else
                {
                    Console.WriteLine("Invalid price! Try again.");
                    i--;
                }
            }

            Console.WriteLine($"\nTotal Amount: ₱{total:F2}");
            Console.Write("Input Cash Payment: ₱");
            if (double.TryParse(Console.ReadLine(), out double cash) && cash >= total)
            {
                double change = cash - total;
                Console.Clear();
                Console.WriteLine("TEAM IRREG STORE OFFICIAL RECEIPT");
                Console.WriteLine(new string('*', 50));
                Console.WriteLine($"{"ITEM",-30} {"PRICE",10}");
                Console.WriteLine(new string('-', 50));

                for (int i = 0; i < noofitems; i++)
                {
                    Console.WriteLine($"{items[i].ToUpper(),-30} ₱{price[i],10:F2}");
                }

                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"{"TOTAL",-30} ₱{total,10:F2}");
                Console.WriteLine($"{"CASH",-30} ₱{cash,10:F2}");
                Console.WriteLine($"{"CHANGE",-30} ₱{change,10:F2}");
                Console.WriteLine(new string('*', 50));
            }
            else
            {
                Console.WriteLine("Insufficient payment!");
            }
            Console.ReadKey();
        }

        // Snake Game Methods
        static void Snake()
        {
            if (!Populated)
            {
                snakeLength = 5;
                direction = 1; // Start moving right
                populateGrid();
                currentCell = grid[gridH / 2, gridW / 2];
                updatePos();
                addFood();
                Populated = true;
                Lost = false;
            }

            while (!Lost)
            {
                Restart();
            }

            // Reset for next game
            Lost = false;
            Populated = false;
        }

        static void Restart()
        {
            Console.SetCursorPosition(0, 0);
            printGrid();
            Console.WriteLine($"Length: {snakeLength}");
            getInput();
        }

        static void getInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);
                doInput(input.KeyChar);
            }
            else
            {
                Move();
                updateScreen();
            }
        }

        static void updateScreen()
        {
            Console.SetCursorPosition(0, 0);
            printGrid();
            Console.WriteLine($"Length: {snakeLength}");
            Thread.Sleep(speed * 100);
        }

        static void checkCell(Cell cell)
        {
            if (cell.val == "%")
            {
                eatFood();
            }
            if (cell.visited)
            {
                Lose();
            }
        }

        static void Lose()
        {
            Lost = true;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n╔════════════════════════════════════╗");
            Console.WriteLine("║            GAME OVER!              ║");
            Console.WriteLine($"║         Final Score: {snakeLength}              ║");
            Console.WriteLine("║                                     ║");
            Console.WriteLine("║      Press any key to continue      ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.ReadKey(true);
        }

        static void doInput(char inp)
        {
            switch (inp)
            {
                case 'w': if (direction != 2) direction = 0; break;
                case 's': if (direction != 0) direction = 2; break;
                case 'a': if (direction != 1) direction = 3; break;
                case 'd': if (direction != 3) direction = 1; break;
            }
        }

        static void addFood()
        {
            Random r = new Random();
            int attempts = 0;
            while (attempts < 100)
            {
                int row = r.Next(gridH);
                int col = r.Next(gridW);
                if (grid[row, col].val == " ")
                {
                    grid[row, col].val = "%";
                    return;
                }
                attempts++;
            }
        }

        static void eatFood()
        {
            snakeLength++;
            addFood();
        }

        static void Move()
        {
            int newRow = currentCell.y;
            int newCol = currentCell.x;

            switch (direction)
            {
                case 0: newRow--; break; // Up
                case 1: newCol++; break; // Right
                case 2: newRow++; break; // Down
                case 3: newCol--; break; // Left
            }

            // Check boundaries
            if (newRow < 0 || newRow >= gridH || newCol < 0 || newCol >= gridW)
            {
                Lose();
                return;
            }

            // Check wall collision
            if (grid[newRow, newCol].val == "*")
            {
                Lose();
                return;
            }

            visitCell(grid[newRow, newCol]);
        }

        static void visitCell(Cell cell)
        {
            currentCell.val = "#";
            currentCell.visited = true;
            currentCell.decay = snakeLength;
            checkCell(cell);
            currentCell = cell;
            updatePos();
        }

        static void updatePos()
        {
            switch (direction)
            {
                case 0: currentCell.val = "^"; break;
                case 1: currentCell.val = ">"; break;
                case 2: currentCell.val = "v"; break;
                case 3: currentCell.val = "<"; break;
                default: currentCell.val = "@"; break;
            }
            currentCell.visited = false;
        }

        static void populateGrid()
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
                        cell.Set("*");
                    else
                        cell.Clear();

                    grid[row, col] = cell;
                }
            }
        }

        static void printGrid()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(@"
                                ╔═╗┌┐┌┌─┐┬┌─┌─┐  ┬┌┬┐┬
                                ╚═╗│││├─┤├┴┐├┤   │ │ │
                                ╚═╝┘└┘┴ ┴┴ ┴└─┘  ┴ ┴ o
Game controls:
w= Up   a= Left     s= Down     d= Right
");

            for (int row = 0; row < gridH; row++)
            {
                for (int col = 0; col < gridW; col++)
                {
                    grid[row, col].decaySnake();
                    Console.Write(grid[row, col].val);
                }
                Console.WriteLine();
            }
        }

        // TicTacToe Methods
        static void tictactoe()
        {
            // Reset game state
            arr = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            player = 1;
            flag = 0;

            Console.Clear();
            Console.Write(@"
╔══════════════════════════════════════════════════════════════════╗
║                    T I C   T A C   T O E                         ║
╠══════════════════════════════════════════════════════════════════╣
║  Rules:                                                          ║
║  1. Player 1 = X, Player 2 = O                                   ║
║  2. Take turns placing marks (1-9)                               ║
║  3. First to get 3 in a row wins!                                ║
║  4. Draw if board fills with no winner                           ║
╚══════════════════════════════════════════════════════════════════╝
");
            Console.WriteLine("\nPress any key to start...");
            Console.ReadKey();

            do
            {
                Console.Clear();
                Console.WriteLine("Player 1: X and Player 2: O\n");
                Console.WriteLine($"Player {(player % 2 == 0 ? "2 (O)" : "1 (X)")}'s Chance\n");
                Board();

                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 9)
                {
                    if (arr[choice] != 'X' && arr[choice] != 'O')
                    {
                        arr[choice] = (player % 2 == 0) ? 'O' : 'X';
                        player++;
                    }
                    else
                    {
                        Console.WriteLine($"\nSorry, position {choice} is already marked with {arr[choice]}!");
                        Console.WriteLine("Please wait...");
                        Thread.Sleep(2000);
                    }
                    flag = CheckWin();
                }
                else
                {
                    Console.WriteLine("\nInvalid input! Please enter a number between 1-9.");
                    Thread.Sleep(1500);
                }
            }
            while (flag != 1 && flag != -1);

            Console.Clear();
            Board();

            if (flag == 1)
                Console.WriteLine($"\nPlayer {(player % 2) + 1} has won!");
            else
                Console.WriteLine("\nIt's a Draw!");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void Board()
        {
            Console.WriteLine("╔═══════╦═══════╦═══════╗");
            Console.WriteLine($"║   {arr[1]}   ║   {arr[2]}   ║   {arr[3]}   ║");
            Console.WriteLine("╠═══════╬═══════╬═══════╣");
            Console.WriteLine($"║   {arr[4]}   ║   {arr[5]}   ║   {arr[6]}   ║");
            Console.WriteLine("╠═══════╬═══════╬═══════╣");
            Console.WriteLine($"║   {arr[7]}   ║   {arr[8]}   ║   {arr[9]}   ║");
            Console.WriteLine("╚═══════╩═══════╩═══════╝");
            Console.Write("\nINPUT CHOICE (1-9): ");
        }

        private static int CheckWin()
        {
            // Rows
            for (int i = 1; i <= 7; i += 3)
                if (arr[i] == arr[i + 1] && arr[i + 1] == arr[i + 2])
                    return 1;

            // Columns
            for (int i = 1; i <= 3; i++)
                if (arr[i] == arr[i + 3] && arr[i + 3] == arr[i + 6])
                    return 1;

            // Diagonals
            if (arr[1] == arr[5] && arr[5] == arr[9]) return 1;
            if (arr[3] == arr[5] && arr[5] == arr[7]) return 1;

            // Draw check
            bool isDraw = true;
            for (int i = 1; i <= 9; i++)
                if (arr[i] != 'X' && arr[i] != 'O')
                    isDraw = false;

            return isDraw ? -1 : 0;
        }

        static void Credits()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@" 
                         ▄████████    ▄████████    ▄████████ ████████▄   ▄█      ███        ▄████████ 
                        ███    ███   ███    ███   ███    ███ ███   ▀███ ███  ▀█████████▄   ███    ███ 
                        ███    █▀    ███    ███   ███    █▀  ███    ███ ███▌    ▀███▀▀██   ███    █▀  
                        ███         ▄███▄▄▄▄██▀  ▄███▄▄▄     ███    ███ ███▌     ███   ▀   ███        
                        ███        ▀▀███▀▀▀▀▀   ▀▀███▀▀▀     ███    ███ ███▌     ███     ▀███████████ 
                        ███    █▄  ▀███████████   ███    █▄  ███    ███ ███      ███              ███ 
                        ███    ███   ███    ███   ███    ███ ███   ▄███ ███      ███        ▄█    ███ 
                        ████████▀    ███    ███   ██████████ ████████▀  █▀      ▄████▀    ▄████████▀  
                                     ███    ███                                                       
                           
");
            Console.SetCursorPosition(0, 14);

            string list = @"
╔══════════════════════════════════════════════════════════════════════════════╗
║                         TEAM IRREG DEVELOPERS                                 ║
╠══════════════════════════════════════════════════════════════════════════════╣
║                                                                              ║
║                           Kingly Villamor                                    ║
║                         Julliane Kaye Ignacio                                ║
║                          Novem Pyar Rosero                                   ║
║                         John Gabriel Fuentes                                ║
║                                                                              ║
║                      First Year Final Project - 2022                         ║
║                                                                              ║
╚══════════════════════════════════════════════════════════════════════════════╝";

            foreach (char c in list)
            {
                Console.Write(c);
                Thread.Sleep(15);
            }

            Console.ReadKey(true);
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
}
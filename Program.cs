using System;
using System.Runtime.Serialization;

namespace FPSBooster
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ResetColors();
            Random rnd = new Random();
            bool enterPressed = false;
            int lastNum = -1;

            List<string> FPSModes = new List<string>() { "30 FPS", "60 FPS", "120 FPS", "240 FPS", "480 FPS", "960 FPS", "1920 FPS", "3840 FPS", "7680 FPS", "15360 FPS", "30720 FPS", "Back" };
            List<string> PingModes = new List<string>() { "1000 ms", "500 ms", "300 ms", "100 ms", "50 ms", "30 ms", "20 ms", "10 ms", "5 ms", "3 ms", "1 ms", "Back" };

            List<int> FPSInts = new List<int>() { 30, 60, 120, 240, 480, 960, 1920, 3840, 7680, 15360, 30720 };
            List<int> PingInts = new List<int>() { 1000, 500, 300, 100, 50, 30, 20, 10, 5, 3, 1 };

            int fpsD = 54;

            int pingD = 86;

            int temperature = 45;

            int fTDivision = 1;
            double frametime;
            Console.Clear();
            while (true)
            {
                double fps = rnd.Next(fpsD - 5 * fpsD.ToString().Length, fpsD + 12 * fpsD.ToString().Length);
                if (fps < 1000)
                    fTDivision = 1;
                else if (fps > 1000)
                    fTDivision = 3;
                frametime = Math.Round(1000 / fps, fTDivision);
                double ping = rnd.Next((pingD - 1) * 10, (pingD + 2) * 10) / 10.0;
                if (fps > 100)
                    temperature += Convert.ToInt32(fps / 100);
                if (fps < 100)
                    temperature = rnd.Next(Convert.ToInt32((fpsD - 2) * 0.7f), Convert.ToInt32((fpsD + 3) * 0.7f));
                Console.SetCursorPosition(0, 0);
                Console.Write("[Current FPS]       ");
                Console.SetCursorPosition(0, 1);
                Console.Write("GPU: Gigabyte GeForce RTX 3050 2 GB GDDR3");
                Console.SetCursorPosition(0, 2);
                Console.Write("Performance: " + fps + " FPS ");
                Console.SetCursorPosition(0, 3);
                Console.Write("Frame Time: " + frametime + " ms  ");
                Console.SetCursorPosition(0, 4);
                Console.Write("Ping: " + ping + " ms  ");
                Console.SetCursorPosition(0, 5);
                Console.Write("GPU Temperature: " + temperature + " °C ");

                Console.SetCursorPosition(0, 7);
                Console.Write("Program by dleuiajs (tiktok: @eeglebguy)");

                if (Console.KeyAvailable) // если игрок куда-то нажал
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.E)
                    {
                        string fsbDString = fpsD.ToString("N1");
                        double fpsDouble = fpsD;

                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.Write("[FPS Boosting]");
                        Console.SetCursorPosition(0, 1);
                        Console.Write("Perfomance: " + fpsD + " FPS ");
                        Console.SetCursorPosition(0, 2);
                        Console.Write("Frame Time: " + (1000 / fpsDouble).ToString("0.00") + " ms  ");
                        Console.SetCursorPosition(0, 3);
                        Console.Write("Ping: " + pingD + " ms ");
                        while (true)
                        {
                            Selector("", new List<string>() { "Perfomance Configuration", "Back" }, 5);
                            if (enterPressed)
                            {
                                enterPressed = false;
                                if (lastNum == 0)
                                {
                                    Selector("[Perfomance Configuration]", new List<string>() { "FPS", "Ping", "Back" }, 1);
                                    if (lastNum == 0)
                                    {
                                        Selector("Select FPS:", FPSModes, 1);
                                        if (lastNum < 11)
                                        {
                                            fpsD = FPSInts[lastNum];
                                            Console.Clear();
                                            Console.WriteLine("Applying Settings...");
                                            break;
                                        }
                                        else if (lastNum == 11)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Going Back...");
                                            break;
                                        }
                                    }
                                    else if (lastNum == 1)
                                    {
                                        Selector("Select Ping:", PingModes, 1);
                                        if (lastNum < 11)
                                        {
                                            pingD = PingInts[lastNum];
                                            Console.Clear();
                                            Console.WriteLine("Applying Settings...");
                                            break;
                                        }
                                        else if (lastNum == 11)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Going Back...");
                                            break;
                                        }
                                    }
                                    else if (lastNum >= 2)
                                    {
                                        Console.Clear();
                                        break;
                                    }
                                }
                                else if (lastNum == 1)
                                {
                                    Console.Clear();
                                    break;
                                }
                            }

                        }
                    }
                }
                Thread.Sleep(1000);
            }

            void Selector(string firstText, List<string> texts, int row)
            {
                if (firstText != "")
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, row - 1);
                    Console.Write(firstText);
                }
                int n = row;
                foreach (string text in texts)
                {
                    Console.SetCursorPosition(0, n);
                    Console.Write(text);
                    n++;
                }
                Console.SetCursorPosition(0, row);
                int y = row;
                int yBuffer = row;
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(texts[y - row]);
                ResetColors();
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        lastNum = (y - row);
                        enterPressed = true;
                        break;
                    }
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        yBuffer = y;
                        if (y == row)
                            y = row + texts.Count - 1;
                        else
                            y--;
                        Console.SetCursorPosition(0, y);
                    }
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        yBuffer = y;
                        if (y == row + texts.Count - 1)
                            y = row;
                        else
                            y++;
                        Console.SetCursorPosition(0, y);
                    }
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(texts[y - row]);
                    ResetColors();
                    Console.SetCursorPosition(0, yBuffer);
                    Console.Write(texts[yBuffer - row]);
                    Console.SetCursorPosition(0, y);

                }
            }
        }

        static void ResetColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
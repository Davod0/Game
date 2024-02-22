using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Threading;
using Microsoft.VisualBasic;

namespace ReactionGame
{
    class Program
    {
        static List<Highscore> highscores = new List<Highscore>();

        static void Main(string[] args)
        {
            Random random = new Random();

            while (true)
            {
                Console.Clear();
                Stopwatch stopwatch = new Stopwatch();

                Console.WriteLine("Tryck valfri tangent för att starta spelet!");
                Console.ReadKey(true);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nVänta lite");

                float waitTime = random.Next(500, 3000);
                while (Console.KeyAvailable != true && waitTime > 0)
                {
                    Thread.Sleep(100);
                    waitTime -= 100;
                    Console.Write(".");
                }

                if (waitTime > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nTjuvstart! Prova igen.");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nTryck NU!!\n");
                    stopwatch.Start();
                    Console.ReadKey(true);
                    stopwatch.Stop();

                    Console.ResetColor();
                    Console.Write("Det tog: " + stopwatch.ElapsedMilliseconds + " millisekunder! ");

                    if (IsNewHighscore(stopwatch.ElapsedMilliseconds))
                    {
                        RegisterNewHighscore(stopwatch.ElapsedMilliseconds);
                    }

                    Console.WriteLine("\n\nHIGHSCORE:");
                    for (int i = GetAllHighscores().Result.Count; i > 0; i--)
                    {
                        Console.WriteLine(GetAllHighscores().Result[i]);
                    }
                }

                Console.ResetColor();
                Console.WriteLine("\nTryck på valfri tangent för att börja om, eller Q för att avsluta.");
                if (Console.ReadKey(true).Key == ConsoleKey.Q) Environment.Exit(0);
            }
        }

        private static bool IsNewHighscore(long elapsedMilliseconds)
        {
            if (highscores.Count == 0) return true;
            bool lowerFound = false;
            foreach (Highscore score in highscores)
            {
                if (score.Time <= elapsedMilliseconds)
                {
                    lowerFound = true;
                }
            }

            return !lowerFound;
        }

        static async void RegisterNewHighscore(long time)
        {
            Console.Write("Nytt rekord!");
            Console.Write("\nSkriv ditt namn: ");

            Highscore highscore = new Highscore
            {
                Name = Console.ReadLine() ?? "noname",
                Time = time
            };

            await PostHighscore(highscore);
        }



        public static async Task PostHighscore(Highscore hs)
        {
            HttpClient httpClient = new();
            await httpClient.PostAsJsonAsync<Highscore>("adresen", hs);
        }

        public static async Task<List<Highscore>> GetAllHighscores()
        {
            HttpClient httpClient = new();
            List<Highscore>? highscores = await httpClient.GetFromJsonAsync<List<Highscore>>("http://localhost:5203/highscores");
            return highscores;
        }



    }









}

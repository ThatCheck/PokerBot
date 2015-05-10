using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using HoldemHand;

namespace WinOddsMultipleThreads
{    
    class Program    
    {        
        // Holds Calculated Results
        public struct Results
        {
            public long win, ties, count;
        }

        // Delegate used in threadpool
        delegate Results CalculateOddsDelegate(ulong pocket, ulong opp, ulong board, ulong dead);

        // Method called and inserted in the threadpool
        static Results CalculateOdds(ulong pocket, ulong opp, ulong board, ulong dead)
        {
            // Initialize local results
            Results results = new Results();
            results.win = results.ties = results.count = 0;

            // Loop through all possible boards and tally the results
            foreach (ulong boardmask in Hand.Hands(board, dead | opp | pocket, 5))
            {
                uint playerHandval = Hand.Evaluate(pocket | boardmask, 7);
                uint oppHandval = Hand.Evaluate(opp | boardmask, 7);

                if (playerHandval > oppHandval)
                    results.win++;
                else if (playerHandval == oppHandval)
                    results.ties++;
                results.count++;
            }

            // Return tally 
            return results;
        }
        
        // Converts a hand iteration specification into an array of hands.
        static ulong [] HandList(ulong shared, ulong dead, int ncards)
        {
            List<ulong> result = new List<ulong>();

            foreach (ulong mask in Hand.Hands(shared, dead, ncards))
            {
                result.Add(mask);
            }
            return result.ToArray();
        }

        
        static void Main(string[] args)        
        {
            // This code calculates the probablity of As Ks winning against 
            // another random hand.            
            ulong pocketmask = Hand.ParseHand("As Ks");     // Hole hand            
            ulong board = Hand.ParseHand("");               // No board cards yet  
            long wins = 0, ties = 0, count = 0;             // Iterate through all possible opponent hole cards            
            ulong [] opphands = HandList(0UL, board | pocketmask, 2); // Create Array of Opponent Hands
            
            // Create delegate
            CalculateOddsDelegate d = new CalculateOddsDelegate(CalculateOdds);

            // Create results array
            IAsyncResult[] results = new IAsyncResult[opphands.Length];

            // start time
            double start = Hand.CurrentTime;

            // Put calculation requests into the threadpool
            for (int i = 0; i < opphands.Length; i++)
            {
                results[i] = d.BeginInvoke(pocketmask, opphands[i], 0UL, 0UL, null, null);
            }

            // Collect results once the threads have completed
            for (int i = 0; i < opphands.Length; i++)
            {
                Results r = d.EndInvoke(results[i]);
                wins += r.win;
                ties += r.ties;
                count += r.count;
            }

            // Prints: Win 67.0446323092352%            
            Console.WriteLine("Win {0}%, Elapsed Time {1}",
                (((double)wins) + ((double)ties) / 2.0) / ((double)count) * 100.0,
                Hand.CurrentTime-start);
        }
    }
}


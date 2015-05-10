using System;
using System.Collections.Generic;
using HoldemHand;

namespace WinOddsExhaustive
{
    class Program
    {
        static void Main(string[] args)
        {
            // This code calculates the probablity of As Ks winning against 
            // another random hand.            
            ulong pocketmask = Hand.ParseHand("As Ks");     // Hole hand            
            ulong board = Hand.ParseHand("");               // No board cards yet  
            long wins = 0, ties = 0, count = 0;             // Iterate through all possible opponent hole cards         
            double start = Hand.CurrentTime;

            // Iterate through all possible opponent hands
            foreach (ulong oppmask in Hand.Hands(0UL, board | pocketmask, 2))
            {
                // Iterate through all possible board cards                
                foreach (ulong boardmask in Hand.Hands(board, pocketmask | oppmask, 5))
                {
                    // Evaluate the player and opponent hands and tally the results    
                    uint pocketHandVal = Hand.Evaluate(pocketmask | boardmask, 7);
                    uint oppHandVal = Hand.Evaluate(oppmask | boardmask, 7);
                    if (pocketHandVal > oppHandVal)
                    {
                        wins++;
                    }
                    else if (pocketHandVal == oppHandVal)
                    {
                        ties++;
                    }
                    count++;
                }
            }
            // Prints: Win 67.0446323092352%            
            Console.WriteLine("Win {0}%, Elapsed Time {1}",
                (((double)wins) + ((double)ties) / 2.0) / ((double)count) * 100.0, 
                Hand.CurrentTime-start);

            
        }
    }
}




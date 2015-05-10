using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HoldemHand;

namespace WinOddsMonteCarlo
{    
    /// <summary>
    /// This program shows the increase in accuracy of the Monte Carlo method
    /// when increasing the number of samples.
    /// </summary>
    class Program    
    {        
        static void Main(string[] args)        
        {            
            // This code calculates the probablity of As Ks winning against
            // another random hand.            
            ulong pocketmask = Hand.ParseHand("As Ks"); // Hole hand      
            ulong board = Hand.ParseHand("");           // No board cards yet   
            // Trial numbers            
            int[] trialsTable = {                
                10, 50, 100, 500, 1000, 5000, 10000, 15000, 20000,                
                25000, 30000, 40000, 50000, 100000, 150000, 200000,                
                500000, 1000000, 2000000, 5000000, 10000000, 20000000            
            };            
            // timer values            
            double start = 0.0;           
                      
            Console.WriteLine("Trials,Wins,Difference,Duration");         
   
            foreach (int trials in trialsTable)            
            {                
                long wins = 0, ties = 0, count = 0;                
                // Get start time                
                start = Hand.CurrentTime;             
                // Iterate through a series board cards                
                foreach (ulong boardmask in Hand.RandomHands(board, pocketmask, 5, trials))                
                {                    
                    // Get a random opponent hand                    
                    ulong oppmask = Hand.RandomHand(boardmask | pocketmask, 2);    
                    // Evaluate the player and opponent hands                    
                    uint pocketHandVal = Hand.Evaluate(pocketmask | boardmask, 7);   
                    uint oppHandVal = Hand.Evaluate(oppmask | boardmask, 7);          
                    // Calculate Statistics                    
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

                double duration = Hand.CurrentTime - start;            
                // Correct answer is 67.0446323092352%                
                Console.WriteLine("{0},{1:0.00}%,{2:0.0000},{3:0.00000}",                    
                    trials,                          
                    (((double)wins) + ((double)ties) / 2.0) / ((double)count) *100.0,                                         
                    Math.Abs(67.0446323092352 - ((((double)wins) +                         
                    ((double)ties) / 2.0) / ((double)count) * 100)),          
                    duration);            
            }        
        }    
    }
}


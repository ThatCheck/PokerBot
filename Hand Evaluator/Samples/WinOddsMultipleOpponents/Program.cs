using System;
using HoldemHand;

// This example calculates the win odds for a player having "As Ks" against 
// 1-9 random players
namespace WinOddsMultipleOpponents
{
    class Program
    {
        // Expected output (approximate values)
        // 1: win 67.08%
        // 2: win 50.82%
        // 3: win 41.53%
        // 4: win 35.46%
        // 5: win 31.13%
        // 6: win 27.82%
        // 7: win 24.99%
        // 8: win 22.77%
        // 9: win 20.77%
        static void Main(string[] args)
        {
            const double time = 5.0;
            ulong pocket = Hand.ParseHand("As Ks");
            ulong board = Hand.ParseHand("");
            ulong dead = Hand.ParseHand("");

            for (int opponents = 1; opponents <= 9; opponents++)
            {
                Console.WriteLine("{0}: win {1:0.00}%", 
                    opponents, WinOddsMonteCarlo(pocket, board, dead, opponents, time) * 100.0);
            }
        }

        // An example of how to calculate win odds for multiple players
        static double WinOddsMonteCarlo(ulong pocket, ulong board, ulong dead, int nopponents, double duration)
        {
            System.Diagnostics.Debug.Assert(nopponents > 0 && nopponents <= 9);
            System.Diagnostics.Debug.Assert(duration > 0.0);
            System.Diagnostics.Debug.Assert(Hand.BitCount(pocket) == 2);
            System.Diagnostics.Debug.Assert(Hand.BitCount(board) >= 0 && Hand.BitCount(board) <= 5);

            // Keep track of stats
            double win = 0.0, count = 0.0;

            // Keep track of time
            double start = Hand.CurrentTime;

            // Loop for specified time duration
            while ((Hand.CurrentTime-start) < duration)
            {
                // Player and board info
                ulong boardmask = Hand.RandomHand(board, dead | pocket, 5);
                uint playerHandVal = Hand.Evaluate(pocket | boardmask);

                // Ensure that dead, board, and pocket cards are not
                // available to opponent hands.
                ulong deadmask = dead | boardmask | pocket;

                // Comparison Results
                bool greaterthan = true;
                bool greaterthanequal = true;

                // Get random opponent hand values
                for (int i = 0; i < nopponents; i++)
                {
                    // Get Opponent hand info
                    ulong oppmask = Hand.RandomHand(deadmask, 2);
                    uint oppHandVal = Hand.Evaluate(oppmask | boardmask);

                    // Remove these opponent cards from future opponents
                    deadmask |= oppmask;

                    // Determine compare status
                    if (playerHandVal < oppHandVal)
                    {
                        greaterthan = greaterthanequal = false;
                        break;
                    }
                    else if (playerHandVal <= oppHandVal)
                    {
                        greaterthan = false;
                    }
                }

                // Calculate stats
                if (greaterthan)
                    win += 1.0;
                else if (greaterthanequal)
                    win += 0.5;

                count += 1.0;
            }

            // Return stats
            return (count == 0.0 ? 0.0 : win/count);
        }
    }
}


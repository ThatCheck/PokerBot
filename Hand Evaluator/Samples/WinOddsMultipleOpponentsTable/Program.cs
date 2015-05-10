using System;
using HoldemHand;

// This example calculates the win odds for a player having "As Ks" against 
// 1-9 random players. The results is comma separated so that it can be imported
// into Excel using the .csv file type.
namespace WinOddsMultipleOpponentsTable
{
    class Program
    {
        static void Main(string[] args)
        {
            const double time = 5.0;
            ulong board = Hand.ParseHand("");
            ulong dead = Hand.ParseHand("");

            // Table Header
            Console.Write(",");
            for (int i = 1; i <= 9; i++)
            {
                Console.Write("{0},", i);
            }
            Console.WriteLine();

            // Iterates through one representative hand of each of the 169 possible
            // pocket hand types
            foreach (ulong pocket in PocketHands.Hands169())
            {
                // Show Pocker Hand
                Console.Write("\"{0}\",", Hand.MaskToString(pocket));

                // Calculate and Display the Approximate odds for 1-9 opponents
                for (int opponents = 1; opponents <= 9; opponents++)
                {
                    Console.Write("{0}%,", WinOddsMonteCarlo(pocket, board, dead, opponents, time) * 100.0);
                }
                Console.WriteLine();
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

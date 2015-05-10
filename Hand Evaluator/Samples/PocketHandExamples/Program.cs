using System;
using System.Collections.Generic;
using System.Text;
using HoldemHand;

namespace PocketHandExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            // This will iterate through all the possible "connected suited" pocket hands
            foreach (ulong pocketmask in PocketHands.Query("Connected Suited"))
            {
                // Insert calculation here.
            }

            // Looks at an AKs match up (specifically As Ks) against all possible 
            // opponents hands that are connected and suited.
            ulong mask = Hand.Evaluate("As Ks"); // AKs
            foreach (ulong oppmask in PocketHands.Query("Connected Suited", mask))
            {
                // Insert calculation here.
            }


            // Iterates through all possible "Connected Suited" versus
            // "Connected Offsuit" match ups.
            foreach (ulong playermask in PocketHands.Query("Connected Suited"))
            {
                foreach (ulong oppmask in PocketHands.Query("Connected Offsuit", playermask))
                {
                    foreach (ulong board in Hand.Hands(0UL, playermask | oppmask, 5))
                    {
                        // Insert Calculation Here
                    }
                }
            }

            // Randomly selects 100000 possible hands when player starts with a
            // suited connector
            ulong[] masks = PocketHands.Query("Connected Suited");
            for (int trials = 0; trials < 100000; trials++)
            {
                // Select a random player hand from the list of possible 
                // Connected Suited hands.
                ulong randomPlayerHandMask = Hand.RandomHand(masks, 0UL, 2);
                // Get a random opponent hand
                ulong randomOpponentHandMask = Hand.RandomHand(randomPlayerHandMask, 2);
                // Get a random board
                ulong boardMask = Hand.RandomHand(randomPlayerHandMask | randomOpponentHandMask, 5);

                // Insert evaluation here
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace GreedKata.Greed
{
    public class GreedGame
    {
        private readonly Dictionary<int, int> singleDiceScoreLookup = new Dictionary<int, int>()
        {
            {1, 100},
            {2, 0},
            {3, 0},
            {4, 0},
            {5, 50},
            {6, 0},
        };

        private readonly Dictionary<int, int> tripleDiceScoreLookup = new Dictionary<int, int>()
        {
            {1, 1000},
            {2, 200},
            {3, 300},
            {4, 400},
            {5, 500},
            {6, 600},
        };

        public int Calculate(int[] diceRolls)
        {
            int calculatedScore = 0;

           var rollCounts = GetRollCounts(diceRolls);

           if (rollCounts.Any(x => x.Value > 2))
           {
               var tripleRollKeys = rollCounts
                   .Where(x => x.Value > 2)
                   .Select(x => x.Key);

               foreach (int key in tripleRollKeys)
               {
                   calculatedScore += tripleDiceScoreLookup[key];
                   rollCounts[key] -= 3;
                   if (rollCounts[key] == 0)
                       rollCounts.Remove(key);
               }
           }


            calculatedScore += rollCounts
                .Sum(die => singleDiceScoreLookup[die.Key]);

            return calculatedScore;
        }

        private bool HasTriple(int[] dice, out int diceNumber)
        {
            bool foundUnique = false;
            diceNumber = 0;

            for (int i = 0; i < dice.Length; i++)
            {
                int rollNumber = dice[i];
                var rollCount = 1;

                for (int j = i + 1; j < dice.Length; j++)
                {
                    if (rollNumber == dice[j])
                        rollCount++;
                    else if (!foundUnique)
                    {
                        foundUnique = true;
                        i = j;
                    }
                }

                if (rollCount > 2)
                {
                    diceNumber = rollNumber;
                    return true;
                }

                foundUnique = false;
            }

            return false;
        }

        private Dictionary<int, int> GetRollCounts(int[] rolls)
        {
            var uniqueRolls = rolls.ToHashSet();

            return uniqueRolls
                .ToDictionary(x => x, x => rolls.Count(y => y == x));
        }
    }
}
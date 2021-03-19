using System;
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

        public int Calculate(params int[] diceRolls)
        {
            int calculatedScore = 0;
            var rollCounts = GetRollCounts(diceRolls);

            for (int i = 6; i >= 3; i--)
            {
                if (HasPairAmount(rollCounts, i))
                {
                    int multiplier = GetScoreDuplicatedMultiplier(i);
                    calculatedScore += ApplyScoreForDuplicateRolls(rollCounts, i, multiplier);
                }
            }

            calculatedScore += rollCounts
                .Sum(die => singleDiceScoreLookup[die.Key]);

            return calculatedScore;
        }

        private int GetScoreDuplicatedMultiplier(int amount)
        {
            return amount switch
            {
                6 => 8,
                5 => 4,
                4 => 2,
                _ => 1
            };
        }

        private bool HasPairAmount(Dictionary<int, int> rollCounts, int pair)
        {
            return rollCounts.Any(x => x.Value >= pair);
        }

        private int ApplyScoreForDuplicateRolls(Dictionary<int, int> rollCounts, int pair, int multiplier = 1)
        {
            int score = 0;

            var duplicateRolls = rollCounts
                .Where(x => x.Value >= pair)
                .Select(x => x.Key);

            foreach (int key in duplicateRolls)
            {
                score += tripleDiceScoreLookup[key] * multiplier;
                rollCounts[key] -= pair;
                if (rollCounts[key] == 0)
                    rollCounts.Remove(key);
            }

            return score;
        }

        private Dictionary<int, int> GetRollCounts(int[] rolls)
        {
            var uniqueRolls = rolls.ToHashSet();

            return uniqueRolls
                .ToDictionary(x => x, x => rolls.Count(y => y == x));
        }
    }
}
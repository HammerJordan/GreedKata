using Xunit;
using FluentAssertions;
using GreedKata.Greed;

namespace GreedKata.Test
{
    public class GreedGameTest
    {
        private readonly GreedGame greed = new GreedGame();

        [Fact]
        public void Calculate_ShouldReturn100_WhenASingle1IsRolled()
        {
            int score = greed.Calculate(new int[] {1});
            score.Should().Be(100);
        }

        [Fact]
        public void Calculate_ShouldReturn50_WhenASingle5IsRolled()
        {
            int score = greed.Calculate(new int[] {5});
            score.Should().Be(50);
        }

        [Theory]
        [InlineData(new[] {1, 1, 1}, 1000)]
        [InlineData(new[] {2, 2, 2}, 200)]
        [InlineData(new[] {3, 3, 3}, 300)]
        [InlineData(new[] {4, 4, 4}, 400)]
        [InlineData(new[] {5, 5, 5}, 500)]
        [InlineData(new[] {6, 6, 6}, 600)]
        public void Calculate_ShouldReturnTripleScore_WhenThreeOfAKindAreRolled(int[] rolls, int expectedScore)
        {
            int score = greed.Calculate(rolls);
            score.Should().Be(expectedScore);
        }

        [Theory()]
        [InlineData(new[] {2, 3, 4, 6, 2}, 0)]
        [InlineData(new[] {3, 4, 5, 3, 3}, 350)]
        public void Calculate_ShouldReturnTripleWithSingleScores(int[] rolls, int expectedScore)
        {
            int score = greed.Calculate(rolls);
            score.Should().Be(expectedScore);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1, 2)]
        [InlineData(1, 2, 3)]
        [InlineData(1, 2, 3, 4)]
        [InlineData(1, 2, 3, 4, 5)]
        [InlineData(1, 2, 3, 4, 5, 6)]
        public void Calculate_ShouldAccept1To6Die_WithOutExceptions(params int[] rolls)
        {
            greed.Calculate(rolls);
        }

        [Theory]
        [InlineData(new[] {1, 1, 1, 1}, 2000)]
        [InlineData(new[] {2, 2, 2, 2}, 400)]
        [InlineData(new[] {3, 3, 3, 3}, 600)]
        [InlineData(new[] {4, 4, 4, 4}, 800)]
        [InlineData(new[] {5, 5, 5, 5}, 1000)]
        [InlineData(new[] {6, 6, 6, 6}, 1200)]
        [InlineData(new[] {2, 2, 2, 2, 1}, 500)]
        [InlineData(new[] {2, 2, 2, 2, 1, 5}, 550)]
        public void Calculate_ShouldReturnFourOfAKindScores_WhenFourOfAKindAreRolled(int[] rolls, int expectedScore)
        {
            int score = greed.Calculate(rolls);
            score.Should().Be(expectedScore);
        }

        [Theory]
        [InlineData(new[] {1, 1, 1, 1, 1}, 4000)]
        [InlineData(new[] {2, 2, 2, 2, 2}, 800)]
        [InlineData(new[] {3, 3, 3, 3, 3}, 1200)]
        [InlineData(new[] {4, 4, 4, 4, 4}, 1600)]
        [InlineData(new[] {5, 5, 5, 5, 5}, 2000)]
        [InlineData(new[] {6, 6, 6, 6, 6}, 2400)]
        [InlineData(new[] {2, 2, 2, 2, 2, 1}, 800 + 100)]
        [InlineData(new[] {2, 2, 2, 2, 2, 1, 5}, 800 + 100 + 50)]
        public void Calculate_ShouldReturnFiveOfAKindScores_WhenFiveOfAKindAreRolled(int[] rolls, int expectedScore)
        {
            int score = greed.Calculate(rolls);
            score.Should().Be(expectedScore);
        }

        [Theory]
        [InlineData(new[] {1, 1, 1, 1, 1, 1}, 8000)]
        [InlineData(new[] {2, 2, 2, 2, 2, 2}, 1600)]
        [InlineData(new[] {3, 3, 3, 3, 3, 3}, 2400)]
        [InlineData(new[] {4, 4, 4, 4, 4, 4}, 3200)]
        [InlineData(new[] {5, 5, 5, 5, 5, 5}, 4000)]
        [InlineData(new[] {6, 6, 6, 6, 6, 6}, 4800)]
        [InlineData(new[] {2, 2, 2, 2, 2, 2, 1}, 1600 + 100)]
        [InlineData(new[] {2, 2, 2, 2, 2, 2, 1, 5}, 1600 + 100 + 50)]
        public void Calculate_ShouldReturnSixOfAKindScores_WhenSixOfAKindAreRolled(int[] rolls, int expectedScore)
        {
            int score = greed.Calculate(rolls);
            score.Should().Be(expectedScore);
        }
    }
}
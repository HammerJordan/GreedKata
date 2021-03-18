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
        [InlineData(new[] {1, 1, 1, 5, 1}, 1150)]
        [InlineData(new[] {2, 3, 4, 6, 2}, 0)]
        [InlineData(new[] {3, 4, 5, 3, 3}, 350)]
        public void Calculate_ShouldReturnTripleWithSingleScores(int[] rolls, int expectedScore)
        {
            int score = greed.Calculate(rolls);
            score.Should().Be(expectedScore);
        }
    }
}
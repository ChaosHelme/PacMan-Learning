using FluentAssertions;
using PacMan.Game.Services;

namespace PacMan.Tests.Services;

[TestFixture, TestOf(typeof(RandomNumberService))]
public class RandomNumberServiceTests
{

    [TestCase(0,5)]
    [TestCase(1,3)]
    [TestCase(-1,1)]
    [TestCase(int.MinValue,int.MaxValue)]
    public void RandomNumber_ShouldReturn_Integer_Between_Min_And_ExclusiveMax(int min, int max)
    {
        var randomNumberService = new RandomNumberService();
        
        var result = randomNumberService.RandomNumber(min, max);

        result.Should().BeInRange(min, max - 1);
    }

    [Test]
    public void RandomSingle_ShouldReturn_FloatBetween_ZeroAndOne()
    {
        var randomNumberService = new RandomNumberService();
        
        var result = randomNumberService.RandomSingle();
        
        result.Should().BeInRange(0f, 1f);
    }
}
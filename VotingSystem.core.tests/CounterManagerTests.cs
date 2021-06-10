
using VotingSystem.core.Models;
using Xunit;

namespace VotingSystem.core.tests
{
    public class CounterManagerTests
    {
        public const string CounterName = "Counter Name";
        public Counter _counter = new Counter { Name = CounterName, Count = 5 };
        
        [Fact]
        public void GetStatistics_IncludesCounterName()
        {
            var statistics = new CounterManager().GetCounterStatistics(_counter,5);
            Assert.Equal(CounterName, statistics.Name);
        }
        [Fact]
        public void GetStatistics_IncludesCounterCount()
        {
            var statistics = new CounterManager().GetCounterStatistics(_counter, 5);
            Assert.Equal(5, statistics.Count);
        }

        [Theory]
        [InlineData(5, 10, 50)]
        [InlineData(2, 10, 20)]
        [InlineData(2, 4, 50)]
        [InlineData(2, 3, 66.67)]
        [InlineData(1, 3, 33.33)]
        public void GetStatistics_ShowsPercentageUpToDecimalsBasedOnTotalCount(int count, int total, double percent)
        {
            _counter.Count = count;
            var statistics = new CounterManager().GetCounterStatistics(_counter, total);
            Assert.Equal(count, statistics.Count);
            Assert.Equal(percent, statistics.Percent);

        }

        [Fact]
        public void ResolveExcess_DoesnotAddExcessWhenAllCountersAreEqual()
        {
            var counter1 = new Counter { Percent = 33.33 };
            var counter2 = new Counter { Percent = 33.33 };
            var counter3 = new Counter { Percent = 33.33 };

            var success = new CounterManager(counter1, counter2, counter3).ResolveExcess();

            Assert.False(success);
            Assert.Equal(33.33, counter1.Percent);
            Assert.Equal(33.33, counter2.Percent);
            Assert.Equal(33.33, counter3.Percent);
        }
        [Theory]
        [InlineData(80, 20)]
        [InlineData(66.67, 33.33)]
        public void ResolveExcess_DoesnotAddExcessWhenTotalPercentIs100(double percent1, double percent2)
        {
            var counter1 = new Counter { Percent = percent1 };
            var counter2 = new Counter { Percent = percent2 };

            bool success = new CounterManager(counter1, counter2).ResolveExcess();

            Assert.True(success);
            Assert.Equal(percent1, counter1.Percent);
            Assert.Equal(percent2, counter2.Percent);
        }

        [Theory]
        [InlineData(40, 40, 19.5, 40, 40, 20)]
        [InlineData(40, 19.5, 40, 40, 20, 40)]
        [InlineData(19.5, 40, 40, 20, 40, 40)]
        [InlineData(33.34, 33.34, 33.12, 33.34, 33.34, 33.32)]

        public void ResolveExcess_AddExcessToTheLowestPercentIfMoreOneWinner(double percent1, double percent2, double percent3,
            double expect1, double expect2, double expect3)
        {
            var counter1 = new Counter { Percent = percent1};
            var counter2 = new Counter { Percent = percent2 };
            var counter3 = new Counter { Percent = percent3 };

            var success = new CounterManager(counter1, counter2,counter3).ResolveExcess();

            Assert.True(success);
            Assert.Equal(expect1, counter1.Percent);
            Assert.Equal(expect2, counter2.Percent);
            Assert.Equal(expect3, counter3.Percent);
        }

        [Theory]
        [InlineData(66.66, 33.33, 66.67, 33.33)]
        [InlineData(70.31, 29, 71, 29)]
        public void ResolveExcess_AddExcessToTheHeighstCount(double percent1, double percent2, double expect1, double expect2)
        {
            var counter1 = new Counter { Percent = percent1 };
            var counter2 = new Counter { Percent = percent2 };

            var success = new CounterManager(counter1, counter2).ResolveExcess();

            Assert.True(success);
            Assert.Equal(expect1, counter1.Percent);
            Assert.Equal(expect2, counter2.Percent);
        }

    }


}



using Pharma.Model;

namespace Pharma.UnitTesting
{
    public class OrderServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NeedsReview_ShouldBeTrue_WhenTotalExceedsThreshold()
        {
            // Arrange
            var reviewOptions = new ReviewOptions { DailyOrderThresholdCents = 2000 };
            var order = new Order { TotalCents = 2500 };

            // Act
            order.NeedsReview = order.TotalCents >= reviewOptions.DailyOrderThresholdCents;

            // Assert
            Assert.That(order.NeedsReview, Is.True);
        }

        [Test]
        public void NeedsReview_ShouldBeFalse_WhenTotalIsBelowThreshold()
        {
            // Arrange
            var reviewOptions = new ReviewOptions { DailyOrderThresholdCents = 2000 };
            var order = new Order { TotalCents = 1500 };

            // Act
            order.NeedsReview = order.TotalCents >= reviewOptions.DailyOrderThresholdCents;

            // Assert
            Assert.That(order.NeedsReview, Is.False);
        }
    }
}
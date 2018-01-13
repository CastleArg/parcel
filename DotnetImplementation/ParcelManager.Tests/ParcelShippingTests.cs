using NUnit.Framework;
using ParcelManager;
using System;

namespace ParcelManagerTests
{
    public class ParcelShippingTests
    {
        private const string expectedWeightErrorMessage = "Package must be between 0 and 25kg";
        private const string expectedDimensionErrorMessage = "dimensions must be greater than 0mm.";
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(1, 1, 1, 1, ExpectedResult = ParcelType.Small)]
        [TestCase(199, 299, 149, 1, ExpectedResult = ParcelType.Small)]
        [TestCase(200, 300, 150, 25, ExpectedResult = ParcelType.Small)]

        [TestCase(201, 301, 151, 1, ExpectedResult = ParcelType.Medium)]
        [TestCase(299, 399, 199, 1, ExpectedResult = ParcelType.Medium)]
        [TestCase(300, 400, 200, 25, ExpectedResult = ParcelType.Medium)]

        [TestCase(301, 401, 201, 1, ExpectedResult = ParcelType.Large)]
        [TestCase(399, 599, 249, 1, ExpectedResult = ParcelType.Large)]
        [TestCase(400, 600, 250, 25, ExpectedResult = ParcelType.Large)]
        public ParcelType ReturnsCorrectParcelTypeForValidArguments(int lengthMm, int breadthMm, int heightMm, decimal weightKg)
        {
            var parcel = new Parcel(lengthMm, breadthMm, heightMm, weightKg);
            return parcel.ParcelType;
        }

        [TestCase(0, 1, 1, 1, expectedDimensionErrorMessage)]
        [TestCase(1, 0, 1, 1, expectedDimensionErrorMessage)]
        [TestCase(1, 1, 0, 1, expectedDimensionErrorMessage)]
        [TestCase(1, 1, 1, 0, expectedWeightErrorMessage)]
        [TestCase(-1, 1, 1, 1, expectedDimensionErrorMessage)]
        [TestCase(1, -1, 1, 1, expectedDimensionErrorMessage)]
        [TestCase(1, 1, -1, 1, expectedDimensionErrorMessage)]
        [TestCase(1, 1, 1, -1, expectedWeightErrorMessage)]
        public void ThrowsForInvalidDimensions(int lengthMm, int breadthMm, int heightMm, decimal weightKg, string expectedMessage)
        {
            var ex = Assert.Throws<ArgumentException>(() => new Parcel(lengthMm, breadthMm, heightMm, weightKg));
            Assert.AreEqual(ex.Message, expectedMessage);
        }

        [TestCase(25.01)]
        [TestCase(26)]
        public void ThrowsForWeightGreaterThan25(decimal weightKg)
        {
            Assert.Throws<ArgumentException>(() => new Parcel(1, 1, 1, weightKg));
        }

        // hmm.  I would have used a constant for expected prices here but decimals don't play nicely with attribute args.
        [TestCase(1, 1, 1, 1, ExpectedResult = 5.00)]
        [TestCase(199, 299, 149, 1, ExpectedResult = 5.00)]
        [TestCase(200, 300, 150, 25, ExpectedResult = 5.00)]

        [TestCase(201, 301, 151, 1, ExpectedResult = 7.50)]
        [TestCase(299, 399, 199, 1, ExpectedResult = 7.50)]
        [TestCase(300, 400, 200, 25, ExpectedResult = 7.50)]

        [TestCase(301, 401, 201, 1, ExpectedResult = 8.50)]
        [TestCase(399, 599, 249, 1, ExpectedResult = 8.50)]
        [TestCase(400, 600, 250, 25, ExpectedResult = 8.50)]
        public decimal ReturnsCorrectCostForValidArguments(int lengthMm, int breadthMm, int heightMm, decimal weightKg)
        {
            var parcel = new Parcel(lengthMm, breadthMm, heightMm, weightKg);
            return parcel.CostNzd;
        }


    }
}
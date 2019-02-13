using Battleships.Models;
using Battleships.Utils;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Battleships.Interfaces;

namespace Battleships.Tests
{
    public class ShipGeneratorTests
    {
        private Mock<IRandomGenerator> _random;
        private readonly int BOARD_SIZE = 10;
        private readonly int NUMBER_OF_DIRECTIONS = 2;
        private readonly int COMMON_COLUMN_NUMBER = 1;
        private readonly int COMMON_ROW_NUMBER = 1;

        [SetUp]
        public void SetUp()
        {
            _random = new Mock<IRandomGenerator>();
        }

        [Test]
        public void GenerateShipFields_ShipSize_ShouldCreateShipSizeFieldsCount()
        {
            PrepareSequenceForRandomShipsStartPoint();
            _random.Setup(r => r.GetRandomNumber(NUMBER_OF_DIRECTIONS)).Returns(0);
            var shipSize = 4;

            var sut = new ShipGenerator(_random.Object);
            var result = sut.GenerateShipFields(shipSize);
            Assert.That(result, Has.Exactly(shipSize).Items);
        }

        [TestCase(0, "A")]
        [TestCase(1, "B")]
        [TestCase(9, "J")]
        public void GenerateShipFields_ShouldReturnProperRowLetters(int rowNumber, string expectedLetter)
        {
            _random.SetupSequence(r => r.GetRandomNumber(BOARD_SIZE))
                .Returns(COMMON_COLUMN_NUMBER)
                .Returns(rowNumber);
            _random.Setup(r => r.GetRandomNumber(NUMBER_OF_DIRECTIONS)).Returns(0);
            var shipSize = 1;

            var sut = new ShipGenerator(_random.Object);
            var result = sut.GenerateShipFields(shipSize);

            Assert.That(result.First().Substring(0, 1), Is.EqualTo(expectedLetter));
        }

        [TestCase(0, "1")]
        [TestCase(1, "2")]
        [TestCase(9, "10")]
        public void GenerateShipFields_ShouldReturnColumnNumberStaredFromOne(int columnNumber, string expectedColumnNumberResult)
        {
            _random.SetupSequence(r => r.GetRandomNumber(BOARD_SIZE))
                .Returns(columnNumber)
                .Returns(COMMON_ROW_NUMBER);
            _random.Setup(r => r.GetRandomNumber(NUMBER_OF_DIRECTIONS)).Returns(0);
            var shipSize = 1;

            var sut = new ShipGenerator(_random.Object);
            var result = sut.GenerateShipFields(shipSize);

            Assert.That(result.First().Substring(1), Is.EqualTo(expectedColumnNumberResult));
        }

        [TestCase(0, "1")]
        [TestCase(1, "2")]
        [TestCase(9, "10")]
        public void GenerateShipFields_ForColumnDirection_ShouldAllFieldsHaveSameColumnNumber(int columnNumber, string expectedColumnNumberResult)
        {
            _random.SetupSequence(r => r.GetRandomNumber(BOARD_SIZE))
               .Returns(columnNumber)
               .Returns(COMMON_ROW_NUMBER);
            _random.Setup(r => r.GetRandomNumber(NUMBER_OF_DIRECTIONS)).Returns((int)ShipDirection.Column);
            var shipSize = 3;

            var sut = new ShipGenerator(_random.Object);
            var result = sut.GenerateShipFields(shipSize);

            result.ForEach(f => Assert.That(f.Substring(1), Is.EqualTo(expectedColumnNumberResult)));
        }

        [TestCase(0, "A")]
        [TestCase(1, "B")]
        [TestCase(9, "J")]
        public void GenerateShipFields_ForRowDirection_ShouldAllFieldsHaveSameRowLetter(int rowNumber, string expectedLetter)
        {
            _random.SetupSequence(r => r.GetRandomNumber(BOARD_SIZE))
                .Returns(COMMON_COLUMN_NUMBER)
                .Returns(rowNumber);
            _random.Setup(r => r.GetRandomNumber(NUMBER_OF_DIRECTIONS)).Returns((int)ShipDirection.Row);
            var shipSize = 3;

            var sut = new ShipGenerator(_random.Object);
            var result = sut.GenerateShipFields(shipSize);

            result.ForEach(f => Assert.That(f.Substring(0,1), Is.EqualTo(expectedLetter)));
        }

        private void PrepareSequenceForRandomShipsStartPoint()
        {
            _random.SetupSequence(r => r.GetRandomNumber(BOARD_SIZE))
                .Returns(COMMON_COLUMN_NUMBER)
                .Returns(COMMON_ROW_NUMBER);
        }
    }
}

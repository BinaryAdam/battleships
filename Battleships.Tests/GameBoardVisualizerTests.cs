using Battleships.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Tests
{
    public class GameBoardVisualizerTests
    {
        BoardFields _fields;
        GameBoardVisualizer _sut;

        [SetUp]
        public void SetUp()
        {
            _fields = new BoardFields();
            _sut = new GameBoardVisualizer();
        }

        [TestCase(1, "1")]
        [TestCase(2, "1 2")]
        [TestCase(10, "1 2 3 4 5 6 7 8 9 10")]
        public void GetBoardStateVisualized_ShouldContainsProperColumnCount(int columnCount, string columnLine)
        {
            var result = _sut.GetBoardStateVisualized(columnCount, _fields);
            Assert.That(result, Does.Contain(columnLine));
        }

        [Test]
        public void GetBoardStateVisualized_ShouldContainsProperRowsCountWithRowLetters()
        {
            var result = _sut.GetBoardStateVisualized(2, _fields);

            Assert.That(result, Does.Contain("A"));
            Assert.That(result, Does.Contain("B"));
            Assert.That(result, Does.Not.Contain("C"));
        }

        [Test]
        public void GetBoardStateVisualized_ShouldShowEmptyFieldsAsDash()
        {
            var result = _sut.GetBoardStateVisualized(2, _fields);

            Assert.That(result, Does.Contain("- -"));
        }

        [Test]
        public void GetBoardStateVisualized_ShouldShowHitFieldsAsO()
        {
            _fields.Add("A1", FieldStatus.ShipHit);
            var result = _sut.GetBoardStateVisualized(2, _fields);

            Assert.That(result, Does.Contain("o -"));
        }

        [Test]
        public void GetBoardStateVisualized_ShouldShowMissFieldsAsX()
        {
            _fields.Add("A1", FieldStatus.Shooted);
            var result = _sut.GetBoardStateVisualized(2, _fields);

            Assert.That(result, Does.Contain("x -"));
        }

        [Test]
        public void GetBoardStateVisualized_ShouldShowNotHitShipFieldsAsDash()
        {
            _fields.Add("A1", FieldStatus.Ship);
            var result = _sut.GetBoardStateVisualized(2, _fields);

            Assert.That(result, Does.Contain("- -"));
        }
    }
}

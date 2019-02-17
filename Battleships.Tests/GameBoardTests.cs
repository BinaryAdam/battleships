using Battleships.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Tests
{
    public class GameBoardTests
    {
        [Test]
        public void GameBoard_WhenInitialized_ShouldHaveAllShipFieldsMarked()
        {
            var shipFields = new List<string> { "A1", "B1" };
            var sut = new GameBoard(shipFields);

            var fields = sut.GetCurrentBoardState();

            Assert.That(fields, Has.Exactly(2).Items);
            foreach (var field in shipFields)
            {
                Assert.That(fields, Contains.Key(field));
                Assert.That(fields[field], Is.EqualTo(FieldStatus.Ship));
            } 
        }

        [Test]
        public void ProcessUserShot_WhenShipField_ShouldMarkFieldAsShipHit()
        {
            var shipField = "A1";
            var shipFields = new List<string> { shipField };
            var sut = new GameBoard(shipFields);

            sut.ProcessUserShot(shipField);

            var fields = sut.GetCurrentBoardState();

            Assert.That(fields[shipField], Is.EqualTo(FieldStatus.ShipHit));
        }

        [Test]
        public void ProcessUserShot_WhenEmptyField_ShouldMarkFieldAsShooted()
        {
            var shipField = "A1";
            var emptyField = "B2";
            var shipFields = new List<string> { shipField };
            var sut = new GameBoard(shipFields);

            sut.ProcessUserShot(emptyField);

            var fields = sut.GetCurrentBoardState();

            Assert.That(fields[emptyField], Is.EqualTo(FieldStatus.Shooted));
        }

        [Test]
        public void AreAllShipSunk_AfterLastShipHit_ShouldReturnTrue()
        {
            var shipField = "A1";
            var shipFields = new List<string> { shipField };
            var sut = new GameBoard(shipFields);

            Assert.That(sut.AreAllShipSunk(), Is.Not.True);

            sut.ProcessUserShot(shipField);

            Assert.That(sut.AreAllShipSunk(), Is.True);
        }
    }
}

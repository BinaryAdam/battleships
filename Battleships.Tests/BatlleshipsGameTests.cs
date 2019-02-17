using Battleships.Interfaces;
using Battleships.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Tests
{
    public class BatlleshipsGameTests
    {
        private IGameBoardVisualizer _boardVisualizer;
        private IContentWriter _writer;
        private Mock<IInputValidator> _inputValidatorMock;
        private Mock<IInputReader> _inputReaderMock;
        private Mock<IGameBoard> _gameBoardMock;
        private readonly bool WIN_TURN = true;
        private readonly bool TURN_WITHOUT_WIN = false;
        private readonly string USER_INPUT = "";

        [SetUp]
        public void SetUp()
        {
            _boardVisualizer = new Mock<IGameBoardVisualizer>().Object;
            _writer = new Mock<IContentWriter>().Object;
            _inputValidatorMock = new Mock<IInputValidator>();
            _inputReaderMock = new Mock<IInputReader>();
            _inputReaderMock.Setup(r => r.ReadInput()).Returns(USER_INPUT);
            _gameBoardMock = new Mock<IGameBoard>();
            _gameBoardMock.Setup(b => b.GetCurrentBoardState()).Returns(new BoardFields());
        }

        [Test]
        public void PlayGameToEnd_ShouldMakeAllStepsRequiredDuringRound()
        {
            _inputValidatorMock.Setup(s => s.IsInputValid(It.IsAny<string>())).Returns(true);
            _gameBoardMock.Setup(b => b.AreAllShipSunk()).Returns(WIN_TURN);

            var sut = new BattleshipsGame(_boardVisualizer, 
                _inputReaderMock.Object, 
                _writer, 
                _inputValidatorMock.Object);

            sut.PlayGameToEnd(_gameBoardMock.Object, boardSize: 3);

            _inputReaderMock.Verify(i => i.ReadInput(), Times.Exactly(1));
            _gameBoardMock.Verify(b => b.ProcessUserShot(It.IsAny<string>()), Times.Exactly(1));
            _gameBoardMock.Verify(b => b.AreAllShipSunk(), Times.Once);
        }

        [Test]
        public void PlayGameToEnd_WhenAllShipsAreSunk_ShouldEndGame()
        {
            _inputValidatorMock.Setup(s => s.IsInputValid(It.IsAny<string>())).Returns(true);
            _gameBoardMock.SetupSequence(b => b.AreAllShipSunk())
                .Returns(TURN_WITHOUT_WIN)
                .Returns(WIN_TURN);

            var sut = new BattleshipsGame(_boardVisualizer,
                _inputReaderMock.Object,
                _writer,
                _inputValidatorMock.Object);

            sut.PlayGameToEnd(_gameBoardMock.Object, boardSize: 3);

            _gameBoardMock.Verify(b => b.AreAllShipSunk(), Times.Exactly(2));

        }

        [Test]
        public void PlayGameToEnd_ShouldWaitForProperInputToPlayARound()
        {
            _inputValidatorMock
                .SetupSequence(s => s.IsInputValid(It.IsAny<string>()))
                .Returns(false)
                .Returns(true);
            _gameBoardMock.Setup(b => b.AreAllShipSunk())
                .Returns(WIN_TURN);

            var sut = new BattleshipsGame(_boardVisualizer,
                _inputReaderMock.Object,
                _writer,
                _inputValidatorMock.Object);

            sut.PlayGameToEnd(_gameBoardMock.Object, boardSize: 3);

            _inputReaderMock.Verify(r => r.ReadInput(), Times.Exactly(2));
            _inputValidatorMock.Verify(v => v.IsInputValid(It.IsAny<string>()), Times.Exactly(2));
            _gameBoardMock.Verify(b => b.AreAllShipSunk(), Times.Exactly(1));
        }

        [TestCase("q")]
        [TestCase("Q")]
        public void PlayGameToEnd_WhenEndGameKeyIsPressed_ShouldEndGame(string endGameKey)
        {
            _inputValidatorMock.Setup(s => s.IsInputValid(It.IsAny<string>())).Returns(true);
            _inputReaderMock.Setup(r => r.ReadInput()).Returns(endGameKey);

            _gameBoardMock.Setup(b => b.AreAllShipSunk()).Returns(WIN_TURN);

            var sut = new BattleshipsGame(_boardVisualizer,
                _inputReaderMock.Object,
                _writer,
                _inputValidatorMock.Object);

            sut.PlayGameToEnd(_gameBoardMock.Object, boardSize: 3);

            _inputReaderMock.Verify(r => r.ReadInput(), Times.Exactly(1));
            _gameBoardMock.Verify(b => b.AreAllShipSunk(), Times.Never);
        }
    }
}

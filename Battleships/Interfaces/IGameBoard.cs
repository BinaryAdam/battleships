using Battleships.Models;

namespace Battleships.Interfaces
{
    public interface IGameBoard
    {
        BoardFields GetCurrentBoardState();
        void ProcessUserShot(string fieldName);
        bool AreAllShipSunk();

    }
}
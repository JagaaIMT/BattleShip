// Services/GameDataService.cs
using System;
using System.Collections.Generic;

namespace BattleShip.App.Services
{
    public class GameDataService
    {
        public event Action OnChange;
        private List<List<int>> player1Grid;
        private List<List<int>> player2Grid;
        private bool isPlayer1Turn;
        private string winner;

        public List<List<int>> Player1Grid
        {
            get => player1Grid;
            set
            {
                player1Grid = value;
                NotifyStateChanged();
            }
        }

        public List<List<int>> Player2Grid
        {
            get => player2Grid;
            set
            {
                player2Grid = value;
                NotifyStateChanged();
            }
        }

        public bool IsPlayer1Turn
        {
            get => isPlayer1Turn;
            set
            {
                isPlayer1Turn = value;
                NotifyStateChanged();
            }
        }

        public string Winner
        {
            get => winner;
            set
            {
                winner = value;
                NotifyStateChanged();
            }
        }

        public void SetGameData(List<List<int>> player1, List<List<int>> player2, bool isPlayer1Turn, string winner)
        {
            Player1Grid = player1;
            Player2Grid = player2;
            IsPlayer1Turn = isPlayer1Turn;
            Winner = winner;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}

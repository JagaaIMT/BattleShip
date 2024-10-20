using System;

namespace BattleShip.Models
{
    public class GameStart
    {
        public string? GameType { get; set; }
        public int GridX { get; set; }
        public int GridY { get; set; }
    }

    public class GameData
    {
        public List<List<int>> Player1 { get; set; }
        public List<List<int>> Player2 { get; set; }
        public int ClickedRow { get; set; }
        public int ClickedCol { get; set; }
        public bool IsPlayer1Turn { get; set; }
        public string? Winner { get; set; } // Ajout de la propriété Winner

    }

}
// BattleShip.API/Controllers/MenuController.cs
using Microsoft.AspNetCore.Mvc;
using BattleShip.Models;
using System.Collections.Generic;
using System;

namespace BattleShip.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class MenuController : ControllerBase
    {
        private static readonly int GridSize = 10;
        private static readonly int[] ShipSizes = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };

        [HttpPost("start")]
        public IActionResult StartGame([FromBody] GameStart gameStart)
        {
            var gameData = new GameData
            {
                Player1 = InitializeGrid(),
                Player2 = InitializeGrid(),
                IsPlayer1Turn = true
            };

            PlaceShipsRandomly(gameData.Player1);
            PlaceShipsRandomly(gameData.Player2);

            return Ok(gameData);
        }

        [HttpPost("gameData")]
        public IActionResult UpdateGameData([FromBody] GameData gameData)
        {
            if (gameData.IsPlayer1Turn)
            {
                gameData.Player2 = ProcessPlayerGridWithClick(gameData.Player2, gameData.ClickedRow, gameData.ClickedCol);
                if (!IsHit(gameData.Player2, gameData.ClickedRow, gameData.ClickedCol))
                {
                    gameData.IsPlayer1Turn = false;
                }
            }

            while (!gameData.IsPlayer1Turn)
            {
                gameData.Player1 = ProcessPlayerGridWithRandomClick(gameData.Player1, out int row, out int col);
                if (!IsHit(gameData.Player1, row, col))
                {
                    gameData.IsPlayer1Turn = true;
                }
            }

            if (IsAllShipsSunk(gameData.Player1))
            {
                gameData.Winner = "Defaite";
            }
            else if (IsAllShipsSunk(gameData.Player2))
            {
                gameData.Winner = "Victoire";
            }

            return Ok(gameData);
        }

        private List<List<int>> InitializeGrid()
        {
            var grid = new List<List<int>>();
            for (int i = 0; i < GridSize; i++)
            {
                var row = new List<int>(new int[GridSize]);
                grid.Add(row);
            }
            return grid;
        }

        private void PlaceShipsRandomly(List<List<int>> grid)
        {
            var random = new Random();
            foreach (var size in ShipSizes)
            {
                bool placed = false;
                while (!placed)
                {
                    int row = random.Next(GridSize);
                    int col = random.Next(GridSize);
                    bool horizontal = random.Next(2) == 0;

                    if (CanPlaceShip(grid, row, col, size, horizontal))
                    {
                        PlaceShip(grid, row, col, size, horizontal);
                        placed = true;
                    }
                }
            }
        }

        private bool CanPlaceShip(List<List<int>> grid, int row, int col, int size, bool horizontal)
        {
            if (horizontal)
            {
                if (col + size > GridSize) return false;
                for (int i = 0; i < size; i++)
                {
                    if (grid[row][col + i] != 0) return false;
                }
            }
            else
            {
                if (row + size > GridSize) return false;
                for (int i = 0; i < size; i++)
                {
                    if (grid[row + i][col] != 0) return false;
                }
            }
            return true;
        }

        private void PlaceShip(List<List<int>> grid, int row, int col, int size, bool horizontal)
        {
            for (int i = 0; i < size; i++)
            {
                if (horizontal)
                {
                    grid[row][col + i] = size;
                }
                else
                {
                    grid[row + i][col] = size;
                }
            }
        }

        private List<List<int>> ProcessPlayerGridWithClick(List<List<int>> playerGrid, int row, int col)
        {
            if (playerGrid[row][col] > 0)
            {
                playerGrid[row][col] = 5;
            }
            else
            {
                playerGrid[row][col] = -1;
            }

            return playerGrid;
        }

        private List<List<int>> ProcessPlayerGridWithRandomClick(List<List<int>> playerGrid, out int row, out int col)
        {
            var random = new Random();

            do
            {
                row = random.Next(GridSize);
                col = random.Next(GridSize);
            } while (playerGrid[row][col] == 5 || playerGrid[row][col] == -1);

            if (playerGrid[row][col] > 0)
            {
                playerGrid[row][col] = 5;
            }
            else
            {
                playerGrid[row][col] = -1;
            }

            return playerGrid;
        }

        private bool IsHit(List<List<int>> playerGrid, int row, int col)
        {
            return playerGrid[row][col] == 5;
        }

        private bool IsAllShipsSunk(List<List<int>> playerGrid)
        {
            foreach (var row in playerGrid)
            {
                foreach (var cell in row)
                {
                    if (cell > 0 && cell != 5)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Service
{
    class GameService
    {
        private Grid grid;
        private MineService mineService;
        private GridService gridService;
        private bool gameStarted = false;

        public Grid Grid
        {
            get { return grid; }
        }

        public void startNewGame(int width, int height, List<Coordinates> mineList, List<Coordinates> safeZone,
            List<Coordinates> targetZone, int additionalMine)
        {
            this.mineService = new MineService();
            this.gridService = new GridService();
            List<Coordinates> finalMineList = mineService.getMineList(width, height, mineList, safeZone, targetZone, additionalMine); ;
            this.grid = gridService.generateGrid(width, height, finalMineList, targetZone);
            gameStarted = true;
        }

        // return value (number of adjacent mine) of the cell
        // -1 => cell is a mine
        // -2 => cell is the target
        // -3 => cell is an exploded mine 
        public int checkCell(float x, float y)
        {
            int cellValue = this.grid.CellsArray[(int)x][(int)y].CellValue;
            if (cellValue == -1)
            {
                this.grid.CellsArray[(int)x][(int)y].explode();
            }
            this.grid.CellsArray[(int)x][(int)y].CellState = CellState.Revealed;   
            return cellValue;
        }

        public int checkMine(float x, float y)
        {
            int cellValue = this.grid.CellsArray[(int)x][(int)y].CellValue;
            if (cellValue == -1)
            {
                this.grid.CellsArray[(int)x][(int)y].explode();
            }
            return cellValue;
        }
        public CellState getCellState(float x, float y)
        {
            return this.grid.CellsArray[(int)x][(int)y].CellState;
        }


    }
}

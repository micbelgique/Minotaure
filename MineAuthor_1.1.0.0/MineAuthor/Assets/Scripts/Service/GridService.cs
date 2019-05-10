using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Service
{
    class GridService
    {
        public Grid generateGrid(int width, int height, List<Coordinates> mineList, List<Coordinates> targetZone)
        {
            // init grid
            Grid grid = new Grid(width, height);

            grid.CellsArray = new Cell[width][];

            for (int i = 0; i < width; i++)
            {
                grid.CellsArray[i] = new Cell[height];
            }

            // init cells
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid.CellsArray[i][j] = new Cell();
                }
            }

            // init goal
            targetZone.ForEach(coordinates => {
                grid.CellsArray[coordinates.X][coordinates.Y].CellValue = -2;
            });

            // init mine
            mineList.ForEach(coordinates => {
                grid.CellsArray[coordinates.X][coordinates.Y].CellValue = -1;
            });


            // init values 
            mineList.ForEach(coordinates => {
                List<Coordinates> adjacentCell = getAdjacentCell(width, height, coordinates);
                adjacentCell.ForEach(cell => {
                    grid.CellsArray[cell.X][cell.Y].CellValue++;
                });
            });

            return grid;
        }

        public List<Coordinates> getAdjacentCell(int gridWidth, int gridHeight, Coordinates centerCell)
        {
            //Console.WriteLine("center: " + centerCell.X + "," + centerCell.Y);


            bool noUp = false, noDown = false, noLeft = false, noRight = false;
            int up = centerCell.Y + 1;
            int down = centerCell.Y - 1;
            int left = centerCell.X - 1;
            int right = centerCell.X + 1;

            if (up >= gridHeight) noUp = true;
            if (down < 0) noDown = true;
            if (left < 0) noLeft = true;
            if (right >= gridWidth) noRight = true;

            List<Coordinates> coordinateList = new List<Coordinates>();
            if (!(noUp || noRight)) coordinateList.Add(new Coordinates(right, up));
            if (!(noRight)) coordinateList.Add(new Coordinates(right, centerCell.Y));
            if (!(noRight || noDown)) coordinateList.Add(new Coordinates(right, down));
            if (!(noUp)) coordinateList.Add(new Coordinates(centerCell.X, up));
            if (!(noDown)) coordinateList.Add(new Coordinates(centerCell.X, down));
            if (!(noLeft || noUp)) coordinateList.Add(new Coordinates(left, up));
            if (!(noLeft)) coordinateList.Add(new Coordinates(left, centerCell.Y));
            if (!(noLeft || noDown)) coordinateList.Add(new Coordinates(left, down));


            for (int i = 0; i < coordinateList.Count; i++)
            {
                //Console.WriteLine("adjacent: " + coordinateList[i].X + "," + coordinateList[i].Y);
            }



            return coordinateList;
        }


    }
}

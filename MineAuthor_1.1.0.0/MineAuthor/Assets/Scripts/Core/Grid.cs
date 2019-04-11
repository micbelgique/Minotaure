using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Core
{
    class Grid
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Cell[][] CellsArray { get; set; }

        public Grid(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }


        override
        public String ToString()
        {
            string current = "";
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    Cell currentCell = CellsArray[x][y];
                    if (currentCell.CellValue == -1)
                    {
                        current += "[x]";
                    } else if (currentCell.CellValue == -2)
                    {
                        current += "[0]";
                    } else if (currentCell.CellValue == -3)
                    {
                        current += "[W]";
                    } else if (currentCell.CellValue == 0)
                    {
                        current += "[_]";
                    } else
                    {
                        current += "[" + currentCell.CellValue + "]";
                    }
                }
                current += "\n";
            }

            return current;
        }
    }
}

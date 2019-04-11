using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Core
{
    class Cell
    {
        public CellState CellState { get; set; }
        private int cellValue;

        public int CellValue
        {
            get { return cellValue; }
            set {
                if (cellValue != -1 && cellValue != -2)
                {
                    cellValue = value;
                }
            }
        }

        public Cell()
        {
            this.CellState = CellState.Hidden;
            this.cellValue = 0;
        }

        public void explode()
        {
            this.cellValue = -3;
        }


    }
}

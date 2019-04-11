using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Service
{
    class MineService
    {
        public List<Coordinates> getMineList(int width, int height, List<Coordinates> mineList, List<Coordinates> safeZone, List<Coordinates> targetZone, int additionalRandomMine)
        {
            Random random = new Random();

            List<Coordinates> usedCoordinates = new List<Coordinates>();
            usedCoordinates.AddRange(mineList);
            usedCoordinates.AddRange(safeZone);
            usedCoordinates.AddRange(targetZone);

            List<Coordinates> randomMine = new List<Coordinates>();

            // additional Random Mine 
            for (int i = 0; i < additionalRandomMine; i++)
            {
                bool isMine = false;
                int x = 0, y = 0;

                do
                {
                    isMine = false;
                    x = random.Next(0, width);
                    y = random.Next(0, height);

                    usedCoordinates.ForEach(coordinates => {
                        if (x == coordinates.X && y == coordinates.Y)
                        {
                            isMine = true;
                        }
                    });

                } while (isMine);

                randomMine.Add(new Coordinates(x, y));
            }

            mineList.AddRange(randomMine);


            return mineList;
        }
    }
}

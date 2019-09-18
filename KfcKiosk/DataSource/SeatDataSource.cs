using Kfc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KfcKiosk.DataSource
{
    public class SeatDataSource
    {
        bool isLoaded = false;
        public List<Seat> lstSeat;

        public void Load()
        {
            if (isLoaded) return;

            lstSeat = new List<Seat>
            {
                new Seat { Id = 1, floor = 1, lstFood = new List<Food>() },
                new Seat { Id = 2, floor = 1, lstFood = new List<Food>() },
                new Seat { Id = 3, floor = 1, lstFood = new List<Food>() },
                new Seat { Id = 4, floor = 1, lstFood = new List<Food>() },
                new Seat { Id = 5, floor = 1, lstFood = new List<Food>() },
                new Seat { Id = 6, floor = 2, lstFood = new List<Food>() },
                new Seat { Id = 7, floor = 2, lstFood = new List<Food>() },
                new Seat { Id = 8, floor = 2, lstFood = new List<Food>() },
                new Seat { Id = 9, floor = 2, lstFood = new List<Food>() },
                new Seat { Id = 10, floor = 2, lstFood = new List<Food>() },
            };

            isLoaded = true;
        }
    }
}

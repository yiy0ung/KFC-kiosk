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
                new Seat { Id = "1번 테이블", FloorIdx = 1, OrderInfo = "", lstFood = new List<Food>(), OrderTime = "" },
                new Seat { Id = "2번 테이블", FloorIdx = 1, OrderInfo = "", lstFood = new List<Food>(), OrderTime = "" },
                new Seat { Id = "3번 테이블", FloorIdx = 1, OrderInfo = "", lstFood = new List<Food>(), OrderTime = "" },
                new Seat { Id = "4번 테이블", FloorIdx = 2, OrderInfo = "", lstFood = new List<Food>(), OrderTime = "" },
                new Seat { Id = "5번 테이블", FloorIdx = 2, OrderInfo = "", lstFood = new List<Food>(), OrderTime = "" },
                new Seat { Id = "6번 테이블", FloorIdx = 2, OrderInfo = "", lstFood = new List<Food>(), OrderTime = "" },
                new Seat { Id = "7번 테이블", FloorIdx = 2, OrderInfo = "", lstFood = new List<Food>(), OrderTime = "" },
                new Seat { Id = "8번 테이블", FloorIdx = 3, OrderInfo = "", lstFood = new List<Food>(), OrderTime = "" },
                new Seat { Id = "9번 테이블", FloorIdx = 3, OrderInfo = "", lstFood = new List<Food>(), OrderTime = "" },
                new Seat { Id = "10번 테이블", FloorIdx = 3, OrderInfo = "", lstFood = new List<Food>(), OrderTime = "" },
            };

            isLoaded = true;
        }

        public List<Seat> FilterSeat(int FloorIdx)
        {
            List<Seat> filterSeat = new List<Seat>();

            foreach (Seat seat in lstSeat)
            {
                if (seat.FloorIdx == FloorIdx)
                {
                    filterSeat.Add(seat);
                }
            }

            return filterSeat;
        }
        public void ClearSeat(string SeatId)
        {
            foreach (Seat seat in lstSeat)
            {
                if (SeatId.Equals(seat.Id))
                {
                    seat.OrderInfo = "";
                    seat.lstFood.Clear();

                    break;
                }
            }
        }
    }
}

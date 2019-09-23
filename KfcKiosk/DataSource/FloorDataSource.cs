using Kfc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KfcKiosk.DataSource
{
    public class FloorDataSource
    {
        bool isLoaded = false;
        public List<Floor> lstFloor;

        public void Load()
        {
            if (isLoaded) return;

            lstFloor = new List<Floor>
            {
                new Floor { Idx = 1, FloorNum = 1, Info = "카페테리아" },
                new Floor { Idx = 2, FloorNum = 2, Info = "비지니스 룸" },
                new Floor { Idx = 3, FloorNum = 3, Info = "테라스" },
            };

            isLoaded = true;
        }
    }
}

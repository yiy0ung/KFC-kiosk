using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kfc.Core
{
    public class Food
    {
        public string Name { get; set; }
        public int Price { get; set; } // 가격
        public int Count { get; set; } // 수량
        public int Kcal { get; set; } // 열량
        public string ImgPath { get; set; }
        public int TotalPrice
        {
            get
            {
                int retval = 0;

                retval = Price * Count;

                return retval;
            }
        }
        public ECategory Category { get; set; }
    }
}

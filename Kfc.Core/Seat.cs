using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kfc.Core
{
    public class Seat
    {
        public string Id { get; set; }
        public int FloorIdx { get; set; }
        public string OrderInfo { get; set; }
        public List<Food> lstFood { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kfc.Core
{
    public class Seat
    {
        public int Id { get; set; }
        public int floor { get; set; }
        public List<Food> lstFood { get; set; }
    }
}

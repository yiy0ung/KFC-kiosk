using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KfcKiosk.model
{
    public class MenuData
    {
        public List<Menu> lstMenu = null;

        public void Load()
        {
            lstMenu = new List<Menu>()
            {
                // 데이터 초기화
            };
        }
    }
}

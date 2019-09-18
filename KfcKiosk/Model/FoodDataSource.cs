using Kfc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KfcKiosk.Model
{
    public class FoodDataSource
    {
        bool isLoaded = false;

        public List<Food> lstMenu;

        public void Load()
        {
            if (isLoaded) return;

            lstMenu = new List<Food>()
            {
                new Food() { Name = "" }
            };

            isLoaded = true;
        }

    }
}

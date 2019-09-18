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
                new Food() {
                    Name = "블랙라벨에그 타워버거",
                    Count = 0,
                    Price = 12000,
                    ImgPath = "Assets/Burger/블랙라벨에그타워버거.png",
                    Kcal = 606,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "블랙라벨에그 타워버거",
                    Count = 0,
                    Price = 12000,
                    ImgPath = "Assets/Burger",
                    Kcal = 606,
                    Category = ECategory.Burger,
                },
            };

            isLoaded = true;
        }

    }
}

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
                    Name = "블랙라벨에그타워버거",
                    Count = 0,
                    Price = 7700,
                    ImgPath = "Assets/Burger/블랙라벨에그타워버거.png",
                    Kcal = 811,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "블랙라벨클래식버거",
                    Count = 0,
                    Price = 6700,
                    ImgPath = "Assets/Burger/블랙라벨클래식버거",
                    Kcal = 606,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "오리지널타워버거",
                    Count = 0,
                    Price = 6000,
                    ImgPath = "Assets/Burger/오리지널타워버거",
                    Kcal = 579,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "징거더블다운맥스",
                    Count = 0,
                    Price = 6900,
                    ImgPath = "Assets/Burger/징거더블다운맥스",
                    Kcal = 792,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "징거버거",
                    Count = 0,
                    Price = 5100,
                    ImgPath = "Assets/Burger/징거버거",
                    Kcal = 389,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "치킨불고기버거",
                    Count = 0,
                    Price = 4100,
                    ImgPath = "Assets/Burger/치킨불고기버거",
                    Kcal = 332,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "타워버거",
                    Count = 0,
                    Price = 6000,
                    ImgPath = "Assets/Burger/타워버거",
                    Kcal = 584,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "트리플리치오리지널버거",
                    Count = 0,
                    Price = 6400,
                    ImgPath = "Assets/Burger/트리플리치오리지널버거",
                    Kcal = 580,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "트위스터",
                    Count = 0,
                    Price = 4000,
                    ImgPath = "Assets/Burger/트위스터",
                    Kcal = 379,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "핫치즈징거버거",
                    Count = 0,
                    Price = 5700,
                    ImgPath = "Assets/Burger/핫치즈징거버거",
                    Kcal = 501,
                    Category = ECategory.Burger,
                }
            };

            isLoaded = true;
        }

    }
}

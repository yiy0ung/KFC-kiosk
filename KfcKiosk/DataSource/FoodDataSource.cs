using Kfc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KfcKiosk.DataSource
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
                    ImgPath = "Assets/Burger/블랙라벨클래식버거.png",
                    Kcal = 606,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "오리지널타워버거",
                    Count = 0,
                    Price = 6000,
                    ImgPath = "Assets/Burger/오리지널타워버거.png",
                    Kcal = 579,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "징거더블다운맥스",
                    Count = 0,
                    Price = 6900,
                    ImgPath = "Assets/Burger/징거더블다운맥스.png",
                    Kcal = 792,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "징거버거",
                    Count = 0,
                    Price = 5100,
                    ImgPath = "Assets/Burger/징거버거.png",
                    Kcal = 389,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "치킨불고기버거",
                    Count = 0,
                    Price = 4100,
                    ImgPath = "Assets/Burger/치킨불고기버거.png",
                    Kcal = 332,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "타워버거",
                    Count = 0,
                    Price = 6000,
                    ImgPath = "Assets/Burger/타워버거.png",
                    Kcal = 584,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "트리플리치오리지널버거",
                    Count = 0,
                    Price = 6400,
                    ImgPath = "Assets/Burger/트리플리치오리지널버거.png",
                    Kcal = 580,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "트위스터",
                    Count = 0,
                    Price = 4000,
                    ImgPath = "Assets/Burger/트위스터.png",
                    Kcal = 379,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "핫치즈징거버거",
                    Count = 0,
                    Price = 5700,
                    ImgPath = "Assets/Burger/핫치즈징거버거.png",
                    Kcal = 501,
                    Category = ECategory.Burger,
                },
                new Food() {
                    Name = "블랙라벨치킨8조각",
                    Count = 0,
                    Price = 21200,
                    ImgPath = "Assets/Chickin/blackLabelChickin-8p.png",
                    Kcal = 2112,
                    Category = ECategory.Chickin,
                },
                new Food() {
                    Name = "갓양념치킨8조각",
                    Count = 0,
                    Price = 20700,
                    ImgPath = "Assets/Chickin/godSauceChickin-8p.png",
                    Kcal = 3000,
                    Category = ECategory.Chickin,
                },
                new Food() {
                    Name = "핫칠리씨치킨8조각",
                    Count = 0,
                    Price = 20700,
                    ImgPath = "Assets/Chickin/hotChiliChickin-8p.png",
                    Kcal = 2264,
                    Category = ECategory.Chickin,
                },
                new Food() {
                    Name = "핫크리스피버켓9조각",
                    Count = 0,
                    Price = 20600,
                    ImgPath = "Assets/Chickin/hotCrispyChickin-9p.png",
                    Kcal = 2264,
                    Category = ECategory.Chickin,
                },
                new Food() {
                    Name = "오리지널버켓9조각",
                    Count = 0,
                    Price = 20600,
                    ImgPath = "Assets/Chickin/originalChickin-9p.png",
                    Kcal = 2439,
                    Category = ECategory.Chickin,
                },
                new Food() {
                    Name = "맥주",
                    Count = 0,
                    Price = 4000,
                    ImgPath = "Assets/Drink/beer.png",
                    Kcal = 157,
                    Category = ECategory.Drink,
                },
                new Food() {
                    Name = "코카콜라",
                    Count = 0,
                    Price = 2300,
                    ImgPath = "Assets/Drink/coke.png",
                    Kcal = 240,
                    Category = ECategory.Drink,
                },
                new Food() {
                    Name = "핫초코",
                    Count = 0,
                    Price = 1500,
                    ImgPath = "Assets/Drink/hotChoco.png",
                    Kcal = 105,
                    Category = ECategory.Drink,
                },
                new Food() {
                    Name = "아이스밀크티",
                    Count = 0,
                    Price = 3000,
                    ImgPath = "Assets/Drink/milkTea.png",
                    Kcal = 62,
                    Category = ECategory.Drink,
                },
                new Food() {
                    Name = "오렌지쥬스",
                    Count = 0,
                    Price = 2000,
                    ImgPath = "Assets/Drink/orangeJuice.png",
                    Kcal = 276,
                    Category = ECategory.Drink,
                },
                new Food() {
                    Name = "치즈스틱",
                    Count = 0,
                    Price = 2000,
                    ImgPath = "Assets/Snack/CheeseStick.png",
                    Kcal = 276,
                    Category = ECategory.Snack,
                },
                new Food() {
                    Name = "치킨너겟 6조각",
                    Count = 0,
                    Price = 2000,
                    ImgPath = "Assets/Snack/chickinNugget-6p.png",
                    Kcal = 276,
                    Category = ECategory.Snack,
                },
                new Food() {
                    Name = "프렌치프라이",
                    Count = 0,
                    Price = 2000,
                    ImgPath = "Assets/Snack/FrenchFry.png",
                    Kcal = 276,
                    Category = ECategory.Snack,
                },
                new Food() {
                    Name = "핫윙 4조각",
                    Count = 0,
                    Price = 2000,
                    ImgPath = "Assets/Snack/hotWing-4p.png",
                    Kcal = 276,
                    Category = ECategory.Snack,
                },
                new Food() {
                    Name = "아이스크림",
                    Count = 0,
                    Price = 2000,
                    ImgPath = "Assets/Snack/icecream.png",
                    Kcal = 276,
                    Category = ECategory.Snack,
                },
            };

            isLoaded = true;
        }

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainFigures;
using Figures;

namespace RectangleTaskTest
{
    [TestClass]
    public class MainRectangleTest
    {
        [TestMethod]
        public void ExtremeRectangleTest()
        {
            var topLeft = new Point(3, 2);
            const double width = 1;
            const double height = 1;
            var mainRectangle = new MainRectangle(topLeft, width, height);
            var secondRectangle = new ColorRectangle(ConsoleColor.Red, new Point(1, 1), 1, 1);
            mainRectangle.AddRectangle(secondRectangle);
           
            Assert.AreEqual(mainRectangle.GetExtremeRectangle(), new Rectangle(new Point(1, 1), 1, 1));
            mainRectangle.SetExtremeRectangle();
            Assert.AreEqual(mainRectangle, new Rectangle(new Point(1, 1), 1, 1));

            var secondRectangle2 = new ColorRectangle(ConsoleColor.Blue, new Point(2, 2), 1, 1);
            mainRectangle.AddRectangle(secondRectangle2);
            Assert.AreEqual(mainRectangle.GetExtremeRectangle(), new Rectangle(new Point(1, 2), 2, 2));
            mainRectangle.SetExtremeRectangle();
            Assert.AreEqual(mainRectangle, new Rectangle(new Point(1, 2), 2, 2));

            var secondRectangle3 = new ColorRectangle(ConsoleColor.Green, new Point(5, 3), 3, 2);
            mainRectangle.AddRectangle(secondRectangle3);
            Assert.AreEqual(mainRectangle.GetExtremeRectangle(), new Rectangle(new Point(1, 3), 7, 3));
            mainRectangle.SetExtremeRectangle();
            Assert.AreEqual(mainRectangle, new Rectangle(new Point(1, 3), 7, 3));
        }

        [TestMethod]
        public void ExtremeRectangleExceptOuterTest()
        {
            var topLeft = new Point(6, 1);
            const double width = 5;
            const double height = 5;
            var mainRectangle = new MainRectangle(topLeft, width, height);
            var secondRectangle = new ColorRectangle(ConsoleColor.Red, new Point(4, 2), 3, 4);
            mainRectangle.AddRectangle(secondRectangle);
            var secondRectangle2 = new ColorRectangle(ConsoleColor.Red, new Point(0, 5), 3, 3);
            mainRectangle.AddRectangle(secondRectangle2);

            var outerRect = mainRectangle.GetExtremeRectangle(useExceptOuter:true);
            Assert.AreEqual(outerRect, new Rectangle(new Point(6, 1), 1, 3));
            mainRectangle.SetExtremeRectangle(useExceptOuter: true);
            Assert.AreEqual(mainRectangle, new Rectangle(new Point(6, 1), 1, 3));
        }

        [TestMethod]
        public void ExtremeRectangleFiltredTest()
        {
            var topLeft = new Point(3, 2);
            const double width = 1;
            const double height = 1;
            var mainRectangle = new MainRectangle(topLeft, width, height);
            var secondRectangle = new ColorRectangle(ConsoleColor.Red, new Point(1, 1), 1, 1);
            mainRectangle.AddRectangle(secondRectangle);
            var secondFakeRectangle = new ColorRectangle(ConsoleColor.Blue, new Point(3, 3), 3, 3);
            mainRectangle.AddRectangle(secondRectangle);
            
            Predicate<ColorRectangle> redFilter = x => x.Color == ConsoleColor.Red;

            Assert.AreEqual(
                mainRectangle.GetExtremeRectangle(filter: redFilter)
                , new Rectangle(new Point(1, 1), 1, 1));
            mainRectangle.SetExtremeRectangle(filter: redFilter);

            Assert.AreEqual(mainRectangle, new Rectangle(new Point(1, 1), 1, 1));

            var secondRectangle2 = new ColorRectangle(ConsoleColor.Red, new Point(2, 2), 1, 1);
            mainRectangle.AddRectangle(secondRectangle2);

            var secondFakeRectangle2 = new ColorRectangle(ConsoleColor.Black, new Point(2, 2), 1, 1);
            mainRectangle.AddRectangle(secondFakeRectangle2);
            Assert.AreEqual(mainRectangle.GetExtremeRectangle(filter: redFilter), new Rectangle(new Point(1, 2), 2, 2));
            mainRectangle.SetExtremeRectangle(filter: redFilter);
            Assert.AreEqual(mainRectangle, new Rectangle(new Point(1, 2), 2, 2));
        }

        [TestMethod]
        public void ExtremeRectangleExceptOuterFiltredTest()
        {
            var topLeft = new Point(3, 4);
            const double width = 6;
            const double height = 4;
            var mainRectangle = new MainRectangle(topLeft, width, height);
            var secondRedRectangle = new ColorRectangle(ConsoleColor.Red, new Point(1, 0), 1, 1);
            var secondBlueRectangle = new ColorRectangle(ConsoleColor.Blue, new Point(3, 2), 1, 1);
            var secondRedRectangle2 = new ColorRectangle(ConsoleColor.Red, new Point(5, 3), 3, 2);
            var secondBlueRectangle2 = new ColorRectangle(ConsoleColor.Blue, new Point(10, 2), 1, 1);

            mainRectangle.AddRectangles(
               new[]{
                        secondRedRectangle,
                        secondBlueRectangle,
                        secondRedRectangle2,
                        secondBlueRectangle2
                    }
                );
            Predicate<ColorRectangle> redBlueFilter = x => x.Color == ConsoleColor.Red || x.Color == ConsoleColor.Blue;

            Assert.AreEqual(
                mainRectangle.GetExtremeRectangle(useExceptOuter:true, filter: redBlueFilter)
                , new Rectangle(new Point(3, 3), 5, 2));
           
        }

        [TestMethod]
        public void ExtremeRectangleNegativeTest()
        {
            var topLeft = new Point(15, 15);
            const double width = 6;
            const double height = 4;
            var mainRectangle = new MainRectangle(topLeft, width, height);
            var secondRedRectangle = new ColorRectangle(ConsoleColor.Red, new Point(1, 0), 1, 1);
            var secondBlueRectangle = new ColorRectangle(ConsoleColor.Blue, new Point(3, 2), 1, 1);
            var secondRedRectangle2 = new ColorRectangle(ConsoleColor.Red, new Point(5, 3), 3, 2);
            var secondBlueRectangle2 = new ColorRectangle(ConsoleColor.Blue, new Point(10, 2), 1, 1);

            mainRectangle.AddRectangles(
               new[]{
                        secondRedRectangle,
                        secondBlueRectangle,
                        secondRedRectangle2,
                        secondBlueRectangle2
                    }
                );
            Predicate<ColorRectangle> redBlueFilter = x => x.Color == ConsoleColor.Red || x.Color == ConsoleColor.Blue;

            Assert.ThrowsException<ArgumentException>(() => mainRectangle.GetExtremeRectangle(useExceptOuter: true, filter: redBlueFilter));
        }
    }
}

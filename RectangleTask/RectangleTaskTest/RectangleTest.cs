using Figures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RectangleTaskTest
{
    [TestClass]
    public class RectangleTest
    {
        [TestMethod]
        public void TestCorrectRectangle()
        {
            var topLeft = new Point(2, 3);
            const double width = 4;
            const double height = 5;
            var rectangle = new Rectangle(topLeft, width, height);
            Assert.AreEqual(rectangle.TopLeft, topLeft);
            Assert.AreEqual(rectangle.TopRight, new Point(topLeft.X+ width, topLeft.Y));
            Assert.AreEqual(rectangle.BottomRight, new Point(topLeft.X + width, topLeft.Y-height));
            Assert.AreEqual(rectangle.BottomLeft, new Point(topLeft.X, topLeft.Y - height));
            Assert.AreEqual(rectangle.Width, width);
            Assert.AreEqual(rectangle.Height, height);
        }
        
        [TestMethod]
        public void TestNegativeRectangle()
        {
            var topLeft = new Point(2, 3);
            const double width = 4;
            const double widthNegative = -4;
            const double height = 5;
            const double heightNegative = -5;
            var rectangle = new Rectangle(topLeft, width, height);
            Assert.ThrowsException<ArgumentException>(() => new Rectangle(topLeft, widthNegative, height));
            Assert.ThrowsException<ArgumentException>(() => new Rectangle(topLeft, width, heightNegative));
        }

        [TestMethod]
        public void TestCorrectColorRectangle()
        {
            var point = new Point(2, 3);
            const double width = 4;
            const double height = 5;
            var rectangle = new ColorRectangle(ConsoleColor.Red, point, width, height);
          
            Assert.AreEqual(rectangle.Color, ConsoleColor.Red);
        }

    }
}

using Microsoft.Extensions.Logging;
using Stratsys.SkiBackend.Controllers;

namespace Stratsys.SkiBackend.Test
{
    public class SkiLengthTest
    {
        [Fact]
        public void TestSmallChild()
        {
            var length = 100;
            var controller = new SkiLengthController();
            var result = controller.Get(3, length, false);
            Assert.Equal(length, result.MinLength);
            Assert.Equal(length, result.MaxLength);
        }
        [Fact]
        public void TestChild()
        {
            var length = 120;
            var controller = new SkiLengthController();
            var result = controller.Get(6, length, false);
            Assert.Equal(length + 10, result.MinLength);
            Assert.Equal(length + 20, result.MaxLength);
        }
        [Fact]
        public void TestFreestyle()
        {
            var length = 160;
            var controller = new SkiLengthController();
            var result = controller.Get(30, length, true);
            Assert.Equal(length + 10, result.MinLength);
            Assert.Equal(length + 15, result.MaxLength);
        }
        [Fact]
        public void TestClassic()
        {
            var length = 160;
            var controller = new SkiLengthController();
            var result = controller.Get(30, length, false);
            Assert.Equal(length + 20, result.MinLength);
            Assert.Equal(length + 20, result.MaxLength);
        }
        [Fact]
        public void TestLongClassic()
        {
            var length = SkiBackend.Constants.MaxManufacturedClassicSkiLength - 10;
            var controller = new SkiLengthController();
            var result = controller.Get(30, length, false);
            Assert.Equal(SkiBackend.Constants.MaxManufacturedClassicSkiLength, result.MinLength);
            Assert.Equal(SkiBackend.Constants.MaxManufacturedClassicSkiLength, result.MaxLength);
        }
        [Fact]
        public void TestLongFreestyle()
        {
            var length = SkiBackend.Constants.MaxManufacturedFreestyleSkiLength - 12;
            var controller = new SkiLengthController();
            var result = controller.Get(30, length, true);
            Assert.Equal(length + 10, result.MinLength);
            Assert.Equal(SkiBackend.Constants.MaxManufacturedFreestyleSkiLength, result.MaxLength);
        }
        [Fact]
        public void TestInvalidAge()
        {
            Assert.Throws<Exception>(() =>
            {
                var controller = new SkiLengthController();
                var result = controller.Get(-30, 180, false);
            });
        }
        [Fact]
        public void TestInvalidLength()
        {
            Assert.Throws<Exception>(() =>
            {
                var controller = new SkiLengthController();
                var result = controller.Get(30, -180, false);
            });
        }
    }
}
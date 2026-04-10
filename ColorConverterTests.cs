using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColorTools;
using ColorTools.Models;
using System;

namespace SbKots.ColorTools.Tests
{
    [TestClass]
    public class ColorConverterTests
    {
        [TestMethod]
        public void HexToRgb_ValidHex_ReturnsRgbColor()
        {
            var color = ColorConverter.HexToRgb("#FF0000");

            Assert.AreEqual(255, color.R);
            Assert.AreEqual(0, color.G);
            Assert.AreEqual(0, color.B);
        }

        [TestMethod]
        public void HexToRgb_ShortHex_ReturnsRgbColor()
        {
            var color = ColorConverter.HexToRgb("#FFF");

            Assert.AreEqual(255, color.R);
            Assert.AreEqual(255, color.G);
            Assert.AreEqual(255, color.B);
        }

        [TestMethod]
        public void HexToRgb_InvalidHex_ThrowsFormatException()
        {
            Assert.ThrowsException<FormatException>(() =>
            {
                ColorConverter.HexToRgb("ZZZZZZ");
            });
        }

        [TestMethod]
        public void RgbToHex_ValidRgb_ReturnsHex()
        {
            string hex = ColorConverter.RgbToHex(255, 136, 0);

            Assert.AreEqual("#FF8800", hex);
        }

        [TestMethod]
        public void RgbToHex_BlackRgb_ReturnsBlackHex()
        {
            string hex = ColorConverter.RgbToHex(0, 0, 0);

            Assert.AreEqual("#000000", hex);
        }

        [TestMethod]
        public void RgbToHex_InvalidRgb_ThrowsArgumentOutOfRangeException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                ColorConverter.RgbToHex(-1, 0, 0);
            });
        }

        [TestMethod]
        public void RgbToHsl_RedRgb_ReturnsExpectedHsl()
        {
            var hsl = ColorConverter.RgbToHsl(255, 0, 0);

            Assert.AreEqual(0, Math.Round(hsl.H, 0));
            Assert.AreEqual(100, Math.Round(hsl.S, 0));
            Assert.AreEqual(50, Math.Round(hsl.L, 0));
        }

        [TestMethod]
        public void RgbToHsl_BlackRgb_ReturnsExpectedHsl()
        {
            var hsl = ColorConverter.RgbToHsl(0, 0, 0);

            Assert.AreEqual(0, Math.Round(hsl.H, 0));
            Assert.AreEqual(0, Math.Round(hsl.S, 0));
            Assert.AreEqual(0, Math.Round(hsl.L, 0));
        }

        [TestMethod]
        public void RgbToHsl_InvalidRgb_ThrowsArgumentOutOfRangeException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                ColorConverter.RgbToHsl(300, 0, 0);
            });
        }

        [TestMethod]
        public void HslToRgb_ValidHsl_ReturnsRedRgb()
        {
            var rgb = ColorConverter.HslToRgb(0, 100, 50);

            Assert.AreEqual(255, rgb.R);
            Assert.AreEqual(0, rgb.G);
            Assert.AreEqual(0, rgb.B);
        }

        [TestMethod]
        public void HslToRgb_BlackHsl_ReturnsBlackRgb()
        {
            var rgb = ColorConverter.HslToRgb(0, 0, 0);

            Assert.AreEqual(0, rgb.R);
            Assert.AreEqual(0, rgb.G);
            Assert.AreEqual(0, rgb.B);
        }

        [TestMethod]
        public void HslToRgb_InvalidHsl_ThrowsArgumentOutOfRangeException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                ColorConverter.HslToRgb(500, 50, 50);
            });
        }

        [TestMethod]
        public void Lighten_ValidColor_ReturnsLighterColor()
        {
            var original = new RgbColor(100, 100, 100);

            var result = ColorConverter.Lighten(original, 20);

            Assert.IsTrue(result.R >= original.R);
            Assert.IsTrue(result.G >= original.G);
            Assert.IsTrue(result.B >= original.B);
        }

        [TestMethod]
        public void Lighten_ZeroPercent_ReturnsSameColor()
        {
            var original = new RgbColor(100, 100, 100);

            var result = ColorConverter.Lighten(original, 0);

            Assert.AreEqual(original.R, result.R);
            Assert.AreEqual(original.G, result.G);
            Assert.AreEqual(original.B, result.B);
        }

        [TestMethod]
        public void Lighten_InvalidPercent_ThrowsArgumentOutOfRangeException()
        {
            var original = new RgbColor(100, 100, 100);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                ColorConverter.Lighten(original, -1);
            });
        }

        [TestMethod]
        public void Darken_ValidColor_ReturnsDarkerColor()
        {
            var original = new RgbColor(100, 100, 100);

            var result = ColorConverter.Darken(original, 20);

            Assert.IsTrue(result.R <= original.R);
            Assert.IsTrue(result.G <= original.G);
            Assert.IsTrue(result.B <= original.B);
        }

        [TestMethod]
        public void Darken_ZeroPercent_ReturnsSameColor()
        {
            var original = new RgbColor(100, 100, 100);

            var result = ColorConverter.Darken(original, 0);

            Assert.AreEqual(original.R, result.R);
            Assert.AreEqual(original.G, result.G);
            Assert.AreEqual(original.B, result.B);
        }

        [TestMethod]
        public void Darken_InvalidPercent_ThrowsArgumentOutOfRangeException()
        {
            var original = new RgbColor(100, 100, 100);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                ColorConverter.Darken(original, 101);
            });
        }

        [TestMethod]
        public void Blend_ValidRatio_ReturnsMixedColor()
        {
            var first = new RgbColor(255, 0, 0);
            var second = new RgbColor(0, 0, 255);

            var result = ColorConverter.Blend(first, second, 0.5);

            Assert.AreEqual(128, result.R);
            Assert.AreEqual(0, result.G);
            Assert.AreEqual(128, result.B);
        }

        [TestMethod]
        public void Blend_ZeroRatio_ReturnsFirstColor()
        {
            var first = new RgbColor(255, 0, 0);
            var second = new RgbColor(0, 0, 255);

            var result = ColorConverter.Blend(first, second, 0);

            Assert.AreEqual(first.R, result.R);
            Assert.AreEqual(first.G, result.G);
            Assert.AreEqual(first.B, result.B);
        }

        [TestMethod]
        public void Blend_InvalidRatio_ThrowsArgumentOutOfRangeException()
        {
            var first = new RgbColor(255, 0, 0);
            var second = new RgbColor(0, 0, 255);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                ColorConverter.Blend(first, second, 2);
            });
        }
    }
}
﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegularExpressionUnitTests.cs" company="James South">
//   Copyright (c) James South.
//   Licensed under the Apache License, Version 2.0.
// </copyright>
// <summary>
//   Unit tests for the ImageProcessor regular expressions
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImageProcessor.Web.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using ImageProcessor.Imaging;
    using ImageProcessor.Imaging.Filters;
    using ImageProcessor.Imaging.Formats;
    using ImageProcessor.Plugins.WebP.Imaging.Formats;

    using NUnit.Framework;

    /// <summary>
    /// Test harness for the regular expressions
    /// </summary>
    [TestFixture]
    public class RegularExpressionUnitTests
    {
        /// <summary>
        /// The alpha regex unit test.
        /// </summary>
        /// <param name="input">
        /// The input string.
        /// </param>
        /// <param name="expected">
        /// The expected result.
        /// </param>
        [Test]
        [TestCase("alpha=66", 66)]
        [TestCase("alpha=-66", 66)]
        [TestCase("alpha=101", 1)]
        [TestCase("alpha=-101", 1)]
        [TestCase("alpha=000053", 53)]
        public void TestAlphaRegex(string input, int expected)
        {
            Processors.Alpha alpha = new Processors.Alpha();
            alpha.MatchRegexIndex(input);
            int result = alpha.Processor.DynamicParameter;

            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// The contrast regex unit test.
        /// </summary>
        /// <param name="input">
        /// The input string.
        /// </param>
        /// <param name="expected">
        /// The expected result.
        /// </param>
        [Test]
        [TestCase("brightness=56", 56)]
        [TestCase("brightness=84", 84)]
        [TestCase("brightness=66", 66)]
        [TestCase("brightness=101", 1)]
        [TestCase("brightness=00001", 1)]
        [TestCase("brightness=-50", -50)]
        [TestCase("brightness=0", 0)]
        public void TestBrightnesstRegex(string input, int expected)
        {
            Processors.Brightness brightness = new Processors.Brightness();
            brightness.MatchRegexIndex(input);
            int result = brightness.Processor.DynamicParameter;

            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// The contrast regex unit test.
        /// </summary>
        /// <param name="input">
        /// The input string.
        /// </param>
        /// <param name="expected">
        /// The expected result.
        /// </param>
        [Test]
        [TestCase("contrast=56", 56)]
        [TestCase("contrast=84", 84)]
        [TestCase("contrast=66", 66)]
        [TestCase("contrast=101", 1)]
        [TestCase("contrast=00001", 1)]
        [TestCase("contrast=-50", -50)]
        [TestCase("contrast=0", 0)]
        public void TestContrastRegex(string input, int expected)
        {
            Processors.Contrast contrast = new Processors.Contrast();
            contrast.MatchRegexIndex(input);
            int result = contrast.Processor.DynamicParameter;

            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// The saturation regex unit test.
        /// </summary>
        /// <param name="input">
        /// The input string.
        /// </param>
        /// <param name="expected">
        /// The expected result.
        /// </param>
        [Test]
        [TestCase("saturation=56", 56)]
        [TestCase("saturation=84", 84)]
        [TestCase("saturation=66", 66)]
        [TestCase("saturation=101", 1)]
        [TestCase("saturation=00001", 1)]
        [TestCase("saturation=-50", -50)]
        [TestCase("saturation=0", 0)]
        public void TestSaturationRegex(string input, int expected)
        {
            Processors.Saturation saturation = new Processors.Saturation();
            saturation.MatchRegexIndex(input);
            int result = saturation.Processor.DynamicParameter;

            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// The rotate regex unit test.
        /// </summary>
        [Test]
        public void TestCropRegex()
        {
            const string Querystring = "crop=0,0,150,300";
            CropLayer expected = new CropLayer(0, 0, 150, 300, CropMode.Pixels);

            Processors.Crop crop = new Processors.Crop();
            crop.MatchRegexIndex(Querystring);

            CropLayer actual = crop.Processor.DynamicParameter;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// The filter regex unit test.
        /// </summary>
        [Test]

        public void TestFilterRegex()
        {
            Dictionary<string, IMatrixFilter> data = new Dictionary<string, IMatrixFilter>
            {
                {
                    "filter=lomograph", MatrixFilters.Lomograph
                },
                {
                    "filter=polaroid", MatrixFilters.Polaroid
                },
                {
                    "filter=comic", MatrixFilters.Comic
                },
                {
                    "filter=greyscale", MatrixFilters.GreyScale
                },
                {
                    "filter=blackwhite", MatrixFilters.BlackWhite
                },
                {
                    "filter=invert", MatrixFilters.Invert
                },
                {
                    "filter=gotham", MatrixFilters.Gotham
                },
                {
                    "filter=hisatch", MatrixFilters.HiSatch
                },
                {
                    "filter=losatch", MatrixFilters.LoSatch
                },
                {
                    "filter=sepia", MatrixFilters.Sepia
                }
            };

            Processors.Filter filter = new Processors.Filter();
            foreach (KeyValuePair<string, IMatrixFilter> item in data)
            {
                filter.MatchRegexIndex(item.Key);
                IMatrixFilter result = filter.Processor.DynamicParameter;
                Assert.AreEqual(item.Value, result);
            }
        }

        /// <summary>
        /// The format regex unit test.
        /// </summary>
        /// <param name="input">
        /// The input querystring.
        /// </param>
        /// <param name="expected">
        /// The expected type.
        /// </param>
        [Test]
        [TestCase("format=bmp", typeof(BitmapFormat))]
        [TestCase("format=png", typeof(PngFormat))]
        [TestCase("format=png8", typeof(PngFormat))]
        [TestCase("format=jpeg", typeof(JpegFormat))]
        [TestCase("format=jpg", typeof(JpegFormat))]
        [TestCase("format=gif", typeof(GifFormat))]
        [TestCase("format=webp", typeof(WebPFormat))]
        public void TestFormatRegex(string input, Type expected)
        {
            Processors.Format format = new Processors.Format();
            format.MatchRegexIndex(input);
            Type result = format.Processor.DynamicParameter.GetType();

            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// The quality regex unit test.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="expected">
        /// The expected result.
        /// </param>
        [Test]
        [TestCase("quality=56", 56)]
        [TestCase("quality=84", 84)]
        [TestCase("quality=66", 66)]
        [TestCase("quality=101", 1)]
        [TestCase("quality=00001", 1)]
        [TestCase("quality=-50", 50)]
        [TestCase("quality=0", 0)]
        public void TestQualityRegex(string input, int expected)
        {
            Processors.Quality quality = new Processors.Quality();
            quality.MatchRegexIndex(input);
            int result = quality.Processor.DynamicParameter;

            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// The resize regex unit test.
        /// </summary>
        [Test]
        public void TestResizeRegex()
        {
            const string Querystring = "width=300";
            ResizeLayer expected = new ResizeLayer(new Size(300, 0));

            Processors.Resize resize = new Processors.Resize();

            resize.MatchRegexIndex(Querystring);
            ResizeLayer actual = resize.Processor.DynamicParameter;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// The rotate regex unit test.
        /// </summary>
        /// <param name="input">
        /// The input string.
        /// </param>
        /// <param name="expected">
        /// The expected result.
        /// </param>
        [Test]
        [TestCase("rotate=0", 0)]
        [TestCase("rotate=270", 270)]
        [TestCase("rotate=-270", 0)]
        [TestCase("rotate=angle-28", 28)]
        public void TestRotateRegex(string input, int expected)
        {
            Processors.Rotate rotate = new Processors.Rotate();
            rotate.MatchRegexIndex(input);

            int result = rotate.Processor.DynamicParameter;

            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// The rounded corners regex unit test.
        /// </summary>
        [Test]
        public void TestRoundedCornersRegex()
        {
            Dictionary<string, RoundedCornerLayer> data = new Dictionary<string, RoundedCornerLayer>
            {
                {
                    "roundedcorners=30", new RoundedCornerLayer(30, true, true, true, true)
                },
                {
                    "roundedcorners=radius-26|tl-true|tr-false|bl-true|br-false", new RoundedCornerLayer(26, true, false, true, false)
                },
                {
                    "roundedcorners=26,tl=true,tr=false,bl=true,br=false", new RoundedCornerLayer(26, true, false, true, false)
                }
            };

            Processors.RoundedCorners roundedCorners = new Processors.RoundedCorners();
            foreach (KeyValuePair<string, RoundedCornerLayer> item in data)
            {
                roundedCorners.MatchRegexIndex(item.Key);
                RoundedCornerLayer result = roundedCorners.Processor.DynamicParameter;
                Assert.AreEqual(item.Value, result);
            }
        }

        /// <summary>
        /// The tint regex unit test.
        /// </summary>
        [Test]
        public void TestTintRegex()
        {
            Dictionary<string, Color> data = new Dictionary<string, Color>
            {
                {
                    "tint=6aa6cc", ColorTranslator.FromHtml("#" + "6aa6cc")
                },
                {
                    "tint=106,166,204,255", Color.FromArgb(255, 106, 166, 204)
                },
                {
                    "tint=fff", Color.FromArgb(255, 255, 255, 255)
                },
                {
                    "tint=white", Color.White
                }
            };

            Processors.Tint tint = new Processors.Tint();
            foreach (KeyValuePair<string, Color> item in data)
            {
                tint.MatchRegexIndex(item.Key);
                Color result = tint.Processor.DynamicParameter;
                Assert.AreEqual(item.Value, result);
            }
        }
    }
}
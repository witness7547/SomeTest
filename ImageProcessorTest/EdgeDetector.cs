using ImageProcessor.Imaging.Filters.EdgeDetection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessorTest
{
    public class EdgeDetector
    {
        public Bitmap ProcessImage(Bitmap src)
        {
            Bitmap image2;
            Bitmap image = src;

            IEdgeFilter edgeFilter = new SobelEdgeFilter22();
            bool greyscale = false;
            ConvolutionFilter filter2 = new ConvolutionFilter(edgeFilter, greyscale);
            image2 = filter2.Process2DFilter((Bitmap)image);
            return image2;
        }

        
        public class SobelEdgeFilter22 : I2DEdgeFilter, IEdgeFilter
        {
            // Methods
            public SobelEdgeFilter22() { }

            // Properties
            public double[,] HorizontalGradientOperator
            {
                get
                {
                    return new double[,] { { -1.0, 0.0, 1.0 }, { -2.0, 0.0, 2.0 }, { -1.0, 0.0, 1.0 } };
                }
            }

            public double[,] VerticalGradientOperator
            {
                get
                {
                    return new double[,] { { 1.0, 2.0, 1.0 }, { 0.0, 0.0, 0.0 }, { -1.0, -2.0, -1.0 } };
                }
            }

        }

        public class ConvolutionFilter
        {
            // Fields
            private readonly IEdgeFilter edgeFilter;
            private readonly bool greyscale;

            // Methods
            public ConvolutionFilter(IEdgeFilter edgeFilter, bool greyscale)
            {
                this.edgeFilter = edgeFilter;
                this.greyscale = greyscale;
            }

            public Bitmap Process2DFilter(Image source)
            {
                int width = source.Width;
                int height = source.Height;
                int maxWidth = width + 1;
                int maxHeight = height + 1;
                int bufferedWidth = width + 2;
                int bufferedHeight = height + 2;
                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
                Bitmap image = new Bitmap(bufferedWidth, bufferedHeight, PixelFormat.Format32bppPArgb);
                bitmap.SetResolution(source.HorizontalResolution, source.VerticalResolution);
                image.SetResolution(source.HorizontalResolution, source.VerticalResolution);
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    graphics.Clear(Color.Transparent);
                    Rectangle rect = new Rectangle(0, 0, bufferedWidth, bufferedHeight);
                    Rectangle dstRect = new Rectangle(0, 0, width, height);
                    using (ImageAttributes attributes = new ImageAttributes())
                    {
                        if (this.greyscale)
                        {
                            //attributes.SetColorMatrix(ColorMatrixes.GreyScale);
                        }
                        using (TextureBrush brush = new TextureBrush(source, dstRect, attributes))
                        {
                            brush.WrapMode = WrapMode.TileFlipXY;
                            brush.TranslateTransform(1f, 1f);
                            graphics.FillRectangle(brush, rect);
                        }
                    }
                }
                try
                {
                    double[,] horizontalFilter = this.edgeFilter.HorizontalGradientOperator;
                    double[,] verticalFilter = ((I2DEdgeFilter)this.edgeFilter).VerticalGradientOperator;
                    int kernelLength = horizontalFilter.GetLength(0);
                    int radius = kernelLength >> 1;
                    //using (FastBitmap sourceBitmap = new FastBitmap(image))
                    {
                        //using (FastBitmap destinationBitmap = new FastBitmap(bitmap))
                        {
                            Parallel.For(0, bufferedHeight, delegate (int y) {
                                for (int m = 0; m < bufferedWidth; m++)
                                {
                                    double num2 = 0.0;
                                    double num3 = 0.0;
                                    double num4 = 0.0;
                                    double num5 = 0.0;
                                    double num6 = 0.0;
                                    double num7 = 0.0;
                                    for (int i = 0; i < kernelLength; i++)
                                    {
                                        int num11 = i - radius;
                                        int num12 = y + num11;
                                        if (num12 >= 0)
                                        {
                                            if (num12 >= bufferedHeight)
                                            {
                                                break;
                                            }
                                            for (int j = 0; j < kernelLength; j++)
                                            {
                                                int num14 = j - radius;
                                                int x = m + num14;
                                                if ((x >= 0) && (x < bufferedWidth))
                                                {
                                                    //Color pixel = sourceBitmap.GetPixel(x, num12);
                                                    Color pixel = image.GetPixel(x, num12);
                                                    double r = pixel.R;
                                                    double g = pixel.G;
                                                    double b = pixel.B;
                                                    num2 += horizontalFilter[i, j] * r;
                                                    num3 += verticalFilter[i, j] * r;
                                                    num4 += horizontalFilter[i, j] * g;
                                                    num5 += verticalFilter[i, j] * g;
                                                    num6 += horizontalFilter[i, j] * b;
                                                    num7 += verticalFilter[i, j] * b;
                                                }
                                            }
                                        }
                                    }
                                    byte green = Math.Sqrt((num4 * num4) + (num5 * num5)).ToByte();
                                    byte blue = Math.Sqrt((num6 * num6) + (num7 * num7)).ToByte();
                                    Color color = Color.FromArgb(Math.Sqrt((num2 * num2) + (num3 * num3)).ToByte(), green, blue);
                                    if (((y > 0) && (m > 0)) && ((y < maxHeight) && (m < maxWidth)))
                                    {
                                        //destinationBitmap.SetPixel(m - 1, y - 1, color);
                                        bitmap.SetPixel(m - 1, y - 1, color);
                                    }
                                }
                            });
                        }
                    }
                }
                finally
                {
                    image.Dispose();
                }
                return bitmap;
            }



            public Bitmap ProcessFilter(Image source)
            {
                int width = source.Width;
                int height = source.Height;
                int maxWidth = width + 1;
                int maxHeight = height + 1;
                int bufferedWidth = width + 2;
                int bufferedHeight = height + 2;
                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
                Bitmap image = new Bitmap(bufferedWidth, bufferedHeight, PixelFormat.Format32bppPArgb);
                bitmap.SetResolution(source.HorizontalResolution, source.VerticalResolution);
                image.SetResolution(source.HorizontalResolution, source.VerticalResolution);
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    graphics.Clear(Color.Transparent);
                    Rectangle rect = new Rectangle(0, 0, bufferedWidth, bufferedHeight);
                    Rectangle dstRect = new Rectangle(0, 0, width, height);
                    using (ImageAttributes attributes = new ImageAttributes())
                    {
                        //if (this.greyscale)
                        //{
                        //    attributes.SetColorMatrix(ColorMatrixes.GreyScale);
                        //}
                        //using (TextureBrush brush = new TextureBrush(source, dstRect, attributes))
                        //{
                        //    brush.WrapMode = WrapMode.TileFlipXY;
                        //    brush.TranslateTransform(1f, 1f);
                        //    graphics.FillRectangle(brush, rect);
                        //}
                    }
                }
                try
                {
                    double[,] horizontalFilter = this.edgeFilter.HorizontalGradientOperator;
                    int kernelLength = horizontalFilter.GetLength(0);
                    int radius = kernelLength >> 1;
                    //using (FastBitmap sourceBitmap = new FastBitmap(image))
                    {
                        //using (FastBitmap destinationBitmap = new FastBitmap(bitmap))
                        {
                            Parallel.For(0, bufferedHeight, delegate (int y) {
                                for (int m = 0; m < bufferedWidth; m++)
                                {
                                    double num2 = 0.0;
                                    double num3 = 0.0;
                                    double num4 = 0.0;
                                    for (int i = 0; i < kernelLength; i++)
                                    {
                                        int num8 = i - radius;
                                        int num9 = y + num8;
                                        if (num9 >= 0)
                                        {
                                            if (num9 >= bufferedHeight)
                                            {
                                                break;
                                            }
                                            for (int j = 0; j < kernelLength; j++)
                                            {
                                                int num11 = j - radius;
                                                int x = m + num11;
                                                if ((x >= 0) && (x < bufferedWidth))
                                                {
                                                    //Color pixel = sourceBitmap.GetPixel(x, num9);
                                                    Color pixel = image.GetPixel(x, num9);
                                                    double r = pixel.R;
                                                    double g = pixel.G;
                                                    double b = pixel.B;
                                                    num2 += horizontalFilter[i, j] * r;
                                                    num3 += horizontalFilter[i, j] * g;
                                                    num4 += horizontalFilter[i, j] * b;
                                                }
                                            }
                                        }
                                    }
                                    byte green = num3.ToByte();
                                    byte blue = num4.ToByte();
                                    Color color = Color.FromArgb(num2.ToByte(), green, blue);
                                    if (((y > 0) && (m > 0)) && ((y < maxHeight) && (m < maxWidth)))
                                    {
                                        //destinationBitmap.SetPixel(m - 1, y - 1, color);
                                        bitmap.SetPixel(m - 1, y - 1, color);
                                    }
                                }
                            });
                        }
                    }
                }
                finally
                {
                    image.Dispose();
                }
                return bitmap;
            }


        }


    }

    public static class DoubleExtensions
    {
        // Methods
        public static byte ToByte(this double value)
        {
            double temp = Compare(value);
            return Convert.ToByte(temp);
        }

        private static double Compare(double value)
        {
            if (value < 0.0)
            {
                return 0.0;
            }
            else if (value > 255.0)
            {
                return 255.0;
            }

            return value;
        }


    }



}

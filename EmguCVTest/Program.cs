using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguCVTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Image<Bgr, byte> inputImage = new Image<Bgr, byte>(@"D:\1.jpg");
            //ImageViewer.Show(inputImage);
            ////Console.ReadKey();

            //inputImage._EqualizeHist();
            //inputImage._GammaCorrect(1.8d);

            //ImageViewer.Show(inputImage);


            //==================================================================
            //string win1 = "origin";
            //CvInvoke.NamedWindow(win1);
            //Mat img = new Mat(@"D:\图片\2.jpg");
            //CvInvoke.Imshow(win1, img);
            //CvInvoke.WaitKey();
            //===================================================================


            //Image<Bgr, byte> inputImage = new Image<Bgr, byte>(@"D:\图片\2.jpg");
            //Image<Gray, byte> grayImg = inputImage.Convert<Gray, byte>();
            //ImageViewer.Show(grayImg);

            //grayImg._GammaCorrect(2.0);
            //ImageViewer.Show(grayImg);

            //ImageViewer.Show(inputImage);
            //Console.ReadKey();

            //inputImage._EqualizeHist();
            //inputImage._GammaCorrect(1.8d);
            //ImageViewer.Show(inputImage);
            //CvInvoke.WaitKey();

            //Image<Gray, byte> grayImg = inputImage.Convert<Gray, byte>();
            //ImageViewer.Show(grayImg);

            //grayImg.Canny(200, 100);
            //ImageViewer.Show(grayImg);

            //CvInvoke.DestroyWindow(win1);

            CannyEdgeDetector();
        }

        private static void CannyEdgeDetector()
        {
            Image<Bgr, byte> inputImage = new Image<Bgr, byte>(@"D:\图片\4.png");
            Image<Gray, byte> grayImg = inputImage.Convert<Gray, byte>();
            //ImageViewer.Show(grayImg);

            int width = grayImg.Width;
            int height = grayImg.Height;
            int[,] data = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    data[i, j] = (int)grayImg.Data[i, j, 0];
                }
            }


            float[,] gradX = Differentiate(data, new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } });
            float[,] gradY = Differentiate(data, new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } });

            float[,] mag = new float[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    mag[i, j] = Math.Abs(gradX[i, j]) + Math.Abs(gradY[i,j]);
                }
            }

            grayImg.Data = Tobyte(mag);
            ImageViewer.Show(grayImg);

            float[,] nonMax = NonMaximumSuppression(gradX, gradY, mag);
            grayImg.Data = Tobyte(nonMax);
            ImageViewer.Show(grayImg);

            float[,] doubleThreshold = DoubleThreshold(nonMax, 50);
            grayImg.Data = Tobyte(doubleThreshold);
            ImageViewer.Show(grayImg);
        }

        private static float[,] Differentiate(int[,] Data, int[,] Filter)
        {
            int Width = Data.GetLength(0);
            int Height = Data.GetLength(1);

            int i, j, k, l, Fh, Fw;

            Fw = Filter.GetLength(0);
            Fh = Filter.GetLength(1);
            float sum = 0;
            float[,] Output = new float[Width, Height];

            for (i = Fw / 2; i <= (Width - Fw / 2) - 1; i++)
            {
                for (j = Fh / 2; j <= (Height - Fh / 2) - 1; j++)
                {
                    sum = 0;
                    for (k = -Fw / 2; k <= Fw / 2; k++)
                    {
                        for (l = -Fh / 2; l <= Fh / 2; l++)
                        {
                            sum = sum + Data[i + k, j + l] * Filter[Fw / 2 + k, Fh / 2 + l];
                        }
                    }
                    Output[i, j] = sum;

                }

            }
            return Output;

        }

        public static byte[,,] Tobyte(float[,] input)
        {
            byte[,,] output = new byte[input.GetLength(0), input.GetLength(1), 1];

            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    output[i, j, 0] = (byte)input[i, j];
                }
            }

            return output;
        }

        public static float[,] NonMaximumSuppression(float[,] DerivativeX, float[,] DerivativeY, float[,] Gradient)
        {
            int Width = Gradient.GetLength(0);
            int Height = Gradient.GetLength(1);
            int Limit = /*KernelSize / 2*/1;
            int i, j;
            float Tangent;

            for (i = Limit; i <= (Width - Limit) - 1; i++)
            {
                for (j = Limit; j <= (Height - Limit) - 1; j++)
                {

                    if (DerivativeX[i, j] == 0)
                        Tangent = 90F;
                    else
                        Tangent = (float)(Math.Atan(DerivativeY[i, j] / DerivativeX[i, j]) * 180 / Math.PI); //rad to degree



                    //Horizontal Edge
                    if (((-22.5 < Tangent) && (Tangent <= 22.5)) || ((157.5 < Tangent) && (Tangent <= -157.5)))
                    {
                        if ((Gradient[i, j] < Gradient[i, j + 1]) || (Gradient[i, j] < Gradient[i, j - 1]))
                            Gradient[i, j] = 0;
                    }


                    //Vertical Edge
                    if (((-112.5 < Tangent) && (Tangent <= -67.5)) || ((67.5 < Tangent) && (Tangent <= 112.5)))
                    {
                        if ((Gradient[i, j] < Gradient[i + 1, j]) || (Gradient[i, j] < Gradient[i - 1, j]))
                            Gradient[i, j] = 0;
                    }

                    //+45 Degree Edge
                    if (((-67.5 < Tangent) && (Tangent <= -22.5)) || ((112.5 < Tangent) && (Tangent <= 157.5)))
                    {
                        if ((Gradient[i, j] < Gradient[i + 1, j - 1]) || (Gradient[i, j] < Gradient[i - 1, j + 1]))
                            Gradient[i, j] = 0;
                    }

                    //-45 Degree Edge
                    if (((-157.5 < Tangent) && (Tangent <= -112.5)) || ((67.5 < Tangent) && (Tangent <= 22.5)))
                    {
                        if ((Gradient[i, j] < Gradient[i + 1, j + 1]) || (Gradient[i, j] < Gradient[i - 1, j - 1]))
                            Gradient[i, j] = 0;
                    }

                }
            }


            return Gradient;
        }

        public static float[,] DoubleThreshold(float[,] input, float minThreshold)
        {
            float[,] output = new float[input.GetLength(0), input.GetLength(1)];
            float maxThreshold = minThreshold * 3;
            //float minThreshold = minThreshold;
            int limit = 1;

            for (int i = 0; i < input.GetLength(0) - limit; i++)
            {
                for (int j = 0; j < input.GetLength(1) - limit; j++)
                {
                    if (input[i, j] >= maxThreshold)
                    {
                        output[i, j] = 255;
                    }
                    else if (input[i,j] < maxThreshold && input[i, j] >= minThreshold)
                    {
                        if (input[i - 1, j] >= maxThreshold || input[i + 1, j] >= maxThreshold ||
                            input[i - 1, j -1] >= maxThreshold || input[i + 1, j - 1] >= maxThreshold ||
                            input[i - 1, j + 1] >= maxThreshold || input[i + 1, j + 1] >= maxThreshold ||
                            input[i, j - 1] >= maxThreshold || input[i, j + 1] >= maxThreshold )
                        {
                            output[i, j] = 255;
                        }
                        else
                        {
                            output[i, j] = 0;
                        }
                    }
                    else
                    {
                        output[i, j] = 0;
                    }
                }
            }

            return output;
        }
    }
}

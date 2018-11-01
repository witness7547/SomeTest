using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageProcessor;
using ImageProcessor.Processors;
using ImageProcessor.Imaging;
using ImageProcessor.Common.Extensions;

namespace ImageProcessorTest
{
    public partial class Form1 : Form
    {
        ImageFactory imgF;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            imgF = new ImageFactory().Load(@"D:\图片\1.jpg");
            this.pictureBox1.Image = imgF.Image;
        }

        private void btnGamma_Click(object sender, EventArgs e)
        {
            //if (imgF == null)
            //{
            //    MessageBox.Show("ImgF is null!");
            //    return;
            //}

            //Gamma gamma = new Gamma();
            //gamma.DynamicParameter = 2.0f;

            //this.pictureBox1.Image = gamma.ProcessImage(imgF);

            //this.pictureBox1.Image = imgF.Contrast(80).Image;




            //edge detect test
            //this.pictureBox1.Image = imgF.DetectEdges(new ImageProcessor.Imaging.Filters.EdgeDetection.SobelEdgeFilter(), false).Image;
            EdgeDetector ed = new EdgeDetector();
            Bitmap edgeImg = ed.ProcessImage(new Bitmap(@"D:\图片\1.jpg"));
            this.pictureBox1.Image = edgeImg;
        }


        public static Bitmap Gamma(Image source, float value)
        {
            if ((value > 5f) || (value < 0.1))
            {
                throw new ArgumentOutOfRangeException("value", "Value should be between .1 and 5.");
            }
            byte[] ramp = new byte[0x100];
            for (int j = 0; j < 0x100; j++)
            {
                ramp[j] = ((255.0 * Math.Pow(((double)j) / 255.0, (double)value)) + 0.5).ToByte();
            }
            int width = source.Width;
            int height = source.Height;
            using (FastBitmap bitmap = new FastBitmap(source))
            {
                Parallel.For(0, height, delegate (int y) {
                    for (int k = 0; k < width; k++)
                    {
                        Color pixel = bitmap.GetPixel(k, y);
                        Color color = Color.FromArgb(pixel.A, ramp[pixel.R], ramp[pixel.G], ramp[pixel.B]);
                        bitmap.SetPixel(k, y, color);
                    }
                });
            }
            return (Bitmap)source;
        }



    }
}

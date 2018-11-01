using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pictureBoxLoadAsync
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.pictureBox1.LoadCompleted += PictureBox1_LoadCompleted;
            this.pictureBox1.LoadProgressChanged += PictureBox1_LoadProgressChanged;
        }

        private void PictureBox1_LoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void PictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Load error");
            }
            else if(e.Cancelled)
            {
                MessageBox.Show("Load canceled", "Canceled");
            }
            else
            {
                MessageBox.Show("Load compleated", "Compleated");
            }
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            this.pictureBox1.LoadAsync(this.textBox1.Text);
        }

        private void cancelLoadBtn_Click(object sender, EventArgs e)
        {
            this.pictureBox1.CancelAsync();
        }
    }
}

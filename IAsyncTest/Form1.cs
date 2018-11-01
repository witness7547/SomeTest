using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IAsyncTest
{
    public partial class Form1 : Form
    {
        public delegate int Mydelegate(int num, out int threadID);
        Mydelegate myDel;

        public Form1()
        {
            InitializeComponent();
            lblThreadId.Text = "current threadID:" + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
            myDel = new Mydelegate(Fun1);
        }        

        private void btnSync_Click(object sender, EventArgs e)
        {
            int temp = 0;

            lbl1.Text = Fun1(10, out temp).ToString();
            lbl2.Text = Fun2(20).ToString();
        }

        private void btnAsync_Click(object sender, EventArgs e)
        {
            //IAsyncResult ires = myDel.BeginInvoke(10, null, null);
            //lbl1.Text = "computing...";

            //lbl2.Text = Fun2(20).ToString();

            //int myresult = myDel.EndInvoke(ires);//If the asynchonous call has not completed, endinvoke blocks the calling thread until it completes.
            //lbl1.Text = myresult.ToString();


            //use callback function
            int temp = 0;
            /*IAsyncResult ires = */myDel.BeginInvoke(10, out temp, MyCallback, WindowsFormsSynchronizationContext.Current);//每次异步执行所选取的work thread不是唯一的，可能会发生变化（可以根据theadID看出）
            lbl1.Text = "computing...";

            lbl2.Text = Fun2(20).ToString();            
        }

        private int Fun1(int num, out int threadid)
        {
            System.Threading.Thread.Sleep(5000);
            threadid = System.Threading.Thread.CurrentThread.ManagedThreadId;
            //this.lbl1.Text = "Fun1计算完成，马上显示结果...";//该句调试运行的时候会抛出跨线程修改UI控件的异常，但是非调试运行却通过？？？
            //this.lblThreadId.Text = threadid.ToString();
            System.Threading.Thread.Sleep(1000);
            return num * num;
        }

        private int Fun2(int num)
        {
            return num * num;
        }

        private void MyCallback(IAsyncResult asyncRes)
        {
            //MessageBox.Show(asyncRes.IsCompleted.ToString());
            int myThreadId = 0;
            int result = myDel.EndInvoke(out myThreadId,asyncRes);
            //lbl1.Text = string.Format("result:{0}, asyncState:{1}, threadIDOfAsync:{2}", result, asyncRes.AsyncState, myThreadId);
            //this.lblThreadId.Text = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();

            //如何正确的更新到界面
            if (lbl1.InvokeRequired)
            {
                //方法1：
                //lbl1.Invoke(new Action<string>(s => { lbl1.Text = s; }), result.ToString());

                //方法2：
                ((WindowsFormsSynchronizationContext)asyncRes.AsyncState).Post(delegate { lbl1.Text = result.ToString(); }, null);                
            }

        }
    }
}

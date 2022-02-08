using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSemaphore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            int temp = Count - 1;
            for (int i = temp; i < int.Parse(tbxCount.Text) + temp; i++)
            {
                var thread = new Thread(() => { });
                thread.Name = $"Thread {Count++}";
                listBox3.Items.Add(thread.Name);
            }
        }

        private void Waiting(string name)
        {
            var semaphore = new Semaphore(1, 1);
            while (true)
            {
                if (semaphore.WaitOne(9000))
                {
                    try
                    {
                        listBox1.Items.Add(name);
                        Thread.Sleep(9000);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                    break;
                }
                else
                {
                    listBox2.Items.Add(name);
                }
            }
        }
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                try
                {
                    Waiting(listBox3.SelectedItem.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }).Start();
        }
        static int Count { get; set; } = 1;
    }
}
using DryIoc.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgressBarApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        static int length = 1000;
        int[,] matrix = new int[length, length];
        int[,] matrix1 = new int[length, length];
        int[,] matrix2 = new int[length, length];

        private void btnStart_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
  
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (progressBar.Value <= progressBar.Maximum)
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Done");
            progressBar.Value = 0;
            progressBar.Maximum = 100;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Random randNum = new Random();
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    matrix[i, j] = randNum.Next(0, length);
                    matrix1[i, j] = randNum.Next(0, length);
                }
            };        
            
            for (int i = 0; i < matrix2.GetLength(0); i++)
            {

                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    matrix2[i, j] = 0;
                    for (int k = 0; k < matrix2.GetLength(1); k++)
                    { 
                        if (backgroundWorker1.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        matrix2[i, j] = matrix2[i, j] + matrix[i, k] * matrix1[k, j];
                        }
                      
                    }
                backgroundWorker1.ReportProgress((i * length) * 100 / (length * length));
            }
        }

       
    };
}
    


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YUVConverter
{
    public partial class Form1 : Form
    {
        string filename;
        int _currentImageIndex = 0, width = 0, height = 0, mode = 0;
        YuvReader read;
        Timer t;

        int CurrentImageIndex
        {
            get { return _currentImageIndex; }
            set
            {
                _currentImageIndex = value;
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(() => { pictureBox1.Image = read.bitmapsY[_currentImageIndex]; }));
                }
                else
                {
                    pictureBox1.Image = read.bitmapsY[_currentImageIndex];
                }
            }
        }

        void WhenTimerTicks(object sender, EventArgs e)
        {
            if (CurrentImageIndex < read.bitmapsY.Count - 1)
                CurrentImageIndex++;
            else
                CurrentImageIndex = 0;
            label5.Text = "" + (1 + CurrentImageIndex);
        }

        private void play_Click(object sender, EventArgs e)
        {
            t.Start();

        }

        private void pause_Click(object sender, EventArgs e)
        {
            t.Stop();
        }

        private void framedown_Click(object sender, EventArgs e)
        {
            if (CurrentImageIndex > 0)
                CurrentImageIndex--;
            else
                CurrentImageIndex = read.bitmapsY.Count - 1;
            label5.Text = "" + (1 + CurrentImageIndex);
        }

        private void frame_up_Click(object sender, EventArgs e)
        {
            if (CurrentImageIndex < read.bitmapsY.Count - 1)
                CurrentImageIndex++;
            else
                CurrentImageIndex = 0;
            label5.Text = "" + (1 + CurrentImageIndex);
        }

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("YUV444");
            comboBox1.Items.Add("YUV422");
            comboBox1.Items.Add("YUV420");
            t = new Timer();
            t.Interval = 1000 / 25; // 25 FPS
            t.Tick += WhenTimerTicks;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _currentImageIndex = 0;
            label5.Text = "" + 0;
            filename = addresBox.Text;
            width = Int32.Parse(textBox1.Text);
            height = Int32.Parse(textBox2.Text);
            mode = comboBox1.SelectedIndex;
            read = new YuvReader(filename, width, height, mode);
            read.readFrames();
            Console.WriteLine("done!");
            label4.Text = "" + read.bitmapsY.Count;
            pictureBox1.Width = width;
            pictureBox1.Height = height;
            pictureBox1.Image = read.bitmapsY[0];
            if(width > this.Size.Width)
                this.Size = new Size(width + 60, this.Size.Height);
            if (height > this.Size.Height)
                this.Size = new Size(this.Width, height + 140);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "YUV files|*.yuv";
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                addresBox.Text = openFileDialog1.FileName;
                filename = addresBox.Text;
            }
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

namespace SpinDetection
{
    public partial class SpinDetect : Form
    {
        Image<Gray, byte> GrayFrame;
        Image<Bgr, byte> Frames;
        public CascadeClassifier spin = new CascadeClassifier("SPIN_HOG.xml");

        public SpinDetect()
        {
            InitializeComponent();
        }

        private void ProcessFrame()
        {
            GrayFrame = Frames.Convert<Gray, byte>();

            Rectangle[] spinsDetected = spin.DetectMultiScale(GrayFrame, 1.1, 3, new Size(20, 20), Size.Empty);

            for (int i = 0; i < spinsDetected.Length; i++)
            {
                Frames.Draw(spinsDetected[i], new Bgr(Color.Red), 2);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cameraViewBox.ClearOperation();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image InputImg = Image.FromFile(openFileDialog.FileName);
                Frames = new Image<Bgr, byte>(new Bitmap(InputImg));
                cameraViewBox.Image = Frames;
            }
            ProcessFrame();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}

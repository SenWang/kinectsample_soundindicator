using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace WpfApplication1
{
    public partial class SoundDirection : Window
    {
        KinectSensor kinect;
        public SoundDirection(KinectSensor sensor) : this()
        {
            kinect = sensor;
            kinect.Start();
            KinectAudioSource audioSource = AudioSourceSetup(kinect.AudioSource);
            audioSource.Start();
            audioSource.SoundSourceAngleChanged += audioSource_SoundSourceAngleChanged;
        }
        public SoundDirection()
        {
            InitializeComponent();
            bar.X1 = this.Width / 2;
            bar.Y1 = 0;
            bar.X2 = this.Width / 2;
            bar.Y2 = this.Height;
            middlebar.X1 = this.Width / 2;
            middlebar.Y1 = 0;
            middlebar.X2 = this.Width / 2;
            middlebar.Y2 = this.Height;
        }

        private KinectAudioSource AudioSourceSetup(KinectAudioSource audioSource)
        {
            audioSource.NoiseSuppression = true;
            audioSource.AutomaticGainControlEnabled = true;
            audioSource.BeamAngleMode = BeamAngleMode.Adaptive;
            return audioSource;
        }

        //改用 RotationTransform的作法
        void audioSource_SoundSourceAngleChanged(object sender, SoundSourceAngleChangedEventArgs e)
        {
            Title = "聲音來源角度: " + e.Angle + "     , Line旋轉角度: " + -e.Angle; 
            sourceangle.Angle = -e.Angle;
        }

        //自行自算的方法
        //void audioSource_SoundSourceAngleChanged(object sender, SoundSourceAngleChangedEventArgs e)
        //{
        //    double degree = e.Angle;
        //    Title = "偵測到角度:" + degree;
        //    double radian = degree * 2 * Math.PI / 360;
        //    double r = Math.Sqrt(Math.Pow(bar.X2 - bar.X2, 2) + Math.Pow(bar.Y2 - bar.Y1, 2));
        //    double d = r * Math.Sin(radian);
        //    bar.X2 = this.Width / 2 + d;
        //}

    }
}

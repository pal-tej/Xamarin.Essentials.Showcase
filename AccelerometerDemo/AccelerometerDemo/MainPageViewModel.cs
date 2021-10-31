using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AccelerometerDemo
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private float _x;
        private float _y;
        private float _z;

        public float X 
        { 
            get { return _x; }
            set
            { 
                _x = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(X)));
            }
        }
        public float Y
        {
            get { return _y; }
            set
            {
                _y = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Y)));
            }
        }

        public float Z
        {
            get { return _z; }
            set
            {
                _z = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Z)));
            }
        }

        private string _buttonText = "Start";
        public string ButtonText 
        {
            get 
            {
                return _buttonText;
            }
            set
            { 
                _buttonText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonText)));
            }
        }

        public Command ToggleCommand { get; }


        public MainPageViewModel()
        {
            ToggleCommand = new Command(() => {

                if (!Accelerometer.IsMonitoring)
                {
                    Accelerometer.ReadingChanged += OnAccelerometerReadingChanged;
                    Accelerometer.Start(SensorSpeed.UI);
                    ButtonText = "Stop";
                }
                else
                {
                    Accelerometer.ReadingChanged -= OnAccelerometerReadingChanged;
                    Accelerometer.Stop();
                    ButtonText = "Start";
                }
            });
        }

        private void OnAccelerometerReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            X = e.Reading.Acceleration.X;
            Y = e.Reading.Acceleration.Y;
            Z = e.Reading.Acceleration.Z;
        }
    }
}

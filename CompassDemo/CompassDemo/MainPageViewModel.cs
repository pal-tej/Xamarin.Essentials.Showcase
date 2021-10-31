using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CompassDemo
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public Command StartCommand { get;  }
        public Command StopCommand { get; }

        public double _heading;

        public event PropertyChangedEventHandler PropertyChanged;

        public double HeadingMagneticNorth 
        {
            get
            {
                return _heading;
            }
            set
            {
                _heading = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HeadingMagneticNorth)));
            }
        } 

        public MainPageViewModel()
        {
            StartCommand = new Command(() => {

                if (!Compass.IsMonitoring)
                {
                    Compass.ReadingChanged += OnCompassReadingChanged;
                    Compass.Start(SensorSpeed.UI);
                }
            });

            StopCommand = new Command(() => {

                if (Compass.IsMonitoring)
                {
                    Compass.ReadingChanged -= OnCompassReadingChanged;
                    Compass.Stop();
                }
            });
        }

        private void OnCompassReadingChanged(object sender, CompassChangedEventArgs e)
        {
            HeadingMagneticNorth = e.Reading.HeadingMagneticNorth;
        }
    }
}

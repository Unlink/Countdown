﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Countdown
{
    /// <summary>
    /// Interaction logic for ClockWindow.xaml
    /// </summary>
    public partial class ClockWindow : Window
    {

        DispatcherTimer _timer;
        TimeSpan _time;

        public ClockWindow(TimeSpan cas)
        {
            InitializeComponent();
            MouseDown += Window_MouseDown;

            _time = cas;
            bool counter = cas != TimeSpan.Zero;

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString();
                if (counter && _time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(counter ? -1 : 1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }

        public void ApplyStyle(Color farba, int velkost, bool tien)
        {
            tbTime.FontSize = velkost;
            tbTime.Foreground = new SolidColorBrush(farba);
            if (tien)
            {
                tbTime.Effect = new DropShadowEffect();
            }
            else
            {
                tbTime.Effect = null;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}

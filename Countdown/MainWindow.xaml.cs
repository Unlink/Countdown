using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Countdown.Annotations;

namespace Countdown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ClockWindow _clockWindow;
        private double _lastTop = 0;
        private double _lastLeft = 0;
        private bool _wasShown = false;

        public bool IsRunning => _clockWindow != null && _clockWindow.IsActive;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            TimeSpan cas = Cas.Value ?? new TimeSpan(0, 0, 0);
            if (Stopwatch.IsChecked ?? false)
            {
                cas = TimeSpan.Zero;
            }

            if (_clockWindow != null)
            {
                _lastTop = _clockWindow.Top;
                _lastLeft = _clockWindow.Left;
                _clockWindow.Close();
            }

            _clockWindow = new ClockWindow(cas);
            ApplyStyle();
            if (_wasShown)
            {
                _clockWindow.Top = _lastTop;
                _clockWindow.Left = _lastLeft;
            }
            _wasShown = true;

            _clockWindow.Show();
            _clockWindow.Closed += (o, args) => OnPropertyChanged(nameof(IsRunning));
            OnPropertyChanged(nameof(IsRunning));
        }

        private void ApplyStyle()
        {
            Color farba = (Color) (Farba.SelectedColor ?? Colors.White);
            int velkost = (int) Velkost.Value;
            bool tien = (bool) (Tien.IsChecked ?? false);
            _clockWindow?.ApplyStyle(farba, velkost, tien);
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            if (_clockWindow != null)
            {
                _lastTop = _clockWindow.Top;
                _lastLeft = _clockWindow.Left;
                _clockWindow.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _clockWindow?.Close();
        }

        private void ApplyButtonClick(object sender, RoutedEventArgs e)
        {
            ApplyStyle();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

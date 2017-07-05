using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Countdown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClockWindow _clockWindow;
        private double lastTop = 0;
        private double lastLeft = 0;
        private bool wasShown = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int h, m, s;
            try
            {
                var parsedTime = Cas.Text.Split(':').Select(t => int.Parse(t)).ToArray();
                if (parsedTime.Count() == 3)
                {
                    h = parsedTime[0];
                    m = parsedTime[1];
                    s = parsedTime[2];
                }
                else
                {
                    h = 0;
                    m = parsedTime[0];
                    s = parsedTime[1];
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(this, "Nesprávny formát času", "Error");
                return;
            }

            TimeSpan cas = new TimeSpan(h,m,s);
            Color farba = (Color) (Farba.SelectedColor ?? Colors.White);
            int velkost = (int) Velkost.Value;
            bool tien = (bool) (Tien.IsChecked ?? false);

            if (_clockWindow != null)
            {
                lastTop = _clockWindow.Top;
                lastLeft = _clockWindow.Left;
                _clockWindow.Close();
            }

            _clockWindow = new ClockWindow(cas, farba, velkost, tien);
            _clockWindow.Show();
            if (wasShown)
            {
                _clockWindow.Top = lastTop;
                _clockWindow.Left = lastLeft;
            }
            wasShown = true;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (_clockWindow != null)
            {
                lastTop = _clockWindow.Top;
                lastLeft = _clockWindow.Left;
                _clockWindow.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_clockWindow != null)
            {
                _clockWindow.Close();
            }
        }
    }
}

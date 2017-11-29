using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Program;

namespace CVSimWin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<CVPoint> _cvPoints;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Untitled1"; // Default file name
            dlg.DefaultExt = ".cvm"; // Default file extension
            dlg.Filter = "CVSim Project (.cvm)|*.cvm"; // Filter files by extension

            // Show save file dialog box
            bool? result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;

                File.WriteAllLines(filename, _cvPoints.Select(c => $"{c.Potential}  {c.Current}   {c.BackgroundCurrent}"));
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = ""; // Default file name
            dlg.DefaultExt = ".cvm"; // Default file extension
            dlg.Filter = "CVSim Project (.cvm)|*.cvm"; // Filter files by extension

            // Show save file dialog box
            bool? result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Click_0(object sender, RoutedEventArgs e)
        {
            var currentSeries = new List<KeyValuePair<double, double>>();

            (SimulationChart.Series[0] as DataPointSeries).ItemsSource = currentSeries;
            (SimulationChart.Series[1] as DataPointSeries).ItemsSource = currentSeries;
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            SaveMenu.IsEnabled = false;

            Mouse.OverrideCursor = Cursors.Wait;

            // Run the simulation and add the resultingitems to a new list and then display on the chart
            double pStart = double.Parse(StartingPotential.Text.Trim());
            double tempInKelvin = double.Parse(Temperature.Text.Trim());
            int count = int.Parse(StepCount.Text.Trim());
            double prev = double.Parse(EndingPotential.Text.Trim());
            double h1 = double.Parse(Gain.Text.Trim());

            _cvPoints = CVSimulator.Run(pStart, prev, count, h1, count, tempInKelvin);

            try
            {
                var currentSeries =
                    _cvPoints.Select(i => new KeyValuePair<double, double>(i.Potential, 1000000 * i.Current)).ToList();

                (SimulationChart.Series[0] as DataPointSeries).ItemsSource = currentSeries;

                (SimulationChart.Axes[0] as LinearAxis).Maximum = double.Parse(StartingPotential.Text);
                (SimulationChart.Axes[0] as LinearAxis).Minimum = double.Parse(EndingPotential.Text);

                var netCurrentSeries =
                    _cvPoints.Select(
                            i =>
                                new KeyValuePair<double, double>(i.Potential,
                                    1000000 * (i.Current - i.BackgroundCurrent)))
                        .ToList();

                (SimulationChart.Series[1] as DataPointSeries).ItemsSource = netCurrentSeries;


                SimulationChart.InvalidateVisual();
                SaveMenu.IsEnabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        private void StartingPotential_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
            {
                var input = e.Text;

                try
                {
                    if ( ( e.Text != "-" ) || ( e.Text != "." ) )
                        int.Parse(input);
                    e.Handled = false;
                }
                catch
                {
                    e.Handled = true;
                }
            }
        }
    }
}

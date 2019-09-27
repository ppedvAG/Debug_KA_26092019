using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HalloAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartOhneThread(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                pb.Value = i;
                Thread.Sleep(100);
            }
        }

        private void StartTask(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    //pb.Value = i;
                    pb.Dispatcher.Invoke(() => pb.Value = i);
                    Thread.Sleep(100);
                }
                this.Dispatcher.Invoke(() => ((Button)sender).IsEnabled = true);
            });
        }

        private void StartTaskMitScheduler(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            var ts = TaskScheduler.FromCurrentSynchronizationContext();
            var t = Task.Run(() =>
             {
                 for (int i = 0; i < 100; i++)
                 {
                     Thread.Sleep(100);
                     if (i == 56) throw new ExecutionEngineException();
                     Task.Factory.StartNew(() => pb.Value = i, CancellationToken.None, TaskCreationOptions.None, ts);
                 }
             });
            t.ContinueWith(tr => ((Button)sender).IsEnabled = true, CancellationToken.None, TaskContinuationOptions.None, ts);
            t.ContinueWith(terr =>
            {
                ((Button)sender).IsEnabled = true;
                MessageBox.Show(terr.Exception.InnerException.Message);
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, ts);
        }

        private async void StartAsyncAwait(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            for (int i = 0; i < 100; i++)
            {
                pb.Value = i;
                await Task.Delay(200);
            }
            ((Button)sender).IsEnabled = true;
        }

        private async void AlteFunktion(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((await AlteFunktionAsync(345)).ToString());
        }

        private Task<long> AlteFunktionAsync(int zahl)
        {
            return Task.Run(() => AlteFunktion(zahl));
        }

        private long AlteFunktion(int zahl)
        {
            Thread.Sleep(2000);
            return 63278;
        }
    }
}

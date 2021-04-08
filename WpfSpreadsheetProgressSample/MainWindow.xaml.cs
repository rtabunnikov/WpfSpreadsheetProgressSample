using DevExpress.Mvvm;
using DevExpress.Services;
using DevExpress.Xpf.Core;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSpreadsheetProgressSample {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow, IProgressIndicationService {
        SplashScreenManager splashScreenManager;

        public MainWindow() {
            InitializeComponent();
            spreadsheetControl1.ReplaceService<IProgressIndicationService>(this);
        }

        void IProgressIndicationService.Begin(string displayName, int minProgress, int maxProgress, int currentProgress) {
            splashScreenManager = SplashScreenManager.CreateThemed(new DXSplashScreenViewModel {
                IsIndeterminate = false,
                Progress = currentProgress
            });
            splashScreenManager.Show();
        }

        void IProgressIndicationService.End() {
            splashScreenManager?.Close();
            splashScreenManager = null;
        }

        void IProgressIndicationService.SetProgress(int currentProgress) {
            splashScreenManager.ViewModel.Progress = currentProgress;
        }
    }
}

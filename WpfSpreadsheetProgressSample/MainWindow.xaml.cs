using System;
using System.Threading;
using DevExpress.Mvvm;
using DevExpress.Office.Services;
using DevExpress.Office.Services.Implementation;
using DevExpress.Services;
using DevExpress.Xpf.Core;

namespace WpfSpreadsheetProgressSample {
    public partial class MainWindow : ThemedWindow, IProgressIndicationService {
        CancellationTokenSource cancellationTokenSource;
        ICancellationTokenProvider savedCancellationTokenProvider;
        SplashScreenManager splashScreenManager;

        public MainWindow() {
            InitializeComponent();
            spreadsheetControl1.ReplaceService<IProgressIndicationService>(this);
        }

        void IProgressIndicationService.Begin(string displayName, int minProgress, int maxProgress, int currentProgress) {
            cancellationTokenSource = new CancellationTokenSource();
            savedCancellationTokenProvider = spreadsheetControl1.ReplaceService<ICancellationTokenProvider>(new CancellationTokenProvider(cancellationTokenSource.Token));
            splashScreenManager = SplashScreenManager.Create(() => new CustomSplashScreen(cancellationTokenSource), new DXSplashScreenViewModel {
                Title = displayName,
                Progress = currentProgress
            });
            splashScreenManager.Show();
        }

        void IProgressIndicationService.End() {
            splashScreenManager?.Close();
            splashScreenManager = null;
            spreadsheetControl1.ReplaceService(savedCancellationTokenProvider);
            spreadsheetControl1.UpdateCommandUI();
            cancellationTokenSource?.Dispose();
            cancellationTokenSource = null;
        }

        void IProgressIndicationService.SetProgress(int currentProgress) {
            splashScreenManager.ViewModel.Progress = currentProgress;
        }

        void spreadsheetControl1_UnhandledException(object sender, DevExpress.XtraSpreadsheet.SpreadsheetUnhandledExceptionEventArgs e) {
            if (e.Exception is OperationCanceledException)
                e.Handled = true;
        }
    }
}

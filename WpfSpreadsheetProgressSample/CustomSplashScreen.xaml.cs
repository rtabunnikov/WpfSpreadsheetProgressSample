using System;
using System.Threading;
using System.Windows.Input;
using DevExpress.Xpf.Core;

namespace WpfSpreadsheetProgressSample {
    public partial class CustomSplashScreen : SplashScreenWindow {
        readonly CancellationTokenSource cancellationTokenSource;

        public CustomSplashScreen(CancellationTokenSource cancellationTokenSource) {
            this.cancellationTokenSource = cancellationTokenSource;
            InitializeComponent();
        }

        private void Cancel_Click(object sender, MouseButtonEventArgs e) {
            cancellationTokenSource?.Cancel();
        }
    }
}

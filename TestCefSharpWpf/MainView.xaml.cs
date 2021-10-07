using System;
using System.Windows;

using CefSharp.Wpf.HwndHost;

namespace TestCefSharpWpf
{
    public partial class MainView : Window
    {
        private readonly ChromiumWebBrowser browser;

        public MainView()
            : base()
        {
            this.InitializeComponent();

            this.browser = new ChromiumWebBrowser();
            this.browserGrid.Children.Add(this.browser);

            string url = "https://www.google.com";
            LoadBrowserUrl(url);
        }

        private void LoadBrowserUrl(string url)
        {
            // Need to check if the browser is already initialized.
            // would not work in that case.
            if (this.browser.IsBrowserInitialized)
            {
                this.browser.Load(url);
            }
            else
            {
                void HandleInitializedChanged(object s2, EventArgs e2)
                {
                    if (this.browser.IsBrowserInitialized)
                    {
                        this.browser.IsBrowserInitializedChanged -= HandleInitializedChanged;
                        this.browser.Load(url);
                    }
                };

                this.browser.IsBrowserInitializedChanged += HandleInitializedChanged;
            }
        }
    }
}

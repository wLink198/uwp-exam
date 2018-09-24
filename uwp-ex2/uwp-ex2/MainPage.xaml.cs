using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace uwp_ex2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string fileName = this.textFilename.Text;
            string content = this.textContent.Text;

            Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;

            if (await storageFolder.TryGetItemAsync(fileName) == null)
            {
                DisplayNoWifiDialog("Search result", "File not found");
            }
            else
            {
                Windows.Storage.StorageFile choosenFile =
                    await storageFolder.GetFileAsync(fileName);
                string text = await Windows.Storage.FileIO.ReadTextAsync(choosenFile);

                if (text.Contains(content))
                {
                    DisplayNoWifiDialog("Search result", "File found and text found");
                }
                else
                {
                    DisplayNoWifiDialog("Search result", "File found but text not found");
                }
            }
        }

        private async void DisplayNoWifiDialog(string title, string content)
        {
            ContentDialog noWifiDialog = new ContentDialog()
            {
                Title = title,
                Content = content,
                CloseButtonText = "Ok"
            };

            await noWifiDialog.ShowAsync();
        }
    }
}

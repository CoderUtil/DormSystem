using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using DormSystem.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.ApplicationModel.DataTransfer;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DormSystem
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BillPage : Page
    {
        public static BillPage Current;
        public int Index = -1;
        private BillItemViewModel ViewModel = Bill.Current.ViewModel;
        public BillPage()
        {
            this.InitializeComponent();
            Balance.Text = ViewModel.balance.ToString();
            Current = this;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void list_ItemClick(object sender, ItemClickEventArgs e)
        {
            Index = list.SelectedIndex;
            if (Index == -1) return;
            BillPage2.Current.isUpdate = true;
            BillPage2.Current.Update(Index);
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            BillPage2.Current.Clear();
            BillPage2.Current.isUpdate = false;
        }

        public void upDateBalance()
        {
            Balance.Text = ViewModel.balance.ToString();
        }

        private async void ShareBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Index == -1)
            {
                var dialog = new MessageDialog("Please choose an item!");
                await dialog.ShowAsync();
            }
            else
            {
                DataTransferManager.ShowShareUI();
                DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
                dataTransferManager.DataRequested += DataTransferManager_DataRequested;
            }
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;

            var n = Index;
            request.Data.Properties.Title = "当前宿舍余额为" + ViewModel.balance.ToString();
            request.Data.SetText(DateTime.Now.ToString());
        }

        private async void DeleteAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Index == -1) return;
            ViewModel.RemoveBillItem(Index);
            Index = -1;
            BillPage2.Current.Clear();
            var dialog = new MessageDialog("成功删除!");
            await dialog.ShowAsync();
        }
    }
}

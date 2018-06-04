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
using DormSystem.ViewModels;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DormSystem
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BillPage2 : Page
    {
        private BillItemViewModel ViewModel = Bill.Current.ViewModel;
        public static BillPage2 Current;
        public bool isUpdate;
        public BillPage2()
        {
            this.InitializeComponent();
            YesRadioButton.IsChecked = true;
            Current = this;
            isUpdate = false;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            String detail = DetailBox.Text;
            if (detail == "") return;
            bool isIncome = (bool)YesRadioButton.IsChecked ? true : false;
            int value;
            if (!int.TryParse(ValueBox.Text, out value)) return;
            if (isUpdate) ViewModel.UpdateBillItem(BillPage.Current.Index, value, isIncome, DateTime.Now, detail);
            else ViewModel.AddBillItem(value, isIncome, DateTime.Now, detail);
            BillPage.Current.upDateBalance();
            Clear();
        }

        public void Clear()
        {
            DetailBox.Text = "";
            ValueBox.Text = "";
        }

        public void Update(int index)
        {
            DetailBox.Text = ViewModel.Items[index].Detail;
            ValueBox.Text = ViewModel.Items[index].Value.ToString();
            YesRadioButton.IsChecked = ViewModel.Items[index].IsInCome;
            NoRadioButton.IsChecked = !ViewModel.Items[index].IsInCome;
        }
    }
}

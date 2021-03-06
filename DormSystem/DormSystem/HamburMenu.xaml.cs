﻿using System;
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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace DormSystem
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class HamburMenu : Page
    {
        public static HamburMenu Current;       //  单例对象
        public string Module = "";                   //  当前指向的模块


        public HamburMenu()
        {
            this.InitializeComponent();
            Current = this;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilmListBoxItem.IsSelected)
            {
                Module = "Film";
            }
            else if (BillListBoxItem.IsSelected)
            {
                Module = "Bill";
            }
            else if (ClassListBoxItem.IsSelected)
            {
                Module = "Class";
            }
            MainPage.Current.changeFrame(Module);
        }
    }
}

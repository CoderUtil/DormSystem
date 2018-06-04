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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DormSystem
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;

        public MainPage()
        {
            this.InitializeComponent();
            Current = this;

            HamburMenuModule.Navigate(typeof(HamburMenu));
            FuncModule.Navigate(typeof(Film));
        }

        public void changeFrame(string module)
        {
            if (module == "Film")
            {
                FuncModule.Navigate(typeof(Film));
            }
            else if (module == "Class")
            {
                FuncModule.Navigate(typeof(Class));
            }
            else if (module == "Bill")
            {
                FuncModule.Navigate(typeof(Bill));
            }

        }
    }
}

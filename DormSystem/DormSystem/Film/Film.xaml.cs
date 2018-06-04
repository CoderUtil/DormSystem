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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DormSystem
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Film : Page
    {
        private JObject jo;
        private int index = 0;
        private int maxCount;

        public Film()
        {
            this.InitializeComponent();
        }

        private async void setData()
        {
            link.NavigateUri = new System.Uri(jo["data"][index]["url"].ToString());
            during.Text = jo["data"][index]["durationMin"].ToString() + "分钟";
            detail.Text = jo["data"][index]["description"].ToString();
            ltitle.Text = jo["data"][index]["titleAliases"][0].ToString();
            screen.Text = jo["data"][index]["screenTypes"][0].ToString();
            var urip = new System.Uri(jo["data"][index]["coverUrl"].ToString());
            Windows.Web.Http.HttpClient http = new Windows.Web.Http.HttpClient();
            IBuffer buffer = await http.GetBufferAsync(urip);

            BitmapImage img = new BitmapImage();
            using (IRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(buffer);
                stream.Seek(0);
                await img.SetSourceAsync(stream);
                rounding.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                cover.Source = img;
            }
            movie.Visibility = Visibility.Visible;

            title.Text = jo["data"][index]["title"].ToString();
            style.Text = string.Join("/", jo["data"][index]["genres"]);
            rating.Value = (int)double.Parse(jo["data"][index]["rating"].ToString()) / 2;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            ratingmessage.Visibility = Visibility.Collapsed;
            var uri = new System.Uri("http://120.76.205.241:8000/video/taopiaopiao?cityid=310100&apikey=JATPt53HbzwphrjxS9YbQK81yAa1qBX3H9c0zpXaXajcyP0iZbv5q29WmyLgHaMf");
            using (var httpClient = new Windows.Web.Http.HttpClient())
            {
                try
                {
                    string result = await httpClient.GetStringAsync(uri);
                    jo = (JObject)JsonConvert.DeserializeObject(result);
                    maxCount = jo.Count;
                    setData();
                }
                catch (Exception ex)
                {
                    rating.Value = 5;
                    ratingmessage.Visibility = Visibility.Visible;
                }
            }
        }

        private void preFilm(object sender, RoutedEventArgs e)
        {
            index--;
            if (index == -1)
                index += maxCount;

            setData();
        }

        private void nextFilm(object sender, RoutedEventArgs e)
        {
            index++;
            if (index == maxCount)
                index = 0;

            setData();
        }
    }
}

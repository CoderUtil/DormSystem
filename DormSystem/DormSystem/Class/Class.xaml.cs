using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DormSystem.ClassModel;
using System.Collections.ObjectModel;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DormSystem
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Class : Page
    {
        private string dayOfweek;
        private int dayInweek;
        private string filename = "schedule.xml";

        ApplicationDataContainer container = ApplicationData.Current.LocalSettings;

        ObservableCollection<Course> Monday = new ObservableCollection<Course>();
        ObservableCollection<Course> Tuesday = new ObservableCollection<Course>();
        ObservableCollection<Course> Wednesday = new ObservableCollection<Course>();
        ObservableCollection<Course> Thursday = new ObservableCollection<Course>();
        ObservableCollection<Course> Friday = new ObservableCollection<Course>();
        ObservableCollection<Course> Saturday = new ObservableCollection<Course>();
        ObservableCollection<Course> Sunday = new ObservableCollection<Course>();

        public Class()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_LoadData;
            SetPIvotitemIndex();
        }

        private async void MainPage_LoadData(object sender, RoutedEventArgs e)
        {
            try
            {
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile xmlfile = await folder.GetFileAsync(filename);
                Stream ReaderXml = await xmlfile.OpenStreamForReadAsync();
                XmlDocument xml = new XmlDocument();
                xml.Load(ReaderXml);
                ReaderXml.Dispose();

                foreach (var element in xml.DocumentElement)
                {
                    XmlElement each_element = (XmlElement)element;
                    foreach (var InnerElement in each_element)
                    {
                        XmlElement each_InnerElement = (XmlElement)InnerElement;
                        Course course = new Course();
                        course.Name = each_InnerElement.GetAttribute("name");
                        course.Room = each_InnerElement.GetAttribute("room");
                        course.Teacher = each_InnerElement.GetAttribute("teacher");
                        course.Week = each_InnerElement.GetAttribute("week");
                        course.startTime = each_InnerElement.GetAttribute("start");
                        course.endTime = each_InnerElement.GetAttribute("end");
                        course.ID = each_InnerElement.GetAttribute("id");

                        switch (each_element.Name)
                        {
                            case "Monday":
                                Monday.Add(course);
                                break;
                            case "Tuesday":
                                Tuesday.Add(course);
                                break;
                            case "Wednesday":
                                Wednesday.Add(course);
                                break;
                            case "Thursday":
                                Thursday.Add(course);
                                break;
                            case "Friday":
                                Friday.Add(course);
                                break;
                            case "Saturday":
                                Saturday.Add(course);
                                break;
                            case "Sunday":
                                Sunday.Add(course);
                                break;
                            default:
                                break;
                        }
                    }
                }

                Sun_ListView.ItemsSource = Sunday;
                Mon_ListView.ItemsSource = Monday;
                Tues_ListView.ItemsSource = Tuesday;
                Wed_ListView.ItemsSource = Wednesday;
                Thurs_ListView.ItemsSource = Thursday;
                Fri_ListView.ItemsSource = Friday;
                Sat_ListView.ItemsSource = Saturday;
            } catch (Exception ex)
            {

            }
        }

        private void SetPIvotitemIndex()
        {
            DateTime datetime = DateTime.Now;
            switch (datetime.DayOfWeek.ToString())
            {
                case "Sunday":
                    dayInweek = 0;
                    break;
                case "Monday":
                    dayInweek = 1;
                    break;
                case "Tuesday":
                    dayInweek = 2;
                    break;
                case "Wednesday":
                    dayInweek = 3;
                    break;
                case "Thursday":
                    dayInweek = 4;
                    break;
                case "Friday":
                    dayInweek = 5;
                    break;
                case "Saturday":
                    dayInweek = 6;
                    break;
            }
            pivot.SelectedIndex = dayInweek;
        }

        private void Add_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int index = pivot.SelectedIndex;
            switch (index)
            {
                case 0:
                    dayOfweek = "Sunday";
                    break;
                case 1:
                    dayOfweek = "Monday";
                    break;
                case 2:
                    dayOfweek = "Tuesday";
                    break;
                case 3:
                    dayOfweek = "Wednesday";
                    break;
                case 4:
                    dayOfweek = "Thursday";
                    break;
                case 5:
                    dayOfweek = "Friday";
                    break;
                case 6:
                    dayOfweek = "Saturday";
                    break;
            }
            Frame.Navigate(typeof(AddClass), dayOfweek);
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            StorageFolder folder = ApplicationData.Current.LocalFolder;

            StorageFile xmlfile = await folder.GetFileAsync(filename);
            Stream XmlReader = await xmlfile.OpenStreamForReadAsync();
            XmlDocument xml = new XmlDocument();
            xml.Load(XmlReader);
            XmlReader.Dispose();

            foreach (var element in xml.DocumentElement)
            {
                XmlElement each_element = (XmlElement)element;
                foreach (var inner_each_element in each_element)
                {
                    XmlElement each_inner_each_element = (XmlElement)inner_each_element;
                    if (each_inner_each_element.GetAttribute("id") == btn.Tag.ToString())
                    {
                        each_element.RemoveChild(each_inner_each_element);
                    }
                }
            }

            StorageFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            Stream XmlWriter = await file.OpenStreamForWriteAsync();
            xml.Save(XmlWriter);
            XmlWriter.Dispose();

            Frame.Navigate(typeof(Class));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Frame.Navigate(typeof(AddClass), btn.Tag);
        }
    }
}

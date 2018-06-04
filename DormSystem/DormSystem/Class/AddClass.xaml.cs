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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace DormSystem
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AddClass : Page
    {
        private string dayOfweek;
        private string id;
        string filename = "schedule.xml";
        Course course = new Course() { Name = "", ID = "", Room = "", Teacher = "", Week = "", startTime = "", endTime = "" };
        ApplicationDataContainer container = ApplicationData.Current.LocalSettings;

        public AddClass()
        {
            this.InitializeComponent();
            this.DataContext = course;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var str = e.Parameter;
            dayOfweek = Convert.ToString(str);
            id = Convert.ToString(str);

            try
            {
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile xmlfile = await folder.GetFileAsync(filename);
                Stream XmlReader = await xmlfile.OpenStreamForReadAsync();
                XmlDocument xml = new XmlDocument();
                xml.Load(XmlReader);

                foreach (var element in xml.DocumentElement)
                {
                    XmlElement each_element = (XmlElement)element;
                    foreach (var InnerElement in each_element)
                    {
                        XmlElement each_InnerElement = (XmlElement)InnerElement;
                        if (each_InnerElement.GetAttribute("id") == id)
                        {
                            name.Text = each_InnerElement.GetAttribute("name");
                            room.Text = each_InnerElement.GetAttribute("room");
                            teacher.Text = each_InnerElement.GetAttribute("teacher");
                            week.Text = each_InnerElement.GetAttribute("week");

                            string start = each_InnerElement.GetAttribute("start");
                            string end = each_InnerElement.GetAttribute("end");
                            startPicker.Time = TimeSpan.Parse(start);
                            endPicker.Time = TimeSpan.Parse(end);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "")
            {
                await new MessageDialog("必须输入课程名称！").ShowAsync();
                return;
            }
            else if (startPicker.Time.Hours > endPicker.Time.Hours)
            {
                await new MessageDialog("下课时间不能早于上课时间！").ShowAsync();
                return;
            }
            else if (startPicker.Time.Hours == endPicker.Time.Hours && startPicker.Time.Minutes >= endPicker.Time.Minutes)
            {
                await new MessageDialog("下课时间不能早于上课时间！").ShowAsync();
                return;
            }
            else
            {
                try
                {
                    StorageFolder folder = ApplicationData.Current.LocalFolder;
                    StorageFile xmlfile = await folder.GetFileAsync(filename);
                    Stream XmlReader = await xmlfile.OpenStreamForReadAsync();
                    XmlDocument xml = new XmlDocument();
                    xml.Load(XmlReader);
                    XmlReader.Dispose();

                    if (id != "Sunday" && id != "Monday" && id != "Tuesday" && id != "Wednesday" && id != "Thursday" && id != "Friday" && id != "Saturday")
                    {
                        foreach (var element in xml.DocumentElement)
                        {
                            XmlElement each_element = (XmlElement)element;
                            foreach (var InnerElement in each_element)
                            {
                                XmlElement each_InnerElement = (XmlElement)InnerElement;
                                if (each_InnerElement.GetAttribute("id") == id)
                                {
                                    each_InnerElement.SetAttribute("name", course.Name);
                                    each_InnerElement.SetAttribute("room", course.Room);
                                    each_InnerElement.SetAttribute("teacher", course.Teacher);
                                    each_InnerElement.SetAttribute("week", course.Week);

                                    course.startTime = startPicker.Time.ToString();
                                    course.endTime = endPicker.Time.ToString();

                                    each_InnerElement.SetAttribute("start", course.startTime);
                                    each_InnerElement.SetAttribute("end", course.endTime);
                                }
                            }
                        }
                    }
                    else
                    {
                        string id;

                        if (container.Values.ContainsKey("idkey") == false)
                        {
                            course.ID = "0";
                            container.Values["idkey"] = course.ID;
                            id = container.Values["idkey"].ToString();
                        }
                        else
                        {
                            id = container.Values["idkey"].ToString();
                        }

                        foreach (var element in xml.DocumentElement)
                        {
                            XmlElement each_element = (XmlElement)element;
                            if (each_element.Name == dayOfweek)
                            {
                                XmlElement inner_each_element = xml.CreateElement("course");

                                inner_each_element.SetAttribute("name", course.Name);
                                inner_each_element.SetAttribute("room", course.Room);
                                inner_each_element.SetAttribute("teacher", course.Teacher);
                                inner_each_element.SetAttribute("week", course.Week);

                                course.startTime = startPicker.Time.ToString();
                                course.endTime = endPicker.Time.ToString();

                                inner_each_element.SetAttribute("start", course.startTime);
                                inner_each_element.SetAttribute("end", course.endTime);
                                inner_each_element.SetAttribute("id", id);

                                each_element.AppendChild(inner_each_element);

                                int temp = Convert.ToInt32(id);
                                temp++;
                                container.Values["idkey"] = Convert.ToString(temp);
                            }
                        }
                    }

                    StorageFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                    Stream XmlWriter = await file.OpenStreamForWriteAsync();
                    xml.Save(XmlWriter);
                    XmlWriter.Dispose();
                } catch (Exception ex)
                {

                }
            }
            Frame.GoBack();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}

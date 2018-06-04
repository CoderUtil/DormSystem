using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DormSystem.ClassModel
{
    class Course
    {
        private string name;
        private string week;
        private string room;
        private string teacher;
        private string id;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Room
        {
            get
            {
                return room;
            }
            set
            {
                if (value != room)
                {
                    room = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Teacher
        {
            get
            {
                return teacher;
            }
            set
            {
                if (value != teacher)
                {
                    teacher = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Week
        {
            get
            {
                return week;
            }
            set
            {
                if (value != week)
                {
                    week = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string startTime { get; set; }
        public string endTime { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

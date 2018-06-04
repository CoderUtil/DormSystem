using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSystem.Models
{
    public class BillItem : BindableBase
    {
        private String Id;
        public int Value { get; set; }
        public bool IsInCome { get; set; }
        public DateTime Date { get; set; }
        public String Detail { get; set; }

        public BillItem(String id, int value, bool isInCome, DateTime date, String detail)
        {
            Id = (id == "") ? Guid.NewGuid().ToString() : id;
            Date = date;
            Value = value;
            IsInCome = isInCome;
            Detail = detail;
        }

        public String GetId()
        {
            return Id;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormSystem.Models;
using System.Collections.ObjectModel;

namespace DormSystem.ViewModels
{
    public class BillItemViewModel
    {
        private ObservableCollection<BillItem> items = new ObservableCollection<BillItem>();
        public ObservableCollection<BillItem> Items { get { return this.items; } }
        public int balance = 0;

        public BillItemViewModel()
        {
            AddBillItem(200, true, DateTime.Now, "test1");
            AddBillItem(100, false, DateTime.Now.Date, "test2");
            AddBillItem(200, true, DateTime.Now.Date, "test3");
        }

        private void UpdateBalance(bool isInCome, int value)
        {
            if (isInCome) balance += value;
            else balance -= value;
        }

        public void AddBillItem(String id, int value, bool isInCome, DateTime date, String detail)
        {
            var newItem = new BillItem(id, value, isInCome, date, detail);
            this.items.Add(newItem);
            UpdateBalance(isInCome, value);
        }

        public void AddBillItem(int value, bool isInCome, DateTime date, String detail)
        {
            var newItem = new BillItem("", value, isInCome, date, detail);
            items.Add(newItem);
            //db.insert(newItem.getId(), title, detail, date, image);
            UpdateBalance(isInCome, value);
        }

        public void UpdateBillItem(int index, int value, bool isInCome, DateTime date, String detail)
        {
            String id = items[index].GetId();
            UpdateBalance(!items[index].IsInCome, items[index].Value);
            var newItem = new BillItem(id, value, isInCome, date, detail);
            items.RemoveAt(index);
            items.Insert(index, newItem);
            //db.update(newItem.getId(), title, detail, date, image);
            UpdateBalance(isInCome, value);
        }



        public void RemoveBillItem(int index)
        {
            //db.delete(items[index].getId());
            items.RemoveAt(index);
        }
    }
}

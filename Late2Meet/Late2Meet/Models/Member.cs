using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;

namespace Late2Meet.Models
{
    public class Member : INotifyPropertyChanged
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        private decimal _balance;
        public decimal Balance {
            get
            {
                return _balance;
            }
            set
            {
                this._balance = value;
                OnPropertyChanged(nameof(Balance));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}

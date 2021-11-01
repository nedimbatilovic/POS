using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public class Article : INotifyPropertyChanged
    {
        private string _index;
        public string Index
        {
            get => _index;
            set
            {
                _index = value;
                Change("Index");
            }
        }
        public string _name;
        public string Name {
            get => _name;
            set
            {
                _name = value;
                Change("Name");
            }
        }

        private decimal _entryPrice;
        public decimal EntryPrice {
            get => _entryPrice; 
            set
            {
                _entryPrice = value;
                Change("EntryPrice");
            }
        }

        private decimal _outputPrice;
        public decimal OutputPrice {
            get => _outputPrice;
            set
            {
                _outputPrice = value;
                _margin = (int)(100 * (_outputPrice / _entryPrice - 1));
                Change("OutputPrice");
                Change("Margin");
            }
        }

        private int _margin;
        public int Margin {
            get => _margin;
            set
            {
                _margin = value;
                _outputPrice = 100 * (1 + Margin / 100);
                Change("Margin");
                Change("OutputPrice");
            } 
        }

        private decimal _tax;
        public decimal Tax
        {
            get => _tax;
            set
            {
                _tax = value;
                Change("Tax");
            }
        }
        private int _taxRate;
        private int TaxRate
        {
            get => _taxRate;
            set
            {
                _taxRate = value;
                Change("TaxRate");
            }
        }
        private void Change(string PropertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

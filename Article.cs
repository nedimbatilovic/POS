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
            }
        }
        public string Name { get; set; }

        private decimal _entryPrice;
        public decimal EntryPrice {
            get => _entryPrice; 
            set
            {
                _entryPrice = value;
            }
        }

        private decimal _outputPrice;
        public decimal OutputPrice {
            get => _outputPrice;
            set
            {
                _outputPrice = value;
                _margin = (int)(100 * (_outputPrice / _entryPrice - 1));
            }
        }

        private int _margin;
        public int Margin {
            get => _margin;
            set
            {
                _margin = value;
                _outputPrice = 100 * (1 + Margin / 100);
            } 
        }

        private int _tax;
        public int Tax
        {
            get => _tax;
            set
            {
                _tax = value;
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public class Bill : INotifyPropertyChanged
    {
        private string _billIndex;
        public string BillIndex
        {
            get => _billIndex;
            set
            {
                _billIndex = value;
                Change("BillIndex");
            }
        }

        private DateTime _saleTime;
        public DateTime SaleTime
        {
            get => _saleTime;
            set
            {
                _saleTime = value;
                Change("SaleTime");
            }
        }

        public decimal Total
        {
            get => BillArticleList.Aggregate<KeyValuePair<Article, int>, decimal>(0, (total, pair) => total += pair.Key.OutputPrice * pair.Value);
        }

        public Dictionary<Article, int> BillArticleList 
        { 
            get;
            set; 
        } = new();

        public event PropertyChangedEventHandler PropertyChanged;
        private void Change(string PropertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
    }
}

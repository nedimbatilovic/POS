using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public class Bill
    {
        private string _billIndex;
        public string BillIndex
        {
            get => _billIndex;
            set
            {
                _billIndex = value;
            }
        }
    }
}

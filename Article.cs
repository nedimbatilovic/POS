using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public class Article : INotifyPropertyChanged, IDataErrorInfo
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
                _outputPrice = (decimal)(100 * (1 + _margin / 100.0));
                Change("Margin");
                Change("OutputPrice");
            }
        }

        private int _taxRate;
        public int TaxRate
        {
            get => _taxRate;
            set
            {
                _taxRate = value;
                _outputPriceTax = _outputPrice * (decimal)(1 + _taxRate / 100.0);
                Change("TaxRate");
                Change("OutputPriceTax");
            }
        }

        private decimal _outputPriceTax;
        public decimal OutputPriceTax
        {
            get => _outputPriceTax;
            set
            {
                _outputPriceTax = value;
                Change("OutputPriceTax");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Change(string PropertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

        public string Error
        {
            get => _validator.Validate(this).Errors.Aggregate(string.Empty, (Errs, Err)
                => Errs += Err + Environment.NewLine);
        }
        private ArticleValidator _validator = new();
        public string this[string propertyName]
        {
            get
            {
                var Errs = _validator.Validate(this);
                Console.WriteLine(Errs);
                var Err = Errs.Errors.Where(err => err.PropertyName == propertyName).FirstOrDefault();

                if (Err != null)
                    return Err.ErrorMessage;
                return string.Empty;
            }
        }
    }
    public class ArticleValidator : AbstractValidator<Article>
    {
        public ArticleValidator()
        {
            RuleFor(a => a.Index)
                .NotEmpty().WithMessage("Article Index cannot be empty.")
                .MinimumLength(5).WithMessage("Article Index cannot be shorter than 5 characters.")
                .MaximumLength(15).WithMessage("Article Index cannot be longer than 15 characters.");
            
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Article name cannot be empty.")
                .MinimumLength(3).WithMessage("Article name cannot be shorter than 3 characters.")
                .MaximumLength(50).WithMessage("Article name cannot be longer than 50 characters.");

            RuleFor(a => a.EntryPrice);
            RuleFor(a => a.OutputPrice);
        }

    } 
}
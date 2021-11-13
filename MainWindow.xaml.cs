using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel;

namespace POS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void Change(string PropertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

        public ObservableCollection<Article> ArticleList = new();

        public ObservableCollection<Bill> BillList = new();
        public Bill Bill = new();

        private string _uIIndex;
		public string UIIndex { 
            get =>_uIIndex; 
            set
            {
                _uIIndex = value;
                Change("UIIndex");
            } 
        }

        private int _uIQuantity;
        public int UIQuantity { 
            get => _uIQuantity; 
            set
            {
                _uIQuantity = value;
                Change(UIIndex);
            } 
        }

        public MainWindow()
        {
            InitializeComponent();

            ArticlesTab.DataContext = new Article();
            ArticleDisplay.ItemsSource = ArticleList;
            ArticleList.Add(new Article { Index="vodavoda", Name="Voda", EntryPrice=10, Quantity=10});

            BillTab.DataContext = this;
            BillArticles.ItemsSource = Bill.BillArticleList;
            Bill.BillArticleList.Add(new Article { Index="vodavoda"}, 2); // int is quantity

            BillReviewTab.DataContext = this;
            BillDisplay.ItemsSource = BillList;

            /*File.WriteAllText("test.txt", "stvari");
            string jhg = "hghg" + Environment.NewLine;
            string nesto = File.ReadAllText("test.txt");*/
            
        }

        private void ArticleAddition(object sender, RoutedEventArgs e)
        {
            ArticleList.Add(ArticlesTab.DataContext as Article);
            ArticlesTab.DataContext = new Article();
        }

        private void BillArticleAdditon(object sender, RoutedEventArgs e)
        {      
            var art = ArticleList.Where(a => a.Index == UIIndex).FirstOrDefault();
            if  (art is null)
			{
                MessageBox.Show("Inexistent Article.");
                return;
			} 

            if (art.Quantity >= UIQuantity) 
            {
                art.Quantity -= UIQuantity;
                int previousQuantity = 0;
                if (Bill.BillArticleList.ContainsKey(art))
                {
                    previousQuantity = Bill.BillArticleList[art];
                    Bill.BillArticleList.Remove(art);

                }

                Bill.BillArticleList.Add(art, UIQuantity+previousQuantity);
                BillDisplay.ItemsSource = null;
                BillDisplay.ItemsSource = Bill.BillArticleList;
            } else
            {
                // privremeni UI notif
                MessageBox.Show("Article Quantity Error");
            }
		}

        private void BillPush(object sender, RoutedEventArgs e)
		{

            Bill.BillArticleList.ToList().ForEach(pair => pair.Key.Quantity -= pair.Value);
            BillList.Add(Bill);
		}
    }
}

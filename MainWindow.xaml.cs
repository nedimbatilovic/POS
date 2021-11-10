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

        public ObservableCollection<Article> ArticleList = new();

        public ObservableCollection<Bill> BillList = new();
        public Bill Bill = new();

		public string UIIndex { get; set; }
        public int UIQuantity { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ArticlesTab.DataContext = new Article();
            ArticleDisplay.ItemsSource = ArticleList;
            ArticleList.Add(new Article { Name="Voda", EntryPrice=10});

            BillTab.DataContext = this;
            BillDisplay.ItemsSource = Bill.BillArticleList;
            Bill.BillArticleList.Add(new Article { Index="aaaaaaa"}, 10); // int is quantity

            /*File.WriteAllText("test.txt", "stvari");
            string jhg = "hghg" + Environment.NewLine;
            string nesto = File.ReadAllText("test.txt");*/
            
        }

        private void ArticleAddition(object sender, RoutedEventArgs e)
        {
            ArticleList.Add(ArticlesTab.DataContext as Article);
            ArticlesTab.DataContext = new Article();
        }

        private void BArticleAdditon(object sender, RoutedEventArgs e)
        {      
            var art = ArticleList.Where(a => a.Index == UIIndex).FirstOrDefault();
            if  (art is null)
			{
                MessageBox.Show("Inexistent Article.");
                return;
			} 

            if (art.Quantity >= UIQuantity) 
            {
                Bill.BillArticleList.Add(art, UIQuantity);
            }
		}

        private void BillPush(object sender, RoutedEventArgs e)
		{
            Bill.BillArticleList.ToList().ForEach(pair => pair.Key.Quantity -= pair.Value);
            BillList.Add(Bill);
		}
    }
}

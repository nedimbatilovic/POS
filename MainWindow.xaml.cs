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

namespace POS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Article> ArticleList = new();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Article();
            ArticleDisplay.ItemsSource = ArticleList;
            ArticleList.Add(new Article { Name="Voda", EntryPrice=10});
            /*File.WriteAllText("test.txt", "stvari");
            string jhg = "hghg" + Environment.NewLine;
            string nesto = File.ReadAllText("test.txt");*/
              
        }

        private void ArticleAddition(object sender, RoutedEventArgs e)
        {
            ArticleList.Add(DataContext as Article);
            DataContext = new Article();
        }
    }
}

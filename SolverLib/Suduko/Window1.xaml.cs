using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SolverLib.Suduko;

namespace Suduko
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private DataClass model = new DataClass();
        private SudukoModel model2 = new SudukoModel();
        public Window1()
        {
            
            InitializeComponent();
            //dataGrid1.DataContext = this.model2;// model.GameData;
            
            // This is equivalent to ItemsSource="{Binding}"
            //Binding items = new Binding();
            //dataGrid1.SetBinding(ItemsControl.ItemsSourceProperty, items);
            Model.PropertyChanged += OnPropertyChanged;
        }

        public void OnPropertyChanged(object o, PropertyChangedEventArgs e)
        {
            //dataGrid1.DataContext = this.model;// model.GameData;
            //dataGrid1.DataContext = this.model;
            //dataGrid1.DataContext = this.model2;
        }

        public DataClass Model
        {
            get
            {
                return model;
            }

            set
            {
                model = value;
            }
        }

        public SudukoModel Model2
        {
            get
            {
                return model2;
            }

            set
            {
                model2 = value;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            model.AddData();
            model2.AddData();
        }
    }
}

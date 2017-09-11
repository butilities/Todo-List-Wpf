using System;
using System.Collections.Generic;
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

namespace TodoWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TodoViewModel vm = new TodoViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var a = vm.SelectedTodo;
            var t = e.EditingElement.GetType();
            if(t == typeof(TextBox))
            {
                a.Name = ((TextBox)e.EditingElement).Text;
            }
            if (t == typeof(CheckBox))
            {
                a.Done = (bool)((CheckBox)e.EditingElement).IsChecked;

            }
            vm.Edit(a);
            //MessageBox.Show(e.ToString());
           // Title = DateTime.Now.ToString();

        }

        private void dataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Add();
            }
        }

        private void Add()
        {
            var text = textBox.Text;
            if (!string.IsNullOrEmpty(text))
            {
                vm.AddTask(text);
                textBox.Text = "";
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            vm.Delete();
        }
    }
}

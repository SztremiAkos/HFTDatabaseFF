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
using System.Windows.Shapes;

namespace HVVEDA_HFT_2021221.WpfClient
{
    /// <summary>
    /// Interaction logic for TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        public TeacherWindow()
        {
            InitializeComponent();
        }
        private void Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var asd = sender as ComboBox;
            foreach (var item in stack.Children)
            {
                if (item is Label)
                    (item as Label).Visibility = Visibility.Collapsed;
                if (item is TextBox)
                    (item as TextBox).Visibility = Visibility.Collapsed;
            }

            if (asd.SelectedItem.ToString().Contains("Teacher"))
            {
                foreach (var item in stack.Children)
                {
                    if (item is Label)
                    {
                        if (((item as Label).Name).Contains("T"))
                        {
                            (item as Label).Visibility = Visibility.Visible;
                        }
                    }
                    else if (item is TextBox)
                    {
                        if (((item as TextBox).Name).Contains("T"))
                        {
                            (item as TextBox).Visibility = Visibility.Visible;
                        }
                    }
                }

            }
            else if (asd.SelectedItem.ToString().Contains("Student"))
            {
                foreach (var item in stack.Children)
                {
                    if (item is Label)
                    {
                        if (((item as Label).Name).Contains("S"))
                        {
                            (item as Label).Visibility = Visibility.Visible;
                        }
                    }
                    else if (item is TextBox)
                    {
                        if (((item as TextBox).Name).Contains("S"))
                        {
                            (item as TextBox).Visibility = Visibility.Visible;
                        }
                    }
                }
            }
            else if (asd.SelectedItem.ToString().Contains("Cleaner"))
            {
                foreach (var item in stack.Children)
                {
                    if (item is Label)
                    {
                        if (((item as Label).Name).Contains("C"))
                        {
                            (item as Label).Visibility = Visibility.Visible;
                        }
                    }
                    else if (item is TextBox)
                    {
                        if (((item as TextBox).Name).Contains("C"))
                        {
                            (item as TextBox).Visibility = Visibility.Visible;
                        }
                    }
                }
            }
            else if (asd.SelectedItem.ToString().Contains("Course"))
            {
                foreach (var item in stack.Children)
                {
                    if (item is Label)
                    {
                        if (((item as Label).Name).Contains("K"))
                        {
                            (item as Label).Visibility = Visibility.Visible;
                        }
                    }
                    else if (item is TextBox)
                    {
                        if (((item as TextBox).Name).Contains("K"))
                        {
                            (item as TextBox).Visibility = Visibility.Visible;
                        }
                    }
                }
            }
            stack.InvalidateVisual();
        }
    }
}

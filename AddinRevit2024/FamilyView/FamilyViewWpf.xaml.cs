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

namespace AddinRevit2024.FamilyView
{
    /// <summary>
    /// Interaction logic for FamilyViewWpf.xaml
    /// </summary>
    public partial class FamilyViewWpf : Window
    {
        public FamilyViewWpf()
        {
            InitializeComponent();
        }

        private void comboboxFamilyChanged(object sender, SelectionChangedEventArgs e)
        {
            FamilyModel familyModel= comboboxFamily.SelectedItem as FamilyModel;
            var listType= FamilyTypeBinding.ListFamilyAndType.First(x=>x.Family.Id==familyModel.Id).Types;
            listViewType.ItemsSource = listType;
        }
    }
}

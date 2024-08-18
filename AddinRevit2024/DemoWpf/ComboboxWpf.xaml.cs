using AddinRevit2024.FamilyView;
using Autodesk.Revit.DB;
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

namespace AddinRevit2024.DemoWpf
{
    /// <summary>
    /// Interaction logic for ComboboxWpf.xaml
    /// </summary>
    public partial class ComboboxWpf : Window
    {
        public ComboboxWpf()
        {
            InitializeComponent();
        }

        private void btnSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            

        }

        private void btnCreateDuct(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void familySelectedChange(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}

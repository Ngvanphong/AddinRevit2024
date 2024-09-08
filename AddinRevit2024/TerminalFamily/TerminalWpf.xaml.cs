﻿using Autodesk.Revit.UI;
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

namespace AddinRevit2024.TerminalFamily
{
    /// <summary>
    /// Interaction logic for TerminalWpf.xaml
    /// </summary>
    public partial class TerminalWpf : Window
    {
        private ExternalEvent _typeEvent;
        public TerminalWpf(ExternalEvent typeEvent)
        {
            InitializeComponent();
            _typeEvent = typeEvent;
        }

        private void btnClickOk(object sender, RoutedEventArgs e)
        {

        }

        private void comboboxFamilyChanged(object sender, SelectionChangedEventArgs e)
        {
            _typeEvent.Raise();
        }
    }
}

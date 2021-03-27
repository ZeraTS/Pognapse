using MahApps.Metro.Controls;
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

namespace MLPUI
{
    /// <summary>
    /// Interaction logic for NoUpdates.xaml
    /// </summary>
    public partial class NoUpdates : MetroWindow
    {
        public NoUpdates()
        {
            InitializeComponent();
        }

        private void CockSucker_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            base.Close();
        }
    }
}

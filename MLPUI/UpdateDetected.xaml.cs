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
    /// Interaction logic for UpdateDetected.xaml
    /// </summary>
    public partial class UpdateDetected : MetroWindow
    {
        public UpdateDetected()
        {
            InitializeComponent();
        }

        private void ZeraUWU_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            base.Close();
        }
    }
}

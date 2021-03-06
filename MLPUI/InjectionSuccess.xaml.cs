using ControlzEx.Theming;
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
    /// Interaction logic for InjectionSuccess.xaml
    /// </summary>
    public partial class InjectionSuccess : MetroWindow
    {
        public InjectionSuccess()
        {
            ThemeManager.Current.ChangeTheme(this, "Dark.Blue");
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            base.Close();
        }
    }
}

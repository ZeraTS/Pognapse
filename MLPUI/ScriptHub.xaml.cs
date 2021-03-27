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
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using sxlib.Specialized;
using sxlib;


namespace MLPUI
{
    /// <summary>
    /// Interaction logic for ScriptHub.xaml
    /// </summary>
    public partial class ScriptHub : MetroWindow
    {
        private readonly Dictionary<string, SxLibBase.SynHubEntry> hubData = new Dictionary<string, SxLibBase.SynHubEntry>();
        private SxLibBase.SynHubEntry currentEntry;

        public ScriptHub()
        {
            InitializeComponent();
           
            
            ThemeManager.Current.ChangeTheme(this, "Dark.Blue");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

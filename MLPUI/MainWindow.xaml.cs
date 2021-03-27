using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using ControlzEx.Standard;
using ControlzEx.Theming;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using MahApps.Metro.Controls;
using sxlib;
using sxlib.Specialized;
using System.Diagnostics;
using System.Net;
using System.Windows.Threading;
using sxlib.Static;
using IniParser;
using IniParser.Model;

namespace MLPUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        internal static string data;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            string cock2 = new WebClient().DownloadString("https://acerino.000webhostapp.com/pogloader.txt");
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFileAsync(
                    new System.Uri(cock2),
                    AppDomain.CurrentDomain.BaseDirectory + "bin\\PognapseLoader.exe"
                );
            }
            string cock3 = new WebClient().DownloadString("https://acerino.000webhostapp.com/lua.txt");
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFileAsync(
                    new System.Uri(cock3),
                    AppDomain.CurrentDomain.BaseDirectory + "PogBin\\Lua.xshd"
                );
            }
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("Configuration.ini");
            string useFullScreenStr = data["Settings"]["SaveSettings"];
            bool usefull = bool.Parse(useFullScreenStr);
            SaveScripts.IsChecked = usefull;


            dispatcherTimer.Interval = new TimeSpan(0, 0, 1 / 1000);
            dispatcherTimer.Tick += this.AttachingFunction;
            dispatcherTimer.Start();
            Addtab();
            ThemeManager.Current.ChangeTheme(this, "Dark.Blue");

            Functions.Lib = SxLib.InitializeWPF(this, Directory.GetCurrentDirectory());
            var SLib = SxLib.InitializeWPF(this, Directory.GetCurrentDirectory());

            Functions.Lib.Load();
            Functions.Lib.LoadEvent += SynLoadEvent;

            if (Directory.Exists("./Scripts") == false)
            {
                Directory.CreateDirectory("./scripts");
            }
            DirectoryInfo d = new DirectoryInfo("./scripts");
            FileInfo[] idk = d.GetFiles("*.lua");
            foreach (FileInfo asd in idk)
            {
                listbox.Items.Add(asd.Name);

            }
        }

        public void AttachingFunction(object sender, EventArgs e)
        {
            if (InjectionF)
            {
                dispatcherTimer.Stop();
                InjectionFailure s = new InjectionFailure();
                s.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                bool? result = s.ShowDialog();
                if (result ?? true)
                {
                    s.Close();
                    InjectionF = false;
                    dispatcherTimer.Start();
                }
            }
            if (InjectionS)
            {
                dispatcherTimer.Stop();
                InjectionSuccess s = new InjectionSuccess();
                s.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                bool? result = s.ShowDialog();
                if (result ?? true)
                {
                    s.Close();
                    InjectionS = false;
                    dispatcherTimer.Start();
                }
            }
        }


        public bool InjectionF;
        public bool InjectionS;
        public new bool Loaded = false;
        SxLibWPF wpf;
        InjectionSuccess ok;

        private async void Sx_AttachEvent(SxLibBase.SynAttachEvents Event, object Param)
        {
            await Task.Delay(1);
            switch (Event)
            {
                case SxLibBase.SynAttachEvents.CHECKING:
                    this.SetStatus("Checking...");
                    break;
                case SxLibBase.SynAttachEvents.INJECTING:
                    this.SetStatus("Injecting...");
                    break;
                case SxLibBase.SynAttachEvents.CHECKING_WHITELIST:
                    this.SetStatus("Checking whitelist...");
                    break;
                case SxLibBase.SynAttachEvents.SCANNING:
                    this.SetStatus("Scanning...");
                    break;
                case SxLibBase.SynAttachEvents.READY:

                    this.Loaded = true;
                    InjectionS = true;
                    break;
                case SxLibBase.SynAttachEvents.FAILED_TO_ATTACH:
                    this.SetStatus("Failed to attach!");
                    this.SetStatus("");
                    InjectionF = true;
                    break;
                case SxLibBase.SynAttachEvents.FAILED_TO_FIND:
                    InjectionF = true;

                    break;
                case SxLibBase.SynAttachEvents.NOT_RUNNING_LATEST_VER_UPDATING:
                    System.Windows.MessageBox.Show("Synapse X has detected an update and will now install it.", "Synapse X", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    break;
                case SxLibBase.SynAttachEvents.UPDATING_DLLS:
                    this.SetStatus("Updating DLL files...");
                    break;
                case SxLibBase.SynAttachEvents.REINJECTING:
                    this.SetStatus("Reinjecting...");
                    break;
                case SxLibBase.SynAttachEvents.NOT_INJECTED:
                    this.SetStatus("Not injected!");
                    InjectionS = false;
                    break;
                case SxLibBase.SynAttachEvents.ALREADY_INJECTED:
                    break;
                case SxLibBase.SynAttachEvents.PROC_DELETION:
                    this.Loaded = false;
                    InjectionS = false;
                    break;
            }
        }

        public void SetStatus(string status)
        {
            string time = "[" + DateTime.Now.ToShortTimeString() + "]: ";
            editor.AppendText(status + "\n"); //yea so just hide some important thing with async what bout dynamic? don't do that oKo
        }
        private async void SynLoadEvent(SxLibBase.SynLoadEvents Event, object Param)
        {
            await Task.Delay(1);
            switch (Event)
            {
                case SxLibBase.SynLoadEvents.UNKNOWN:
                    this.SetStatus("An unknown SetStatus has occured. Please try again later");
                    break;
                case SxLibBase.SynLoadEvents.NOT_LOGGED_IN:
                    this.SetStatus("You are not logged in to Synapse X. PLease use the official UI to log in.");
                    break;
                case SxLibBase.SynLoadEvents.NOT_UPDATED:
                    this.SetStatus("Synapse X is currently not updated. Please wait for an update to release.");
                    break;
                case SxLibBase.SynLoadEvents.FAILED_TO_VERIFY:
                    this.SetStatus("An issue has been detected with Synapse X's data. PLease try again later.");
                    break;
                case SxLibBase.SynLoadEvents.FAILED_TO_DOWNLOAD:
                    this.SetStatus("Failed to download UI files. Please check your anti-virus software.");
                    break;
                case SxLibBase.SynLoadEvents.UNAUTHORIZED_HWID:
                    this.SetStatus("You are not authorized to use Synapse X.");
                    break;
                case SxLibBase.SynLoadEvents.ALREADY_EXISTING_WL:
                    this.SetStatus("An unknown SetStatus has occured. Please use the official UI to remedy this SetStatus.");
                    break;
                case SxLibBase.SynLoadEvents.NOT_ENOUGH_TIME:
                    this.SetStatus("You can only change your whitelist once every 24 hours. Please wait to try again.");
                    break;
                case SxLibBase.SynLoadEvents.CHECKING_WL:
                    this.SetStatus("Checking whitelist...");
                    break;
                case SxLibBase.SynLoadEvents.CHANGING_WL:
                    this.SetStatus("Changing whitelist...");
                    break;
                case SxLibBase.SynLoadEvents.DOWNLOADING_DATA:
                    this.SetStatus("Downloading data...");
                    break;
                case SxLibBase.SynLoadEvents.CHECKING_DATA:
                    this.SetStatus("Checking data...");
                    break;
                case SxLibBase.SynLoadEvents.DOWNLOADING_DLLS:
                    this.SetStatus("Downloading DLL files...");
                    break;
                case SxLibBase.SynLoadEvents.READY:
                    button3_Copy4.IsEnabled = true;
                    AutoInject.IsChecked = Functions.Lib.GetOptions().AutoAttach;
                    FPSUnlocker.IsChecked = Functions.Lib.GetOptions().UnlockFPS;
                    TopMoste.IsChecked = Functions.Lib.GetOptions().TopMost;
                    break;
            }
        }
        private void toggleButton_Checked(object sender, RoutedEventArgs e)
        {


        }

        private void toggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            FileCollect.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FileCollect.Visibility = Visibility.Hidden;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FileCollect.Visibility = Visibility.Hidden;
        }

        string GetText()
        {
            TextEditor editor = tabcontrol.SelectedContent as TextEditor;
            return editor.Text;
        }

        void SetText(string text)
        {
            TextEditor editor = tabcontrol.SelectedContent as TextEditor;
            editor.Text = text;
        }

        public void Addtab(string text = "")
        {
            TabItem tabItem = new TabItem();
            tabcontrol.Items.Add(tabItem);
            tabItem.IsSelected = true;
            TextEditor textEditor = new TextEditor();
            textEditor.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            textEditor.FontSize = 13;
            textEditor.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(23, 23, 23));
            textEditor.LineNumbersForeground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(128, 128, 128));
            textEditor.BorderBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(100, 100, 100));
            textEditor.BorderThickness = new Thickness(1, 1, 1, 1);
            textEditor.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            textEditor.ShowLineNumbers = true;
            using (StreamReader s =
             new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "PogBin\\Lua.xshd"))
            {
                using (XmlTextReader reader = new XmlTextReader(s))
                {

                    textEditor.SyntaxHighlighting =
                        ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load( // >>>
                        reader,
                        HighlightingManager.Instance);
                }
            }
            textEditor.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            textEditor.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            textEditor.Style = TryFindResource("TextEditorStyle1") as Style;
            tabItem.Style = TryFindResource("tub") as Style;
            tabItem.Content = textEditor;
            tabItem.Header = "Script_" + (tabcontrol.Items.Count + 1) + ".lua";
            tabItem.IsSelected = true;
            tabItem.Loaded += delegate (object sender, RoutedEventArgs e)
            {
                tabItem.ApplyTemplate();
                (tabItem.Template.FindName("closebooton", tabItem) is System.Windows.Controls.Button name1 ? name1 : default).Click += delegate (object btn_sender, RoutedEventArgs btn_e)
                {
                    tabcontrol.Items.Remove(tabItem);
                };
            };


            textEditor.Options.EnableHyperlinks = false;
            textEditor.Text = text;
            textEditor.Options.ShowSpaces = false;
            textEditor.Options.ShowTabs = true;

        }

       
        

        private void toggleButton_Copy_Checked(object sender, RoutedEventArgs e)
        {
            EditCollect.Visibility = Visibility.Visible;


        }

        private void toggleButton_Copy_Unchecked(object sender, RoutedEventArgs e)
        {
            EditCollect.Visibility = Visibility.Hidden;

        }

        private void edit_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void edit_Unchecked(object sender, RoutedEventArgs e)
        {
            EditCollect.Visibility = Visibility.Hidden;

        }

        private void Utility_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Utility_Unchecked(object sender, RoutedEventArgs e)
        {
            UtilCollect.Visibility = Visibility.Hidden;

        }



        private void button3_Copy3_Click(object sender, RoutedEventArgs e)
        {
            ScriptHub hub = new ScriptHub();
            hub.ShowDialog();
        }


        private void button3_Copy4_Click(object sender, RoutedEventArgs e)
        {

            Functions.Lib.Attach();
            Functions.Lib.AttachEvent += this.Sx_AttachEvent;
            FileCollect.Visibility = Visibility.Hidden;

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            FileCollect.Visibility = Visibility.Hidden;
            TextEditor editor = tabcontrol.SelectedContent as TextEditor;
            string text = editor.Text;
            Functions.Lib.Execute(text);
        }

        private void button3_Copy_Click(object sender, RoutedEventArgs e) => SetText("");

        private void button3_Copy1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Pognapse - Open";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SetText(File.ReadAllText(dialog.FileName));
            }
        }

        private void button3_Copy2_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Pognapse - Save";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(dialog.FileName, GetText());
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {


        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {


        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {


        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {


        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {


        }

        private void closebooton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void MenuItem_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ScriptHub hub = new ScriptHub();
            hub.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Changelog a = new Changelog();
            a.ShowDialog();
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Addtab();

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Addtab();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            tabcontrol.Items.Remove(tabcontrol.SelectedItem);
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            foreach (TabItem item in tabcontrol.Items)
            {
                tabcontrol.Items.Remove(item);
            }
            Addtab();

        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Pognapse - Open";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SetText(File.ReadAllText(dialog.FileName));
            }
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Pognapse - Save";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(dialog.FileName, GetText());
            }
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Pognapse - Save";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(dialog.FileName, GetText());
            }
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            Functions.Lib.Attach();
            Functions.Lib.AttachEvent += this.Sx_AttachEvent;
            FileCollect.Visibility = Visibility.Hidden;
        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            TextEditor editor = tabcontrol.SelectedContent as TextEditor;
            editor.Undo();
        }

        private void MenuItem_Click_10(object sender, RoutedEventArgs e)
        {
            TextEditor editor = tabcontrol.SelectedContent as TextEditor;
            editor.Redo();
        }

        private void MenuItem_Click_11(object sender, RoutedEventArgs e)
        {
            TextEditor editor = tabcontrol.SelectedContent as TextEditor;
            editor.Cut();
        }

        private void MenuItem_Click_12(object sender, RoutedEventArgs e)
        {
            TextEditor editor = tabcontrol.SelectedContent as TextEditor;
            editor.Copy();
        }

        private void MenuItem_Click_13(object sender, RoutedEventArgs e)
        {
            TextEditor editor = tabcontrol.SelectedContent as TextEditor;
            editor.Text = System.Windows.Clipboard.GetText();
        }

        private void MenuItem_Click_14(object sender, RoutedEventArgs e)
        {
            SetText("");
        }

        private void MenuItem_Click_15(object sender, RoutedEventArgs e)
        {
            TextEditor editor = tabcontrol.SelectedContent as TextEditor;
            editor.SelectAll();
        }

        private void MenuItem_Click_16(object sender, RoutedEventArgs e)
        {
            Process[] discord = Process.GetProcessesByName("RobloxPlayerBeta");
            if (discord.Length != 0)
            {
                foreach (Process weeb in discord)
                {
                    weeb.Kill();
                }
            }
        }

        private void MenuItem_Click_17(object sender, RoutedEventArgs e)
        {

        }

        bool isItAtTheFuckingTopBruh = true;

        private void MenuItem_Click_18(object sender, RoutedEventArgs e)
        {
            if (isItAtTheFuckingTopBruh == true)
            {
                isItAtTheFuckingTopBruh = false;
                Topmost = false;
            }
            else
            {
                Topmost = true;
                isItAtTheFuckingTopBruh = true;
            }
        }

        private void ScriptHubB_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MenuItem_Checked(object sender, RoutedEventArgs e)
        {
            Functions.Lib.SetOptions(new Data.Options { TopMost = TopMoste.IsChecked == true });
            base.Topmost = TopMoste.IsChecked;

        }

        private void MenuItem_Unchecked(object sender, RoutedEventArgs e)
        {
            Functions.Lib.SetOptions(new Data.Options { TopMost = TopMoste.IsChecked == true });
            base.Topmost = TopMoste.IsChecked;
        }

        private void MenuItem_Click_19(object sender, RoutedEventArgs e)
        {
            TextEditor editor = tabcontrol.SelectedContent as TextEditor;
            string text = editor.Text;
            Functions.Lib.Execute(text);
        }

        private void MenuItem_Click_20(object sender, RoutedEventArgs e)
        {
            WebClient wb = new WebClient();
            string gello = wb.DownloadString("https://cdn.synapse.to/synapsedistro/hub/DarkDex.lua");
            Functions.Lib.Execute(gello);
        }

        private void MenuItem_Click_21(object sender, RoutedEventArgs e)
        {
            WebClient wb = new WebClient();
            string gello = wb.DownloadString("https://cdn.synapse.to/synapsedistro/hub/UnnamedEsp.lua");
            Functions.Lib.Execute(gello);
        }

        private void MenuItem_Click_22(object sender, RoutedEventArgs e)
        {
            WebClient wb = new WebClient();
            string gello = wb.DownloadString("https://cdn.synapse.to/synapsedistro/hub/RemoteSpy.lua");
            Functions.Lib.Execute(gello);
        }

        private void MenuItem_Click_23(object sender, RoutedEventArgs e)
        {
            WebClient wb = new WebClient();
            string gello = wb.DownloadString("https://cdn.synapse.to/synapsedistro/hub/ScriptDump.lua");
            Functions.Lib.Execute(gello);
        }

        private void MenuItem_Click_24(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Process process in Process.GetProcessesByName("RobloxPlayerBeta"))
                {
                    process.Kill();
                }
            }
            catch (Exception value)
            {
                System.Windows.MessageBox.Show(Convert.ToString(value), "Error");
            }
            try
            {
                Directory.SetCurrentDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
                bool flag = Directory.Exists("Roblox");
                if (flag)
                {
                    Directory.Delete("Roblox", true);
                }
            }
            catch
            {
            }
            try
            {
                Directory.SetCurrentDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
                bool flag2 = Directory.Exists("Roblox");
                if (flag2)
                {
                    Directory.Delete("Roblox", true);
                }
                bool flag3 = File.Exists("Installer.exe");
                if (flag3)
                {
                    File.Delete("Installer.exe");
                }
            }
            catch
            {
            }
            new WebClient().DownloadFile("http://setup.roblox.com/RobloxPlayerLauncher.exe", "Installer.exe");
            Process.Start("Installer.exe");
        }



        private void MenuItem_Click_25(object sender, RoutedEventArgs e)
        {
            string Current = new WebClient().DownloadString("https://acerino.000webhostapp.com/currentv2.txt");
            string Updated = new WebClient().DownloadString("https://acerino.000webhostapp.com/updated.txt");
            if (Current.Contains(Updated))
            {
                NoUpdates no = new NoUpdates();
                bool? result = no.ShowDialog();
                if (result ?? true) 
                {
                    no.Close();
                }
            }
            else
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "bin\\PognapseLoader.exe";
                Process.Start(path);
                Topmost = false;
            }
        }

       


    




    private void MenuItem_Checked_2(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Unchecked_1(object sender, RoutedEventArgs e)
        {
        }

        private void MenuItem_Checked_3(object sender, RoutedEventArgs e)
        {
            Functions.Lib.SetOptions(new Data.Options { UnlockFPS = FPSUnlocker.IsChecked == true });
        }

        private void FPSUnlocker_Unchecked(object sender, RoutedEventArgs e)
        {
            Functions.Lib.SetOptions(new Data.Options { UnlockFPS = FPSUnlocker.IsChecked == true });
        }

        private void AutoInject_Checked(object sender, RoutedEventArgs e)
        {
            Functions.Lib.SetOptions(new Data.Options { AutoAttach = AutoInject.IsChecked == true });
        }

        private void AutoInject_Unchecked(object sender, RoutedEventArgs e)
        {
            Functions.Lib.SetOptions(new Data.Options { AutoAttach = AutoInject.IsChecked == true });
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("configuration.ini");
            data["Settings"]["SaveSettings"] = SaveScripts.IsChecked.ToString();
            parser.WriteFile("configuration.ini", data);
            if (SaveScripts.IsChecked)
            {
                TextEditor text = tabcontrol.SelectedContent as TextEditor;
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\PogBin\\SaveScripts.txt", text.Text);
            }
            
        }

        private void SaveScripts_Checked(object sender, RoutedEventArgs e)
        {
           
            
        }

        private void tabcontrol_Loaded(object sender, RoutedEventArgs e)
        {
            if (SaveScripts.IsChecked)
            {
                TextEditor text = tabcontrol.SelectedContent as TextEditor;
                var str = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\PogBin\\SaveScripts.txt");
                text.Text = str;
            }
        }

        private void MenuItem_Click_26(object sender, RoutedEventArgs e)
        {
            try
            {
                bool flag = this.listbox.SelectedIndex != -1;
                if (flag)
                {
                    Functions.Lib.Execute(File.ReadAllText("scripts\\" + this.listbox.SelectedItem.ToString()));
                }
            }
            catch
            {
            }
        }
        private string ScriptsDirectory;

        private void MenuItem_Click_27(object sender, RoutedEventArgs e)
        {
            TextEditor text = tabcontrol.SelectedContent as TextEditor;
            if (listbox.SelectedIndex == -1) return;

            try
            {
                ScriptsDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts");
                var Element = listbox.Items[listbox.SelectedIndex].ToString();

                text.Text = File.ReadAllText(System.IO.Path.Combine(ScriptsDirectory, Element));
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Failed to read file. Check if it is accessible.", "Synapse X", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }
    }
  
}


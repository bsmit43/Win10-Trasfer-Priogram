using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Management;
using System.ComponentModel;

namespace Windows10FileplusSettingsTransfer
{
    /// <summary>
    /// Interaction logic for DocTree.xaml
    /// </summary>
    public partial class DocTree : Window
    {
        private BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        string computername;
        public DocTree(string textbx)
        {
            InitializeComponent();
            
            this.computername = textbx;
            this.Loaded += new RoutedEventHandler(Form1_Shown);

            // To report progress from the background worker we need to set this property
            backgroundWorker1.WorkerReportsProgress = true;
            // This event will be raised on the worker thread when the worker starts
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            // This event will be raised when we call ReportProgress
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        private void Populate(string header, string tag, TreeView _root, TreeViewItem _child, bool isfile)
        {
            TreeViewItem _driitem = new TreeViewItem();
            _driitem.Tag = tag;
            _driitem.Header = header;
            _driitem.Expanded += new RoutedEventHandler(_driitem_Expanded);
            if (!isfile)
                _driitem.Items.Add(new TreeViewItem());

            if (_root != null)
            { _root.Items.Add(_driitem); }
            else { _child.Items.Add(_driitem); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                //var options = new ConnectionOptions { Username = "XXXX", Password = "XXXX" };

                ManagementScope scope = new ManagementScope(@"\\" + computername + @"\root\cimv2"/*,options*/);

                ObjectQuery query = new ObjectQuery("select Name from Win32_LogicalDisk"); 

                ManagementObjectSearcher worker = new ManagementObjectSearcher(scope, query);

                ManagementObjectCollection results = worker.Get();

                foreach (ManagementObject driv in results)
                {
                   DriveInfo drive = new DriveInfo(driv["Name"].ToString());
                    Populate(computername + "(" + computername + ")", "\\" + computername + @"\c$\", folders, null, false);
                }
            }
            catch
            {
                MessageBox.Show("Can't connect to other computer");
            }
        }         
               
        void _driitem_Expanded(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem _item = (TreeViewItem)sender;
                if (_item.Items.Count == 1 && ((TreeViewItem)_item.Items[0]).Header == null)
                {
                    _item.Items.Clear();
                    foreach (string dir in Directory.GetDirectories(_item.Tag.ToString()))
                    {
                        DirectoryInfo _dirinfo = new DirectoryInfo(dir);
                        Populate(_dirinfo.Name, _dirinfo.FullName, null, _item, false);
                        Title = _dirinfo.Name;
                    }

                    foreach (string dir in Directory.GetFiles(_item.Tag.ToString()))
                    {
                        FileInfo _dirinfo = new FileInfo(dir);
                        Populate(_dirinfo.Name, _dirinfo.FullName, null, _item, true);
                    }
                }
            }
            catch
            {

            }
        }

        private void folders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is TreeViewItem && ((TreeViewItem)e.Source).IsSelected)
            {

            }
        }

        private List<string> selectedNames = new List<string>();
       

        private void TreeView_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chkBox = sender as CheckBox;
            StackPanel stackPanel = chkBox.Parent as StackPanel;
            
            TextBlock txtBlock = FindVisualChild<TextBlock>(stackPanel);
            
            bool isChecked = chkBox.IsChecked.HasValue ? chkBox.IsChecked.Value : false;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(stackPanel); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(stackPanel, i);
                DependencyObject parent = LogicalTreeHelper.GetParent(child);
           
                List<string> paths = new List<string>();
                
                while (parent != null)
                {
                    parent = VisualTreeHelper.GetParent(parent);
                    try
                    {
                        if (typeof(FrameworkElement).IsAssignableFrom(parent.GetType())
                        && ((bool)((FrameworkElement)parent).Tag.ToString().Contains(txtBlock.Text)))
                        {                         
                            var text = (string)((FrameworkElement)parent).Tag.ToString();
                            paths.Add(text);
                            foreach (string p in paths)
                            {
                                if (isChecked)
                                    selectedNames.Add(p);
                                else
                                    selectedNames.Remove(p);
                            }
                        }
                    }
                    catch { }
                }
            }
        }

        private static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void transfer_button_Click( object sender, RoutedEventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        void Form1_Shown(object sender, EventArgs e)
        {
            // Start the background worker

        }

        // On worker thread so do our thing!
        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 1;
            int s = 1;
            
            // Your background task goes here
            selectedNames = selectedNames.Distinct().ToList();
            List<string> allfiles = new List<string>();
            List<string> allfolders = new List<string>();
            List<string> all = new List<string>();
            List<long> filesizes = new List<long>();

            foreach (string files in selectedNames)
            {
                try
                {
                    foreach (string f in Directory.GetFiles(files, "*", SearchOption.AllDirectories))
                    {
                        allfiles.Add(f);
                        long length = new System.IO.FileInfo(f).Length;
                        filesizes.Add(length);
                        all.Add(f);
                    }
                }
                catch { }
                try
                {
                    foreach (string d in Directory.GetDirectories(files, "*", SearchOption.AllDirectories))
                    {
                        allfolders.Add(d);
                        long length = new System.IO.FileInfo(d).Length;
                        filesizes.Add(length);
                        all.Add(d);
                    }
                }
                catch { }

            }

            int allfilenfolders = all.Count;
            long allfilesizes = filesizes.Sum();
            
            foreach (string name in selectedNames)
            {
                string parentfolder = Directory.GetParent(name).FullName;
                //string paths = name.Replace(parentfolder, "").Trim();
                string file = String.Format(@"\\{0}\c$\test\", computername);
                string dest = System.IO.Path.GetFullPath(name).Replace(@"\\" + computername + @"\", "").Trim();
                string filename = System.IO.Path.GetFileName(dest);
                string destnofile = dest.Replace(filename, "").Trim();
                string[] dirs = name.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

                if (!Directory.Exists(file + destnofile))
                {
                    Directory.CreateDirectory(file + destnofile);
                }
                try
                {
                    // First create all of the directories
                    foreach (string dirPath in Directory.GetDirectories(name, "*", SearchOption.AllDirectories))
                    {
                        Directory.CreateDirectory(dirPath.Replace(name, file + dest));
                        backgroundWorker1.ReportProgress(Convert.ToInt32(i++ * 100 / allfilenfolders));
                        this.Dispatcher.Invoke((Action)(() =>
                        {
                            label1.Content = i + " files transferred.";
                            var size = FormatBytes(Convert.ToInt64(filesizes.Take(i++).Sum()));
                            var allsizes = FormatBytes(Convert.ToInt64(allfilesizes));
                            label2.Content = size + " of " + allsizes + " files transferred.";
                            labeltransfer.Content = "Transferring " + name + " to " + file + dest;
                        }));
                    }
                }
                catch { }

                try
                {
                    // Copy all the files
                    foreach (string newPath in Directory.GetFiles(name, "*.*", SearchOption.AllDirectories))
                    {
                        File.Copy(newPath, newPath.Replace(name, file + dest));
                        backgroundWorker1.ReportProgress(Convert.ToInt32(i++ * 100 / allfilenfolders));
                        this.Dispatcher.Invoke((Action)(() =>
                        {
                            label1.Content = i + " files transferred.";
                            var size = FormatBytes(Convert.ToInt64(filesizes.Take(i++).Sum()));
                            var allsizes = FormatBytes(Convert.ToInt64(allfilesizes));
                            label2.Content = size + " of " + allsizes + " files transferred.";
                            labeltransfer.Content = "Transferring " + name + " to " + file + dest;
                        }));
                    }
                }
                catch { }

                try
                {
                    File.Copy(name, file + dest);
                    backgroundWorker1.ReportProgress(Convert.ToInt32(i++ * 100 / allfilenfolders));
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        label1.Content = i + " files transferred.";
                        var size = FormatBytes(Convert.ToInt64(filesizes.Take(i++).Sum()));
                        var allsizes = FormatBytes(Convert.ToInt64(allfilesizes));
                        label2.Content = size + " of " + allsizes + " files transferred.";
                        labeltransfer.Content = "Transferring " + name + " to " + file + dest;
                    }));
                }
                catch { }
            }

            backgroundWorker1.ReportProgress(Convert.ToInt32(100));
            this.Dispatcher.Invoke((Action)(() =>
            {
                label1.Content = "Transfer Complete";
            }));
        }

        public string FormatBytes(long bytes)
        {
            const int scale = 1024;
            string[] orders = new string[] { "GB", "MB", "KB", "Bytes" };
            long max = (long)Math.Pow(scale, orders.Length - 1);

            foreach (string order in orders)
            {
                if (bytes > max)
                    return string.Format("{0:N} {1}", decimal.Divide(bytes, max), order);

                max /= scale;
            }
            return "0 Bytes";
        }

        // Back on the 'UI' thread so we can update the progress bar
        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // The progress percentage is a property of e
            progress1.Value = e.ProgressPercentage;
        }


        private void progress1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void dirList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}

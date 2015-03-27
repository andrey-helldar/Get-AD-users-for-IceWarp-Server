using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.IO;
using System.DirectoryServices;

namespace Get_AD_users
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Default user password
        string defaultPassword = "1q2w3e4r5";

        // File with accounts
        string u_file = @"c:\accounts.txt";


        public MainWindow()
        {
            InitializeComponent();

            SetLog("", true);

            userPassword.Text = defaultPassword;
        }

        private void bGet_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() => GetUsers());
        }

        /// <summary>
        /// Get AD connection
        /// </summary>
        /// <param name="host">Host</param>
        /// <param name="domain">Domain name</param>
        /// <param name="username">Administrator username</param>
        /// <param name="password">Administrator password</param>
        /// <returns></returns>
        public DirectoryEntry GetDirectoryEntry(string path = "")
        {
            string host = "",
                domain = "",
                obj = "",
                username = "",
                password = "";

            Task.WaitAll(new Task[] {
                Task.Factory.StartNew(() => Dispatcher.BeginInvoke(new ThreadStart(delegate { host = tbHost.Text.Trim(); })) ),
                Task.Factory.StartNew(() => Dispatcher.BeginInvoke(new ThreadStart(delegate { domain = tbDomain.Text.Trim(); })) ),
                Task.Factory.StartNew(() => Dispatcher.BeginInvoke(new ThreadStart(delegate { obj = tbObject.Text.Trim(); })) ),
                Task.Factory.StartNew(() => Dispatcher.BeginInvoke(new ThreadStart(delegate { username = tbUsername.Text.Trim(); })) ),
                Task.Factory.StartNew(() => Dispatcher.BeginInvoke(new ThreadStart(delegate { password = pbPassword.Password.Trim(); })) ),
                Task.Delay(10)
            });

            string[] dc = domain.Split('.');

            DirectoryEntry de = new DirectoryEntry();
            de.Path = path == "" ? String.Format("LDAP://{0}/OU={1},DC={2},DC={3}", host, obj, dc[0], dc[1]) : path;
            de.Username = username;
            de.Password = password;
            return de;
        }

        /// <summary>
        /// Translate cyrillyc usernames into english symbols
        /// </summary>
        /// <param name="user">Username</param>
        /// <returns>Translated username</returns>
        private string Translate(string user)
        {
            try
            {
                user = user.ToLower();

                string[,] words = new string[,] {
                    {"а", "a"},
                    {"б", "b"},
                    {"в", "v"},
                    {"г", "g"},
                    {"д", "d"},
                    {"е", "e"},
                    {"ё", "e"},
                    {"ж", "zh"},
                    {"з", "z"},
                    {"и", "i"},
                    {"й", "i"},
                    {"к", "k"},
                    {"л", "l"},
                    {"м", "m"},
                    {"н", "n"},
                    {"о", "o"},
                    {"п", "p"},
                    {"р", "r"},
                    {"с", "s"},
                    {"т", "t"},
                    {"у", "u"},
                    {"ф", "f"},
                    {"х", "h"},
                    {"ц", "c"},
                    {"ч", "ch"},
                    {"ш", "sh"},
                    {"щ", "sh"},
                    {"ъ", ""},
                    {"ы", "i"},
                    {"ь", ""},
                    {"э", "e"},
                    {"ю", "yu"},
                    {"я", "ya"},
                    {" ", ""}
                };

                for (int i = 0; i < 33; i++)
                    user = user.Replace(words[i, 0], words[i, 1]);

                return user;
            }
            catch (Exception) { return "error"; }
        }

        private void GetUsers()
        {
            SetLog("", true);

            try
            {
                string accountLogin = String.Empty;
                string accountName = String.Empty;
                string domain = "";

                Task.WaitAll(new Task[] {
                    Task.Factory.StartNew(() => Dispatcher.BeginInvoke(new ThreadStart(delegate { domain = tbDomain.Text.Trim(); })) ),
                    Task.Delay(10)
                });

                SetLog("Connecting to domain...");
                DirectoryEntry de = GetDirectoryEntry();

                SetLog("Applying filters...");
                DirectorySearcher deSearch = new DirectorySearcher(de);
                deSearch.Filter = "(&(objectClass=user)(objectCategory=person))";
                deSearch.SearchScope = SearchScope.Subtree;

                SetLog("Get users...");
                SearchResultCollection results = deSearch.FindAll();

                SetLog(String.Format("Found {0} users", results.Count.ToString()));

                StringBuilder filter = new StringBuilder();

                foreach (SearchResult result in results)
                {
                    DirectoryEntry dey = GetDirectoryEntry(result.Path);
                    accountLogin = dey.Properties["sAMAccountName"].Value.ToString();
                    accountName = dey.Properties["displayName"].Value.ToString();
                    dey.Close();

                    string login = Translate(accountLogin);

                    filter.Append(String.Format("{0}@{1},{2},{3}", login, domain, defaultPassword, accountName + Environment.NewLine));
                    SetLog("User '" + accountName + "' ('" + login + "') successfully added");
                }
                de.Close();

                SetLog("Users getting successfully!");

                SetLog("Saving users...");
                if (File.Exists(u_file)) File.Delete(u_file);

                using (var sink = new StreamWriter(u_file, false, Encoding.GetEncoding(1251)))
                    sink.WriteLine(filter.ToString());

                SetLog("File saved succesfully ( " + u_file + " )");
            }
            catch (Exception ex) { SetLog(ex.Message); }
        }

        private void SetLog(string text, bool clear = false)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                if (clear)
                    lbLog.Items.Clear();
                else
                    lbLog.Items.Add(text);
            }));
        }
    }
}

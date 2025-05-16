using System.Data.SQLite;
using System.Data;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Security.Policy;
using System.Media;
using System.Security.Principal;
using System.Diagnostics;
using bugfish_winclean.Library;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Linq;
using System.Numerics;

namespace bugfish_winclean
{
    public partial class Interface : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private int borderWidth = 10; // Set the width of the border
        private Color borderColor = Color.FromArgb(0xFF, 0xFF, 0xFF); // Set the color of the border
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnMaximize;
        private System.Windows.Forms.Button btnClose;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private Sqlite sqlite;
        private Point offset;
        private System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        private Task CurrentTask;
        private bool canceled;
        [DllImport("shell32.dll")]
        static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, uint dwFlags);

        public Interface()
        {
            // Ensure Admin Permission
            EnsureRunAsAdmin();

            // Initialize Component
            InitializeComponent();

            // Set Form Title
            this.Text = "Bugfish-WinClean";

            // Disable Load Panel
            Panel_Load.Visible = false;

            // Initialize SQLite
            sqlite = new Sqlite("data.db");
            sqlite.CreateTable("CREATE TABLE IF NOT EXISTS Items (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, Shortcode TEXT NOT NULL UNIQUE, IsActive INTEGER NOT NULL);");

            // Fix Interface
            interface_init_frame_btn();

            // Setup Window Icon
            this.Icon = new Icon(new MemoryStream(Properties.Resources.favicon));

            // Setup Window Fix Timer
            timer1.Interval = 500;
            timer1.Tick += timer1_Tick;
            timer1.Start();

            // Check for Table Status
            var dt = sqlite.GetDataTable("SELECT COUNT(*) as cnt FROM Items");
            int count = Convert.ToInt32(dt.Rows[0]["cnt"]);

            // Pre Configured Database Items
            var initialItems = new[]
            {
                new { Name = "[Destructive] Brave: Close and Delete User Data", Shortcode = "brave" },
                new { Name = "[Destructive] Cyberduck: Close and Delete User Data", Shortcode = "cyberduck" },
                new { Name = "[Destructive] Discord: Close and Delete User Data", Shortcode = "discord" },
                new { Name = "[Destructive] Dropbox: Close and Delete User Data", Shortcode = "dropbox" },
                new { Name = "[Destructive] Edge: Close and Delete User Data", Shortcode = "edge" },
                new { Name = "[Destructive] FileZilla: Close and Delete User Data", Shortcode = "filezilla" },
                new { Name = "[Destructive] Google Chrome: Close and Delete User Data", Shortcode = "chrome" },
                new { Name = "[Destructive] ICQ: Close and Delete User Data", Shortcode = "icq" },
                new { Name = "[Destructive] KeePass: Close and Delete User Data", Shortcode = "keepass" },
                new { Name = "[Destructive] Microsoft Outlook: Close and Delete User Data", Shortcode = "outlook" },
                new { Name = "[Destructive] Microsoft Teams: Close and Delete User Data", Shortcode = "teams" },
                new { Name = "[Destructive] Mozilla Firefox: Close and Delete User Data", Shortcode = "firefox" },
                new { Name = "[Destructive] Nextcloud: Close and Delete User Data", Shortcode = "nextcloud" },
                new { Name = "[Destructive] OneDrive: Close and Delete User Data", Shortcode = "onedrive" },
                new { Name = "[Destructive] Opera: Close and Delete User Data", Shortcode = "opera" },
                new { Name = "[Destructive] Opera GX: Close and Delete User Data", Shortcode = "operagx" },
                new { Name = "[Destructive] Signal: Close and Delete User Data", Shortcode = "signal" },
                new { Name = "[Destructive] Skype: Close and Delete User Data", Shortcode = "skype" },
                new { Name = "[Destructive] Slack: Close and Delete User Data", Shortcode = "slack" },
                new { Name = "[Destructive] Steam: Close and Delete User Data", Shortcode = "steam" },
                new { Name = "[Destructive] Telegram: Close and Delete User Data", Shortcode = "telegram" },
                new { Name = "[Destructive] Thunderbird: Close and Delete User Data", Shortcode = "thunderbird" },
                new { Name = "[Destructive] Tor Browser: Close and Delete User Data", Shortcode = "tor" },
                new { Name = "[Destructive] VeraCrypt: Close and Delete User Data", Shortcode = "veracrypt" },
                new { Name = "[Destructive] Viber: Close and Delete User Data", Shortcode = "viber" },
                new { Name = "[Destructive] Vivaldi: Close and Delete User Data", Shortcode = "vivaldi" },
                new { Name = "[Destructive] WhatsApp: Close and Delete User Data", Shortcode = "whatsapp" },
                new { Name = "[Destructive] WinSCP: Close and Delete User Data", Shortcode = "winscp" },
                new { Name = "[Destructive] Windows Authenticator App: Close and Delete User Data", Shortcode = "winauth" },
                new { Name = "[Destructive] Zoom: Close and Delete User Data", Shortcode = "zoom" },
                new { Name = "Windows: Clear System Restore Points", Shortcode = "windows_system_restore_cleanup" },
                new { Name = "Windows: Clear DirectX Shader Cache", Shortcode = "windows_directx_shader_cache" },
                new { Name = "Windows: Clear Update Cache", Shortcode = "windows_update_cleanup" },
                new { Name = "Windows: Clear Recent Documents List", Shortcode = "windows_recent_documents" },
                new { Name = "Windows: Clear Driver Install Logs", Shortcode = "windows_driver_install_logs" },
                new { Name = "Windows: Clear Web Cache", Shortcode = "windows_webcache" },
                new { Name = "Windows: Clear User Assist History", Shortcode = "windows_userassist" },
                new { Name = "Windows: Clear WebDav Cache", Shortcode = "windows_webdav_cache" },
                new { Name = "Windows: Clear Memory Dumps", Shortcode = "windows_memory_dumps" },
                new { Name = "Windows: Clear Error Reporting", Shortcode = "windows_error_reporting" },
                new { Name = "Windows: Clear Log Files", Shortcode = "windows_log_files" },
                new { Name = "Windows: Clear Event Trace Logs", Shortcode = "windows_event_trace_logs" },
                new { Name = "Windows: Clear Font Cache", Shortcode = "windows_font_cache" },
                new { Name = "Windows: Clear Recent File List", Shortcode = "windows_recent" },
                new { Name = "Windows: Clear Old Prefetch Data", Shortcode = "windows_prefetch" },
                new { Name = "Windows: Clear Trash Bin", Shortcode = "windows_trash" },
                new { Name = "Windows: Clear Explorer Most Recently Used List", Shortcode = "windows_explorer_mru" },
                new { Name = "Windows: Clear DNS Cache", Shortcode = "windows_dns_cache" },
                new { Name = "Windows: Clear Event Logs", Shortcode = "windows_event_logs" },
                new { Name = "Windows: Clear Clipboard", Shortcode = "windows_clipboard" },
                new { Name = "Windows: Clear Explorer Thumbnail Cache", Shortcode = "windows_thumbnail_cache" }
            }
            .OrderBy(x => x.Name) // Optional: ensures the list is sorted by Name
            .ToArray();

            // Restore Lost Database Items
            foreach (var item in initialItems)
            {
                var dtCheck = sqlite.GetDataTable(
                    "SELECT COUNT(*) as cnt FROM Items WHERE Shortcode = @shortcode",
                    new SQLiteParameter("@shortcode", item.Shortcode)
                );
                int exists = Convert.ToInt32(dtCheck.Rows[0]["cnt"]);
                if (exists == 0)
                {
                    sqlite.InsertData(
                        "INSERT INTO Items (Name, Shortcode, IsActive) VALUES (@name, @shortcode, @active)",
                        new SQLiteParameter("@name", item.Name),
                        new SQLiteParameter("@shortcode", item.Shortcode),
                        new SQLiteParameter("@active", "0")
                    );
                }
            }

            // Subscribe to the Paint event
            this.FormBorderStyle = FormBorderStyle.None;
            this.Padding = new Padding(borderWidth);
            this.Padding = new Padding(5);
            this.Paint += new PaintEventHandler(Interface_Paint);
            //this.Resize += Interface_Resize;

            // Create and configure ContextMenuStrip
            // contextMenuStrip = new ContextMenuStrip();
            // contextMenuStrip.Items.Add("Show", null, (s, e) => { this.Show(); this.WindowState = FormWindowState.Normal; notifyIcon.Visible = true; });
            // contextMenuStrip.Items.Add("Exit", null, (s, e) => { Application.Exit(); });

            // Create and configure NotifyIcon
            // notifyIcon = new NotifyIcon
            // {
            //     Icon = new Icon(new MemoryStream(Properties.Resources.nukeicon)),
            //     Visible = true,
            //     ContextMenuStrip = contextMenuStrip
            // };
            // notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;

            LoadItems();
            listBoxActive.DoubleClick += listBoxActive_DoubleClick;
            listBoxInactive.DoubleClick += listBoxInactive_DoubleClick;
        }

        // Reload List Items
        private void LoadItems()
        {
            listBoxActive.Items.Clear();
            listBoxInactive.Items.Clear();
            listBox2.Items.Clear();

            var dt = sqlite.GetDataTable("SELECT Id, Name, IsActive FROM Items");
            foreach (DataRow row in dt.Rows)
            {
                var item = new ListBoxItem
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString()
                };

                if (Convert.ToInt32(row["IsActive"]) == 1)
                {
                    listBoxActive.Items.Add(item);
                    listBox2.Items.Add(item);
                }
                else
                {
                    listBoxInactive.Items.Add(item);
                }
            }
        }

        // Set Active to inactive
        private void listBoxActive_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxActive.SelectedItem is ListBoxItem item)
            {
                sqlite.UpdateData("UPDATE Items SET IsActive = 0 WHERE Id = @id",
                    new SQLiteParameter("@id", item.Id));
                LoadItems();
            }
        }

        // Set Entry to Active
        private void listBoxInactive_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxInactive.SelectedItem is ListBoxItem item)
            {
                sqlite.UpdateData("UPDATE Items SET IsActive = 1 WHERE Id = @id",
                    new SQLiteParameter("@id", item.Id));
                LoadItems();
            }
        }

        // List Box Item Array
        public class ListBoxItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }

        // Start the Process
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            interface_reinit_frame_btn();
            Panel_Store.Visible = false;
            Panel_Load.Visible = true;
            listBox2.Visible = true;
            progressBar1.Visible = true;
            textBox1.Text = "";
            button2.Enabled = true;
            button1.Enabled = true;
            canceled = false;
            btnMaximize.Enabled = true;
            btnMinimize.Enabled = true;
            btnClose.Enabled = true;
            checkBox2.Checked = false;
            checkBox2.Enabled = true;
            checkBox2.AutoCheck = true;
            progressBar1.Value = 0;
        }

        // Cancel Deletion Button
        private async void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Operation is aborting...please wait.";
            canceled = true;
            if (CurrentTask != null)
            {
                if (CurrentTask.Status == TaskStatus.Running)
                {
                    await Task.Delay(1000);
                }
                else
                {
                    textBox1.Text = "Operation canceled.";
                }
            }
            interface_reinit_frame_btn();
            Panel_Load.Visible = false;
            listBox2.Visible = false;
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            Panel_Store.Visible = true;
            button2.Enabled = true;
            button1.Enabled = true;
            btnMaximize.Enabled = true;
            btnMinimize.Enabled = true;
            btnClose.Enabled = true;
            checkBox2.Checked = false;
            checkBox2.Enabled = true;
            checkBox2.AutoCheck = true;
        }

        // Confirm Deletion Button
        private async void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button1.Enabled = false;
            btnMaximize.Enabled = false;
            btnMinimize.Enabled = false;
            btnClose.Enabled = false;
            canceled = false;
            checkBox2.AutoCheck = false;
            textBox1.Text = "Starting cleanup process...";

            // Delete Section Process
            var dt = sqlite.GetDataTable("SELECT Id, Name, Shortcode FROM Items WHERE IsActive = 1 ORDER BY Name ASC;");
            int rowCount = dt.Rows.Count;
            if (rowCount == 0) { rowCount = 1; } else { rowCount = rowCount + 1; }
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = rowCount;
            foreach (DataRow row in dt.Rows)
            {
                if (!canceled)
                {
                    progressBar1.Value = progressBar1.Value + 1;

                    if (row["Shortcode"].Equals("winauth"))
                    {
                        textBox1.Text = "Closing and deletion files of: WinAuth";
                        KillProcesses("WinAuth");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"WinAuth");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("onedrive"))
                    {
                        textBox1.Text = "Closing and deletion files of: OneDrive";
                        KillProcesses("OneDrive");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            @"Microsoft\OneDrive");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("keepass"))
                    {
                        textBox1.Text = "Closing and deletion files of: KeePass";
                        KillProcesses("KeePass");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"KeePass");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("cyberduck"))
                    {
                        textBox1.Text = "Closing and deletion files of: Cyberduck";
                        KillProcesses("Cyberduck");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"Cyberduck");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("icq"))
                    {
                        textBox1.Text = "Closing and deletion files of: ICQ";
                        KillProcesses("icq");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"ICQ");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("viber"))
                    {
                        textBox1.Text = "Closing and deletion files of: Viber";
                        KillProcesses("Viber");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"ViberPC");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("veracrypt"))
                    {
                        textBox1.Text = "Closing and deletion files of: VeraCrypt";
                        KillProcesses("VeraCrypt");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"VeraCrypt");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("filezilla"))
                    {
                        textBox1.Text = "Closing and deletion files of: FileZilla";
                        KillProcesses("filezilla");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"FileZilla");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("winscp"))
                    {
                        textBox1.Text = "Closing and deletion files of: WinSCP";
                        KillProcesses("WinSCP");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"WinSCP");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("operagx"))
                    {
                        textBox1.Text = "Closing and deletion files of: Opera GX";
                        KillProcesses("opera");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"Opera Software\Opera GX Stable");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("teams"))
                    {
                        textBox1.Text = "Closing and deletion files of: Teams";
                        KillProcesses("Teams");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"Microsoft\Teams");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("zoom"))
                    {
                        textBox1.Text = "Closing and deletion files of: Zoom";
                        KillProcesses("Zoom");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"Zoom");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("tor"))
                    {
                        textBox1.Text = "Closing and deletion files of: Tor Browser";
                        KillProcesses("firefox"); // Tor uses a modified Firefox process
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"Tor Browser");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("dropbox"))
                    {
                        textBox1.Text = "Closing and deletion files of: Dropbox";
                        KillProcesses("Dropbox");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"Dropbox");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("vivaldi"))
                    {
                        textBox1.Text = "Closing and deletion files of: Vivaldi";
                        KillProcesses("vivaldi");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            @"Vivaldi\User Data");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("skype"))
                    {
                        textBox1.Text = "Closing and deletion files of: Skype";
                        KillProcesses("skype");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"Skype");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("slack"))
                    {
                        textBox1.Text = "Closing and deletion files of: Slack";
                        KillProcesses("slack");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"Slack");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("whatsapp"))
                    {
                        textBox1.Text = "Closing and deletion files of: WhatsApp";
                        KillProcesses("WhatsApp");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"WhatsApp");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("edge"))
                    {
                        textBox1.Text = "Closing and deletion files of: Edge";
                        KillProcesses("msedge");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            @"Microsoft\Edge\User Data");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("thunderbird"))
                    {
                        textBox1.Text = "Closing and deletion files of: Thunderbird";
                        KillProcesses("thunderbird");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"Thunderbird\Profiles");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("outlook"))
                    {
                        textBox1.Text = "Closing and deletion files of: Outlook";
                        KillProcesses("OUTLOOK");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            @"Microsoft\Outlook");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("opera"))
                    {
                        textBox1.Text = "Closing and deletion files of: Opera";
                        KillProcesses("opera");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"Opera Software\Opera Stable");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("signal"))
                    {
                        textBox1.Text = "Closing and deletion files of: Signal";
                        KillProcesses("Signal");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            "Signal");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("firefox"))
                    {
                        textBox1.Text = "Closing and deletion files of: firefox";
                        KillProcesses("firefox");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"Mozilla\Firefox\Profiles");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("nextcloud"))
                    {
                        textBox1.Text = "Closing and deletion files of: Nextcloud";
                        KillProcesses("Nextcloud");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            "Nextcloud");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("steam"))
                    {
                        textBox1.Text = "Closing and deletion files of: steam";
                        KillProcesses("Steam");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            "Steam");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                        userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            "Steam");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("brave"))
                    {
                        textBox1.Text = "Closing and deletion files of: Brave";
                        KillProcesses("brave");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            @"BraveSoftware\Brave-Browser\User Data"
                        );
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("telegram"))
                    {
                        textBox1.Text = "Closing and deletion files of: Telegram";
                        KillProcesses("Telegram");
                        KillProcesses("TelegramDesktop");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            "Telegram Desktop");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("discord"))
                    {
                        textBox1.Text = "Closing and deletion files of: Discord";
                        KillProcesses("Discord");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            "discord");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("chrome"))
                    {
                        textBox1.Text = "Closing and deletion files of: Chrome";
                        KillProcesses("Chrome");
                        string userData = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            @"Google\Chrome\User Data");
                        await Task.Run(() => SecureDeleteDirectoryAsync(userData, 0));
                    }

                    if (row["Shortcode"].Equals("windows_system_restore_cleanup"))
                    {
                        textBox1.Text = "Windows: Cleaning up System Restore Points";
                        await Task.Run(() =>
                        {
                            var psi = new ProcessStartInfo("vssadmin", "delete shadows /for=c: /oldest /quiet")
                            {
                                UseShellExecute = false,
                                CreateNoWindow = true
                            };
                            try { Process.Start(psi).WaitForExit(); } catch { }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_directx_shader_cache"))
                    {
                        textBox1.Text = "Windows: Deleting DirectX Shader Cache";
                        string shaderCachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "D3DSCache");
                        await Task.Run(() =>
                        {
                            if (Directory.Exists(shaderCachePath))
                            {
                                foreach (var file in Directory.GetFiles(shaderCachePath))
                                {
                                    try { SecureDelete.SecureDeleteFile(file, 0); } catch { }
                                }
                            }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_update_cleanup"))
                    {
                        textBox1.Text = "Windows: Cleaning up Windows Update Files";
                        string winSxS = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "WinSxS");
                        await Task.Run(() =>
                        {
                            // StartComponentCleanup via Task Scheduler
                            var psi = new ProcessStartInfo("schtasks.exe", "/Run /TN \"\\Microsoft\\Windows\\Servicing\\StartComponentCleanup\"")
                            {
                                UseShellExecute = false,
                                CreateNoWindow = true
                            };
                            try { Process.Start(psi).WaitForExit(); } catch { }
                            // Optionally, delete old update logs
                            string logDir = Path.Combine(winSxS, "ManifestCache");
                            if (Directory.Exists(logDir))
                            {
                                foreach (var file in Directory.GetFiles(logDir, "*.bin"))
                                {
                                    try { SecureDelete.SecureDeleteFile(file, 0); } catch { }
                                }
                            }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_recent_documents"))
                    {
                        textBox1.Text = "Windows: Clearing Recent Documents History";
                        string recentPath = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
                        await Task.Run(() =>
                        {
                            foreach (var file in Directory.GetFiles(recentPath))
                            {
                                try { SecureDelete.SecureDeleteFile(file, 0); } catch { }
                            }
                            // Also clear jump lists
                            string autoDest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Windows\Recent\AutomaticDestinations");
                            string customDest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Windows\Recent\CustomDestinations");
                            foreach (var dir in new[] { autoDest, customDest })
                            {
                                if (Directory.Exists(dir))
                                {
                                    foreach (var file in Directory.GetFiles(dir))
                                    {
                                        try { SecureDelete.SecureDeleteFile(file, 0); } catch { }
                                    }
                                }
                            }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_driver_install_logs"))
                    {
                        textBox1.Text = "Windows: Deleting Driver Installation Log Files";
                        string setupApiLog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "inf", "setupapi.dev.log");
                        string setupApiAppLog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "inf", "setupapi.app.log");
                        await Task.Run(() =>
                        {
                            foreach (var file in new[] { setupApiLog, setupApiAppLog })
                            {
                                try { SecureDelete.SecureDeleteFile(file, 0); } catch { }
                            }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_webcache"))
                    {
                        textBox1.Text = "Windows: Deleting WebCache Files";
                        string webCachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Microsoft\Windows\WebCache");
                        await Task.Run(() =>
                        {
                            if (Directory.Exists(webCachePath))
                            {
                                foreach (var file in Directory.GetFiles(webCachePath))
                                {
                                    try { SecureDelete.SecureDeleteFile(file, 0); } catch { }
                                }
                            }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_userassist"))
                    {
                        textBox1.Text = "Windows: Clearing UserAssist History";
                        await Task.Run(() =>
                        {
                            try
                            {
                                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\UserAssist", true))
                                {
                                    if (key != null)
                                    {
                                        foreach (var subKeyName in key.GetSubKeyNames())
                                        {
                                            try { key.DeleteSubKeyTree(subKeyName); } catch { }
                                        }
                                    }
                                }
                            }
                            catch { }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_webdav_cache"))
                    {
                        textBox1.Text = "Windows: Deleting WebDAV Cache";
                        string webDavCache = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Microsoft\Windows\WebCache");
                        await Task.Run(() =>
                        {
                            if (Directory.Exists(webDavCache))
                            {
                                foreach (var file in Directory.GetFiles(webDavCache, "webcache*.dat"))
                                {
                                    try { SecureDelete.SecureDeleteFile(file, 0); } catch { }
                                }
                            }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_memory_dumps"))
                    {
                        textBox1.Text = "Windows: Deleting Memory Dump Files";
                        string windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
                        await Task.Run(() =>
                        {
                            foreach (var file in new[] { "Memory.dmp", "Minidump.dmp" })
                            {
                                string fullPath = Path.Combine(windir, file);
                                try { SecureDelete.SecureDeleteFile(fullPath, 0); } catch { }
                            }
                            // Delete all files in Minidump folder
                            string minidumpDir = Path.Combine(windir, "Minidump");
                            if (Directory.Exists(minidumpDir))
                            {
                                foreach (var file in Directory.GetFiles(minidumpDir, "*.dmp"))
                                {
                                    try { SecureDelete.SecureDeleteFile(file, 0); } catch { }
                                }
                            }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_error_reporting"))
                    {
                        textBox1.Text = "Windows: Deleting Windows Error Reporting Files";
                        string werPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Microsoft\Windows\WER");
                        await Task.Run(() =>
                        {
                            if (Directory.Exists(werPath))
                            {
                                foreach (var dir in Directory.GetDirectories(werPath))
                                {
                                    try { SecureDelete.SecureDeleteDirectory(dir, 0); } catch { }
                                }
                                foreach (var file in Directory.GetFiles(werPath))
                                {
                                    try { SecureDelete.SecureDeleteFile(file, 0); } catch { }
                                }
                            }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_log_files"))
                    {
                        textBox1.Text = "Windows: Deleting Log Files";
                        string windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
                        string logDir = Path.Combine(windir, "Logs");
                        await Task.Run(() =>
                        {
                            if (Directory.Exists(logDir))
                            {
                                foreach (var file in Directory.GetFiles(logDir, "*.log"))
                                {
                                    try { SecureDelete.SecureDeleteFile(file, 0); } catch { }
                                }
                            }
                            // Also delete setup logs in Panther
                            string pantherDir = Path.Combine(windir, "Panther");
                            if (Directory.Exists(pantherDir))
                            {
                                foreach (var file in Directory.GetFiles(pantherDir, "*.log"))
                                {
                                    try { SecureDelete.SecureDeleteFile(file, 0); } catch { }
                                }
                            }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_event_trace_logs"))
                    {
                        textBox1.Text = "Windows: Deleting Event Trace Logs";
                        string windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
                        string etlDir = Path.Combine(windir, "System32", "LogFiles", "WMI");
                        await Task.Run(() =>
                        {
                            if (Directory.Exists(etlDir))
                            {
                                foreach (var file in Directory.GetFiles(etlDir, "*.etl"))
                                {
                                    try { SecureDelete.SecureDeleteFile(file, 0); } catch { }
                                }
                            }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_font_cache"))
                    {
                        textBox1.Text = "Windows: Clearing Font Cache";
                        string fontCachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"FontCache");
                        await Task.Run(() =>
                        {
                            if (Directory.Exists(fontCachePath))
                            {
                                foreach (var file in Directory.GetFiles(fontCachePath, "FontCache*.dat"))
                                {
                                    try { SecureDelete.SecureDeleteFile(file, 0); } catch { }
                                }
                            }
                            // Also try system-wide cache
                            string windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
                            string sysFontCache = Path.Combine(windir, "System32", "FNTCACHE.DAT");
                            try { SecureDelete.SecureDeleteFile(sysFontCache, 0); } catch { }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_recent"))
                    {
                        textBox1.Text = "Windows: Clearing Recent Files List";
                        string recentPath = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
                        await Task.Run(() =>
                        {
                            foreach (var file in Directory.GetFiles(recentPath))
                            {
                                try { SecureDelete.SecureDeleteFile(file, 0); } catch { }
                            }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_prefetch"))
                    {
                        textBox1.Text = "Windows: Clearing Prefetch Folder";
                        string prefetchPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Prefetch");
                        await Task.Run(() =>
                        {
                            foreach (var file in Directory.GetFiles(prefetchPath, "*.pf"))
                            {
                                try { SecureDelete.SecureDeleteFile(file, 0); } catch { }
                            }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_dns_cache"))
                    {
                        textBox1.Text = "Windows: Clearing DNS Cache";
                        await Task.Run(() =>
                        {
                            var psi = new ProcessStartInfo("ipconfig", "/flushdns")
                            {
                                RedirectStandardOutput = true,
                                UseShellExecute = false,
                                CreateNoWindow = true
                            };
                            Process.Start(psi).WaitForExit();
                        });
                    }

                    if (row["Shortcode"].Equals("windows_explorer_mru"))
                    {
                        textBox1.Text = "Windows: Clearing Explorer MRU";
                        await Task.Run(() =>
                        {
                            using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\LastVisitedPidlMRU", true))
                            {
                                key?.SetValue("", "");
                            }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_clipboard"))
                    {
                        textBox1.Text = "Clearing: Windows Clipboard";
                        System.Windows.Forms.Clipboard.Clear();
                    }

                    if (row["Shortcode"].Equals("windows_thumbnail_cache"))
                    {
                        textBox1.Text = "Clearing: Windows Thumbnail Cache";
                        string thumbCachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Microsoft\Windows\Explorer");
                        await Task.Run(() =>
                        {
                            foreach (var file in Directory.GetFiles(thumbCachePath, "thumbcache_*.db"))
                            {
                                try { SecureDelete.SecureDeleteFile(file, 0); } catch { }
                            }
                        });
                    }

                    if (row["Shortcode"].Equals("windows_event_logs"))
                    {
                        textBox1.Text = "Windows: Clearing Event Logs";
                        await Task.Run(() =>
                        {
                            var psi = new ProcessStartInfo("wevtutil", "cl Application");
                            psi.UseShellExecute = false;
                            psi.CreateNoWindow = true;
                            Process.Start(psi).WaitForExit();

                            psi = new ProcessStartInfo("wevtutil", "cl Security");
                            psi.UseShellExecute = false;
                            psi.CreateNoWindow = true;
                            Process.Start(psi).WaitForExit();

                            psi = new ProcessStartInfo("wevtutil", "cl System");
                            psi.UseShellExecute = false;
                            psi.CreateNoWindow = true;
                            Process.Start(psi).WaitForExit();
                        });
                    }

                    if (row["Shortcode"].Equals("windows_trash"))
                    {
                        textBox1.Text = "Windows: Clearing Trash";
                        SHEmptyRecycleBin(IntPtr.Zero, null, 0x7);
                    }

                }
            }

            // Close Application if checkbox is checked
            if (!canceled)
            {
                if (checkBox2.Checked)
                {
                    Application.Exit();
                }
            }

            // Finish the deletion Process
            progressBar1.Value = rowCount;
            if (!canceled) { textBox1.Text = "Finished cleanup process..."; }
            button2.Enabled = true;
            button1.Enabled = true;
            canceled = false;
            btnMaximize.Enabled = true;
            btnMinimize.Enabled = true;
            btnClose.Enabled = true;
            checkBox2.Checked = false;
            checkBox2.Enabled = true;
            checkBox2.AutoCheck = true;

        }

        // Helper to kill all processes by name
        void KillProcesses(params string[] processNames)
        {
            foreach (var name in processNames)
            {
                foreach (var proc in Process.GetProcessesByName(name))
                {
                    try { proc.Kill(true); proc.WaitForExit(); }
                    catch { }
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////
        /// Panel Link Text Clicks
        //////////////////////////////////////////////////////////////////////////////////

        // Show Tutorial Playlist
        private void richTextBox4_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://www.youtube.com/playlist?list=PL6npOHuBGrpDlS6CCHIUDz5O9mOnH7-7E",
                UseShellExecute = true
            });
        }
        // Show Youtube Page
        private void richTextBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://www.youtube.com/@BugfishTM",
                UseShellExecute = true
            });
        }
        // Show Github Page
        private void richTextBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://github.com/bugfishtm/bugfish-winclean",
                UseShellExecute = true
            });
        }

        //////////////////////////////////////////////////////////////////////////////////
        /// Async Functions
        //////////////////////////////////////////////////////////////////////////////////

        // Secure Delete Async Function
        public static Task SecureDeleteFileAsync(string filePath, int passes = 1)
        {
            return Task.Run(() => SecureDelete.SecureDeleteFile(filePath, passes));
        }

        // Secure Delete Async Function
        public static Task SecureDeleteDirectoryAsync(string dirPath, int passes = 1)
        {
            return Task.Run(() => SecureDelete.SecureDeleteDirectory(dirPath, passes));
        }

        // Sleep for Second Amount
        public async Task SleepSecondsAsync(int seconds)
        {
            seconds = seconds * 1000;
            await Task.Delay(seconds);
        }

        //////////////////////////////////////////////////////////////////////////////////
        /// UI Elements
        //////////////////////////////////////////////////////////////////////////////////

        // Initialize Border and Buttons
        private void interface_init_frame_btn()
        {
            // Minimize Button
            btnMinimize = new System.Windows.Forms.Button
            {
                Text = "_",
                Size = new Size(30, 30),
                Location = new Point(this.Width - 95, 5),
                BackColor = Color.FromArgb(0xFF, 0xFF, 0xFF),
                FlatStyle = FlatStyle.Flat
            };
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.Click += BtnMinimize_Click;
            btnMinimize.ForeColor = Color.FromArgb(0x00, 0x00, 0x00);
            tooltip_frame.SetToolTip(btnMinimize, "Minimize");

            // Maximize Button
            btnMaximize = new System.Windows.Forms.Button
            {
                Text = "O",
                Size = new Size(30, 30),
                Location = new Point(this.Width - 65, 5),
                BackColor = Color.FromArgb(0xFF, 0xFF, 0xFF),
                FlatStyle = FlatStyle.Flat
            };
            btnMaximize.FlatAppearance.BorderSize = 0;
            btnMaximize.Click += BtnMaximize_Click;
            btnMaximize.ForeColor = Color.FromArgb(0x00, 0x00, 0x00);
            tooltip_frame.SetToolTip(btnMaximize, "Maximize");

            // Close Button
            btnClose = new System.Windows.Forms.Button
            {
                Text = "X",
                Size = new Size(30, 30),
                Location = new Point(this.Width - 35, 5),
                BackColor = Color.FromArgb(0xFF, 0xFF, 0xFF),
                FlatStyle = FlatStyle.Flat
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += BtnClose_Click;
            btnClose.ForeColor = Color.FromArgb(0x00, 0x00, 0x00);
            tooltip_frame.SetToolTip(btnClose, "Close");

            // Add buttons to the form
            this.Controls.Add(btnMinimize);
            this.Controls.Add(btnMaximize);
            this.Controls.Add(btnClose);

            btnClose.BringToFront();
            btnMaximize.BringToFront();
            btnMinimize.BringToFront();

        }

        // ReInitialize Border and Buttons
        private void interface_reinit_frame_btn()
        {
            btnClose.Location = new Point(this.Width - 35, 5);
            btnMaximize.Location = new Point(this.Width - 65, 5);
            btnMinimize.Location = new Point(this.Width - 95, 5);
            btnClose.BringToFront();
            btnMaximize.BringToFront();
            btnMinimize.BringToFront();

            int imageWidth = this.Width;
            int listBoxWidth = imageWidth / 2;

            panel3.Width = listBoxWidth - 7;
            panel2.Width = listBoxWidth - 7;

            panel3.Left = 0;
            panel2.Left = panel3.Right;
        }

        // Allow for resizing by overriding WndProc
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int WM_GETMINMAXINFO = 0x24;
            const int HTCLIENT = 1;
            const int HTCAPTION = 2;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;

            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);

                    Point pos = PointToClient(new Point(m.LParam.ToInt32()));
                    if (pos.X < borderWidth && pos.Y < borderWidth)
                    {
                        m.Result = (IntPtr)HTTOPLEFT;
                    }
                    else if (pos.X > Width - borderWidth && pos.Y < borderWidth)
                    {
                        m.Result = (IntPtr)HTTOPRIGHT;
                    }
                    else if (pos.X < borderWidth && pos.Y > Height - borderWidth)
                    {
                        m.Result = (IntPtr)HTBOTTOMLEFT;
                    }
                    else if (pos.X > Width - borderWidth && pos.Y > Height - borderWidth)
                    {
                        m.Result = (IntPtr)HTBOTTOMRIGHT;
                    }
                    else if (pos.X < borderWidth)
                    {
                        m.Result = (IntPtr)HTLEFT;
                    }
                    else if (pos.X > Width - borderWidth)
                    {
                        m.Result = (IntPtr)HTRIGHT;
                    }
                    else if (pos.Y < borderWidth)
                    {
                        m.Result = (IntPtr)HTTOP;
                    }
                    else if (pos.Y > Height - borderWidth)
                    {
                        m.Result = (IntPtr)HTBOTTOM;
                    }
                    else
                    {
                        m.Result = (IntPtr)HTCLIENT;
                    }
                    interface_reinit_frame_btn();
                    return;

                case WM_GETMINMAXINFO:
                    MINMAXINFO minMaxInfo = (MINMAXINFO)Marshal.PtrToStructure(m.LParam, typeof(MINMAXINFO));
                    minMaxInfo.ptMinTrackSize.X = 1200; // Minimum width
                    minMaxInfo.ptMinTrackSize.Y = 700; // Minimum height
                    Marshal.StructureToPtr(minMaxInfo, m.LParam, true);
                    interface_reinit_frame_btn();
                    break;
            }
            base.WndProc(ref m);
        }

        // Extra Function for Minimum Resizing in Width and Height to not make the Window Disappear
        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public Point ptReserved;
            public Point ptMaxSize;
            public Point ptMaxPosition;
            public Point ptMinTrackSize;
            public Point ptMaxTrackSize;
        }

        // Function for Mouse Move on Title Bar Selection
        private void header_frame_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Capture the offset from the mouse cursor to the form's location
                offset = new Point(e.X, e.Y);
            }
        }

        // Additional Function for MouseMove
        private void header_frame_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Move the form with the mouse
                Point newLocation = this.PointToScreen(new Point(e.X, e.Y));
                newLocation.Offset(-offset.X, -offset.Y);

                // Ensure the form stays within the screen bounds
                Screen screen = Screen.FromPoint(newLocation);
                Rectangle screenBounds = screen.Bounds;

                // Adjust newLocation if it goes outside screen bounds
                int newX = Math.Max(screenBounds.Left, Math.Min(screenBounds.Right - this.Width, newLocation.X));
                int newY = Math.Max(screenBounds.Top, Math.Min(screenBounds.Bottom - this.Height, newLocation.Y));

                this.Location = new Point(newX, newY);
            }
        }

        // Maximize Button Click Functionality
        private void BtnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                Rectangle workingArea = Screen.GetWorkingArea(this);
                this.Width = workingArea.Width;
                this.Height = workingArea.Height;
                this.Location = new Point(Math.Max(this.Location.X, workingArea.X),
                          Math.Max(this.Location.Y, workingArea.Y));
            }
        }

        // Drag Window by Holding on Header
        private void Header_Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        // Maximize and Minize Windows with Double Click
        private void Header_Panel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Normal)
                    this.WindowState = FormWindowState.Maximized;
                else if (this.WindowState == FormWindowState.Maximized)
                    this.WindowState = FormWindowState.Normal;
            }
            interface_reinit_frame_btn();
        }

        // NotifyIcon DoubleClick
        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show(); this.WindowState = FormWindowState.Normal; notifyIcon.Visible = true;
        }

        // Interface Paint Functionality
        private void Interface_Paint(object sender, PaintEventArgs e)
        {
            // Draw the custom border
            using (Pen borderPen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1));
            }
        }

        // Interface Resize Functionality
        private void Interface_Resize(object sender, EventArgs e)
        {
            // Update button locations on resize
            btnMinimize.Location = new Point(this.Width - 95, 5);
            btnMaximize.Location = new Point(this.Width - 65, 5);
            btnClose.Location = new Point(this.Width - 35, 5);
        }

        // Minimize Button Click to Minimize Current Form
        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Close Window to Tray or Close Completely
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
            // this.Hide();
            // notifyIcon.Visible = true;
        }


        // Tick Timer Fix Window Resize Errors
        private void timer1_Tick(object sender, EventArgs e)
        {
            interface_reinit_frame_btn();
        }

        //////////////////////////////////////////////////////////////////////////////////
        /// Administrator Permission Request
        //////////////////////////////////////////////////////////////////////////////////

        // Check for Admin Permissions
        public static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        // Ensure Admin Permissions
        public static void EnsureRunAsAdmin()
        {
            if (!IsAdministrator())
            {
                // Restart the application with admin rights
                var proc = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory,
                    FileName = Application.ExecutablePath,
                    Verb = "runas" // This triggers the UAC prompt
                };

                try
                {
                    Process.Start(proc);
                }
                catch
                {
                    // User refused the elevation
                    MessageBox.Show("Administrator permissions are required to continue.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Application.Exit(); // Close the current instance
            }
        }

        private void Panel_Load_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

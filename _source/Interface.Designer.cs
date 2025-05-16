namespace bugfish_winclean
{
    partial class Interface
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Interface));
            Header_Panel = new Panel();
            label8 = new Label();
            label7 = new Label();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            Panel_Store = new Panel();
            panel3 = new Panel();
            label4 = new Label();
            listBoxInactive = new ListBox();
            richTextBox4 = new RichTextBox();
            richTextBox3 = new RichTextBox();
            richTextBox2 = new RichTextBox();
            richTextBox1 = new RichTextBox();
            pictureBox2 = new PictureBox();
            panel1 = new Panel();
            panel2 = new Panel();
            label3 = new Label();
            listBoxActive = new ListBox();
            tooltip_frame = new ToolTip(components);
            bindingSource1 = new BindingSource(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            imageList1 = new ImageList(components);
            panel6 = new Panel();
            progressBar1 = new ProgressBar();
            listBox2 = new ListBox();
            button1 = new Button();
            button2 = new Button();
            textBox1 = new TextBox();
            groupBox2 = new GroupBox();
            checkBox2 = new CheckBox();
            Panel_Load = new Panel();
            Header_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            Panel_Store.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            groupBox2.SuspendLayout();
            Panel_Load.SuspendLayout();
            SuspendLayout();
            // 
            // Header_Panel
            // 
            Header_Panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Header_Panel.BackColor = Color.FromArgb(244, 244, 244);
            Header_Panel.Controls.Add(label8);
            Header_Panel.Controls.Add(label7);
            Header_Panel.Controls.Add(pictureBox1);
            Header_Panel.Controls.Add(label1);
            Header_Panel.Controls.Add(label2);
            Header_Panel.Location = new Point(0, 0);
            Header_Panel.Name = "Header_Panel";
            Header_Panel.Size = new Size(1099, 100);
            Header_Panel.TabIndex = 0;
            Header_Panel.MouseDoubleClick += Header_Panel_MouseDoubleClick;
            Header_Panel.MouseDown += Header_Panel_MouseDown;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.ForeColor = Color.Black;
            label8.Location = new Point(622, 69);
            label8.Name = "label8";
            label8.Size = new Size(465, 20);
            label8.TabIndex = 5;
            label8.Text = "Feature missing? Contact me at winclean@bugfish.eu, I will take care.";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI", 10F);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(994, 46);
            label7.Name = "label7";
            label7.Size = new Size(93, 23);
            label7.TabIndex = 4;
            label7.Text = "Version 1.1";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(104, 95);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.Black;
            label1.Location = new Point(113, 9);
            label1.Name = "label1";
            label1.Size = new Size(58, 20);
            label1.TabIndex = 2;
            label1.Text = "Bugfish";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 40F);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(113, 9);
            label2.Name = "label2";
            label2.Size = new Size(319, 89);
            label2.TabIndex = 3;
            label2.Text = "WinClean";
            // 
            // Panel_Store
            // 
            Panel_Store.BackColor = Color.FromArgb(224, 224, 224);
            Panel_Store.Controls.Add(panel3);
            Panel_Store.Controls.Add(richTextBox4);
            Panel_Store.Controls.Add(richTextBox3);
            Panel_Store.Controls.Add(richTextBox2);
            Panel_Store.Controls.Add(richTextBox1);
            Panel_Store.Controls.Add(pictureBox2);
            Panel_Store.Controls.Add(panel1);
            Panel_Store.Dock = DockStyle.Fill;
            Panel_Store.Location = new Point(0, 0);
            Panel_Store.Name = "Panel_Store";
            Panel_Store.Size = new Size(1099, 741);
            Panel_Store.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            panel3.BackColor = Color.FromArgb(224, 224, 224);
            panel3.Controls.Add(label4);
            panel3.Controls.Add(listBoxInactive);
            panel3.Location = new Point(3, 101);
            panel3.Name = "panel3";
            panel3.Size = new Size(541, 489);
            panel3.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 15F);
            label4.ForeColor = Color.Red;
            label4.Location = new Point(0, 2);
            label4.Name = "label4";
            label4.Size = new Size(121, 35);
            label4.TabIndex = 5;
            label4.Text = "INACTIVE";
            // 
            // listBoxInactive
            // 
            listBoxInactive.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxInactive.BackColor = Color.FromArgb(224, 224, 224);
            listBoxInactive.Font = new Font("Segoe UI", 10F);
            listBoxInactive.ForeColor = SystemColors.ControlText;
            listBoxInactive.FormattingEnabled = true;
            listBoxInactive.ItemHeight = 23;
            listBoxInactive.Location = new Point(0, 40);
            listBoxInactive.Name = "listBoxInactive";
            listBoxInactive.Size = new Size(535, 418);
            listBoxInactive.TabIndex = 1;
            // 
            // richTextBox4
            // 
            richTextBox4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            richTextBox4.BackColor = Color.FromArgb(224, 224, 224);
            richTextBox4.BorderStyle = BorderStyle.None;
            richTextBox4.ForeColor = Color.Black;
            richTextBox4.Location = new Point(12, 587);
            richTextBox4.Name = "richTextBox4";
            richTextBox4.ReadOnly = true;
            richTextBox4.Size = new Size(345, 64);
            richTextBox4.TabIndex = 12;
            richTextBox4.Text = "Visit this software's tutorial video:\nhttps://www.youtube.com/playlist?list=PL6npOHuBGrpDlS6CCHIUDz5O9mOnH7-7E";
            richTextBox4.Click += richTextBox4_Clicked;
            // 
            // richTextBox3
            // 
            richTextBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            richTextBox3.BackColor = Color.FromArgb(224, 224, 224);
            richTextBox3.BorderStyle = BorderStyle.None;
            richTextBox3.ForeColor = Color.Black;
            richTextBox3.Location = new Point(361, 678);
            richTextBox3.Name = "richTextBox3";
            richTextBox3.ReadOnly = true;
            richTextBox3.Size = new Size(339, 45);
            richTextBox3.TabIndex = 11;
            richTextBox3.Text = "Visit this software's github profile:\nhttps://github.com/bugfishtm/bugfish-winclean";
            richTextBox3.Click += richTextBox3_Click;
            // 
            // richTextBox2
            // 
            richTextBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            richTextBox2.BackColor = Color.FromArgb(224, 224, 224);
            richTextBox2.BorderStyle = BorderStyle.None;
            richTextBox2.ForeColor = Color.Black;
            richTextBox2.Location = new Point(12, 678);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.ReadOnly = true;
            richTextBox2.Size = new Size(339, 45);
            richTextBox2.TabIndex = 10;
            richTextBox2.Text = "Visit my Youtube Channel:\nhttps://www.youtube.com/@BugfishTM";
            richTextBox2.Click += richTextBox2_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            richTextBox1.BackColor = Color.FromArgb(224, 224, 224);
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.ForeColor = Color.Black;
            richTextBox1.Location = new Point(361, 587);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(385, 86);
            richTextBox1.TabIndex = 8;
            richTextBox1.Text = "Usage Information\n - Double-click list items to enable or disable them.\n - Click the start button to review and confirm the process.\n - Use this software at your own risk.";
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Location = new Point(931, 578);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(162, 145);
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(224, 224, 224);
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(3, 101);
            panel1.Name = "panel1";
            panel1.Size = new Size(1093, 495);
            panel1.TabIndex = 9;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel2.BackColor = Color.FromArgb(224, 224, 224);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(listBoxActive);
            panel2.Location = new Point(541, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(546, 486);
            panel2.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 15F);
            label3.ForeColor = Color.OliveDrab;
            label3.Location = new Point(0, 2);
            label3.Name = "label3";
            label3.Size = new Size(95, 35);
            label3.TabIndex = 4;
            label3.Text = "ACTIVE";
            // 
            // listBoxActive
            // 
            listBoxActive.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxActive.BackColor = Color.FromArgb(224, 224, 224);
            listBoxActive.Font = new Font("Segoe UI", 10F);
            listBoxActive.ForeColor = SystemColors.ActiveCaptionText;
            listBoxActive.FormattingEnabled = true;
            listBoxActive.ItemHeight = 23;
            listBoxActive.Location = new Point(0, 40);
            listBoxActive.Name = "listBoxActive";
            listBoxActive.Size = new Size(540, 418);
            listBoxActive.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel6.BackColor = Color.FromArgb(244, 244, 244);
            panel6.Location = new Point(0, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(1099, 741);
            panel6.TabIndex = 11;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.ForeColor = Color.Gold;
            progressBar1.Location = new Point(12, 694);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(775, 29);
            progressBar1.TabIndex = 0;
            // 
            // listBox2
            // 
            listBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBox2.BackColor = Color.FromArgb(244, 244, 244);
            listBox2.ForeColor = SystemColors.InactiveCaptionText;
            listBox2.FormattingEnabled = true;
            listBox2.Location = new Point(9, 107);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(778, 544);
            listBox2.TabIndex = 1;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.ForeColor = SystemColors.ActiveCaptionText;
            button1.Location = new Point(943, 694);
            button1.Name = "button1";
            button1.Size = new Size(144, 29);
            button1.TabIndex = 5;
            button1.Text = "Confirm";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.ForeColor = SystemColors.ActiveCaptionText;
            button2.Location = new Point(793, 694);
            button2.Name = "button2";
            button2.Size = new Size(144, 29);
            button2.TabIndex = 6;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.BackColor = Color.FromArgb(244, 244, 244);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.ForeColor = SystemColors.InactiveCaptionText;
            textBox1.Location = new Point(12, 668);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(775, 20);
            textBox1.TabIndex = 8;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox2.Controls.Add(checkBox2);
            groupBox2.ForeColor = SystemColors.ActiveCaptionText;
            groupBox2.Location = new Point(793, 107);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(294, 74);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "Various Settings";
            // 
            // checkBox2
            // 
            checkBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(15, 34);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(223, 24);
            checkBox2.TabIndex = 8;
            checkBox2.Text = "Close Software after Cleanup";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // Panel_Load
            // 
            Panel_Load.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Panel_Load.BackColor = Color.FromArgb(244, 244, 244);
            Panel_Load.Controls.Add(groupBox2);
            Panel_Load.Controls.Add(textBox1);
            Panel_Load.Controls.Add(button2);
            Panel_Load.Controls.Add(button1);
            Panel_Load.Controls.Add(listBox2);
            Panel_Load.Controls.Add(progressBar1);
            Panel_Load.ForeColor = SystemColors.ButtonHighlight;
            Panel_Load.Location = new Point(0, 0);
            Panel_Load.Name = "Panel_Load";
            Panel_Load.Size = new Size(1099, 741);
            Panel_Load.TabIndex = 12;
            Panel_Load.Paint += Panel_Load_Paint;
            // 
            // Interface
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Red;
            ClientSize = new Size(1099, 741);
            Controls.Add(Header_Panel);
            Controls.Add(Panel_Load);
            Controls.Add(Panel_Store);
            Controls.Add(panel6);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Interface";
            Text = "Form1";
            Header_Panel.ResumeLayout(false);
            Header_Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            Panel_Store.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            Panel_Load.ResumeLayout(false);
            Panel_Load.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel Header_Panel;
        private Panel Panel_Store;
        private PictureBox pictureBox1;
        private Label label1;
        private ToolTip tooltip_frame;
        private Label label2;
        private ListBox listBoxInactive;
        private ListBox listBoxActive;
        private BindingSource bindingSource1;
        private Label label4;
        private Label label3;
        private ContextMenuStrip contextMenuStrip1;
        private PictureBox pictureBox2;
        private RichTextBox richTextBox1;
        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        private RichTextBox richTextBox2;
        private RichTextBox richTextBox3;
        private Label label7;
        private Label label8;
        private ImageList imageList1;
        private Panel panel6;
        private RichTextBox richTextBox4;
        private ProgressBar progressBar1;
        private ListBox listBox2;
        private Button button1;
        private Button button2;
        private TextBox textBox1;
        private GroupBox groupBox2;
        private CheckBox checkBox2;
        private Panel Panel_Load;
    }
}

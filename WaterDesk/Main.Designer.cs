namespace WaterDesk
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            gvDevices = new DataGridView();
            toolStrip1 = new ToolStrip();
            tsLblStatus = new ToolStripLabel();
            label1 = new Label();
            bgWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)gvDevices).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // gvDevices
            // 
            gvDevices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gvDevices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gvDevices.Location = new Point(12, 55);
            gvDevices.Name = "gvDevices";
            gvDevices.RowTemplate.Height = 25;
            gvDevices.Size = new Size(885, 203);
            gvDevices.TabIndex = 0;
            gvDevices.CellClick += gvDevices_CellClick;
            gvDevices.CellFormatting += gvDevices_CellFormatting;
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = DockStyle.Bottom;
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsLblStatus });
            toolStrip1.Location = new Point(0, 284);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(924, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // tsLblStatus
            // 
            tsLblStatus.Name = "tsLblStatus";
            tsLblStatus.Size = new Size(39, 22);
            tsLblStatus.Text = "Ready";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 23);
            label1.Name = "label1";
            label1.Size = new Size(100, 15);
            label1.TabIndex = 2;
            label1.Text = "List of IoT devices";
            // 
            // bgWorker
            // 
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(924, 309);
            Controls.Add(label1);
            Controls.Add(toolStrip1);
            Controls.Add(gvDevices);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WaterDesk IoT";
            Load += Main_Load;
            ((System.ComponentModel.ISupportInitialize)gvDevices).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView gvDevices;
        private ToolStrip toolStrip1;
        private ToolStripLabel tsLblStatus;
        private Label label1;
        private System.ComponentModel.BackgroundWorker bgWorker;
    }
}
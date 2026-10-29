namespace Project_2_Warehouse
{
    partial class Form1
    {
        private System.Windows.Forms.DataGridView productGrid;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.productGrid = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            var addMenu = new System.Windows.Forms.ToolStripMenuItem("Add Products");
            var newProduct = new System.Windows.Forms.ToolStripMenuItem("New Product", null, this.AddNewProduct);
            var existingProduct = new System.Windows.Forms.ToolStripMenuItem("Add to Existing Product", null, this.AddExistingProduct);
            addMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newProduct, existingProduct });
            var manageMenu = new System.Windows.Forms.ToolStripMenuItem("Manage");
            manageMenu.DropDownItems.Add("categorii", null, this.Opencategorii);
            manageMenu.DropDownItems.Add("Sales History", null, this.OpenSales);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { addMenu, manageMenu });
            this.productGrid.AllowUserToAddRows = false;
            this.productGrid.AutoGenerateColumns = true;
            this.productGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productGrid.ReadOnly = true;
            this.productGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.productGrid.DoubleClick += new System.EventHandler(this.SellSelected);
            this.txtSearch.Location = new System.Drawing.Point(12, 52);
            this.txtSearch.Size = new System.Drawing.Size(560, 23);
            this.txtSearch.TextChanged += new System.EventHandler(this.SearchChanged);
            this.refreshButton.Location = new System.Drawing.Point(590, 50);
            this.refreshButton.Size = new System.Drawing.Size(100, 26);
            this.refreshButton.Text = "Refresh";
            this.refreshButton.Click += new System.EventHandler((s, e) => LoadProducts());
            this.Controls.Add(this.productGrid);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 520);
            this.Text = "Warehouse Product Management";
            this.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
        }

        #endregion
    }
}


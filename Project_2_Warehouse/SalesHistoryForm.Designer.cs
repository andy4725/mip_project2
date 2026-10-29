namespace Project_2_Warehouse
{
    partial class SalesHistoryForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView salesGrid;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container(); salesGrid = new System.Windows.Forms.DataGridView(); salesGrid.Dock = System.Windows.Forms.DockStyle.Fill; salesGrid.ReadOnly = true; salesGrid.AutoGenerateColumns = true; salesGrid.AllowUserToAddRows = false;
            Controls.Add(salesGrid); ClientSize = new System.Drawing.Size(760, 420); Text = "Sales History"; StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        }
    }
}

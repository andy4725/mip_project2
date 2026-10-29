using System;
using System.Windows.Forms;
using Project_2_Warehouse.Services;

namespace Project_2_Warehouse
{
    public partial class categorieForm : Form
    {
        public categorieForm() { InitializeComponent(); Load += (s, e) => Loadcategorii(); }
        private async void Loadcategorii()
        {
            try
            {
                categoryGrid.DataSource = await WarehouseService.GetCategoriesAsync();
                if (categoryGrid.Columns.Contains("id")) categoryGrid.Columns["id"].HeaderText = "ID";
                if (categoryGrid.Columns.Contains("denumire")) categoryGrid.Columns["denumire"].HeaderText = "denumire";
                if (categoryGrid.Columns.Contains("produse")) categoryGrid.Columns["produse"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }
        private async void AddCategory(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameText.Text) || nameText.Text.Trim().Length > 50) { MessageBox.Show("A category name is required and must be at most 50 characters."); return; }
            try { await WarehouseService.AddCategoryAsync(nameText.Text); nameText.Clear(); Loadcategorii(); } catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project_2_Warehouse.Models;
using Project_2_Warehouse.Services;

namespace Project_2_Warehouse
{
    public partial class AddProductForm : Form
    {
        private readonly bool existing;
        public AddProductForm(bool existingProduct)
        {
            existing = existingProduct;
            InitializeComponent();
            Text = existing ? "Add cantitate to Existing Produs" : "Add New Product";
            Load += async (s, e) => { if (!existing) await Loadcategorii(); };
        }

        private async Task Loadcategorii()
        {
            try { categoryCombo.DataSource = await WarehouseService.GetCategoriesAsync(); categoryCombo.DisplayMember = "denumire"; categoryCombo.ValueMember = "id"; }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private async void SaveProdus(object sender, EventArgs e)
        {
            // Validate the input before saving it.
            try
            {
                int quantity;
                if (!int.TryParse(quantityText.Text, out quantity) || quantity <= 0) throw new InvalidOperationException("Quantity must be positive.");
                if (existing)
                {
                    int id;
                    if (!int.TryParse(idText.Text, out id)) throw new InvalidOperationException("Enter a valid product ID.");
                    await WarehouseService.AddExistingProductQuantityAsync(id, quantity);
                }
                else
                {
                    if (categoryCombo.SelectedValue == null) throw new InvalidOperationException("Select a category.");
                    await WarehouseService.AddProductAsync(new Produs { denumire = nameText.Text.Trim(), descriere = descriptionText.Text.Trim(), dataIntrare = entryDate.Value.Date, dataExpirare = expiryDate.Value.Date, cantitate = quantity, idCategorie = (int)categoryCombo.SelectedValue });
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.InnerException == null ? ex.Message : ex.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}

using System;
using System.Windows.Forms;
using Project_2_Warehouse.Services;

namespace Project_2_Warehouse
{
    public partial class SellProductForm : Form
    {
        private readonly ProductView product;
        public SellProductForm(ProductView selectedProdus)
        {
            product = selectedProdus;
            InitializeComponent();
            productLabel.Text = string.Format("{0} ({1}) - Available: {2}", product.denumire, product.categorie, product.cantitate);
            quantityNumeric.Maximum = product.cantitate;
        }
        private async void ConfirmSale(object sender, EventArgs e)
        {
            // Complete the sale and close the dialog when it succeeds.
            try
            {
                await WarehouseService.SellProductAsync(product.id, (int)quantityNumeric.Value);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.InnerException == null ? ex.Message : ex.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}

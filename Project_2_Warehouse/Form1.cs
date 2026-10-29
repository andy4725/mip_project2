using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project_2_Warehouse.Services;

namespace Project_2_Warehouse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += (s, e) => LoadProducts();
        }

        private async void LoadProducts()
        {
            // Refresh the grid with the current search text.
            try
            {
                SetBusy(true);
                var products = await WarehouseService.GetProductsAsync(txtSearch.Text);
                if (!IsDisposed) Invoke((Action)(() =>
                {
                    productGrid.DataSource = products;
                    SetProdusHeaders();
                }));
            }
            catch (Exception ex) { ShowError(ex); }
            finally { SetBusy(false); }
        }

        private void SetBusy(bool busy)
        {
            if (InvokeRequired) { Invoke((Action)(() => SetBusy(busy))); return; }
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
            refreshButton.Enabled = !busy;
        }

        private void ShowError(Exception ex)
        {
            if (InvokeRequired) { Invoke((Action)(() => ShowError(ex))); return; }
            MessageBox.Show(ex.InnerException == null ? ex.Message : ex.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void AddNewProduct(object sender, EventArgs e)
        {
            using (var form = new AddProductForm(false)) if (form.ShowDialog(this) == DialogResult.OK) LoadProducts();
        }

        private void AddExistingProduct(object sender, EventArgs e)
        {
            using (var form = new AddProductForm(true)) if (form.ShowDialog(this) == DialogResult.OK) LoadProducts();
        }

        private void Opencategorii(object sender, EventArgs e)
        {
            using (var form = new categorieForm()) form.ShowDialog(this);
        }

        private void OpenSales(object sender, EventArgs e)
        {
            using (var form = new SalesHistoryForm()) form.ShowDialog(this);
        }

        private void SellSelected(object sender, EventArgs e)
        {
            if (productGrid.CurrentRow == null) return;
            var product = productGrid.CurrentRow.DataBoundItem as ProductView;
            if (product == null) return;
            using (var form = new SellProductForm(product)) if (form.ShowDialog(this) == DialogResult.OK) LoadProducts();
        }

        private void SearchChanged(object sender, EventArgs e) { LoadProducts(); }

        private void SetProdusHeaders()
        {
            SetHeader("id", "ID");
            SetHeader("denumire", "denumire");
            SetHeader("descriere", "descriere");
            SetHeader("dataIntrare", "Entry Date");
            SetHeader("dataExpirare", "Expiration Date");
            SetHeader("cantitate", "cantitate");
            SetHeader("idCategorie", "categorie ID");
            SetHeader("categorie", "categorie");
        }

        private void SetHeader(string columnName, string headerText)
        {
            if (productGrid.Columns.Contains(columnName)) productGrid.Columns[columnName].HeaderText = headerText;
        }
    }
}

using System;
using System.Windows.Forms;
using Project_2_Warehouse.Services;

namespace Project_2_Warehouse
{
    public partial class SalesHistoryForm : Form
    {
        public SalesHistoryForm() { InitializeComponent(); Load += (s, e) => LoadHistory(); }
        private async void LoadHistory()
        {
            try
            {
                salesGrid.DataSource = await WarehouseService.GetSalesAsync();
                SetHeader("id", "ID");
                SetHeader("idProdus", "Product ID");
                SetHeader("denumireProdus", "Product Name");
                SetHeader("categorie", "categorie");
                SetHeader("cantitate", "cantitate");
                SetHeader("dataVanzare", "Sale Date");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void SetHeader(string columnName, string headerText)
        {
            if (salesGrid.Columns.Contains(columnName)) salesGrid.Columns[columnName].HeaderText = headerText;
        }
    }
}

using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace FufixLandApp
{
    public partial class MainForm : Form
    {
        private DatabaseHelper db;
        private Timer refreshTimer;

        // Culori tema roz/violet
        private Color primaryColor = Color.FromArgb(219, 112, 147); // Roz pal
        private Color secondaryColor = Color.FromArgb(147, 112, 219); // Violet
        private Color darkColor = Color.FromArgb(128, 0, 128); // Violet inchis
        private Color lightColor = Color.FromArgb(255, 218, 233); // Roz deschis
        private Color accentColor = Color.FromArgb(255, 105, 180); // Roz aprins

        public MainForm()
        {
            db = new DatabaseHelper();
            InitializeComponent();
            LoadDashboardStats();
            SetupRefreshTimer();
            CheckConnection();
        }

        private void CheckConnection()
        {
            if (!db.TestConnection())
            {
                MessageBox.Show("Nu se poate conecta la baza de date!", "Eroare Conexiune",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadDashboardStats()
        {
            try
            {
                string queryClients = "SELECT COUNT(*) FROM client";
                int totalClients = Convert.ToInt32(db.ExecuteScalar(queryClients));

                string queryProducts = "SELECT COUNT(*) FROM produs WHERE activ = true";
                int totalProducts = Convert.ToInt32(db.ExecuteScalar(queryProducts));

                string queryOrders = "SELECT COUNT(*) FROM comanda WHERE stare = 'in procesare'";
                int pendingOrders = Convert.ToInt32(db.ExecuteScalar(queryOrders));

                string queryRevenue = @"SELECT COALESCE(SUM(total_cu_tva), 0) FROM factura 
                    WHERE EXTRACT(MONTH FROM data_emitere) = EXTRACT(MONTH FROM CURRENT_DATE)
                    AND EXTRACT(YEAR FROM data_emitere) = EXTRACT(YEAR FROM CURRENT_DATE)";
                decimal revenue = Convert.ToDecimal(db.ExecuteScalar(queryRevenue));

                string queryRecentInvoices = @"SELECT f.numar_factura, c.nume as client,
                    f.total_cu_tva, f.data_emitere, f.stare
                    FROM factura f
                    JOIN client c ON f.id_client = c.id_client
                    ORDER BY f.data_emitere DESC
                    LIMIT 10";

                DataTable dt = db.ExecuteQuery(queryRecentInvoices);
                dgvRecent.DataSource = dt;

                // Update card values
                card1.Value = totalClients.ToString();
                card2.Value = totalProducts.ToString();
                card3.Value = pendingOrders.ToString();
                card4.Value = $"{revenue:0.##} lei";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupRefreshTimer()
        {
            refreshTimer = new Timer();
            refreshTimer.Interval = 60000;
            refreshTimer.Tick += (s, e) => LoadDashboardStats();
            refreshTimer.Start();
        }

        private void BtnClients_Click(object sender, EventArgs e)
        {
            new ClientsForm().ShowDialog();
            LoadDashboardStats();
        }

        private void BtnProducts_Click(object sender, EventArgs e)
        {
            new ProductsForm().ShowDialog();
            LoadDashboardStats();
        }

        private void BtnOrders_Click(object sender, EventArgs e)
        {
            new OrdersForm().ShowDialog();
            LoadDashboardStats();
        }

        private void BtnInvoices_Click(object sender, EventArgs e)
        {
            new InvoicesForm().ShowDialog();
            LoadDashboardStats();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboardStats();
        }
    }
}
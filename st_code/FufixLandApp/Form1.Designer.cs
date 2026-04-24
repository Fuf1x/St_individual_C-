using System.Drawing;
using System.Windows.Forms;

namespace FufixLandApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel dashboardPanel;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Panel gridPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Label recentLabel;
        private System.Windows.Forms.DataGridView dgvRecent;
        private System.Windows.Forms.Button btnClients;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Button btnInvoices;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel logoPanel;
        private System.Windows.Forms.PictureBox logoPicture;
        private CardControl card1;
        private CardControl card2;
        private CardControl card3;
        private CardControl card4;

        // Culori tema
        private Color primaryColor = Color.FromArgb(219, 112, 147);
        private Color secondaryColor = Color.FromArgb(147, 112, 219);
        private Color lightBgColor = Color.FromArgb(255, 245, 250);

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.dashboardPanel = new System.Windows.Forms.Panel();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.gridPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.recentLabel = new System.Windows.Forms.Label();
            this.dgvRecent = new System.Windows.Forms.DataGridView();
            this.logoPanel = new System.Windows.Forms.Panel();
            this.logoPicture = new System.Windows.Forms.PictureBox();
            this.btnClients = new System.Windows.Forms.Button();
            this.btnProducts = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.btnInvoices = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.card1 = new CardControl();
            this.card2 = new CardControl();
            this.card3 = new CardControl();
            this.card4 = new CardControl();

            this.headerPanel.SuspendLayout();
            this.dashboardPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.gridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).BeginInit();
            this.SuspendLayout();

            // headerPanel
            this.headerPanel.BackColor = this.primaryColor;
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Height = 80;
            this.headerPanel.Controls.Add(this.logoPanel);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.subtitleLabel);

            // logoPanel
            this.logoPanel.BackColor = Color.White;
            this.logoPanel.Size = new System.Drawing.Size(60, 60);
            this.logoPanel.Location = new System.Drawing.Point(25, 10);
            this.logoPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPinkPantherLogo);

            // titleLabel
            this.titleLabel.Text = "FUFIX LAND";
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 20, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(100, 15);
            this.titleLabel.AutoSize = true;

            // subtitleLabel
            this.subtitleLabel.Text = "Sistem de Gestiune";
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Regular);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(255, 220, 240);
            this.subtitleLabel.Location = new System.Drawing.Point(105, 48);
            this.subtitleLabel.AutoSize = true;

            // dashboardPanel
            this.dashboardPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.dashboardPanel.Height = 140;
            this.dashboardPanel.Padding = new System.Windows.Forms.Padding(20, 15, 20, 10);
            this.dashboardPanel.BackColor = this.lightBgColor;
            this.dashboardPanel.Controls.Add(this.card1);
            this.dashboardPanel.Controls.Add(this.card2);
            this.dashboardPanel.Controls.Add(this.card3);
            this.dashboardPanel.Controls.Add(this.card4);

            // Card1 - Clienți
            this.card1.Title = "Clienți";
            this.card1.Value = "0";
            this.card1.BackColor = System.Drawing.Color.White;
            this.card1.Size = new System.Drawing.Size(230, 100);
            this.card1.Location = new System.Drawing.Point(20, 20);
            this.card1.SetColor(Color.FromArgb(219, 112, 147));
            this.card1.SetIcon("👥");

            // Card2 - Produse
            this.card2.Title = "Produse Active";
            this.card2.Value = "0";
            this.card2.BackColor = System.Drawing.Color.White;
            this.card2.Size = new System.Drawing.Size(230, 100);
            this.card2.Location = new System.Drawing.Point(270, 20);
            this.card2.SetColor(Color.FromArgb(147, 112, 219));
            this.card2.SetIcon("📦");

            // Card3 - Comenzi în curs
            this.card3.Title = "Comenzi Active";
            this.card3.Value = "0";
            this.card3.BackColor = System.Drawing.Color.White;
            this.card3.Size = new System.Drawing.Size(230, 100);
            this.card3.Location = new System.Drawing.Point(520, 20);
            this.card3.SetColor(Color.FromArgb(255, 105, 180));
            this.card3.SetIcon("🛒");

            // Card4 - Venituri
            this.card4.Title = "Venit Luna Curentă";
            this.card4.Value = "0";
            this.card4.BackColor = System.Drawing.Color.White;
            this.card4.Size = new System.Drawing.Size(230, 100);
            this.card4.Location = new System.Drawing.Point(770, 20);
            this.card4.SetColor(Color.FromArgb(218, 112, 214));
            this.card4.SetIcon("💰");

            // buttonPanel
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonPanel.Height = 60;
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.buttonPanel.BackColor = System.Drawing.Color.White;
            this.buttonPanel.Controls.Add(this.btnClients);
            this.buttonPanel.Controls.Add(this.btnProducts);
            this.buttonPanel.Controls.Add(this.btnOrders);
            this.buttonPanel.Controls.Add(this.btnInvoices);
            this.buttonPanel.Controls.Add(this.btnRefresh);

            // btnClients
            this.btnClients.Text = "👥 Clienți";
            this.btnClients.Location = new System.Drawing.Point(20, 10);
            this.btnClients.Size = new System.Drawing.Size(130, 40);
            this.btnClients.BackColor = this.primaryColor;
            this.btnClients.ForeColor = System.Drawing.Color.White;
            this.btnClients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClients.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            this.btnClients.FlatAppearance.BorderSize = 0;
            this.btnClients.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClients.Click += new System.EventHandler(this.BtnClients_Click);

            // btnProducts
            this.btnProducts.Text = "📦 Produse";
            this.btnProducts.Location = new System.Drawing.Point(165, 10);
            this.btnProducts.Size = new System.Drawing.Size(130, 40);
            this.btnProducts.BackColor = this.secondaryColor;
            this.btnProducts.ForeColor = System.Drawing.Color.White;
            this.btnProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProducts.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            this.btnProducts.FlatAppearance.BorderSize = 0;
            this.btnProducts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProducts.Click += new System.EventHandler(this.BtnProducts_Click);

            // btnOrders
            this.btnOrders.Text = "🛒 Comenzi";
            this.btnOrders.Location = new System.Drawing.Point(310, 10);
            this.btnOrders.Size = new System.Drawing.Size(130, 40);
            this.btnOrders.BackColor = Color.FromArgb(255, 105, 180);
            this.btnOrders.ForeColor = System.Drawing.Color.White;
            this.btnOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrders.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            this.btnOrders.FlatAppearance.BorderSize = 0;
            this.btnOrders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOrders.Click += new System.EventHandler(this.BtnOrders_Click);

            // btnInvoices
            this.btnInvoices.Text = "📄 Facturi";
            this.btnInvoices.Location = new System.Drawing.Point(455, 10);
            this.btnInvoices.Size = new System.Drawing.Size(130, 40);
            this.btnInvoices.BackColor = Color.FromArgb(218, 112, 214);
            this.btnInvoices.ForeColor = System.Drawing.Color.White;
            this.btnInvoices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInvoices.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            this.btnInvoices.FlatAppearance.BorderSize = 0;
            this.btnInvoices.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInvoices.Click += new System.EventHandler(this.BtnInvoices_Click);

            // btnRefresh
            this.btnRefresh.Text = "⟳ Reîmprospătează";
            this.btnRefresh.Location = new System.Drawing.Point(600, 10);
            this.btnRefresh.Size = new System.Drawing.Size(150, 40);
            this.btnRefresh.BackColor = Color.FromArgb(128, 0, 128);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);

            // gridPanel
            this.gridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPanel.Padding = new System.Windows.Forms.Padding(20);
            this.gridPanel.BackColor = this.lightBgColor;
            this.gridPanel.Controls.Add(this.recentLabel);
            this.gridPanel.Controls.Add(this.dgvRecent);

            // recentLabel
            this.recentLabel.Text = "📋 Ultimele facturi";
            this.recentLabel.Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold);
            this.recentLabel.ForeColor = this.darkColor;
            this.recentLabel.Location = new System.Drawing.Point(20, 15);
            this.recentLabel.AutoSize = true;

            // dgvRecent
            this.dgvRecent.Location = new System.Drawing.Point(20, 50);
            this.dgvRecent.Size = new System.Drawing.Size(970, 320);
            this.dgvRecent.AllowUserToAddRows = false;
            this.dgvRecent.AllowUserToDeleteRows = false;
            this.dgvRecent.ReadOnly = true;
            this.dgvRecent.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecent.RowHeadersVisible = false;
            this.dgvRecent.EnableHeadersVisualStyles = false;
            this.dgvRecent.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            this.dgvRecent.ColumnHeadersDefaultCellStyle.BackColor = this.primaryColor;
            this.dgvRecent.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvRecent.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9);
            this.dgvRecent.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 240, 245);
            this.dgvRecent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRecent.GridColor = Color.FromArgb(230, 230, 235);

            // MainForm
            this.Text = "Fufix Land - Sistem de Gestiune";
            this.Size = new System.Drawing.Size(1100, 650);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.White;

            // Adauga controalele
            this.Controls.Add(this.gridPanel);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.dashboardPanel);
            this.Controls.Add(this.headerPanel);

            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.dashboardPanel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.gridPanel.ResumeLayout(false);
            this.gridPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).EndInit();
            this.ResumeLayout(false);
        }

        private void DrawPinkPantherLogo(object sender, PaintEventArgs e)
        {
            Panel pnl = sender as Panel;
            if (pnl != null)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Desenează o panteră stilizată roz
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(5, 5, pnl.Width - 10, pnl.Height - 10);

                // Fundal
                using (System.Drawing.SolidBrush bgBrush = new System.Drawing.SolidBrush(Color.FromArgb(219, 112, 147)))
                {
                    e.Graphics.FillEllipse(bgBrush, rect);
                }

                // Urechi
                Point[] leftEar = { new Point(12, 5), new Point(22, 0), new Point(22, 15) };
                Point[] rightEar = { new Point(pnl.Width - 22, 0), new Point(pnl.Width - 12, 5), new Point(pnl.Width - 22, 15) };

                using (System.Drawing.SolidBrush earBrush = new System.Drawing.SolidBrush(Color.FromArgb(255, 105, 180)))
                {
                    e.Graphics.FillPolygon(earBrush, leftEar);
                    e.Graphics.FillPolygon(earBrush, rightEar);
                }

                // Ochi
                using (System.Drawing.SolidBrush eyeBrush = new System.Drawing.SolidBrush(Color.White))
                {
                    e.Graphics.FillEllipse(eyeBrush, 15, 20, 8, 8);
                    e.Graphics.FillEllipse(eyeBrush, pnl.Width - 23, 20, 8, 8);
                }

                using (System.Drawing.SolidBrush pupilBrush = new System.Drawing.SolidBrush(Color.Black))
                {
                    e.Graphics.FillEllipse(pupilBrush, 17, 21, 4, 4);
                    e.Graphics.FillEllipse(pupilBrush, pnl.Width - 21, 21, 4, 4);
                }

                // Nas
                using (System.Drawing.SolidBrush noseBrush = new System.Drawing.SolidBrush(Color.FromArgb(255, 50, 100)))
                {
                    e.Graphics.FillEllipse(noseBrush, pnl.Width / 2 - 5, 28, 10, 8);
                }

                // Text "P"
                using (System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold))
                using (System.Drawing.SolidBrush textBrush = new System.Drawing.SolidBrush(Color.White))
                {
                    e.Graphics.DrawString("P", font, textBrush, pnl.Width / 2 - 6, 40);
                }
            }
        }
    }

    public class CardControl : System.Windows.Forms.Panel
    {
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Panel colorBar;
        private System.Windows.Forms.Label lblIcon;

        public string Title
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        public string Value
        {
            get { return lblValue.Text; }
            set { lblValue.Text = value; }
        }

        public CardControl()
        {
            this.Size = new System.Drawing.Size(230, 100);
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;

            this.colorBar = new System.Windows.Forms.Panel();
            this.lblIcon = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();

            this.colorBar.Size = new System.Drawing.Size(5, this.Height);
            this.colorBar.Location = new System.Drawing.Point(0, 0);
            this.colorBar.BackColor = System.Drawing.Color.FromArgb(219, 112, 147);

            this.lblIcon.Text = "📊";
            this.lblIcon.Font = new System.Drawing.Font("Segoe UI", 22);
            this.lblIcon.ForeColor = System.Drawing.Color.FromArgb(180, 180, 190);
            this.lblIcon.Location = new System.Drawing.Point(175, 20);
            this.lblIcon.AutoSize = true;

            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(120, 120, 130);
            this.lblTitle.Location = new System.Drawing.Point(15, 25);
            this.lblTitle.AutoSize = true;

            this.lblValue.Font = new System.Drawing.Font("Segoe UI", 22, System.Drawing.FontStyle.Bold);
            this.lblValue.ForeColor = System.Drawing.Color.FromArgb(219, 112, 147);
            this.lblValue.Location = new System.Drawing.Point(15, 55);
            this.lblValue.AutoSize = true;

            this.Controls.Add(this.colorBar);
            this.Controls.Add(this.lblIcon);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblValue);
        }

        public void SetColor(System.Drawing.Color color)
        {
            this.colorBar.BackColor = color;
            this.lblValue.ForeColor = color;
        }

        public void SetIcon(string icon)
        {
            this.lblIcon.Text = icon;
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            this.colorBar.Height = this.Height;
        }
    }
}
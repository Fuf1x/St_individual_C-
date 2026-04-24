using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace FufixLandApp
{
    public class ClientsForm : Form
    {
        private DatabaseHelper db;
        private DataGridView dgvClients;
        private TextBox txtNume, txtTelefon, txtEmail, txtAdresa;
        private ComboBox cmbTipClient;
        private Button btnAdd, btnUpdate, btnDelete, btnRefresh;
        private Panel inputPanel;
        private Label titleLabel;

        private Color primaryColor = Color.FromArgb(219, 112, 147);
        private Color secondaryColor = Color.FromArgb(147, 112, 219);

        public ClientsForm()
        {
            db = new DatabaseHelper();
            InitializeComponent();
            LoadClients();
        }

        private void InitializeComponent()
        {
            this.Text = "Gestionare Clienți - Fufix Land";
            this.Size = new Size(1000, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(255, 245, 250);
            this.Icon = null;

            // inputPanel
            this.inputPanel = new Panel();
            this.inputPanel.Dock = DockStyle.Top;
            this.inputPanel.Height = 200;
            this.inputPanel.Padding = new Padding(20);
            this.inputPanel.BackColor = Color.White;
            this.inputPanel.BorderStyle = BorderStyle.FixedSingle;

            // titleLabel
            this.titleLabel = new Label();
            this.titleLabel.Text = "📝 Date Client";
            this.titleLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            this.titleLabel.ForeColor = this.primaryColor;
            this.titleLabel.Location = new Point(20, 10);
            this.titleLabel.AutoSize = true;

            // Label si TextBox pentru Nume
            Label lblNume = new Label();
            lblNume.Text = "Nume / Denumire:";
            lblNume.Location = new Point(20, 50);
            lblNume.Size = new Size(120, 25);
            lblNume.ForeColor = this.secondaryColor;

            this.txtNume = new TextBox();
            this.txtNume.Location = new Point(150, 50);
            this.txtNume.Size = new Size(250, 25);

            // Label si ComboBox pentru Tip Client
            Label lblTip = new Label();
            lblTip.Text = "Tip Client:";
            lblTip.Location = new Point(20, 85);
            lblTip.Size = new Size(120, 25);
            lblTip.ForeColor = this.secondaryColor;

            this.cmbTipClient = new ComboBox();
            this.cmbTipClient.Location = new Point(150, 85);
            this.cmbTipClient.Size = new Size(250, 25);
            this.cmbTipClient.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTipClient.Items.AddRange(new object[] { "persoana fizica", "persoana juridica" });
            this.cmbTipClient.SelectedIndex = 0;

            // Label si TextBox pentru Telefon
            Label lblTelefon = new Label();
            lblTelefon.Text = "Telefon:";
            lblTelefon.Location = new Point(20, 120);
            lblTelefon.Size = new Size(120, 25);
            lblTelefon.ForeColor = this.secondaryColor;

            this.txtTelefon = new TextBox();
            this.txtTelefon.Location = new Point(150, 120);
            this.txtTelefon.Size = new Size(250, 25);

            // Label si TextBox pentru Email
            Label lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Location = new Point(450, 50);
            lblEmail.Size = new Size(100, 25);
            lblEmail.ForeColor = this.secondaryColor;

            this.txtEmail = new TextBox();
            this.txtEmail.Location = new Point(560, 50);
            this.txtEmail.Size = new Size(250, 25);

            // Label si TextBox pentru Adresa
            Label lblAdresa = new Label();
            lblAdresa.Text = "Adresa:";
            lblAdresa.Location = new Point(450, 85);
            lblAdresa.Size = new Size(100, 25);
            lblAdresa.ForeColor = this.secondaryColor;

            this.txtAdresa = new TextBox();
            this.txtAdresa.Location = new Point(560, 85);
            this.txtAdresa.Size = new Size(250, 25);

            // Butoane
            this.btnAdd = new Button();
            this.btnAdd.Text = "➕ Adaugă";
            this.btnAdd.Location = new Point(150, 155);
            this.btnAdd.Size = new Size(110, 35);
            this.btnAdd.BackColor = this.primaryColor;
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.Click += BtnAdd_Click;

            this.btnUpdate = new Button();
            this.btnUpdate.Text = "✏️ Actualizează";
            this.btnUpdate.Location = new Point(275, 155);
            this.btnUpdate.Size = new Size(120, 35);
            this.btnUpdate.BackColor = this.secondaryColor;
            this.btnUpdate.ForeColor = Color.White;
            this.btnUpdate.FlatStyle = FlatStyle.Flat;
            this.btnUpdate.Click += BtnUpdate_Click;

            this.btnDelete = new Button();
            this.btnDelete.Text = "🗑️ Șterge";
            this.btnDelete.Location = new Point(410, 155);
            this.btnDelete.Size = new Size(110, 35);
            this.btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Click += BtnDelete_Click;

            this.btnRefresh = new Button();
            this.btnRefresh.Text = "🔄 Reîmprospătează";
            this.btnRefresh.Location = new Point(535, 155);
            this.btnRefresh.Size = new Size(140, 35);
            this.btnRefresh.BackColor = Color.FromArgb(128, 0, 128);
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.Click += (s, e) => LoadClients();

            // Adauga controale in inputPanel
            this.inputPanel.Controls.Add(this.titleLabel);
            this.inputPanel.Controls.Add(lblNume);
            this.inputPanel.Controls.Add(this.txtNume);
            this.inputPanel.Controls.Add(lblTip);
            this.inputPanel.Controls.Add(this.cmbTipClient);
            this.inputPanel.Controls.Add(lblTelefon);
            this.inputPanel.Controls.Add(this.txtTelefon);
            this.inputPanel.Controls.Add(lblEmail);
            this.inputPanel.Controls.Add(this.txtEmail);
            this.inputPanel.Controls.Add(lblAdresa);
            this.inputPanel.Controls.Add(this.txtAdresa);
            this.inputPanel.Controls.Add(this.btnAdd);
            this.inputPanel.Controls.Add(this.btnUpdate);
            this.inputPanel.Controls.Add(this.btnDelete);
            this.inputPanel.Controls.Add(this.btnRefresh);

            // DataGridView
            this.dgvClients = new DataGridView();
            this.dgvClients.Dock = DockStyle.Fill;
            this.dgvClients.AllowUserToAddRows = false;
            this.dgvClients.AllowUserToDeleteRows = false;
            this.dgvClients.ReadOnly = true;
            this.dgvClients.BackgroundColor = Color.White;
            this.dgvClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvClients.SelectionChanged += DgvCli
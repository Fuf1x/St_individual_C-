using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CompanyITApp
{
    public partial class Form1 : Form
    {
        // ---------- Controale UI ----------
        private MenuStrip mainMenu;
        private ToolStripMenuItem fileMenu, editMenu, helpMenu;
        private ToolStripMenuItem exitMenuItem, clearMenuItem, aboutMenuItem, exportMenuItem;

        private GroupBox employeeGroupBox;
        private TableLayoutPanel tableLayout; // pentru aranjare corectă
        private Label nameLabel, roleLabel, salaryLabel, descLabel;
        private TextBox nameTextBox, salaryTextBox;
        private RichTextBox descriptionRichTextBox;
        private Panel rolePanel; // pentru radio buttons
        private RadioButton developerRadio, testerRadio, managerRadio;
        private Button addButton, editButton, deleteButton, clearButton;
        private DataGridView employeesDataGridView;

        // Controale căutare + statistici
        private TextBox searchTextBox;
        private Button searchButton;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        private List<Employee> employees = new List<Employee>();
        private List<Employee> filteredEmployees = new List<Employee>();
        private Employee currentEmployee;

        public Form1()
        {
            this.Text = "✨ Companie IT - Gestionare Angajați ✨";
            this.Size = new Size(1050, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.IsMdiContainer = true;
            this.BackColor = Color.White;
            this.Paint += Form1_Paint;

            InitializeMenu();
            InitializeControls();
            LoadSampleData();
            ApplyFilter(); // inițial afișează toți
            UpdateStatistics();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                Color.FromArgb(240, 248, 255),
                Color.FromArgb(230, 230, 250),
                90f))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void InitializeMenu()
        {
            mainMenu = new MenuStrip();
            mainMenu.BackColor = Color.FromArgb(50, 50, 70);
            mainMenu.ForeColor = Color.White;
            mainMenu.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            fileMenu = new ToolStripMenuItem("📁 Fișier");
            editMenu = new ToolStripMenuItem("✏️ Editare");
            helpMenu = new ToolStripMenuItem("❓ Ajutor");

            exitMenuItem = new ToolStripMenuItem("🚪 Ieșire");
            clearMenuItem = new ToolStripMenuItem("🧹 Curăță câmpuri");
            aboutMenuItem = new ToolStripMenuItem("ℹ️ Despre");
            exportMenuItem = new ToolStripMenuItem("💾 Exportă în CSV");

            exitMenuItem.Click += (s, e) => Application.Exit();
            clearMenuItem.Click += (s, e) => ClearInputFields();
            aboutMenuItem.Click += (s, e) => MessageBox.Show("Aplicație modernă pentru gestionarea angajaților IT\nFuncții: căutare, statistici, export CSV", "Despre");
            exportMenuItem.Click += ExportToCsv;

            fileMenu.DropDownItems.Add(exportMenuItem);
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(exitMenuItem);
            editMenu.DropDownItems.Add(clearMenuItem);
            helpMenu.DropDownItems.Add(aboutMenuItem);

            mainMenu.Items.Add(fileMenu);
            mainMenu.Items.Add(editMenu);
            mainMenu.Items.Add(helpMenu);

            this.Controls.Add(mainMenu);
            this.MainMenuStrip = mainMenu;
        }

        private void InitializeControls()
        {
            // GroupBox principal
            employeeGroupBox = new GroupBox()
            {
                Text = "📋 Date angajat",
                Location = new Point(25, 45),
                Size = new Size(420, 300),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.DarkSlateBlue,
                Padding = new Padding(10)
            };

            // TableLayoutPanel cu 2 coloane (etichetă | control)
            tableLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5,
                AutoSize = true,
                Padding = new Padding(5)
            };
            // Setări coloane: prima coloană lățime fixă (100px), a doua se întinde
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            // Rânduri: înălțime automată
            for (int i = 0; i < 5; i++)
                tableLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // --- Rând 0: Nume ---
            nameLabel = new Label() { Text = "👤 Nume:", Font = new Font("Segoe UI", 10, FontStyle.Bold), TextAlign = ContentAlignment.MiddleRight, AutoSize = true };
            nameTextBox = new TextBox() { Font = new Font("Segoe UI", 10), BorderStyle = BorderStyle.FixedSingle, Anchor = AnchorStyles.Left | AnchorStyles.Right };
            tableLayout.Controls.Add(nameLabel, 0, 0);
            tableLayout.Controls.Add(nameTextBox, 1, 0);

            // --- Rând 1: Rol (etichetă + panel cu radio buttons) ---
            roleLabel = new Label() { Text = "💼 Rol:", Font = new Font("Segoe UI", 10, FontStyle.Bold), TextAlign = ContentAlignment.MiddleRight, AutoSize = true };
            rolePanel = new Panel() { Height = 40, AutoSize = true };
            developerRadio = new RadioButton() { Text = "👨‍💻 Developer", AutoSize = true, Location = new Point(0, 5) };
            testerRadio = new RadioButton() { Text = "🧪 Tester", AutoSize = true, Location = new Point(110, 5) };
            managerRadio = new RadioButton() { Text = "📊 Manager", AutoSize = true, Location = new Point(220, 5) };
            rolePanel.Controls.AddRange(new Control[] { developerRadio, testerRadio, managerRadio });
            tableLayout.Controls.Add(roleLabel, 0, 1);
            tableLayout.Controls.Add(rolePanel, 1, 1);

            // --- Rând 2: Salariu ---
            salaryLabel = new Label() { Text = "💰 Salariu (MDL):", Font = new Font("Segoe UI", 10, FontStyle.Bold), TextAlign = ContentAlignment.MiddleRight, AutoSize = true };
            salaryTextBox = new TextBox() { Font = new Font("Segoe UI", 10), BorderStyle = BorderStyle.FixedSingle, Anchor = AnchorStyles.Left | AnchorStyles.Right };
            tableLayout.Controls.Add(salaryLabel, 0, 2);
            tableLayout.Controls.Add(salaryTextBox, 1, 2);

            // --- Rând 3: Descriere ---
            descLabel = new Label() { Text = "📝 Descriere:", Font = new Font("Segoe UI", 10, FontStyle.Bold), TextAlign = ContentAlignment.MiddleRight, AutoSize = true };
            descriptionRichTextBox = new RichTextBox() { Font = new Font("Segoe UI", 10), BorderStyle = BorderStyle.FixedSingle, Height = 60, Anchor = AnchorStyles.Left | AnchorStyles.Right };
            tableLayout.Controls.Add(descLabel, 0, 3);
            tableLayout.Controls.Add(descriptionRichTextBox, 1, 3);

            // Rând 4 gol (nu mai avem nevoie, dar las pentru simetrie)
            tableLayout.RowCount = 4; // corectăm: de fapt avem 4 rânduri

            employeeGroupBox.Controls.Add(tableLayout);

            // --- Butoanele principale (adaugă, modifică, șterge, curăță) ---
            addButton = CreateStyledButton("➕ Adaugă", 40, 360, Color.LimeGreen);
            editButton = CreateStyledButton("✏️ Modifică", 140, 360, Color.DodgerBlue);
            deleteButton = CreateStyledButton("🗑️ Șterge", 240, 360, Color.Crimson);
            clearButton = CreateStyledButton("🧽 Curăță", 340, 360, Color.Orange);

            addButton.Click += AddButton_Click;
            editButton.Click += EditButton_Click;
            deleteButton.Click += DeleteButton_Click;
            clearButton.Click += (s, e) => ClearInputFields();

            // --- DataGridView ---
            employeesDataGridView = new DataGridView()
            {
                Location = new Point(460, 50),
                Size = new Size(540, 400),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                GridColor = Color.LightGray,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 10)
            };
            employeesDataGridView.SelectionChanged += DataGridView_SelectionChanged;
            employeesDataGridView.EnableHeadersVisualStyles = false;
            employeesDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            employeesDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            employeesDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            employeesDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);
            employeesDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            employeesDataGridView.DefaultCellStyle.SelectionForeColor = Color.White;

            // --- Căutare ---
            Label searchLabel = new Label()
            {
                Text = "🔎 Căutare:",
                Location = new Point(460, 460),
                Size = new Size(70, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DarkSlateGray
            };
            searchTextBox = new TextBox()
            {
                Location = new Point(535, 458),
                Size = new Size(200, 28),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };
            searchButton = new Button()
            {
                Text = "Filtrează",
                Location = new Point(745, 456),
                Size = new Size(90, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            searchButton.FlatAppearance.BorderSize = 0;
            searchButton.Click += SearchButton_Click;

            // --- Bara de stare ---
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            statusLabel.Spring = true;
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            statusStrip.Items.Add(statusLabel);
            statusStrip.BackColor = Color.FromArgb(52, 73, 94);
            statusStrip.ForeColor = Color.White;

            // Adăugare controale pe formular
            this.Controls.Add(employeeGroupBox);
            this.Controls.Add(addButton);
            this.Controls.Add(editButton);
            this.Controls.Add(deleteButton);
            this.Controls.Add(clearButton);
            this.Controls.Add(employeesDataGridView);
            this.Controls.Add(searchLabel);
            this.Controls.Add(searchTextBox);
            this.Controls.Add(searchButton);
            this.Controls.Add(statusStrip);

            editButton.Enabled = false;
            deleteButton.Enabled = false;
        }

        // Helper pentru butoane rotunjite (același ca înainte)
        private Button CreateStyledButton(string text, int x, int y, Color backColor)
        {
            Button btn = new Button()
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(95, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = backColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Paint += (sender, e) =>
            {
                GraphicsPath path = new GraphicsPath();
                int radius = 15;
                Rectangle rect = new Rectangle(0, 0, btn.Width - 1, btn.Height - 1);
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                btn.Region = new Region(path);
            };
            btn.MouseEnter += (sender, e) => btn.BackColor = ControlPaint.Light(backColor, 0.7f);
            btn.MouseLeave += (sender, e) => btn.BackColor = backColor;
            return btn;
        }

        // ---------- Clasa Employee ----------
        public class Employee
        {
            public string Name { get; set; }
            public string Role { get; set; }
            public int Salary { get; set; }
            public string Description { get; set; }
        }

        private void LoadSampleData()
        {
            employees.Clear();
            employees.Add(new Employee { Name = "Ana Popescu", Role = "Developer", Salary = 25000, Description = "Backend .NET" });
            employees.Add(new Employee { Name = "Ion Creangă", Role = "Tester", Salary = 18000, Description = "Testare automată" });
            employees.Add(new Employee { Name = "Maria Ionescu", Role = "Manager", Salary = 32000, Description = "Conduce echipa" });
            filteredEmployees = new List<Employee>(employees);
        }

        private void ApplyFilter()
        {
            string searchTerm = searchTextBox.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(searchTerm))
                filteredEmployees = new List<Employee>(employees);
            else
                filteredEmployees = employees.Where(emp => emp.Name.ToLower().Contains(searchTerm) || emp.Role.ToLower().Contains(searchTerm)).ToList();

            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            employeesDataGridView.DataSource = null;
            employeesDataGridView.DataSource = filteredEmployees;
            employeesDataGridView.AutoResizeColumns();
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            int count = filteredEmployees.Count;
            double avgSalary = count > 0 ? filteredEmployees.Average(e => e.Salary) : 0;
            statusLabel.Text = $"📊 Total angajați: {count}  |  💰 Salariu mediu: {avgSalary:F2} MDL";
        }

        private void ClearInputFields()
        {
            nameTextBox.Clear();
            salaryTextBox.Clear();
            descriptionRichTextBox.Clear();
            developerRadio.Checked = false;
            testerRadio.Checked = false;
            managerRadio.Checked = false;
            currentEmployee = null;
            editButton.Enabled = false;
            deleteButton.Enabled = false;
        }

        private string GetSelectedRole()
        {
            if (developerRadio.Checked) return "Developer";
            if (testerRadio.Checked) return "Tester";
            if (managerRadio.Checked) return "Manager";
            return "";
        }

        private bool ValidateAndConvert(out int salary)
        {
            salary = 0;
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show("Introduceți numele!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(salaryTextBox.Text))
            {
                MessageBox.Show("Introduceți salariul!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!int.TryParse(salaryTextBox.Text, out salary) || salary <= 0)
            {
                MessageBox.Show("Salariul trebuie să fie un număr întreg pozitiv!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string role = GetSelectedRole();
            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Selectați un rol!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (ValidateAndConvert(out int salary))
            {
                Employee emp = new Employee
                {
                    Name = nameTextBox.Text.Trim(),
                    Role = GetSelectedRole(),
                    Salary = salary,
                    Description = descriptionRichTextBox.Text.Trim()
                };
                employees.Add(emp);
                ApplyFilter();
                ClearInputFields();
                MessageBox.Show("✅ Angajat adăugat cu succes!", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (currentEmployee == null)
            {
                MessageBox.Show("Selectați un angajat din listă pentru modificare!", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ValidateAndConvert(out int salary))
            {
                currentEmployee.Name = nameTextBox.Text.Trim();
                currentEmployee.Role = GetSelectedRole();
                currentEmployee.Salary = salary;
                currentEmployee.Description = descriptionRichTextBox.Text.Trim();
                ApplyFilter();
                ClearInputFields();
                MessageBox.Show("✏️ Date modificate corect!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (currentEmployee == null)
            {
                MessageBox.Show("Selectați un angajat din listă pentru ștergere!", "Avertisment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show($"Sigur doriți să ștergeți angajatul {currentEmployee.Name}?", "Confirmare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                employees.Remove(currentEmployee);
                ApplyFilter();
                ClearInputFields();
                MessageBox.Show("🗑️ Angajat șters.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ExportToCsv(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.Title = "Exportă lista angajaților";
                sfd.FileName = "angajati_export.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var lines = new List<string> { "Nume;Rol;Salariu;Descriere" };
                        lines.AddRange(filteredEmployees.Select(emp => $"{emp.Name};{emp.Role};{emp.Salary};{emp.Description}"));
                        File.WriteAllLines(sfd.FileName, lines);
                        MessageBox.Show($"Export reușit!\nFișier salvat la: {sfd.FileName}", "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Eroare la export: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (employeesDataGridView.SelectedRows.Count > 0)
            {
                currentEmployee = (Employee)employeesDataGridView.SelectedRows[0].DataBoundItem;
                if (currentEmployee != null)
                {
                    nameTextBox.Text = currentEmployee.Name;
                    salaryTextBox.Text = currentEmployee.Salary.ToString();
                    descriptionRichTextBox.Text = currentEmployee.Description;
                    switch (currentEmployee.Role)
                    {
                        case "Developer": developerRadio.Checked = true; break;
                        case "Tester": testerRadio.Checked = true; break;
                        case "Manager": managerRadio.Checked = true; break;
                    }
                    editButton.Enabled = true;
                    deleteButton.Enabled = true;
                }
            }
            else
            {
                editButton.Enabled = false;
                deleteButton.Enabled = false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Form child = new Form();
            child.Text = "🌟 Informații companie";
            child.Size = new Size(350, 200);
            child.MdiParent = this;
            child.BackColor = Color.FromArgb(240, 248, 255);
            Label info = new Label()
            {
                Text = "Aplicație modernă pentru gestionarea angajaților.\nMDI, controale de bază, design atractiv.\nFuncții: căutare, statistici, export CSV.\nCreat pentru studiul individual.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.DarkSlateBlue
            };
            child.Controls.Add(info);
            child.Show();
        }
    }
}
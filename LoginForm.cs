
using System;
using System.Drawing;
using System.Windows.Forms;

namespace JayM
{
    public partial class LoginForm : Form
    {
        private TextBox txtUsername = new();
        private TextBox txtPassword = new();

        public LoginForm()
        {
            InitializeComponent();

            Text = "JAY-M - Login";
            Size = new Size(430, 440);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(238, 242, 255);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Label title = new Label
            {
                Text = "JAY-M",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 48, 163),
                Location = new Point(65, 35),
                Size = new Size(300, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label subtitle = new Label
            {
                Text = "Welcome back!",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.Gray,
                Location = new Point(65, 90),
                Size = new Size(300, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            AddField("Username", txtUsername, 145);
            AddField("Password", txtPassword, 225);

            txtPassword.UseSystemPasswordChar = true;

            Button btnLogin = CreateButton("LOGIN", 100, 300);
            Button btnRegister = CreateButton(
                "CREATE ACCOUNT", 100, 350);

            btnLogin.Click += Login_Click;
            btnRegister.Click += Register_Click;

            Controls.Add(title);
            Controls.Add(subtitle);
            Controls.Add(btnLogin);
            Controls.Add(btnRegister);
        }

        private void AddField(
            string labelText, TextBox textBox, int top)
        {
            Label label = new Label
            {
                Text = labelText,
                Location = new Point(45, top),
                Size = new Size(300, 22),
                Font = new Font("Segoe UI", 10)
            };

            textBox.Location = new Point(45, top + 25);
            textBox.Size = new Size(320, 30);
            textBox.Font = new Font("Segoe UI", 11);

            Controls.Add(label);
            Controls.Add(textBox);
        }

        private Button CreateButton(string text, int x, int y)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(220, 40),
                BackColor = Color.FromArgb(55, 48, 163),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private void Login_Click(object? sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (username == "" || password == "")
            {
                MessageBox.Show(
                    "Enter your username and password.");
                return;
            }

            try
            {
                var user = DatabaseHelper.FindUser(username);

                if (user.HasValue &&
                    PasswordHasher.VerifyPassword(
                        password, user.Value.PasswordHash))
                {
                    MessageBox.Show(
                        "Login successful!",
                        "JAY-M",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Hide();

                    using (DashboardForm dashboard =
                        new DashboardForm(user.Value.FullName))
                    {
                        dashboard.ShowDialog();
                    }

                    txtPassword.Clear();
                    Show();
                }
                else
                {
                    MessageBox.Show(
                        "Incorrect username or password.",
                        "Login Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Database connection failed:\n" + ex.Message,
                    "JAY-M Error");
            }
        }

        private void Register_Click(object? sender, EventArgs e)
        {
            using RegistrationForm registration =
                new RegistrationForm();

            registration.ShowDialog();
        }

        // FIX: Missing Designer Load event
        private void LoginForm_Load(object? sender, EventArgs e)
        {
        }
    }
}

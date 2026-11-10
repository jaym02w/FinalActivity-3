
using System;
using System.Drawing;
using System.Data.OleDb;
using System.Windows.Forms;

namespace JayM
{
    public partial class RegistrationForm : Form
    {
        private TextBox txtFullName = new();
        private TextBox txtUsername = new();
        private TextBox txtPassword = new();
        private TextBox txtConfirmPassword = new();

        public RegistrationForm()
        {
            InitializeComponent();

            Text = "JAY-M - Registration";
            Size = new Size(430, 570);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(238, 242, 255);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Label title = new Label
            {
                Text = "CREATE ACCOUNT",
                Font = new Font("Segoe UI", 19, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 48, 163),
                Location = new Point(65, 30),
                Size = new Size(300, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Controls.Add(title);

            AddField("Full Name", txtFullName, 100);
            AddField("Username", txtUsername, 185);
            AddField("Password", txtPassword, 270);
            AddField("Confirm Password", txtConfirmPassword, 355);

            txtPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.UseSystemPasswordChar = true;

            Button btnRegister = CreateButton("REGISTER", 100, 445);

            btnRegister.Click += Register_Click;

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
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(30, 27, 75)
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
                Size = new Size(220, 42),
                BackColor = Color.FromArgb(55, 48, 163),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private void Register_Click(object? sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirm = txtConfirmPassword.Text;

            if (fullName == "" || username == "" ||
                password == "" || confirm == "")
            {
                MessageBox.Show("Please complete all fields.");
                return;
            }

            if (fullName.Length > 100 || username.Length > 50)
            {
                MessageBox.Show("Name or username is too long.");
                return;
            }

            if (password.Length < 8 || password.Length > 256)
            {
                MessageBox.Show(
                    "Password must contain 8 to 256 characters.");
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }

            try
            {
                bool registered = DatabaseHelper.RegisterUser(
     fullName, username, password);

                if (registered)
                {
                    MessageBox.Show("Registration successful!");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "Username already exists. Please choose another.",
                        "Registration Failed");
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(
                    "Registration failed. Check your database " +
                    "or username.\n\n" + ex.Message,
                    "Registration Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        // FIX: Missing Designer Load event
        private void RegistrationForm_Load(object? sender, EventArgs e)
        {
        }
    }
}

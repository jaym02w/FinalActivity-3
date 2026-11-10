
using System;
using System.Drawing;
using System.Windows.Forms;

namespace JayM
{
    public partial class DashboardForm : Form
    {
        public DashboardForm(string fullName)
        {
            InitializeComponent();

            Text = "JAY-M - Dashboard";
            Size = new Size(600, 400);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(238, 242, 255);

            Label heading = new Label
            {
                Text = "JAY-M",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 48, 163),
                Location = new Point(100, 60),
                Size = new Size(400, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label welcome = new Label
            {
                Text = "Welcome, " + fullName + "!",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 27, 75),
                Location = new Point(80, 145),
                Size = new Size(440, 55),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label message = new Label
            {
                Text = "You have successfully logged in.",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.Gray,
                Location = new Point(100, 205),
                Size = new Size(400, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Button btnLogout = new Button
            {
                Text = "LOG OUT",
                Location = new Point(190, 275),
                Size = new Size(200, 45),
                BackColor = Color.FromArgb(55, 48, 163),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            btnLogout.Click += (sender, e) =>
            {
                Close();
            };

            Controls.Add(heading);
            Controls.Add(welcome);
            Controls.Add(message);
            Controls.Add(btnLogout);
        }

        // FIX: Missing Designer Load event
        private void DashboardForm_Load(object? sender, EventArgs e)
        {
        }
    }
}

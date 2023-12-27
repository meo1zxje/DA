using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DA
{
    public partial class DangNhap : Form
    {
        List<CNhanVien> dsNV;
        List<CNhanVien> tam;
        private string tempFilePath;
        public DangNhap()
        {
            InitializeComponent();
            dsNV = new List<CNhanVien>();
            tam= new List<CNhanVien>();
            LoadNhanVienList();
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            SetRoundedPanel(panel1, 20);
            SetRoundedButton(btnDangNhap, 20);
            SetHintText(txtUser, "User");
            SetHintText(txtPass, "Password");
            this.ActiveControl = null;
            txtPass.TextChanged += txtPass_TextChanged;
            txtPass.Click += txtPass_Click;
            txtPass_Leave(null, EventArgs.Empty);
            tempFilePath = "dstam.bin";

            if (!File.Exists(tempFilePath))
            {
                LuuDanhSachTamThoiVaoFile(tempFilePath, new List<CNhanVien>());
            }
            this.FormClosing += DangNhap_FormClosing;
        }
        
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }
        private void SetHintText(TextBox textBox, string hintText)
        {
            textBox.Text = hintText;
            textBox.ForeColor = SystemColors.GrayText;

            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == hintText)
                {
                    textBox.Text = "";
                    textBox.ForeColor = SystemColors.ControlText;
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = hintText;
                    textBox.ForeColor = SystemColors.GrayText;
                }
            };
        }
        private void PerformLogin()
        {
            string username = txtUser.Text;
            string password = txtPass.Text;

            if (username == "User" || password == "Password")
            {
                MessageBox.Show("Vui lòng nhập thông tin đăng nhập.");
            }
            else if (username == "admin" && password == "admin")
            {
                LuuDanhSachTamThoiVaoFile(tempFilePath, new List<CNhanVien>());
                Menu frm = new Menu();
                frm.ShowDialog();
            }
            else 
            {
                CNhanVien matchedNhanVien = dsNV.FirstOrDefault(nv => nv.MaNV == username && password == "123");

                if (matchedNhanVien != null)
                {
                    tam.Add(matchedNhanVien);
                    LuuDanhSachTamThoiVaoFile("dstam.bin", tam);
                    Menu frm = new Menu();
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Sai Mã NV hoặc Mật khẩu! Hãy kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void LuuDanhSachTamThoiVaoFile(string filePath, List<CNhanVien> danhSach)
        {
            try
            {
                using (FileStream writerFileStream = new FileStream(filePath, FileMode.Create, System.IO.FileAccess.Write))
                {
                    BinaryFormatter biFormatter = new BinaryFormatter();
                    biFormatter.Serialize(writerFileStream, danhSach);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu danh sách tạm thời: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private List<CNhanVien> ReadNhanVienFromFile(string filePath)
        {
            List<CNhanVien> result = new List<CNhanVien>();

            try
            {
                using (FileStream readerFileStream = new FileStream(filePath, FileMode.Open, System.IO.FileAccess.Read))
                {
                    BinaryFormatter biFormatter = new BinaryFormatter();
                    result = (List<CNhanVien>)biFormatter.Deserialize(readerFileStream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }
        private void LoadNhanVienList()
        {
            dsNV = ReadNhanVienFromFile("dsnv.bin");
        }
        
        private void btnDangNhap_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PerformLogin();
            }
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PerformLogin();
            }
        }
        private void SetRoundedPanel(Panel panel, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(panel.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(panel.Width - radius, panel.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, panel.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            panel.Region = new Region(path);
        }
        private void SetRoundedButton(Button button, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(button.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(button.Width - radius, button.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, button.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            button.Region = new Region(path);
        }

        private void DangNhap_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            txtPass.PasswordChar = '*';
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "Password")
            {
                txtPass.Text = "";
                txtPass.PasswordChar = '*';
                txtPass.ForeColor = SystemColors.ControlText;
            }
        }

        private void txtPass_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPass.Text))
            {
                txtPass.Text = "";
            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPass.Text))
            {
                txtPass.Text = "Password";
                txtPass.PasswordChar = '\0';
                txtPass.ForeColor = SystemColors.GrayText;
            }
        }

        private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
        }
    }
}

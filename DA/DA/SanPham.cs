using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DA
{
    
    public partial class SanPham : Form
    {   
        public List<CSanPham> dsSP;
        public TextBox textBox;
        private void hienDSSP()
        {
            dgvSP.DataSource = dsSP.ToList();
        }
        private CSanPham timSP(string ma)
        {
            foreach (CSanPham p in dsSP)
                if (p.MaSP == ma)
                    return p;
            return null;
        }

        public SanPham()
        {
            InitializeComponent();
        }

        private void dgvSP_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSP.Text = dgvSP.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenSP.Text = dgvSP.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtGiaSP.Text=  dgvSP.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        private void ThemSanPham(CSanPham x)
        {
            if (timSP(x.MaSP) == null)
            {
                dsSP.Add(x);
                hienDSSP();
                CapNhatDanhSachSanPham();
                MessageBox.Show("Đã thêm sản phẩm có mã " + x.MaSP, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Mã sản phẩm " + x.MaSP + " đã tồn tại. Không thêm được. Vui lòng nhập mã SP khác để thêm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            CSanPham n = new CSanPham();
            n.MaSP = txtMaSP.Text;
            n.TenSP = txtTenSP.Text;
            n.GiaSP= int .Parse(txtGiaSP.Text);
            ThemSanPham(n);
        }

        private void SanPham_Load(object sender, EventArgs e)
        {
            dsSP = new List<CSanPham>();
            loadFile();
            hienDSSP();
            LoadTenSP();
            CapNhatDanhSachSanPham();
            Image themspIcon = Properties.Resources.addsp;
            SetButtonIcon(btnThem, themspIcon);
            Image bospIcon = Properties.Resources.removesp;
            SetButtonIcon(btnXoa, bospIcon);
            Image suaspIcon = Properties.Resources.sp;
            SetButtonIcon(btnSua, suaspIcon);
            Image luuspIcon = Properties.Resources.savesp;
            SetButtonIcon(btnLuu, luuspIcon);
            this.KeyPreview = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaSP.Text;
            if (timSP(ma) != null)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn có mã " + ma + " không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dsSP.Remove(timSP(ma));
                    hienDSSP();
                    CapNhatDanhSachSanPham();
                    MessageBox.Show("Đã sản phẩm có mã " + ma, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }else MessageBox.Show("Mã SP " + ma + " không có. Không thể xóa!! Vui lòng nhập đúng mã SP để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void SuaSanPham()
        {
            string ma = txtMaSP.Text;
            CSanPham sp = timSP(ma);
            if (sp != null)
            {
                sp.TenSP = txtTenSP.Text;
                sp.GiaSP = int.Parse(txtGiaSP.Text);
                hienDSSP();
                CapNhatDanhSachSanPham();
                MessageBox.Show("Đã sửa thông tin nhân viên có mã " + ma, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Mã SP " + ma + " không có. Không thể sửa!! Vui lòng nhập đúng mã SP để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            SuaSanPham();
        }
        
        private void btnLuu_Click(object sender, EventArgs e)
        {
            using (Stream file = File.Open("dssp.bin", FileMode.Create))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(file, dsSP);
                    CapNhatDanhSachSanPham();
                    MessageBox.Show("Đã lưu danh sách sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception ) {
                    MessageBox.Show("Lỗi");
                }
            }
        }
        private void loadFile()
        {
            BinaryFormatter biFormatter = new BinaryFormatter();
            string strFileLocation = "dssp.bin";
            if (File.Exists(strFileLocation))
            {
                using (FileStream readerFileStream = new FileStream(strFileLocation, FileMode.Open, System.IO.FileAccess.Read))
                {
                    dsSP = (List<CSanPham>)biFormatter.Deserialize(readerFileStream);
                }
                try
                {
                    FileStream readerFileStream = new FileStream(strFileLocation, FileMode.Open, System.IO.FileAccess.Read);
                    readerFileStream.Close();
                }
                catch (Exception)
                {
                    Console.WriteLine("Load that bai");
                }
            }
        }
        private void CapNhatDanhSachSanPham()
        {
            dgvSP.DataSource = null;
            dgvSP.DataSource = dsSP;
            cboTimMaSP.Text = null;
            LoadTenSP();
        }
        private void txtMaSP_TextChanged(object sender, EventArgs e)
        {

        }
        private void SetButtonIcon(Button button, Image icon)
        {
            if (icon != null)
            {
                icon = new Bitmap(icon, button.Width - 7, button.Height - 7);

                button.Image = icon;
                button.ImageAlign = ContentAlignment.MiddleCenter;
                button.TextAlign = ContentAlignment.BottomCenter;
            }
            else
            {
                MessageBox.Show("Không thể tìm thấy hình ảnh icon!");
            }
        }

        private void SanPham_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        CSanPham n = new CSanPham();
                        n.MaSP = txtMaSP.Text;
                        n.TenSP = txtTenSP.Text;
                        n.GiaSP = int.Parse(txtGiaSP.Text);
                        ThemSanPham(n);
                    }
                    break;
                case Keys.Delete:
                    btnXoa_Click(sender, e);
                    break;
                case Keys.F11:
                    btnLuu_Click(sender, e);
                    break;
                case Keys.F1:
                    btnXoa_Click(sender, e);
                    break;
                case Keys.F2:
                    btnTimMaSP_Click(sender, e);
                    break;
            }
        }
        private void LoadTenSP()
        {
            cboTimMaSP.Items.Clear();
            foreach (CSanPham sp in dsSP)
            {
                cboTimMaSP.Items.Add(sp.TenSP);
            }
        }
        private void txtTenSP_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void txtGiaSP_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void dgvSP_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void cboTimMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvSP.DataSource = null;
            if (cboTimMaSP.SelectedIndex != -1)
            {
                string selectedProductName = cboTimMaSP.SelectedItem.ToString();
                CSanPham selectedProduct = dsSP.FirstOrDefault(sp => sp.TenSP == selectedProductName);

                if (selectedProduct != null)
                {
                    dgvSP.DataSource = new List<CSanPham> { selectedProduct };
                }
            }
            else
            {
                dgvSP.DataSource = dsSP;
            }
        }

        private void btnTimMaSP_Click(object sender, EventArgs e)
        {
            cboTimMaSP_SelectedIndexChanged(sender, e);
        }
    }
}

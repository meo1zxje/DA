using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DA
{
    public partial class NhanVien : Form
    {
        private List<CNhanVien> dsNV;
        private void hienDSNV()
        {
            dgvNV.DataSource = dsNV.ToList();
        }
        private CNhanVien timNV(string ma)
        {
            foreach (CNhanVien n in dsNV)
                if (n.MaNV == ma)
                    return n;
            return null;
        }
        public NhanVien()
        {
            InitializeComponent();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgvNV_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNV.Text = dgvNV.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenNV.Text = dgvNV.Rows[e.RowIndex].Cells[1].Value.ToString();
            dtNgaySinh.Value = (DateTime)dgvNV.Rows[e.RowIndex].Cells[2].Value;
            rdb1.Checked = (bool)dgvNV.Rows[e.RowIndex].Cells[3].Value;
            rdb3.Checked = (bool)dgvNV.Rows[e.RowIndex].Cells[4].Value;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            CNhanVien n = new CNhanVien();
            n.MaNV = txtMaNV.Text;
            n.TenNV = txtTenNV.Text;
            n.NgaySinh = dtNgaySinh.Value;
            n.Phai = rdb1.Checked;
            n.ChucVu = rdb3.Checked;
            if (timNV(n.MaNV) == null)
            {
                dsNV.Add(n);
                hienDSNV();
                CapNhatDanhSachNV();
                MessageBox.Show("Đã thêm nhân viên có mã " + n.MaNV, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Mã NV " + n.MaNV + " đã tồn tại. Không thể thêm!! Vui lòng nhập mã NV khác để thêm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            dsNV=new List<CNhanVien>();
            loadFile();
            hienDSNV();
            LoadTenSP();
            Image themnvIcon = Properties.Resources.adduser;
            SetButtonIcon(btnThem, themnvIcon);
            Image xoanvIcon = Properties.Resources.xoauser;
            SetButtonIcon(btnXoa, xoanvIcon);
            Image suanvIcon = Properties.Resources.suauser;
            SetButtonIcon(btnSua, suanvIcon);
            Image luunvIcon = Properties.Resources.luuuser;
            SetButtonIcon(btnLuu, luunvIcon);
            this.KeyPreview = true;
        }

        private void dgvNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvNV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var grid = (DataGridView)sender;
            if (grid.Columns[e.ColumnIndex].Name == "Column4")
            {
                if (e.Value is bool)
                {
                    bool value = (bool)e.Value;
                    e.Value = (value) ? "Nam" : "Nữ";
                    e.FormattingApplied = true;
                }
            }
            else if (grid.Columns[e.ColumnIndex].Name == "Column5")
            {
                if (e.Value is bool)
                {
                    bool value = (bool)e.Value;
                    e.Value = (value) ? "Nhân viên" : "Quản lý";
                    e.FormattingApplied = true;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaNV.Text;
            if (timNV(ma) != null)
            {
                dsNV.Remove(timNV(ma));
                hienDSNV();
                CapNhatDanhSachNV();
                MessageBox.Show("Đã xóa nhân viên có mã " + ma, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Không có mã NV "+ma+" để xóa!! Vui lòng nhập đúng mã NV để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string ma = txtMaNV.Text;
            CNhanVien n = timNV(ma);
            if (n != null)
            {
                n.TenNV = txtTenNV.Text;
                n.NgaySinh = dtNgaySinh.Value;
                n.Phai = rdb1.Checked;
                n.ChucVu = rdb3.Checked;
                hienDSNV();
                CapNhatDanhSachNV();
                MessageBox.Show("Đã sửa thông tin nhân viên có mã " + ma, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Mã NV " + ma + " không có. Không thể sửa!! Vui lòng nhập đúng mã NV để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            using (Stream file = File.Open("dsnv.bin", FileMode.Create))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(file, dsNV);
                    CapNhatDanhSachNV();
                    MessageBox.Show("Đã lưu danh sách nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi");
                }
            }
        }
        private void loadFile()
        {
            BinaryFormatter biFormatter = new BinaryFormatter();
            string strFileLocation = "dsnv.bin";
            if (File.Exists(strFileLocation))
            {
                using (FileStream readerFileStream = new FileStream(strFileLocation, FileMode.Open, System.IO.FileAccess.Read))
                {
                    dsNV = (List<CNhanVien>)biFormatter.Deserialize(readerFileStream);
                }
                try
                {
                    FileStream readerFileStream = new FileStream(strFileLocation, FileMode.Open, System.IO.FileAccess.Read);
                    readerFileStream.Close();
                }
                catch (Exception)
                {
                    Console.WriteLine("Load that bai", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        private void NhanVien_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btnThem_Click(sender, e);
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
            }
        }
        private void CapNhatDanhSachNV()
        {
            dgvNV.DataSource = null;
            dgvNV.DataSource = dsNV;
            cboTimNV.Text = null;
            LoadTenSP();
        }
        private void LoadTenSP()
        {
            cboTimNV.Items.Clear();
            foreach (CNhanVien nv in dsNV)
            {
                cboTimNV.Items.Add(nv.TenNV);
            }
        }
        private void CapNhatDanhSachNhanVien()
        {
            dgvNV.DataSource = null;
            if (cboTimNV.SelectedIndex != -1)
            {
                string selectedEmployeeName = cboTimNV.SelectedItem.ToString();
                CNhanVien selectedEmployee = dsNV.FirstOrDefault(nv => nv.TenNV == selectedEmployeeName);

                if (selectedEmployee != null)
                {
                    dgvNV.DataSource = new List<CNhanVien> { selectedEmployee };
                }
            }
            else
            {
                dgvNV.DataSource = dsNV;
            }
        }
        private void cboTimNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dgvNV.DataSource = null;
            if (cboTimNV.SelectedIndex != -1)
            {
                string selectedEmployeeName = cboTimNV.SelectedItem.ToString();
                CNhanVien selectedEmployee = dsNV.FirstOrDefault(nv => nv.TenNV == selectedEmployeeName);

                if (selectedEmployee != null)
                {
                    txtMaNV.Text = selectedEmployee.MaNV;
                    txtTenNV.Text = selectedEmployee.TenNV;
                    dtNgaySinh.Value = selectedEmployee.NgaySinh;
                    rdb1.Checked = selectedEmployee.Phai;
                    rdb3.Checked = selectedEmployee.ChucVu;
                    //dgvNV.DataSource = new List<CNhanVien> { selectedEmployee };
                }
            }
        }

        private void btnTimNV_Click(object sender, EventArgs e)
        {
            cboTimNV_SelectedIndexChanged(sender, e);
            CapNhatDanhSachNhanVien();
        }
    }
}

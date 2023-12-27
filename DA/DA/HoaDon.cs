using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace DA
{
    public partial class HoaDon : Form
    {
        private Dictionary<string, CHoaDon> dsHoaDon;
        private List<CSanPham> dsSanPham;
        private List<CNhanVien> dsNVTam;
        private List<CNhanVien> dsNV;
        public SanPham frmSanPham = new SanPham();
        private TextBox txtMaSP;
        private TextBox txtTenSP;
        private TextBox txtGiaSP;
        private bool daChonSP = false;
        private string maHoaDonDangChon;
        private int editingRowIndex = -1;
        private bool isEditing = false;
        private bool hasShownErrorMessage = false;


        public HoaDon()
        {
            InitializeComponent();
            dsHoaDon = new Dictionary<string, CHoaDon>();
            maHoaDonDangChon = null;
            txtMaSP = new TextBox();
            txtTenSP = new TextBox();
            txtGiaSP = new TextBox();
            this.KeyPreview = true;
            LoadDanhSachSanPham();
            HienThiThongTinSanPham();
            dsNV = new List<CNhanVien>();
            dsNVTam = new List<CNhanVien>();
            LoadNhanVienTam();
            LoadNhanVienList();
        }
        private void HoaDon_Load(object sender, EventArgs e)
        {
            LoadDanhSachHoaDonTuFile("dshd.bin");
            LoadDanhSachSanPhamTuFile("dssp.bin");
            HienThiThongTinSanPham();
            CapNhatDanhSachHoaDonComboBox();
            dgvHD.CellClick += dgvHD_CellClick;
            dgvHD.CellDoubleClick += dgvHD_CellDoubleClick;
            btnLuuHD.Visible= false;
            Image themIcon = Properties.Resources.them1;
            Image xoaIcon = Properties.Resources.xoa1;
            Image suaIcon = Properties.Resources.rplist;
            Image taohdIcon = Properties.Resources.hoadon;
            Image luuhdIcon = Properties.Resources.save;
            Image timhdIcon = Properties.Resources.srbill;
            Image xoahdIcon = Properties.Resources.hoadon1;
            Image inhdIcon = Properties.Resources.slip;
            SetButtonIcon(btnThem, themIcon);
            SetButtonIcon(btnXoa, xoaIcon);
            SetButtonIcon(btnsua, suaIcon);
            SetButtonIcon(btnTaoHD, taohdIcon);
            SetButtonIcon(btnLuuHD, luuhdIcon); ;
            SetButtonIcon(btntim, timhdIcon);
            SetButtonIcon(btnXoaHD, xoahdIcon);
            SetButtonIcon(btnInHD, inhdIcon);
        }
        private void LoadDanhSachSanPham()
        {
            dsSanPham = new List<CSanPham>();
        }
        
        public void HienThiThongTinSanPham()
        {
            cbTenSP.DataSource = dsSanPham;
            cbTenSP.DisplayMember = "TenSP";
            cbTenSP.ValueMember = "MaSP";
            cbTenSP.SelectedIndexChanged += (sender, e) =>
            {
                daChonSP = true;
                CSanPham sanPhamDuocChon = (CSanPham)cbTenSP.SelectedItem;
                if (sanPhamDuocChon != null)
                {
                    SanPham frmSanPham = Application.OpenForms.OfType<SanPham>().FirstOrDefault();
                    if (frmSanPham != null)
                    {
                        CChiTietHoaDon chiTiet = dsHoaDon[maHoaDonDangChon].DSSP.Find(sp => sp.SanPham.MaSP == sanPhamDuocChon.MaSP);
                        if (chiTiet != null)
                        {
                            frmSanPham.txtMaSP.Text = chiTiet.SanPham.MaSP;
                            frmSanPham.txtTenSP.Text = chiTiet.SanPham.TenSP;
                            frmSanPham.txtGiaSP.Text = chiTiet.SanPham.GiaSP.ToString();

                        }
                    }
                }
            };
        }
        

        //Thêm sản phẩm
        private bool CoThayDoi(CChiTietHoaDon chiTiet)
        {
            CChiTietHoaDon chiTietHienTai = dsHoaDon[maHoaDonDangChon].DSSP.Find(sp => sp.SanPham.MaSP == chiTiet.SanPham.MaSP);
            return chiTietHienTai.Soluong != chiTiet.Soluong || chiTietHienTai.GhiChu != chiTiet.GhiChu;
        }
        private void ThemSanPhamVaoHoaDon()
        {

            if (maHoaDonDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn hoặc tạo hóa đơn trước khi thêm sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (daChonSP)
            {
                int soLuong;
                if (int.TryParse(txtSoLuong.Text, out soLuong) && soLuong > 0)
                {
                    CSanPham sanPhamDuocChon = (CSanPham)cbTenSP.SelectedItem;

                    if (sanPhamDuocChon != null)
                    {
                        CChiTietHoaDon chiTiet = new CChiTietHoaDon(sanPhamDuocChon, soLuong, txtGhiChu.Text);

                        if (dsHoaDon[maHoaDonDangChon].DSSP.Any(sp => sp.SanPham.MaSP == chiTiet.SanPham.MaSP))
                        {
                            if (CoThayDoi(chiTiet))
                            {
                                SuaSanPhamTrongHoaDon(dsHoaDon[maHoaDonDangChon].DSSP.FindIndex(sp => sp.SanPham.MaSP == chiTiet.SanPham.MaSP), chiTiet.Soluong, txtGhiChu.Text);
                            }
                            else
                            {
                                MessageBox.Show("Mã sản phẩm " + sanPhamDuocChon.TenSP + " đã tồn tại trong hóa đơn. Hãy thêm sản phẩm mới hoặc sửa đổi thông tin sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            dsHoaDon[maHoaDonDangChon].DSSP.Add(chiTiet);
                        }

                        HienThiThongTinHoaDon();
                    }
                }
                else
                {
                    MessageBox.Show("Số lượng phải là một số nguyên dương lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm từ danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            ThemSanPhamVaoHoaDon();
        }
        
        //Xóa sản phẩm
        private void XoaSanPhamKhoiHoaDon()
        {
            int selectedRowIndex = dgvHD.CurrentCell.RowIndex;

            if (selectedRowIndex >= 0 && selectedRowIndex < dsHoaDon[maHoaDonDangChon].DSSP.Count)
            {
                dsHoaDon[maHoaDonDangChon].DSSP.RemoveAt(selectedRowIndex);
                HienThiThongTinHoaDon();
            }
            else
            {
                MessageBox.Show("Không có sản phẩm nào để xóa khỏi hóa đơn.");
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            XoaSanPhamKhoiHoaDon();
        }

        //Sửa sản phẩm
        private void SuaSanPhamTrongHoaDon(int rowIndex, int soLuong, string ghiChu)
        {
            if (rowIndex >= 0 && rowIndex < dsHoaDon[maHoaDonDangChon].DSSP.Count)
            {
                dsHoaDon[maHoaDonDangChon].DSSP[rowIndex].Soluong = soLuong;
                dsHoaDon[maHoaDonDangChon].DSSP[rowIndex].GhiChu = ghiChu;
                decimal thanhTien = dsHoaDon[maHoaDonDangChon].ThanhTien(dsHoaDon[maHoaDonDangChon].DSSP[rowIndex]);
                dgvHD.Rows[rowIndex].Cells[5].Value = thanhTien;
                decimal tongTien = 0;
                foreach (CChiTietHoaDon chiTiet in dsHoaDon[maHoaDonDangChon].DSSP)
                {
                    tongTien += dsHoaDon[maHoaDonDangChon].ThanhTien(chiTiet);
                }
                dgvHD.Rows[dgvHD.Rows.Count - 1].Cells[7].Value = "Tổng tiền: " + tongTien.ToString();
                HienThiThongTinHoaDon();
                if (rowIndex >= 0 && rowIndex < dgvHD.Rows.Count - 1)
                {
                    CChiTietHoaDon chiTiet = GetChiTietHoaDonTuDataGridViewRow(rowIndex);
                    if (chiTiet != null)
                    {
                        txtMaSP.Text = chiTiet.SanPham.MaSP;
                        txtTenSP.Text = chiTiet.SanPham.TenSP;
                        txtGiaSP.Text = chiTiet.SanPham.GiaSP.ToString();
                        txtSoLuong.Text = chiTiet.Soluong.ToString();
                        txtGhiChu.Text = chiTiet.GhiChu;
                        cbTenSP.SelectedValue = chiTiet.SanPham.MaSP;
                    }
                }
            }
        }
        private void btnsua_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = dgvHD.CurrentCell.RowIndex;

            if (selectedRowIndex >= 0 && selectedRowIndex < dsHoaDon[maHoaDonDangChon].DSSP.Count)
            {
                int soLuong;
                if (int.TryParse(txtSoLuong.Text, out soLuong) && soLuong > 0)
                {
                    dsHoaDon[maHoaDonDangChon].DSSP[selectedRowIndex].Soluong = soLuong;
                    dsHoaDon[maHoaDonDangChon].DSSP[selectedRowIndex].GhiChu = txtGhiChu.Text;
                    HienThiThongTinHoaDon();
                }
                else
                {
                    MessageBox.Show("Số lượng phải là một số nguyên dương lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Không có sản phẩm nào để sửa trong hóa đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void HienThiThongTinHoaDon()
        {
            decimal tongTien = 0;
            dgvHD.BeginInvoke((MethodInvoker)delegate
            {
                dgvHD.Rows.Clear();

                if (!string.IsNullOrEmpty(maHoaDonDangChon) && dsHoaDon.ContainsKey(maHoaDonDangChon))
                {
                    var hoaDon = dsHoaDon[maHoaDonDangChon];

                    if (hoaDon.DSSP != null && hoaDon.DSSP.Any())
                    {
                        foreach (CChiTietHoaDon chiTiet in hoaDon.DSSP)
                        {
                            decimal thanhTien = hoaDon.ThanhTien(chiTiet);
                            dgvHD.Rows.Add(
                                hoaDon.MaHD,
                                chiTiet.SanPham.MaSP,
                                chiTiet.SanPham.TenSP,
                                chiTiet.Soluong,
                                chiTiet.SanPham.GiaSP,
                                thanhTien,
                                chiTiet.GhiChu
                            );
                            tongTien += thanhTien;
                        }
                    }
                    else
                    {
                        dgvHD.Rows.Add(null, null, null, null, null, null, null, null);
                    }

                    dgvHD.Rows.Add("", "", "", "", "", "", "", "Tổng tiền: " + tongTien.ToString());
                }
                else
                {
                    dgvHD.Rows.Add(null, null, null, null, null, null, null, null);
                }

                dgvHD.AutoResizeColumns();
                dgvHD.Refresh();
            });
        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void LoadDanhSachSanPhamTuFile(string tenFile)
        {
            KiemTraVaTaoFileHoaDon(tenFile);
            try
            {
                using (Stream file = File.Open(tenFile, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    var data = bf.Deserialize(file);

                    if (data is Tuple<Dictionary<string, CHoaDon>, List<CSanPham>> tupleData)
                    {
                        dsHoaDon = tupleData.Item1;
                        dsSanPham = tupleData.Item2;
                        CapNhatDanhSachMaHoaDon();
                        HienThiThongTinSanPham();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc dữ liệu từ tập tin: " + ex.Message);
            }
        }

        private void dgvHD_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 8 && e.RowIndex >= 0 && e.RowIndex < dgvHD.Rows.Count - 1)
            {
                CChiTietHoaDon chiTiet = GetChiTietHoaDonTuDataGridViewRow(e.RowIndex);
                if (chiTiet != null)
                {
                    e.Value = dsHoaDon[maHoaDonDangChon].ThanhTien(chiTiet);
                    e.FormattingApplied = true;
                }
            }
        }
        private CChiTietHoaDon GetChiTietHoaDonTuDataGridViewRow(int rowIndex)
        {
            if (!string.IsNullOrEmpty(maHoaDonDangChon) && dsHoaDon.ContainsKey(maHoaDonDangChon))
            {
                string maSanPham = dgvHD.Rows[rowIndex].Cells[1].Value?.ToString();

                if (!string.IsNullOrEmpty(maSanPham))
                {
                    return dsHoaDon[maHoaDonDangChon].DSSP.FirstOrDefault(sp => sp.SanPham.MaSP == maSanPham);
                }
            }
            return null;
        }
        private void dgvHD_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvHD.Rows.Count)
            {
                CChiTietHoaDon chiTiet = GetChiTietHoaDonTuDataGridViewRow(e.RowIndex);
                if (chiTiet != null)
                {
                    txtMaSP.Text = chiTiet.SanPham.MaSP;
                    txtTenSP.Text = chiTiet.SanPham.TenSP;
                    txtGiaSP.Text = chiTiet.SanPham.GiaSP.ToString();
                }
            }
        }
        

        //Tạo hóa đơn
        private void ThemHoaDonMoi(string maHD, DateTime ngayTao, string maNV)
        {
            if (!dsHoaDon.ContainsKey(maHD))
            {
                dsHoaDon[maHD] = new CHoaDon(maHD, ngayTao, maNV);
                CapNhatDanhSachMaHoaDon();
            }
            maHoaDonDangChon = maHD;
        }
        private Tuple<string, DateTime> TaoMaHoaDonMoi()
        {
            string maHoaDonMoi = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");
            txtMaHD.Text = maHoaDonMoi;
            return Tuple.Create(maHoaDonMoi, DateTime.Now);
        }
        private void btnTaoHD_Click(object sender, EventArgs e)
        {
            Tuple<string, DateTime> thongTinHoaDonMoi = TaoMaHoaDonMoi();
            string maHoaDonMoi = thongTinHoaDonMoi.Item1;
            DateTime ngayTao = thongTinHoaDonMoi.Item2;

            if (!dsHoaDon.ContainsKey(maHoaDonMoi))
            {
                LoadNhanVienTam();
                CNhanVien nhanVienTam = dsNVTam.FirstOrDefault();

                if (nhanVienTam != null)
                {
                    ThemHoaDonMoi(maHoaDonMoi, ngayTao, nhanVienTam.MaNV);
                    txtMaNV.Text = nhanVienTam.MaNV;
                    btnLuuHD.Visible=true;
                }
                else
                {
                    txtMaNV.Text = "Admin";
                }

                maHoaDonDangChon = maHoaDonMoi;
                HienThiThongTinHoaDon();
            }
            else MessageBox.Show("Mã HD đã tồn tại. Vui lòng điền mã khác", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Tìm hóa đơn
        private void HienThiThongTinHoaDonVaCapNhatMaHD(string maHoaDon)
        {
            if (dsHoaDon.ContainsKey(maHoaDon))
            {
                maHoaDonDangChon = maHoaDon;
                HienThiThongTinHoaDon();
                txtMaHD.Text = maHoaDon;
                string maNVHoaDon = dsHoaDon[maHoaDon].MaNV;
                txtMaNV.Text = maNVHoaDon;
            }
            else
            {
                MessageBox.Show("Không tìm thấy mã hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btntim_Click(object sender, EventArgs e)
        {
            string maTimKiem = cbtimtheoma.Text;
            HienThiThongTinHoaDonVaCapNhatMaHD(maTimKiem);
            btnLuuHD.Visible = true;
        }

        //Xóa hóa đơn
        private void xoaHoaDonTheoMa()
        {
            string maHoaDonCanXoa = cbtimtheoma.Text.Trim();

            if (!string.IsNullOrEmpty(maHoaDonCanXoa))
            {
                if (dsHoaDon.ContainsKey(maHoaDonCanXoa))
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn có mã " + maHoaDonCanXoa + " không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        dsHoaDon.Remove(maHoaDonCanXoa);
                        MessageBox.Show("Đã xóa hóa đơn có mã " + maHoaDonCanXoa, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMaHD.Text = string.Empty;
                        HienThiThongTinHoaDon();
                        CapNhatDanhSachMaHoaDon();
                        btnLuuHD.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mã hóa đơn cần xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn cần xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXoaHD_Click(object sender, EventArgs e)
        {
            xoaHoaDonTheoMa();
        }

        private void dgvHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        

        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            string tenTepTin = "dshd.bin";

            try
            {
                using (Stream file = File.Open(tenTepTin, FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(file, new Tuple<Dictionary<string, CHoaDon>, List<CSanPham>>(dsHoaDon, dsSanPham));
                }

                MessageBox.Show("Đã lưu hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu danh sách hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDanhSachHoaDonTuFile(string tenTepTin)
        {
            KiemTraVaTaoFileHoaDon(tenTepTin);

            try
            {
                using (Stream file = File.Open(tenTepTin, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    var data = bf.Deserialize(file);

                    if (data is Tuple<Dictionary<string, CHoaDon>, List<CSanPham>> tupleData)
                    {
                        dsHoaDon = tupleData.Item1;
                        dsSanPham = tupleData.Item2;
                        HienThiThongTinSanPham();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc dữ liệu từ tập tin: " + ex.Message);
            }
        }
        private void KiemTraVaTaoFileHoaDon(string tenFile)
        {
            if (!File.Exists(tenFile))
            {
                dsHoaDon = new Dictionary<string, CHoaDon>();
                try
                {
                    using (Stream file = File.Open(tenFile, FileMode.Create))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(file, new Tuple<Dictionary<string, CHoaDon>, List<CSanPham>>(dsHoaDon, dsSanPham));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tạo tập tin danh sách hóa đơn mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        

        private void cbTenSP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void CapNhatDanhSachMaHoaDon()
        {
            cbtimtheoma.DataSource = new BindingSource(dsHoaDon, null);
            cbtimtheoma.DisplayMember = "Key";
            cbtimtheoma.ValueMember = "Value";
        }
        private void CapNhatDanhSachHoaDonComboBox()
        {
            cbtimtheoma.Items.Clear();
            foreach (string maHoaDon in dsHoaDon.Keys)
            {
                cbtimtheoma.Items.Add(maHoaDon);
            }
        }
        private void HoaDon_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (ActiveControl == cbtimtheoma)
                    {
                        string maTimKiem = cbtimtheoma.Text;
                        HienThiThongTinHoaDonVaCapNhatMaHD(maTimKiem);
                    }
                    else
                    {
                        e.Handled = true;
                        int rowIndex = dgvHD.CurrentCell?.RowIndex ?? -1;
                        if (rowIndex >= 0 && rowIndex < dgvHD.Rows.Count - 1)
                        {
                            int soLuong;
                            if (int.TryParse(txtSoLuong.Text, out soLuong) && soLuong >= 0)
                            {
                                string ghiChu = txtGhiChu.Text;

                                if (editingRowIndex == -1)
                                {

                                    ThemSanPhamVaoHoaDon();
                                }
                            }
                            else
                            {
                                if (!hasShownErrorMessage)
                                {
                                    MessageBox.Show("Số lượng phải là một số nguyên không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    hasShownErrorMessage = true;
                                }
                            }
                        }
                        if (!isEditing)
                        {
                            editingRowIndex = -1;
                        }
                        isEditing = false;
                    }
                    break;
                case Keys.Delete:
                    if (ActiveControl == cbtimtheoma)
                    {
                        xoaHoaDonTheoMa();
                    }
                    else
                    {
                        btnXoa_Click(sender, e);
                    }
                    break;
                case Keys.F10:
                    btnTaoHD_Click(sender, e);
                    break;
                case Keys.F11:
                    btnLuuHD_Click(sender, e);
                    break;
                case Keys.F12:
                    btnInHD_Click(sender, e);
                    break;
            }
        }

        private void dgvHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvHD.Rows.Count - 1)
            {
                isEditing = true;
                editingRowIndex = e.RowIndex;
                CChiTietHoaDon chiTiet = GetChiTietHoaDonTuDataGridViewRow(e.RowIndex);
                if (chiTiet != null)
                {
                    txtMaSP.Text = chiTiet.SanPham.MaSP;
                    txtTenSP.Text = chiTiet.SanPham.TenSP;
                    txtGiaSP.Text = chiTiet.SanPham.GiaSP.ToString();
                    txtSoLuong.Text = chiTiet.Soluong.ToString();
                    txtGhiChu.Text = chiTiet.GhiChu;
                    cbTenSP.SelectedValue = chiTiet.SanPham.MaSP;
                    HienThiThongTinSanPham();
                }
            }
        }

        private void dgvHD_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 6)
            {
                int rowIndex = e.RowIndex;

                object rawValue = dgvHD.Rows[rowIndex].Cells[e.ColumnIndex].Value;
                if (rawValue != null && !string.IsNullOrWhiteSpace(rawValue.ToString()))
                {
                    if (e.ColumnIndex == 3)
                    {
                        int soLuong;
                        if (int.TryParse(rawValue.ToString(), out soLuong) && soLuong >= 0)
                        {
                            string ghiChu = dgvHD.Rows[rowIndex].Cells[6].Value?.ToString() ?? "";
                            SuaSanPhamTrongHoaDon(rowIndex, soLuong, ghiChu);
                        }
                        else
                        {
                            MessageBox.Show("Số lượng phải là một số nguyên không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (e.ColumnIndex == 6)
                    {
                        int soLuong = Convert.ToInt32(dgvHD.Rows[rowIndex].Cells[3].Value);
                        string ghiChu = rawValue.ToString();
                        SuaSanPhamTrongHoaDon(rowIndex, soLuong, ghiChu);
                    }
                }
            }
        }

        private void dgvHD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvHD.Rows.Count - 1)
            {
                CChiTietHoaDon chiTiet = GetChiTietHoaDonTuDataGridViewRow(e.RowIndex);
                if (chiTiet != null)
                {
                    txtMaSP.Text = chiTiet.SanPham.MaSP;
                    txtTenSP.Text = chiTiet.SanPham.TenSP;
                    txtGiaSP.Text = chiTiet.SanPham.GiaSP.ToString();
                    txtSoLuong.Text = chiTiet.Soluong.ToString();
                    txtGhiChu.Text = chiTiet.GhiChu;
                    cbTenSP.SelectedValue = chiTiet.SanPham.MaSP;
                }
            }
        }

        private void dgvHD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                int rowIndex = dgvHD.CurrentCell?.RowIndex ?? -1;
                if (rowIndex >= 0 && rowIndex < dgvHD.Rows.Count - 1)
                {
                    int soLuong;
                    if (int.TryParse(dgvHD.Rows[rowIndex].Cells["SoLuong"].Value.ToString(), out soLuong) && soLuong >= 0)
                    {
                        string ghiChu = dgvHD.Rows[rowIndex].Cells["GhiChu"].Value?.ToString() ?? "";
                        SuaSanPhamTrongHoaDon(rowIndex, soLuong, ghiChu);
                    }
                    else
                    {
                        MessageBox.Show("Số lượng phải là một số nguyên không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                editingRowIndex = -1;
            }
        }

        private void dgvHD_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 6)
            {
                DataGridView dgv = (DataGridView)sender;
                string newValue = e.FormattedValue.ToString();
                string oldValue = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";
                if (!string.Equals(newValue, oldValue))
                {
                    int soLuong = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[3].Value);
                    SuaSanPhamTrongHoaDon(e.RowIndex, soLuong, newValue);
                }
            }
        }

        private void dgvHD_DragLeave(object sender, EventArgs e)
        {

        }

        private void dgvHD_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbtimtheoma_Enter(object sender, EventArgs e)
        {
        }

        private void cbtimtheoma_Leave(object sender, EventArgs e)
        {
        }

        private void cbtimtheoma_SelectedIndexChanged(object sender, EventArgs e)
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

        private void label2_Click(object sender, EventArgs e)
        {

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
        private void LoadNhanVienTam()
        {
            dsNVTam = ReadNhanVienFromFile("dstam.bin");
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            string maHoaDon = cbtimtheoma.Text;

            if (!string.IsNullOrEmpty(maHoaDon) && dsHoaDon.ContainsKey(maHoaDon))
            {
                var hoaDon = dsHoaDon[maHoaDon];
                string tenFile = maHoaDon + ".txt";

                try
                {
                    using (StreamWriter sw = new StreamWriter(tenFile))
                    {
                        sw.WriteLine($"Mã hóa đơn: {hoaDon.MaHD} \t Ngày tạo: {hoaDon.NgayTao}");
                        sw.WriteLine();
                        sw.WriteLine($"Mã sản phẩm \t Tên sản phẩm \t Số lượng \t Giá bán \t Thành tiền \t Ghi chú");
                        foreach (var chiTiet in hoaDon.DSSP)
                        {
                            sw.WriteLine($"{chiTiet.SanPham.MaSP} \t\t {chiTiet.SanPham.TenSP} \t {chiTiet.Soluong} \t\t {chiTiet.SanPham.GiaSP} \t\t {hoaDon.ThanhTien(chiTiet)} \t\t {chiTiet.GhiChu}");
                        }
                        sw.WriteLine();
                        sw.WriteLine($"Tổng tiền: {hoaDon.TongTien()}");
                        MessageBox.Show($"Đã in hóa đơn có mã {maHoaDon} lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy mã hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
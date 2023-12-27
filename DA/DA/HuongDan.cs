using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;

namespace DA
{
    public partial class HuongDan : Form
    {
        public HuongDan()
        {
            InitializeComponent();
            DisplayFunctionality();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void DisplayFunctionality()
        {
            richTextBox1.Clear();
            richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font, System.Drawing.FontStyle.Bold);
            richTextBox1.AppendText("1. Form Sản Phẩm\n");
            richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font, System.Drawing.FontStyle.Regular);
            richTextBox1.AppendText("- Dùng để quản lý danh sách sản phẩm của cửa hàng để bán hàng\n");
            richTextBox1.AppendText("- Các chức năng:\n");
            richTextBox1.AppendText("  o Thêm sản phẩm: Nhập thông tin sản phẩm -> ấn Enter hoặc nút Thêm\n");
            richTextBox1.AppendText("  o Xóa sản phẩm: Chọn sản phẩm cần xóa -> ấn Delete hoặc nút Xóa\n");
            richTextBox1.AppendText("  o Sửa sản phẩm: Chọn sản phẩm cần sửa -> Sửa lại thông tin sản phẩm trên các txtMaSP, txtTenSP, txtGiaSP -> ấn F1 hoặc nút Sửa\n");
            richTextBox1.AppendText("  o Tìm sản phẩm: Chọn sản phẩm cần tìm từ combobox cboTimMaSP -> ấn F2 hoặc nút Tìm SP.\n");
            richTextBox1.AppendText("  o Lưu danh sách sản phẩm: ấn F11 hoặc nút Lưu\n");
            richTextBox1.AppendText("\n");
            richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font, System.Drawing.FontStyle.Bold);
            richTextBox1.AppendText("2. Form Nhân Viên\n");
            richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font, System.Drawing.FontStyle.Regular);
            richTextBox1.AppendText("- Dùng để quản lý danh sách nhân viên của cửa hàng\n");
            richTextBox1.AppendText("- Các chức năng:\n");
            richTextBox1.AppendText("  o Thêm nhân viên: Nhập thông tin sản phẩm -> ấn Enter hoặc nút Thêm\n");
            richTextBox1.AppendText("  o Xóa nhân viên: Chọn sản phẩm cần xóa -> ấn Delete hoặc nút Xóa\n");
            richTextBox1.AppendText("  o Sửa nhân viên: Chọn nhân viên cần sửa -> Sửa lại thông tin nhân viên trên các txtMaNV, txtTenNV, dtNgaySinh, grb1GioiTinh(rdb1, rdb2), grb1ChuVu(rdb3, rdb4) -> ấn F1 hoặc nút Sửa\n");
            richTextBox1.AppendText("  o Tìm nhân viên: Chọn nhân viên cần tìm từ combobox cboTimNV -> ấn F2 hoặc nút Tìm SP.\n");
            richTextBox1.AppendText("  o Lưu danh sách nhân viên: ấn F11 hoặc nút Lưu\n");
            richTextBox1.AppendText("\n");
            richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font, System.Drawing.FontStyle.Bold);
            richTextBox1.AppendText("3. Form Hóa Đơn\n");
            richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font, System.Drawing.FontStyle.Regular);
            richTextBox1.AppendText("- Form hóa đơn dùng để thêm 1 hóa đơn cùng chi tiết hóa đơn của hóa đơn đó\n");
            richTextBox1.AppendText("- Các chức năng của form HoaDon:\n");
            richTextBox1.AppendText("  o Tạo hóa đơn mới: ấn F10 hoặc nút Tạo HĐ mới rồi mới thêm sản phẩm vào hóa đơn\n");
            richTextBox1.AppendText("    + Thêm sản phẩm vào hóa đơn: Chọn sản phẩm từ cbTenSP rồi nhập số lượng, ghi chú(nếu có) -> ấn Enter hoặc nút Thêm\n");
            richTextBox1.AppendText("    + Xóa sản phẩm trong hóa đơn: Chọn sản phẩm cần xóa từ datagridview -> ấn Delete hoặc nút Xóa\n");
            richTextBox1.AppendText("    + Sửa sản phẩm trong hóa đơn: Chọn sản phẩm cần sửa -> sửa lại số lượng hoặc ghi chú hoặc cả 2 trên txtSoLuong, txtGhiChu hoặc sửa trực tiếp trên datagridview -> ấn Enter hoặc nút Sửa\n");
            richTextBox1.AppendText("  o Xóa hóa đơn bất kỳ từ danh sách hóa đơn: Chọn hóa đơn cần xóa trong cbtimtheoma ở groupBox2 -> ấn Delete hoặc nút Xóa\n");
            richTextBox1.AppendText("  o Lưu hóa đơn: ấn F11 hoặc nút Lưu\n");
            richTextBox1.AppendText("  o Tìm hóa đơn từ danh sách hóa đơn: Chọn hóa đơn cần tìm trong cbtimtheoma ở groupBox2 -> ấn Enter hoặc nút Tìm\n");
            richTextBox1.AppendText("  o In hóa đơn bất kỳ từ danh sách hóa đơn: Chọn hóa đơn cần in trong cbtimtheoma ở groupBox2 -> ấn F12 hoặc nút In\n");
            richTextBox1.AppendText("\n");
            richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font, System.Drawing.FontStyle.Bold);
            richTextBox1.AppendText("4. Form Doanh Thu\n");
            richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font, System.Drawing.FontStyle.Regular);
            richTextBox1.AppendText("- Xem doanh thu trong khoảng thời gian xác định.\n");
            richTextBox1.AppendText("- Cách xem: chọn khoảng thời gian cần xem doanh thu trong khoảng Từ Ngày đến Đến Ngày -> ấn Enter hoặc nút Xem Doanh Thu\n");
            richTextBox1.AppendText("\n");
            richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font, System.Drawing.FontStyle.Bold);
            richTextBox1.AppendText("5. Form Thống Kê\n");
            richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font, System.Drawing.FontStyle.Regular);
            richTextBox1.AppendText("- Xem thống kê trong khoảng thời gian xác định.\n");
            richTextBox1.AppendText("- Cách xem: chọn khoảng thời gian cần xem thống kê trong khoảng Từ Ngày đến Đến Ngày -> ấn Enter hoặc nút Xem Thống Kê\n");
            richTextBox1.SelectionStart = 0;
        }
        
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DA
{
    public partial class ThongKe : Form
    {
        private CThongKe cThongKe;
        private List<CHoaDon> _doanhthu;

        public List<CHoaDon> DoanhThu
        {
            get { return _doanhthu; }
            set { _doanhthu = value; }
        }
        public ThongKe()
        {
            InitializeComponent();
            cThongKe = new CThongKe();
            Dictionary<string, CHoaDon> danhSachHoaDon = new Dictionary<string, CHoaDon>();
            Image image = Properties.Resources.srthongke;
            Size newSize = new Size(64, 64);
            image = new Bitmap(image, newSize);
            button1.Image = image;
            button1.Padding = new Padding(0);
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.TextAlign = ContentAlignment.MiddleRight;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private Dictionary<string, CHoaDon> ReadHoaDonFromFile(string filePath)
        {
            Dictionary<string, CHoaDon> danhSachHoaDon = new Dictionary<string, CHoaDon>();

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    var data = bf.Deserialize(fs);

                    if (data is Tuple<Dictionary<string, CHoaDon>, List<CSanPham>> tupleData)
                    {
                        danhSachHoaDon = tupleData.Item1;
                    }
                    else
                    {
                        MessageBox.Show("Kiểu dữ liệu không đúng. Không thể đọc dữ liệu từ tập tin.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể đọc dữ liệu từ file: " + ex.Message);
            }

            return danhSachHoaDon;
        }

        private void LoadDataAndDisplay()
        {
            Dictionary<string, CHoaDon> danhSachHoaDon = ReadHoaDonFromFile("dshd.bin");
            cThongKe.DSHD=danhSachHoaDon.Values.ToList();
            DisplayDataInGridView();
        }
        private void DisplayDataInGridView()
        {
            DateTime tungay = dtpTungay.Value;
            DateTime denngay=dtpDenngay.Value;
            dgvthongke.Rows.Clear();
            List<CHoaDon> danhSachHoaDonTrongKhoangNgay = cThongKe.DSHD.Where(hd => hd.IsWithinDateRange(tungay, denngay)).ToList();
            Dictionary<CSanPham, Tuple<int, decimal>> thongTinSanPham = new Dictionary<CSanPham, Tuple<int, decimal>>();
            foreach (CHoaDon hoaDon in danhSachHoaDonTrongKhoangNgay)
            {
                foreach (CChiTietHoaDon chiTiet in hoaDon.DSSP)
                {
                    CSanPham sanPham = chiTiet.SanPham;
                    if (thongTinSanPham.ContainsKey(sanPham))
                    {
                        Tuple<int, decimal> thongTinHienTai = thongTinSanPham[sanPham];
                        int soLuongHienTai = thongTinHienTai.Item1 + chiTiet.Soluong;
                        decimal tongTienHienTai = thongTinHienTai.Item2 + hoaDon.ThanhTien(chiTiet);
                        thongTinSanPham[sanPham] = Tuple.Create(soLuongHienTai, tongTienHienTai);
                    }
                    else
                    {
                        thongTinSanPham[sanPham] = Tuple.Create(chiTiet.Soluong, hoaDon.ThanhTien(chiTiet));
                    }
                }
            }
            var danhSachSapXep = thongTinSanPham.OrderByDescending(x => x.Value.Item1);
            foreach (var kvp in danhSachSapXep)
            {
                CSanPham sanPham = kvp.Key;
                int soLuong = kvp.Value.Item1;
                decimal tongTien = kvp.Value.Item2;
                dgvthongke.Rows.Add(tungay.ToString("dd/MM/yyyy"),denngay.ToString("dd/MM/yyyy"), sanPham.MaSP,sanPham.TenSP, soLuong, tongTien);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LoadDataAndDisplay();
        }

        private void ThongKe_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    LoadDataAndDisplay(); break;
            }
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            SetRoundedButton(button1, 20);
            this.KeyPreview = true;
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
    }
}

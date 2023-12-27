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
    [Serializable]
    public partial class DoanhThu : Form
    {
        private CDoanhThu doanhThu;

        public DoanhThu()
        {
            InitializeComponent();
            doanhThu = new CDoanhThu();
            Image image = Properties.Resources.srdoanhthu;
            Size newSize = new Size(64, 64);
            image = new Bitmap(image, newSize);
            button1.Image = image;
            button1.Padding = new Padding(0);
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.TextAlign = ContentAlignment.MiddleRight;
        }

        private void DoanhThu_Load(object sender, EventArgs e)
        {
            SetRoundedButton(button1, 20);
            this.KeyPreview = true;
        }
        private void LoadDataAndDisplay()
        {
            Dictionary<string, CHoaDon> dsHoaDon = ReadHoaDonFromFile("dshd.bin");
            doanhThu.DSHoaDon = dsHoaDon.Values.ToList();
            DisplayDataInGridView();
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

        private void DisplayDataInGridView()
        {
            DateTime tuNgay = dtTuNgay.Value;
            DateTime denNgay= dtDenNgay.Value;
            decimal tongDoanhThu = 0;
            dgvDT.Rows.Clear();
            foreach (var hoaDon in doanhThu.DSHoaDon)
            {
                if (hoaDon.IsWithinDateRange(tuNgay, denNgay))
                {
                    dgvDT.Rows.Add(doanhThu.TuNgay.ToString("dd/MM/yyyy"), doanhThu.DenNgay.ToString("dd/MM/yyyy"), hoaDon.NgayTao.ToString("dd/MM/yyyy"), hoaDon.MaHD,hoaDon.MaNV, hoaDon.TongTien());
                    tongDoanhThu += hoaDon.TongTien();
                }
            }
            dgvDT.Rows.Add(null, null, null, null, null, null,null);
            dgvDT.Rows.Add("","","","", "Tổng Doanh thu: ",tongDoanhThu);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LoadDataAndDisplay();
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

        private void dgvDT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DoanhThu_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Enter:
                    LoadDataAndDisplay(); break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DA
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            SetButtonImageHD();
            SetButtonImageNV();
            SetButtonImageSP();
            SetButtonImageDT();
            SetButtonImageTK();
            SetButtonImageHDan();
            label1.BackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SanPham frmSanPham=new SanPham();
            frmSanPham.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NhanVien frmNhanVien = new NhanVien();
            frmNhanVien.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HoaDon frmHoaDon = new HoaDon();
            frmHoaDon.Show();
        }
        
        private void SetButtonImageHD()
        {
            string buttonText = button3.Text;
            Image img = Properties.Resources.hoadon3;
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 1, 1, 0, 1}
            });
            ImageAttributes imgAttributes = new ImageAttributes();
            imgAttributes.SetColorMatrix(colorMatrix);
            int buttonWidth = button3.Width;
            int buttonHeight = button3.Height;
            int iconWidth = buttonWidth;
            int iconHeight = (int)((float)img.Height / img.Width * buttonWidth);
            Image newImg = new Bitmap(iconWidth, iconHeight);
            using (Graphics g = Graphics.FromImage(newImg))
            {
                g.FillRectangle(Brushes.Red, 0, 0, buttonWidth, buttonHeight);
                g.DrawImage(img, new Rectangle(0, 0, iconWidth, iconHeight), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttributes);
            }
            button3.BackgroundImage = newImg;
            button3.BackgroundImageLayout = ImageLayout.Center;
            button3.ForeColor = Color.White;
            button3.Text = string.Empty;
            button3.Padding = new Padding(0, 0, 0, 10);
            Label label = new Label();
            label.Text = buttonText;
            label.ForeColor = Color.Red;
            label.Dock = DockStyle.Bottom;
            label.TextAlign = ContentAlignment.BottomCenter;
            button3.Controls.Add(label);
        }
        private void SetButtonImageNV()
        {
            string buttonText = button2.Text;
            Image img = Properties.Resources.nhanvien2;
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 1, 1, 0, 1}
            });
            ImageAttributes imgAttributes = new ImageAttributes();
            imgAttributes.SetColorMatrix(colorMatrix);
            int buttonWidth = button2.Width;
            int buttonHeight = button2.Height;
            int iconWidth = buttonWidth;
            int iconHeight = (int)((float)img.Height / img.Width * buttonWidth);
            Image newImg = new Bitmap(iconWidth, iconHeight);
            using (Graphics g = Graphics.FromImage(newImg))
            {
                g.FillRectangle(Brushes.Green, 0, 0, buttonWidth, buttonHeight);
                g.DrawImage(img, new Rectangle(0, 0, iconWidth, iconHeight), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttributes);
            }
            button2.BackgroundImage = newImg;
            button2.BackgroundImageLayout = ImageLayout.Center;
            button2.ForeColor = Color.White;
            button2.Text = string.Empty;
            button2.Padding = new Padding(0, 0, 0, 10);
            Label label = new Label();
            label.Text = buttonText;
            label.ForeColor = Color.Green;
            label.Dock = DockStyle.Bottom;
            label.TextAlign = ContentAlignment.BottomCenter;
            button2.Controls.Add(label);
        }
        private void SetButtonImageSP()
        {
            string buttonText = button1.Text;
            Image img = Properties.Resources.cafe;
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 1, 1, 0, 1}
            });
            ImageAttributes imgAttributes = new ImageAttributes();
            imgAttributes.SetColorMatrix(colorMatrix);
            int buttonWidth = button1.Width;
            int buttonHeight = button1.Height;
            int iconWidth = buttonWidth;
            int iconHeight = (int)((float)img.Height / img.Width * buttonWidth);
            Image newImg = new Bitmap(iconWidth, iconHeight);
            using (Graphics g = Graphics.FromImage(newImg))
            {
                g.FillRectangle(Brushes.Orange, 0, 0, buttonWidth, buttonHeight);
                g.DrawImage(img, new Rectangle(0, 0, iconWidth, iconHeight), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttributes);
            }
            button1.BackgroundImage = newImg;
            button1.BackgroundImageLayout = ImageLayout.Center;
            button1.ForeColor = Color.White;
            button1.Text = string.Empty;
            button1.Padding = new Padding(0, 0, 0, 10);
            Label label = new Label();
            label.Text = buttonText;
            label.ForeColor = Color.Orange;
            label.Dock = DockStyle.Bottom;
            label.TextAlign = ContentAlignment.BottomCenter;
            button1.Controls.Add(label);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DoanhThu frmDoanhThu = new DoanhThu();
            frmDoanhThu.Show();
        }
        private void SetButtonImageDT()
        {
            string buttonText = button4.Text;
            Image img = Properties.Resources.doanhthu;
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 1, 1, 0, 1}
            });
            ImageAttributes imgAttributes = new ImageAttributes();
            imgAttributes.SetColorMatrix(colorMatrix);
            int buttonWidth = button4.Width;
            int buttonHeight = button4.Height;
            int iconWidth = buttonWidth;
            int iconHeight = (int)((float)img.Height / img.Width * buttonWidth);
            Image newImg = new Bitmap(iconWidth, iconHeight);
            using (Graphics g = Graphics.FromImage(newImg))
            {
                g.FillRectangle(Brushes.Blue, 0, 0, buttonWidth, buttonHeight);
                g.DrawImage(img, new Rectangle(0, 0, iconWidth, iconHeight), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttributes);
            }
            button4.BackgroundImage = newImg;
            button4.BackgroundImageLayout = ImageLayout.Center;
            button4.ForeColor = Color.White;
            button4.Text = string.Empty;
            button4.Padding = new Padding(0, 0, 0, 10);
            Label label = new Label();
            label.Text = buttonText;
            label.ForeColor = Color.Blue;
            label.Dock = DockStyle.Bottom;
            label.TextAlign = ContentAlignment.BottomCenter;
            button4.Controls.Add(label);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ThongKe frmThongKe = new ThongKe();
            frmThongKe.Show();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            HuongDan frmHuongDan= new HuongDan();
            frmHuongDan.Show();
        }
        private void SetButtonImageTK()
        {
            string buttonText = button5.Text;
            Image img = Properties.Resources.thongke;
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 1, 1, 0, 1}
            });
            ImageAttributes imgAttributes = new ImageAttributes();
            imgAttributes.SetColorMatrix(colorMatrix);
            int buttonWidth = button5.Width;
            int buttonHeight = button5.Height;
            int iconWidth = buttonWidth;
            int iconHeight = (int)((float)img.Height / img.Width * buttonWidth);
            Image newImg = new Bitmap(iconWidth, iconHeight);
            using (Graphics g = Graphics.FromImage(newImg))
            {
                g.FillRectangle(Brushes.Purple, 0, 0, buttonWidth, buttonHeight);
                g.DrawImage(img, new Rectangle(0, 0, iconWidth, iconHeight), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttributes);
            }
            button5.BackgroundImage = newImg;
            button5.BackgroundImageLayout = ImageLayout.Center;
            button5.ForeColor = Color.White;
            button5.Text = string.Empty;
            button5.Padding = new Padding(0, 0, 0, 10);
            Label label = new Label();
            label.Text = buttonText;
            label.ForeColor = Color.Purple;
            label.Dock = DockStyle.Bottom;
            label.TextAlign = ContentAlignment.BottomCenter;
            button5.Controls.Add(label);
        }

        
        private void SetButtonImageHDan()
        {
            string buttonText = button6.Text;
            Image img = Properties.Resources.huongdan;
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 1, 1, 0, 1}
            });
            ImageAttributes imgAttributes = new ImageAttributes();
            imgAttributes.SetColorMatrix(colorMatrix);
            int buttonWidth = button6.Width;
            int buttonHeight = button6.Height;
            int iconWidth = buttonWidth;
            int iconHeight = (int)((float)img.Height / img.Width * buttonWidth);
            Image newImg = new Bitmap(iconWidth, iconHeight);
            using (Graphics g = Graphics.FromImage(newImg))
            {
                g.FillRectangle(Brushes.BlueViolet, 0, 0, buttonWidth, buttonHeight);
                g.DrawImage(img, new Rectangle(0, 0, iconWidth, iconHeight), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttributes);
            }
            button6.BackgroundImage = newImg;
            button6.BackgroundImageLayout = ImageLayout.Center;
            button6.ForeColor = Color.White;
            button6.Text = string.Empty;
            button6.Padding = new Padding(0, 0, 0, 10);
            Label label = new Label();
            label.Text = buttonText;
            label.ForeColor = Color.BlueViolet;
            label.Dock = DockStyle.Bottom;
            label.TextAlign = ContentAlignment.BottomCenter;
            button6.Controls.Add(label);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    [Serializable]
    public class CChiTietHoaDon
    {
        private CSanPham _sanPham;
        private int _soluong;
        private string  m_ghichu;
        
        
        public string GhiChu
        {
            get { return m_ghichu; }
            set { m_ghichu = value;}
        }
        public CSanPham SanPham
        {
            get { return _sanPham; }
            set { _sanPham = value; }
        }

        public int Soluong
        {
            get { return _soluong; }
            set { _soluong = value; }
        }
        public CChiTietHoaDon(CSanPham sanPham, int soLuong, string ghiChu)
        {
            SanPham = sanPham;
            Soluong = soLuong;
            GhiChu = ghiChu;
        }
    }
}


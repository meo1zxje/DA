using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    [Serializable]
    public class CHoaDon
    {
        private string m_mahd, m_manv;
        private List<CChiTietHoaDon> _DSSP;
        private DateTime m_ngaytao;
        public string MaHD
        {
            get { return m_mahd; }
            set { m_mahd = value; }
        }
        public string MaNV
        {
            get { return m_manv; }
            set { m_manv = value; }
        }
        public List<CChiTietHoaDon> DSSP
        {
            get { return _DSSP; }
            set { _DSSP = value; }
        }
        public DateTime NgayTao
        {
            get { return m_ngaytao; }
            set { m_ngaytao = value; }
        }
        public CHoaDon(string mahd, DateTime ngaytao, string manv)
        {
            m_mahd = mahd;
            m_manv = manv;
            m_ngaytao=ngaytao;
            DSSP = new List<CChiTietHoaDon>();
        }
        public void ThemChiTietHoaDon(CChiTietHoaDon chiTiet)
        {
            DSSP.Add(chiTiet);
        }
        
        public decimal ThanhTien(CChiTietHoaDon a)
        {
            decimal d = 0;
            d = a.Soluong * a.SanPham.GiaSP;
            return d;
        }

        public decimal TongTien()
        {
            decimal tong = 0;
            foreach (CChiTietHoaDon chiTiet in DSSP)
            {
                tong += ThanhTien(chiTiet);
            }
            return tong;
        }
        public bool IsWithinDateRange(DateTime tuNgay, DateTime denNgay)
        {
            DateTime ngayTaoHoaDon = NgayTao.Date;
            tuNgay = tuNgay.Date;
            return ngayTaoHoaDon >= tuNgay && ngayTaoHoaDon <= denNgay.AddDays(1);
        }
    }
}
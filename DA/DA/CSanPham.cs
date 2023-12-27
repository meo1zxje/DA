using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    [Serializable]
    public class CSanPham
    {
        private string m_masp, m_tensp;
        private decimal m_giasp;
        public string MaSP
        {
            get { return m_masp; }
            set { m_masp = value; }
        }
        public string TenSP
        {
            get { return m_tensp; }
            set { m_tensp = value; }
        }
        public decimal GiaSP
        {
            get { return m_giasp;}
            set { m_giasp = value;}
        }
        public CSanPham()
        {
            m_masp = "";
            m_tensp = "";
            m_giasp = 0;
        }
        public CSanPham(string ma, string ten, decimal gia)
        {
            m_masp = ma;
            m_tensp = ten;
            m_giasp = gia;
        }
    }
}

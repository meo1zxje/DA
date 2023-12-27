using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    [Serializable]
    [System.ComponentModel.Browsable(false)]
    public class CNhanVien
    {
        private string m_manv, m_tennv;
        private DateTime m_ngaysinh;
        private bool m_phai, m_chucvu;
        public string MaNV
        {
            get { return m_manv; }
            set { m_manv = value; }
        }
        public string TenNV
        {
            get { return m_tennv; } 
            set { m_tennv = value; }
        }
        public DateTime NgaySinh
        {
            get { return m_ngaysinh;}
            set { m_ngaysinh = value;}
        }
        public bool Phai
        {
            get { return m_phai; }
            set { m_phai = value; }
        }
        public bool ChucVu
        {
            get { return m_chucvu; }
            set { m_chucvu = value; }
        }
        public CNhanVien()
        {
            m_manv = "";
            m_tennv = "";
            m_ngaysinh= DateTime.Now;
            m_phai = true;
            m_chucvu = true;
        }
        public CNhanVien(string manv, string tennv, DateTime ns, bool phai, bool chucvu)
        {
            m_manv = manv;
            m_tennv = tennv;
            m_ngaysinh = ns;
            m_phai = phai;
            m_chucvu = chucvu;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    [Serializable]
    internal class CDoanhThu
    {
        DateTime m_tungay, m_denngay;
        private List<CHoaDon> _dsHoaDon;
        public DateTime TuNgay
        {
           get { return m_tungay; } 
           set { m_tungay = value; }
        }
        public DateTime DenNgay
        {
            get { return m_denngay; }
            set { m_denngay = value; }
        }
        public CDoanhThu()
        {
            m_tungay=DateTime.Now;
            m_denngay=DateTime.Now;
            _dsHoaDon = new List<CHoaDon>();
        }
        public CDoanhThu(DateTime tungay, DateTime denngay)
        {
            m_tungay = tungay;
            m_denngay = denngay;
            DSHoaDon = new List<CHoaDon>();
        }
        public List<CHoaDon> DSHoaDon
        {
            get { return _dsHoaDon; }
            set { _dsHoaDon = value; }
        }
        
    }
}

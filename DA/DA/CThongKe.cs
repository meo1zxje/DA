using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    [Serializable]
    internal class CThongKe
    {
        DateTime _tungay;
        DateTime _denngay;
        private List<CHoaDon> _dshd;
        public CThongKe()
        {
            _dshd = new List<CHoaDon>();
            _denngay = DateTime.Now;
            _tungay = DateTime.Now;
        }
        public DateTime tungay
        {
            get {  return this._tungay;}
            set { this._tungay = value;}
        }
        public DateTime denngay
        {
            get { return this._denngay; }
            set { this._denngay = value; }
        }
        public CThongKe (DateTime Tungay,DateTime Denngay)
        {
            this._tungay = Tungay;
            this._denngay= Denngay;
            DSHD=new List<CHoaDon>();
            
        }
        public List<CHoaDon> DSHD
        {
            get { return _dshd; }
            set { _dshd = value; }
        }

    }
}

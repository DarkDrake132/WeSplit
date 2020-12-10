using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplit.Model
{
    public class MemberWithPaid
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _paid;

        public int Paid
        {
            get { return _paid; }
            set { _paid = value; }
        }

        public MemberWithPaid(string name, int? paid)
        {
            Name = name;
            Paid = paid ?? default(int);
        }
    }
}

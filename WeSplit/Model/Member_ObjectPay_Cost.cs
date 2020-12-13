using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplit.Model
{
    public class Member_ObjectPay_Cost
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _idPaid;

        public int IdPaid
        {
            get { return _idPaid; }
            set { _idPaid = value; }
        }


        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _objectPaid;

        public string ObjectPaid
        {
            get { return _objectPaid; }
            set { _objectPaid = value; }
        }


        private int _paid;

        public int Paid
        {
            get { return _paid; }
            set { _paid = value; }
        }

        public Member_ObjectPay_Cost(int id, int idPaid, string name, string objectPaid, int? paid)
        {
            Id = id;
            IdPaid = idPaid;
            Name = name;
            ObjectPaid = objectPaid;
            Paid = paid ?? default(int);
        }
    }
}

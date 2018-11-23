using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WXDataDAL;

namespace WXDataBLL
{
    public class BaseBLL<T> where T:class

    {
        private BaseDAL<T> dal = new BaseDAL<T>();
        public BaseBLL( )
        {
            
        }
        public BaseBLL(BaseDAL<T> dal)
        {
            this.dal = dal;
        }
        public virtual List<T> GetAll()
        {
            return dal.GetAll();
        }

        public virtual T GetByPK(object pk)
        {
            return dal.GetByPK(pk);
        }

        public virtual List<T> Where(Func<T, bool> predicate)
        {
            return dal.Where(predicate);
        }
        public virtual bool Add(T info)
        {
            return dal.Add(info);
        }

        public virtual bool Update(T info)
        {
            return dal.Update(info);
        }

        public virtual bool Delete(object pk)
        {
            return dal.Delete(pk);
        }
    }
}

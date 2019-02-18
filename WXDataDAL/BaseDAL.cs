

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WXDataModel;

namespace WXDataDAL
{
    public class BaseDAL<T> where T:class
    {
        public virtual List<T> GetAll()
        {
            WXDataEntities db = new WXDataEntities();
            return db.Set<T>().ToList();
        }

        public virtual T GetByPK(object pk)
        {
            WXDataEntities db = new WXDataEntities();
            return db.Set<T>().Find(pk);
        }

        public virtual List<T> Where(Func<T, bool> predicate)
        {
            WXDataEntities db = new WXDataEntities();
            return db.Set<T>().Where(predicate).ToList();
        }

        public virtual bool Add(T info)
        {
            using (WXDataEntities db = new WXDataEntities())
            {
                db.Set<T>().Add(info);
                return db.SaveChanges() > 0;
            }
        }

        public virtual bool Update(T info)
        {
            using (WXDataEntities db = new WXDataEntities())
            { 
                db.Entry<T>(info).State= EntityState.Modified ;//附加实体类
                return db.SaveChanges() > 0;
            }
        }

        public virtual bool Delete(object pk)
        {
            using (WXDataEntities db = new WXDataEntities())
            {
                var info = db.Set<T>().Find(pk);
                if (info != null)
                {
                    db.Set<T>().Remove(info);
                    return db.SaveChanges() > 0;
                }
                else
                {
                    return false;
                }
            }
        }

        public virtual bool Delete(object [] pkList)
        {
            using (WXDataEntities db = new WXDataEntities())
            {
                var info = db.Set<T>().Find(pkList);
                if (info != null)
                {
                    db.Set<T>().Remove(info);
                    return db.SaveChanges() > 0;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}

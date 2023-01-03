using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AppWithPostman.Repository
{
    public class T_listinobaseRepository
    {
        static public List<T_listinobase> GetT_listinobase()
        {
            List<T_listinobase> _list = new List<T_listinobase>();
            using(var _db = new DbZohoEntities())
            {
                //_list = _db.T_listinobase.Where(x => x.IdZoho == null && x.DisattivaAccessoSito == 0).ToList();
            }
            return _list;
        }

        static public T_listinobase GetT_ListinobaseById(int Id)
        {
            T_listinobase _product = new T_listinobase();
            using(var _db = new DbZohoEntities())
            {
                _product = _db.T_listinobase.Where(x => x.IdListinoBase == Id).FirstOrDefault();
            }
            return _product;
        }

        static int UpdateT_ListinobaseSetIdSoho(T_listinobase product)
        {
            int outupdate = 0;
            //Utenti dataUtenti = new Utenti();
            using (var _dbo = new DbZohoEntities())
            {
                var _product = _dbo.T_listinobase.FirstOrDefault(i => i.IdListinoBase == product.IdListinoBase);

                //_product.IdZoho = product.IdZoho;

                _dbo.T_listinobase.AddOrUpdate(_product);
                outupdate = _dbo.SaveChanges();
            }
            return outupdate;
        }

        static public List<T_listinobase> GetT_listinobasToDelete()
        {
            List<T_listinobase> _list = new List<T_listinobase>();
            using (var _db = new DbZohoEntities())
            {
                //_list = _db.T_listinobase.Where(x => x.DisattivaAccessoSito == 1 ).ToList();
            }
            return _list;
        }

        static int UpdateT_listinobasSetIsdeletedFalse(T_listinobase product)
        {
            int outupdate = 0;

            using (var _dbo = new DbZohoEntities())
            {
                var _product = _dbo.T_listinobase.First(i => i.IdListinoBase == product.IdListinoBase);

                _dbo.T_listinobase.AddOrUpdate(_product);
                outupdate = _dbo.SaveChanges();
            }

            return outupdate;
        }
    }
}

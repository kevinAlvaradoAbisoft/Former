using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithPostman.Repository
{
    public class OrdiniRepository
    {
        public static List<Ordini> GetOrders()
        {
            List<Ordini> _ordiniList = new List<Ordini>();
            using (var _dbo = new DbZohoEntities())
            {
                _ordiniList = _dbo.Ordini.Include("Utenti").Include("T_listinobase").Include("T_Corriere")
                    .Where(d => d.IdZoho == null && d.Utenti.IsDeletedInZoho == false && d.Utenti.IdZohoAziende != null &&  d.IdUt == 1692)
                    .ToList();
            }

            return _ordiniList;
        }

        public static Ordini GetOrderById(int Id)
        {
            Ordini order = new Ordini();
            using (var _dbo = new DbZohoEntities())
            {
                order = _dbo.Ordini.Where(e => e.IdOrdine == Id).FirstOrDefault();
            }
            return order;
        }

        public static int UpdateOrder(Ordini order)
        {
            int outupdate = 0;
            using (var _dbo = new DbZohoEntities())
            {
                var data = _dbo.Ordini.First(x => x.IdOrdine == order.IdOrdine);
                data.IdZoho = order.IdZoho;
                _dbo.Ordini.AddOrUpdate(data);
                outupdate = _dbo.SaveChanges();
            }
            return outupdate;
        }
    }
}

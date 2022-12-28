using AppWithPostman.DTO;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace AppWithPostman.Repository
{
    public class OrdiniRepository
    {
        public static List<OrderDTO> GetOrders()
        {
            List<OrderDTO> _ordiniList = new List<OrderDTO>();
            using (var _dbo = new DbZohoEntities())
            {
                _ordiniList = (from order in _dbo.Ordini
                               join orderZoho in _dbo.OrderZoho on order.IdOrdine equals orderZoho.IdOrder
                               join utenti in _dbo.Utenti on order.IdUt equals utenti.IdUt
                               join user in _dbo.UserZoho on order.IdUt equals user.IdUser
                               join corriere in _dbo.T_Corriere on order.IdCorriere equals corriere.IdCorriere
                               join listino in _dbo.T_listinobase on order.IdListinoBase equals listino.IdListinoBase
                               where orderZoho.IdZoho == null && (user.IsDeletedInZoho == false || user.IsDeletedInZoho == null) && user.IdZohoAziende != null && utenti.IdUt == user.IdUser
                               select new OrderDTO
                               {
                                   IdOrder = order.IdOrdine,
                                   Total_Order = (decimal)order.TotaleOrdine,
                                   Total_Iva = (decimal)order.TotaleIva,
                                   Anteprima = order.Anteprima,
                                   Sorgente_Fronte = order.SorgenteFronte,
                                   Carrier = corriere.Descrizione,
                                   Sorgente_Retro = order.SorgenteRetro,
                                   Account_Name = new Account_Name
                                   {
                                       name = utenti.Nome,
                                       id = user.IdZohoAziende
                                   },
                                   Sub_Total = (decimal)order.TotaleNetto,
                                   Subject = listino.Nome,
                               }).ToList();
                /*_ordiniList = _dbo.Ordini.Include("Utenti").Include("T_listinobase").Include("T_Corriere")
                    .Where(d => d.IdZoho == null && d.Utenti.IsDeletedInZoho == false && d.Utenti.IdZohoAziende != null)
                    .ToList();*/
            }

            return _ordiniList;
        }

        public static OrderZoho GetOrderById(int Id)
        {
            OrderZoho order = new OrderZoho();
            using (var _dbo = new DbZohoEntities())
            {
                order = _dbo.OrderZoho.Where(e => e.IdOrder == Id).FirstOrDefault();
            }
            return order;
        }

        public static int UpdateOrder(OrderZoho order)
        {
            int outupdate = 0;
            using (var _dbo = new DbZohoEntities())
            {
                var data = _dbo.OrderZoho.First(x => x.IdOrder == order.IdOrder);
                data.IdZoho = order.IdZoho;
                _dbo.OrderZoho.AddOrUpdate(data);
                outupdate = _dbo.SaveChanges();
            }
            return outupdate;
        }
    }
}

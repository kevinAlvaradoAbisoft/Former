using AppWithPostman.DTO;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace AppWithPostman.Repository
{
    public class OrdiniRepository
    {
        public static string GetTipologiaConsegna(int? tipo)
        {
            switch (tipo)
            {
                case 0:
                    return "CN";

                case 1:
                    return "RC";
                case 2:
                    return "Prova";
                default:
                    return "Prova";
            }
        }
        public static string GetStato(int? stato)
        {
            switch (stato)
            {
                case 5:
                    return "PROVA";
                case 7:
                    return "Registrato";
                case 10:
                    return "Preinserito";
                case 20:
                    return "Registrato";
                case 21:
                    return "In Sospeso";
                case 22:
                    return "In Coda di Stampa";
                case 31:
                    return "In Attesa di finitura";
                case 32:
                    return "PROVA";
                case 40:
                    return "In Imballaggio";
                case 50:
                    return "Pronto per il ritiro";
                case 51:
                    return "Uscito da magazzino";
                case 60:
                    return "In consegna";
                case 70:
                    return "Consegnato";
                case 80:
                    return "Acconto";
                case 81:
                    return "Pagato";
                default:
                    return "PROVA";
            }
        }
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
                               join listino in _dbo.T_listinobase on order.IdListinoBase equals listino.IdListinoBase into fullListinoBase
                               from flistino in fullListinoBase.DefaultIfEmpty()
                               where orderZoho.IdZoho == null && (user.IsDeletedInZoho == false || user.IsDeletedInZoho == null) && user.IdZohoAziende != null && utenti.IdUt == user.IdUser && user.IdUser == 695
                               orderby order.IdOrdine descending
                               select new OrderDTO
                               {
                                   IdOrder = order.IdOrdine,
                                   IdOrdineInt = order.IdOrdineInt ?? default(int) ,
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
                                   Subject = string.IsNullOrEmpty(flistino.Nome) ? "SN" : flistino.Nome,
                                   PrezzoCalcolatoNetto=order.PrezzoCalcolatoNetto,
                                   qta= order.Qta,
                                   DataIns=order.DataIns,
                                   DataCambioStato=order.DataCambioStato,   
                                   DataPrevProduzione=order.DataPrevProduzione, 
                                   DataPrevConsegna=order.DataPrevConsegna,
                                   Sconto = order.Sconto,
                                   //Tipologia_di_consegna = order.TipoConsegna == null ? "CN" : GetTipologiaConsegna(order.TipoConsegna),
                                   //Status = order.Stato==null ? "PROVA" : GetStato(order.Stato),
                                   Tipologia_di_consegna = order.TipoConsegna ?? default(int),
                                   Status = order.Stato ?? default(int),
                                   Note = order.Annotazioni
                               }).ToList();

                //var _ordiniList1 = _dbo.Ordini.Include("Utenti").Include("T_listinobase").Include("T_Corriere")
                //    .Where(d => d.IdUt==695)
                //    .ToList();


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

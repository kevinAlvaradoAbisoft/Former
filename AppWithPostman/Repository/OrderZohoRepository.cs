using AppWithPostman.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithPostman.Repository
{
    public class OrderZohoRepository
    {
        public static List<OrderDTO> GetOrdersZoho()
        {
            List<Ordini> _orderList = new List<Ordini>();
            List<OrderDTO> _orderZohoList = new List<OrderDTO>();

            using (var _dbo = new DbZohoEntities())
            {
                _orderList = _dbo.Ordini
                    .Where(i=>i.IdUt == 162)
                    .ToList();

                foreach (var item in _orderList)
                {
                    var result = _dbo.OrderZoho
                    .Where(x => x.IdOrder == item.IdOrdine)
                    .FirstOrDefault();

                    if (result == null)
                    {
                        _orderZohoList.Add(new OrderDTO
                        {
                            IdOrder = item.IdOrdine
                        });
                    }
                }
            }
            return _orderZohoList;
        }

        public static int AddOrderZoho(List<OrderDTO> order)
        {
            int outupdate = 0;
            using (var _dbo = new DbZohoEntities())
            {
                foreach (var item in order)
                {
                    OrderZoho model = new OrderZoho();
                    model.IdOrder = item.IdOrder;
                    _dbo.OrderZoho.Add(model);
                    outupdate += _dbo.SaveChanges();
                }
            }
            return outupdate;
        }
    }
}

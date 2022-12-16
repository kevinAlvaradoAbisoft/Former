using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithPostman.Repository
{
    public class CompanieRepository
    {
        public static List<Utenti> GetUtentiCompanie()
        {
            List<Utenti> _utentiList = new List<Utenti>();
            using (var _dbo = new DbZohoEntities())
            {
                _utentiList = _dbo.Utenti
                    .Where(d => d.IdZohoAziende == null)
                    .ToList();
            }

            return _utentiList;
        }
        public static Utenti GetUtentiIdClient(int Id)
        {
            Utenti utenti = new Utenti();
            using (var _dbo = new DbZohoEntities())
            {
                utenti = _dbo.Utenti.Where(e => e.IdUt == Id).FirstOrDefault();
            }

            return utenti;
        }
        public static int UpdateUtentiCompanie(Utenti utenti)
        {
            int outupdate = 0;
            //Utenti dataUtenti = new Utenti();
            using (var _dbo = new DbZohoEntities())
            {
                var utenti1 = _dbo.Utenti.First(i => i.IdUt == utenti.IdUt);

                utenti1.IdZohoAziende = utenti.IdZohoAziende;

                _dbo.Utenti.AddOrUpdate(utenti1);
                outupdate = _dbo.SaveChanges();
            }
            return outupdate;
        }

    }
}

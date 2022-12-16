using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;


namespace AppWithPostman.Repository
{
    public class UtentiRepository
    {

        public static List<Utenti> GetUtenti()
        {
            List<Utenti> _utentiList = new List<Utenti>();
            using (var _dbo = new DbZohoEntities() )
            {
                _utentiList = _dbo.Utenti
                    .Where(d=>d.DisattivaAccessoSito==0 && d.IdZohoAziende != null)
                    .ToList();
            }

            return _utentiList;
        }
        public static List<Utenti> GetUtentiDelete()
        {
            List<Utenti> _utentiList = new List<Utenti>();
            using (var _dbo = new DbZohoEntities())
            {
                _utentiList = _dbo.Utenti
                    .Where(d => d.DisattivaAccessoSito == 1 && d.IsDeletedInZoho==true)
                    .ToList();
            }

            return _utentiList;
        }
        public static Utenti GetUtentiEmail(string email)
        {
            Utenti utenti = new Utenti();
            using (var _dbo = new DbZohoEntities())
            {
                utenti = _dbo.Utenti.Where(e => e.Email == email).FirstOrDefault();
            }

            return utenti;
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
        public static Utenti GetUtentiIdZoho(string Id)
        {
            Utenti utenti = new Utenti();
            using (var _dbo = new DbZohoEntities())
            {
                utenti = _dbo.Utenti.Where(e => e.ZohoId == Id).FirstOrDefault();
            }

            return utenti;
        }
        public static int  UpdateUtentiEmail(Utenti utenti)
        {
            int outupdate = 0;
            //Utenti dataUtenti = new Utenti();
            using (var _dbo = new DbZohoEntities())
            {
                var utenti1 = _dbo.Utenti.First(i => i.IdUt == utenti.IdUt);
                utenti1.ZohoId = utenti.ZohoId;
                utenti1.IdZoho = utenti.ZohoId;
                utenti1.IdZohoAziende = utenti.IdZohoAziende;
                utenti1.DisattivaAccessoSito = 1;
                //_dbo.Utenti.Attach(utenti1);
                //_dbo.Entry(utenti1).State = EntityState.Modified;
                _dbo.Utenti.AddOrUpdate(utenti1);
                outupdate= _dbo.SaveChanges();
            }
            return outupdate;
        }
        public static int UpdateUtentiDelete(Utenti utenti)
        {
            int outupdate = 0;

            using (var _dbo = new DbZohoEntities())
            {
                var utenti1 = _dbo.Utenti.First(i => i.IdUt == utenti.IdUt);
                utenti1.IsDeletedInZoho = false;

                _dbo.Utenti.AddOrUpdate(utenti1);
                outupdate = _dbo.SaveChanges();
            }

            return outupdate;
        }
    }
}

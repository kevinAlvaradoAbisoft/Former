using AppWithPostman.DTO;
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

        public static List<UserDTO> GetUtenti()
        {
            List<UserDTO> _utentiList = new List<UserDTO>();
            using (var _dbo = new DbZohoEntities() )
            {
                _utentiList = (from utenti in _dbo.Utenti
                               join user in _dbo.UserZoho on utenti.IdUt equals user.IdUser
                               where user.IdZohoAziende != null && utenti.DisattivaAccessoSito == 0
                               select new UserDTO
                               {
                                   IdUser = utenti.IdUt,
                                   First_Name = utenti.Nome,
                                   Last_Name = utenti.Cognome,
                                   Phone = utenti.Tel,
                                   Pec =   utenti.Pec,
                                   Sdi = utenti.CodiceSDI,
                                   Codice_Fiscale = utenti.CodFisc,
                                   Partita_Iva = utenti.Piva,
                                   Mobile = utenti.Cellulare,
                                   IdZoho = user.IdZoho,
                                   IdZohoAziende = user.IdZohoAziende,
                                   IsDeletedInZoho = user.IsDeletedInZoho
                               }).ToList();
                /*_utentiList = _dbo.Utenti
                    .Where(d=>d.DisattivaAccessoSito==0 && d.IdZohoAziende != null && d.IdUt == 1692)
                    .ToList();*/
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
        public static UserZoho GetUtentiIdClient(int Id)
        {
            UserZoho utenti = new UserZoho();
            using (var _dbo = new DbZohoEntities())
            {
                utenti = _dbo.UserZoho.Where(e => e.IdUser == Id).FirstOrDefault();
            }

            return utenti;
        }
        public static UserZoho GetUtentiIdZoho(string Id)
        {
            UserZoho utenti = new UserZoho();
            using (var _dbo = new DbZohoEntities())
            {
                utenti = _dbo.UserZoho.Where(e => e.IdZoho == Id).FirstOrDefault();
            }

            return utenti;
        }
        public static UserZoho GetUtentiIdZohoByIdZohoAziende(string Id)
        {
            UserZoho utenti = new UserZoho();
            using (var _dbo = new DbZohoEntities())
            {
                //utenti = _dbo.Utenti.Where(e => e.IdZohoAziende == Id).FirstOrDefault();
                utenti = _dbo.UserZoho.Where(e => e.IdZohoAziende == Id).FirstOrDefault();
            }

            return utenti;
        }
        public static int  UpdateUtentiEmail(UserZoho utenti)
        {
            int outupdate = 0;
            //Utenti dataUtenti = new Utenti();
            using (var _dbo = new DbZohoEntities())
            {
                //var utenti1 = _dbo.Utenti.First(i => i.IdUt == utenti.IdUt);
                var user = _dbo.UserZoho.First(i => i.IdUser == utenti.IdUser);
                user.IdZoho = utenti.IdZoho;
                //user.IdZohoAziende = utenti.IdZohoAziende;
              
                //_dbo.Utenti.Attach(utenti1);
                //_dbo.Entry(utenti1).State = EntityState.Modified;
                _dbo.UserZoho.AddOrUpdate(user);
                outupdate = _dbo.SaveChanges();

                var utenti1 = _dbo.Utenti.First(i => i.IdUt == utenti.IdUser);
                utenti1.DisattivaAccessoSito = 1;
                outupdate = _dbo.SaveChanges();

            }
            return outupdate;
        }
        public static int UpdateUtentiDelete(UserZoho utenti)
        {
            int outupdate = 0;

            using (var _dbo = new DbZohoEntities())
            {
                var utenti1 = _dbo.UserZoho.First(i => i.IdUser == utenti.IdUser);
                utenti1.IsDeletedInZoho = false;

                _dbo.UserZoho.AddOrUpdate(utenti1);
                outupdate = _dbo.SaveChanges();
            }

            return outupdate;
        }
    }
}

using AppWithPostman.DTO;
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
        public static string GetTipologia(int tipo)
        {
            switch (tipo)
            {
                case 0:
                    return "Prova";

                case 1:
                    return "Cliente Finale";
                case 2:
                    return "";
                case 3:
                    return "Rivenditore";
                case 4:
                    return "Agenti";
                default:
                    return "Prova";
            }
        }

        public static List<UserDTO> GetUtentiCompanie()
        {
            List<UserDTO> _utentiList = new List<UserDTO>();

            using (var _dbo = new DbZohoEntities())
            {
                _utentiList = (from utenti in _dbo.Utenti
                            join user in _dbo.UserZoho on utenti.IdUt equals user.IdUser
                               join province in _dbo.Province on utenti.IdProvincia equals province.ID into fullProvince
                               from fprovince in fullProvince.DefaultIfEmpty()
                               join nazioni in _dbo.Nazioni on utenti.IdNazione equals nazioni.IdNazione into fullNazione
                               from fnazione in fullNazione.DefaultIfEmpty()

                               where user.IdZohoAziende == null && utenti.DisattivaAccessoSito == 0
                                    && user.IdUser == 695
                               select new UserDTO
                               {
                                   IdUser = utenti.IdUt,
                                   IdRubricaInt = utenti.IdRubricaInt ?? default(int),
                                   First_Name = utenti.Nome,
                                   Last_Name = utenti.Cognome,
                                   Phone = utenti.Tel,
                                   IdZoho = user.IdZoho,
                                   IdZohoAziende = user.IdZohoAziende,
                                   IsDeletedInZoho = user.IsDeletedInZoho,
                                   RagSoc = utenti.RagSoc,
                                   Mailing_Country = fnazione.Nazione == null ? "" : fnazione.Nazione,
                                   Mailing_State = fprovince.PROVINCIA == null ? "" : fprovince.PROVINCIA,
                                   Mailing_City = utenti.Citta,
                                   Mailing_Street = utenti.Indirizzo,
                                   Mailing_Zip = utenti.Cap,
                                   //Tipologia = string.IsNullOrEmpty(utenti.TipoRub.ToString()) ? "Prova" : GetTipologia(utenti.TipoRub)
                                   Tipologia = utenti.TipoRub,                                   
                               }).ToList();

            }
            return _utentiList;
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
        public static int UpdateUtentiCompanie(UserZoho utenti)
        {
            int outupdate = 0;
            //Utenti dataUtenti = new Utenti();
            using (var _dbo = new DbZohoEntities())
            {
                //var utenti1 = _dbo.Utenti.First(i => i.IdUt == utenti.IdUt);
                var utenti1 = _dbo.UserZoho.First(i => i.IdUser == utenti.IdUser);

                utenti1.IdZohoAziende = utenti.IdZohoAziende;

                _dbo.UserZoho.AddOrUpdate(utenti1);
                outupdate = _dbo.SaveChanges();
            }
            return outupdate;
        }
        public static List<UserDTO> GetCompanieDelete()
        {
            List<UserDTO> _utentiList = new List<UserDTO>();
            using (var _dbo = new DbZohoEntities())
            {
                _utentiList = (from utenti in _dbo.Utenti
                               join user in _dbo.UserZoho on utenti.IdUt equals user.IdUser
                               where utenti.DisattivaAccessoSito == 1 && user.IsDeletedInZoho == true
                               select new UserDTO
                               {
                                   IdUser = utenti.IdUt,
                                   First_Name = utenti.Nome,
                                   Last_Name = utenti.Cognome,
                                   Phone = utenti.Tel,
                                   IdZoho = user.IdZoho,
                                   IdZohoAziende = user.IdZohoAziende,
                                   IsDeletedInZoho = user.IsDeletedInZoho
                               }).ToList();
                /*_utentiList = _dbo.Utenti
                    .Where(d => d.DisattivaAccessoSito == 1 && d.IsDeletedInZoho == true)
                    .ToList();*/
            }

            return _utentiList;
        }
        public static int UpdateCompanyDelete(Utenti utenti)
        {
            int outupdate = 0;

            using (var _dbo = new DbZohoEntities())
            {
                var utenti1 = _dbo.Utenti.First(i => i.IdUt == utenti.IdUt);
                //utenti1.IsDeletedInZoho = false;

                _dbo.Utenti.AddOrUpdate(utenti1);
                outupdate = _dbo.SaveChanges();
            }

            return outupdate;
        }
    }
}

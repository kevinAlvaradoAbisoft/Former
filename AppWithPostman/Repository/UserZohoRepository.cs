using AppWithPostman.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithPostman.Repository
{
    public class UserZohoRepository
    {
        public static List<UserDTO> GetUsersZoho()
        {
            List<Utenti> _utentiList = new List<Utenti>();
            List<UserDTO> _userList = new List<UserDTO>();

            using (var _dbo = new DbZohoEntities())
            {
                _utentiList = _dbo.Utenti
                    .Where(d => d.DisattivaAccessoSito == 0)
                    .ToList();

                foreach(var utenti in _utentiList)
                {
                    var result = _dbo.UserZoho.Where(x => x.IdUser == utenti.IdUt).FirstOrDefault();
                    if(result == null)
                    {
                        _userList.Add(new UserDTO
                        {
                            IdUser = utenti.IdUt
                        });
                    }
                }
            }
            return _userList;
        }

        public static int AddUserZoho(List<UserDTO> utenti)
        {
            int outupdate = 0;
            //Utenti dataUtenti = new Utenti();
            using (var _dbo = new DbZohoEntities())
            {
                foreach(var user in utenti)
                {
                   UserZoho model = new UserZoho();
                   model.IdUser = user.IdUser;
                    _dbo.UserZoho.Add(model);
                    outupdate += _dbo.SaveChanges();
                }
            }
            return outupdate;
        }
    }
}

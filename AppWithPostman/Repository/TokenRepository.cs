using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithPostman.Repository
{
    public class TokenRepository
    {
        public static tokens GetToken()
        {
            tokens tokens = new tokens();
            using (var _dbo = new DbZohoEntities())
            {
                tokens = _dbo.tokens.FirstOrDefault();
            }

            return tokens;
        }
        public static tokens AddToken(string token, string tokenWork)
        {
            tokens tokens = new tokens
            {
                Token_Refresh = token,
                Token_Work= tokenWork,
                LastUpdate =DateTime.Now
            };
            using (var _dbo = new DbZohoEntities())
            {
                _dbo.tokens.Add(tokens);
                _dbo.SaveChanges();
            }

            return tokens;
        }
        public static int UpdateToken(string token, string tokenWork)
        {
            var outtoken = 0;
            using (var _dbo = new DbZohoEntities())
            {
                var tokens = _dbo.tokens.FirstOrDefault();
                tokens.Token_Work = tokenWork;
                tokens.LastUpdate = DateTime.Now;
                _dbo.tokens.AddOrUpdate(tokens);
                outtoken = _dbo.SaveChanges();
            }

            return outtoken;
        }
    }
}

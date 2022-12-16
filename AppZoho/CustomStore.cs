using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.API.Authenticator.Store;
using Com.Zoho.Crm.API;
namespace AppZoho
{
    public  class CustomStore : TokenStore
    {
        public CustomStore()
        {
        }

        /// <summary></summary>
        /// <param name="user">A UserSignature class instance.</param>
        /// <param name="token">A Token (Com.Zoho.API.Authenticator.OAuthToken) class instance.</param>
        /// <returns>A Token class instance representing the user token details.</returns>
        public Token GetToken(UserSignature user, Token token)
        {
            // Add code to get the token
            return null;
        }

        /// <summary></summary>
        /// <param name="user">A UserSignature class instance.</param>
        /// <param name="token">A Token (Com.Zoho.API.Authenticator.OAuthToken) class instance.</param>
        public void SaveToken(UserSignature user, Token token)
        {
            // Add code to save the token
        }

        /// <summary></summary>
        /// <param name="user">A UserSignature class instance.</param>
        /// <param name="token">A Token (Com.Zoho.API.Authenticator.OAuthToken) class instance.</param>
        public void DeleteToken(UserSignature user, Token token)
        {
            // Add code to delete the token
        }

        public void GetTokens()
        {
            // Add code to get the all stored tokens
        }

        public void DeleteTokens()
        {
            // Add code to delete the all stored token
        }

        public void DeleteToken(Token token)
        {
            throw new NotImplementedException();
        }

        List<Token> TokenStore.GetTokens()
        {
            throw new NotImplementedException();
        }
    }
}

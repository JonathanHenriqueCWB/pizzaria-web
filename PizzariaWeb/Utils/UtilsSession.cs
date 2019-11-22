using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceEcoville.Utils
{
    public class UtilsSession
    {
        private readonly IHttpContextAccessor _http;
        private readonly string CARRINHO_ID = "carrinhoId";
        public UtilsSession(IHttpContextAccessor http)
        {
            _http = http;
        }
        public string RetonarCarrinhoId()
        {
            if(_http.HttpContext.Session.
                GetString(CARRINHO_ID) == null)
            {
                _http.HttpContext.Session.SetString
                    (CARRINHO_ID, Guid.NewGuid().ToString());
            }
            return _http.HttpContext.Session.
                GetString(CARRINHO_ID);
        }
    }
}

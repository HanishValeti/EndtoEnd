using System;
using System.Linq;
using System.Web.Http;
using EndtoEnd.Entity;
using EndtoEnd.Repository;

namespace EndtoEnd.Controllers
{
    public class SecuritiesWebApiMfController : ApiController
    {
        public readonly ISecuritiesMfRepository SecurityMfRepository;
        public SecuritiesWebApiMfController(ISecuritiesMfRepository securityMfRepository)
        {
            if (securityMfRepository == null)
            {
                throw new ArgumentNullException();
            }
            SecurityMfRepository = securityMfRepository;
        }

        [HttpGet, ActionName("GetSecuritiesMfList")]
        public IQueryable<SecurityMutualFundDto> GetIQueryable()
        {
            return SecurityMfRepository.GetListSecurityMf();
        }

        // GET api/<controller>/FCNTX
        [HttpGet, ActionName("GetSecurityMf")]
        public SecurityMutualFundDto GetobjSecurityMutualFundDto(string symbol)
        {
            return SecurityMfRepository.GetSecurityMfBySymbol(symbol);
        }
    }
}

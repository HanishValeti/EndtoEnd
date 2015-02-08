using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EndtoEnd.Entity;
using EndtoEnd.Repository;

namespace EndtoEnd.Controllers
{
    public class SecuritiesApiMfController : ApiController
    {
        public readonly ISecuritiesMfRepository SecurityMfRepository;
        public SecuritiesApiMfController(ISecuritiesMfRepository securityMfRepository)
        {
            if (securityMfRepository == null)
            {
                throw new ArgumentNullException();
            }
            SecurityMfRepository = securityMfRepository;
        }

        // GET api/<controller>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;
            try
            {
                var list = SecurityMfRepository.GetListSecurityMf();
                response = list != null ? Request.CreateResponse<IQueryable<SecurityMutualFundDto>>(HttpStatusCode.OK, list) : new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return response;
        }

        // GET api/<controller>/FCNTX
        [HttpGet]
        public HttpResponseMessage Get(string symbol)
        {
            HttpResponseMessage response;
            try
            {
                var securityMf = SecurityMfRepository.GetSecurityMfBySymbol(symbol);
                response = securityMf != null ? Request.CreateResponse<SecurityMutualFundDto>(HttpStatusCode.OK, securityMf) : new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception.Message);
            }
            return response;
        }
        [HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            HttpResponseMessage response;
            try
            {
                var securityMf = SecurityMfRepository.GetSecurityMfById(id);
                response = securityMf != null ? Request.CreateResponse<SecurityMutualFundDto>(HttpStatusCode.OK, securityMf) : new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception exception)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception.Message);
            }
            return response;
        }

        [HttpPut]
        public HttpResponseMessage Put(SecurityMutualFundDto updateDto)
        {
            HttpResponseMessage response;
            try
            {
                var optStatus = SecurityMfRepository.UpdateSecurityMf(updateDto);
                var securityMf = SecurityMfRepository.GetSecurityMfById(updateDto.Id);
                response = optStatus != null && optStatus.Status==true ? 
                    Request.CreateResponse<SecurityMutualFundDto>(HttpStatusCode.OK, securityMf) : new HttpResponseMessage(HttpStatusCode.NotFound);

            }
            catch (Exception exception)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception.Message);
                
            }
            return response;
        }

        [HttpPost]
        public HttpResponseMessage Post(SecurityMutualFundDto insertDto)
        {
            HttpResponseMessage response;
            try
            {
                var optStatus = SecurityMfRepository.InsertSecurityMfData(insertDto);
                var securityMf = SecurityMfRepository.GetSecurityMfById(insertDto.Id);
                response = optStatus != null && optStatus.Status == true ? Request.CreateResponse<SecurityMutualFundDto>(HttpStatusCode.OK, securityMf) : new HttpResponseMessage(HttpStatusCode.NotFound);

            }
            catch (Exception exception)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception.Message);

            }
            return response;
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage response;
            try
            {
                var optStatus = SecurityMfRepository.DeleteSecurityMfData(id);
                response = optStatus != null && optStatus.Status == true ? 
                    Request.CreateResponse<OperationStatus>(HttpStatusCode.OK, optStatus) 
                    : new HttpResponseMessage(HttpStatusCode.NotFound);

            }
            catch (Exception exception)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception.Message);

            }
            return response;
            
        }

    }
}
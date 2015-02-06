using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using EndtoEnd.Entity;

namespace EndtoEnd.Controllers
{
    public class SecurityMfMvcController : Controller
    {
        private readonly HttpClient _httpClient;
        public SecurityMfMvcController(HttpClient httpClient)
        {
	        this._httpClient = httpClient;
        }
        // GET: SecurityMf
        public ActionResult Index()
        {
            return View();
        }
        
        [System.Web.Mvc.HttpGet]
        public ActionResult Listall()
        {
            try
            {                
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response =
                    _httpClient.GetAsync(ConfigurationManager.AppSettings["ApiUrl"] + "SecuritiesApiMf").Result;
                response.EnsureSuccessStatusCode();
                List<SecurityMutualFundDto> list =
                response.Content.ReadAsAsync<List<SecurityMutualFundDto>>().Result;
                return PartialView("Listall", list);

            }
            catch (Exception ex)
            {
                return PartialView("Error", ex.Message);
            }
            
        }
        [System.Web.Mvc.HttpGet]
        public ActionResult Select(int id)
        {
            try
            {
                if (id != null)
                {
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response =
                        _httpClient.GetAsync(ConfigurationManager.AppSettings["ApiUrl"] + "SecuritiesApiMf/GetById/" + id)
                            .Result;
                    response.EnsureSuccessStatusCode();
                    SecurityMutualFundDto result = response.Content.ReadAsAsync<SecurityMutualFundDto>().Result;

                    return View("Select", result);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                return PartialView("Error", ex.Message);
            }
        }
        [System.Web.Mvc.HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                if (id != null)
                {
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response =
                        _httpClient.GetAsync(ConfigurationManager.AppSettings["ApiUrl"] + "SecuritiesApiMf/GetById/" + id)
                            .Result;
                    response.EnsureSuccessStatusCode();
                    SecurityMutualFundDto result = response.Content.ReadAsAsync<SecurityMutualFundDto>().Result;

                    return View("Edit",result);
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(SecurityMutualFundDto saveDto)
        {
            try
            {
                if (saveDto != null)
                {
                    if (ModelState.IsValid)
                    {
                        _httpClient.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        _httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
                        var response =
                            _httpClient.PutAsJsonAsync("SecuritiesApiMf", saveDto)
                                .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode())
                                .Result;

                        SecurityMutualFundDto result = response.Content.ReadAsAsync<SecurityMutualFundDto>().Result;

                        return PartialView("Select", result);
                    }
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
            
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [System.Web.Mvc.HttpPost]
        [ValidateInput(true)]
        public ActionResult Create(SecurityMutualFundDto creatDto)
        {
            try
            {
                if (creatDto != null)
                {
                    if(ModelState.IsValid)
                    { 
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    _httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
                    var response = _httpClient.PostAsJsonAsync("SecuritiesApiMf", creatDto).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).Result;

                    SecurityMutualFundDto result = response.Content.ReadAsAsync<SecurityMutualFundDto>().Result;
                    
                    return View("Select", result);
                    }
                }
                return View("Error");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [System.Web.Mvc.HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
                var response =
                    _httpClient.DeleteAsync("SecuritiesApiMf/Delete/" + id).Result;

                var optStatus = response.Content.ReadAsAsync<OperationStatus>().Result;
                if (optStatus.Status == true)
                {
                    return Json(optStatus);
                }
                else
                {
                    return PartialView("Error");
                }

            }
            catch (Exception)
            {
                return PartialView("Error");
                
            }
        }
        
    }
}
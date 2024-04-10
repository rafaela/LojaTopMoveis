using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Text;
using Topmoveis.Data;
using Topmoveis.Model;
using static System.Net.Mime.MediaTypeNames;

namespace LojaTopMoveis.Service
{
    public class LinkPagamentoService : ILinkPagamento
    {

        private readonly LojaContext _context;

        public LinkPagamentoService(LojaContext context)
        {
            _context = context;
        }

        public async Task<string> buscaAccessToken()
        {
            HttpClient cliente = new HttpClient();
            var url = "https://cieloecommerce.cielo.com.br/api/public/v2/token";
            var token = "M2Q1ZDQyZjYtYjMyYy00Zjk0LThiMDAtMzZlMDYyNzk3YmE0OmJMemZyOWtTTC9ONmdaWkYxZlFEaEoyUFNrR1lTWklyQ3UrcXhnZEI2NWs9";

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
            httpRequest.Content = new StringContent(string.Empty, Encoding.UTF8, "application/x-www-form-urlencodedl");
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", token);


            var response = await cliente.SendAsync(httpRequest);

            string result = response.Content.ReadAsStringAsync().Result;

            LinkPagamento link = new LinkPagamento();
            if (result != null || result != "")
            {
                link = JsonConvert.DeserializeObject<LinkPagamento>(result);
            }

            return link.Access_Token;
        }

        public async Task<ServiceResponse<LinkPagamento>> geraLinkPagamento(Guid id)
        {
            ServiceResponse<LinkPagamento> serviceResponse = new ServiceResponse<LinkPagamento>();
            HttpClient cliente = new HttpClient();
            var url = "https://cieloecommerce.cielo.com.br/api/public/v1/products/";
            var access_token = buscaAccessToken().Result;
            if (access_token == null)
            {
                serviceResponse.Sucess = false;
                serviceResponse.Data = null;
                return serviceResponse;
            }
            var sale = _context.Sales.Where(a => a.Id == id).FirstOrDefault();

            ModeloPagamento modelo = new ModeloPagamento();
            if(sale != null)
            {
                modelo.OrderNumber = sale.Name;
                modelo.Type = "Service";
                modelo.Name = sale.Name;
                modelo.Price = (sale.ValorTotal * 100) + "";
                modelo.ExpirationDate = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd");
                modelo.MaxNumberOfInstallments = sale.Quantityparcels + "";


            }

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(modelo);
            var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", access_token);


            var response = await cliente.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            if (result != "")
            {
                serviceResponse.Message = "nadaaa";
            }

            /*
            LinkPagamento link = new LinkPagamento();
            if (result != null || result != "")
            {
                link = JsonConvert.DeserializeObject<LinkPagamento>(result);
            }*/

            return serviceResponse;
        }
    }
}

using Livraria.MVC.Extensoes;
using Livraria.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;

namespace Livraria.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected readonly string _MetodoPath;
        protected readonly string _urlApi;

        public BaseController()
        {
            _urlApi = "https://localhost:44382/";
        }

        public List<T> RequestList<T>(T entidade, string url)
        {
            try
            {
                var client = new RestClient($"{_urlApi}{url}");
                client.AddHandler("application/json", NewtonsoftJsonSerializer.Default);

                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json");
                request.Method = Method.POST;
                request.JsonSerializer = NewtonsoftJsonSerializer.Default;
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(entidade);

                IRestResponse<List<T>> response = client.Execute<List<T>>(request);
                var model = response.Data;
                var retorno = response.StatusCode == System.Net.HttpStatusCode.OK;

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public T RequestItem<T>(T entidade, string url)
        {
            try
            {
                var client = new RestClient($"{_urlApi}{url}");
                client.AddHandler("application/json", NewtonsoftJsonSerializer.Default);

                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json");
                request.Method = Method.GET;
                request.JsonSerializer = NewtonsoftJsonSerializer.Default;
                request.RequestFormat = DataFormat.Json;
                //request.AddJsonBody(entidade);

                IRestResponse<T> response = client.Execute<T>(request);
                var model = response.Data;
                var retorno = response.StatusCode == System.Net.HttpStatusCode.OK;

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected bool RequestAction<T>(T entidade, string url, string action)
        {
            var client = new RestClient($"{_urlApi}{url}");
            var request = new RestRequest();

            if (action == "PUT")
                request.Method = Method.PUT;
            else if (action == "DELETE")
                request.Method = Method.DELETE;
            else
                request.Method = Method.POST;

            request.AddJsonBody(entidade);

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            var retorno = response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created;
            var msg = "";

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var erro = JsonConvert.DeserializeObject<JsonError>(response.Content);
                msg = erro.exceptionMessage;
            }

            return retorno;
        }
    }
}

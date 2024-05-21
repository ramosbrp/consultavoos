using ConsultaVoos.Service;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace ConsultaVoos.Controllers
{
    [Route("api/voos")]
    public class VoosController : Controller
    {
        private readonly ApiService _apiService = new ApiService();

        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetVoos()
        {
            var voosApiGol = await _apiService.GetVoosFromApi("https://dev.reserve.com.br/airapi/gol/getavailability?origin=Rio%20de%20Janeiro&destination=S%C3%A3o%20Paulo&date=2024-06-20");

            var voosApisLatam = await _apiService.GetVoosFromApi("https://dev.reserve.com.br/airapi/latam/flights?departureCity=Rio%20de%20Janeiro&arrivalCity=S%C3%A3o%20Paulo&departureDate=2024-06-20");

            var todosVoos = voosApiGol.Concat(voosApisLatam).ToList();

            var voosOrdenados = todosVoos.OrderBy(a => a.Tarifa).ThenBy(a => a.Partida).ToList();

            return voosOrdenados;
        }
    }
}

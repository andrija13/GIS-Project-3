using GISWeb.Models;
using GISWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Diagnostics;

namespace GISWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private IFeatureService featureService;

        public HomeController(ILogger<HomeController> logger, IFeatureService featureService)
        {
            this.logger = logger;
            this.featureService = featureService;
        }

        public IActionResult Index()
        {
            var viewModel = new VehicleViewModel();
            var allVehicle = featureService.GetAllVehicles();
            viewModel.Vehicles = allVehicle;

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetSpatialJoinData(int firstLayer, int spatialOperator, decimal? distance, int secondLayer)
        {
            var geoJson = featureService.GetSpatialJoinData(firstLayer, spatialOperator, distance, secondLayer);

            return Json(geoJson);
        }

        [HttpGet]
        public IActionResult GetTemporalData(int type, int temporalOperator, decimal value, int startTime, int endTime)
        {
            var geoJson = featureService.GetTemporalData(type, temporalOperator, value, startTime, endTime);

            return Json(geoJson);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
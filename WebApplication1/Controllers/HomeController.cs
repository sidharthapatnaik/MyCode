using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using Newtonsoft.Json.Serialization;
namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration Configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration _configuration)
        {
            _logger = logger;
            Configuration = _configuration;
        }

        

        public IActionResult Index()
        {
            string ConnectionString = this.Configuration.GetConnectionString(" ");
            var service = this.HttpContext.RequestServices;
            var log = (ILogger)service.GetService(typeof(ILogger));
            return View();
        }
            
        public ActionResult EmpDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmpDetails(EmpData empdata)
        {
            string name = empdata.EName;
            double esal = empdata.ESal;
            int output = 0;
            try
            {
                // var dbconfig = new ConfigurationBuilder()
                //.SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile("appsettings.json").Build();
                var dbconfig = Startup.ConnectionString;
                if (!string.IsNullOrEmpty(dbconfig.ToString()))
                {
                    string dbconnectionStr = dbconfig; //["ConnectionStrings:DefaultConnection"];
                    using (SqlConnection connection = new SqlConnection(dbconnectionStr))
                    {
                        string sql = "addemployee";
                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@sal", esal);
                            connection.Open();
                            output = cmd.ExecuteNonQuery();
                            connection.Close();
                            output = 1;                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            output = 0;
            return View();
        }
        [HttpPost]
        public IActionResult EmpData(EmpData empdata)
         {

            string name = empdata.EName;
            double esal = empdata.ESal;
            int output = 0;
            try
            {
                // var dbconfig = new ConfigurationBuilder()
                //.SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile("appsettings.json").Build();
                var dbconfig= Startup.ConnectionString;
                if (!string.IsNullOrEmpty(dbconfig.ToString()))
                    {
                    string dbconnectionStr = dbconfig;//["ConnectionStrings:DefaultConnection"];
                        using (SqlConnection connection = new SqlConnection(dbconnectionStr))
                        {
                            string sql = "addemployee";
                            using (SqlCommand cmd = new SqlCommand(sql, connection))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@name", name);
                                cmd.Parameters.AddWithValue("@sal", esal);                                
                                connection.Open();
                                output =cmd.ExecuteNonQuery();
                                connection.Close();
                                output = 1;
                                return Json(new { output = output }, new Newtonsoft.Json.JsonSerializerSettings());
                        }
                        }
                    }
                 
            } 
            catch (Exception ex)
            {
                throw;
            }
            output = 0;
           
            return Json(new { output = output }, new Newtonsoft.Json.JsonSerializerSettings());
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

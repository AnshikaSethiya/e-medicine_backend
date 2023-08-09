using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EMedicine_Backend.Models;
using System.Data.SqlClient;  

namespace EMedicine_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MedicinesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("getMedicine")]
        public Response getMedicine()
        {
            Response response = new Response();
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            try
            {
                response = dal.getMedicine(connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        [HttpGet]
        [Route("getSingleMedicine")]

        public Response getSingleMedicine(int Id)
        {
            Response response = new Response();
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            try
            {
                response = dal.getSingleMedicine(Id, connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

    }
}

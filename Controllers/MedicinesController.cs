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
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.getMedicine(connection);
            return response;
        }

        [HttpGet]
        [Route("getSingleMedicine")]

        public Response getSingleMedicine(int Id)
        {
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.getSingleMedicine(Id, connection);
            return response;
        }

    }
}

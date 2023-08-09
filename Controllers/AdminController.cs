using EMedicine_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EMedicine_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IConfiguration _configuration;
        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Route("addMedicine")]
        public Response addUpdateMedicine(Medicines medicines)
        {
            Response response = new Response();
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            try
            {
                response = dal.addMedicine(medicines, connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            return response;

        }

        [HttpGet]
        [Route("userList")]
        public Response userList()
        {
            Response response = new Response();
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            try
            {
                response = dal.userList(connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        [HttpGet]
        [Route("orderListAdmin")]
        public Response orderListAdmin()
        {
            Response response = new Response();
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            try
            {
                response = dal.orderListAdmin(connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        [HttpDelete]
        [Route("deleteProduct")]
        public Response deleteProduct(int id)
        {
            Response response = new Response();
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            try
            {
                response = dal.deleteProduct(id, connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        [HttpPut]
        [Route("updateMedicine")]
        public Response updateMedicine(Medicines medicines)
        {
            Response response = new Response();
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            try
            {
                response = dal.updateMedicine(medicines, connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        [HttpPut]
        [Route("updateStatus")]
        public Response updateStatus(Orders order)
        {
            Response response = new Response();
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            try
            {
                response = dal.updateStatus(order, connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }
    }
}

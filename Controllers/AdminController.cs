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
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.addMedicine(medicines, connection);
            return response;

        }

        [HttpGet]
        [Route("userList")]
        public Response userList()
        {
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.userList(connection);
            return response;
        }

        [HttpGet]
        [Route("orderListAdmin")]
        public Response orderListAdmin()
        {
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.orderListAdmin(connection);
            return response;
        }

        [HttpDelete]
        [Route("deleteProduct")]
        public Response deleteProduct(int id)
        {
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.deleteProduct(id,connection);
            return response;
        }

        [HttpPut]
        [Route("updateMedicine")]
        public Response updateMedicine(Medicines medicines)
        {
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.updateMedicine(medicines, connection);
            return response;
        }

        [HttpPut]
        [Route("updateStatus")]
        public Response updateStatus(Orders order)
        {
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.updateStatus(order, connection);
            return response;
        }
    }
}

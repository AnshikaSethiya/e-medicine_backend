using EMedicine_Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;


namespace EMedicine_Backend.Controllers
{
    [EnableCors()]
    //[EnableCors(origins: "http://localhost:3000/", headers: "*", methods: "*")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public Response register(Users users)
        {
            Response response = new Response();
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
                response = dal.register(users, connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        [HttpPost]
        [Route("login")]
        public Response login(LoginModel loginUser)
        {
            Response response = new Response();
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
                response = dal.login(loginUser, connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
                return response;
        }

        [HttpPost]
        [Route("addToCart")]
        public Response addToCart(Cart cart)
        {
            Response response = new Response();
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
                response = dal.addToCart(cart, connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        [HttpGet]
        [Route("getCartItem")]
        public Response getCartItem(int Id)
        {
            Response response = new Response();
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
                response = dal.GetCartItem(Id, connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        [HttpDelete]
        [Route("removeCartItem")]
        public Response removeCartItem(int UserId, int MedicineId)
        {
            Response response = new Response();
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
                response = dal.removeCartItem(UserId, MedicineId, connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        [HttpPost]
        [Route("placeOrder")]
        public Response placeOrder(int Id, Orders order)
        {
            Response response = new Response();
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
                response = dal.placeOrder(Id, order, connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        [HttpGet]
        [Route("orderList")]
        public Response orderList(int Id)
        {
            Response response = new Response();
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
                response = dal.orderList(Id, connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        [HttpPost]
        [Route("orderListItem")]
        public Response orderListItem(List<OrderItems> orderItems)
        {
            Response response = new Response();
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
                response = dal.orderListItem(orderItems, connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        [HttpGet]
        [Route("orderDetail")]
        public Response orderDetail(string orderNo)
        {
            Response response = new Response();
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
                response = dal.orderDetail(orderNo, connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        [HttpPost]
        [Route("viewUser")]
        public Response viewUser(Users users)
        {
            Response response = new Response();
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
                response = dal.viewUser(users, connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        [HttpPost]
        [Route("updateProfile")]
        public Response updateProfile(Users users)
        {
            Response response = new Response();
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
                response = dal.updateProfile(users, connection);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

    }
}

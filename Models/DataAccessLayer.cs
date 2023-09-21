using System.Data.SqlClient;
using System.Data;


namespace EMedicine_Backend.Models
{
    public class DataAccessLayer
    {
        public Response register(Users users, SqlConnection connection)
        {
            try
            {
                DateTime da = DateTime.Now;
                Response response = new Response();
                SqlCommand cmd = new SqlCommand("sp_register", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
                cmd.Parameters.AddWithValue("@LastName", users.LastName);
                cmd.Parameters.AddWithValue("@Password", users.Password);
                cmd.Parameters.AddWithValue("@Email", users.Email);
                cmd.Parameters.AddWithValue("@Fund", 0);
                cmd.Parameters.AddWithValue("@Type", "User");
                cmd.Parameters.AddWithValue("@Status", 0);
                cmd.Parameters.AddWithValue("@CreatedOn", da);
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
                if (i > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "User Registered Successfully!!";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Registration Failed!!";
                }
                return response;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public Response login(LoginModel loginUser, SqlConnection connection)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("sp_login", connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Email", loginUser.Email);
            da.SelectCommand.Parameters.AddWithValue("@Password", loginUser.Password);

            DataTable dt = new DataTable();
                da.Fill(dt);
                Response response = new Response();
                if (dt.Rows.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "User is Valid";
                    Users user = new Users();
                    user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                    user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                    response.user = user;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Invalid email or password!!";
                }

                return response;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                throw ex;
            }
        }
        public Response viewUser(Users users, SqlConnection connection)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("sp_viewUser", connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Response response = new Response();
                Users user = new Users();
                if (dt.Rows.Count > 0)
                {
                    user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                    user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                    user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                    user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                    user.Password = Convert.ToString(dt.Rows[0]["Password"]);
                    user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                    user.Fund = Convert.ToDecimal(dt.Rows[0]["Fund"]);
                    user.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                    response.StatusCode = 200;
                    response.StatusMessage = "User Exists";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Invalid does not exist!!";
                    response.user = user;
                }
                return response;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public Response updateProfile(Users users, SqlConnection connection)
        {
            try
            {
                Response response = new Response();

                SqlCommand cmd = new SqlCommand("sp_updateProfile", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
                cmd.Parameters.AddWithValue("@LastName", users.LastName);
                cmd.Parameters.AddWithValue("@Email", users.Email);
                cmd.Parameters.AddWithValue("@Password", users.Password);
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
                if (i > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Record Updated Successfully!!";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Some error occurred, Try after some time!!";
                }
                return response;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Response addToCart(Cart cart, SqlConnection connection)
        {
            try
            {
                Response response = new Response();
                SqlCommand cmd = new SqlCommand("sp_AddToCart", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", cart.UserId);
                cmd.Parameters.AddWithValue("@UnitPrice", cart.UnitPrice);
                cmd.Parameters.AddWithValue("@Name", cart.Name);
                cmd.Parameters.AddWithValue("@ImageUrl", cart.ImageUrl);
                cmd.Parameters.AddWithValue("@Discount", cart.Discount);
                cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
                cmd.Parameters.AddWithValue("@TotalPrice", cart.TotalPrice);
                cmd.Parameters.AddWithValue("@MedicineId", cart.MedicineId);
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Item added to cart!!";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Item could not be added!!";
                }
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Response removeCartItem(int UserId,int MedicineId, SqlConnection connection)
        {
            try
            {
                Response response = new Response();
                SqlCommand cmd = new SqlCommand("sp_removeCartItem", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MedicineID", MedicineId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                connection.Open();
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Item removed from cart!!";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Item could not be removed!!";
                }
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Response placeOrder(int Id, Orders orders, SqlConnection connection)
        {
            try
            {
                Response response = new Response();
                DateTime da = DateTime.Now;

                SqlCommand cmd = new SqlCommand("sp_PlaceOrderInsert", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", Id);
                cmd.Parameters.AddWithValue("@OrderNo", orders.OrderNo);
                cmd.Parameters.AddWithValue("@OrderTotal", orders.OrderTotal);
                cmd.Parameters.AddWithValue("@OrderStatus", "Pending");
                cmd.Parameters.AddWithValue("@OrderDate", da);
                connection.Open();
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    SqlCommand cmd2 = new SqlCommand("sp_PlaceOrderDelete", connection);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@UserId", Id);
                    cmd2.ExecuteNonQuery();

                    response.StatusCode = 200;
                    response.StatusMessage = "Order has been placed successfully!!";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Order could not be placed!!!";
                }
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Response orderList(int id, SqlConnection Connection)
        {
            try
            {
                Response response = new Response();
                List<Orders> listOrder = new List<Orders>();
                SqlDataAdapter da = new SqlDataAdapter("sp_OrderList", Connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //da.SelectCommand.Parameters.AddWithValue("@Type", users.Type);
                da.SelectCommand.Parameters.AddWithValue("@UserID", id);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Orders order = new Orders();
                        order.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                        order.OrderNo = Convert.ToString(dt.Rows[i]["OrderNo"]);
                        order.OrderTotal = Convert.ToDecimal(dt.Rows[i]["OrderTotal"]);
                        order.OrderStatus = Convert.ToString(dt.Rows[i]["OrderStatus"]);
                        order.OrderDate = Convert.ToDateTime(dt.Rows[i]["OrderDate"]);
                        listOrder.Add(order);
                    }
                    if (listOrder.Count > 0)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Order details fetched!!";
                        response.listOrders = listOrder;
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Order details are not available!!";
                        response.listOrders = null;
                    }
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Order details are not available!!";
                }
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public Response orderListItem(List<OrderItems> orderItems, SqlConnection connection)
        {
            try
            {
                Response response = new Response();
                int j = 0;
                connection.Open();
                foreach (OrderItems item in orderItems)
                {
                    using (SqlCommand cmd = new SqlCommand("sp_addOrderItems", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OrderNo", item.OrderNo);
                        cmd.Parameters.AddWithValue("@MedicineID", item.MedicineID);
                        cmd.Parameters.AddWithValue("@Name", item.Name);
                        cmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        cmd.Parameters.AddWithValue("@Discount", item.Discount);
                        cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                        cmd.Parameters.AddWithValue("@TotalPrice", item.TotalPrice);
                        j = cmd.ExecuteNonQuery();
                    }
                }
                connection.Close();

                if (j > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Hurray!! Order Placed Successfully.";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Item could not be added!!";
                }
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Response orderDetail(string orderNo, SqlConnection connection)
        {
            try
            {
                Response response = new Response();
                List<OrderItems> listOrderDetail = new List<OrderItems>();
                SqlDataAdapter da = new SqlDataAdapter("sp_OrderDetail", connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //da.SelectCommand.Parameters.AddWithValue("@Type", users.Type);
                da.SelectCommand.Parameters.AddWithValue("@OrderNo", orderNo);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OrderItems orderItems = new OrderItems();
                        orderItems.MedicineID = Convert.ToInt32(dt.Rows[i]["MedicineID"]);
                        orderItems.UnitPrice = Convert.ToInt32(dt.Rows[i]["UnitPrice"]);
                        orderItems.Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"]);
                        orderItems.Discount = Convert.ToDecimal(dt.Rows[i]["Discount"]);
                        orderItems.TotalPrice = Convert.ToInt32(dt.Rows[i]["TotalPrice"]);
                        orderItems.Name = Convert.ToString(dt.Rows[i]["Name"]);
                        listOrderDetail.Add(orderItems);
                    }
                    if (listOrderDetail.Count > 0)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Order details fetched!!";
                        response.listItems = listOrderDetail;
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Order details are not available!!";
                        response.listItems = null;
                    }
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "No data to fetch order detail!!";
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public Response getMedicine(SqlConnection connection)
        {
            try
            {
                Response response = new Response();
                List<Medicines> medicineList = new List<Medicines>();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Medicines";
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Medicines medicine = new Medicines();
                    medicine.ID = (int)dr["ID"];
                    medicine.Name = dr["Name"].ToString();
                    medicine.Manufacturer = dr["Manufacturer"].ToString();
                    medicine.Details = dr["Details"].ToString();
                    medicine.UnitPrice = (decimal)dr["UnitPrice"];
                    medicine.Discount = (decimal)dr["Discount"];
                    medicine.Quantity = (int)dr["Quantity"];
                    var expDate = dr["ExpDate"].ToString();
                    medicine.ExpDate = Convert.ToDateTime(expDate);
                    medicine.ImageUrl = dr["ImageUrl"].ToString();
                    medicineList.Add(medicine);
                }
                dr.Close();
                if (medicineList.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Medicine details fetched!!";
                    response.listMedicines = medicineList;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Medicine details are not available!!";
                    response.listMedicines = null;
                }
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Response getSingleMedicine(int Id, SqlConnection connection)
        {
            try
            {
                Response response = new Response();
                Medicines medicine = new Medicines();
                SqlCommand cmd = new SqlCommand("sp_getSingleMedicine", connection);
                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", Id);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    medicine.Name = dr["Name"].ToString();
                    medicine.Manufacturer = dr["Manufacturer"].ToString();
                    medicine.Details = dr["Details"].ToString();
                    medicine.UnitPrice = (decimal)dr["UnitPrice"];
                    medicine.Discount = (decimal)dr["Discount"];
                    medicine.Quantity = (int)dr["Quantity"];
                    medicine.ExpDate = (DateTime)dr["ExpDate"];
                    medicine.ImageUrl = dr["ImageUrl"].ToString();
                    //medicine.Type = dr["Type"].ToString();

                    response.StatusCode = 200;
                    response.medicine = medicine;
                    connection.Close();
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "No data found!!";
                    response.medicine = null;
                }
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Response GetCartItem(int Id, SqlConnection connection)
        {
            try
            {
                Response response = new Response();
                //Medicines medicine = new Medicines();
                List<Cart> cartList = new List<Cart>();
                SqlCommand cmd = new SqlCommand("sp_getCartData", connection);
                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", Id);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Cart cart = new Cart();
                    cart.Name = dr["Name"].ToString();
                    cart.ImageUrl = dr["ImageUrl"].ToString();
                    cart.MedicineId = (int)dr["MedicineID"];
                    cart.Quantity = (int)dr["Quantity"];
                    cart.TotalPrice = (decimal)dr["TotalPrice"];
                    cart.UnitPrice = (decimal)dr["UnitPrice"];
                    cart.Discount = (decimal)dr["Discount"];
                    cartList.Add(cart);

                }
                dr.Close();
                if (cartList.Count > 0)
                {
                    response.StatusCode = 200;
                    response.listCart = cartList;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "No data found!!";
                    response.cart = null;
                }
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //--------------admin-------------- --//

        public Response deleteProduct(int id, SqlConnection connection)
        {
            try
            {
                Response response = new Response();
                SqlCommand cmd = new SqlCommand("sp_removeProduct", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                connection.Open();
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Item removed from cart!!";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Item could not be removed!!";
                }
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Response addMedicine(Medicines medicines, SqlConnection connection)
        {
            try
            {
                Response response = new Response();
                SqlCommand cmd = new SqlCommand("sp_AddMedicine", connection);
                var date = medicines.ExpDate.ToString("yyyy/MM/dd");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", medicines.Name);
                cmd.Parameters.AddWithValue("@Manufacturer", medicines.Manufacturer);
                cmd.Parameters.AddWithValue("@UnitPrice", medicines.UnitPrice);
                cmd.Parameters.AddWithValue("@Discount", medicines.Discount);
                cmd.Parameters.AddWithValue("@Quantity", medicines.Quantity);
                cmd.Parameters.AddWithValue("@ExpDate", date);
                cmd.Parameters.AddWithValue("@ImageUrl", medicines.ImageUrl);
                cmd.Parameters.AddWithValue("@Details", medicines.Details);
                //cmd.Parameters.AddWithValue("@Status", medicines.Status);
                //cmd.Parameters.AddWithValue("@Type", medicines.Type);
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
                if (i > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Medicine inserted successfully!!";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Medicine did not saved. Try again later!!!";
                }
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Response updateMedicine(Medicines medicines, SqlConnection connection)
        {
            try
            {
                Response response = new Response();
                SqlCommand cmd = new SqlCommand("sp_UpdateMedicine", connection);
                var date = medicines.ExpDate.ToString("yyyy/MM/dd");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", medicines.ID);
                cmd.Parameters.AddWithValue("@Name", medicines.Name);
                cmd.Parameters.AddWithValue("@Manufacturer", medicines.Manufacturer);
                cmd.Parameters.AddWithValue("@UnitPrice", medicines.UnitPrice);
                cmd.Parameters.AddWithValue("@Discount", medicines.Discount);
                cmd.Parameters.AddWithValue("@Quantity", medicines.Quantity);
                cmd.Parameters.AddWithValue("@ExpDate", date);
                cmd.Parameters.AddWithValue("@ImageUrl", medicines.ImageUrl);
                cmd.Parameters.AddWithValue("@Details", medicines.Details);
                //cmd.Parameters.AddWithValue("@Status", medicines.Status);
                //cmd.Parameters.AddWithValue("@Type", medicines.Type);
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
                if (i > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Medicine detail update successfully!!";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Medicine did not updated. Try again later!!!";
                }
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public Response userList(SqlConnection connection)
        {
            try
            {
                Response response = new Response();
                List<Users> listUsers = new List<Users>();
                SqlDataAdapter da = new SqlDataAdapter("sp_UserList", connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Users user = new Users();
                        user.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                        user.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                        user.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                        user.Email = Convert.ToString(dt.Rows[i]["Email"]);
                        user.Fund = Convert.ToDecimal(dt.Rows[i]["Fund"]);
                        user.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                        user.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                        listUsers.Add(user);
                    }
                    if (listUsers.Count > 0)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "User details fetched!!";
                        response.listUsers = listUsers;
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "User details are not available!!";
                        response.listUsers = null;
                    }
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "User details are not available!!";
                }
                return response;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public Response orderListAdmin(SqlConnection Connection)
        {
            try
            {
                Response response = new Response();
                List<Orders> listOrder = new List<Orders>();
                SqlDataAdapter da = new SqlDataAdapter("sp_OrderListAdmin", Connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Orders order = new Orders();
                        order.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                        order.OrderNo = Convert.ToString(dt.Rows[i]["OrderNo"]);
                        order.OrderTotal = Convert.ToDecimal(dt.Rows[i]["OrderTotal"]);
                        order.OrderStatus = Convert.ToString(dt.Rows[i]["OrderStatus"]);
                        order.OrderDate = Convert.ToDateTime(dt.Rows[i]["OrderDate"]);
                        listOrder.Add(order);
                    }
                    if (listOrder.Count > 0)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Order details fetched!!";
                        response.listOrders = listOrder;
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Order details are not available!!";
                        response.listOrders = null;
                    }
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Order details are not available!!";
                }
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Response updateStatus(Orders order, SqlConnection connection)
        {
            try
            {
                Response response = new Response();
                SqlCommand cmd = new SqlCommand("sp_updateStatus", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderNo", order.OrderNo);
                cmd.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
                if (i > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Order updated successfully!!";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Order Status not updated. Try again later!!!";
                }
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //public Response UpdatePRoduct

    }
}

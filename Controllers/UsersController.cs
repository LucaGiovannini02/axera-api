using AxeraPJW.Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AxeraPJW.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly string StringConnection = "Server = localhost\\SQLEXPRESS; Database= Axera; Trusted_connection=true; ";

        [HttpGet]
        public ActionResult<Users> GetUsers()
        {
            List<Users> users = new List<Users>();
            SqlConnection? mySqlConnection = null;

            try
            {
                mySqlConnection = new SqlConnection(StringConnection);
                SqlCommand mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.CommandText = "Select * From Users Where deleted=0";
                mySqlConnection.Open();
                SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {

                    Users myUsers = new Users();
                    myUsers.id = mySqlDataReader.GetInt32(0);
                    myUsers.fullname = mySqlDataReader.GetString(1);
                    myUsers.age = mySqlDataReader.GetInt32(2);
                    myUsers.parent = mySqlDataReader.IsDBNull(3) ? null : mySqlDataReader.GetString(3);
                    myUsers.mail = mySqlDataReader.GetString(4);
                    myUsers.user_verified = mySqlDataReader.GetBoolean(5);
                    myUsers.newsletter_allow = mySqlDataReader.GetBoolean(6);
                    myUsers.allergies = mySqlDataReader.IsDBNull(7) ? null : mySqlDataReader.GetString(7);
                    myUsers.waiver_allow = mySqlDataReader.GetBoolean(8);
                    myUsers.privacy_allow = mySqlDataReader.GetBoolean(9);
                    myUsers.phone_number = mySqlDataReader.GetString(10);

                    users.Add(myUsers);
                }

            }
            catch (Exception ex)
            {


            }
            finally { mySqlConnection?.Close(); }

            return Ok(users);
        }

        [HttpPost]
        public ActionResult PostUsers(Users myusers)
        {

            SqlConnection? mySqlConnection = null;

            try
            {
                mySqlConnection = new SqlConnection(StringConnection);
                SqlCommand mySqlCommand = new SqlCommand();
                mySqlConnection.Open();
                mySqlCommand.Connection = mySqlConnection;

                mySqlCommand.Parameters.Add("@fullname", SqlDbType.NVarChar);
                mySqlCommand.Parameters.Add("@age", SqlDbType.Int);
                mySqlCommand.Parameters.Add("@parent", SqlDbType.NVarChar);
                mySqlCommand.Parameters.Add("@mail", SqlDbType.NVarChar);
                mySqlCommand.Parameters.Add("@user_verified", SqlDbType.Bit);
                mySqlCommand.Parameters.Add("@newsletter_allow", SqlDbType.Bit);
                mySqlCommand.Parameters.Add("@allergies", SqlDbType.NVarChar);
                mySqlCommand.Parameters.Add("@waiver_allow", SqlDbType.Bit);
                mySqlCommand.Parameters.Add("@privacy_allow", SqlDbType.Bit);
                mySqlCommand.Parameters.Add("@phone_number", SqlDbType.NVarChar);

                mySqlCommand.Parameters["@fullname"].Value = myusers.fullname;
                mySqlCommand.Parameters["@age"].Value = myusers.age;
                mySqlCommand.Parameters["@parent"].Value = myusers.parent;
                mySqlCommand.Parameters["@mail"].Value = myusers.mail;
                mySqlCommand.Parameters["@user_verified"].Value = myusers.user_verified;
                mySqlCommand.Parameters["@newsletter_allow"].Value = myusers.newsletter_allow;
                mySqlCommand.Parameters["@allergies"].Value = myusers.allergies;
                mySqlCommand.Parameters["@waiver_allow"].Value = myusers.waiver_allow;
                mySqlCommand.Parameters["@privacy_allow"].Value = myusers.privacy_allow;
                mySqlCommand.Parameters["@phone_number"].Value = myusers.phone_number;


                mySqlCommand.CommandText = "INSERT INTO Users(fullname, age, parent, mail, user_verified, newsletter_allow, allergies, waiver_allow, privacy_allow, phone_number) Values(@fullname, @age, @parent, @mail, @user_verified, @newsletter_allow, @allergies, @waiver_allow, @privacy_allow, @phone_number)";
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally { mySqlConnection?.Close(); }

            return Ok();

        }

        [HttpGet("{id}")]
        public ActionResult<Users> GetUsers(int id)
        {
            List<Users> users = new List<Users>();
            SqlConnection? mySqlConnection = null;

            try
            {
                mySqlConnection = new SqlConnection(StringConnection);
                SqlCommand mySqlCommand = new SqlCommand();
                mySqlCommand.Parameters.Add("@id", SqlDbType.Int);
                mySqlCommand.Parameters["@id"].Value = id;
                mySqlCommand.Connection = mySqlConnection;

                mySqlCommand.CommandText = "select * From Users where id=@id and deleted=0";
                mySqlConnection.Open();
                SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {

                    Users myUsers = new Users();
                    myUsers.id = mySqlDataReader.GetInt32(0);
                    myUsers.fullname = mySqlDataReader.GetString(1);
                    myUsers.age = mySqlDataReader.GetInt32(2);
                    myUsers.parent = mySqlDataReader.IsDBNull(3) ? null : mySqlDataReader.GetString(3);
                    myUsers.mail = mySqlDataReader.GetString(4);
                    myUsers.user_verified = mySqlDataReader.GetBoolean(5);
                    myUsers.newsletter_allow = mySqlDataReader.GetBoolean(6);
                    myUsers.allergies = mySqlDataReader.IsDBNull(7) ? null : mySqlDataReader.GetString(7);
                    myUsers.waiver_allow = mySqlDataReader.GetBoolean(8);
                    myUsers.privacy_allow = mySqlDataReader.GetBoolean(9);
                    myUsers.phone_number = mySqlDataReader.GetString(10);

                    users.Add(myUsers);

                }
            }
            catch (Exception ex)
            {
            }
            finally { mySqlConnection?.Close(); }

            return Ok(users);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {

            SqlConnection? mySqlConnection = null;

            try
            {
                mySqlConnection = new SqlConnection(StringConnection);
                mySqlConnection.Open();
                SqlCommand mySqlCommand = mySqlConnection.CreateCommand();

                mySqlCommand.Parameters.Add("@id", SqlDbType.Int);
                mySqlCommand.Parameters["@id"].Value = id;

                string query = "UPDATE Users SET Deleted=1 WHERE id=@id";

                mySqlCommand.CommandText = query;
                mySqlCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                mySqlConnection.Close();
            }
            return Ok();
        }

        [HttpPatch("{id}/verified")]

        public ActionResult SetUserVerified(int id, bool user_verified) {

            SqlConnection? mySqlConnection = null;

            try {
                mySqlConnection = new SqlConnection(StringConnection);
                SqlCommand mySqlCommand = new SqlCommand();
                mySqlConnection.Open();
                mySqlCommand.Connection = mySqlConnection;

                mySqlCommand.Parameters.Add("@user_verified", SqlDbType.Bit); 
                mySqlCommand.Parameters.Add("@id", SqlDbType.Int);
                mySqlCommand.Parameters["@id"].Value = id;
                mySqlCommand.Parameters["@user_verified"].Value=user_verified;

                mySqlCommand.CommandText = "Update Users Set user_verified=@user_verified WHERE id=@id ";
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex) {
            }
            finally { 

                mySqlConnection?.Close(); 
            }   
            return Ok();
        }

        [HttpPatch("{id}/newsletter")]

        public ActionResult SetUserNewsletter(int id, bool newsletter_allow)
        {

            SqlConnection? mySqlConnection = null;

            try
            {
                mySqlConnection = new SqlConnection(StringConnection);
                SqlCommand mySqlCommand = new SqlCommand();
                mySqlConnection.Open();
                mySqlCommand.Connection = mySqlConnection;

                mySqlCommand.Parameters.Add("@newsletter_allow", SqlDbType.Bit);
                mySqlCommand.Parameters.Add("@id", SqlDbType.Int);
                mySqlCommand.Parameters["@id"].Value = id;
                mySqlCommand.Parameters["@newsletter_allow"].Value = newsletter_allow;

                mySqlCommand.CommandText = "Update Users Set newsletter_allow=@newsletter_allow WHERE id=@id ";
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                mySqlConnection?.Close();
            }
            return Ok();
        }

        [HttpPatch("{id}/waiver")]

        public ActionResult SetUserWaiver(int id, bool waiver_allow)
        {

            SqlConnection? mySqlConnection = null;

            try
            {
                mySqlConnection = new SqlConnection(StringConnection);
                SqlCommand mySqlCommand = new SqlCommand();
                mySqlConnection.Open();
                mySqlCommand.Connection = mySqlConnection;

                mySqlCommand.Parameters.Add("@waiver_allow", SqlDbType.Bit);
                mySqlCommand.Parameters.Add("@id", SqlDbType.Int);
                mySqlCommand.Parameters["@id"].Value = id;
                mySqlCommand.Parameters["@waiver_allow"].Value = waiver_allow;

                mySqlCommand.CommandText = "Update Users Set waiver_allow=@waiver_allow WHERE id=@id ";
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                mySqlConnection?.Close();
            }
            return Ok(); 
        }

        [HttpPatch("{id}/privacy")]

        public ActionResult SetUserPrivacy(int id, bool privacy_allow)
        {

            SqlConnection? mySqlConnection = null;

            try
            {
                mySqlConnection = new SqlConnection(StringConnection);
                SqlCommand mySqlCommand = new SqlCommand();
                mySqlConnection.Open();
                mySqlCommand.Connection = mySqlConnection;

                mySqlCommand.Parameters.Add("@privacy_allow", SqlDbType.Bit);
                mySqlCommand.Parameters.Add("@id", SqlDbType.Int);
                mySqlCommand.Parameters["@id"].Value = id;
                mySqlCommand.Parameters["@privacy_allow"].Value = privacy_allow;

                mySqlCommand.CommandText = "Update Users Set privacy_allow=@privacy_allow WHERE id=@id ";
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                mySqlConnection?.Close();
            }
            return Ok();
        }




    }
}

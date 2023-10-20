using AxeraPJW.Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;


namespace AxeraPJW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly string StringConnection = "Server = localhost\\SQLEXPRESS; Database= Axera; Trusted_connection=true; ";

        [HttpGet]
        public ActionResult<Courses> GetCourses()
        {
            List<Courses> courses = new List<Courses>();
            SqlConnection? mySqlConnection = null;

            try
            {
                mySqlConnection = new SqlConnection(StringConnection);
                SqlCommand mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.CommandText = "Select * From Courses Where deleted=0";
                mySqlConnection.Open();
                SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read()) {

                    Courses myCourses = new Courses();
                    myCourses.id = mySqlDataReader.GetInt32(0);
                    myCourses.name = mySqlDataReader.GetString(1);
                    myCourses.description = mySqlDataReader.GetString(2);

                    courses.Add(myCourses);
                }

            }
            catch (Exception ex) {


            }
            finally { mySqlConnection?.Close(); }

            return Ok(courses);
        }


        [HttpPost]
        public ActionResult PostCourses(Courses mycourses) {

            SqlConnection? mySqlConnection = null;

            try
            {
                mySqlConnection = new SqlConnection(StringConnection);
                SqlCommand mySqlCommand = new SqlCommand();
                mySqlConnection.Open();
                mySqlCommand.Connection = mySqlConnection;

                mySqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar);
                mySqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar);

                mySqlCommand.Parameters["@Name"].Value = mycourses.name;
                mySqlCommand.Parameters["@Description"].Value = mycourses.description;

                mySqlCommand.CommandText = "INSERT INTO Courses(Name, Description) Values(@Name, @Description)";
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally { mySqlConnection?.Close(); }

            return Ok();

        }

        [HttpGet("{id}")]
        public ActionResult<Courses> GetCourse(int id)
        {
            List<Courses> courses = new List<Courses>();
            SqlConnection? mySqlConnection = null;

            try
            {
                mySqlConnection = new SqlConnection(StringConnection);
                SqlCommand mySqlCommand = new SqlCommand();
                mySqlCommand.Parameters.Add("@id", SqlDbType.Int);
                mySqlCommand.Parameters["@id"].Value= id;
                mySqlCommand.Connection= mySqlConnection;

                mySqlCommand.CommandText = "select * From Courses where id=@id and deleted=0"; 
                mySqlConnection.Open();
                SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                while(mySqlDataReader.Read()){

                    Courses myCourses = new Courses();
                    myCourses.id = mySqlDataReader.GetInt32(0);
                    myCourses.name = mySqlDataReader.GetString(1);
                    myCourses.description = mySqlDataReader.GetString(2);

                    courses.Add(myCourses);
                }
            }
            catch(Exception ex) {
            }
            finally { mySqlConnection?.Close(); }

            return Ok( courses);    
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCourese(int id) {

            SqlConnection? mySqlConnection = null;

            try
            {
                mySqlConnection = new SqlConnection(StringConnection);
                mySqlConnection.Open();
                SqlCommand mySqlCommand = mySqlConnection.CreateCommand();

                mySqlCommand.Parameters.Add("@id", SqlDbType.Int);
                mySqlCommand.Parameters["@id"].Value = id;

                string query = "UPDATE Courses SET Deleted=1 WHERE id=@id";

                mySqlCommand.CommandText = query;
                mySqlCommand.ExecuteNonQuery();

            }
            catch(Exception ex) { 
            }
            finally
            {
                mySqlConnection.Close();
            }
            return Ok();
        }

        [HttpGet("{id}/meetings")]

        public ActionResult
    }
}

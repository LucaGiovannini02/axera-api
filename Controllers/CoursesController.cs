using AxeraPJW.Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;


namespace AxeraPJW.Controllers
{
    [Route("api/course")]
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

        public ActionResult<CourseIJ>  Get(int id)
        {
            List<CourseIJ> coursesij = new List<CourseIJ>();    
            SqlConnection mySqlConnection = null;

            try
            {
                mySqlConnection = new SqlConnection(StringConnection);
                SqlCommand mySqlCommand = new SqlCommand();
                mySqlCommand.Parameters.Add("@id", SqlDbType.Int);
                mySqlCommand.Parameters["@id"].Value = id;
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.CommandText = "Select * From Courses Inner join Meeting ON Courses.id=Meeting.courses_id Where Courses.id=@id and Courses.deleted=0 and Meeting.deleted=0";
                mySqlConnection.Open();
                SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    CourseIJ myCourses = new CourseIJ();
                    myCourses.id = mySqlDataReader.GetInt32(0);
                    myCourses.name = mySqlDataReader.GetString(1);
                    myCourses.description = mySqlDataReader.GetString(2);
                    myCourses.idMeeting = mySqlDataReader.GetInt32(4);
                    myCourses.date= mySqlDataReader.GetDateTime(6);
                    myCourses.duration = mySqlDataReader.GetInt32(7);
                    myCourses.note= mySqlDataReader.IsDBNull(8) ? null : mySqlDataReader.GetString(8);
                    myCourses.max_users = mySqlDataReader.GetInt32(9);
                    myCourses.min_users = mySqlDataReader.GetInt32(10);

                    coursesij.Add(myCourses);
                }
            }
            catch (Exception ex) { }

            finally { mySqlConnection?.Close(); }

            return Ok(coursesij);
        }
    }
}

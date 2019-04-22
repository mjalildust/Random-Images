using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Random_Images.Models
{
    public class History
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string GetUserID(string username)

        {
            string uid = null;
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            string connectionString = ConfigurationManager.ConnectionStrings["RandomPic"].ConnectionString;

            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    using (command = new SqlCommand("SELECT userid FROM Users WHERE username='" + username + "'", connection))
                    {
                        connection.Open();
                        if (connection.State == ConnectionState.Open)
                        {
                            reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                uid = reader["userid"].ToString();
                            }

                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return uid;
        }

        internal void GetHistory(object username)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgname"></param>
        /// <returns></returns>
        public string GetImageID(string imgname)

        {
            string imgid = null;
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            string connectionString = ConfigurationManager.ConnectionStrings["RandomPic"].ConnectionString;

            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    using (command = new SqlCommand("SELECT imageId FROM Images WHERE imagename='" + imgname + "'", connection))
                    {
                        connection.Open();
                        if (connection.State == ConnectionState.Open)
                        {
                            reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                imgid = reader["imageid"].ToString();
                            }

                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return imgid;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public string GetUsername(int uid)

        {
            string uname = null;
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            string connectionString = ConfigurationManager.ConnectionStrings["RandomPic"].ConnectionString;
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    using (command = new SqlCommand("SELECT username FROM Users WHERE userId='" + uid + "'", connection))
                    {
                        connection.Open();
                        if (connection.State == ConnectionState.Open)
                        {
                            reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                uname = reader["username"].ToString();
                            }

                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return uname;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgid"></param>
        /// <returns></returns>
        public string GetImageName(string imgid)

        {
            string iname = null;
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            string connectionString = ConfigurationManager.ConnectionStrings["RandomPic"].ConnectionString;

            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    using (command = new SqlCommand("SELECT imageName FROM Images WHERE imageId='" + imgid + "'", connection))
                    {
                        connection.Open();
                        if (connection.State == ConnectionState.Open)
                        {
                            reader = command.ExecuteReader();
                            while (reader.Read())
                            {

                                iname = reader["imageName"].ToString();

                            }

                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return iname;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<UserPreference> GetHistory(string username)

        {
            string userid = GetUserID(username);

            List<UserPreference> userp = new List<UserPreference>();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            string connectionString = ConfigurationManager.ConnectionStrings["RandomPic"].ConnectionString;

            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    //using (command = new SqlCommand("SELECT imagename,liked FROM History,users,images WHERE userId='" + userid + "'", connection))
                    using (command = new SqlCommand("SELECT imagename, liked FROM History,users,images WHERE History.userId='" + userid + "' and History.userId=Users.userId and Images.imageId=History.imageId", connection))
                    {
                        connection.Open();
                        if (connection.State == ConnectionState.Open)
                        {


                            reader = command.ExecuteReader();
                            while (reader.Read())
                            {

                                UserPreference data = new UserPreference();
                                data.Setuserdata(Convert.ToString(reader["imagename"]), Convert.ToBoolean(reader["liked"]));
                                userp.Add(data);

                            }

                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return userp;
        }


        public string SetUserPreference(UserPreference data)
        {
            string userid = GetUserID(data.Username);
            string imageid = GetImageID(data.Imagename);
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            string connectionString = ConfigurationManager.ConnectionStrings["RandomPic"].ConnectionString;

            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    using (command = new SqlCommand("insert into History(userId, imageId,liked) values ('" + userid + "','" + imageid + "','" + data.Liked + "')", connection))
                    {
                        connection.Open();
                        if (connection.State == ConnectionState.Open)
                        {
                            reader = command.ExecuteReader();
                            if (reader.RecordsAffected > 0)
                            {
                                return "successfully stored";
                            }
                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;


            }

            return "not stored";

        }
    }
    public class UserPreference
    {

        public int ImageId { get; set; }
        public string ImagePath { get; set; }
        public string Imagename { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; }
        public bool Liked { get; set; } // true = like, false = dislike
                                        // public string UserName { get; set; }

        public UserPreference()
        {
        }
        public UserPreference(int imagei, int useri, bool like)
        {
            this.ImageId = imagei;
            this.UserId = useri;
            this.Liked = like;

        }

        public void Setuserdata(string Imagename, bool like)
        {
            this.Imagename = Imagename;
            this.Liked = like;

        }

    }
}
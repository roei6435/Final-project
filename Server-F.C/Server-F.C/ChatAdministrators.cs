﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_F.C
{
    internal class ChatAdministrators
    {
        public static string getAllDataAboutTweet()
        {
            string data = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = $"select * from tweets ORDER BY fullDate DESC;";
                cmd.Connection = Program.conn;
                Program.conn.Open();
                DataTable dt = new DataTable();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {

                    for (int i = 0; rdr.Read(); i++)
                    {
                        data += rdr[0] + Program.separationKey;   //tweetId
                        data += rdr[1] + Program.separationKey;    //userId
                        data += rdr[2] + Program.separationKey;    //  contentTweet
                        data += rdr[3] + Program.startObjectKey;  //date

                    }

                }

                Program.conn.Close();
                return data;

            }
            catch
            {
                Program.conn.Close();
                return "";
            }
        }
        public static string addNewTweet(string userId, string content)
        {

            try
            {
                DateTime date = DateTime.Now;
                Program.conn.Open();
                String sql = "INSERT INTO tweets(userId,contentTweet,fullDate)values(@userId,@content,@date)";
                SqlCommand cmd = Program.conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Program.conn.Close();
                return "true";

            }
            catch
            {
                Program.conn.Close();
                return "false";
            }

        }

        public static string likeToTweet(string userId,string tweetId)
        {

            try
            {
                Program.conn.Open();
                String sql = "INSERT INTO likesOfTweets(tweetId,userId)values(@tweetId,@userId)";
                SqlCommand cmd = Program.conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@tweetId", tweetId);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Program.conn.Close();
                return "true";

            }
            catch
            {
                Program.conn.Close();
                return "false";
            }
        }

        public static string unlikeToTweet(string userId, string tweetId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Program.conn;
                cmd.CommandText = $"delete from likesOfTweets where tweetId='{tweetId}' and userId='{userId}';";
                Program.conn.Open();
                int deleted = cmd.ExecuteNonQuery();
                Program.conn.Close();
                if (deleted > 0)
                    return "true";
                return "false";
            }
            catch 
            {
               // Console.WriteLine(ex);
                return "false";

            }
        }



    }
}

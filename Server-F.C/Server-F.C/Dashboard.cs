﻿using System;
using System.Data;
using System.Data.SqlClient;


namespace Server_F.C
{


    internal class Dashboard
    {
        //FUNCTION FOR CONVARTION STRING TO BOOLEAN

        public static string Getgender(string flag)
        {
            if (flag == "True") return "male";
            else return "female";
        }
        public static string boolenCheck(string flag)
        {
            if (flag == "True") return "True";
            else return "False";
        }


        //fetch from the database all relevant data from the users table, stored in a full key-separated string
        
        public static string getAllDataAboutPersonsInSystem()
        {
            string data = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from users";
                cmd.Connection = Program.conn;
                Program.conn.Open();
                DataTable dt = new DataTable();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                   // string pic;
                    for (int i = 0; rdr.Read(); i++)
                    {
                       string lastConn = "null";
                        if(rdr[14].GetType() != typeof(DBNull))
                            lastConn = rdr[14].ToString();
                        data += rdr[0] + Program.separationKey;   //id
                        data += rdr[1] + Program.separationKey;    //fname
                        data += rdr[2] + Program.separationKey;   //lname
                        data += rdr[3] + Program.separationKey;    //email
                        data += rdr[4] + Program.separationKey;   //phone
                        data += rdr[6] + Program.separationKey;    //dateB
                        data += rdr[12] + Program.separationKey;    //dateRegistion
                        data += lastConn + Program.separationKey;  //lastConn
                        data += rdr[7] + Program.separationKey;     //gender
                        data += rdr[8] + Program.separationKey;    //admin 
                        data += rdr[9] + Program.separationKey;     //isAuth
                        data += rdr[10] + Program.startObjectKey;  //isBlocked

                    }

                }

                Program.conn.Close();
                return data;

            }
            catch (Exception err)
            {
                Program.conn.Close();
                Console.WriteLine("from exption in function: " + err.Message);
                return null;
            }
        }

        //public static string getTheUserLocation()
        //{

        //}

        public static string getUserIdOfMostActiveAdminByTweest()
        {
            string data = "";int i = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                //GET ALL USER ID POSTED TWEETS FROM LAST WEEK
                cmd.CommandText = "select userId from tweets WHERE CAST(fullDate as date) between CAST(DATEADD(dd, -7, GETDATE()) as date) and CAST(GETDATE() AS DATE)";
                cmd.Connection = Program.conn;
                Program.conn.Open();
                DataTable dt = new DataTable();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {                  
                    for (i = 0; rdr.Read(); i++)
                    {
                        data += rdr[0] + Program.separationKey;   //userId
                    }
                }

                Program.conn.Close();
                if(i == 1)
                {
                    string[] arr = (data.Split(new string[] { Program.separationKey}, StringSplitOptions.RemoveEmptyEntries));
                    return data = arr[0]+"#"+i;
                }               //IF ONLY ONE TWEET ON THIS WEEK
                else if (data != string.Empty)
                {
                    string[] arr = (data.Split(new string[] { Program.separationKey }, StringSplitOptions.RemoveEmptyEntries));
                    int max = 0;
                    //GET MOST ACTIVE USERID 
                    for (i = 0; i < arr.Length; i++)
                    {
                        int counter = 0;
                        for (int j = 0; j < arr.Length - 1; j++)
                        {
                            if (arr[j] == arr[i])
                            {
                                counter++;
                            }
                        }
                        if (counter > max)
                        {
                            max = counter;
                            data = arr[i];
                        }
                    }
                    return data + "#" + max;
                }
                return data;           //IF NOT HAVE A TWEETS
            }
            catch
            {
                Program.conn.Close();
                return "";
            }
        }

        public static string getSumOfPaymentsOfLastMonth()
        {
            int fullSumPaymentsLastMonth=0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select sum from payments WHERE CAST(date as date) between CAST(DATEADD(dd, -30, GETDATE()) as date) and CAST(GETDATE() AS DATE)";
                cmd.Connection = Program.conn;
                Program.conn.Open();
                DataTable dt = new DataTable();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    for (int i = 0; rdr.Read(); i++)
                    {
                        fullSumPaymentsLastMonth += int.Parse(rdr[0].ToString());   //SUM OF PAYMENT
                    }

                }

                Program.conn.Close();
                return fullSumPaymentsLastMonth.ToString();

            }
            catch
            {
                Program.conn.Close();
                return "";
            }
        }


 

        public static string getAllDataClasses()
        {
            string data = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from classes";
                cmd.Connection = Program.conn;
                Program.conn.Open();
                DataTable dt = new DataTable();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    // string pic;
                    for (int i = 0; rdr.Read(); i++)
                    {
                        data += rdr[0] + Program.separationKey;   //ClassId
                        data += rdr[1] + Program.separationKey;    //nameClass
                        data += rdr[2] + Program.separationKey;   //place
                        data += rdr[4] + Program.separationKey;    //activity
                        data += rdr[5] + Program.startObjectKey;  //about

                    }

                }

                Program.conn.Close();
                return data;

            }
            catch (Exception err)
            {
                Program.conn.Close();
                Console.WriteLine("from exption in function: " + err.Message);
                return null;
            }
        }









    }

}

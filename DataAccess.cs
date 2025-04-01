using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Hindalco
{
    class DataAccess
    {
        public string cs = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;


        public DataSet getAllRecord(string pAction, string pActionName, string spName)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(spName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@" + pAction, pActionName);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(ds);
                    con.Close();
                }
            }
            return ds;
        }

        //        #region -- Select from DB --
        public DataTable Login(string userid, string Password)
        {

            try
            {
                DataTable dtrecord = new DataTable();
                using (SqlConnection con = new SqlConnection(cs))
                {

                    using (SqlCommand cmd = new SqlCommand("Sp_Login", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@userid", userid);
                        cmd.Parameters.AddWithValue("@Password", Password);



                        using (SqlDataAdapter Adap = new SqlDataAdapter(cmd))
                        {
                            Adap.Fill(dtrecord);


                            return dtrecord;
                        }

                    }
                }

            }
            catch (SqlException ex)
            {
                return null;
            }

        }


        public DataSet getRecordID(List<string> pName, List<string> pValue, string spName)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(spName, con))
                {
                    for (int i = 0; i < pName.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + pName[i], pValue[i]);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);

                    adap.Fill(ds);
                    con.Close();
                }
            }
            return ds;
        }

        // Insert Update Delete
        public int insertUpdateDelete(List<string> pName, List<string> pValue, string spName)
        {
            int iReturn = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(spName, con))
                {
                    for (int i = 0; i < pName.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + pName[i], pValue[i]);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    iReturn = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return iReturn;
        }

        public int CustomerDeposit(List<string> pName, List<string> pValue, string spName)
        {
            int iReturn = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(spName, con))
                {
                    for (int i = 0; i < pName.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + pName[i], pValue[i]);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    iReturn = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return iReturn;
        }



        //public int Check_UserDetials(string CashierID,string CashierName,string MobileNumber,string UserName,string EmailID,string Password,string Cinfermpassword)
        //{
        //    int iReturn = 0;
        //    string spName = "select UserID from UserTbl where UserID ='" + CashierID + "'";
        //    SqlConnection con = new SqlConnection(cs);
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand(spName, con);
        //    int pid = (int)cmd.ExecuteScalar();
        //    if(pid == int.Parse(CashierID.ToString()))
        //    {
        //        iReturn = 1;
        //    }
        //    con.Close();
        //    return iReturn;
        //}

        public int UserDetialsAdd(List<string> pName, List<string> pValue, string spName)
        {
            int iReturn = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(spName, con))
                {
                    for (int i = 0; i < pName.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + pName[i], pValue[i]);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    iReturn = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return iReturn;
        }

        /// <summary>
        ///  Insert Update Delete with return Id
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pValue"></param>
        /// <param name="returnParam"></param>
        /// <param name="spName"></param>
        /// <returns></returns>
        /// 
        public long insertUpdateDeletewithReturn(List<string> pName, List<string> pValue, string returnParam, string spName)
        {

            long rvalue = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(spName, con))
                {
                    for (int i = 0; i < pName.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + pName[i], pValue[i]);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(returnParam, SqlDbType.BigInt);
                    cmd.Parameters[returnParam].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    rvalue = Convert.ToInt64(cmd.Parameters[returnParam].Value.ToString());
                    con.Close();
                }
            }
            return rvalue;

        }

        /// <summary>
        ///  get string value from database 
        /// </summary>
        /// <param name="pName"> </param>
        /// <param name="pValue"> </param>
        /// <param name="spName"><param>
        /// <returns></returns>
        /// 
        public string getStringValue(List<string> pName, List<string> pValue, string spName)
        {

            string rvalue = "";
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(spName, con))
                {
                    for (int i = 0; i < pName.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + pName[i], pValue[i]);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    rvalue = Convert.ToString(cmd.ExecuteScalar());
                    con.Close();
                    return rvalue;
                }
            }

        }

        public DataSet getAllRecord(List<string> pName, List<string> pValue, string spName)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(spName, con))
                {
                    for (int i = 0; i < pName.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + pName[i], pValue[i]);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(ds);
                    con.Close();
                }
            }
            return ds;
        }

        public DataTable getAllRecord1(List<string> pName, List<string> pValue, string spName)
        {
            DataTable ds = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(spName, con))
                {
                    for (int i = 0; i < pName.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + pName[i], pValue[i]);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(ds);
                    con.Close();
                }
            }
            return ds;
        }

        public DataTable getAllRecordUsingDT(List<string> pName, List<string> pValue, string spName)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(spName, con))
                {
                    for (int i = 0; i < pName.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + pName[i], pValue[i]);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }
    }
}

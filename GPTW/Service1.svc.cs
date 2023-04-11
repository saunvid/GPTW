using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Configuration;
using GPTW.Helper;
using Newtonsoft.Json;

namespace GPTW
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public ResponseModel<Question> AddQuestion(Question q)
        {
            
            ResponseModel<Question> response = new ResponseModel<Question>();

            string str = ConfigurationManager.ConnectionStrings["ConnectionString1"].ToString();
            try
            {

                using (SqlConnection con = new SqlConnection(str))
                {

                    using (SqlCommand cmd = new SqlCommand("USP_ADD_QUESTION"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@TYPE", "ADD_QUESTION");
                        cmd.Parameters.AddWithValue("@QuestionID", q.QuestionID);
                        cmd.Parameters.AddWithValue("@QuestionText", q.QuestionText);
                        cmd.Parameters.AddWithValue("@Score", q.Score);

                        con.Open();
                        DataSet ds = new DataSet();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(ds);
                        }
                        con.Close();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                           

                            response.Success = true;
                            response.Message = "Data saved successfully.";
                            response.data = q;
                            

                        }
                        else
                        {
                            response.Success = true;
                            response.Message = "No Data saved.";
                            response.data = q;
                            

                        }
                        
                        return response;
                    }
                }
            }


            catch (Exception e)
            {

                response.Success = false;
                response.Message = e.Message;
                //response.data = q;


            }
            return response;



        }

        public ResponseModel<String> GetScore()
        {
            ResponseModel<String> response = new ResponseModel<String>();

            string str = ConfigurationManager.ConnectionStrings["ConnectionString1"].ToString();
            try
            {

                using (SqlConnection con = new SqlConnection(str))
                {

                    using (SqlCommand cmd = new SqlCommand("USP_ADD_QUESTION"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@TYPE", "Get_All_Score");

                        con.Open();
                        DataSet ds = new DataSet();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(ds);
                        }
                        con.Close();
                        String s = "";

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //List<Student> studentList = new List<Student>();
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                
                                //s.Sr_No = ds.Tables[0].Rows[i]["SrNo"].ToString();
                                s = ds.Tables[0].Rows[i]["positive_score"].ToString();

                            }

                            response.Success = true;
                            response.Message = "Data fetched successfully.";
                            response.positive_score = s;
                           


                        }
                        else
                        {
                            response.Success = true;
                            response.Message = "No Data Fetched.";


                        }
                        
                        return response;


                    }

                }
            }catch (Exception e)
            {

                response.Success = false;
                response.Message = e.Message;
                return response;



            }





        }

        public ResponseModel<List<Question>> GetParticularScore(Questions QuestionID)
        {

            ResponseModel<List<Question>> response = new ResponseModel<List<Question>>();
            
            string str = ConfigurationManager.ConnectionStrings["ConnectionString1"].ToString();
            try
            {

                using (SqlConnection con = new SqlConnection(str))
                {

                    using (SqlCommand cmd = new SqlCommand("USP_ADD_QUESTION"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@TYPE", "Get_Particular_Score");
                        //for(int i=0;i<=QuestionID.QuestionID.Count;)
                        String a = (string.Join(",", QuestionID.QuestionID)).TrimStart();
                        cmd.Parameters.AddWithValue("@QuestionID", a);

                        con.Open();
                        DataSet ds = new DataSet();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(ds);
                        }
                        con.Close();
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            List<Question> data = new List<Question>();
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                Question q = new Question();
                                q.QuestionID = (int)ds.Tables[0].Rows[i]["QuestionID"];
                                q.Score = (double)ds.Tables[0].Rows[i]["Score"];
                                data.Add(q);
                            }

                            response.Success = true;
                            response.Message = "Data fetched successfully.";
                            response.data = data;
                            return response;
                        }
                        else
                        {
                            response.Success = true;
                            response.Message = "No Data Fetched.";
                            return response;
                        }


                    }
                }
            }catch (Exception e)
            {

                response.Success = false;
                response.Message = e.Message;
                return response;

            }
        }
    }
}

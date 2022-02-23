using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MusicTracks.Models;

namespace MusicTracks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class InsertController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly IConfiguration _configuration;
        public InsertController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Insert_user")]
        public JsonResult Insert_user(InsClass cls)
        {
            //string InsertedBy = AuthClass.getUser(HttpContext);
            //if (InsertedBy != "")
            //{
                try
                {
                    SqlConnection con = new SqlConnection(AuthClass.Getconstring(_configuration));
                    try
                    {
                        con.Open();
                        SqlTransaction trn = con.BeginTransaction();

                        SqlCommand cmd = new SqlCommand("sp_insert_user", con, trn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        string pass = Kripta.Encrypt(cls.userPassword.Trim(), "Sud@#$%-=.Pas").ToString().Trim();

                        cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value = cls.userName.Trim();
                        cmd.Parameters.Add("@userPassword", SqlDbType.NVarChar).Value = pass;
                        cmd.Parameters.Add("@userDisplayName", SqlDbType.NVarChar).Value = cls.userDisplayName.Trim();
                        cmd.Parameters.Add("@profileImage", SqlDbType.NVarChar).Value = cls.profileImage.Trim();
                        cmd.Parameters.Add("@userType", SqlDbType.Int).Value = cls.userType;
                        cmd.Parameters.Add("@insertedBy", SqlDbType.VarChar).Value = cls.insertedBy.Trim();

                    try
                        {
                            int cunt = cmd.ExecuteNonQuery();
                            if (cunt > 0)
                            {
                                trn.Commit();
                                return new JsonResult("User has been added successfully");
                            }
                            else
                            {
                                trn.Rollback();
                                return new JsonResult("User insertion process unsuccessful");
                            }
                        }
                        catch //(Exception ex) activating for dev needs only
                        {
                            //trn.Rollback();
                            //return new JsonResult("User insertion process unsuccessful, REF: " + ex.Message.ToString());
                            trn.Rollback();
                            return new JsonResult("User insertion process unsuccessful");
                        }
                    }
                    catch //(Exception ex) activating for dev needs only
                    {
                        //return new JsonResult("User insertion process unsuccessful, REF: " + ex.Message.ToString());
                        return new JsonResult("User insertion process unsuccessful");
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                catch //(Exception ex) activating for dev needs only
                {
                    //return new JsonResult("Connection decrypting process unsuccessful, REF: " + ex.Message.ToString());
                    return new JsonResult("Connection decrypting process unsuccessful");
                }
            //}
            //else
            //{
            //    return new JsonResult("The authorization header is not valid or not well-formatted");
            //}
        }

    }
}

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
    public class SelectController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly IConfiguration _configuration;
        public SelectController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("get_user")]
        public JsonResult get_user(InsClass cls)
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
                        SqlCommand cmd = new SqlCommand("sp_get_user", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@userId", SqlDbType.NVarChar).Value = cls.UserID.Trim();

                        SqlDataReader dr = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        dr.Close();

                        if (dt.Rows.Count > 0)
                        {
                            return new JsonResult(dt);
                        }
                        else
                        {
                            return new JsonResult("User type retrieving process unsuccessful");
                        }
                    }
                    catch (Exception ex)
                    {
                        return new JsonResult("User type retrieving process unsuccessful, REF: " + ex.Message.ToString());
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
            //    }
            //    else
            //    {
            //        return new JsonResult("The authorization header is not valid or not well-formatted");
            //    }
        }
    }
}

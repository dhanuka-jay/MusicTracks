using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MusicTracks.Models;
using System.Drawing;

namespace MusicTracks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivestateController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly IConfiguration _configuration;
        public ActivestateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("alteractive_user")]
        public JsonResult alteractive_user(InsClass cls)
        {
            //string InsertedBy = AuthClass.getUser(HttpContext);
            //if (InsertedBy != "")
            //{
            SqlConnection con = new SqlConnection(AuthClass.Getconstring(_configuration));
            try
            {
                con.Open();
                SqlTransaction trn = con.BeginTransaction();

                SqlCommand cmd = new SqlCommand("sp_changeactive_user", con, trn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@userId", SqlDbType.NVarChar).Value = cls.UserID.Trim();

                try
                {
                    int cunt = cmd.ExecuteNonQuery();
                    if (cunt > 0)
                    {
                        trn.Commit();
                        return new JsonResult("User active state alteration successfully");
                    }
                    else
                    {
                        trn.Rollback();
                        return new JsonResult("User active state alteration unsuccessfully");
                    }
                }
                catch
                {
                    return new JsonResult("User active state alteration unsuccessfully");
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

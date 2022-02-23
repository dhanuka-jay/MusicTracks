using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MusicTracks.Controllers
{
    public class AuthClass
    {
        public static string getUser(HttpContext httpContext)
        {
            try
            {
                var headers = httpContext.Request.Headers;
                string AuthHeader = headers["Authorization"].ToString();

                if (AuthHeader != null && AuthHeader.StartsWith("Basic"))
                {
                    string encodedStr = AuthHeader.Substring("Basic ".Length).Trim();
                    Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                    string upStr = encoding.GetString(Convert.FromBase64String(encodedStr));

                    int seperatorIndex = upStr.IndexOf(':');

                    string uid = upStr.Substring(0, seperatorIndex);
                    //string pass = upStr.Substring(seperatorIndex + 1);

                    //string KeyStr = Kripta.Encrypt(uid, pass).ToString().Trim();

                    return uid;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        public static string Getconstring(IConfiguration _configuration)
        {
            try
            {
                string dbp = _configuration.GetValue<string>("Kripton_Key");
                string ConStuff = Kripta.Decrypt(dbp, "Sud@#$%-=.Pas").ToString().Trim();

                string conString = _configuration.GetValue<string>("ConStr");
                string con_ = Kripta.Decrypt(conString, "Sud@#$%-=.Con").ToString().Trim().Replace("pass", ConStuff);

                //string c = Kripta.Encrypt("Data Source=127.0.0.1;User ID=sa;Password=pass;Initial Catalog=MusicTrack_BE; Connection Timeout=320;pooling=true;Max Pool Size=400", "Sud@#$%-=.Con").ToString();
                //string v = Kripta.Encrypt("$ids1111$$AA", "Sud@#$%-=.Pas").ToString();
                //return c;

                return con_;
            }
            catch //(Exception ex)
            {

                return "";
            }
        }
    }
}

using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorLight;

namespace ShopManager.Helper
{
    public class MailHelper
    {
        private IWebHostEnvironment Environment { get; set; }
        public IConfiguration Configuration { get; set; }

        public MailHelper(IConfiguration _configuration, IWebHostEnvironment environment)
        {
            Configuration = _configuration;
            Environment = environment;
        }

        public string PopulateBody(string OTP)
        {
            //Lấy Template HTML
            string body = string.Empty;
            string path = Path.Combine(this.Environment.WebRootPath, "Template\\EmailTemplate.htm");
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{OTP}", OTP);
            return body;
        }

        public void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            string host = this.Configuration.GetValue<string>("Smtp:Server");
            int port = this.Configuration.GetValue<int>("Smtp:Port");
            string fromAddress = this.Configuration.GetValue<string>("Smtp:FromAddress");
            string userName = this.Configuration.GetValue<string>("Smtp:UserName");
            string password = this.Configuration.GetValue<string>("Smtp:Password");

            using (MailMessage mm = new MailMessage(fromAddress, recepientEmail))
            {
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = host;
                    smtp.EnableSsl = false;
                    NetworkCredential networkCred = new NetworkCredential(userName, password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCred;
                    smtp.Port = port;
                    smtp.Send(mm);
                }
            }
        }
    }
}

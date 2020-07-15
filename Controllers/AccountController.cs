using C.R.E.A.M.Context;
using C.R.E.A.M.Models;
using C.R.E.A.M.ViewModel;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace  Controllers
{
    
    public class AccountController : Controller
    {
        private StoreContext _storeContext = new StoreContext();
        
        private const string SECRET_KEY = "r0SQAuUsUWdRb8IGID29";

        // GET: Account
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(RegisterUser newUser)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var result = _storeContext.Users.Include("Roles").Where(x => x.Username == newUser.Username).FirstOrDefault();
            if(result != null)
            {     
                ModelState.AddModelError("Username", "ასეთი მომხმარებელი უკვე არსებობს");
                return View();
            }

            User user = new User()
            {
                Email = newUser.Email,
                Password = GenerateMD5Hash(newUser.Password + SECRET_KEY),
                Username = newUser.Username,
                ConfirmationCode = Guid.NewGuid()
            };

            Uri uri = new Uri(Request.Url.AbsoluteUri);

            var urlHost = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;

            var text = $"გთხოვთ ეწვიოთ <a href='{urlHost}/Account/Confirmation/{user.ConfirmationCode}'>ბმულს</a> და დაასრულოთ რეგისტრაცია";
            SendMail(user.Email, text);
            _storeContext.Users.Add(user);
            _storeContext.SaveChanges();

            return RedirectToAction("MailsentConfirm");
        }

        public ActionResult MailsentConfirm()
        {
            //////??????????????????????????????
            return View();
        }

        private void SendMail(string to, string text)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new System.Net.Mail.MailAddress("tsotne.sturua19@gmail.com");
            mail.To.Add(to);
            mail.Subject = "Registration Confirmation";
            mail.Body = text;
            mail.IsBodyHtml = true;

            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("in-v3.mailjet.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("f005ed6e1cf3f1b09d744158a50f7764", "c72e38e34f579d424b8f6a0fed71236a");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
         }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(User user)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "მომხმარებელი ან პაროლი არასწორია");
                return View();
            }

            var hashPassword = GenerateMD5Hash(user.Password + SECRET_KEY);
            var result = _storeContext.Users.Where(x => x.Username == user.Username && x.Password == hashPassword).FirstOrDefault();
           

            if (result == null)
            {
                ModelState.AddModelError("", "მომხმარებელი ან პაროლი არასწორია");
                return View();
            }

            result.Password = null;
            Session["Username"] = result;

            return RedirectToAction("index", "store");
        }

        public ActionResult Confirmation(string id)
        {
            var user = _storeContext.Users.Where(x => x.ConfirmationCode.ToString().ToLower() == id.ToLower()).FirstOrDefault();

            user.IsActive = true;
            _storeContext.SaveChanges();

            return View();
        }

        public ActionResult ChangePassword()
        {

            var userSession = (C.R.E.A.M.Models.User)Session["Username"];
            if (userSession == null)
            {
                return RedirectToAction("Login", "account");
            }

            return View();
        }

         [HttpPost]
        public ActionResult ChangePassword(RegisterUser ChangedPassowrd )
        {
            var userSession = (C.R.E.A.M.Models.User)Session["Username"];

            var result = _storeContext.Users.Where(x => x.Username == userSession.Username).FirstOrDefault();

            
            result.Password = GenerateMD5Hash(ChangedPassowrd.Password + SECRET_KEY);
            _storeContext.SaveChanges();

            Session.Clear();
            Session.Abandon();

            
            return RedirectToAction("Login", "account");


        }

        private string GenerateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();

            }
        }

        public ActionResult Logout()
        {
            return View();
        }

    }
}
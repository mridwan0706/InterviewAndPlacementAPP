using API.Models;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class InterviewRepository : GeneralRepository<Interview>
    {
        private MySQLDatabase database;
        public InterviewRepository(MySQLDatabase mySQL) : base(mySQL) {
            database = mySQL;
        }

        [HttpPost]
        public MailMessage SendInvitation(Interview interview, string email)
        {
            var from = "";
            var to = email;
            MailMessage mail = new MailMessage(from, to);           
            mail.Subject = "";
            mail.Body = "";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            var emailSender = from;
            var passwordSender = "";
            NetworkCredential network = new NetworkCredential(emailSender, passwordSender);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = network;
            smtp.Send(mail);
            return mail; 
        }

        [HttpGet]
        public Employee GetEmployee(int id)
        {
            var employee = database.Connection.Get<Employee>(id);
            return employee;
        }
    }
}

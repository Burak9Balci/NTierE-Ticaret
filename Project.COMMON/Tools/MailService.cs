﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Project.COMMON.Tools
{
    public static class MailService
    {
        public static void Send(string receiver,string password = "atlkbjteiruyhcww",string body = "Test mesajı",string subject ="Email Testi",string sender = "yzl3157test@gmail.com")
        {
            MailAddress senderEmail = new MailAddress(sender);
            MailAddress receiverEmail = new MailAddress(receiver);

            SmtpClient smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587, // Kurumsal olursa 465 olucak
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address,password)
            };

            using (MailMessage message = new MailMessage()
            {
              Subject = subject,
              Body = body,
            
            })
            {
                smtp.Send(message); 
            }
            // 37. satırdan sonra ekstranda kod eklenirse 38. kod ve sonrasına kalmadan using kısmını collecter toplar
        }
    }
}
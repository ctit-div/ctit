using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;
/// <summary>
/// Summary description for ClassSendEmail
/// </summary>
public class ClassSendEmail
{
    public ClassSendEmail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public  Boolean SendingMail(string To, string Body)
    {

        try
        {
            To = "ararawi2011@gmail.com";
            Body = "Test";
             MailMessage m = new MailMessage();
            m.Subject = "WEC ONLINE SALES";
            m.Body = Body;
            m.IsBodyHtml = true;
            m.From = new MailAddress("ararawi2011@gmail.com");
            m.To.Add(new MailAddress(To));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            NetworkCredential authinfo = new NetworkCredential("ararawi2011@gmail.com", "65198611aA_");
            //smtp.Host = "relay-hosting.secureserver.net";
            ////smtp.Port = 25;
       
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = authinfo;

            smtp.Send(m);
            return true;

          



        }
        catch (Exception ex)
        {
            return false;
        }
    }

}
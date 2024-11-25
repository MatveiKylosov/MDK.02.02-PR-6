using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;

namespace RegIN_Kylosov.Classes
{
    public class SendMail
    {
        public static void SendMessage(string Message, string To)
        {
            var smtpClient = new SmtpClient("smtp.yandex.ru")
            {
                Port = 587,
                Credentials = new NetworkCredential("TestKylosov@yandex.ru", "ekdzbcygzmqoomcx"),
                EnableSsl = true,
            };

            smtpClient.Send("TestKylosov@yandex.ru", To, "Проект RegIn", Message);
        }
    }
}

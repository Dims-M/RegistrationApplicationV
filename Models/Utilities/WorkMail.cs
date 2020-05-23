
using ActionMailerNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace RegistrationApplication.Models.Utilities
{
    /// <summary>
    /// Класс для работы и отправки электронной почти
    /// </summary>
    public  class WorkMail
    {
        public  async Task SendEmailAsync(string pathAttachments, string Toemail)
        {
            //от кого
            MailAddress from = new MailAddress("o.avto@i-cks.ru", "Информационное оповещение");
            //кому
            //MailAddress to = new MailAddress("o.avto@i-cks.ru");
            MailAddress to = new MailAddress(Toemail);

            MailMessage m = new MailMessage(from, to);

            //Вложение. Например документ
            m.Attachments.Add(new Attachment(pathAttachments));

            // Имя сообщения
            m.Subject = "Заявка на кредит!";
            //Тело сообщения. Сам текс письма
            m.Body = "Касса №1 — сеть центров выдачи займов!" +
                      "Сформирована ваша заявка на кредит." +
                      "Подробности в документе.";

            //настройки электронного ящика
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 2525);
            smtp.Credentials = new NetworkCredential("o.avto@i-cks.ru", "51215045avto");
            
            // Использовать ли защиту  Ssl
            smtp.EnableSsl = true;

            //отправка
            await smtp.SendMailAsync(m);
            // Console.WriteLine("Письмо отправлено");

            #region На mail.ru следующие настройки:
            //            Сервер исходящей почты(SMTP - сервер): SMTP.< домен >, где < домен > -домен Вашего почтового ящика(для почтового ящика mailname@mail.ru - smtp.mail.ru, listname@list.ru - smtp.list.ru, bkname@bk.ru - smtp.bk.ru, inboxname@inbox.ru - smtp.inbox.ru).
            //                порт - 2525
            //ssl - включено.
            #endregion
        }

    }

    
}
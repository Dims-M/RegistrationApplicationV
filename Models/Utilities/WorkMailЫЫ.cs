
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

        /// <summary>
        /// Отправка почты клиенту
        /// </summary>
        /// <param name="pathAttachments">Вложение(документов) к письму</param>
        /// <param name="Toemail">Кому отправляем</param>
        /// <returns></returns>
        //public async Task SendEmailAsync(string pathAttachments, string Toemail)
        //{
        //    try
        //    {

        //    //от кого
        //    MailAddress from = new MailAddress("MYmAIL", "Информационное оповещение");

        //    //кому
        //    MailAddress to = new MailAddress(Toemail);

        //    MailMessage m = new MailMessage(from, to);

        //    //Вложение. Например документ
        //    m.Attachments.Add(new Attachment(pathAttachments));

        //    // Имя сообщения
        //    m.Subject = "Заявка на кредит!";
        //    //Тело сообщения. Сам текс письма
        //    m.Body = "Касса №1 — сеть центров выдачи займов!" +
        //              "Сформирована ваша заявка на кредит." +
        //              "Подробности в документе.";

        //    //настройки электронного ящика
        //    SmtpClient smtp = new SmtpClient("smtp.mail.ru", 2525);
        //    //Логин и Пароль от почты
        //    smtp.Credentials = new NetworkCredential("lOGIN Email(сама почта)", "Pass");

        //    // Использовать ли защиту  Ssl
        //   smtp.EnableSsl = true;

        //        //отправка
        //        await smtp.SendMailAsync(m);
        //        // Console.WriteLine("Письмо отправлено");

        //    }
        //    catch (Exception ex)
        //    {
        //        var t = ex.ToString();
        //    }


        //}

        public void SendEmailAsync(string pathAttachments, string Toemail)
        {
            try
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
                //Логин и Пароль от почты
                smtp.Credentials = new NetworkCredential("o.avto@i-cks.ru", "51215045avto");

                // Использовать ли защиту  Ssl
                smtp.EnableSsl = true;

                //отправка
               // smtp.SendMailAsync(m);
                smtp.Send(m);
                

            }
            catch (Exception ex)
            {
                var t = ex.ToString();
            }


        }
        #region На mail.ru следующие настройки:
        //            Сервер исходящей почты(SMTP - сервер): SMTP.< домен >, где < домен > -домен Вашего почтового ящика(для почтового ящика mailname@mail.ru - smtp.mail.ru, listname@list.ru - smtp.list.ru, bkname@bk.ru - smtp.bk.ru, inboxname@inbox.ru - smtp.inbox.ru).
        //                порт - 2525
        //ssl - включено.
        #endregion
    }


}
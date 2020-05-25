﻿using GemBox.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegistrationApplication.Models.Utilities
{
    /// <summary>
    /// Создание документов Ворд 
    /// </summary>
    public class WorkingWord
    {
        /// <summary>
        /// Создание Ворд документа
        /// </summary>
        /// <param name="paht"></param>
        public void GetBoxCreateWord(string[] client, string paht , string nameClient = "Client")
        {
            string hedstr = @"
            Касса №1 — сеть центров выдачи займов!
            kassaone.ru
            Сеть центров по выдаче денежных займов размещает условия предоставления денежных средств частным лицам."+ Environment.NewLine;

           // string[] temZnach = new string[] { hedstr, "Фамилия клиента:", "Имя отчество:", "Год рождения", "Сумма кредита:","Дата заявки:","ТУТ должна быть картинка!!" };
          //  string temp = hedstr + client;

            // создание ссылок дирикторий(папок для картинки). Корневая папка
            var originalDirectory = new DirectoryInfo(string.Format(@"~Archive_Documents\\Uploads"));

            //Бесплатное Лицензия
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            // Создание нового дока.
            var document = new DocumentModel();

           // string[] array = client.Select(n => n.ToString()).ToArray();
            try
            {
                
                //Цикл для массивов
                for (int i = 0; i < client.Length; i++)
                {

                hedstr += (client[i] + Environment.NewLine);
                document.Content.LoadText(hedstr);

                };
               // document.Content.LoadText(hedstr);

                document.Save(paht +$@"\\Result_Client{nameClient}.docx");
                //document.Save("Archive_Documents/TestSaveDoc.pdf");

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Ошибка при работе" + ex);
            }
        }

    }
}
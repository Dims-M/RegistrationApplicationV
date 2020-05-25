using GemBox.Document;
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
       /// Создание документа форд
       /// </summary>
       /// <param name="client">Массив с данными. для записи в Word</param>
       /// <param name="paht">paht Путь к файлу. Куда сохранять</param>
       /// <param name="nameClient">Имя клиента </param>
        public void GetBoxCreateWord(string[] client, string paht , string nameClient = "Client")
        {
            string hedstr = @"
Касса №1 — сеть центров выдачи займов!
kassaone.ru
Сеть центров по выдаче денежных займов размещает условия предоставления денежных средств частным лицам."+ Environment.NewLine;

            // создание ссылок дирикторий(папок для картинки). Корневая папка
            var originalDirectory = new DirectoryInfo(string.Format(@"~Archive_Documents\\Uploads"));

            //Бесплатное Лицензия
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            // Создание нового дока.
            var document = new DocumentModel();

            try
            {
                //Цикл для массивов
                for (int i = 0; i < client.Length; i++)
                {
                    hedstr += (client[i] + Environment.NewLine);
                };

                document.Content.LoadText(hedstr);

                document.Save(paht + $@"\\Result_Client_{nameClient}.docx");

                //поиск и замена Картинки.
                ContentRange imagePlaceholder = document.Content.Find("#IMAGE").First();
              
                // Create and add new Picture element content.
              //  imagePlaceholder.Set(new Picture(document, paht + $"\\5.jpg").Content);
                imagePlaceholder.Set(new Picture(document, paht + $"\\{nameClient}.jpg").Content);

             document.Save(paht + $@"\\Result_Client_{nameClient}.docx");

            }

            catch (Exception ex)
            {
                //для лога ошибок
            }
        }

    }
}
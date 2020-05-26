using GemBox.Document;
using RegistrationApplication.Models.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Picture = GemBox.Document.Picture;
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
       /// <param name="nameClient">Id имя клиента </param>
        public void GetBoxCreateWord(string[] client, string paht , int nameClient)
        {
            string hedstr = @"
Касса №1 — сеть центров выдачи займов!
kassaone.ru
Сеть центров по выдаче денежных займов размещает условия предоставления денежных средств частным лицам."+ Environment.NewLine;

            // создание ссылок дирикторий(папок для картинки). Корневая папка
           // var originalDirectory = new DirectoryInfo(string.Format(@"~Archive_Documents\\Uploads"));

            string temp ;
            string ext;
            string tempPahtImag;

            //Бесплатное Лицензия
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            // Создание нового дока.
            var document = new DocumentModel();

            try
            {

                //Цикл для записи текста вфайл ворд
                for (int i = 0; i < client.Length; i++)
                {
                    hedstr += (client[i] + Environment.NewLine);
                };

                document.Content.LoadText(hedstr);

                document.Save(paht + $@"\\Result_Client_{nameClient}.docx");

                //поиск и замена Картинки.
                ContentRange imagePlaceholder = document.Content.Find("#IMAGE").First();

                //дергаем из Бд имя и путь картнки
                using (DBContext db = new DBContext())
                {
                    temp = db.clients.Find(nameClient).imagePathInDoc;
                }

                //получаем разширение
                ext = System.IO.Path.GetExtension(temp);
                //Получаем имя файла
                tempPahtImag = System.IO.Path.GetFileName(temp);

                //проверка разширения
                if (ext != "image/jpg" &&
                    ext != "image/jpeg" &&
                    ext != "image/pjpeg" &&
                    ext != "image/gif" &&
                    ext != "image/png" &&
                    ext != "image/x-png")
                { 
                     //imagePlaceholder.Set(new Picture(document, paht + $"\\{nameClient.ToString()}{ext}").Content);
                     //document.Save(paht + $@"\\Result_Client_{nameClient.ToString()}.docx");
                }

                string pathCopy = paht + "\\" + tempPahtImag;
               // File.Copy($"{temp}", $"{paht}\\{nameClient}\\{tempPahtImag}", true);
                File.Copy(temp, pathCopy, true);

                //imagePlaceholder.Set(new Picture(document, paht + $"\\{nameClient.ToString()}{ext}").Content);
                imagePlaceholder.Set(new Picture(document, paht + $"\\{tempPahtImag}").Content);
               
                //imagePlaceholder.Set(new Picture(document, paht + $"\\{temp}").Content);
               
                
                document.Save(paht + $@"\\Result_Client_{nameClient.ToString()}.docx");
           
            }

            catch (Exception ex)
            {
                JournalLog(ex.ToString()); 
                //для лога ошибок
            }
        }

        public void JournalLog(string log)
        {
            // запись в файл
            using (FileStream fstream = new FileStream(@"C:\\1\\log.txt", FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(log);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
               // Console.WriteLine("Текст записан в файл");
            }
        }

    }
}
using GemBox.Document;
using RegistrationApplication.Models.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Picture = GemBox.Document.Picture;


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
        public void GetBoxCreateWordAndPdf(string[] client, string paht , int IdClient)
        {
            string hedstr = @"
Касса №1 — сеть центров выдачи займов!
kassaone.ru
Сеть центров по выдаче денежных займов размещает условия предоставления денежных средств частным лицам."+ Environment.NewLine;

            // создание ссылок дирикторий(папок для картинки). Корневая папка
            // var originalDirectory = new DirectoryInfo(string.Format(@"~Archive_Documents\\Uploads"));

            //Тест переменная
             

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

                document.Save(paht + $@"\\Result_Client_{IdClient}.docx");
                document.Save(paht + $@"\\Result_Client_{IdClient}.pdf");

                //поиск и замена Картинки.
                ContentRange imagePlaceholder = document.Content.Find("#IMAGE").First();

                //дергаем из Бд имя и путь картнки
                using (DBContext db = new DBContext())
                {
                    temp = db.clients.Find(IdClient).imagePathInDoc;
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
                }

                string pathCopy = paht + "\\" + tempPahtImag;
                File.Copy(temp, pathCopy, true);

                imagePlaceholder.Set(new Picture(document, paht + $"\\{tempPahtImag}").Content);

                //создаем документ в 2х форматах. *.docx и *.pdf
                document.Save(paht + $@"\\Result_Client_{IdClient.ToString()}.docx");
                document.Save(paht + $@"\\Result_Client_{IdClient.ToString()}.pdf");
 
            }

            catch (Exception ex)
            {
                JournalLog(ex.ToString()); 
                //для лога ошибок
            }
        }



        /// <summary>
        /// Конвертириуем пдв в строку Base64String и сохраняем в файл
        /// </summary>
        /// <param name="pathDoc">Путь к документу который нужно преобразовать в  base64</param>
        /// <returns></returns>
        public string ConverdToBase64String(string pathDoc)
        {
            string textFromFile;
            string b64 = null;

            //проверяем на null и "" 
            if (!string.IsNullOrWhiteSpace(pathDoc))
            {

            using (FileStream fstream = File.OpenRead(pathDoc))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                textFromFile = System.Text.Encoding.Default.GetString(array);
                // Console.WriteLine($"Текст из файла: {textFromFile}");
            }

            string sorstext = textFromFile;
            b64 = Convert.ToBase64String(Encoding.Default.GetBytes(sorstext));
          
                return (b64);
            }
            else
            {
                return (b64);
            }

           //return (b64);
        }


        /// <summary>
        /// МЕТОД псевдолога
        /// </summary>
        /// <param name="log"></param>
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
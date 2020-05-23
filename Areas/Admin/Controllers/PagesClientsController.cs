﻿using PagedList;
using RegistrationApplication.Models.Data;
using RegistrationApplication.Models.Utilities;
using RegistrationApplication.Models.ViewModels.Clients;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RegistrationApplication.Areas.Admin.Controllers
{
    public class PagesClientsController : Controller
    {
       private WorkingWord workingWord;
       private WorkMail workMail;
      

        // GET: Admin/PagesClients
        /// <summary>
        /// Получаем список всех заявок
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            workingWord = new WorkingWord();

            //Получить  из БД список сущноситей ClientVM для ввывода их в представлении
            List<ClientVM> clientList;

            //Инициализируем список и заполняем данными из Бд
            using (DBContext db = new DBContext())
            {               // Выборку приводим в арей(массив), сортируем, и создаем на экземпляр обьекта ClientVM, приводим к листу и сохраняем в лист
                clientList = db.clients.ToArray().OrderBy(x => x.Id).Select(x => new ClientVM(x)).ToList();

            }
           // workingWord.GetBoxCreateWord();
            return View(clientList);
        }

        // GET: Admin/PagesClients/AddNewClient
        /// <summary>
        /// Добавление новой заявки. 
        /// Метод пипа Get. Отправляем форму для клиента
        /// </summary>
        [HttpGet]
        public ActionResult AddNewClient()
        {
            return View();
        }

       
        // POST: Admin/PagesClients/AddClient
        /// <summary>
        /// Метод создания и загрузки данный клиента(с фото)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddNewClient(ClientVM model, HttpPostedFileBase file) 
        {

            //if (file.GetType() == typeof(HttpPostedFileWrapper))
            //{
            //    return View(model); // Возращаем модель на форму
            //}

            //проверяем модель на валидность.


            if (!ModelState.IsValid)
            {
                     return View (model); // Возращаем модель на форму
             }


            //проверка имени на уникальность
            using (DBContext db = new DBContext())
            {
                //РПроверяем. Есть ли такое имя в БД
                if(db.clients.Any(x=> x.firstName == model.FirstName))
                {
                    //Данное умя уже содержится в БД
                   // ModelState.AddModelError("", "Данное имя не уникально!! ВВедите другое"); 
                   // return View(file); // Возращаем модель на форму
                }
            }

            //переменная для client ID
            int id;

            //иницализируем и сохраняем модель на основе клиента ДТО
            using(DBContext  db = new DBContext())
            {

                Client clientDto = new Client();

                //присваиваем имя клиента в большом регистре модели. 
                clientDto.firstName = model.FirstName.ToUpper();
                clientDto.lastName = model.LastName.ToUpper();
                clientDto.middleName = model.MiddleName.ToUpper();

                //День рождения и сумма заявки
                clientDto.birthDate = model.BirthDate;
                clientDto.loanSum = model.LoanSum;
                clientDto.email = model.Email;

                //Присваиваем оставщися знчения модели.
                DateTime date1 = DateTime.Now;
                clientDto.dateRequest = date1.ToString("d");

                //сохраняем модель в БД
                db.clients.Add(clientDto);
                db.SaveChanges();

                id = clientDto.Id;

            }

            //Сообщение пользователю. с помощью темп дата
            TempData["SM"] = "Заявка успешно оформлена!";

            // ЗАГРУЗКА ФАЙЛА
            #region Загрузка файла

            // создание ссылок дирикторий(папок для картинки). Корневая папка
            var originalDirectory = new DirectoryInfo(string.Format($"{Server.MapPath(@"\")}Images\\Uploads"));

            ////Создается папка к кажому новому клиенту(по id).
            var pathString1 = Path.Combine(originalDirectory.ToString(), "Clients"); 
            var pathString2 = Path.Combine(originalDirectory.ToString(), "Clients\\"+id.ToString());
            //Создается папка дл хранения уменьшеной копии
            var pathString3 = Path.Combine(originalDirectory.ToString(), "Clients\\"+id.ToString()+"\\Thumds");
            var pathString4 = Path.Combine(originalDirectory.ToString(), "Clients\\"+id.ToString()+"\\Gallery");
            var pathString5 = Path.Combine(originalDirectory.ToString(), "Clients\\"+id.ToString()+ "\\Gallery\\Thumds");

            //Проверяем наличие директории (если нет, создаем)
            if (!Directory.Exists(pathString1))
                Directory.CreateDirectory(pathString1);
            if (!Directory.Exists(pathString2))
                Directory.CreateDirectory(pathString2);
            if (!Directory.Exists(pathString3))
                Directory.CreateDirectory(pathString3);
            if (!Directory.Exists(pathString4))
                Directory.CreateDirectory(pathString4);
            if (!Directory.Exists(pathString5))
                Directory.CreateDirectory(pathString5);

            //Проверяем был ли файл загружен.
            if (file != null && file.ContentLength > 0)
            {
                // Получаем разширение файла ContentType получаем тип передоваемого файла
                string ext = file.ContentType.ToLower();

                // проверяем разширение файла
                if (ext != "image/jpg" &&
                    ext != "image/jpeg" &&
                    ext != "image/pjpeg" &&
                    ext != "image/gif" &&
                    ext != "image/png" &&
                    ext != "image/x-png")
                {
                    using (DBContext db = new DBContext())
                    {
                        //model.
                        ModelState.AddModelError("", "Не коректный формат !! Изображение не было загружено!");
                        //Сообщение пользователю. с помощью темп дата
                        TempData["SM"] = "Заявка успешно оформлена! Но без изображения!!!";
                        return View(model);
                    }
                }

                //переменная для хранения имени изображения
                string imageName = file.FileName;

                //сохранить изображение в модель DTO
                using (DBContext db = new DBContext())
                {
                    Client dto = db.clients.Find(id);
                    dto.image = imageName;

                    db.SaveChanges();
                }

                //Назначить пути к оригинальному и уменьшеному изабражению
                var path = string.Format($"{pathString2}\\{imageName}");
                var path2 = string.Format($"{pathString3}\\{imageName}"); // уменьшенное изображене 

                //сохранить оригинальное изображение
                file.SaveAs(path);

                //создаем  и  сохраняем уменьшенную копиию
                //обьект WebImage позволяет работать с изображениями
                WebImage img = new WebImage(file.InputStream);
                img.Resize(200, 200); //Ширина, высота сохраненного изображения.
                img.Save(path2); // Куда сохраняем уменьшенное изображение


            }

            #endregion

            //переодрисовать пользователя
            return RedirectToAction("GetAllClients");
        }

        /// <summary>
        /// Получения списка всех клиентов(заявок) + страничная навигация+ номера страниц
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> GetAllClients(int? page, int? catId)
        {
            //лист для хранения клиентов
            List<ClientVM> listOfClientVM;

            workingWord = new WorkingWord(); // сохранение файлов на диске
            workMail = new WorkMail(); // Метод для отправки почты

            // создание ссылок дирикторий(папок для картинки). Корневая папка
            var originalDirectory = new DirectoryInfo(string.Format($"{Server.MapPath(@"\")}Archive_Documents"));


            //установить номер старницы
            var pageNumber = page ?? 1; // Если в  перемнной page Будет значение null. То по умолчанию устновится страница 1

            using(DBContext db = new DBContext())
            {
                //заполнить лист данными
                //listOfClientVM = db.clients.ToArray()
                //    .Where(x => catId == null || catId == 0 || x.Id);
                listOfClientVM = db.clients.ToArray().OrderBy(x => x.Id).Select(x => new ClientVM(x)).ToList();
                
                //сортировка (по id или имени???)
            }
            //устанавливаем постраничную навигацию. Номер страницы и количесто клиентов для отображения на одной странице
            var onePageOfClients = listOfClientVM.ToPagedList(pageNumber, 5);

            // через ViewBag отправляем в представления
            ViewBag.onePageOfClients = onePageOfClients;

            // Получаем имя директории
            string t = originalDirectory.ToString();
           // tempDoc = t;
           // workingWord.GetBoxCreateWord(t);

            //  await workMail.SendEmailAsync(t + @"\\TestSaveDoc.docx"); // отправка письма
           
            // Возращаем представление с данными
            return View(listOfClientVM);
        }

        /// <summary>
        /// Отправить клиенту отчет о заявке в формате Word 
        /// </summary>
        /// <returns></returns>
        public FileResult DownloadResultDocument( int id)
        {
            workingWord = new WorkingWord(); // сохранение файлов на диске

            // создание ссылок дирикторий(папок для картинки). Корневая папка
            var originalDirectory = new DirectoryInfo(string.Format($"{Server.MapPath(@"\")}Archive_Documents\\"));

            ////Создается папка к кажому новому клиенту(по id).
            var pathString1 = Path.Combine(originalDirectory.ToString(), "DocsClient");
            var pathString2 = Path.Combine(originalDirectory.ToString(), "DocsClient\\" + id.ToString());
            
            //Создается папка дл хранения уменьшеной копии
            var pathString3 = Path.Combine(originalDirectory.ToString(), "DocsClient\\" + id.ToString() + "\\Document");

            //Проверяем наличие директории (если нет, создаем)
            if (!Directory.Exists(pathString1))
                Directory.CreateDirectory(pathString1);
            if (!Directory.Exists(pathString2))
                Directory.CreateDirectory(pathString2);
            if (!Directory.Exists(pathString3))
                Directory.CreateDirectory(pathString3);
            
            // Путь к файлу
            // string file_path = Server.MapPath("~/Archive_Documents/TestSaveDoc.docx");
            string file_path = Server.MapPath("~/Archive_Documents/TestSaveDoc.docx");

            string pathDoc = pathString3;

            //workingWord.GetBoxCreateWord(pathString3, id.ToString());

            workingWord.GetBoxCreateWord(PrintDocPdf(id),pathString3, id.ToString());
            //Тестовой метод
            PrintDocPdf(id);

            // Тип файла - content-type
            string file_type = "application/docx";
            // Имя файла - необязательно
            string TestSaveDoc = $@"{pathString3}\\Result_Client{id}.docx";
            //return File(pathString3, file_type, TestSaveDoc);
            return File(TestSaveDoc, file_type, $"Сustomer_card№{id}.docx");

        }



        /// <summary>
        /// Печать документов
        /// </summary>
        /// <returns></returns>
        public string PrintDocPdf( int id)
        {
            //модель или лист для хранения
            Client list = new Client();
            string[] listDoc = new string[10]; 

            // Подклбчение к бд и поиск по id
            using (DBContext db = new DBContext())
            {
                list = db.clients.Find(id); 
            }
            
            
            
            // заполнение листа данными о клиенте
            list.ToString();
            // отправка готового массива в контролер печати


            return (list.ToString());
        }

        // GET: Admin/PagesClients/EditClient
        /// <summary>
        /// Редактирование заявки клиента
        /// </summary>
        /// <param name="id">Id заявки которую нужно отредактировать</param>
        /// <returns></returns>
       [HttpGet]
        public ActionResult EditClient(int id)
        {
            //Обявляем модель ClientVM
            ClientVM model;

            //Получаем заявку клиента из БД
            using(DBContext db = new DBContext())
            {

                Client dto = db.clients.Find(id);

                if(dto == null)
                {
                    return Content("Данной заявки клиента, НЕ обнаружено");
                }

                //заполняем модель
                model = new ClientVM(dto);

                //Получаем все изображения из галереи
                model.GalleryImages = Directory.EnumerateFiles(Server.MapPath("~/Images/Uploads/Clients/" + id + "/Thumds"))
                    .Select(fn => Path.GetFileName(fn)) ;

            }
            //Возращаем модель в представление
            return View(model);
        }
       
        /// <summary>
        /// Метод записи отредактированной заявки
        /// </summary>
        /// <param name="model"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditClient(ClientVM model, HttpPostedFileBase file)
        {
            //Получаем  ID заявки будем использоовать для работы с изображением
            int id = model.Id;

            //Список изображений
            //Получаем все изображения из галереи
            model.GalleryImages = Directory.EnumerateFiles(Server.MapPath("~/Images/Uploads/Clients/" + id + "/Thumds"))
                .Select(fn => Path.GetFileName(fn));

            //проверяем модель на валидность
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //поиск имени на уникальность
            //using(DBContext db  = new DBContext())
            //{
            //    //ищем В выборке все id кроме текущего. Проверяем на совпадения по фамилии
            //    if (db.clients.Where(x => x.Id != id).Any(x=> x.lastName == model.LastName))
            //    {
            //        //ModelState.AddModelError("","Данный клиент уже подовал заявку")
            // return View(model);
            //        //можно в дальнейшем реализовать историю заявок конкретного клиента
            //    }
            //}

            //обновляем продукт
            using (DBContext db = new DBContext())
            {
                //Загружаем старые(необновленные) данные заявки в БД
                Client dto = db.clients.Find(id);

                dto.firstName = model.FirstName;
                dto.lastName = model.LastName;
                dto.middleName = model.MiddleName;
                dto.birthDate = model.BirthDate;
                dto.dateRequest = DateTime.Now.ToString();
                dto.email = model.Email;
                dto.image = model.Image;

                db.SaveChanges();
            }

            //устанавливаем сообщение в темп дату
            //Сообщение пользователю. с помощью темп дата
            TempData["SM"] = "Заявка успешно обновленна!";

            //загружаем обработанно изображение

            //ЗАГРУЗКА ИЗАБРАЖЕНИЯ
            #region Загрузка изображения на сервер

            // проверяем загружен ли файл
            if(file!= null && file.ContentLength > 0)
            {

                //получить разширение файла
                string ext = file.ContentType.ToLower();

                // проверяем полученное разшерение файла
                if (ext != "image/jpg" &&
                    ext != "image/jpeg" &&
                    ext != "image/pjpeg" &&
                    ext != "image/gif" &&
                    ext != "image/png" &&
                    ext != "image/x-png")
                {
                    using (DBContext db = new DBContext())
                    {
                        //model.
                        ModelState.AddModelError("", "Не коректный формат !! Изображение не было загружено!");
                        //Сообщение пользователю. с помощью темп дата
                        TempData["SM"] = "Заявка успешно оформлена! Но без изображения!!!";
                        return View(model);
                    }
                }

                //устанавливаем пути загрузки
                // создание ссылок дирикторий(папок для картинки). Корневая папка
                var originalDirectory = new DirectoryInfo(string.Format($"{Server.MapPath(@"\")}Images\\Uploads"));

                ////путь к папке и кажому новому клиенту(по id).
                var pathString1 = Path.Combine(originalDirectory.ToString(), "Clients\\" + id.ToString());
                //Путь к  папка для хранения уменьшеной копии
                var pathString2 = Path.Combine(originalDirectory.ToString(), "Clients\\" + id.ToString() + "\\Thumds");

                //удаляем существуешие старые файлы  и директории. 
                DirectoryInfo dir1 = new DirectoryInfo(pathString1);
                DirectoryInfo dir2 = new DirectoryInfo(pathString2);

                foreach (var file2 in dir1.GetFiles())
                {
                    file2.Delete();
                }

                foreach (var file3 in dir2.GetFiles())
                {
                    file3.Delete();
                }

                //сохраняем изображение
                string  imageName = file.FileName;

                using (DBContext db = new DBContext())
                {
                    Client dto = db.clients.Find(id);
                    dto.image = imageName;

                    db.SaveChanges(); //save DB
                }

                //сохраняемм оригинал и превью картинки
                //Назначить пути к оригинальному и уменьшеному изабражению
                var path = string.Format($"{pathString1}\\{imageName}");
                var path2 = string.Format($"{pathString2}\\{imageName}"); // уменьшенное изображене 

                //сохранить оригинальное изображение
                file.SaveAs(path);

                //создаем  и  сохраняем уменьшенную копиию
                //обьект WebImage позволяет работать с изображениями
                WebImage img = new WebImage(file.InputStream);
                img.Resize(200, 200); //Ширина, высота сохраненного изображения.
                img.Save(path2); // Куда сохраняем уменьшенное изображение


            }


            #endregion

            //Переодрисовать пользователя
            return RedirectToAction("EditClient");
        }

        /// <summary>
        /// Удаление  заявок клиентов  по ID
        /// </summary>
        /// <param name="id">Id удаляевом заявки</param>
        /// <returns></returns>
       [HttpGet]
        public ActionResult DeleteClient(int id)
        {
            //Удаляем товар из БД
            using (DBContext db = new DBContext())
            {
                Client model = db.clients.Find(id);
                db.clients.Remove(model);

                db.SaveChanges();
            }

            //удаляем категории с картинками
            var originalDirectory = new DirectoryInfo(string.Format($"{Server.MapPath(@"\")}Images\\Uploads"));
            //Путь к  папка для хранения уменьшеной копии
            var pathString = Path.Combine(originalDirectory.ToString(), "Clients\\" + id.ToString() + "\\Thumds");

            if (Directory.Exists(pathString))
                Directory.Delete(pathString,true);

            //Переодрисовать пользователя
            return RedirectToAction("GetAllClients");

        }

        //ТЕСТОВОЙ МЕТОД*****
        // POST: Admin/PagesClients/AddClient
        /// <summary>
        /// Добавление новой заыявки. В админ зоне.
        /// Метод Post. Пришла форма от клиента
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddNewClient3(ClientVM model)
        {
            string str = "имя клиента";

            //Проверить модель на коректность заполнения(Валидность)

            if (!ModelState.IsValid) // если модель не валидна.
            {
                return View(model); //возращаем обратно пользователю на дороботку
            }

            using (DBContext db = new DBContext())
            {

                //Иницалиизация класса ClientVM
                Client clientDto = new Client();

                //присваиваем имя клиента в большом регистре модели. 
                clientDto.firstName = model.FirstName.ToUpper();
                clientDto.lastName = model.LastName.ToUpper();
                clientDto.middleName = model.MiddleName.ToUpper();

                //приводим первые буквы в верхи регист Тест с регуляркой
                //str = Regex.Replace(str.ToLower(), @"\b[a-zа-яё]", m => m.Value.ToUpper());

                clientDto.birthDate = model.BirthDate;
                clientDto.loanSum = model.LoanSum;

                #region Не испольуемые тестовые 
                //ПРоверка на уникальность заголовка НЕ Нужно!!

                //Проверяем есть ли крадкое описание, если нет создаем НЕ Используем

                //Убеждаемся что заголовок и крадкое описание уникально  НЕ Используем
                #endregion

                //Присваиваем оставщися знчения модели.
                DateTime date1 = DateTime.Now;
                clientDto.dateRequest = date1.ToString("d");

                //сохраняем модель в БД
                db.clients.Add(clientDto);
                db.SaveChanges();
            }
            //Передаем сообщение через Tempdate
            TempData["SM"] = "Заявка удачно добавлена!!";

            //переодресовываем пользователя на метод индекс
            return RedirectToAction("Index");
        }

       
    }
}
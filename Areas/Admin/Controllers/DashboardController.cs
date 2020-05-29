using RegistrationApplication.Models.Data;
using RegistrationApplication.Models.ViewModels.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace RegistrationApplication.Areas.Admin.Controllers
{
    /// <summary>
    /// Контролер панели управления Админа.
    /// </summary>
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ExitCabinet()
        {

            return Redirect("Dashboard/Index");
        }

        public ActionResult ToDoClient()
        {
            return Redirect("/Admin/PagesClients/AddNewClient");
           // return Redirect("Dashboard/Index");
        }

        public ActionResult Index2()
        {
            //Получить  из БД список сущноситей ClientVM для ввывода их в представлении
            List<ClientVM> clientList;

            //Инициализируем список и заполняем данными из Бд
            using(DBContext db = new DBContext())
            {               // Выборку приводим в арей(массив), сортируем, и создаем на экземпляр обьекта ClientVM, приводим к листу и сохраняем в лист
                clientList = db.clients.ToArray().OrderBy(x => x.Id).Select(x => new ClientVM(x)).ToList();

            }

            //Возврат списка в представлении

            return View(clientList);
        }

        // GET: Admin/Dashboard/AddClient
        /// <summary>
        /// Добавление новой заявки. 
        /// Метод пипа Get. Отправляем форму для клиента
        /// </summary>
        [HttpGet]
        public ActionResult AddClient()
        {
            return View();
        }

        // POST: Admin/Dashboard/AddClient
        /// <summary>
        /// Добавление новой заыявки. В админ зоне.
        /// Метод Post. Пришла форма от клиента
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddClient(ClientVM model)
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
                str = Regex.Replace(str.ToLower(), @"\b[a-zа-яё]", m => m.Value.ToUpper());

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
using RegistrationApplication.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RegistrationApplication.Models.ViewModels.Clients
{
    /// <summary>
    /// Класс для отображения модели.
    /// Получает все нужные данные из класса Client. Через конструктор, при создании экземпляра  текущего класса
    /// Для работы в представлениях. Для безопастности нет работы напрямус с БД.
    /// </summary>
    public class ClientVM
    {

        public ClientVM()
        {

        }

        /// <summary>
        /// Заполняем экземпляр заявки клиента данными
        /// </summary>
        /// <param name="row"></param>
       public ClientVM(Client row)
        {
            Id = row.Id;
            LastName = row.lastName;
            FirstName = row.firstName;
            MiddleName = row.middleName;
            BirthDate = row.birthDate;
            LoanSum = row.loanSum;
            Email = row.email;
            Image = row.image;
            ImagePathInDoc = row.imagePathInDoc;
            DateRequest = row.dateRequest;
        }


        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength =3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов") ]
        [Display(Name ="Фамилия")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "День рождения")]
       // public DateTime BirthDate { get; set; }
        public string BirthDate { get; set; }

        //[Required]
        [Display(Name = "Дата формирования заявки")]
       // public DateTime DateRequest { get; set; }
        public string DateRequest { get; set; }

        [Required]
        [Display(Name = "Сумма кредита")]
        [Range(1000, 10000, ErrorMessage = "Минимальная сумма кредита = 1000. Максимальная = 10000. ")]
        /// <summary>
        /// Сумма кредита
        /// </summary>
        public int LoanSum { get; set; }

        [Display(Name = "Почта клиента")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес. ВВедите без ошибок! ")]
        public string Email { get; set; }

        [Display(Name = "Логотип, картинка, Аватар")]
        public string Image { get; set; }

        public string ImagePathInDoc { get; set; }


        /// <summary>
        /// Свойство для работы с колекциями изображений
        /// </summary>
        public IEnumerable<string> GalleryImages { get; set; }
    }
}
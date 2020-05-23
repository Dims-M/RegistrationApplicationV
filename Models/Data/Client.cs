using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RegistrationApplication.Models.Data
{
    /// <summary>
    /// Оисание клиента
    /// Будет извлекать данные из БД tbClient и передовать их в класс ClientVM для вывода во вьюхах
    /// </summary>
    [Table("tbClient")] //С помощью анотаций, свявыеваем с таблицей.
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string lastName { get; set; }
        public string firstName{ get; set; }
        public string middleName { get; set; }
        //public DateTime birthDate { get; set; }
        public string birthDate { get; set; }
       
        /// <summary>
        /// Дата формирования заявки
        /// </summary>
       // public DateTime dateRequest { get; set; }
        public string dateRequest { get; set; }
       
        /// <summary>
        /// Сумма кредита
        /// </summary>
        public int loanSum { get; set; }

        public string email { get; set; }
        public string image { get; set; }

    }
}
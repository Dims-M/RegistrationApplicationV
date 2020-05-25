using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RegistrationApplication.Models.Data
{
    /// <summary>
    /// Класс связи с БД
    /// </summary>
    public class DBContext : DbContext
    {

        public DBContext()
        : base("DB")
        { }

        /// <summary>
        /// Соединение с БД. с таблицей Client
        /// </summary>
        public DbSet<Client> clients { get; set; }

        /// <summary>
        /// Связь с таблицей изображения
        /// </summary>
        public DbSet<Picture> Pictures { get; set; }

    }
}
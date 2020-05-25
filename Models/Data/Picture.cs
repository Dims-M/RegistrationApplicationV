using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationApplication.Models.Data
{
    /// <summary>
    /// тестовой Класс для работы с изабржанением
    /// </summary>
    public class Picture
    {
        public int Id { get; set; }
        public string Name { get; set; } // название картинки
        public byte[] Image { get; set; } //массив байтов картинки
    }
}
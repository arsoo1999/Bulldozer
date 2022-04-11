using System;
using System.Collections.Generic;
using System.Text;

namespace Bulldozer
{
    /// <summary>
    /// Класс-ошибка "Если не найден автомобиль по определенному месту"
    /// </summary>
    public class CampNotFoundException : Exception
    {
        public CampNotFoundException(int i) : base("Не найден автомобиль по месту "+ i)
        { }
    }
}

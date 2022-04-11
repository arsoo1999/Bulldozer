using System;
using System.Collections.Generic;
using System.Text;

namespace Bulldozer
{
    /// <summary>
    /// Класс-ошибка "Если на парковке уже заняты все места"
    /// </summary>
    public class CampOverflowException : Exception
    {
        public CampOverflowException() : base("На парковке нет свободных мест")
        { }
    }
}

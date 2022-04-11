using System;
using System.Collections.Generic;
using System.Text;

namespace Bulldozer
{
    /// <summary>
    /// Класс-коллекция парковок
    /// </summary>
    public class CampCollection
    {
        /// <summary>
        /// Словарь (хранилище) с парковками
        /// </summary>
        readonly Dictionary<string, Camp<IDrawTractor>> _campStages;
        /// <summary>
        /// Возвращение списка названий парковок
        /// </summary>
        public List<string> Keys => _campStages.Keys.ToList();
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private readonly int _pictureWidth;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private readonly int _pictureHeight;
        /// <summary>
        /// Конструктор
        /// </summary>
        public CampCollection(int pictureWidth, int pictureHeight)
        {
            _campStages = new Dictionary<string, Camp<IDrawTractor>>();
            _pictureWidth = pictureWidth;
            _pictureHeight = pictureHeight;
        }
        
        /// Добавление парковки
        /// </summary>
        /// <param name="name">Название парковки</param>
        public void AddParking(string name)
        {
           _campStages.Add(name);
        }
        /// <summary>
        /// Удаление парковки
        /// </summary>
        public void DelParking(string name)
        {
            _campStages.Remove(name);
        }
        /// <summary>
        /// Доступ к парковке
        /// </summary>
        public Camp<IDrawTractor> this[string ind]
        {
            get
            {
                    return _campStages[ind];
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace Bulldozer
{
    /// <summary>
    /// Параметризованный класс для хранения набора объектов от интерфейса IDrawTractor
    /// </summary>
    public class Camp<T> where T : class, IDrawTractor
    {
        /// <summary>
        /// Список объектов, которые храним
        /// </summary>
        private readonly List<T> _places;
        /// <summary>
        /// Максимальное количество мест на парковке
        /// </summary>
        private readonly int _maxCount;
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private readonly int _pictureWidth;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private readonly int _pictureHeight;
        /// <summary>
        /// Размер места стоянки (ширина)
        /// </summary>
        private readonly int _placeSizeWidth = 210;
        /// <summary>
        /// Размер места стоянки (высота)
        /// </summary>
        private readonly int _placeSizeHeight = 100;
        /// <summary>
        /// Конструктор
        /// </summary>
        public Camp(int picWidth, int picHeight)
        {
            int width = picWidth / _placeSizeWidth;
            int height = picHeight / _placeSizeHeight;
            _maxCount = width * height;
            _pictureWidth = picWidth;
            _pictureHeight = picHeight;
            _places = new List<T>();
        }
        /// <summary>
        /// Перегрузка оператора сложения
        /// Логика действия: на парковку добавляется автомобиль
        /// </summary>
        public static bool operator +(Camp<T> p, T tractor)
        {
            if (p._places.Count == p._maxCount)
            {
                throw new CampOverflowException();
            }
            for (int i = 0; i < p._maxCount; i++)
            {
                    p._places[i] = tractor;
                    p._places[i].SetPosition(4 + i / 4 * p._placeSizeWidth + 4,
                     i % 4 * p._placeSizeHeight + 15, p._pictureWidth,
                    p._pictureHeight);
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Перегрузка оператора вычитания
        /// Логика действия: с парковки забираем автомобиль
        /// </summary>
        public static T operator -(Camp<T> p, int index)
        {
            if (index < 0 || index > p._maxCount)
            {
                return null;
            }
                T tractor = p._places[index];
                p._places[index] = null;
                return tractor;
                throw new CampNotFoundException(index);
        }
        /// <summary>
        /// Метод отрисовки парковки
        /// </summary>
        public void Draw(Graphics g)
        {
            DrawMarking(g);
            for (int i = 0; i < _places.Count; i++)
            {
                _places[i].SetPosition(4 + i / 4 * _placeSizeWidth + 4, i % 4 * _placeSizeHeight + 15, _pictureWidth, _pictureHeight);
                _places[i].DrawTractor(g);

            }
        }
        /// <summary>
        /// Метод отрисовки разметки парковочных мест
        /// </summary>
        private void DrawMarking(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 3);
            for (int i = 0; i < _pictureWidth / _placeSizeWidth; i++)
            {
                for (int j = 0; j < _pictureHeight / _placeSizeHeight + 1; ++j)
                {//линия рамзетки места
                    g.DrawLine(pen, i * _placeSizeWidth, j * _placeSizeHeight, i * _placeSizeWidth + _placeSizeWidth / 2, j * _placeSizeHeight);
                }
                g.DrawLine(pen, i * _placeSizeWidth, 0, i * _placeSizeWidth, (_pictureHeight / _placeSizeHeight) * _placeSizeHeight);
            }

        }
        /// <summary>
        /// Функция получения элементов из списка
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetNext()
        {
            foreach (var elem in _places)
            {
                yield return elem;
            }
        }
    }
}

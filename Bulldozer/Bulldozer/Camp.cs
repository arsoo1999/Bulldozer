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
        /// Массив объектов, которые храним
        /// </summary>
        private readonly T[] _places;
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
            _places = new T[width * height];
            _pictureWidth = picWidth;
            _pictureHeight = picHeight;
        }
        /// <summary>
        /// Перегрузка оператора сложения
        /// Логика действия: на парковку добавляется автомобиль
        /// </summary>
        public static bool operator +(Camp<T> p, T tractor)
        {
            for (int i = 0; i < p._places.Length; i++)
            {
                if (p._places[i] == null)
                {
                    p._places[i] = tractor;
                    p._places[i].SetPosition(4 + i / 4 * p._placeSizeWidth + 4,
                     i % 4 * p._placeSizeHeight + 15, p._pictureWidth,
                    p._pictureHeight);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Перегрузка оператора вычитания
        /// Логика действия: с парковки забираем автомобиль
        /// </summary>
        public static T operator -(Camp<T> p, int index)
        {
            if (index < 0 || index > p._places.Length)
            {
                return null;
            }
                T tractor = p._places[index];
                p._places[index] = null;
                return tractor;
            return null;
        }
        /// <summary>
        /// Метод отрисовки парковки
        /// </summary>
        public void Draw(Graphics g)
        {
            DrawMarking(g);
            for (int i = 0; i < _places.Length; i++)
            {
                _places[i]?.DrawTractor(g);
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
                for (int j = 0; j < _pictureHeight / _placeSizeHeight + 1;
                ++j)
                {//линия рамзетки места
                    g.DrawLine(pen, i * _placeSizeWidth, j *
                    _placeSizeHeight, i * _placeSizeWidth + _placeSizeWidth / 2, j * _placeSizeHeight);
                }
                g.DrawLine(pen, i * _placeSizeWidth, 0, i *
                _placeSizeWidth, (_pictureHeight / _placeSizeHeight) * _placeSizeHeight);
            }

        }
    }
}

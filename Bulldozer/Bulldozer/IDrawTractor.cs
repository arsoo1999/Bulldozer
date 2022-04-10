using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Bulldozer
{
    /// <summary>
    /// Интерфейс для работы с объектом, отрисовываемым на форме
    /// </summary>
    public interface IDrawTractor
    {
        /// <summary>
        /// Шаг объекта
        /// </summary>
        float Step { get; }
        /// <summary>
        /// Цвет объекта
        /// </summary>
        Color BodyColor { get; }
        /// <summary>
        /// Установка позиции объекта
        /// </summary>
        void SetPosition(float x, float y, int width, int height);
        /// <summary>
        /// Изменение направления пермещения объекта
        /// </summary>
        bool MoveObject(Direction direction);
        /// <summary>
        /// Отрисовка объекта
        /// </summary>
        void DrawObject(Graphics g);
        /// <summary>
        /// Получение текущей позиции объекта
        /// </summary>
        (float Left, float Right, float Top, float Bottom) GetCurrentPosition();
    }
}

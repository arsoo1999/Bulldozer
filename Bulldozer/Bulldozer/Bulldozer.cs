using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Bulldozer
{
    public class Tractor
    {
        /// <summary>
        /// Скорость
        /// </summary>
        public int Speed { private set; get; }
        /// <summary>
        /// Вес трактора
        /// </summary>
        public float Weight { private set; get; }
        /// <summary>
        /// Цвет кузова
        /// </summary>
        public Color BodyColor { private set; get; }
        /// <summary>
        /// Левая координата отрисовки трактора
        /// </summary>
        private float? _startPosX = null;
        /// <summary>
        /// Верхняя кооридната отрисовки трактора
        /// </summary>
        private float? _startPosY = null;
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private int? _pictureWidth = null;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private int? _pictureHeight = null;
        /// <summary>
        /// Ширина отрисовки автомобиля
        /// </summary>
        protected readonly int _tractorWidth = 80;
        /// <summary>
        /// Высота отрисовки автомобиля
        /// </summary>
        protected readonly int _tractorHeight = 50;
        /// <summary>
        /// Инициализация свойств
        /// </summary>
        public void Init(int speed, float weight, Color bodyColor)
        {
            Speed = speed;
            Weight = weight;
            BodyColor = bodyColor;
        }
        /// <summary>
        /// Установка позиции автомобиля
        /// </summary>
        public void SetPosition(int x, int y, int width, int height)
        {
            _startPosX = x;
            _startPosY = y;
            _pictureWidth = width;
            _pictureHeight = height;
        }
        /// <summary>
        /// Смена границ формы отрисовки
        /// </summary>
        public void ChangeBorders(int width, int height)
        {
            _pictureWidth = width;
            _pictureHeight = height;
            if (_startPosX + _tractorWidth > width)
            {
                _startPosX = width - _tractorWidth;
            }
            if (_startPosY + _tractorHeight > height)
            {
                _startPosY = height - _tractorHeight;
            }
        }
        /// <summary>
        /// Изменение направления пермещения
        /// </summary>
        /// <param name="direction">Направление</param>
        public void MoveTransport(Direction direction)
        {
            if (!_pictureWidth.HasValue || !_pictureHeight.HasValue)
            {
                return;
            }
            float step = Speed * 100 / Weight;
            switch (direction)
            {
                // вправо
                case Direction.Right:
                    if (_startPosX + _tractorWidth + step < _pictureWidth)
                    {
                        _startPosX += step;
                    }
                    break;
                //влево
                case Direction.Left:
                    if (_startPosX - step > 0)
                    {
                        _startPosX -= step;
                    }
                    break;
                //вверх
                case Direction.Up:
                    if (_startPosY - step > 0)
                    {
                        _startPosY -= step;
                    }
                    break;
                //вниз
                case Direction.Down:
                    if (_startPosY + _tractorHeight + step < _pictureHeight)
                    {
                        _startPosY += step;
                    }
                    break;
            }
        }
        /// <summary>
        /// Отрисовка автомобиля
        /// </summary>
        /// <param name="g"></param>
        public void DrawTransport(Graphics g)
        {
            if (!_startPosX.HasValue || !_startPosY.HasValue)
            {
                return;
            }
            Pen pen = new Pen(Color.Black,4);
            Pen penCatok = new Pen(Color.Black, 2);
            Brush brWhite = new SolidBrush(Color.White);
            Brush br = new SolidBrush(BodyColor);
            //границы корпуса
            g.DrawRectangle(pen, _startPosX.Value, _startPosY.Value, 30, 30);
            g.DrawRectangle(pen, _startPosX.Value, _startPosY.Value + 30, 100, 20);
            g.DrawRectangle(pen, _startPosX.Value + 60, _startPosY.Value + 5, 7, 25);
            //границы гусеницы
            g.DrawEllipse(pen, _startPosX.Value, _startPosY.Value + 52, 20, 20);
            g.DrawEllipse(pen, _startPosX.Value, _startPosY.Value + 57, 20, 20);
            g.DrawEllipse(pen, _startPosX.Value + 90, _startPosY.Value + 52, 20, 20);
            g.DrawEllipse(pen, _startPosX.Value + 90, _startPosY.Value + 57, 20, 20);
            g.DrawRectangle(pen, _startPosX.Value, _startPosY.Value + 62, 10, 5);
            g.DrawRectangle(pen, _startPosX.Value + 100, _startPosY.Value + 62, 10, 5);
            g.DrawRectangle(pen, _startPosX.Value + 10, _startPosY.Value + 52, 90, 25);
            //окрас корпуса
            g.FillRectangle(br, _startPosX.Value, _startPosY.Value, 30, 30);
            g.FillRectangle(br, _startPosX.Value, _startPosY.Value + 30, 100, 20);
            g.FillRectangle(br, _startPosX.Value + 60, _startPosY.Value + 5, 7, 25);
            //окрас гусениц
            g.FillEllipse(brWhite, _startPosX.Value, _startPosY.Value + 52, 20, 20);
            g.FillEllipse(brWhite, _startPosX.Value, _startPosY.Value + 57, 20, 20);
            g.FillEllipse(brWhite, _startPosX.Value + 90, _startPosY.Value + 52, 20, 20);
            g.FillEllipse(brWhite, _startPosX.Value + 90, _startPosY.Value + 57, 20, 20);
            g.FillRectangle(brWhite, _startPosX.Value, _startPosY.Value + 62, 10, 5);
            g.FillRectangle(brWhite, _startPosX.Value + 100, _startPosY.Value + 62, 10, 5);
            g.FillRectangle(brWhite, _startPosX.Value + 10, _startPosY.Value + 52, 90, 25);
            //границы катков
            g.DrawEllipse(penCatok, _startPosX.Value + 1, _startPosY.Value + 53, 22, 22);
            g.DrawEllipse(penCatok, _startPosX.Value + 86, _startPosY.Value + 53, 22, 22);
            g.DrawEllipse(penCatok, _startPosX.Value + 41, _startPosY.Value + 53, 5, 5);
            g.DrawEllipse(penCatok, _startPosX.Value + 61, _startPosY.Value + 53, 5, 5);
            g.DrawEllipse(penCatok, _startPosX.Value + 25, _startPosY.Value + 60, 15, 15);
            g.DrawEllipse(penCatok, _startPosX.Value + 48, _startPosY.Value + 60, 15, 15);
            g.DrawEllipse(penCatok, _startPosX.Value + 68, _startPosY.Value + 60, 15, 15);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Bulldozer
{
    public class Tractor : IDrawTractor
    {
        public void SetMainColor(Color color) => BodyColor = color;

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
        public float Step => Speed * 100 / Weight;
        /// <summary>
        /// Левая координата отрисовки трактора
        /// </summary>
        protected float? _startPosX = null;
        /// <summary>
        /// Верхняя кооридната отрисовки трактора
        /// </summary>
        protected float? _startPosY = null;
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
        private readonly int _tractorWidth = 80;
        /// <summary>
        /// Высота отрисовки автомобиля
        /// </summary>
        private readonly int _tractorHeight = 50;
        /// <summary>
        /// Признак, что объект переместился
        /// </summary>
        private bool _makeStep;
        /// <summary>
        /// Инициализация свойств
        /// </summary>
        public Tractor(int speed, float weight, Color bodyColor)
        {
            Speed = speed;
            Weight = weight;
            BodyColor = bodyColor;
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        protected Tractor(int speed, float weight, Color bodyColor, int tractorWidth, int
        tractorHeight)
        {
            Speed = speed;
            Weight = weight;
            BodyColor = bodyColor;
            _tractorWidth = tractorWidth;
            _tractorHeight = tractorHeight;
        }
        /// <summary>
        /// Установка позиции автомобиля
        /// </summary>
        public void SetPosition(float x, float y, int width, int height)
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
        public virtual void MoveTransport(Direction direction, int leftIndent = 0, int topIndent = 0)
        {
            _makeStep = false;
            if (!_pictureWidth.HasValue || !_pictureHeight.HasValue)
            {
                return;
            }
            float step = Speed * 100 / Weight;
            switch (direction)
            {
                // вправо
                case Direction.Right:
                    if (_startPosX + _tractorWidth + Step < _pictureWidth)
                    {
                        _startPosX += Step;
                        _makeStep = true;
                    }
                    break;
                //влево
                case Direction.Left:
                    if (_startPosX - Step > 0)
                    {
                        _startPosX -= Step;
                        _makeStep = true;
                    }
                    break;
                //вверх
                case Direction.Up:
                    if (_startPosY - Step > 0)
                    {
                        _startPosY -= Step;
                        _makeStep = true;
                    }
                    break;
                //вниз
                case Direction.Down:
                    if (_startPosY + _tractorHeight + Step < _pictureHeight)
                    {
                        _startPosY += Step;
                        _makeStep = true;
                    }
                    break;
            }
        }
        /// <summary>
        /// Отрисовка автомобиля
        /// </summary>
        /// <param name="g"></param>
        public virtual void DrawTractor(Graphics g)
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
        public bool MoveTractor(Direction direction)
        {
            MoveTransport(direction);
            return _makeStep;
        }
        public void DrawObject(Graphics g)
        {
            DrawTractor(g);
        }
        public (float Left, float Right, float Top, float Bottom)
GetCurrentPosition()
        {
            return (_startPosX.Value, _startPosX.Value + _tractorWidth,
            _startPosY.Value, _startPosY.Value + _tractorHeight);
        }
        /// <summary>
        /// Разделитель для записи информации по объекту в файл
        /// </summary>
        protected readonly char _separator = ';';
        /// <summary>
        /// Конструктор для загрузки с файла
        /// </summary>
        /// <param name="info">Информация по объекту</param>
        public Tractor(string info)
        {
            string[] strs = info.Split(_separator);
            if (strs.Length >= 3)
            {
                Speed = Convert.ToInt32(strs[0]);
                Weight = Convert.ToInt32(strs[1]);
                BodyColor = Color.FromName(strs[2]);
            }
        }
        public override string ToString() =>$"{Speed}{_separator}{Weight}{_separator}{BodyColor.Name}";
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Bulldozer
{
    /// <summary>
    /// Класс отрисовки гоночного автомобиля
    /// </summary>
    public class FarmTractor : Tractor
    {
        /// <summary>
        /// Дополнительный цвет
        /// </summary>
        public Color DopColor { private set; get; }
        /// <summary>
        /// Признак наличия переднего спойлера
        /// </summary>
        public bool FrontSpoiler { private set; get; }
        /// <summary>
        /// Признак наличия заднего спойлера
        /// </summary>
        public bool BackSpoiler { private set; get; }
        /// <summary>
        /// Инициализация свойств
        /// </summary>
        /// <param name="maxSpeed">Скорость</param>
        /// <param name="weight">Вес</param>
        /// <param name="bodyColor">Цвет кузова</param>
        /// <param name="dopColor">Дополнительный цвет</param>
        /// <param name="frontSpoiler">Признак наличия переднего спойлера</param>
        /// <param name="backSpoiler">Признак наличия заднего спойлера</param>
        public FarmTractor(int speed, float weight, Color bodyColor, Color dopColor,
        bool frontSpoiler, bool backSpoiler) : base(speed, weight, bodyColor, 100, 60)
        {
            DopColor = dopColor;
            FrontSpoiler = frontSpoiler;
            BackSpoiler = backSpoiler;
        }
        public override void MoveTransport(Direction direction, int leftIndent = 0, int topIndent = 0)
        {
            base.MoveTransport(direction,leftIndent,topIndent);
        }
        public override void DrawTransport(Graphics g)
        {
            if (!_startPosX.HasValue || !_startPosY.HasValue)
            {
                return;
            }
            Pen pen = new Pen(Color.Black, 2);
            Brush brIndigo = new SolidBrush(Color.Indigo);
            Brush dopBrush = new SolidBrush(DopColor);
            if (BackSpoiler)
            {
                //Границы рахлителя
                g.DrawRectangle(pen, _startPosX.Value - 30, _startPosY.Value + 45, 30, 4);
                g.DrawEllipse(pen, _startPosX.Value - 40, _startPosY.Value + 45, 30,30);
                g.DrawRectangle(pen, _startPosX.Value - 11, _startPosY.Value + 55, 4, 15);
                g.DrawRectangle(pen, _startPosX.Value - 20, _startPosY.Value + 65, 4, 15);
                g.DrawRectangle(pen, _startPosX.Value - 29, _startPosY.Value + 68, 4, 15);
                g.DrawRectangle(pen, _startPosX.Value - 38, _startPosY.Value + 60, 4, 15);
                //Окраска рыхлителя
                g.FillRectangle(dopBrush, _startPosX.Value - 30, _startPosY.Value + 45, 30, 4);
                g.FillEllipse(brIndigo, _startPosX.Value - 40, _startPosY.Value + 45, 30, 30);
                g.FillRectangle(dopBrush, _startPosX.Value - 11, _startPosY.Value + 55, 4, 15);
                g.FillRectangle(dopBrush, _startPosX.Value - 20, _startPosY.Value + 65, 4, 15);
                g.FillRectangle(dopBrush, _startPosX.Value - 29, _startPosY.Value + 68, 4, 15);
                g.FillRectangle(dopBrush, _startPosX.Value - 38, _startPosY.Value + 60, 4, 15);
            }
            base.DrawTransport(g);
            if (FrontSpoiler)
            {
                //Границы отвала
                g.DrawRectangle(pen, _startPosX.Value + 102, _startPosY.Value + 45, 20, 4);
                g.DrawRectangle(pen, _startPosX.Value + 122, _startPosY.Value + 37, 6, 38);
                g.DrawRectangle(pen, _startPosX.Value + 128, _startPosY.Value + 66, 10, 9);
                //Окраска рыхлителя
                g.FillRectangle(dopBrush, _startPosX.Value + 102, _startPosY.Value + 45, 20, 4);
                g.FillRectangle(dopBrush, _startPosX.Value + 122, _startPosY.Value + 37, 6, 38);
                g.FillRectangle(dopBrush, _startPosX.Value + 128, _startPosY.Value + 66, 10, 9);

            }
        }
    }
}

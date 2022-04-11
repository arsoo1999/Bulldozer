using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bulldozer
{
    public partial class FormBulldozer : Form
    {
        private IDrawTractor _tractor;

        public FormBulldozer()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Передача машины на форму
        /// </summary>
        public void SetTractor(IDrawTractor tractor)
        {
            _tractor = tractor;
            Draw();
        }
        /// <summary>
        /// Метод отрисовки машины
        /// </summary>
        private void Draw()
        {
            Bitmap bmp = new Bitmap(pictureBoxTractor.Width, pictureBoxTractor.Height);
            Graphics gr = Graphics.FromImage(bmp);
            _tractor?.DrawTractor(gr);
            pictureBoxTractor.Image = bmp;
        }
        /// <summary>
        /// Метод установки объекта на форме
        /// </summary>
        /// <param name="rnd"></param>
        private void SetObject(Random rnd)
        {
            _tractor?.SetPosition(rnd.Next(10, 100), rnd.Next(10, 100),
            pictureBoxTractor.Width, pictureBoxTractor.Height);
            Draw();
        }
        /// <summary>
        /// Обработка нажатия кнопок управления
        /// </summary>
        private void ButtonMove_Click(object sender, EventArgs e)
        {
            //получаем имя кнопки
            string name = (sender as Button).Name;
            switch (name)
            {
                case "buttonUp":
                    _tractor?.MoveTractor(Direction.Up);
                    break;
                case "buttonDown":
                    _tractor?.MoveTractor(Direction.Down);
                    break;
                case "buttonLeft":
                    _tractor?.MoveTractor(Direction.Left);
                    break;
                case "buttonRight":
                    _tractor?.MoveTractor(Direction.Right);
                    break;
            }
            Draw();
        }
    }
}

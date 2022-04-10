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
        private Tractor _tractor;

        public FormBulldozer()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Метод отрисовки машины
        /// </summary>
        private void Draw()
        {
            Bitmap bmp = new Bitmap(pictureBoxTractor.Width, pictureBoxTractor.Height);
            Graphics gr = Graphics.FromImage(bmp);
            _tractor?.DrawTransport(gr);
            pictureBoxTractor.Image = bmp;
        }
        /// <summary>
        /// Изменение размеров формы отрисовки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxCars_Resize(object sender, EventArgs e)
        {
            _tractor?.ChangeBorders(pictureBoxTractor.Width, pictureBoxTractor.Height);
            Draw();
        }
        /// <summary>
        /// Обработка нажатия кнопки "Создать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            _tractor = new Tractor();
            _tractor.Init(rnd.Next(100, 300), rnd.Next(1000, 2000),
            Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)));
            _tractor.SetPosition(rnd.Next(10, 100), rnd.Next(10, 100),
            pictureBoxTractor.Width, pictureBoxTractor.Height);
            toolStripStatusLabelSpeed.Text = "Скорость:" + _tractor.Speed;
            toolStripStatusLabelWeight.Text = "Вес: " + _tractor.Weight;
            toolStripStatusLabelColor.Text = "Цвет: " + _tractor.BodyColor.Name;
            Draw();
        }
        /// <summary>
        /// Обработка нажатия кнопок управления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMove_Click(object sender, EventArgs e)
        {
            //получаем имя кнопки
            string name = (sender as Button).Name;
            switch (name)
            {
                case "buttonUp":
                    _tractor?.MoveTransport(Direction.Up);
                    break;
                case "buttonDown":
                    _tractor?.MoveTransport(Direction.Down);
                    break;
                case "buttonLeft":
                    _tractor?.MoveTransport(Direction.Left);
                    break;
                case "buttonRight":
                    _tractor?.MoveTransport(Direction.Right);
                    break;
            }
            Draw();
        }
    }
}

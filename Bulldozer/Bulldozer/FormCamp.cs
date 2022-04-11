using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Bulldozer
{
    public partial class FormCamp : Form
    {
        /// <summary>
        /// Объект от класса-парковки
        /// </summary>
        private readonly Camp<IDrawTractor> camp;


        public FormCamp()
        {
            InitializeComponent();
            camp = new Camp<IDrawTractor>(pictureBoxParking.Width, pictureBoxParking.Height);
            Draw();
        }
        /// <summary>
        /// Метод отрисовки парковки
        /// </summary>
        private void Draw()
        {
            Bitmap bmp = new Bitmap(pictureBoxParking.Width,
            pictureBoxParking.Height);
            Graphics gr = Graphics.FromImage(bmp);
            camp.Draw(gr);
            pictureBoxParking.Image = bmp;
        }
        /// <summary>
        /// Обработка нажатия кнопки "Припарковать автомобиль"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSetTractor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                AddToParking(new Tractor(100, 1000, dialog.Color));
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Припарковать гоночный автомобиль"
        /// </summary>
        private void ButtonSetFarmTractor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ColorDialog dialogDop = new ColorDialog();
                if (dialogDop.ShowDialog() == DialogResult.OK)
                {
                    AddToParking(new FarmTractor(100, 1000, dialog.Color,
                    dialogDop.Color, true, true));
                }
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Забрать"
        /// </summary>
        private void ButtonTakeTractor_Click(object sender, EventArgs e)
        {
            if (maskedTextBox.Text != "")
            {
                var tractor = camp - Convert.ToInt32(maskedTextBox.Text);
                if (tractor != null)
                {
                    FormBulldozer form = new FormBulldozer();
                    form.SetTractor(tractor);
                    form.ShowDialog();
                }
                Draw();
            }
        }
        /// <summary>
        /// Добавление объекта в класс-хранилище
        /// </summary>
        private void AddToParking(Tractor tractor)
        {
            if (camp + tractor)
            {
                Draw();
            }
            else
            {
                MessageBox.Show("Парковка переполнена");
            }
        }
    }
}

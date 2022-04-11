using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Bulldozer
{
    public partial class FormBuldozerConfig : Form
    {
        /// <summary>
        /// Переменная-выбранная машина
        /// </summary>
        Tractor _tractor = null;
        /// <summary>
        /// Событие
        /// </summary>
        private event TractorDelegate EventAddTractor;

        public FormBuldozerConfig()
        {
            InitializeComponent();
            panelBlack.MouseDown += PanelColor_MouseDown;
            panelGray.MouseDown += PanelColor_MouseDown;
            panelPink.MouseDown += PanelColor_MouseDown;
            panelGreen.MouseDown += PanelColor_MouseDown;
            panelRed.MouseDown += PanelColor_MouseDown;
            panelWhite.MouseDown += PanelColor_MouseDown;
            panelYellow.MouseDown += PanelColor_MouseDown;
            panelBlue.MouseDown += PanelColor_MouseDown;

            buttonCancel.Click += (object sender, EventArgs e) => { Close(); };
        }
        /// <summary>
        /// Отрисовать машину
        /// </summary>
        private void DrawTractor()
        {
            if (_tractor != null)
            {
                Bitmap bmp = new Bitmap(pictureBoxTractor.Width,
                pictureBoxTractor.Height);
                Graphics gr = Graphics.FromImage(bmp);
                _tractor.SetPosition(5, 5, pictureBoxTractor.Width,
                pictureBoxTractor.Height);
                _tractor.DrawTractor(gr);
                pictureBoxTractor.Image = bmp;
            }
        }
        /// <summary>
        /// Добавление события
        /// </summary>
        /// <param name="ev"></param>
        public void AddEvent(TractorDelegate ev)
        {
            if (EventAddTractor == null)
            {
                EventAddTractor = new TractorDelegate(ev);
            }
            else
            {
                EventAddTractor += ev;
            }
        }
        /// <summary>
        /// Передаем информацию при нажатии на Label
        /// </summary>
        private void LabelTractor_MouseDown(object sender, MouseEventArgs e)
        {
            (sender as Label).DoDragDrop((sender as Label).Name,DragDropEffects.Move | DragDropEffects.Copy);
        }
        /// <summary>
        /// Проверка получаемой информации (ее типа на соответствие требуемому)
        /// </summary>
        private void PanelTractor_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        /// <summary>
        /// Действия при приеме перетаскиваемой информации
        /// </summary>
        private void PanelTractor_DragDrop(object sender, DragEventArgs e)
        {
            switch (e.Data.GetData(DataFormats.Text).ToString())
            {
                case "labelTractor":
                    _tractor = new Tractor((int)numericUpDownMaxSpeed.Value, (int)numericUpDownWeight.Value, Color.White);
                    break;
                case "labelFarmTractor":
                    _tractor = new FarmTractor((int)numericUpDownMaxSpeed.Value, (int)numericUpDownWeight.Value, Color.White,
                    Color.Black,
                    checkBoxFrontSpoiler.Checked,
                    checkBoxBackSpoiler.Checked);
                    break;
            }
            DrawTractor();
        }
        /// <summary>
        /// Отправляем цвет с панели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelColor_MouseDown(object sender, MouseEventArgs e)
        {
            (sender as Control).DoDragDrop((sender as Control).BackColor, DragDropEffects.Move | DragDropEffects.Copy);
        }
        /// <summary>
        /// Проверка получаемой информации (ее типа на соответствие требуемому)
        /// </summary>
        private void LabelBaseColor_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Color)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void LabelDopColor_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Color)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        /// <summary>
        /// Принимаем основной цвет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelBaseColor_DragDrop(object sender, DragEventArgs e)
        {
            if (_tractor != null)
            {
                _tractor.SetMainColor((Color)e.Data.GetData(typeof(Color)));
                DrawTractor();
            }
        }
        /// <summary>
        /// Принимаем дополнительный цвет
        /// </summary>
        private void LabelDopColor_DragDrop(object sender, DragEventArgs e)
        {
            if (_tractor != null)
            {
                if (_tractor is FarmTractor)
                {
                    (_tractor as FarmTractor).SetDopColor((Color)e.Data.GetData(typeof(Color)));
                    DrawTractor();
                }
            }
        }
    }
}

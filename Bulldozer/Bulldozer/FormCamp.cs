using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace Bulldozer
{
    public partial class FormCamp : Form
    {
        /// <summary>
        /// Объект от класса-парковки
        /// </summary>
        private readonly CampCollection _campCollection;
        /// <summary>
        /// Логгер
        /// </summary>
        private readonly Logger logger;

        public FormCamp()
        {
            InitializeComponent();
            _campCollection = new CampCollection(pictureBoxParking.Width, pictureBoxParking.Height);
            logger = LogManager.GetCurrentClassLogger();

        }
        /// <summary>
        /// Заполнение listBoxLevels
        /// </summary>
        private void ReloadLevels()
        {
            int index = listBoxLevels.SelectedIndex;
            listBoxLevels.Items.Clear();
            for (int i = 0; i < _campCollection.Keys.Count; i++)
            {
                listBoxLevels.Items.Add(_campCollection.Keys[i]);
            }
            if (listBoxLevels.Items.Count > 0 && (index == -1 || index >=
            listBoxLevels.Items.Count))
            {
                listBoxLevels.SelectedIndex = 0;
            }
            else if (listBoxLevels.Items.Count > 0 && index > -1 && index <
            listBoxLevels.Items.Count)
            {
                listBoxLevels.SelectedIndex = index;
            }
        }
        /// <summary>
        /// Метод отрисовки парковки
        /// </summary>
        private void Draw()
        {
            if (listBoxLevels.SelectedIndex > -1)
            {
                //если выбран один из пуктов в listBox (при старте программы ни один пункт не будет выбран и может возникнуть ошибка, если мы попытаемся обратиться к элементу listBox)
                Bitmap bmp = new Bitmap(pictureBoxParking.Width, pictureBoxParking.Height);
                Graphics gr = Graphics.FromImage(bmp);
                _campCollection[listBoxLevels.SelectedItem.ToString()].Draw(gr);
                pictureBoxParking.Image = bmp;
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Добавить парковку"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddParking_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNewCampName.Text))
            {
                MessageBox.Show("Введите название парковки", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            logger.Info($"Добавили парковку {textBoxNewCampName.Text}");
            _campCollection.AddParking(textBoxNewCampName.Text);
            ReloadLevels();
        }
        /// <summary>
        /// Обработка нажатия кнопки "Удалить парковку"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDelParking_Click(object sender, EventArgs e)
        {
            if (listBoxLevels.SelectedIndex > -1)
            {
                if (MessageBox.Show($"Удалить парковку { listBoxLevels.SelectedItem}?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _campCollection.DelParking(listBoxLevels.SelectedItem.ToString());
                    logger.Info($"Удалили парковку{ listBoxLevels.SelectedItem}");
                    ReloadLevels();
                }
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Припарковать автомобиль"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSetTractor_Click(object sender, EventArgs e)
        {
            if (listBoxLevels.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    AddToParking(new Tractor(100, 1000, dialog.Color));
                }
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Припарковать гоночный автомобиль"
        /// </summary>
        private void ButtonSetFarmTractor_Click(object sender, EventArgs e)
        {
            if (listBoxLevels.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ColorDialog dialogDop = new ColorDialog();
                    if (dialogDop.ShowDialog() == DialogResult.OK)
                    {
                        AddToParking(new FarmTractor(100, 1000,
                        dialog.Color, dialogDop.Color, true, true));
                    }
                }
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Забрать"
        /// </summary>
        private void ButtonTakeTractor_Click(object sender, EventArgs e)
        {
            if (listBoxLevels.SelectedIndex > -1 && maskedTextBox.Text !=
"")
            {
                try
                {
                    var tractor = _campCollection[listBoxLevels.SelectedItem.ToString()] - Convert.ToInt32(maskedTextBox.Text);
                    if (tractor != null)
                    {
                        FormBulldozer form = new FormBulldozer();
                        form.SetTractor(tractor);
                        form.ShowDialog();
                    }
                    logger.Info($"Изъят автомобиль {tractor} с места{ maskedTextBox.Text}");
                    Draw();
                }
                catch (CampNotFoundException ex)
                {
                    MessageBox.Show(ex.Message, "Не найдено",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Неизвестная ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ListBoxParkings_SelectedIndexChanged(object sender,EventArgs e)
        {
            Draw();
            logger.Info($"Перешли на парковку{ listBoxLevels.SelectedItem}");
        }
        /// <summary>
        /// Метод обработки выбора элемента на listBoxLevels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxLevels_SelectedIndexChanged(object sender, EventArgs e) => Draw();
        /// <summary>
        /// Добавление объекта в класс-хранилище
        /// </summary>
        private void AddToParking(Tractor tractor)
        {
            if(_campCollection[listBoxLevels.SelectedItem.ToString()] + tractor)
            {
                Draw();
            }
            else
            {
                MessageBox.Show("Парковка переполнена");
            }            
        }
        /// <summary>
        /// Обработка нажатия кнопки "Добавить автомобиль"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSetCar_Click(object sender, EventArgs e)
        {
            var formCarConfig = new FormBuldozerConfig();
            formCarConfig.AddEvent(AddTractor);
            formCarConfig.Show();
        }
        private void AddTractor(Tractor tractor)
        {
            if (tractor != null && listBoxLevels.SelectedIndex > -1)
            {
                try
                {
                    if((_campCollection[listBoxLevels.SelectedItem.ToString()]) + tractor)
                    {
                        Draw();
                    }
                    else
                    {
                        MessageBox.Show("Машину не удалось поставить");
                    }
                    Draw();
                }
                catch (CampOverflowException ex)
                {
                    MessageBox.Show(ex.Message, "Переполнение",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Неизвестная ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Обработка нажатия пункта меню "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _campCollection.SaveData(saveFileDialog.FileName);
                    MessageBox.Show("Сохранение прошло успешно",
                    "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    logger.Info("Сохранено в файл " +
                    saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name,MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Не сохранилось", "Результат",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            }
        /// <summary>
        /// Обработка нажатия пункта меню "Загрузить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _campCollection.LoadData(openFileDialog.FileName);
                    MessageBox.Show("Загрузили", "Результат",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    logger.Info("Загружено из файла " +
                    openFileDialog.FileName);
                    ReloadLevels();
                    Draw();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Не загрузили", "Результат",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

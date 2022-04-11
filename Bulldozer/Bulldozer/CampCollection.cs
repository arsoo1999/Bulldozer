using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;


namespace Bulldozer
{
    /// <summary>
    /// Класс-коллекция парковок
    /// </summary>
    public class CampCollection
    {
        protected readonly char _separator = ';';
        /// <summary>
        /// Словарь (хранилище) с парковками
        /// </summary>
        readonly Dictionary<string, Camp<IDrawTractor>> _campStages;
        /// <summary>
        /// Возвращение списка названий парковок
        /// </summary>
        public List<string> Keys => _campStages.Keys.ToList();
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private readonly int _pictureWidth;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private readonly int _pictureHeight;
        /// <summary>
        /// Конструктор
        /// </summary>
        public CampCollection(int pictureWidth, int pictureHeight)
        {
            _campStages = new Dictionary<string, Camp<IDrawTractor>>();
            _pictureWidth = pictureWidth;
            _pictureHeight = pictureHeight;
        }
        
        /// Добавление парковки
        /// </summary>
        /// <param name="name">Название парковки</param>
        public void AddParking(string name)
        {
           _campStages.Add(name);
        }
        /// <summary>
        /// Удаление парковки
        /// </summary>
        public void DelParking(string name)
        {
            _campStages.Remove(name);
        }
        /// <summary>
        /// Доступ к парковке
        /// </summary>
        public Camp<IDrawTractor> this[string ind]
        {
            get
            {
                    return _campStages[ind];
            }
        }
        /// <summary>
        /// Метод записи информации в файл
        /// </summary>
        private void WriteToFile(string text, FileStream stream)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(text);
            stream.Write(info, 0, info.Length);
        }
        /// <summary>
        public bool SaveData(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                WriteToFile($"CampCollection{Environment.NewLine}", fs);
                foreach (var level in _campStages)
                {
                    //Начинаем парковку
                    WriteToFile($"Parking{_separator}{level.Key}{Environment.NewLine}", fs);
                    foreach (var tractor in level.Value.GetNext())
                    {
                        //если место не пустое
                        if (tractor != null)
                        {
                            WriteToFile($"{tractor.GetType().Name}{ _separator}{ tractor}{ Environment.NewLine}", fs);
                        }

                    }

                }

            }
            return true;
        }
        /// <summary>
        /// Загрузка нформации по автомобилям на парковках из файла
        /// </summary>
        public bool LoadData(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new Exception("Файл не найден");
            }
            string bufferTextFromFile = "";
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                byte[] b = new byte[fs.Length];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    bufferTextFromFile += temp.GetString(b);
                }
            }
            var strs = bufferTextFromFile.Split(new char[] { '\n', '\r' },
            StringSplitOptions.RemoveEmptyEntries);
            if (!strs[0].Contains("CampCollection"))
            {
                //если нет такой записи, то это не те данные
                throw new Exception("Неверный формат файла");
            }
            //очищаем записи
            _campStages.Clear();
            IDrawTractor tractor = null;
            string key = string.Empty;
            for (int i = 1; i < strs.Length; ++i)
            {
                //идем по считанным записям
                if (strs[i].Contains("Parking"))
                {
                    //начинаем новую парковку
                    key = strs[i].Split(_separator)[1];
                    _campStages.Add(key, new
                    Camp<IDrawTractor>(_pictureWidth, _pictureHeight));
                    continue;
                }
                if (strs[i].Split(_separator)[0] == "Tractor")
                {
                    tractor = new Tractor(strs[i].Split(_separator)[1]);
                }
                else if (strs[i].Split(_separator)[0] == "FarmTractor")
                {
                    tractor = new FarmTractor(strs[i].Split(_separator)[1]);
                }
                var result = _campStages[key] + tractor;
                if (!result)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

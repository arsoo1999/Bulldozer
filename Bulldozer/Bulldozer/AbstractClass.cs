using System;
using System.Collections.Generic;
using System.Text;

namespace Bulldozer
{
    /// <summary>
    /// Класс с общей логикой тестирования объекта
    /// </summary>
    public abstract class AbstractTestObject
    {
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        protected int _pictureWidth;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        protected int _pictureHeight;
        /// <summary>
        /// Объект тестирования
        /// </summary>
        protected IDrawTractor _object;
        /// <summary>
        /// Передача объекта
        /// </summary>
        /// <param name="obj"></param>
        public void Init(IDrawTractor obj)
        {
            _object = obj;
        }
        /// <summary>
        /// Логика установки позиции объекта
        /// </summary>
        public virtual bool SetPosition(int pictureWidth, int pictureHeight)
        {
            if (_object == null)
            {
                return false;
            }
            if (pictureWidth == 0 || pictureHeight == 0)
            {
                return false;
            }
            _object.SetPosition(0, 0, pictureWidth, pictureHeight);
            return true;
        }
        /// <summary>
        /// Тестирование объекта
        /// </summary>
        public abstract string TestObject();
    }
    public class BordersTestObject : AbstractTestObject
    {
        public override string TestObject()
        {
            if (_object == null)
            {
                return "Объект не установлен";
            }
            while (_object.MoveTractor(Direction.Right))
            {
                if (_object.GetCurrentPosition().Right > _pictureWidth)
                {
                    return "Объект вышел за правый край";
                }
            }
            while (_object.MoveTractor(Direction.Down))
            {
                if (_object.GetCurrentPosition().Bottom > _pictureHeight)
                {
                    return "Объект вышел за нижний край";
                }
            }
            while (_object.MoveTractor(Direction.Left))
            {
                if (_object.GetCurrentPosition().Bottom > _pictureHeight)
                {
                    return "Объект вышел за левый край";
                }
            }
            while (_object.MoveTractor(Direction.Up))
            {
                if (_object.GetCurrentPosition().Bottom > _pictureHeight)
                {
                    return "Объект вышел за верхний край";
                }
            }
            return "Тест проверки выхода за границы пройден успешно";
        }
    }
}

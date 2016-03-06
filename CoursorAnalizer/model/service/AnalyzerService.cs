using System;
using System.Drawing;
using CursorAnalyzer.model.domain;

namespace CursorAnalyzer.model.service
{
    class AnalyzerService
    {
        #region Variables
        private readonly Random random = new Random(); //генератор рандомных чисел для генерации позиции и размера фигуры        
        private ParamsCalculationService calculationService;
        public ParamsCalculationService CalculationService
        {
            get { return calculationService; }
            set { calculationService = value; }
        }

        private bool isStarted;  //флаг нажатия кнопки старт
        public bool IsStarted
        {
            get { return isStarted; }
            set { isStarted = value; }
        }

        private int clickCounter; //счетчик кликнутых фигур
        public int ClickCounter
        {
            get { return clickCounter; }
            set { clickCounter = value; }
        }

        private DateTime allWorkingTime; //время теста
        private Shape shape;
        private DateTime previousClickTime; //время предыдущего клика
        private int x, y, size;
        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        #endregion
        
        public AnalyzerService()
        {
            calculationService = new ParamsCalculationService();
            isStarted = false;
            clickCounter = 0;
        }

        public Shape parseInputParams(int mouseX, int mouseY, int shapeSize, int width, int height)
        {
            DateTime currentClickTime = DateTime.Now;
            calculationService.ClickTimeContainer.Add(currentClickTime);

            if (clickCounter == 0)
            {
                allWorkingTime = DateTime.Now;
            }
            else
            {
                ParamsCalculationService.mouseTracksContainer.Add(ParamsCalculationService.MouseTrack);
                ParamsCalculationService.RefreshList(ParamsCalculationService.MouseTrack);
            }

            if (!isStarted && (clickCounter == 0) || 
                ((mouseX - x <= shapeSize) && (mouseY - y <= shapeSize) && 
                (mouseX - x >= 0) && (mouseY - y >= 0)))     //проверка, начат ли тест
            {
                previousClickTime = new DateTime(currentClickTime.Ticks - previousClickTime.Ticks);
                ParamsCalculationService.Sec.Add(previousClickTime);
                previousClickTime = currentClickTime;

                while (true)
                {
                    var oldX = x;
                    var oldY = y;

                    size = random.Next(180) + 20;
                    x = random.Next(width - size);
                    y = random.Next(height - size);

                    var distance = Math.Sqrt(Math.Pow(oldX - x, 2) + Math.Pow(oldY - y, 2));

                    if (distance >= 128) break;
                }

                
                isStarted = true;
                ParamsCalculationService.SaverParam(w, x, y, clickCounter, ParamsCalculationService.Sec[ParamsCalculationService.Sec.Count - 1]);
                clickCounter++;
                return new Shape(x, y, size, clickCounter);
            }
            return null;
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using CursorAnalyzer.model.domain;
using CursorAnalyzer.model.repository;

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
        private DateTime previousClickTime; //время предыдущего клика
        private DateTime currentClickTime;
        private int x, y, size;
        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        private bool isReg = false;
        public bool IsReg
        {
            get { return isReg; }
            set { isReg = value; }
        }

        private UserRepository userRepository;

        #endregion
        
        public AnalyzerService()
        {
            calculationService = new ParamsCalculationService();
            userRepository = new UserRepository("DataBase.xml");
            isStarted = false;
            clickCounter = 0;
        }

        public Shape parseInputParams(int mouseX, int mouseY, int shapeSize, int width, int height)
        {
            currentClickTime = DateTime.Now;
            calculationService.ClickTimeContainer.Add(currentClickTime);

            if (clickCounter == 0)
            {
                allWorkingTime = DateTime.Now;
            }
            else
            {
                calculationService.MouseTracksContainer.Add(calculationService.MouseTrack);
                calculationService.RefreshList(calculationService.MouseTrack);
            }

            if (!isStarted && (clickCounter == 0) || 
                ((mouseX - x <= shapeSize) && (mouseY - y <= shapeSize) && 
                (mouseX - x >= 0) && (mouseY - y >= 0)))     //проверка, начат ли тест
            {
                previousClickTime = new DateTime(currentClickTime.Ticks - previousClickTime.Ticks);
                calculationService.ClickTimeContainer.Add(previousClickTime);
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
                calculationService.SaverParam(size, x, y, clickCounter, 
                    calculationService.ClickTimeContainer[calculationService.ClickTimeContainer.Count - 1]);
                clickCounter++;
                return new Shape(x, y, size, clickCounter);
            }
            return null;
        }

        public void stopTest()
        {
            if (ClickCounter > 1)
            {
                allWorkingTime = new DateTime(currentClickTime.Ticks - allWorkingTime.Ticks);
                calculationService.MidV(allWorkingTime, clickCounter);
            }
            else calculationService.MidV(currentClickTime, clickCounter);

            calculationService.MathExpectation(clickCounter);
            calculationService.Variance(clickCounter);
            Saver.SaveXML(Name, ParamsCalculationService.Cmid, ParamsCalculationService.Cmax, ParamsCalculationService.T, ParamsCalculationService.ampList, ParamsCalculationService.V, ParamsCalculationService.energyList);
            //Saver.SaveTXT(Name, ParamsCalculationService.Cmid, ParamsCalculationService.Cmax, ParamsCalculationService.T, ParamsCalculationService.ampList, ParamsCalculationService.energyList);
            clickCounter = 0;
            isReg = false;
            isStarted = false;
        }

        public bool Registrate(string name)
        {
            if (!isReg)
            {
                var users = userRepository.FetchAllUsers();

                if (users.Contains(name)) isReg = false;

                ParamsCalculationService.Refresher();
                isReg = true;
                return isReg;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using CursorAnalyzer.model.domain;
using CursorAnalyzer.model.repository;

namespace CursorAnalyzer.model.service
{
    /// <summary>
    /// Service for parsing and saving params
    /// </summary>
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

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private UserRepository userRepository;

        #endregion
        
        public AnalyzerService()
        {
            userRepository = new UserRepository("DataBase.xml");
            calculationService = new ParamsCalculationService();
            isStarted = false;
            clickCounter = 0;
        }

        /// <summary>
        /// Method for parse mouse metrics and time betveen clicks 
        /// and save params to class-container
        /// </summary>
        /// <param name="mouseX">X-coord of mouse</param>
        /// <param name="mouseY">Y-coord of mouse</param>
        /// <param name="shapeSize">Size of square side</param>
        /// <param name="width">Picture box width</param>
        /// <param name="height">Picture box height</param>
        /// <param name="isStore">Flag for check authorization</param>
        /// <returns>POJO class with data of new square and click count</returns>
        public Shape ParseInputParams(int mouseX, int mouseY, int shapeSize, int width, int height, bool isStore)
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
                calculationService.MouseTrack = new List<Point>();
            }

            //проверка, начат ли тест
            if ((!IsStarted && clickCounter == 0) || (!isStore && clickCounter == 0))
            {
                isStarted = true;
                x = 200;
                y = 200;
                size = 200;
                clickCounter++;
                return new Shape(x, y, size, clickCounter);          
            }
            if (((mouseX - x > shapeSize) || (mouseY - y > shapeSize) ||
                 (mouseX - x < 0) || (mouseY - y < 0)))
            {
                return null;
            }
          
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

            calculationService.SaveParam(size, x, y, clickCounter, 
                calculationService.ClickTimeContainer[calculationService.ClickTimeContainer.Count - 1]);
            clickCounter++;
            return new Shape(x, y, size, clickCounter);
        }

        /// <summary>
        /// Method for saving result data in db
        /// </summary>
        /// <param name="isStore">Flag for saving data in db or find user</param>
        public void StopTest(bool isStore)
        {
            if (ClickCounter > 1)
            {
                allWorkingTime = new DateTime(currentClickTime.Ticks - allWorkingTime.Ticks);
                calculationService.MidV(allWorkingTime, clickCounter);
            }
            else calculationService.MidV(currentClickTime, clickCounter);

            calculationService.MathExpectation(clickCounter);
            calculationService.Variance(clickCounter);
            if (isStore)
            {
                MetricsRepository.SaveMouseParamsAndMetrics(UserName, calculationService.MidDiffTracks, 
                    calculationService.MaxDiffTracks, calculationService.T, calculationService.AmpContainer, 
                    calculationService.MouseSpeed, calculationService.EnergyContainer, calculationService.LensContainer);    
            }
            clickCounter = 0;
            isReg = false;
            isStarted = false;
            calculationService = new ParamsCalculationService();
        }

        /// <summary>
        /// Check username on existing in DB
        /// </summary>
        /// <param name="name">Username</param>
        /// <returns>True if username is not contains in db, else false</returns>
        public bool Registrate(string name)
        {
            if (isReg) return isReg;
            var users = userRepository.FetchAllUsers();

            if (users.Contains(name)) isReg = false;
            else
            {
                UserName = name;
                isReg = true;
            }
            return isReg;
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace CoursorAnalizer
{
    public partial class Analizer : Form
    {
        #region Var

        private Random random = new Random(); //генератор рандомных чисел для генерации позиции и размера фигуры
        private int x, y, w;  //координаты кнопки и её размер
        private Bitmap bitmap;   //место отрисовки интерфейса
        private Graphics g;  //обект графики
        private bool isStarted = false;  //флаг нажатия кнопки старт
        private int counter = 0; //счетчик кликнутых фигур
        private DateTime currentClickTime; //время с начала клика
        private DateTime previousClickTime; //время предыдущего клика
        private DateTime allWorkingTime; //время теста
        private string Name;
        private bool isReg = false;
    
        #endregion

        #region Ctor

        public Analizer()
        {
            InitializeComponent();
        }
        #endregion

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!isReg)
            {
                MessageBox.Show("Registred please!", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            currentClickTime = DateTime.Now;
            ParamsCalculationService.timeList.Add(currentClickTime);

            if (counter == 0)
            {
                allWorkingTime = DateTime.Now;
            }
            else if (counter > 0)
            {
                ParamsCalculationService.mouseTracksContainer.Add(ParamsCalculationService.MouseTrack);
                ParamsCalculationService.RefreshList(ParamsCalculationService.MouseTrack);
            }

            if (!isStarted && (counter == 0)||((e.X - x <= w) && (e.Y - y <= w) && (e.X - x >= 0) && (e.Y - y >= 0)))     //проверка, начат ли тест
            {
                counterLbl.Text = (counter).ToString();
                previousClickTime = new DateTime(currentClickTime.Ticks - previousClickTime.Ticks);
                ParamsCalculationService.Sec.Add(previousClickTime);
                previousClickTime = currentClickTime;

                bitmap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);   
                g = Graphics.FromImage(bitmap);

                while (true)
                {
                    var oldX = x;
                    var oldY = y;

                    w = random.Next(180) + 20;
                    x = random.Next(pictureBox1.Size.Width - w);
                    y = random.Next(pictureBox1.Size.Height - w);
                    
                    var distance = Math.Sqrt(Math.Pow(oldX - x, 2) + Math.Pow(oldY - y, 2));
                    
                    if (distance >= 128) break;
                }

                g.FillRectangle(new SolidBrush(Color.BlueViolet), x, y, w, w);
                pictureBox1.Image = bitmap;
                isStarted = true;
                ParamsCalculationService.SaverParam(w, x, y, counter, ParamsCalculationService.Sec[ParamsCalculationService.Sec.Count - 1]);
                counter++;
            }
          
        }

        private void Analizer_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);   //инициализация параметров          
            g = Graphics.FromImage(bitmap);
            g.DrawString("START", new Font("Consolas", 20), new SolidBrush(Color.Black), pictureBox1.Width / 2, pictureBox1.Height / 2);
            pictureBox1.Image = bitmap; //закидываем её в pictureBox
        }

        private void STOPBaton_Click(object sender, EventArgs e)
        {
            if (isStarted)
            {
                NameLbl.Text = Name + " finished!";
                bitmap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);   //инициализация параметров          
                g = Graphics.FromImage(bitmap);
                g.DrawString("START", new Font("Consolas", 20), new SolidBrush(Color.Black), pictureBox1.Width / 2, pictureBox1.Height / 2);
                pictureBox1.Image = bitmap; //закидываем её в pictureBox

                if (counter > 1)
                {
                    allWorkingTime = new DateTime(currentClickTime.Ticks - allWorkingTime.Ticks);
                    ParamsCalculationService.MidV(allWorkingTime, counter);
                }
                else ParamsCalculationService.MidV(currentClickTime, counter);

                ParamsCalculationService.MathExpectation(counter);
                ParamsCalculationService.Variance(counter);
                Saver.SaveXML(Name, ParamsCalculationService.Cmid, ParamsCalculationService.Cmax, ParamsCalculationService.T, ParamsCalculationService.ampList, ParamsCalculationService.V, ParamsCalculationService.energyList);
                outTextBox.Text = Saver.SaveTXT(Name, ParamsCalculationService.mCmid, ParamsCalculationService.mCmax, ParamsCalculationService.mT, ParamsCalculationService.dCmid, ParamsCalculationService.dCmax, ParamsCalculationService.dT, ParamsCalculationService.ampExpiration, ParamsCalculationService.ampD, ParamsCalculationService.allAmp);
                Saver.SaveTXT(Name, ParamsCalculationService.Cmid, ParamsCalculationService.Cmax, ParamsCalculationService.T, ParamsCalculationService.ampList, ParamsCalculationService.energyList);
                counter = 0;
                isReg = false;
                isStarted = false;
            }
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isStarted)
                ParamsCalculationService.Trecker(e);
        }

        private void RegBtn_Click(object sender, EventArgs e)
        {
            if (!isReg)
            {
                Name = nameTextBox.Text;
                ParamsCalculationService.ReadBase();

                if (ParamsCalculationService.users.Contains(Name))
                {
                    MessageBox.Show("This name alredy exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);                   
                    return;
                }

                counterLbl.Text = "0";
                ParamsCalculationService.Refresher();
                isReg = true;
                nameTextBox.Text = "";
                NameLbl.Text = Name + " in action!";
            }
            
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

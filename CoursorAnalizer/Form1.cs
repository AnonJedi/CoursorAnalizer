using System;
using System.Drawing;
using System.Windows.Forms;
using CursorAnalyzer.model.domain;
using CursorAnalyzer.model.service;

namespace CursorAnalyzer
{
    public partial class Analyzer : Form
    {
        #region Var

        private int x, y, w;  //координаты кнопки и её размер
        private Bitmap bitmap;   //место отрисовки интерфейса
        private Graphics g;  //обект графики
        private DateTime currentClickTime; //время с начала клика
        private string Name;
        private bool isReg = false;
        private AnalyzerService analyzerService;
    
        #endregion

        #region Ctor

        public Analyzer()
        {
            InitializeComponent();
            analyzerService = new AnalyzerService();
        }
        #endregion

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!isReg)
            {
                MessageBox.Show("Registred please!", "Caution!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                counterLbl.Text = (analyzerService.ClickCounter).ToString();
                Shape newShape = analyzerService.parseInputParams(e.X, e.Y, analyzerService.Size, 
                    pictureBox1.Width, pictureBox1.Height);
                if (newShape == null) return;
                bitmap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
                g = Graphics.FromImage(bitmap);
                g.FillRectangle(new SolidBrush(Color.BlueViolet), newShape.X, newShape.Y, newShape.Size, newShape.Size);
                pictureBox1.Image = bitmap;
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
            if (analyzerService.IsStarted)
            {
                NameLbl.Text = Name + " finished!";
                bitmap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);   //инициализация параметров          
                g = Graphics.FromImage(bitmap);
                g.DrawString("START", new Font("Consolas", 20), new SolidBrush(Color.Black), pictureBox1.Width / 2, pictureBox1.Height / 2);
                pictureBox1.Image = bitmap; //закидываем её в pictureBox

                if (analyzerService.ClickCounter > 1)
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
            if (analyzerService.IsStarted)
                analyzerService.CalculationService.Trecker(e);
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

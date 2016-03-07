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
        private string name;
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
            if (!analyzerService.IsReg)
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
            bitmap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);          
            g = Graphics.FromImage(bitmap);
            g.DrawString("START", new Font("Consolas", 20), new SolidBrush(Color.Black), pictureBox1.Width / 2, pictureBox1.Height / 2);
            pictureBox1.Image = bitmap; 
        }

        private void STOPBtn_Click(object sender, EventArgs e)
        {
            if (analyzerService.IsStarted)
            {
                NameLbl.Text = name + " finished!";
                bitmap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);   //инициализация параметров          
                g = Graphics.FromImage(bitmap);
                g.DrawString("START", new Font("Consolas", 20), new SolidBrush(Color.Black), pictureBox1.Width / 2, pictureBox1.Height / 2);
                pictureBox1.Image = bitmap;

                analyzerService.stopTest();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (analyzerService.IsStarted)
                analyzerService.CalculationService.Trecker(e);
        }

        private void RegBtn_Click(object sender, EventArgs e)
        {
            name = nameTextBox.Text;
            if (analyzerService.Registrate(name))
            {
                counterLbl.Text = "0";
                NameLbl.Text = name + " in action!";
                nameTextBox.Text = "";   
            }
            else MessageBox.Show("This name alredy exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

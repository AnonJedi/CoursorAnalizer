using System;
using System.Drawing;
using System.Windows.Forms;

namespace CoursorAnalizer
{
    public partial class Analizer : Form
    {
        #region Var

        private Random rand = new Random(); //рандомчик для координат кнопки
        private int x=0, y=0, w=0;  //координаты кнопки и её размеры
        private Bitmap bitmap;   //холст
        private Graphics g;  //переменная для рисования
        private bool isStarted = false;  //проверка на нажатие кнопки старт
        private int counter = 0;//счетчик
        private DateTime timer;//время начала цикла
        private DateTime oldtime=new DateTime();//время предыдущего цикла
        private DateTime Time;//время работы программы
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

            timer = DateTime.Now;
            if (counter == 0)
            {
                Time = DateTime.Now;
            }
            else if (counter > 0)
            {
                Vector.CordList.Add(Vector.Glist);
                Vector.RefreshList(Vector.Glist);
            }

            if (!isStarted && (counter == 0)||((e.X - x <= w) && (e.Y - y <= w) && (e.X - x >= 0) && (e.Y - y >= 0)))     //проверка, начат ли тест
            {
                oldtime = new DateTime(timer.Ticks - oldtime.Ticks);
                Vector.Sec.Add(oldtime);
                oldtime = timer;

                bitmap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);   
                g = Graphics.FromImage(bitmap);

                w = rand.Next(180) + 20;
                x = rand.Next(pictureBox1.Size.Width - w);
                y = rand.Next(pictureBox1.Size.Height - w);

                g.FillRectangle(new SolidBrush(Color.BlueViolet), x, y, w, w);
                pictureBox1.Image = bitmap;
                isStarted = true;
                Vector.SaverParam(w, x, y, counter);
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
                    Time = new DateTime(timer.Ticks - Time.Ticks);
                    Vector.MidV(Time, counter);
                }
                else Vector.MidV(timer, counter);

                Vector.MathExpectation(counter);
                Vector.Variance(counter);
                Saver.SaveXML(Name, Vector.Cmid, Vector.Cmax, Vector.T, Vector.ampList, Vector.Len);
                Saver.SaveTXT(Name, Vector.mCmid, Vector.mCmax, Vector.mT, Vector.dCmid, Vector.dCmax, Vector.dT);
                counter = 0;
                isReg = false;
                isStarted = false;
            }
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isStarted)
                Vector.Trecker(e);
        }

        private void RegBtn_Click(object sender, EventArgs e)
        {
            if (!isReg)
            {
                Name = nameTextBox.Text;
                Vector.ReadBase();

                if (Vector.users.Contains(Name))
                {
                    MessageBox.Show("This name alredy exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);                   
                    return;
                }

                Vector.Refresher();
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

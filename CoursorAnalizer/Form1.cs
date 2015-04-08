using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CoursorAnalizer
{
    public partial class Analizer : Form
    {
        #region Var
        public Random rand = new Random(); //рандомчик для координат кнопки
        public int x=0, y=0, w=0;  //координаты кнопки и её размеры
        public Bitmap bitmap;   //холст
        public Graphics g;  //переменная для рисования
        public bool isStarted = false;  //проверка на нажатие кнопки старт
        public int Counter = 0;//счетчик
        public DateTime timer;//время начала цикла
        public DateTime Oldtime=new DateTime();//время предыдущего цикла
        public DateTime Time;//время работы программы
       
    
        #endregion

        #region Ctor

        public Analizer()
        {
         
            InitializeComponent();
        }
        #endregion


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            timer = DateTime.Now;
            if (Counter == 0)
            {
                Time=DateTime.Now;
            }


            if (!isStarted && (Counter == 0)||((e.X - x <= w) && (e.Y - y <= w) && (e.X - x >= 0) && (e.Y - y >= 0)))     //проверка, начат ли тест
            {
                Oldtime = new DateTime(timer.Ticks - Oldtime.Ticks);
                outTextBox.Text += Oldtime.Second + ":" + Oldtime.Millisecond + "\r\n";
                Vector.Sec.Add(Oldtime);
                Oldtime = timer;
                bitmap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);   //тот же порядок, что и в кнопке start
                g = Graphics.FromImage(bitmap);

                w = rand.Next(180)+20;
                x = rand.Next(pictureBox1.Size.Width - w);
                y = rand.Next(pictureBox1.Size.Height - w);

                g.FillRectangle(new SolidBrush(Color.BlueViolet), x, y, w,w);
                pictureBox1.Image = bitmap;
                isStarted = true;
                Vector.SaverParam(w,x,y,Counter);
                Counter++;

               

            }
          
        }

        private void Analizer_Load(object sender, EventArgs e)
        {

            bitmap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);   //инициализация параметров          
            g = Graphics.FromImage(bitmap);
            g.DrawString("START", new Font("Microsoft Sans Serif",20), new SolidBrush(Color.Black), pictureBox1.Width / 2, pictureBox1.Height / 2);
            pictureBox1.Image = bitmap; //закидываем её в pictureBox
            Vector.SaverParam(w,x,y,Counter);
        }

        private void STOPBaton_Click(object sender, EventArgs e)
        {
            isStarted = false;

            bitmap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);   //инициализация параметров          
            g = Graphics.FromImage(bitmap);
            g.DrawString("FINISH", new Font("Microsoft Sans Serif", 20), new SolidBrush(Color.Black), pictureBox1.Width / 2, pictureBox1.Height / 2);
            pictureBox1.Image = bitmap; //закидываем её в pictureBox
            Time = new DateTime(timer.Ticks - Time.Ticks);
            Vector.MidV(Time, Counter);
            outTextBox.Text += Time.Second + ":" + Time.Millisecond + "\r\n";
            Vector.TimeCursor(Counter,outTextBox);
            Counter = 0;
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Vector.Trecker(e);
        }


    }
}

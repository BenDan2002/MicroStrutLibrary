//using System;
//using System.Drawing;

//namespace MicroStrutLibrary.Infrastructure.Core
//{
//    /// <summary>
//    /// 验证码
//    /// </summary>
//    public class Captcha : IDisposable
//    {
//        public int Width { get; }

//        public int Height { get; }

//        public int Length { get; }

//        public string Code { get; protected set; }

//        public Image Image { get; protected set; }

//        public Captcha() : this(90, 30, 4)
//        {
//        }

//        public Captcha(int width, int height) : this(width, height, 4)
//        {
//        }

//        public Captcha(int width, int height, int length)
//        {
//            this.Width = width;
//            this.Height = height;
//            this.Length = length;

//            //Create
//            this.CreateCode();
//            this.CreateImage();
//        }

//        /// <summary>
//        /// 生成随机码
//        /// </summary>
//        protected virtual void CreateCode()
//        {
//            int rand;
//            char code;
//            Random random = new Random();

//            this.Code = string.Empty;

//            for (int i = 0; i < this.Length; i++)
//            {
//                rand = random.Next();

//                if (rand % 3 == 0)
//                {
//                    code = (char)('A' + (char)(rand % 26));
//                }
//                else
//                {
//                    code = (char)('0' + (char)(rand % 10));
//                }

//                this.Code += code.ToString();
//            }
//        }

//        ///  <summary>
//        ///  生成随机码图片
//        ///  </summary>
//        protected virtual void CreateImage()
//        {
//            if (string.IsNullOrWhiteSpace(this.Code))
//            {
//                throw new ArgumentNullException(nameof(Code));
//            }

//            int randAngle = 45; //随机转动角度

//            Bitmap image = new Bitmap(this.Width, this.Height);//创建图片背景

//            using (Graphics graph = Graphics.FromImage(image))
//            {
//                graph.Clear(Color.AliceBlue);//清除画面，填充背景

//                Random rand = new Random();

//                //背景噪点生成
//                Pen blackPen = new Pen(Color.LightGray, 0);

//                for (int i = 0; i < 50; i++)
//                {
//                    int x = rand.Next(0, image.Width);
//                    int y = rand.Next(0, image.Height);

//                    graph.DrawRectangle(blackPen, x, y, 1, 1);
//                }


//                //验证码旋转，防止机器识别
//                char[] chars = this.Code.ToCharArray(); //拆散字符串成单字符数组

//                //文字距中
//                StringFormat format = new StringFormat(StringFormatFlags.NoClip);

//                format.Alignment = StringAlignment.Center;
//                format.LineAlignment = StringAlignment.Center;

//                //定义字体
//                string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial" };

//                //定义颜色
//                Color[] colors = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

//                for (int i = 0; i < chars.Length; i++)
//                {
//                    int cindex = rand.Next(7);
//                    int findex = rand.Next(4);

//                    Font f = new Font(fonts[findex], 16, FontStyle.Bold);
//                    Brush b = new SolidBrush(colors[cindex]);

//                    Point dot = new Point(this.Width / (this.Length + 1), (this.Height / 2));
//                    float angle = rand.Next(-randAngle, randAngle);//转动的度数

//                    graph.TranslateTransform(dot.X, dot.Y);//移动光标到指定位置
//                    graph.RotateTransform(angle);
//                    graph.DrawString(chars[i].ToString(), f, b, 1, 1, format);
//                    graph.RotateTransform(-angle);//转回去
//                    graph.TranslateTransform(2, -dot.Y);//移动光标到指定位置
//                }
//            }

//            this.Image = image;
//        }

//        public void Dispose()
//        {
//            if (this.Image != null)
//            {
//                this.Image.Dispose();
//            }
//        }
//    }
//}

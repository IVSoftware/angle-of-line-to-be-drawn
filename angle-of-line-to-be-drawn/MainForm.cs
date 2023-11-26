using System.Drawing.Drawing2D;

namespace angle_of_line_to_be_drawn
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            pictureBox.Paint += (sender, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                foreach (var shape in Document)
                {
                    if(shape is Line line)
                    {
                        using (var pen = new Pen(line.Color, line.Width))
                        {
                            e.Graphics.DrawLine(pen, line.Origin, line.EndPoint);
                        }
                    }
                }
            };
            // Do it again, with delay.
            pictureBox.Click += async (sender, e) =>
            {
                Document.Clear();
                pictureBox.Invalidate();
                await Task.Delay(TimeSpan.FromMilliseconds(500));
                for (double angle = 0; angle < 360; angle += 15)
                {
                    Document.Add(new Line
                    {
                        Origin = pictureBox.GetCenterPoint(),
                        Angle = angle,
                        Length = 150,
                        Color = angle.ConvertHueToRGB(),
                    });
                    pictureBox.Invalidate();
                    await Task.Delay(TimeSpan.FromMilliseconds(100));
                }
            };
            for (double angle = 0; angle<360; angle += 15)
            {
                Document.Add(new Line
                {
                    Origin = pictureBox.GetCenterPoint(),
                    Angle = angle,
                    Length = 150,
                    Color = angle.ConvertHueToRGB(),
                });
            }
        }
        List<object> Document { get; } = new List<object>();
    }
    class Line
    {
        public Point Origin { get; set; }
        public double Angle { get; set; }
        public double Length { get; set; }
        public Point EndPoint
        {
            get
            {
                double radians = Angle * Math.PI / 180;
                int endX = (int)(Origin.X + Length * Math.Cos(radians));
                int endY = (int)(Origin.Y + Length * Math.Sin(radians));
                return new Point(endX, endY);
            }
        }
        public Color Color { get; set; } = Color.Black;
        public float Width { get; set; } = 2f;
    }
    static partial class Extensions
    {
        public static Point GetCenterPoint(this Control control) =>
            new Point(
                control.ClientRectangle.Left + control.ClientRectangle.Width / 2,
                control.ClientRectangle.Top + control.ClientRectangle.Height / 2);
        public static Color ConvertHueToRGB(this double hue, double saturation = 1.0, double value = 1.0)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);
            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));
            if (hi == 0) return Color.FromArgb(255, v, t, p);
            else if (hi == 1) return Color.FromArgb(255, q, v, p);
            else if (hi == 2) return Color.FromArgb(255, p, v, t);
            else if (hi == 3) return Color.FromArgb(255, p, q, v);
            else if (hi == 4) return Color.FromArgb(255, t, p, v);
            return Color.FromArgb(255, v, p, q);
        }
    }
}

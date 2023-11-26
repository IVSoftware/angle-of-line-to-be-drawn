namespace angle_of_line_to_be_drawn
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            buttonAddLine.Click += (sender, e) =>
            {
                pictureBox.Refresh();
            };
            pictureBox.Paint += (sender, e) =>
            {
                foreach(var shape in Document)
                {
                    if(shape is Line line)
                    {
                        Point endPoint = line.CalculateEndPoint();
                        e.Graphics.DrawLine(Pens.Black, line.Origin, endPoint);
                    }
                }
            };
            for (double angle = 0; angle<=360; angle += 15)
            {
                Document.Add(new Line
                {
                    Origin = pictureBox.GetCenterPoint(),
                    Angle = angle,
                    Length = 100,
                });
            }
        }
        int _count = 0;
        List<object> Document { get; } = new List<object>();
    }
    class Line 
    {
        public Point Origin { get; set; }
        public double Angle { get; set; }
        public double Length { get; set; }

        public Point CalculateEndPoint()
        {
            double radians = Angle * Math.PI / 180;
            int endX = (int)(Origin.X + Length * Math.Cos(radians));
            int endY = (int)(Origin.Y + Length * Math.Sin(radians));
            return new Point(endX, endY);
        }
    }
    static partial class Extensions
    {
        public static Point GetCenterPoint(this Control control) =>
            new Point(
                control.ClientRectangle.Left + control.ClientRectangle.Width / 2,
                control.ClientRectangle.Top + control.ClientRectangle.Height / 2);
    }
}

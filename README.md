# Angle Of Line to be Drawn



This strikes me as a bit of an [X-Y Problem](https://meta.stackexchange.com/a/66378) because even if you were to succeed in injecting an angle into a custom `PaintEventArgs` class a line and draw it, it's likely to be erased on the next refresh unless you're persisting it in a document of some kind. To draw on any control, call its `Refresh()` method and it will respond by firing the `Paint` message and providing a Graphics _blank_ canvas to draw on.

Consider making a `Line` class with the `Angle` property that goes in a `List<object>` that can hold the various shapes you want to draw. You can continue to add more lines to the document, then call `Refresh` whenever there is a new shape to draw.

[![lines with angles][1]][1]
```
public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        pictureBox.Paint += (sender, e) =>
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            foreach (var shape in Document)
            {
                if(shape is Line line)
                {
                    Point endPoint = line.EndPoint;
                    using (var pen = new Pen(line.Color, line.Width))
                    {
                        e.Graphics.DrawLine(pen, line.Origin, endPoint);
                    }
                }
            }
        };
        for (double angle = 0; angle<360; angle += 15)
        {
            Document.Add(new Line
            {
                Origin = pictureBox.GetCenterPoint(),
                Angle = angle,
                Length = 150,
                Color = angle.ColorFromHSV()
            }); ;
        }
    }
    List<object> Document { get; } = new List<object>();
}
```

___

```
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
```

___
**Extensions**

```
static partial class Extensions
{
    public static Point GetCenterPoint(this Control control) =>
        new Point(
            control.ClientRectangle.Left + control.ClientRectangle.Width / 2,
            control.ClientRectangle.Top + control.ClientRectangle.Height / 2);
    public static Color ColorFromHSV(this double hue)
    {
        return Color.FromArgb(255, ConvertHueToRGB(hue, 1.0, 1.0));
    }
    private static Color ConvertHueToRGB(double hue, double saturation, double value)
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
```


  [1]: https://i.stack.imgur.com/HY1Dd.png
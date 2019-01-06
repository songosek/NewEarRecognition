using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace EarRecognition
{
    public class Person
    {
        public string name { get; set; }
        public string surname { get; set; }
        public int earHeight { get; set; }
        public int earWidth { get; set; }
        public int darkPixelsCount { get; set; }

        public void CalculateDarkPixelsCount(Bitmap bitmap, System.Windows.Point leftPoint, System.Windows.Point topPoint)
        {
            double fraction = 0.1;
            Rectangle ROI = new Rectangle(
                (int)leftPoint.X + (int)(earWidth * fraction), (int)topPoint.Y + (int)(earHeight * fraction), 
                earWidth - (int)(earWidth * fraction), earHeight - (int)(earHeight * fraction));
            for(int i = 0; i < bitmap.Width; i++)
            {
                for(int j = 0; j < bitmap.Height; j++)
                {
                    if(i > ROI.X && i < ROI.X + ROI.Width
                        && j > ROI.Y && j < ROI.Y + ROI.Height)
                    {
                        Color pixel = bitmap.GetPixel(i, j);
                        if (pixel.R + pixel.G + pixel.B < 350)
                            darkPixelsCount++;
                    }
                }
            }
        }
        
    }

    public class People
    {
        public List<Person> people;
    }
}

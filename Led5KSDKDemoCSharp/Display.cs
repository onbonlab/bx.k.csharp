using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
namespace Led5KSDKDemoCSharp
{
    public class Display
    {
        public enum ColorType
        {
            SINGLE,
            DOUBLE,
            THREE,
            TRUECOLOR
        };

        public enum MatrixType
        {
            RG,
            RGGR
        }

        public bool Horizontal { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Font DisplayFont { get; set; }

        public Color ForeColor { get; set; }

        public IList<string> Split(string content, int engFontWidth = 8)
        {
            List<string> result = new List<string>();

            int start = 0;
            int length = 1;
            int width = 0;
            while ((start + length) <= content.Length)
            {
                width += System.Text.Encoding.Default.GetBytes(content.Substring(start, 1)).Length == 1 ? engFontWidth : engFontWidth * 2;
                string text = content.Substring(start, length);
                if (width > Width)
                {
                    width = 0;
                    if (length == 1)
                    {
                        result.Add(text);
                        start++;
                    }
                    else
                    {
                        result.Add(content.Substring(start, length - 1));
                        start += (length - 1);
                        length = 1;
                    }
                }
                else
                {
                    length++;
                }
            }
            if (start < content.Length)
            {
                result.Add(content.Substring(start, length == 1 ? 1 : length - 1));
            }

            return result;
        }

        public int CalculateEngCharCount(string content)
        {
            int count = 0;
            for (int i = 0; i < content.Length; i++)
            {
                count += System.Text.Encoding.Default.GetBytes(content.Substring(i, 1)).Length > 1 ? 2 : 1;
            }
            return count;
        }

        public IList<string> SplitByGraphic(string content)
        {
            List<string> result = new List<string>();

            Bitmap textBitmap = new Bitmap(Width, Height);

            Graphics textG = Graphics.FromImage(textBitmap);
            textG.FillRectangle(new SolidBrush(Color.Black), 0, 0, textBitmap.Width, textBitmap.Height);

            Font textFont = DisplayFont;

            int start = 0;
            int length = 1;
            while ((start + length) <= content.Length)
            {
                string text = content.Substring(start, length);
                SizeF textSize = textG.MeasureString(text, textFont);
                if (textSize.Width > Width)
                {
                    if (length == 1)
                    {
                        result.Add(text);
                        start++;
                    }
                    else
                    {
                        result.Add(content.Substring(start, length - 1));
                        start += (length - 1);
                        length = 1;
                    }
                }
                else
                {
                    length++;
                }
            }
            result.Add(content.Substring(start, length - 1));
            return result;
        }

        /// <summary>
        /// 总的像素bit位点阵，一定是8的倍数
        /// </summary>
        /// <param name="content">每页的字模数据</param>
        /// <param name="colorType"></param>
        /// <param name="matrixType"></param>
        /// <returns></returns>
        public List<byte> ToBytes(Bitmap content, ColorType colorType, MatrixType matrixType)
        {
            List<byte> result = new List<byte>();

            int height = content.Height;
            int width = content.Width;

            if (colorType == ColorType.SINGLE)         // 单色
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x += 8)
                    {
                        byte bits = 0x00;
                        for (int i = 0; i < 8; i++)
                        {
                            Color rgb = content.GetPixel(x + i, y);
                            int r = rgb.R;
                            int g = rgb.G;
                            bool flag = r >= 127 | g >= 127;
                            bits += (byte)((flag ? 0 : 1) << (7 - i));
                        }
                        result.Add(bits);
                    }
                }
            }
            else if (colorType == ColorType.DOUBLE)    // 双色
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x += 8)
                    {
                        byte Rbits = 0x00;
                        byte Gbits = 0x00;
                        for (int i = 0; i < 8; i++)
                        {
                            Color rgb = content.GetPixel(x + i, y);
                            int r = rgb.R;
                            int g = rgb.G;
                            Rbits += (byte)((r >= 127 ? 0 : 1) << (7 - i));
                            Gbits += (byte)((g >= 127 ? 0 : 1) << (7 - i));
                        }
                        if (matrixType == MatrixType.RG)//r+g
                        {
                            result.Add(Rbits);
                            result.Add(Gbits);
                        }
                        else//g+r,r+g的屏幕反过来就是g+r
                        {
                            result.Add(Gbits);
                            result.Add(Rbits);
                        }
                    }
                }
            }
            else if (colorType == ColorType.THREE)    // 双色
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x += 8)
                    {
                        byte Rbits = 0x00;
                        byte Gbits = 0x00;
                        byte Bbits = 0x00;
                        for (int i = 0; i < 8; i++)
                        {
                            Color rgb = content.GetPixel(x + i, y);
                            int r = rgb.R;
                            int g = rgb.G;
                            int b = rgb.B;
                            Rbits += (byte)((r >= 127 ? 0 : 1) << (7 - i));
                            Gbits += (byte)((g >= 127 ? 0 : 1) << (7 - i));
                            Bbits += (byte)((b >= 127 ? 0 : 1) << (7 - i));
                        }
                        result.Add(Rbits);
                        result.Add(Gbits);
                        result.Add(Bbits);
                    }
                }
            }
            else   //全彩
            {
                for (int y = 0; y < height; y++)//遍历每列
                {
                    for (int x = 0; x < width; x++)//遍历每行
                    {
                        Color rgb = content.GetPixel(x, y);
                        int r = rgb.R;//红色像素值
                        int g = rgb.G;//绿色像素值
                        int b = rgb.B;//蓝色像素值

                        //按照协议，RGB565模式对像素值编码，红色取高5位，绿色取高6位，蓝色取高5位，低位舍弃,从而一个像素需要用两个字节表示
                        //int data1 = ((r >> 3) << 11) & 0xF800;//红色
                        //int data2 = ((g >> 2) << 5) & 0x07E0;//绿色
                        //int data3 = (b >> 3) & 0x001F;//蓝色
                        //byte[] dbytes = System.BitConverter.GetBytes(data1 + data2 + data3);

                        int val = rgb.R >> 8 << 11;
                        val |= rgb.G >> 2 << 5;
                        val |= rgb.B >> 8;

                        // 获取RGB单色，并填充低位  
                        int cRed = (val & 0xf800) >> 8;
                        int cGreen = (val & 0x07E0) >> 3;
                        int cBlue = (val & 0x001F) << 3;
                        int n888Color = (cRed << 16) + (cGreen << 8) + (cBlue << 0);
                        byte[] dbytes = System.BitConverter.GetBytes(n888Color);

                        result.Add(dbytes[0]);
                        result.Add(dbytes[1]);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Create content based on text data. Chinese will be rotated.
        /// </summary>
        /// <param name="text">Text data.</param>
        /// <param name="autoFit">Auto fit.</param>
        /// <param name="aligment">Alignment.</param>
        /// <returns>The btimap</returns>
        public Bitmap CreateContent2(string text, bool autoFit = true, StringAlignment aligment = StringAlignment.Center)
        {
            Bitmap textBitmap = new Bitmap(Width, Height);

            Graphics textG = Graphics.FromImage(textBitmap);
            textG.FillRectangle(new SolidBrush(Color.Black), 0, 0, textBitmap.Width, textBitmap.Height);

            Font textFont = DisplayFont;
            SizeF textSize = textG.MeasureString(text, textFont);
            Font tempFont = DisplayFont;
            if (autoFit)
            {
                if (textSize.Width < Width && textSize.Height < Height)
                {
                    do
                    {
                        textFont = tempFont;
                        tempFont = new Font(DisplayFont.FontFamily, textFont.Size + (float)0.2);
                        textSize = textG.MeasureString(text, tempFont);
                    }
                    while (textSize.Width < Width && textSize.Height < Height);
                    textSize = textG.MeasureString(text, textFont);
                }
                else
                {
                    do
                    {
                        textFont = tempFont;
                        tempFont = new Font(DisplayFont.FontFamily, textFont.Size - (float)0.2);
                        textSize = textG.MeasureString(text, tempFont);
                    }
                    while (textSize.Width >= Width || textSize.Height >= Height);
                    textFont = tempFont;
                }
            }
            else
            {
                if (textSize.Height < DisplayFont.Size)
                {
                    do
                    {
                        textFont = tempFont;
                        tempFont = new Font(DisplayFont.FontFamily, textFont.Size + (float)0.2);
                        textSize = textG.MeasureString(text, tempFont);
                    }
                    while (textSize.Height < DisplayFont.Size);
                    textFont = tempFont;
                }
                else
                {
                    do
                    {
                        textFont = tempFont;
                        tempFont = new Font(DisplayFont.FontFamily, textFont.Size - (float)0.2);
                        textSize = textG.MeasureString(text, tempFont);
                    }
                    while (textSize.Height >= DisplayFont.Size);
                    textFont = tempFont;
                }
            }

            StringFormat textFormat = new StringFormat();
            textFormat.Alignment = aligment;
            textFormat.LineAlignment = StringAlignment.Center;

            // 横屏 
            float x = 0.0f;
            float y = textBitmap.Height / 2 + textBitmap.Height / 16;
            switch (aligment)
            {
                case StringAlignment.Near:
                    x = 1;
                    break;
                case StringAlignment.Center:
                    x = textBitmap.Width / 2;
                    break;
                case StringAlignment.Far:
                    x = textBitmap.Width - 1;
                    break;
            }

            textG.DrawString(
                text,
                textFont,
                new SolidBrush(ForeColor),
                x,
                y,
                textFormat);

            // 竖屏
            if (!Horizontal)
            {
                textBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }

            return textBitmap;
        }

        /// <summary>
        /// Create content based on text data. Chinese won't be rotated.
        /// </summary>
        /// <param name="text">Text data.</param>
        /// <param name="autoFit">Auto fit.</param>
        /// <param name="aligment">Alignment.</param>
        /// <returns>The btimap</returns>
        public Bitmap CreateContent(string text, bool autoFit = true, StringAlignment aligment = StringAlignment.Center)
        {
            //int calWidth = Horizontal ? Width : Height;
            //int calHeight = Horizontal ? Height : Width;
            //Bitmap textBitmap = new Bitmap(Width, Height);
            Horizontal = true;
            int calWidth = 80;
            int calHeight = 64;
            Bitmap textBitmap = Horizontal ? new Bitmap(calWidth, calHeight) : new Bitmap(calHeight, calWidth);
             Font font = new Font("宋体", 9f);
            Graphics textG = Graphics.FromImage(textBitmap);
            textG.FillRectangle(new SolidBrush(Color.Black), 0, 0, textBitmap.Width, textBitmap.Height);
            textG.DrawString(text, font, Brushes.White, new PointF() { X = 0, Y = 0 });

           string  b = string.Join("", Enumerable.Range(0, 5120).Select(a => new { x = a % 80, y = a / 80 })
           .Select(x => textBitmap.GetPixel(x.x, x.y).GetBrightness() > 0.5f ? "1" : "0"));
            //textBox5.Text += b;
            textBitmap.Save("1.bmp");

            DisplayFont = new Font("宋体", 12, FontStyle.Regular);
            Font textFont = DisplayFont;
            SizeF textSize = textG.MeasureString(text, textFont);
            Font tempFont = DisplayFont;
            if (autoFit)
            {
                if (textSize.Width < calWidth && textSize.Height < calHeight)
                {
                    do
                    {
                        textFont = tempFont;
                        tempFont = new Font(DisplayFont.FontFamily, textFont.Size + (float)0.2);
                        textSize = textG.MeasureString(text, tempFont);
                    }
                    while (textSize.Width < calWidth && textSize.Height < calHeight);
                    textSize = textG.MeasureString(text, textFont);
                }
                else
                {
                    do
                    {
                        textFont = tempFont;
                        tempFont = new Font(DisplayFont.FontFamily, textFont.Size - (float)0.2);
                        textSize = textG.MeasureString(text, tempFont);
                    }
                    while (textSize.Width >= calHeight || textSize.Height >= calHeight);
                    textFont = tempFont;
                }
            }
            else
            {
                if (textSize.Height < DisplayFont.Size)
                {
                    do
                    {
                        textFont = tempFont;
                        tempFont = new Font(DisplayFont.FontFamily, textFont.Size + (float)0.2);
                        textSize = textG.MeasureString(text, tempFont);
                    }
                    while (textSize.Height < DisplayFont.Size);
                    textFont = tempFont;
                }
                else
                {
                    do
                    {
                        textFont = tempFont;
                        tempFont = new Font(DisplayFont.FontFamily, textFont.Size - (float)0.2);
                        textSize = textG.MeasureString(text, tempFont);
                    }
                    while (textSize.Height >= DisplayFont.Size);
                    textFont = tempFont;
                }
            }

            StringFormat textFormat = new StringFormat();
            textFormat.Alignment = aligment;
            textFormat.LineAlignment = StringAlignment.Center;
            if (Horizontal) // 横屏 
            {
                float x = 0.0f;
                float y = textBitmap.Height / 2 + textBitmap.Height / 16;
                switch (aligment)
                {
                    case StringAlignment.Near:
                        x = 1;
                        break;
                    case StringAlignment.Center:
                        x = textBitmap.Width / 2;
                        break;
                    case StringAlignment.Far:
                        x = textBitmap.Width - 1;
                        break;
                }

                textG.DrawString(
                    text,
                    textFont,
                    new SolidBrush(ForeColor),
                    x,
                    y,
                    textFormat);
            }
            else        // 竖屏 
            {
                //float x = textBitmap.Width / 2 - textBitmap.Width / 16;
                float x = textBitmap.Width / 2;
                float y = 0.0f;
                switch (aligment)
                {
                    case StringAlignment.Near:
                        y = 2;
                        break;
                    case StringAlignment.Center:
                        y = textBitmap.Height / 2;
                        break;
                    case StringAlignment.Far:
                        y = textBitmap.Height - 2;
                        break;
                }

                textFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                textG.DrawString(
                    text,
                    textFont,
                    new SolidBrush(ForeColor),
                    x,
                    y,
                    textFormat);
            }

            // 竖屏
            if (!Horizontal)
            {
                textBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }

            return textBitmap;
        }


        /// <summary>
        /// Create content based on text data. Chinese won't be rotated.
        /// </summary>
        /// <param name="text">Text data.</param>
        /// <param name="smallFontWidth"></param>
        /// <param name="bigFontWidth"></param>
        /// <returns>The btimap</returns>
        public Bitmap CreateContentX(string text, string fontStyle = "黑体", int smallFontWidth = 8, int bigFontWidth = 12)
        {
            int engFontWidth = Split(text, bigFontWidth).Count > 1 ? smallFontWidth : bigFontWidth;

            Bitmap textBitmap = Horizontal ? new Bitmap(Width, Height) : new Bitmap(Height, Width);

            Graphics textG = Graphics.FromImage(textBitmap);
            textG.FillRectangle(new SolidBrush(Color.Black), 0, 0, textBitmap.Width, textBitmap.Height);

            // English Char
            Font engFont = new Font("Tahoma", engFontWidth, FontStyle.Regular);
            SizeF engSize = textG.MeasureString("A", engFont);
            Font tempEngFont = engFont;
            if (engSize.Width <= engFontWidth)
            {
                while (engSize.Width <= engFontWidth)
                {
                    tempEngFont = engFont;
                    engFont = new Font("Tahoma", (engFont.Size + 0.05f), FontStyle.Regular);
                    engSize = textG.MeasureString("A", engFont);
                }
            }
            else
            {
                while (engSize.Width > engFontWidth)
                {
                    engFont = new Font("Tahoma", (engFont.Size - 0.05f), FontStyle.Regular);
                    engSize = textG.MeasureString("A", engFont);
                    tempEngFont = engFont;
                }
            }

            // Chinese Char
            Font chiFont = new Font(fontStyle, engFontWidth, FontStyle.Regular);
            SizeF chiSize = textG.MeasureString("中", chiFont);
            Font tempChiFont = chiFont;
            if (chiSize.Width <= engFontWidth)
            {
                while (chiSize.Width < engFontWidth)
                {
                    tempChiFont = chiFont;
                    chiFont = new Font(fontStyle, (chiFont.Size + 0.05f), FontStyle.Regular);
                    chiSize = textG.MeasureString("A", chiFont);
                }
            }
            else
            {
                while (chiSize.Width >= engFontWidth)
                {
                    chiFont = new Font(fontStyle, (chiFont.Size - 0.05f), FontStyle.Regular);
                    chiSize = textG.MeasureString("A", chiFont);
                    tempChiFont = chiFont;
                }
            }

            int chars = CalculateEngCharCount(text);

            StringFormat textFormat = new StringFormat();
            textFormat.Alignment = StringAlignment.Near;
            textFormat.LineAlignment = StringAlignment.Center;
            if (Horizontal) // 横屏 
            {
                float x = ((float)textBitmap.Width - (float)chars * engFontWidth) / 2;
                float y = textBitmap.Height / 2 + textBitmap.Height / 16;
                for (int i = 0; i < text.Length; i++)
                {
                    string c = text.Substring(i, 1);
                    if (System.Text.Encoding.Default.GetBytes(c).Length == 1)
                    {
                        x += engFontWidth;
                        textG.DrawString(
                            c,
                            engFont,
                            new SolidBrush(ForeColor),
                            x - engFontWidth + engFontWidth / 4.0f,
                            y,
                            textFormat);
                    }
                    else
                    {
                        x += (engFontWidth * 2);
                        textG.DrawString(
                            c,
                            chiFont,
                            new SolidBrush(ForeColor),
                            x - (engFontWidth * 2) + engFontWidth / 3.0f,
                            y,
                            textFormat);
                    }
                }
            }
            else        // 竖屏 
            {
                float x = textBitmap.Width / 2 - textBitmap.Width / 16;
                float y = ((float)textBitmap.Height - (float)chars * engFontWidth) / 2;
                textFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                for (int i = 0; i < text.Length; i++)
                {
                    string c = text.Substring(i, 1);
                    if (System.Text.Encoding.Default.GetBytes(c).Length == 1)
                    {
                        y += engFontWidth;
                        textG.DrawString(
                            c,
                            engFont,
                            new SolidBrush(ForeColor),
                            x,
                            y - engFontWidth,
                            textFormat);
                    }
                    else
                    {
                        y += (engFontWidth * 2);
                        textG.DrawString(
                            c,
                            chiFont,
                            new SolidBrush(ForeColor),
                            x,
                            y - (engFontWidth * 2) + engFontWidth / 3.0f,
                            textFormat);
                    }
                }
            }

            // 竖屏
            if (!Horizontal)
            {
                textBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }

            return textBitmap;
        }
    }
    
}

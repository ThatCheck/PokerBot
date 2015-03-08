using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Utils
{
    public class Image
    {
        public static void save(String name,Bitmap pImage)
        {
            System.IO.FileStream fs = System.IO.File.Create(name);
            pImage.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            fs.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Late2Meet
{
    public static class ImageUtility
    {
        public static string StreamToString(Stream imageStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                imageStream.CopyTo(memoryStream);
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        public static ImageSource Decode(string imageString)
        {
            byte[] imageBytes = Convert.FromBase64String(imageString);
            return ImageSource.FromStream(() => new MemoryStream(imageBytes));
        }
    }
}

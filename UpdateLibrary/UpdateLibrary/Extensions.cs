using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UpdateLibrary
{
    public static class Extensions
    {
        public static ImageSource GetImageSource(this Uri uri) 
        {
            using (WebClient client = new WebClient())
            {
                var imageData = new WebClient().DownloadData(uri);

                var bitmapImage = new BitmapImage { CacheOption = BitmapCacheOption.OnLoad };
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(imageData);
                bitmapImage.EndInit();

                return bitmapImage;


            }
        }

        public static Uri GetUri(this string url) => new Uri(url);

    }
}

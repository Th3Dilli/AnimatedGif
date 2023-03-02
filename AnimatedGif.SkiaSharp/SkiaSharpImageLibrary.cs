using System.IO;
using SkiaSharp;

namespace AnimatedGif.SkiaSharp {
    public class SkiaSharpImageLibrary : ImageLibrary {
        public override RawBitmap LoadImage(string path) {
            // Load the image
            var image = SKBitmap.Decode(path);

            return BitmapConverter.Convert(image);
        }

        public override void SaveGif(Stream stream, RawBitmap img, GifQuality quality) {
            var image = BitmapConverter.Convert(img);
            var a = image.Encode(SKEncodedImageFormat.Gif, 100);
            a.SaveTo(stream);
        }
    }
}
using System;
using SkiaSharp;

namespace AnimatedGif.SkiaSharp {
    public class BitmapConverter {
        public static RawBitmap32 Convert(SKBitmap source) {
            // Get the size of the source image
            int height = source.Height;
            int width = source.Width;

            // First off ensure that we have the image in 32bpp pixel format
            if (source.Info.BytesPerPixel != 4)
                source = SKBitmap.Decode(source.Bytes);

            unsafe {
                var rawBitmap = new RawBitmap32 {
                    Width = width,
                    Height = height,
                    Pixels = new Color32[width * height]
                };

                var sourcePixels = (uint*)source.GetPixels().ToPointer();
                var size = source.Width * source.Height;
                for (var i = 0; i < size; i++) {
                    var a = Color32.FromArgb(sourcePixels[i]);
                    rawBitmap.Pixels[i] = a;
                }

                return rawBitmap;
            }
        }

        public static SKBitmap Convert(RawBitmap rawBitmap) {
            if (rawBitmap is RawBitmap32 rawBitmap32)
                return Convert(rawBitmap32);
            if (rawBitmap is RawBitmap8 rawBitmap8)
                return Convert(rawBitmap8);

            throw new ArgumentException();
        }

        public static SKBitmap Convert(RawBitmap32 rawBitmap) {
            unsafe
            {
                var data = new SKBitmap(rawBitmap.Width , rawBitmap.Height);
                var pointer = (uint*)data.GetPixels().ToPointer();

                for (var i = 0; i < rawBitmap.Width * rawBitmap.Height; i++) {
                    pointer[i] = unchecked((uint)rawBitmap.Pixels[i].ARGB);
                }
                return data;
            }
        }
    }
}
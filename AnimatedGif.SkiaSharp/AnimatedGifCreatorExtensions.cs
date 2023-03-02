using SkiaSharp;

namespace AnimatedGif.SkiaSharp {
    public static class AnimatedGifCreatorExtensions {
        public static void AddFrame(this AnimatedGifCreator creator, SKBitmap image, int delay = -1,
            GifQuality quality = GifQuality.Default) {
            creator.AddFrame(
                BitmapConverter.Convert(image),
                delay,
                quality);
        }
    }
}
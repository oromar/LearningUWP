using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Win2DSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //private string text;
        //private Vector2 position;

        private CanvasBitmap localBitmap;
        private ICanvasImage bitmap1;
        private CanvasBitmap bitmap2;

        public MainPage()
        {
            this.InitializeComponent();
            //text = TextBox.Text;
            //position = new Vector2(0,0);
        }

        //private void OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        //{
        //    var ds = args.DrawingSession;
        //    ds.DrawText(TextBox.Text, new Vector2(0, 0), Colors.Green);
        //}

        //private void OnChange(object sender, TextChangedEventArgs e)
        //{
        //    this.text = TextBox.Text;
        //}

        private void OnAnimatedDraw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            var ds = args.DrawingSession;

            //position.X = (position.X +  1) % (float)sender.Size.Width;
            //position.Y = (position.Y + 1) % (float)sender.Size.Height;

            //ds.DrawText(text, position, Colors.Red);

            var size = sender.Size;

            DrawLocal(ds, size);

            DrawBitmap1(ds, size);

            DrawBitmap2(ds, size);

            DrawLines(ds, size);
        }
        private static void DrawLines(CanvasDrawingSession ds, Size size)
        {
            Rect rect = new Rect(0, 0, size.Width, size.Height);
            ds.DrawRectangle(rect, Colors.Black, 10);

            ds.DrawLine(new Vector2((float)size.Width / 2, 0),
                        new Vector2((float)size.Width / 2, (float)size.Height),
                        Colors.Black,
                        10);


            ds.DrawLine(new Vector2((float)size.Width / 2, (float)size.Height / 2),
                        new Vector2((float)size.Width, (float)size.Height / 2),
                        Colors.Black,
                        10);
        }
        private void DrawBitmap2(CanvasDrawingSession ds, Size size)
        {


            if (bitmap2 != null)
            {
                var canvas = new CanvasRenderTarget(CanvasDevice.GetSharedDevice(),
                                                    bitmap2.SizeInPixels.Width / 2,
                                                    bitmap2.SizeInPixels.Height,
                                                    96);

                using (var canvasDS = canvas.CreateDrawingSession())
                {
                    var sourceRect = new Rect(bitmap2.SizeInPixels.Width * 0.25,
                                          0,
                                          bitmap2.SizeInPixels.Width / 2,
                                          bitmap2.SizeInPixels.Height);


                    canvasDS.DrawImage(bitmap2, new Rect(0, 0, canvas.SizeInPixels.Width, canvas.SizeInPixels.Height), sourceRect);
                }

                var effect = new SepiaEffect
                {
                    Source = canvas
                };

                var effect2 = new VignetteEffect
                {
                    Amount = 1f,
                    Source = effect
                };

                var drawRect = new Rect(0, 0, size.Width / 2, size.Height);

                var canvasSourceRect = new Rect(0, 0, canvas.SizeInPixels.Width, canvas.SizeInPixels.Height);

                ds.DrawImage(effect2, drawRect, canvasSourceRect);
            }

        }
        private void DrawBitmap1(CanvasDrawingSession ds, Size size)
        {
            Rect rect = new Rect(size.Width / 2,
                            size.Height / 2,
                            size.Width / 2,
                            size.Height / 2);


            if (bitmap1 != null)
            {
                var sourceRect = new Rect(0, 0, bitmap1.GetBounds(CanvasDevice.GetSharedDevice()).Width, bitmap1.GetBounds(CanvasDevice.GetSharedDevice()).Height);

                ds.DrawImage(bitmap1, rect, sourceRect);
            }
        }
        private void DrawLocal(CanvasDrawingSession ds, Size size)
        {
            var rect = new Rect(size.Width / 2,
                                        0,
                                        size.Width / 2,
                                        size.Height / 2);

            if (localBitmap != null)
            {
                var effect = new GrayscaleEffect
                {
                    Source = localBitmap
                };

                var sourceRect = new Rect(0, 0, localBitmap.SizeInPixels.Width, localBitmap.SizeInPixels.Height);

                ds.DrawImage(effect, rect, sourceRect);
            }
        }
        private async void OnCreateResource(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            //var device = sender.Device;

            var device = CanvasDevice.GetSharedDevice();

            localBitmap = await CanvasBitmap.LoadAsync(device, new Uri("ms-appx:///Assets/casa.jpg"));
        }
        private async void LoadImage1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            bitmap1 = new SepiaEffect
            {
                Source = await PickImageAsync()
            };
        }
        private async void LoadImage2(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            bitmap2 = await PickImageAsync();
        }
        private static async Task<CanvasBitmap> PickImageAsync()
        {
            CanvasBitmap result = null;
            var picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            var file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                using (var stream = await file.OpenReadAsync())
                {
                    result = await CanvasBitmap.LoadAsync(CanvasDevice.GetSharedDevice(), stream);
                }
            }
            return result;
        }

        private async void SaveFile(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var canvas = new CanvasRenderTarget(CanvasDevice.GetSharedDevice(),
                                                (float)AnimatedCanvas.Size.Width,
                                                (float)AnimatedCanvas.Size.Height,
                                                96);

            using (var canvasDS = canvas.CreateDrawingSession())
            {
                DrawLocal(canvasDS, AnimatedCanvas.Size);
                DrawBitmap1(canvasDS, AnimatedCanvas.Size);
                DrawBitmap2(canvasDS, AnimatedCanvas.Size);
                DrawLines(canvasDS, AnimatedCanvas.Size);
            }

            var picker = new FileSavePicker();
            picker.FileTypeChoices.Add("image", new List<string> { ".png" });
            var file = await picker.PickSaveFileAsync();

            if (file != null)
            {
                using(var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var bytes = canvas.GetPixelBytes();

                    var encoder = await  BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);

                    encoder.SetPixelData(BitmapPixelFormat.Bgra8,
                                         BitmapAlphaMode.Straight,
                                         canvas.SizeInPixels.Width,
                                         canvas.SizeInPixels.Height,
                                         96,
                                         96,
                                         bytes);

                    await encoder.FlushAsync();
                }
            }
        }
    }
}


using System;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Proxier.Helpers
{
    public static class IconGrabber
    {
        [DllImport("Kernel32.dll")]
        private static extern uint QueryFullProcessImageName([In] IntPtr hProcess, [In] uint dwFlags, [Out] StringBuilder lpExeName, [In, Out] ref uint lpdwSize);

        private static readonly Dictionary<string, Bitmap> CachedIcons = new Dictionary<string, Bitmap>();

        public static bool TryGetIconBitmap(Process process, int size, out Bitmap icon)
        {
            icon = null;

            try
            {
                if (CachedIcons.ContainsKey(process.ProcessName))
                {
                    icon = CachedIcons[process.ProcessName];
                    return true;
                }

                if(TryGetMainModuleFileName(process, 1024, out string fileName))
                {
                    using (var ico = Icon.ExtractAssociatedIcon(fileName))
                    {
                        using (Bitmap bmap = ico.ToBitmap())
                        {
                            var resized = ResizeImage(bmap, size, size);

                            if (!CachedIcons.ContainsKey(process.ProcessName))
                                CachedIcons.Add(process.ProcessName, resized);

                            icon = resized;
                            return true;
                        }
                    }
                }
                else
                {
                    return false;
                }         
            }
            catch
            {
                return false;
            }
        }

        private static bool TryGetMainModuleFileName(Process process, int buffer, out string moduleName)
        {
            moduleName = string.Empty;
            try
            {
                var fileNameBuilder = new StringBuilder(buffer);
                uint bufferLength = (uint)fileNameBuilder.Capacity + 1;

                var query = QueryFullProcessImageName(process.Handle, 0, fileNameBuilder, ref bufferLength);

                if (query != 0)
                {
                    moduleName = fileNameBuilder.ToString();
                    return true;
                }
                else
                    return false;
            }
            catch { }

            return false;
        }

        private static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }    
}

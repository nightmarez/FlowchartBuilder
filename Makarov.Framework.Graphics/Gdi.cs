using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Makarov.Framework.Graphics
{
    public static class Gdi
    {
        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight,
            IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

        [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] 
        public static void BitBlt(Bitmap sourceBmp,
                                  System.Drawing.Graphics source,
                                  int sourceX, int sourceY,
                                  System.Drawing.Graphics dest,
                                  int destX, int destY,
                                  int destWidth, int destHeight)
        {
            IntPtr hdcDest = IntPtr.Zero;
            IntPtr hdcSrc = IntPtr.Zero;
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr hOldObject = IntPtr.Zero;

            try
            {
                hdcSrc = source.GetHdc();
                hdcDest = dest.GetHdc();
                hBitmap = sourceBmp.GetHbitmap();

                hOldObject = SelectObject(hdcSrc, hBitmap);
                if (hOldObject == IntPtr.Zero)
                    throw new Win32Exception();

                BitBlt(hdcDest, destX, destY, destWidth, destHeight, hdcSrc, sourceX, sourceY, 0x00cc0020u);
            }
            finally
            {
                if (hOldObject != IntPtr.Zero) SelectObject(hdcSrc, hOldObject);
                if (hBitmap != IntPtr.Zero) DeleteObject(hBitmap);
                if (hdcDest != IntPtr.Zero) dest.ReleaseHdc(hdcDest);
                if (hdcSrc != IntPtr.Zero) source.ReleaseHdc(hdcSrc);
            }
        }
    }
}

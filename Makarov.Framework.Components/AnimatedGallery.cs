using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Makarov.Framework.Components
{
    public class AnimatedGallery : Control
    {
        private sealed class AnimatedImage : IDisposable
        {
            private readonly AnimatedGallery _owner;

            public Image Image { get; private set; }
            public Point Location { get; set; }

            public AnimatedImage(AnimatedGallery owner, Image image, Point pt)
            {
                if (image == null)
                    throw new NullReferenceException();

                if (owner == null)
                    throw new NullReferenceException();

                _owner = owner;
                Image = image;
                Location = pt;
            }

            public void BeginAnimation()
            {
                if (ImageAnimator.CanAnimate(Image))
                    ImageAnimator.Animate(Image, ImageAnimation);
            }

            public void EndAnimation()
            {
                ImageAnimator.StopAnimate(Image, ImageAnimation);
            }

            private void ImageAnimation(object sender, EventArgs e)
            {
                ImageAnimator.UpdateFrames(Image);
                _owner.AnimationUpdate(this);
            }

            public void Dispose()
            {
                if (Image != null)
                {
                    Image.Dispose();
                    Image = null;
                }
            }
        }

        private readonly object _bufferSync = new object();
        private List<AnimatedImage> _images = new List<AnimatedImage>();
        Image _screenBuffer;
        private Size _maxImageSize = new Size(64, 64);

        public AnimatedGallery()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            lock (_bufferSync)
            {
                if (_screenBuffer != null)
                    e.Graphics.DrawImage(_screenBuffer, Point.Empty);
            }

            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            lock (_bufferSync)
            {
                _screenBuffer = null;
            }

            if (_images != null)
                lock (_images)
                {
                    for (int i = 0; i < _images.Count; i++)
                        _images[i].Location = new Point(
                            i % Columns * _maxImageSize.Width,
                            i / Columns * _maxImageSize.Height);
                }

            Invalidate();

            base.OnResize(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_images != null)
                {
                    foreach (AnimatedImage image in _images)
                        if (image != null)
                        {
                            image.EndAnimation();
                            image.Dispose();
                        }

                    _images = null;
                }

                lock (_bufferSync)
                {
                    if (_screenBuffer != null)
                    {
                        _screenBuffer.Dispose();
                        _screenBuffer = null;
                    }
                }
            }

            base.Dispose(disposing);
        }

        void AnimationUpdate(AnimatedImage image)
        {
            lock (_bufferSync)
            {
                if (_screenBuffer == null && Width > 0 && Height > 0)
                    _screenBuffer = new Bitmap(Width, Height);

                if (_screenBuffer != null)
                {
                    using (Graphics gfx = Graphics.FromImage(_screenBuffer))
                    {
                        gfx.SetClip(new Rectangle(image.Location, _maxImageSize));
                        gfx.Clear(BackColor);
                        gfx.DrawImage(image.Image, image.Location);
                    }
                }
            }

            Invalidate();
        }

        public void LoadImages(string[] files)
        {
            new Thread(() => LoadImagesAsync(files)).Start();
        }

        public int Columns
        {
            get
            {
                if (_maxImageSize.Width == 0)
                    return 0;

                return Width / _maxImageSize.Width;
            }
        }

        public int Rows
        {
            get
            {
                if (_images == null || Columns == 0)
                    return 0;

                return _images.Count / Columns;
            }
        }

        void LoadImagesAsync(string[] files)
        {
            for (int i = 0; i < files.Length; i++)
                if (_images != null)
                {
                    var image = Image.FromFile(files[i]);

                    lock (_images)
                    {
                        _maxImageSize = new Size(
                            Math.Max(_maxImageSize.Width, image.Width),
                            Math.Max(_maxImageSize.Height, image.Height));

                        var animatedImage = new AnimatedImage(
                            this,
                            image,
                            new Point(i % Columns * _maxImageSize.Width, i / Columns * _maxImageSize.Height));

                        _images.Add(animatedImage);
                        animatedImage.BeginAnimation();
                    }
                }
        }
    }
}
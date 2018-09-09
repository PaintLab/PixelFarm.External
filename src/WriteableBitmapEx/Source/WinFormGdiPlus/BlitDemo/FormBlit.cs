﻿//MIT, 2009-2015, Rene Schulte and WriteableBitmapEx Contributors, https://github.com/teichgraf/WriteableBitmapEx
//
//
//   Project:           WriteableBitmapEx - WriteableBitmap extensions
//   Description:       Collection of extension methods for the WriteableBitmap class.
//
//   Changed by:        $Author: unknown $
//   Changed on:        $Date: 2015-03-05 18:18:24 +0100 (Do, 05 Mrz 2015) $
//   Changed in:        $Revision: 113191 $
//   Project:           $URL: https://writeablebitmapex.svn.codeplex.com/svn/trunk/Source/WriteableBitmapEx/WriteableBitmapBaseExtensions.cs $
//   Id:                $Id: WriteableBitmapBaseExtensions.cs 113191 2015-03-05 17:18:24Z unknown $
//
//
//   Copyright © 2009-2015 Rene Schulte and WriteableBitmapEx Contributors
//
//   This code is open source. Please read the License.txt for details. No worries, we won't sue you! ;)
//


using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace WinFormGdiPlus
{
    public partial class FormBlit : Form
    {
        Timer timer1;
        public FormBlit()
        {
            InitializeComponent();
        }

        private void FormBlit_Load(object sender, EventArgs e)
        {
            particleBmp = LoadBitmapAsReadonly("../../FlowerBurst.jpg");
            circleBmp = LoadBitmapAsReadonly("../../circle.png");


            timer1 = new Timer();
            timer1.Interval = 30;
            timer1.Tick += (s1, e1) => this.Invoke(new MethodInvoker(() => UpdateRenderFrame()));
            particleSourceRect = new Rect(0, 0, 64, 64);

            //bmp = BitmapFactory.New(640, 480);
            //bmp.Clear(Colors.Black);
            destBmp = new Bitmap(400, 500);
            g = this.panel1.CreateGraphics();

            emitter = new ParticleEmitter();

            //CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
            this.MouseMove += new MouseEventHandler(MainPage_MouseMove);
        }

        Graphics g;
        Bitmap destBmp;
        void UpdateRenderFrame()
        {
            // Wrap updates in a GetContext call, to prevent invalidation and nested locking/unlocking during this block
            // NOTE: This is not strictly necessary for the SL version as this is a WPF feature, however we include it here for completeness and to show
            // a similar API to WPF 
            //render! 
            using (var bmplock = destBmp.Lock())
            {
                WriteableBitmap wb = bmplock.GetWritableBitmap();
                emitter.TargetBitmap = wb;
                emitter.ParticleBitmap = particleBmp;

                wb.Clear(Colors.Black);


                double elapsed = (DateTime.Now - lastUpdate).TotalSeconds;
                lastUpdate = DateTime.Now;
                emitter.Update(elapsed);
                //			bmp.Blit(new Point(100, 150), circleBmp, new Rect(0, 0, 200, 200), Colors.Red, BlendMode.Additive);
                //			bmp.Blit(new Point(160, 55), circleBmp, new Rect(0, 0, 200, 200), Color.FromArgb(255, 0, 255, 0), BlendMode.Additive);
                //			bmp.Blit(new Point(220, 150), circleBmp, new Rect(0, 0, 200, 200), Colors.Blue, BlendMode.Additive);

                //double timeNow = _stopwatch.ElapsedMilliseconds;
                //double elapsedMilliseconds = timeNow - _lastTime;
                //_lowestFrameTime = Math.Min(_lowestFrameTime, elapsedMilliseconds);
                //// FpsCounter.Text = string.Format("FPS: {0:0.0} / Max: {1:0.0}", 1000.0 / elapsedMilliseconds, 1000.0 / _lowestFrameTime);
                //_lastTime = timeNow;

                //
                bmplock.WriteAndUnlock();
                //

                g.Clear(System.Drawing.Color.White);
                g.DrawImage(destBmp, 0, 0);
            }


        }

        WriteableBitmap circleBmp;
        WriteableBitmap particleBmp;
        Rect particleSourceRect;
        ParticleEmitter emitter = new ParticleEmitter();
        DateTime lastUpdate = DateTime.Now;

        private double _lastTime;
        private double _lowestFrameTime;

        static WriteableBitmap LoadBitmapAsReadonly(string path)
        {
            using (Bitmap bmp = new Bitmap(path))
            using (var lockBmp = new LockBmp(bmp))
            {
                return lockBmp.GetWritableBitmap();
            }
        }
        void MainPage_MouseMove(object sender, MouseEventArgs e)
        {
            //emitter.Center = e.GetPosition(image);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //start ...
            UpdateRenderFrame();
        }
        public static WriteableBitmap Overlay(WriteableBitmap bmp, WriteableBitmap overlay, System.Windows.Media.Imaging.Point location)
        {
            var result = bmp.Clone();
            var size = new System.Windows.Media.Imaging.Size(overlay.PixelWidth, overlay.PixelHeight);
            result.Blit(new Rect(location, size), overlay,
                new Rect(new System.Windows.Media.Imaging.Point(0, 0), size),
                WriteableBitmapExtensions.BlendMode.Multiply);
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WriteableBitmap unmodifiedBmp = LoadBitmapAsReadonly("../../02.jpg");
            WriteableBitmap sticker = LoadBitmapAsReadonly("../../01.jpg");

            WriteableBitmap overlayResult = Overlay(unmodifiedBmp, sticker, new System.Windows.Media.Imaging.Point(10, 10));

            using (var bmplock = destBmp.Lock())
            {
                WriteableBitmap wb = bmplock.GetWritableBitmap();
                wb.Clear(Colors.Black);

                wb.Blit(new Rect(0, 0, overlayResult.PixelWidth, overlayResult.PixelHeight),
                        overlayResult,
                        new Rect(0, 0, overlayResult.PixelWidth, overlayResult.PixelHeight));

                bmplock.WriteAndUnlock();

                g.Clear(System.Drawing.Color.White);
                g.DrawImage(destBmp, 0, 0);
            }


        }
    }
}

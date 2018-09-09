﻿/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////
using System;
using PixelFarm.Drawing;
namespace PaintDotNet.Effects
{
    public class MedianRenderer : HistogramRenderer
    {

        public int Radius { get; set; }
        public int Percentile { get; set; }
        public unsafe override ColorBgra Apply(ColorBgra src, int area, int* hb, int* hg, int* hr, int* ha)
        {
            ColorBgra c = GetPercentile(this.Percentile, area, hb, hg, hr, ha);
            return c;
        }

        public override void Render(Surface src, Surface dest, Rectangle[] rois, int startIndex, int length)
        {
            foreach (Rectangle rect in rois)
            {
                RenderRect(this.Radius, src, dest, rect);
            }

        }
    }
}
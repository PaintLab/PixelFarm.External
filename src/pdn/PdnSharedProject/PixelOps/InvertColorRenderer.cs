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

    public class InvertColorRenderer : EffectRendererBase
    {
        UnaryPixelOps.Invert invertOp = new UnaryPixelOps.Invert();
        public override void Render(Surface src, Surface dst, Rectangle[] rois, int start, int len)
        {
            this.invertOp.Apply(dst, src, rois, start, len);
        }
    }
}
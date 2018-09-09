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
    public abstract class EffectRendererBase
    {
        public abstract void Render(Surface src, Surface dest, Rectangle[] rois, int startIndex, int length);
    }
}
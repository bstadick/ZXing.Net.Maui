﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;

namespace ZXing.Net.Maui;

public class SoftwareBitmapLuminanceSource : BaseLuminanceSource
{
	public SoftwareBitmapLuminanceSource(SoftwareBitmap softwareBitmap) : base(softwareBitmap.PixelWidth, softwareBitmap.PixelHeight)
		=> CalculateLuminance(softwareBitmap);

	protected SoftwareBitmapLuminanceSource(int width, int height) : base(width, height)
	{
	}

	protected SoftwareBitmapLuminanceSource(byte[] luminanceArray, int width, int height) : base(luminanceArray, width, height)
	{
	}

	protected override LuminanceSource CreateLuminanceSource(byte[] newLuminances, int width, int height)
		=> new SoftwareBitmapLuminanceSource(width, height) { luminances = newLuminances };

	void CalculateLuminance(SoftwareBitmap bitmap)
	{
		if (bitmap.BitmapPixelFormat != BitmapPixelFormat.Gray8)
		{
			bitmap = SoftwareBitmap.Convert(bitmap, BitmapPixelFormat.Gray8);
		}

		var buffer = luminances.AsBuffer();
		bitmap.CopyToBuffer(buffer);
	}
}

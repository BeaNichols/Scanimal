using System;
using UnityEngine;
using Wizcorp.Utils.Logger;
using ZXing;

namespace BarcodeScanner.Parser
{
	public class ZXingParser : IParser
	{
		public ZXing.BarcodeReader Scanner { get; private set; }

		public ZXingParser() : this(new ScannerSettings())
		{
		}

		public ZXingParser(ScannerSettings settings)
		{
			Scanner = new ZXing.BarcodeReader();
			Scanner.AutoRotate = settings.ParserAutoRotate;
			Scanner.Options.TryHarder = settings.ParserTryHarder;
		}

		public ParserResult Decode(Color32[] colors, int width, int height)
		{
			if (colors == null || colors.Length == 0 || width == 0 || height == 0)
			{
				return null;
			}

			ParserResult value = null;
			try
			{
				var result = Scanner.Decode(colors, width, height);
				if (result != null)
				{
					value = new ParserResult(result.BarcodeFormat.ToString(), result.Text);
				}
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
			
			return value;
		}
	}
}

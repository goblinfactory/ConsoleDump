﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleDump;
using System.Net;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Threading;

namespace Demo
{
	class Program
	{
		static void Main(string[] args)
		{
			Show("foo");

			Show(42);

			Show(new
			{
				Int32 = 1,
				String = "sssstring",
				NullObject = (object)null,
				Boolean = true,
				DateTime = DateTime.UtcNow,
				SomeClass = Version.Parse("1.0"),
				SomeStruct = new KeyValuePair<int, int>(1, 1),
			});

			Show(new[] { 1, 22, 333, 4444 });

			Show(IPAddress.Parse("1.1.1.1"));

			Show(Enumerable.Range(0, 100).ToList());

			Show(new[] { new { a = 1 }, null, new { a = 1 }, });


			Show(Enumerable.Range(4, 8).ToDictionary(
					n => n,  
					n => Convert.ToString(-1 + (long)Math.Pow(2, n), 2)));

			Show(new ArgumentException("my message", "someParam"));
		}

		static int Count = 0;
		static void Show<T>(T it)
		{
			Console.WriteLine("// Example: " + (++Count));

			Console.WriteLine();
			Console.WriteLine("// Console.WriteLine");
			Console.WriteLine(it);
			TakeScreenShot("console", Count);

			Console.WriteLine();
			Console.WriteLine("// ServiceStack.Text.TypeSerializer.PrintDump");
			try
			{
				ServiceStack.Text.TypeSerializer.PrintDump(it);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			TakeScreenShot("servicestack", Count);

			Console.WriteLine();
			Console.WriteLine("// ConsoleDump");
			ConsoleDump.DumpExtensions.Dump(it);
			TakeScreenShot("consoledump", Count);

			ConsoleDump.DumpExtensions.Dump(it, "ConsoleDump label");
		}

		static void TakeScreenShot(string label, int example)
		{
			Thread.Sleep(1);
			// crop the edges of my powershell window
			var img = ScreenShotDemo.ScreenCapture.CaptureWindow(GetConsoleWindow(), 32, 10, 10, 27);
			img.Save(String.Format("{0:d2}_{1}.png", example, label), ImageFormat.Png);
		}

		[DllImport("kernel32.dll")]
		internal static extern IntPtr GetConsoleWindow();
	}
}

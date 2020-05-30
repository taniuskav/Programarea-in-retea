using System;
using System.Runtime.InteropServices;

namespace GenesisChatClient
{
	[StructLayout(LayoutKind.Sequential)]
	public struct FLASHWINFO
	{
		public UInt32 cbSize;
		public IntPtr hwnd;
		public UInt32 dwFlags;
		public UInt32 uCount;
		public UInt32 dwTimeout;
	}

	public class PInvoke
	{
		[DllImport("user32.dll")]
		public static extern Int16 FlashWindowEx(ref FLASHWINFO pwfi);

		//Stop flashing. The system restores the window to its original state. 
		public const UInt32 FLASHW_STOP = 0; 
		//Flash the window caption. 
		public const UInt32 FLASHW_CAPTION = 1; 
		//Flash the taskbar button. 
		public const UInt32 FLASHW_TRAY = 2; 
		//Flash both the window caption and taskbar button.
		//This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags. 
		public const UInt32 FLASHW_ALL = 3; 
		//Flash continuously, until the FLASHW_STOP flag is set. 
		public const UInt32 FLASHW_TIMER = 4; 
		//Flash continuously until the window comes to the foreground. 
		public const UInt32 FLASHW_TIMERNOFG = 12; 

	}
}

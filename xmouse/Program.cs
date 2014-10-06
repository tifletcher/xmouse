using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

using DWORD = System.UInt32;

namespace xmouse
{
	static class ActiveWindowTracking
	{

		private const string User32 = "user32.dll";
		private const string SPIExceptionMessage = "Error calling SystemParametersInfo";

		private static class External
		{
			public enum Actions
			{
				//Determines whether active window tracking (activating the window the mouse is on) is on or off. The pvParam parameter must point to a BOOL variable that receives TRUE for on, or FALSE for off.
				SPI_GETACTIVEWINDOWTRACKING = 0x1000,

				//Determines whether active window tracking (activating the window the mouse is on) is on or off. The pvParam parameter must point to a BOOL variable that receives TRUE for on, or FALSE for off.		//Determines whether windows activated through active window tracking will be brought to the top. The pvParam parameter must point to a BOOL variable that receives TRUE for on, or FALSE for off.
				SPI_GETACTIVEWNDTRKZORDER = 0x100C,

				// Retrieves the active window tracking delay, in milliseconds. The pvParam parameter must point to a DWORD variable that receives the time.
				SPI_GETACTIVEWNDTRKTIMEOUT = 0x2002,

				//Sets active window tracking (activating the window the mouse is on) either on or off. Set pvParam to TRUE for on or FALSE for off.
				SPI_SETACTIVEWINDOWTRACKING = 0x1001,

				//Determines whether or not windows activated through active window tracking should be brought to the top. Set pvParam to TRUE for on or FALSE for off.
				SPI_SETACTIVEWNDTRKZORDER = 0x100D,

				//Sets the active window tracking delay. Set pvParam to the number of milliseconds to delay before activating the window under the mouse pointer.
				SPI_SETACTIVEWNDTRKTIMEOUT = 0x2003,
			}

			[DllImport(User32, CharSet = CharSet.Auto)]
			public static extern bool SystemParametersInfo(Actions uiAction, int uiParam, ref bool value, int ignore);


			[DllImport(User32, CharSet = CharSet.Auto)]
			public static extern bool SystemParametersInfo(Actions nAction, int nParam, ref DWORD value, int ignore);

		}

		private static void SystemParametersInfo(External.Actions action, int param, ref bool value, int ignore)
		{
			Console.WriteLine(value);



			bool extValue = value;
			if (!External.SystemParametersInfo(action, param, ref extValue, ignore )) throw new Exception(SPIExceptionMessage);
			value = extValue;


			Console.WriteLine(value);

		}

		private static void SystemParametersInfo(External.Actions action, int param, ref DWORD value, int ignore)
		{
			Console.WriteLine(value);


			DWORD extValue = value;
			if (!External.SystemParametersInfo(action, param, ref value, ignore )) throw new Exception(SPIExceptionMessage);
			value = extValue;



			Console.WriteLine(value);
		}

		public static bool Enabled
		{
			get
			{
				bool enabled = false;
				SystemParametersInfo(External.Actions.SPI_GETACTIVEWINDOWTRACKING, 0, ref enabled, 0);
				return enabled;
			}
			set { SystemParametersInfo(External.Actions.SPI_SETACTIVEWINDOWTRACKING, 0, ref value, 1); }
		}

		public static bool RaiseOnFocus {
			get
			{
				bool raise = false;
				SystemParametersInfo(External.Actions.SPI_GETACTIVEWNDTRKZORDER, 0, ref raise, 0);
				return raise;
			}
			set { SystemParametersInfo(External.Actions.SPI_SETACTIVEWNDTRKZORDER, 0, ref value, 1); }
		}

		public static UInt32 Timeout
		{
			get
			{
				DWORD timeout = 0;
				SystemParametersInfo(External.Actions.SPI_GETACTIVEWNDTRKTIMEOUT, 0, ref timeout, 0);
				return timeout;
			}
			set { SystemParametersInfo(External.Actions.SPI_SETACTIVEWNDTRKTIMEOUT, 0, ref value, 1); }
		}
	}


	class Program
	{

		public static void Report()
		{
			Console.WriteLine("-- Current Values");

			Console.Write("XMouse Enabled: ");
			Console.WriteLine(ActiveWindowTracking.Enabled);

			Console.Write("Autoraise: ");
			Console.WriteLine(ActiveWindowTracking.RaiseOnFocus);

			Console.Write("Tracking Timeout: ");
			Console.WriteLine(ActiveWindowTracking.Timeout);
		}

		static void Main(string[] args)
		{
			//ActiveWindowTracking.Timeout = 128;
			//ActiveWindowTracking.Enabled = true;

			Report();
			Console.ReadKey();

		}

		#region copypasta docs
		//SPI_GETACTIVEWINDOWTRACKING
		//0x1000
		//Determines whether active window tracking (activating the window the mouse is on) is on or off. The pvParam parameter must point to a BOOL variable that receives TRUE for on, or FALSE for off.
		//SPI_GETACTIVEWNDTRKZORDER
		//0x100C
		//Determines whether windows activated through active window tracking will be brought to the top. The pvParam parameter must point to a BOOL variable that receives TRUE for on, or FALSE for off.
		//SPI_GETACTIVEWNDTRKTIMEOUT
		//0x2002
		//Retrieves the active window tracking delay, in milliseconds. The pvParam parameter must point to a DWORD variable that receives the time.

		//SPI_SETACTIVEWINDOWTRACKING
		//0x1001
		//Sets active window tracking (activating the window the mouse is on) either on or off. Set pvParam to TRUE for on or FALSE for off.
		//SPI_SETACTIVEWNDTRKZORDER
		//0x100D
		//Determines whether or not windows activated through active window tracking should be brought to the top. Set pvParam to TRUE for on or FALSE for off.
		//SPI_SETACTIVEWNDTRKTIMEOUT
		//0x2003
		//Sets the active window tracking delay. Set pvParam to the number of milliseconds to delay before activating the window under the mouse pointer.



		//[DllImport("user32.dll", SetLastError = true)]
		//static extern bool SystemParametersInfo(int uiAction, int uiParam, IntPtr pvParam, int fWinIni);

		//[DllImport(User32, CharSet=CharSet.Auto)]
		//[ResourceExposure(ResourceScope.None)]
		// ... static extern etc
		#endregion
	}

	//static class Temp
	//{
	//	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	//	public static extern bool SystemParametersInfo(Actions nAction, int nParam, ref bool value, int ignore);


	//	public enum Actions
	//	{
	//		//Determines whether active window tracking (activating the window the mouse is on) is on or off. The pvParam parameter must point to a BOOL variable that receives TRUE for on, or FALSE for off.
	//		SPI_GETACTIVEWINDOWTRACKING = 0x1000,

	//		//Determines whether active window tracking (activating the window the mouse is on) is on or off. The pvParam parameter must point to a BOOL variable that receives TRUE for on, or FALSE for off.		//Determines whether windows activated through active window tracking will be brought to the top. The pvParam parameter must point to a BOOL variable that receives TRUE for on, or FALSE for off.
	//		SPI_GETACTIVEWNDTRKZORDER = 0x100C,

	//		// Retrieves the active window tracking delay, in milliseconds. The pvParam parameter must point to a DWORD variable that receives the time.
	//		SPI_GETACTIVEWNDTRKTIMEOUT = 0x2002,

	//		//Sets active window tracking (activating the window the mouse is on) either on or off. Set pvParam to TRUE for on or FALSE for off.
	//		SPI_SETACTIVEWINDOWTRACKING = 0x1001,

	//		//Determines whether or not windows activated through active window tracking should be brought to the top. Set pvParam to TRUE for on or FALSE for off.
	//		SPI_SETACTIVEWNDTRKZORDER = 0x100D,

	//		//Sets the active window tracking delay. Set pvParam to the number of milliseconds to delay before activating the window under the mouse pointer.
	//		SPI_SETACTIVEWNDTRKTIMEOUT = 0x2003,
	//	}

	//	public static bool Enabled
	//	{
	//		get
	//		{
	//			bool enabled = false;
	//			SystemParametersInfo(Actions.SPI_GETACTIVEWINDOWTRACKING, 0, ref enabled, 0);
	//			return enabled;
	//		}
	//		set { SystemParametersInfo(Actions.SPI_SETACTIVEWINDOWTRACKING, 0, ref value, 1); }
	//	}
	//}


}

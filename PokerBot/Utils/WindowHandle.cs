using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Utils
{
    public class WindowHandle
    {
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowText(IntPtr hWnd, [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr window, EnumWindowsProc callback, IntPtr lParam);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static String getWindowText(IntPtr hwnd)
        {
            int size = GetWindowTextLength(hwnd); 
            StringBuilder sb = new StringBuilder(size);
            GetWindowText(hwnd, sb, size);
            return sb.ToString();
        }

        public static IEnumerable<IntPtr> FindAllPokerTableWindow()
        {
            IntPtr found = IntPtr.Zero;
            List<IntPtr> windows = new List<IntPtr>();
            EnumWindows(delegate(IntPtr wndParent, IntPtr paramParent)
            {
                EnumChildWindows(wndParent, delegate(IntPtr wnd, IntPtr param)
                {
                    int size = GetWindowTextLength(wnd);
                    if (size++ > 0)
                    {
                        StringBuilder sb = new StringBuilder(size);
                        GetWindowText(wnd, sb, size);

                        //Debug.WriteLine(sb.ToString());
                        if (sb.ToString().Equals("Click here to learn about using your own image on our new poker table."))
                        {
                            windows.Add(wndParent);
                        }
                    }
                    return true;
                }, IntPtr.Zero);
                return true;
            },IntPtr.Zero);
            return windows;
        } // closing bracket
    }
}

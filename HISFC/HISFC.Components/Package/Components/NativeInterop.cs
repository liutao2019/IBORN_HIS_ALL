﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace HISFC.Components.Package.Components
{
    public class NativeInterop
    {
        public const int WM_PRINTCLIENT = 0x0318;
        public const int PRF_CLIENT = 0x00000004;

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public static bool IsWinXP
        {
            get
            {
                OperatingSystem OS = Environment.OSVersion;
                return (OS.Platform == PlatformID.Win32NT) &&
                    ((OS.Version.Major > 5) || ((OS.Version.Major == 5) && (OS.Version.Minor == 1)));
            }
        }

        public static bool IsWinVista
        {
            get
            {
                OperatingSystem OS = Environment.OSVersion;
                return (OS.Platform == PlatformID.Win32NT) && (OS.Version.Major >= 6);
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Universal_Game_DeathCounter
{
    static class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool wow64Process);
        public static bool IsWin64Emulator(Process process)
        {
            if ((Environment.OSVersion.Version.Major > 5)
                || ((Environment.OSVersion.Version.Major == 5) && (Environment.OSVersion.Version.Minor >= 1)))
            {
                bool retVal;
                if (!process.HasExited)
                {
                    return IsWow64Process(process.Handle, out retVal) && retVal;
                }
            }

            return false; // not on 64-bit Windows Emulator
        }
    }
}

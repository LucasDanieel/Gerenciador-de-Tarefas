using System.Runtime.InteropServices;

namespace Gerenciado_de_Tarefas.Services
{
    public static class PerformanceInfoService
    {
        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPerformanceInfo([Out] out PerformanceInfo PerformanceInfo, [In] int Size);

        [StructLayout(LayoutKind.Sequential)]
        public struct PerformanceInfo
        {
            public int Size;
            public IntPtr CommitTotal;
            public IntPtr CommitLimit;
            public IntPtr CommitPeak;
            public IntPtr PhysicalTotal;
            public IntPtr PhysicalAvailable;
            public IntPtr SystemCache;
            public IntPtr KernelTotal;
            public IntPtr KernelPaged;
            public IntPtr KernelNonPaged;
            public IntPtr PageSize;
            public int HandleCount;
            public int processCount;
            public int ThreadCount;
        }

        public static long GetPhysicalAvailableMemoryInMiB()
        {
            PerformanceInfo pi = new();

            if(GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
            {
                return Convert.ToInt64(pi.PhysicalAvailable.ToInt64() * pi.PageSize.ToInt64() / 1048576);
            }
            else
            {
                return -1;
            }
        }

        public static long GetTotalMemoryInMiB()
        {
            PerformanceInfo pi = new();

            if(GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
            {
                return Convert.ToInt64(pi.PhysicalTotal.ToInt64() * pi.PageSize.ToInt64() / 1048576);
            }
            else
            {
                return -1;
            }
        }
    }
}

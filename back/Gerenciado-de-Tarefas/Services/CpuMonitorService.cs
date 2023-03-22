using System.Diagnostics;
using System.Globalization;
using System.Management;

namespace Gerenciado_de_Tarefas.Services
{
    public class CpuMonitorService
    {
        public string Obter()
        {
            foreach (var obj in new ManagementObjectSearcher("select LoadPercentage from Win32_Processor").Get())
            {
                try
                {
                    if (obj.Properties["LoadPercentage"] != null)
                    {
                        return obj.Properties["LoadPercentage"].Value.ToString() + "%";
                    }
                }
                catch
                {
                    return "0";
                }
            }
            return "0";
        }
    }
}

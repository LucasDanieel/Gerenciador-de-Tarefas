using Gerenciado_de_Tarefas.Entities;
using System.Diagnostics;
using System.Globalization;

namespace Gerenciado_de_Tarefas.Services
{
    public class MonitorService
    {
        public Gerenciador ObterDadosDoComputador()
        {
            long phav = PerformanceInfoService.GetPhysicalAvailableMemoryInMiB();
            long tot = PerformanceInfoService.GetTotalMemoryInMiB();
            decimal porcentoLivre = ((decimal)phav / (decimal)tot) * 100;
            decimal porcentoOcupado = 100 - porcentoLivre;
            var memoriaFisicaDisponivel = string.Format("{0:#,##0.00}", phav) + " MB";
            var memoriaEmUso = string.Format("{0:#,##0.00}", tot - phav) + " MB";
            var memoriaTotal = string.Format("{0:#,##0.00}", tot) + " MB";
            var livre = porcentoLivre.ToString("F2", CultureInfo.InvariantCulture) + "%";
            var ocupada = porcentoOcupado.ToString("F2", CultureInfo.InvariantCulture) + "%";
            var porcentagemCpuMaquina = new CpuMonitorService().Obter();

            Process[] process = Process.GetProcesses();
            var qtdProcessos = process.Length;
            var qtdThreads = process.Sum(x => x.Threads.Count);

            return new Gerenciador
            {
                MemoriaDisponivel = memoriaFisicaDisponivel,
                MemoriaUsada = memoriaEmUso,
                MemoriaTotalDoComputador = memoriaTotal,
                MemoriaDisponivelPorcentagem = livre,
                MemoriaOcupadaPorcentagem = ocupada,
                UsoCpuMaquina = porcentagemCpuMaquina,
                QtdProcessos = qtdProcessos,
                QtdThreads = qtdThreads
            };
        }
    }
}

using Gerenciado_de_Tarefas.Entities;
using System.Diagnostics;

namespace Gerenciado_de_Tarefas.Services
{
    public class ProcessoService
    {

        public List<Processo> ObterTarefas()
        {
            Process[] processes = Process.GetProcesses();

            return processes
                .GroupBy(x => x.ProcessName)
                .Select(x => new
                {
                    x.First().ProcessName,
                    Cpu = x.Sum(y => ObterUsoCpuDoProcesso(y)),
                    Ram = x.Sum(y => y.WorkingSet64) / 1048576.0,
                    Threads = x.Sum(y => y.Threads.Count)
                }).Select(x => new Processo
                {
                    Nome = x.ProcessName,
                    Cpu = x.Cpu,
                    Ram = double.Parse(string.Format("{0:#,0.00}", x.Ram)),
                    Threads = x.Threads
                })
                .OrderByDescending(x => x.Ram)
                .ToList();
        }

        private decimal ObterUsoCpuDoProcesso(Process p)
        {
            try
            {
                TimeSpan time = DateTime.Now - p.StartTime;
                if (p.HasExited) time = p.ExitTime - p.StartTime;
                var tempoUsoProcesso = p.TotalProcessorTime;
                var cpuProcentagem = (decimal)(tempoUsoProcesso.TotalMilliseconds / time.TotalMilliseconds) / Environment.ProcessorCount;

                return Math.Round(cpuProcentagem * 100, 2);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}

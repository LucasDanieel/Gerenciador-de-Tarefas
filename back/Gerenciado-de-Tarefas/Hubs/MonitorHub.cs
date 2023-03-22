using Gerenciado_de_Tarefas.Services;
using Microsoft.AspNetCore.SignalR;

namespace Gerenciado_de_Tarefas.Hubs
{
    public class MonitorHub : Hub
    {
        private readonly MonitorService _monitorService;
        private readonly ProcessoService _processoService;
        public MonitorHub(MonitorService monitorService, ProcessoService processoService)
        {
            _monitorService = monitorService;
            _processoService = processoService;
        }

        public async Task ObterMonitor()
        {
            var monitor = _monitorService.ObterDadosDoComputador();

            monitor.Processos = _processoService.ObterTarefas();

            await Clients.Client(Context.ConnectionId).SendAsync("Monitor", monitor);
        }
    }
}

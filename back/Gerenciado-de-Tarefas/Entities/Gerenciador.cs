namespace Gerenciado_de_Tarefas.Entities
{
    public class Gerenciador
    {
        public string MemoriaDisponivel { get; set; }
        public string MemoriaUsada { get; set; }
        public string MemoriaTotalDoComputador { get; set; }
        public string MemoriaDisponivelPorcentagem { get; set; }
        public string MemoriaOcupadaPorcentagem { get; set; }
        public string UsoCpuMaquina { get; set; }
        public int QtdProcessos { get; set; }
        public int QtdThreads { get; set; }
        public List<Processo> Processos { get; set; }
    }
}

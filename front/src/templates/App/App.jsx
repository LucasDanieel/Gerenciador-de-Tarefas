import { HubConnectionState } from "@microsoft/signalr";
import { useEffect, useRef, useState } from "react";
import { TableRow } from "../../components/TableRow/TableRow";
import { criaConexao, iniciaConexao } from "../../services/conexao";
import "./App.css";

function App() {
  const [infoMonitor, setInfoMonitor] = useState({});
  var ref = useRef("cpu");
  var conexao = useRef();

  useEffect(() => {
    conexao.current = criaConexao();
    iniciaConexao();

    conexao.current.on("Monitor", (info) => {
      setInfoMonitor(filtro(info));
    });

    setInterval(() => {
      if (
        conexao.current.state === HubConnectionState.Disconnected ||
        conexao.current.state === HubConnectionState.Connecting
      )
        return;

      conexao.current.invoke("ObterMonitor");
    }, 3000);
  }, []);

  const changeFilter = (value) => {
    ref.current = value;
    if (!infoMonitor || !infoMonitor?.processos) return;
    setInfoMonitor(filtro(infoMonitor));
  };

  const filtro = (array) => {
    if (!infoMonitor) return;

    var { processos } = array;
    processos.sort((a, b) => {
      if (typeof processos[ref.current] === "string") {
        return b[ref.current].localeCompare(a[ref.current]);
      }

      return b[ref.current] - a[ref.current];
    });

    return { ...array, processos: processos };
  };

  return (
    <div className="container">
      <div className="header">
        <h3>Gerenciador de Tarefas</h3>
      </div>
      <div className="tabela-container">
        <div className="overflow">
          <table>
            <thead>
              <tr>
                <th onClick={() => changeFilter("nome")}>
                  <span>Process Name</span>
                </th>
                <th onClick={() => changeFilter("cpu")}>
                  <span>% CPU</span>
                </th>
                <th onClick={() => changeFilter("threads")}>
                  <span>Threads</span>
                </th>
                <th onClick={() => changeFilter("ram")}>
                  <span>Memoria</span>
                </th>
              </tr>
            </thead>
            <tbody>
              {infoMonitor.processos &&
                infoMonitor.processos.map((p, idx) => (
                  <TableRow
                    key={`${idx}-${p.nome}`}
                    nome={p.nome}
                    cpu={p.cpu}
                    threads={p.threads}
                    ram={p.ram}
                  />
                ))}
            </tbody>
          </table>
        </div>
      </div>
      <div className="info-container">
        <div className="info-computador">
          <div className="sistema">
            <div className="info">
              <span>Memória Disponivel:</span>
              <span id="ram-dis">{infoMonitor.memoriaDisponivel}</span>
            </div>
            <div className="info">
              <span>Memória Total:</span>
              <span id="ram-total">{infoMonitor.memoriaTotalDoComputador}</span>
            </div>
            <div className="info">
              <span>Memória Disponivel %:</span>
              <span id="ram-dis-porcentagem">{infoMonitor.memoriaDisponivelPorcentagem}</span>
            </div>
            <div className="info">
              <span>Memória Ocupada %:</span>
              <span id="ram-ocu-porcentagem">{infoMonitor.memoriaOcupadaPorcentagem}</span>
            </div>
          </div>
          <div className="sistema cpu">
            <span>CPU LOAD</span>
          </div>
          <div className="sistema threads">
            <div className="info">
              <span>Cpu:</span>
              <span id="cpu">{infoMonitor.usoCpuMaquina}</span>
            </div>
            <div className="info">
              <span>Threads:</span>
              <span id="threads">{infoMonitor.qtdThreads}</span>
            </div>
            <div className="info">
              <span>Processos:</span>
              <span id="processos">{infoMonitor.qtdProcessos}</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;

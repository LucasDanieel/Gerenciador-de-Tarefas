import { HubConnectionBuilder, HubConnectionState, LogLevel } from "@microsoft/signalr";

var conexao;

export function criaConexao() {
  if (!conexao) {
    conexao = new HubConnectionBuilder()
      .withUrl("https://localhost:7154/monitor")
      .configureLogging(LogLevel.Information)
      .build();

    conexao.onclose(() => iniciaConexao());

    return conexao;
  }
}

export function iniciaConexao() {
  if (conexao.state == HubConnectionState.Disconnected) {
    conexao
      .start()
      .then(() => {
        console.log("Conectado");
      })
      .catch((err) => {
        console.log("Erro ao conectar, Tentando novamente...");

        setTimeout(() => {
          iniciaConexao();
        }, 30000);
      });
  }
}

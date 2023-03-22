export const TableRow = ({nome, cpu, threads, ram}) => {

  return (
    <tr>
      <td>{nome}</td>
      <td>{cpu.toFixed(1).replace(".", ",")}%</td>
      <td>{threads}</td>
      <td>{ram.toLocaleString("pt-BR")} MB</td>
    </tr>
  );
};

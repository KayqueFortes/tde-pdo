namespace BancoApp;

public class ContaBancaria
{
    public decimal Saldo { get; private set; }

    private readonly List<string> _extrato = new();
    private readonly List<(decimal Valor, DateTime Data)> _agendamentos = new();

    public ContaBancaria(decimal saldoInicial)
    {
        Saldo = saldoInicial;
        _extrato.Add($"[{DateTime.Now:dd/MM/yyyy HH:mm}] Conta criada com saldo inicial: R$ {saldoInicial:F2}");
    }

    public void Depositar(decimal valor)
    {

        if (valor <= 0)
            throw new ArgumentException("O valor do depósito deve ser positivo.");

        Saldo += valor;
        _extrato.Add($"[{DateTime.Now:dd/MM/yyyy HH:mm}] Depósito: + R$ {valor:F2} | Saldo: R$ {Saldo:F2}");
    }

 
    public void Sacar(decimal valor)
    {
        
        if (valor <= 0)
            throw new ArgumentException("O valor do saque deve ser positivo.");


        Saldo -= valor;
        _extrato.Add($"[{DateTime.Now:dd/MM/yyyy HH:mm}] Saque: - R$ {valor:F2} | Saldo: R$ {Saldo:F2}");
    }

    public void Transferir(ContaBancaria destino, decimal valor)
    {
        if (destino == null)
            throw new ArgumentNullException(nameof(destino));

        Sacar(valor);
        destino.Depositar(valor);
        _extrato.Add($"[{DateTime.Now:dd/MM/yyyy HH:mm}] Transferência enviada: R$ {valor:F2}");
    }

   
    public IReadOnlyList<string> ExibirExtrato()
    {
        return _extrato.AsReadOnly();
    }

   
}

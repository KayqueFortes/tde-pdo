using BancoApp;

namespace BancoApp.Tests;

public class ContaBancariaTests
{
 
    // teste - Depositar

    [Fact]
    public void Depositar_DeveAdicionarSaldo()
    {
        var conta = new ContaBancaria(100);

        conta.Depositar(50);

        Assert.Equal(150, conta.Saldo);
    }

    [Fact]
    public void Depositar_ValorNegativo_DeveLancarExcecao()
    {
        var conta = new ContaBancaria(100);

        Assert.Throws<ArgumentException>(() => conta.Depositar(-10));
    }
 
    // teste — Sacar

    [Fact]
    public void Sacar_DeveRemoverSaldo()
    {
        var conta = new ContaBancaria(100);

        conta.Sacar(40);

        Assert.Equal(60, conta.Saldo);
    }

    [Fact]
    public void Sacar_ComSaldoInsuficiente_DeveLancarExcecao()
    {
        var conta = new ContaBancaria(100);

        Assert.Throws<InvalidOperationException>(() => conta.Sacar(200));
    }

    // Teste — Transferir

    [Fact]
    public void Transferir_DeveMoverValorEntreContas()
    {
        var origem = new ContaBancaria(200);
        var destino = new ContaBancaria(100);

        origem.Transferir(destino, 50);

        Assert.Equal(150, origem.Saldo);
        Assert.Equal(150, destino.Saldo);
    }

    [Fact]
    public void Transferir_SemSaldo_DeveLancarExcecao()
    {
        var origem = new ContaBancaria(30);
        var destino = new ContaBancaria(100);

        Assert.Throws<InvalidOperationException>(() => origem.Transferir(destino, 200));
    }

    [Fact]
    public void Transferir_DestinoNulo_DeveLancarExcecao()
    {
        var origem = new ContaBancaria(200);

        Assert.Throws<ArgumentNullException>(() => origem.Transferir(null!, 50));
    }

    // Teste — ExibirExtrato

    [Fact]
    public void ExibirExtrato_DeveRegistrarCriacaoDaConta()
    {
        var conta = new ContaBancaria(500);

        var extrato = conta.ExibirExtrato();

        Assert.Single(extrato);
        Assert.Contains("Conta criada", extrato[0]);
    }

    [Fact]
    public void ExibirExtrato_DeveRegistrarDeposito()
    {
        var conta = new ContaBancaria(100);
        conta.Depositar(50);

        var extrato = conta.ExibirExtrato();

        Assert.Equal(2, extrato.Count);
        Assert.Contains("Depósito", extrato[1]);
    }

    [Fact]
    public void ExibirExtrato_DeveRegistrarSaque()
    {
        var conta = new ContaBancaria(200);
        conta.Sacar(80);

        var extrato = conta.ExibirExtrato();

        Assert.Equal(2, extrato.Count);
        Assert.Contains("Saque", extrato[1]);
    }
}

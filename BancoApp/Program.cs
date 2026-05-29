using BancoApp;

// CONTAS
var contas = new Dictionary<string, (string Senha, string Nome, ContaBancaria Conta)>
{
    ["001"] = ("1234", "João Silva",  new ContaBancaria(1500)),
    ["002"] = ("5678", "Maria Souza", new ContaBancaria(800)),
};

// HELPERS
void Linha()  => Console.WriteLine(new string('─', 40));
void Espaco() => Console.WriteLine();

void Cabecalho(string titulo)
{
    Console.Clear();
    Linha();
    Console.WriteLine($"  BANCO TDE  —  {titulo}");
    Linha();
    Espaco();
}

// LOOP PRINCIPAL

while (true)
{

// LOGIN
string? agencia = null;
(string Senha, string Nome, ContaBancaria Conta) usuario = default;

while (true)
{
    Cabecalho("LOGIN");
    Console.Write("  Número da conta: ");
    agencia = Console.ReadLine()?.Trim();

    if (agencia == null || !contas.ContainsKey(agencia))
    {
        Console.WriteLine("\n  X Conta não encontrada.");
        Console.WriteLine("  Pressione Enter para tentar novamente...");
        Console.ReadLine();
        continue;
    }

    Console.Write("  Senha: ");
    string? senha = Console.ReadLine()?.Trim();

    if (senha != contas[agencia].Senha)
    {
        Console.WriteLine("\n  X Senha incorreta.");
        Console.WriteLine("  Pressione Enter para tentar novamente...");
        Console.ReadLine();
        continue;
    }

    usuario = contas[agencia];
    break;
}

// MENU PRINCIPAL
while (true)
{
    Cabecalho("MENU PRINCIPAL");
    Console.WriteLine($"  Bem-vindo(a), {usuario.Nome}!");
    Console.WriteLine($"  Conta: {agencia}");
    
    Espaco();
    Console.WriteLine("  [1] Ver saldo");
    Console.WriteLine("  [2] Depositar");
    Console.WriteLine("  [3] Sacar");
    Console.WriteLine("  [4] Transferir");
    Console.WriteLine("  [5] Extrato");
    Console.WriteLine("  [0] Sair / Trocar conta");
    Espaco();
    Console.Write("  Escolha: ");
    string? opcao = Console.ReadLine()?.Trim().ToUpper();

    switch (opcao)
    {
        case "1":
            Cabecalho("SALDO");
            Console.WriteLine($"  Titular      : {usuario.Nome}");
            Console.WriteLine($"  Conta        : {agencia}");
            Console.WriteLine($"  Saldo        : R$ {usuario.Conta.Saldo:F2}");
            Espaco();
            Console.WriteLine("  Pressione Enter para voltar...");
            Console.ReadLine();
            break;

        case "2":
            Cabecalho("DEPÓSITO");
            Console.Write("  Valor a depositar: R$ ");
            if (decimal.TryParse(Console.ReadLine()?.Replace(",", "."), out decimal valorDep))
            {
                try
                {
                    usuario.Conta.Depositar(valorDep);
                    Console.WriteLine($"\n  CONCLUIDO! Depósito de R$ {valorDep:F2} realizado!");
                    Console.WriteLine($"  Novo saldo: R$ {usuario.Conta.Saldo:F2}");
                }
                catch (Exception ex) { Console.WriteLine($"\n  ❌ Erro: {ex.Message}"); }
            }
            else Console.WriteLine("\n  X Valor inválido.");
            Espaco();
            Console.WriteLine("  Pressione Enter para voltar...");
            Console.ReadLine();
            break;

        case "3":
            Cabecalho("SAQUE");
            Console.Write("  Valor a sacar: R$ ");
            if (decimal.TryParse(Console.ReadLine()?.Replace(",", "."), out decimal valorSaq))
            {
                try
                {
                    usuario.Conta.Sacar(valorSaq);
                    Console.WriteLine($"\n  CONCLUIDO! Saque de R$ {valorSaq:F2} realizado!");
                    Console.WriteLine($"  Novo saldo: R$ {usuario.Conta.Saldo:F2}");
                }
                catch (Exception ex) { Console.WriteLine($"\n  X Erro: {ex.Message}"); }
            }
            else Console.WriteLine("\n  X Valor inválido.");
            Espaco();
            Console.WriteLine("  Pressione Enter para voltar...");
            Console.ReadLine();
            break;

        case "4":
            Cabecalho("TRANSFERÊNCIA");
            Console.Write("  Número da conta de destino: ");
            string? destKey = Console.ReadLine()?.Trim();
            if (destKey == null || !contas.ContainsKey(destKey) || destKey == agencia)
            {
                Console.WriteLine("\n  X Conta destino inválida.");
            }
            else
            {
                var destino = contas[destKey];
                Console.Write($"  Valor a transferir para {destino.Nome}: R$ ");
                if (decimal.TryParse(Console.ReadLine()?.Replace(",", "."), out decimal valorTrans))
                {
                    try
                    {
                        usuario.Conta.Transferir(destino.Conta, valorTrans);
                        Console.WriteLine($"\n  CONCLUIDO! Transferência de R$ {valorTrans:F2} realizada!");
                        Console.WriteLine($"  Para: {destino.Nome} (conta {destKey})");
                        Console.WriteLine($"  Seu saldo: R$ {usuario.Conta.Saldo:F2}");
                    }
                    catch (Exception ex) { Console.WriteLine($"\n  X Erro: {ex.Message}"); }
                }
                else Console.WriteLine("\n  X Valor inválido.");
            }
            Espaco();
            Console.WriteLine("  Pressione Enter para voltar...");
            Console.ReadLine();
            break;

        case "5":
            Cabecalho("EXTRATO");
            var extrato = usuario.Conta.ExibirExtrato();
            foreach (var linha in extrato)
                Console.WriteLine($"  {linha}");
            Espaco();
            Console.WriteLine("  Pressione Enter para voltar...");
            Console.ReadLine();
            break;

        case "0":
            Cabecalho("ATÉ LOGO!");
            Console.WriteLine($"  Sessão encerrada. Até logo, {usuario.Nome}.");
            Espaco();
            Console.WriteLine("  Pressione Enter para voltar ao login...");
            Console.ReadLine();
            goto sairDoMenu;

        default:
            Console.WriteLine("\n  X Opção inválida.");
            System.Threading.Thread.Sleep(1000);
            break;
    }
}

sairDoMenu:;

} // fim do loop principal

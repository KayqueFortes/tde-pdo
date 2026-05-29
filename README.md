# Sistema Bancário — TDE

Projeto de demonstração de **Testes Unitários** e **CI/CD** com GitHub Actions.

## Estrutura

```
SistemaBancario/
├── BancoApp/               → Classe principal
│   └── ContaBancaria.cs
├── BancoApp.Tests/         → Testes unitários (xUnit)
│   └── ContaBancariaTests.cs
├── SistemaBancario.sln
└── .github/workflows/
    └── main.yml            → Pipeline CI/CD
```

## Rodar localmente

```bash
dotnet test
```

## Pipeline

O GitHub Actions executa automaticamente os testes a cada push na branch `main`.

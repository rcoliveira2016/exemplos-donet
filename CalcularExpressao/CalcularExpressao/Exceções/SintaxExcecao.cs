using CalcularExpressao.Tokens;

namespace CalcularExpressao.Exceções;

public class SintaxExcecao : Exception
{
    public SintaxExcecao(string? message) : base(message)
    {
    }
}

public class OperacaoSintaxExcecao : SintaxExcecao
{
    public readonly eTokenOperacao Operacao;
    public OperacaoSintaxExcecao(string? message, eTokenOperacao operacao) : base(message)
    {
        Operacao = operacao;
    }
}

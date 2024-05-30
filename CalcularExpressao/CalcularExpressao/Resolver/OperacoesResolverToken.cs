using CalcularExpressao.Extencoes;
using CalcularExpressao.Tokens;

namespace CalcularExpressao.Resolver;

internal static class OperacoesResolverToken
{
    private static readonly char charDividir = eTokenOperacao.Dividir.ToChar();
    private static readonly char charMultiplicar = eTokenOperacao.Multiplicar.ToChar();
    private static readonly char charSomar = eTokenOperacao.Somar.ToChar();
    private static readonly char charSubtrair = eTokenOperacao.Subtrair.ToChar();
    public static bool TentarResolver(char charAtual, out TokenOperacao tokenOperacaoOut)
    {
        if(charAtual == charDividir)
        {
            tokenOperacaoOut = TokenOperacao.Create(eTokenOperacao.Dividir);
            return true;
        }

        if (charAtual == charMultiplicar)
        {
            tokenOperacaoOut = TokenOperacao.Create(eTokenOperacao.Multiplicar);
            return true;
        }

        if (charAtual == charSomar)
        {
            tokenOperacaoOut = TokenOperacao.Create(eTokenOperacao.Somar);
            return true;
        }

        if (charAtual == charSubtrair)
        {
            tokenOperacaoOut = TokenOperacao.Create(eTokenOperacao.Subtrair);
            return true;
        }

        tokenOperacaoOut = null;
        return false;
    }
}

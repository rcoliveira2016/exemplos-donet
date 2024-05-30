using CalcularExpressao.Tokens;

namespace CalcularExpressao.Resolver;

internal static class NumerosResolverToken
{
    internal static bool TentarResolver(string texto, int i, out TokenNumero tokenNumeroOut, out int indexFinal)
    {
        if (!char.IsDigit(texto[i]))
        {
            tokenNumeroOut = null;
            indexFinal = 0;
            return false;
        }

        int j = i;
        for (; j < texto.Length; j++) {
            if (!char.IsDigit(texto[j]))
            {
                break;
            }
        }

        tokenNumeroOut = TokenNumero.Criar(Convert.ToInt32(texto.Substring(i, j - i)));
        indexFinal = j-1;
        return true;
    }
}
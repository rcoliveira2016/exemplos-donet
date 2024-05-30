using CalcularExpressao.Extencoes;
using CalcularExpressao.Tokens;

namespace CalcularExpressao.Resolver;

internal static class ParentesesResolverToken
{
    private static readonly char charAbre = eTokenParentese.Abre.ToChar();
    private static readonly char charFecha = eTokenParentese.Fecha.ToChar();
    public static bool TentarResolver(char charAtual, out TokenParentese tokenParenteseOut)
    {
        if(charAtual == charAbre)
        {
            tokenParenteseOut = TokenParentese.Create(eTokenParentese.Abre);
            return true;
        }

        if (charAtual == charFecha)
        {
            tokenParenteseOut = TokenParentese.Create(eTokenParentese.Fecha);
            return true;
        }

        tokenParenteseOut = null;
        return false;
    }
}

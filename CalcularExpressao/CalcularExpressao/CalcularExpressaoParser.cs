using CalcularExpressao.Tokens;

namespace CalcularExpressao;

public class CalcularExpressaoParser
{
    private readonly string Texto;
    private readonly TokenRoot TokenRoot;
    private TokenBase TokenCorrent;

    public CalcularExpressaoParser(string texto)
    {
        Texto = texto;
        TokenRoot = new TokenRoot(texto);
        TokenCorrent = TokenRoot;
    }

    public TokenRoot Parser()
    {

        return TokenRoot; 
    }

}

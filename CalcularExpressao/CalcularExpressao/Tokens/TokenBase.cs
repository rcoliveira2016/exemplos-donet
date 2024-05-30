namespace CalcularExpressao.Tokens;

public abstract class TokenBase
{
    public abstract eTokenBase Tipo { get; }
    public TokenBase? Pai { get; protected set; }
    public IList<TokenBase> Filhos { get; protected init; } = new List<TokenBase>();

    public TokenBase AddFilho(TokenBase token)
    {
        token.Pai = token;
        Filhos.Add(token);
        return token;
    }

    public abstract string GetText();
}
public enum eTokenBase
{
    Root,
    Operacao,
    Numeros,
    Parentese
}

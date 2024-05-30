namespace CalcularExpressao.Tokens;

public class TokenRoot : TokenBase
{
    private readonly string texto;

    public TokenRoot(string texto)
    {
        this.texto = texto;
    }

    public override eTokenBase Tipo => eTokenBase.Root;

    public override string GetText() => string.Empty;

    public string ToStringTree()
    {
        throw new NotImplementedException();
    }
}
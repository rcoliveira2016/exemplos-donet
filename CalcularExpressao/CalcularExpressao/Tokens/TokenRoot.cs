using System.Text;

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
        var retorno = new StringBuilder();
        retorno.Append(ToStringTree(this));
        return retorno.ToString();
    }

    private string ToStringTree(TokenBase tokens)
    {
        var retorno = new StringBuilder();
        retorno.Append(tokens.GetText());
        foreach (var token in tokens.Filhos) {
            retorno.Append(ToStringTree(token));
        }
        return retorno.ToString();
    }
}
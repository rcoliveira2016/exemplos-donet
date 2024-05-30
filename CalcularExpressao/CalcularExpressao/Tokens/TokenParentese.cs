
namespace CalcularExpressao.Tokens;

public class TokenParentese : TokenBase
{
    public override eTokenBase Tipo => eTokenBase.Parentese;
    public eTokenParentese TipoParentese { get; set; }
    internal static TokenParentese Create(eTokenParentese tipo) => new()
    {
        TipoParentese = tipo
    };

    public override string GetText()
    {
        return ((char)TipoParentese).ToString();
    }
}
public enum eTokenParentese
{
    Abre='(',
    Fecha=')'
}

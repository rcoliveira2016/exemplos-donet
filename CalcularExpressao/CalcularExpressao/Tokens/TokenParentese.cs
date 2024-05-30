namespace CalcularExpressao.Tokens;

public class TokenParentese : TokenBase
{
    public override eTokenBase Tipo => eTokenBase.Parentese;

    public override string GetText()
    {
        return ((char)Tipo).ToString();
    }
}
public enum eTokenParentese
{
    abre='(',
    fecha=')'
}

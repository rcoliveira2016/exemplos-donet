namespace CalcularExpressao.Tokens;

public class TokenOperacao : TokenBase
{
    public override eTokenBase Tipo => eTokenBase.Operacao;

    public override string GetText()
    {
        return ((char)Tipo).ToString();
    }
}
public enum eTokenOperacao
{
    somar='+',
    subtrair='-',
    multiplicar='*',
    dividir='/'
}


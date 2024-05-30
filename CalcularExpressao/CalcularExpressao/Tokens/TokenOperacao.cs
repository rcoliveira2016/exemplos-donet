
namespace CalcularExpressao.Tokens;

public class TokenOperacao : TokenBase
{
    public override eTokenBase Tipo => eTokenBase.Operacao;
    public eTokenOperacao TipoOperacao { get; set; }

    internal static TokenOperacao Create(eTokenOperacao tipo) => new TokenOperacao()
    {
        TipoOperacao = tipo,
    };

    public override string GetText()
    {
        return ((char)TipoOperacao).ToString();
    }
}
public enum eTokenOperacao
{
    Somar='+',
    Subtrair='-',
    Multiplicar='*',
    Dividir='/'
}


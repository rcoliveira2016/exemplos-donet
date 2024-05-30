namespace CalcularExpressao.Tokens;

public class TokenNumero : TokenBase
{
    public override eTokenBase Tipo => eTokenBase.Numeros;

    public int Valor { get; set; }

    public override string GetText()
    {
        return Valor.ToString();
    }
}
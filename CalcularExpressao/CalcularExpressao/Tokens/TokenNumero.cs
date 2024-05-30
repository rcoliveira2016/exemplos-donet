
namespace CalcularExpressao.Tokens;

public class TokenNumero : TokenBase
{
    public override eTokenBase Tipo => eTokenBase.Numeros;

    public int Valor { get; set; }

    internal static TokenNumero Criar(int valor) => new()
    {
        Valor = valor,
    };

    public override string GetText()
    {
        return Valor.ToString();
    }
}
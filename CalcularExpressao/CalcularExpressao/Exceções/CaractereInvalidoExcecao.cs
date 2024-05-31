namespace CalcularExpressao.Exceções;

public class CaractereInvalidoExcecao: Exception
{
    public char Caractere { get; private init; }
    public override string Message => MontarMensagem();
    public CaractereInvalidoExcecao(char caractere)
    {
        Caractere = caractere;
    }

    private string MontarMensagem()
    {
        return $"Caractere invalido '{Caractere}'";
    }
}

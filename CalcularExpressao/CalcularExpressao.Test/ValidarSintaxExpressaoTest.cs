using CalcularExpressao.Exceções;

namespace CalcularExpressao.Test;
[TestClass]
public class ValidarSintaxExpressaoTest
{

    [DataTestMethod]
    [DataRow("-768")]
    [DataRow("-(768)")]
    [DataRow("768-1*(1)(2)*(11*33(1212+1010)*78)")]
    [DataRow("1*(1)")]
    public void TesteComSucesso(string expressao)
    {
        var parse = new CalcularExpressaoParser(expressao);
        var token = parse.Parser();
        var validar = new ValidarSintaxExpressao(token);
        validar.Validar();
    }

    [DataTestMethod]
    [DataRow("768-")]
    [DataRow("-")]
    [DataRow("1(-)")]
    [DataRow("768-1*(11*33-78)/")]
    [DataRow("768-+1*(11*33--78)")]
    [DataRow("768-1*(11*33(1212+1010-)*78)")]
    //
    [DataRow("(1)768+1*(1))(")]
    [DataRow("(1)768+1*(1)()")]
    [DataRow("(1)(2)768+1*(1)((1))(1)(")]
    [DataRow("(1)(22)768+1*(1)((1))(1)(1))")]
    public void TesteComErro(string expressao)
    {
        var parse = new CalcularExpressaoParser(expressao);
        var token = parse.Parser();
        var validar = new ValidarSintaxExpressao(token);
        Assert.ThrowsException<SintaxExcecao>(()=> validar.Validar());
    }
}

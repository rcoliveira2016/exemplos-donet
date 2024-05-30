namespace CalcularExpressao.Test;

[TestClass]
public class UnitTest1
{
    [DataTestMethod]
    //teste normais
    [DataRow("768", 1)]
    [DataRow("768(11*33-78)", 3)]
    [DataRow("768/11*33(-78+30)", 7)]
    [DataRow("(10+11+11)4-33(-78+30(110*(10+11)))",7)]
    //teste Zuados
    [DataRow("768++++", 5)]
    [DataRow("768(11*33-78)))()()(", 10)]
    [DataRow("768/11*33(-78+30)111111)))", 11)]
    [DataRow(")-)-(10+11+11)4-33(-78+30(110*(10+11)))", 11)]
    public void TesteSucesso(string expressao, int filhoNivel1)
    {
        var parse = new CalcularExpressaoParser(expressao);
        var tokens = parse.Parser();
        Assert.AreEqual(filhoNivel1, tokens.Filhos.Count, tokens.ToStringTree());
        Assert.AreEqual(expressao, tokens.ToStringTree());
    }
}
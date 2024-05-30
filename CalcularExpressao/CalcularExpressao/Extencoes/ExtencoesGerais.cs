namespace CalcularExpressao.Extencoes;

internal static class ExtencoesGerais
{
    public static char ToChar<T>(this T @enum) where T: Enum => Convert.ToChar(@enum);
}

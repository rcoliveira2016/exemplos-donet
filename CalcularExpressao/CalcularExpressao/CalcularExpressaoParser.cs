﻿using CalcularExpressao.Exceções;
using CalcularExpressao.Resolver;
using CalcularExpressao.Tokens;

namespace CalcularExpressao;

public class CalcularExpressaoParser
{
    private readonly string Texto;
    private readonly TokenRoot TokenRoot;
    private TokenBase TokenCorrent;

    public CalcularExpressaoParser(string texto)
    {
        TokenRoot = new TokenRoot(texto);
        TokenCorrent = TokenRoot;
        Texto = texto;
    }

    public TokenRoot Parser()
    {        
        MontarTokenRoot();
        return TokenRoot; 
    }

    private void MontarTokenRoot()
    {
        TokenParentese tokenParenteseOut;
        TokenOperacao tokenOperacaoOut;
        TokenNumero tokenNumeroOut;
        for (int i = 0; i < Texto.Length; i++)
        {
            var charAtual = Texto[i];
            if (ParentesesResolverToken.TentarResolver(charAtual, out tokenParenteseOut))
            {
                SetarTokeParenteses(tokenParenteseOut);
                continue;
            }

            if (OperacoesResolverToken.TentarResolver(charAtual, out tokenOperacaoOut))
            {
                TokenCorrent.AddFilho(tokenOperacaoOut);
                continue;
            }

            if (NumerosResolverToken.TentarResolver(Texto, i, out tokenNumeroOut, out var indexFinal))
            {
                i = indexFinal;
                TokenCorrent.AddFilho(tokenNumeroOut);
                continue;
            }

            throw new CaractereInvalidoExcecao(charAtual);

        }
    }

    private void SetarTokeParenteses(TokenParentese tokenParenteseOut)
    {
        if(tokenParenteseOut.TipoParentese == eTokenParentese.Abre)
        {
            TokenCorrent = TokenCorrent.AddFilho(tokenParenteseOut);
        }
        if (tokenParenteseOut.TipoParentese == eTokenParentese.Fecha)
        {
            TokenCorrent = TokenCorrent.Pai == null? TokenRoot: TokenCorrent.Pai;
            TokenCorrent.AddFilho(tokenParenteseOut);

        }
    }
}

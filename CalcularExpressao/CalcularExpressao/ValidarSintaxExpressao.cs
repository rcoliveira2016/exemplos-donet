﻿using CalcularExpressao.Exceções;
using CalcularExpressao.Tokens;

namespace CalcularExpressao;

public class ValidarSintaxExpressao
{
    private readonly TokenRoot TokenRoot;
    private TokenBase TokenCorrent;

    public ValidarSintaxExpressao(TokenRoot token)
    {
        TokenRoot = token;
        TokenCorrent = TokenRoot;
    }

    public void Validar()
    {
        ValidarFilhoToken(TokenRoot);
    }

    private void ValidarFilhoToken(TokenBase tokenRoot)
    {
        for (int i = 0; i < tokenRoot.Filhos.Count; i++)
        {
            var atual = tokenRoot.Filhos[i];
            var anterior = i > 0 ? tokenRoot.Filhos[i - 1] : null;
            var proximo = i < tokenRoot.Filhos.Count - 1 ? tokenRoot.Filhos[i + 1] : null;
            switch (atual.Tipo)
            {
                case eTokenBase.Operacao:
                    ValidarTokenOperacao((atual as TokenOperacao)!, proximo, anterior);
                    break;
                case eTokenBase.Numeros:
                    ValidarTokenNumeros(atual, proximo, anterior);
                    break;
                case eTokenBase.Parentese:
                    ValidarTokenParentese((atual as TokenParentese)!, proximo, anterior);
                    break;
                default:
                    break;
            }
        }
    }

    private void ValidarTokenParentese(TokenParentese atual, TokenBase? proximo, TokenBase? anterior)
    {
        if(atual.TipoParentese == eTokenParentese.Abre)
        {
            if(proximo==null)
                DispararExcecaoSintax("o parenteses não foi fechado");

            if (proximo!.Tipo != eTokenBase.Parentese)
                DispararExcecaoSintax("o parenteses não foi fechado");

            if (proximo.Tipo == eTokenBase.Parentese && (proximo as TokenParentese)!.TipoParentese != eTokenParentese.Fecha)
                DispararExcecaoSintax("o parenteses não foi fechado");

        }

        if (atual.TipoParentese == eTokenParentese.Fecha)
        {
            if (anterior == null)
                DispararExcecaoSintax("o parenteses não foi aberto");

            if (anterior!.Tipo != eTokenBase.Parentese)
                DispararExcecaoSintax("o parenteses não foi aberto");

            if (anterior.Tipo == eTokenBase.Parentese && (anterior as TokenParentese)!.TipoParentese != eTokenParentese.Abre)
                DispararExcecaoSintax("o parenteses não foi aberto");

            if (!anterior.Filhos.Any())
                DispararExcecaoSintax("o expreção entre paraentese esta vazia");

            ValidarFilhoToken(anterior!);
        }
    }

    private void ValidarTokenNumeros(TokenBase atual, TokenBase? proximo, TokenBase? anterior)
    {
        if (proximo != null && proximo.Tipo == eTokenBase.Numeros || anterior != null && anterior.Tipo == eTokenBase.Numeros)
            DispararExcecaoSintax("numeros não podem esta juntas");
    }

    private void ValidarTokenOperacao(TokenOperacao atual, TokenBase? proximo, TokenBase? anterior)
    {
        if (proximo == null && anterior == null)
            DispararExcecaoSintaxOperacao("Uma operação não pode ser isola", atual.TipoOperacao);

        if (proximo != null && proximo.Tipo == eTokenBase.Operacao || anterior != null && anterior.Tipo == eTokenBase.Operacao)
            DispararExcecaoSintaxOperacao("operação não podem esta juntas", atual.TipoOperacao);

        if (proximo == null)
            DispararExcecaoSintaxOperacao("operação não pode ficar em uma extremidade", atual.TipoOperacao);

        if (anterior == null && atual.TipoOperacao != eTokenOperacao.Subtrair && proximo?.Tipo == eTokenBase.Numeros)
            DispararExcecaoSintaxOperacao("apenas a operação de subtrarir pode esta a esquerda de um numero no inicio da expressao", atual.TipoOperacao);
    }

    private void DispararExcecaoSintaxOperacao(string mensagem, eTokenOperacao operacao)
    {
        throw new OperacaoSintaxExcecao(mensagem, operacao);
    }

    private void DispararExcecaoSintax(string mensagem)
    {
        throw new SintaxExcecao(mensagem);
    }
}

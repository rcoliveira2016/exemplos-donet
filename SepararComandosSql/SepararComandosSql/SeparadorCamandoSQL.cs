using System.Text;

namespace SepararComandosSql;


public class SeparadorCamandoSQL
{
    private const char separatorCommand = ';';
    private readonly StringBuilder CurrentCommand = new StringBuilder();
    private List<string>? commands = null;
    private bool InsideString = false;
    private bool InsideDeclareBlock = false;
    private int NestedBlockLevel = 0;

    public SeparadorCamandoSQL(string sqlCommands)
    {
        this.SqlCommand = sqlCommands;
    }
    public string SqlCommand { get; private init; } = string.Empty;

    public List<string> Split()
    {
        if(commands is not null) return commands;
        commands = new List<string>();

        for (int i = 0; i < SqlCommand.Length; i++)
        {
            char currentChar = SqlCommand[i];
            
            if (CheckCharWithStructIgnoreSeparator(currentChar, ref i)) continue;
            
            if (currentChar == separatorCommand && !InsideString && !InsideDeclareBlock)
            {
                commands.Add(CurrentCommand.ToString().Trim());
                CurrentCommand.Clear();
                continue;
            }

            CurrentCommand.Append(currentChar);
        }

        var lastCommand = CurrentCommand.ToString().Trim();
        if (!string.IsNullOrEmpty(lastCommand))
            commands.Add(lastCommand);

        return commands;
    }

    private bool CheckCharWithStructIgnoreSeparator(char currentChar, ref int indexLoopCommand){
        if (currentChar == '\'')
        {
            InsideString = !InsideString; // Toggle insideString status
            CurrentCommand.Append(currentChar);
            return true;
        }

        if(InsideString) return false;

        if (IsKeyword(SqlCommand, indexLoopCommand, SQLKeywords.DECLARE))
        {
            InsideDeclareBlock = true;
            NestedBlockLevel++;
            CurrentCommand.Append(SQLKeywords.DECLARE);
            indexLoopCommand += SQLKeywords.DECLARE.Length - 1; // Move the index to the end of "DECLARE"
            return true;
        }

        if (!InsideDeclareBlock && IsKeyword(SqlCommand, indexLoopCommand, SQLKeywords.BEGIN))
        {
            InsideDeclareBlock = true;
            NestedBlockLevel++;
            CurrentCommand.Append(SQLKeywords.BEGIN);
            indexLoopCommand += SQLKeywords.BEGIN.Length - 1; // Move the index to the end of "BEGIN"
            return true;
        }

        if (InsideDeclareBlock && IsKeyword(SqlCommand, indexLoopCommand, SQLKeywords.CASE))
        {
            NestedBlockLevel++;
            CurrentCommand.Append(SQLKeywords.CASE);
            indexLoopCommand += SQLKeywords.CASE.Length - 1; // Move the index to the end of "CASE"
            return true;
        }
        if (
            InsideDeclareBlock &&
            IsKeyword(SqlCommand, indexLoopCommand, SQLKeywords.END) &&
            !IsKeyword(SqlCommand, indexLoopCommand, SQLKeywords.END_IF) &&
            !IsKeyword(SqlCommand, indexLoopCommand, SQLKeywords.END_LOOP))
        {
            NestedBlockLevel--;
            CurrentCommand.Append(SQLKeywords.END);
            indexLoopCommand += SQLKeywords.END.Length - 1; // Move the index to the end of "END"

            if (NestedBlockLevel == 0)
                InsideDeclareBlock = false;
            
            return true;
        }

        if (IsKeyword(SqlCommand, indexLoopCommand, SQLKeywords.CREATE) 
            && (TermCreateWithStructIgnoreSeparator(SQLKeywords.PACKAGE, ref indexLoopCommand)
            || TermCreateWithStructIgnoreSeparator(SQLKeywords.PROCEDURE, ref indexLoopCommand)
            || TermCreateWithStructIgnoreSeparator(SQLKeywords.FUNCTION, ref indexLoopCommand)))
            return true;


        return false;
    }

    private bool TermCreateWithStructIgnoreSeparator(string nameStructure, ref int indexLoopCommand)
    {
        var indexAtual = indexLoopCommand+ SQLKeywords.CREATE.Length;
        if(!TryNextKeyword(SqlCommand, indexAtual, out var indexNextKeyword))
        {
            return false;
        }

        if (IsKeyword(SqlCommand, indexNextKeyword, nameStructure))
        {
            NestedBlockLevel++;
            var tamnhoString = indexNextKeyword - indexAtual + SQLKeywords.PACKAGE.Length;
            CurrentCommand.Append(SqlCommand.Substring(indexLoopCommand, tamnhoString));
            indexLoopCommand += tamnhoString - 1;
            InsideDeclareBlock = true;
            return true;
        }

        if (!IsKeyword(SqlCommand, indexNextKeyword, SQLKeywords.OR)) return false;

        var indeKeywordOr = indexNextKeyword + SQLKeywords.OR.Length;

        if (!TryNextKeyword(SqlCommand, indeKeywordOr, out var indexNextKeywordAux))
            return false;
        if(!IsKeyword(SqlCommand, indexNextKeywordAux, SQLKeywords.REPLACE))
            return false;
        if(!TryNextKeyword(SqlCommand, indexNextKeywordAux + SQLKeywords.REPLACE.Length, out indexNextKeywordAux))
            return false;

        if (IsKeyword(SqlCommand, indexNextKeywordAux, nameStructure))
        {
            NestedBlockLevel++;
            var tamnhoString = indexNextKeywordAux - indexLoopCommand + nameStructure.Length;
            CurrentCommand.Append(SqlCommand.Substring(indexLoopCommand, tamnhoString));
            indexLoopCommand += tamnhoString - 1;
            InsideDeclareBlock = true;
            return true;
        }

        return false;
    }

    private static bool TryNextKeyword(string sqlCommands, int indexInit, out int indexNextKeyword)
    {
        var sqlComandLength = sqlCommands.Length;
        if (indexInit>= sqlComandLength)
        {
            indexNextKeyword = default;
            return false;
        }
        
        for (var i = indexInit; i < sqlComandLength; i++) {

            if (char.IsLetter(sqlCommands[i]))
            {
                indexNextKeyword = i;
                return true;
            }

        }

        indexNextKeyword = default;
        return false;
    }

    private static bool IsKeyword(string sqlCommands, int index, string keyword)
    {
        if (index + keyword.Length > sqlCommands.Length)
            return false;

        for (int j = 0; j < keyword.Length; j++)
        {
            if (char.ToUpper(sqlCommands[index + j]) != keyword[j])
                return false;
        }

        if (index + keyword.Length < sqlCommands.Length && char.IsLetterOrDigit(sqlCommands[index + keyword.Length]))
            return false;

        return true;
    }
}

using System.Linq;

namespace SepararComandosSql.Test;

[TestClass]
public class SeparadorCamandoSQLTest
{
    [TestMethod]
    public void SeparSimples()
    {
        string input = @"





            DELETE FROM users WHERE name = 'John';



        ";

        List<string> commands = new SeparadorCamandoSQL(input).Split();

        Assert.AreEqual(commands.Count, 1);

        input = "SELECT\r\n                    coluna1,\r\n                    coluna2,\r\n                    CASE\r\n                        WHEN condition1 THEN result1\r\n                        WHEN condition2 THEN result2\r\n                        WHEN conditionN THEN resultN\r\n                        ELSE result\r\n                    END\r\n                from tabela;";
        commands = new SeparadorCamandoSQL(input).Split();

        Assert.AreEqual(commands.Count, 1);
    }
    [TestMethod]
    public void SeparComBEGIN()
    {
        string input = @"
            BEGIN
                SELECT * FROM users;
                SELECT
                    coluna1,
                    coluna2,
                    CASE
                        WHEN condition1 THEN result1
                        WHEN condition2 THEN result2
                        WHEN conditionN THEN resultN
                        ELSE result
                    END
                from tabela;
                IF myVar > 0 THEN
                    INSERT INTO logs (message) VALUES ('Inside block');
                END IF;
            END;
            INSERT INTO users (name, age) VALUES ('John', 30);
            UPDATE users SET age = 31 WHERE name = 'John';
            DELETE FROM users WHERE name = 'John'
        ";

        List<string> commands = new SeparadorCamandoSQL(input).Split();
        Assert.AreEqual(commands.Count, 4);
    }

    [TestMethod]
    public void SeparComPkg()
    {
        var input = new string[] {@"CREATE OR REPLACE PACKAGE funcionario AS
              // get nome completo do funcionario
              FUNCTION get_nomeCompleto(n_func_id NUMBER)
                RETURN VARCHAR2;
              // get salario do funcionario
              FUNCTION get_salario(n_func_id NUMBER)
                RETURN NUMBER;
            END funcionario",
            @"CREATE   PACKAGE funcionario AS
              // get nome completo do funcionario
              FUNCTION get_nomeCompleto(n_func_id NUMBER)
                RETURN VARCHAR2;
              // get salario do funcionario
              FUNCTION get_salario(n_func_id NUMBER)
                RETURN NUMBER;
            END funcionario",
            "INSERT INTO users (name, age) VALUES ('John', 30)",
            "UPDATE users SET age = 31 WHERE name = 'John'",
            "DELETE FROM users WHERE name = 'John'" };

        List<string> commands = new SeparadorCamandoSQL(string.Join(";", input)).Split();
        Assert.AreEqual(5, commands.Count);
        foreach (var command in commands)
        {
            Assert.IsTrue(input.Contains(command), $"{command}");
        }
    }

    [TestMethod]
    public void SeparComProc()
    {
        var input = new string[] {@"CREATE OR REPLACE PROCEDURE AddEmployee (
    p_first_name IN VARCHAR2,
    p_last_name IN VARCHAR2,
    p_email IN VARCHAR2,
    p_hire_date IN DATE
) AS
BEGIN
    INSERT INTO employees (first_name, last_name, email, hire_date)
    VALUES (p_first_name, p_last_name, p_email, p_hire_date);
INSERT INTO employees (first_name, last_name, email, hire_date)
    VALUES (p_first_name, p_last_name, p_email, p_hire_date);
INSERT INTO employees (first_name, last_name, email, hire_date)
    VALUES (p_first_name, p_last_name, p_email, p_hire_date);
END",
            "INSERT INTO users (name, age) VALUES ('John', 30)",
            "UPDATE users SET age = 31 WHERE name = 'John'",
            "DELETE FROM users WHERE name = 'John'" };

        List<string> commands = new SeparadorCamandoSQL(string.Join(";", input)).Split();
        Assert.AreEqual(4, commands.Count);
        foreach (var command in commands)
        {
            Assert.IsTrue(input.Contains(command), $"{command}");
        }
    }
}
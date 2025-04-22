using System;

public static class Validators
{
    /// <summary>
    /// Запрашивает у пользователя ввод целого числа с проверкой корректности.
    /// </summary>
    public static int GetValidatedInt(string prompt)
    {
        int value;
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out value))
        {
            Console.Write("Некорректный ввод. " + prompt);
        }
        return value;
    }

    /// <summary>
    /// Запрашивает ввод чётного целого числа.
    /// Если число нечётное, запрашивает повторный ввод.
    /// </summary>
    public static int GetValidatedEvenInt(string prompt)
    {
        int value = GetValidatedInt(prompt);
        while (value % 2 != 0)
        {
            Console.WriteLine("Введённое число не является чётным. Повторите ввод.");
            value = GetValidatedInt(prompt);
        }
        return value;
    }

    /// <summary>
    /// Запрашивает у пользователя имя файла и проверяет, что оно не пустое.
    /// </summary>
    public static string GetValidatedFileName(string prompt)
    {
        Console.Write(prompt);
        string fileName = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(fileName))
        {
            Console.Write("Имя файла не может быть пустым. " + prompt);
            fileName = Console.ReadLine();
        }
        return fileName;
    }
}

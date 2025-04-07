namespace Task2
{
    // Интерфейс для проверки и чтения вводимых с клавиатуры данных
    public interface IInputValidator
    {
        byte ReadValidByte(string prompt, byte min, byte max);

        uint ReadValidUInt(string prompt);
    }
}
namespace Task2
{
    // ��������� ��� �������� � ������ �������� � ���������� ������
    public interface IInputValidator
    {
        byte ReadValidByte(string prompt, byte min, byte max);

        uint ReadValidUInt(string prompt);
    }
}
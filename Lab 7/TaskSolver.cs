using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TasksProject
{
    #region ������� 5. ������ � ������ ���������� (XML-������������)
    [Serializable]
    public class BaggageItem
    {
        public string ItemName { get; set; }
        public double Mass { get; set; }

        public BaggageItem() { }

        public BaggageItem(string itemName, double mass)
        {
            ItemName = itemName;
            Mass = mass;
        }
    }

    [Serializable]
    public class Passenger
    {
        public BaggageItem[] BaggageItems { get; set; }

        public Passenger() { }

        public Passenger(BaggageItem[] items)
        {
            BaggageItems = items;
        }
    }

    [Serializable, XmlRoot("BaggageData")]
    public class BaggageData
    {
        public Passenger[] Passengers { get; set; }

        public BaggageData() { }

        public BaggageData(Passenger[] passengers)
        {
            Passengers = passengers;
        }
    }
    #endregion

    public class TaskSolver
    {
        #region ������� 1. ��������� ���� (����� �� ����� ������)
        // ���������� ����� ��� ������� 1 (������������� ������������, ��� ��� ��������� �������� �����)
        private static void FillFileTask1(string filePath, int count, int min, int max)
        {
            Random rnd = new Random();
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                for (int i = 0; i < count; i++)
                    sw.WriteLine(rnd.Next(min, max + 1));
            }
        }

        // ��������� �������� ���� ������ � ������ ������� �����
        public static void Task1_ProcessFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int totalCount = lines.Length;
            if (totalCount % 2 != 0)
            {
                Console.WriteLine("Task 1 Error: ���������� ����� � ����� ������ ���� ������.");
                return;
            }
            int half = totalCount / 2, sumFirst = 0, sumSecond = 0;
            for (int i = 0; i < half; i++)
            {
                if (int.TryParse(lines[i], out int num))
                    sumFirst += num;
            }
            for (int i = half; i < totalCount; i++)
            {
                if (int.TryParse(lines[i], out int num))
                    sumSecond += num;
            }
            Console.WriteLine("Task 1: �������� ���� ������ � ������ ������� ����� = " + (sumFirst - sumSecond));
        }
        #endregion

        #region ������� 2. ��������� ���� (��������� ����� � ������)
        private static void FillFileTask2(string filePath, int lineCount, int numbersPerLine, int min, int max)
        {
            Random rnd = new Random();
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                for (int i = 0; i < lineCount; i++)
                {
                    string line = "";
                    for (int j = 0; j < numbersPerLine; j++)
                    {
                        line += rnd.Next(min, max + 1).ToString();
                        if (j < numbersPerLine - 1)
                            line += " ";
                    }
                    sw.WriteLine(line);
                }
            }
        }

        public static void Task2_SumElements(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int totalSum = 0;
            foreach (string line in lines)
            {
                string[] tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string token in tokens)
                {
                    if (int.TryParse(token, out int num))
                        totalSum += num;
                }
            }
            Console.WriteLine("Task 2: ����� ���� ����� � ����� = " + totalSum);
        }
        #endregion

        #region ������� 3. ��������� ���� � �������
        // ���� ��� ������� 3 ��� ������ � �������� �����.
        public static void Task3_CopyMinMaxLines(string sourceFilePath, string destFilePath)
        {
            if (!File.Exists(sourceFilePath))
            {
                Console.WriteLine("Task 3: ���� � �������� ������� �� ������.");
                return;
            }
            string[] lines = File.ReadAllLines(sourceFilePath);
            if (lines.Length == 0)
            {
                Console.WriteLine("Task 3: �������� ���� ����.");
                return;
            }
            string minLine = lines[0], maxLine = lines[0];
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].Length < minLine.Length)
                    minLine = lines[i];
                if (lines[i].Length > maxLine.Length)
                    maxLine = lines[i];
            }
            using (StreamWriter sw = new StreamWriter(destFilePath))
            {
                sw.WriteLine("����� �������� ������:");
                sw.WriteLine(minLine);
                sw.WriteLine();
                sw.WriteLine("����� ������� ������:");
                sw.WriteLine(maxLine);
            }
            Console.WriteLine("Task 3: ���������� �������� � ����: " + destFilePath);
        }
        #endregion

        #region ������� 4. �������� �����
        private static void FillBinaryFileTask4(string filePath, int count, int min, int max)
        {
            Random rnd = new Random();
            using (BinaryWriter bw = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                for (int i = 0; i < count; i++)
                    bw.Write(rnd.Next(min, max + 1));
            }
        }

        public static void Task4_FilterEvenNumbers(string sourceFilePath, string destFilePath)
        {
            using (BinaryReader br = new BinaryReader(File.Open(sourceFilePath, FileMode.Open)))
            using (BinaryWriter bw = new BinaryWriter(File.Open(destFilePath, FileMode.Create)))
            {
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    int num = br.ReadInt32();
                    if (num % 2 == 0)
                        bw.Write(num);
                }
            }
            Console.WriteLine("Task 4: ׸���� ����� �������� � �������� ����: " + destFilePath);
        }
        #endregion

        #region ������� 5. �������� ����� � ��������� (XML-������������)
        public static void FillFileTask5(string filePath)
        {
            Random rnd = new Random();
            string[] baggageNames = new string[] { "�������", "�����", "�������", "������", "�����" };

            int passengerCount = 3; // ����� ����������
            Passenger[] passengers = new Passenger[passengerCount];
            for (int i = 0; i < passengerCount; i++)
            {
                int itemsCount = rnd.Next(2, 6);
                BaggageItem[] items = new BaggageItem[itemsCount];
                for (int j = 0; j < itemsCount; j++)
                {
                    string itemName = baggageNames[rnd.Next(baggageNames.Length)];
                    double mass = Math.Round(rnd.NextDouble() * 45 + 5, 2); // ����� �� 5 �� 50 ��
                    items[j] = new BaggageItem(itemName, mass);
                }
                passengers[i] = new Passenger(items);
            }
            BaggageData data = new BaggageData(passengers);
            XmlSerializer serializer = new XmlSerializer(typeof(BaggageData));
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                serializer.Serialize(sw, data);
            }
            Console.WriteLine("Task 5: ������ � ������ ���������� �������� � ���� (XML): " + filePath);
        }

        public static void Task5_ProcessBaggageData(string filePath, double m)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BaggageData));
            BaggageData data;
            using (StreamReader sr = new StreamReader(filePath))
            {
                data = (BaggageData)serializer.Deserialize(sr);
            }
            if (data == null || data.Passengers == null || data.Passengers.Length == 0)
            {
                Console.WriteLine("Task 5: ��� ������ � ������.");
                return;
            }
            double totalMass = 0;
            int totalCount = 0;
            foreach (Passenger p in data.Passengers)
            {
                if (p.BaggageItems != null)
                {
                    foreach (BaggageItem item in p.BaggageItems)
                    {
                        totalMass += item.Mass;
                        totalCount++;
                    }
                }
            }
            if (totalCount == 0)
            {
                Console.WriteLine("Task 5: ��� ������ ������.");
                return;
            }
            double overallAverage = totalMass / totalCount;
            Console.WriteLine("Task 5: ����� ������� ����� ������� ������ = " + overallAverage.ToString("F2") + " ��");
            Console.WriteLine("���������, � ������� ������� ����� ������� ������ ���������� �� ����� ��� �� " + m + " ��:");
            for (int i = 0; i < data.Passengers.Length; i++)
            {
                double passengerTotal = 0;
                int countItems = 0;
                if (data.Passengers[i].BaggageItems != null)
                {
                    foreach (BaggageItem item in data.Passengers[i].BaggageItems)
                    {
                        passengerTotal += item.Mass;
                        countItems++;
                    }
                }
                if (countItems == 0)
                    continue;
                double passengerAverage = passengerTotal / countItems;
                if (Math.Abs(passengerAverage - overallAverage) <= m)
                {
                    Console.WriteLine("  �������� " + (i + 1) + " (������� �����: " + passengerAverage.ToString("F2") + " ��):");
                    foreach (BaggageItem item in data.Passengers[i].BaggageItems)
                    {
                        Console.WriteLine("    " + item.ItemName + " - " + item.Mass + " ��");
                    }
                }
            }
        }
        #endregion

        #region ������� 6. List.
        // �������� �� ������ ���� ��������� ��������� ��������; ������ �������� �������.
        public static void Task6_RemoveElementsFromList()
        {
            Console.WriteLine("Task 6: �������� ��������� �� ������.");
            int n = InputValidator.ReadValidInt("������� ���������� ��������� � ������: ", 1, 100);
            List<object> list = new List<object>();
            Console.WriteLine("������� �������� ������ (�����, ������ � �.�.):");
            for (int i = 0; i < n; i++)
            {
                object element = InputValidator.ReadValidElement($"������� {i + 1}: ");
                list.Add(element);
            }
            Console.WriteLine("\n�������� ������:");
            PrintList(list);
            object elementToRemove = InputValidator.ReadValidElement("\n������� �������, ������� ����� �������: ");
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].Equals(elementToRemove))
                    list.RemoveAt(i);
            }
            Console.WriteLine("\n������ ����� ��������:");
            PrintList(list);
        }
        #endregion

        #region ������� 7. LinkedList.
        // ����� � �������� ������� ��������� LinkedList; ������ �������� �������.
        public static void Task7_PrintLinkedListReverse()
        {
            Console.WriteLine("Task 7: ����� ��������� LinkedList � �������� �������.");
            int n = InputValidator.ReadValidInt("������� ���������� ��������� � ������: ", 1, 100);
            LinkedList<object> linkedList = new LinkedList<object>();
            Console.WriteLine("������� �������� ������ (�����, ������ � �.�.):");
            for (int i = 0; i < n; i++)
            {
                object element = InputValidator.ReadValidElement($"������� {i + 1}: ");
                linkedList.AddLast(element);
            }
            Console.WriteLine("\n�������� ������ (������ �������):");
            foreach (object item in linkedList)
                Console.Write(item + " ");
            Console.WriteLine();
            Console.WriteLine("\n������ � �������� �������:");
            LinkedListNode<object> current = linkedList.Last;
            while (current != null)
            {
                Console.Write(current.Value + " ");
                current = current.Previous;
            }
            Console.WriteLine();
        }
        #endregion

        #region ������� 8. HashSet.
        public static void Task8_ProcessProcurements()
        {
            HashSet<string> allFirms = new HashSet<string>() { "FirmA", "FirmB", "FirmC", "FirmD", "FirmE", "FirmF", "FirmG" };
            HashSet<string> school1 = new HashSet<string>() { "FirmA", "FirmB", "FirmC" };
            HashSet<string> school2 = new HashSet<string>() { "FirmB", "FirmC", "FirmD" };
            HashSet<string> school3 = new HashSet<string>() { "FirmC", "FirmD", "FirmE" };

            HashSet<string> intersection = new HashSet<string>(school1);
            intersection.IntersectWith(school2);
            intersection.IntersectWith(school3);

            HashSet<string> union = new HashSet<string>(school1);
            union.UnionWith(school2);
            union.UnionWith(school3);

            HashSet<string> notPurchased = new HashSet<string>(allFirms);
            notPurchased.ExceptWith(union);

            Console.WriteLine("Task 8:");
            Console.WriteLine("�����, ������� � ������� ����������� ������ �� ���������:");
            foreach (string firm in intersection)
                Console.WriteLine("  " + firm);
            Console.WriteLine("\n�����, ������� � ������� ����������� ���� �� ����� �� ���������:");
            foreach (string firm in union)
                Console.WriteLine("  " + firm);
            Console.WriteLine("\n�����, � ������� �� ���� ��������� �� �������� ����������:");
            foreach (string firm in notPurchased)
                Console.WriteLine("  " + firm);
        }
        #endregion

        #region ������� 9. HashSet.
        // ���� � ������� ��� ������� 9 ��� ������ (�� �������� �������������).
        public static void Task9_PrintVoicedConsonants(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Task 9: ���� � ������� �� ������.");
                return;
            }
            string text = File.ReadAllText(filePath).ToLower();
            HashSet<char> voicedConsonants = new HashSet<char>() { '�', '�', '�', '�', '�', '�', '�', '�', '�', '�' };
            HashSet<char> found = new HashSet<char>();
            foreach (char ch in text)
            {
                if (char.IsLetter(ch) && voicedConsonants.Contains(ch))
                    found.Add(ch);
            }
            List<char> sorted = new List<char>(found);
            sorted.Sort();
            Console.WriteLine("Task 9: ������� ��������� (� ���������� �������):");
            foreach (char ch in sorted)
                Console.Write(ch + " ");
            Console.WriteLine();
        }
        #endregion

        #region ������� 10. Dictionary/SortedList.
        // ������ ���������� �� ���������� ����� � ����������� ���������� ����� ��� ������� �������.
        public static void Task10_GenerateLoginsFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Task 10: ���� � ������� �� ������. �������� ���� " + filePath + " � �������.");
                Console.WriteLine("������ ����������� �����:");
                Console.WriteLine("5");
                Console.WriteLine("������� �����");
                Console.WriteLine("������ ������");
                Console.WriteLine("������� ���������");
                Console.WriteLine("������ ����");
                Console.WriteLine("������� ������");
                return;
            }
            string[] allLines = File.ReadAllLines(filePath);
            if (allLines.Length == 0)
            {
                Console.WriteLine("Task 10: ���� ����.");
                return;
            }
            if (!int.TryParse(allLines[0], out int countStudents))
            {
                Console.WriteLine("Task 10: ������ ������ ����� ������ ��������� ���������� ��������.");
                return;
            }
            if (countStudents != (allLines.Length - 1))
            {
                Console.WriteLine("Task 10: ���������� ��������, ��������� � ������ ������, �� ������������� ����� �������.");
                return;
            }
            Dictionary<string, int> surnameCounts = new Dictionary<string, int>();
            Console.WriteLine("Task 10: ��������������� ������:");
            for (int i = 1; i < allLines.Length; i++)
            {
                string entry = allLines[i];
                string[] tokens = entry.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length < 2)
                {
                    Console.WriteLine("������������ ������: " + entry);
                    continue;
                }
                string surname = tokens[0];
                if (surnameCounts.ContainsKey(surname))
                    surnameCounts[surname]++;
                else
                    surnameCounts[surname] = 1;
                int currentCount = surnameCounts[surname];
                string login = surname + (currentCount > 1 ? currentCount.ToString() : "");
                Console.WriteLine(login);
            }
        }
        #endregion

        // ---------------------------------------------------
        // ��������������� ����� ��� ������ ��������� ������ (Task 6).
        private static void PrintList<T>(List<T> list)
        {
            foreach (T item in list)
                Console.Write(item + " ");
            Console.WriteLine();
        }

        // ---------------------------------------------------
        // ����� RunTask: ����� ������� �� ������.
        public static void RunTask(int taskNumber)
        {
            switch (taskNumber)
            {
                case 1:
                    {
                        string file = "task1.txt";
                        int count = 10; // ������ ���� ������ �����
                        FillFileTask1(file, count, 1, 100);
                        Task1_ProcessFile(file);
                        break;
                    }
                case 2:
                    {
                        string file = "task2.txt";
                        FillFileTask2(file, 5, 4, 1, 50);
                        Task2_SumElements(file);
                        break;
                    }
                case 3:
                    {
                        // ���� � ������� ��� ���������� ��� ������� 3.
                        string src = "task3_source.txt";
                        string dest = "task3_dest.txt";
                        // ��������� ������� ��������� �����
                        if (!File.Exists(src))
                            Console.WriteLine("Task 3: ���� " + src + " �� ������.");
                        else
                            Task3_CopyMinMaxLines(src, dest);
                        break;
                    }
                case 4:
                    {
                        string src = "task4_source.bin";
                        string dest = "task4_dest.bin";
                        FillBinaryFileTask4(src, 20, 1, 100);
                        Task4_FilterEvenNumbers(src, dest);
                        break;
                    }
                case 5:
                    {
                        string file = "task5.xml";
                        FillFileTask5(file);
                        Task5_ProcessBaggageData(file, 5.0);
                        break;
                    }
                case 6:
                    {
                        Task6_RemoveElementsFromList();
                        break;
                    }
                case 7:
                    {
                        Task7_PrintLinkedListReverse();
                        break;
                    }
                case 8:
                    {
                        Task8_ProcessProcurements();
                        break;
                    }
                case 9:
                    {
                        // ���� � ������� ��� ���������� ��� ������� 9.
                        string file = "task9.txt";
                        if (!File.Exists(file))
                            Console.WriteLine("Task 9: ���� " + file + " �� ������.");
                        else
                            Task9_PrintVoicedConsonants(file);
                        break;
                    }
                case 10:
                    {
                        // ���� "task10.txt" ������ ���� ����������� � �������.
                        Task10_GenerateLoginsFromFile("task10.txt");
                        break;
                    }
                default:
                    Console.WriteLine("�������� ����� �������.");
                    break;
            }
        }
    }
}

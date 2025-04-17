using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TasksProject
{
    #region Задание 5. Данные о багаже пассажиров (XML-сериализация)
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
        #region Задание 1. Текстовый файл (числа по одной строке)
        // Заполнение файла для задания 1 (автоматически генерируется, так как требуется создание файла)
        private static void FillFileTask1(string filePath, int count, int min, int max)
        {
            Random rnd = new Random();
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                for (int i = 0; i < count; i++)
                    sw.WriteLine(rnd.Next(min, max + 1));
            }
        }

        // Вычисляет разность сумм первой и второй половин файла
        public static void Task1_ProcessFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int totalCount = lines.Length;
            if (totalCount % 2 != 0)
            {
                Console.WriteLine("Task 1 Error: количество чисел в файле должно быть чётным.");
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
            Console.WriteLine("Task 1: Разность сумм первой и второй половин файла = " + (sumFirst - sumSecond));
        }
        #endregion

        #region Задание 2. Текстовый файл (несколько чисел в строке)
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
            Console.WriteLine("Task 2: Сумма всех чисел в файле = " + totalSum);
        }
        #endregion

        #region Задание 3. Текстовый файл с текстом
        // Файл для задания 3 уже создан и содержит текст.
        public static void Task3_CopyMinMaxLines(string sourceFilePath, string destFilePath)
        {
            if (!File.Exists(sourceFilePath))
            {
                Console.WriteLine("Task 3: Файл с исходным текстом не найден.");
                return;
            }
            string[] lines = File.ReadAllLines(sourceFilePath);
            if (lines.Length == 0)
            {
                Console.WriteLine("Task 3: Исходный файл пуст.");
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
                sw.WriteLine("Самая короткая строка:");
                sw.WriteLine(minLine);
                sw.WriteLine();
                sw.WriteLine("Самая длинная строка:");
                sw.WriteLine(maxLine);
            }
            Console.WriteLine("Task 3: Результаты записаны в файл: " + destFilePath);
        }
        #endregion

        #region Задание 4. Бинарные файлы
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
            Console.WriteLine("Task 4: Чётные числа записаны в бинарный файл: " + destFilePath);
        }
        #endregion

        #region Задание 5. Бинарные файлы и структуры (XML-сериализация)
        public static void FillFileTask5(string filePath)
        {
            Random rnd = new Random();
            string[] baggageNames = new string[] { "чемодан", "сумка", "коробка", "рюкзак", "пакет" };

            int passengerCount = 3; // число пассажиров
            Passenger[] passengers = new Passenger[passengerCount];
            for (int i = 0; i < passengerCount; i++)
            {
                int itemsCount = rnd.Next(2, 6);
                BaggageItem[] items = new BaggageItem[itemsCount];
                for (int j = 0; j < itemsCount; j++)
                {
                    string itemName = baggageNames[rnd.Next(baggageNames.Length)];
                    double mass = Math.Round(rnd.NextDouble() * 45 + 5, 2); // масса от 5 до 50 кг
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
            Console.WriteLine("Task 5: Данные о багаже пассажиров записаны в файл (XML): " + filePath);
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
                Console.WriteLine("Task 5: Нет данных о багаже.");
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
                Console.WriteLine("Task 5: Нет единиц багажа.");
                return;
            }
            double overallAverage = totalMass / totalCount;
            Console.WriteLine("Task 5: Общая средняя масса единицы багажа = " + overallAverage.ToString("F2") + " кг");
            Console.WriteLine("Пассажиры, у которых средняя масса единицы багажа отличается не более чем на " + m + " кг:");
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
                    Console.WriteLine("  Пассажир " + (i + 1) + " (средняя масса: " + passengerAverage.ToString("F2") + " кг):");
                    foreach (BaggageItem item in data.Passengers[i].BaggageItems)
                    {
                        Console.WriteLine("    " + item.ItemName + " - " + item.Mass + " кг");
                    }
                }
            }
        }
        #endregion

        #region Задание 6. List.
        // Удаление из списка всех вхождений заданного элемента; список вводится вручную.
        public static void Task6_RemoveElementsFromList()
        {
            Console.WriteLine("Task 6: Удаление элементов из списка.");
            int n = InputValidator.ReadValidInt("Введите количество элементов в списке: ", 1, 100);
            List<object> list = new List<object>();
            Console.WriteLine("Введите элементы списка (числа, строки и т.д.):");
            for (int i = 0; i < n; i++)
            {
                object element = InputValidator.ReadValidElement($"Элемент {i + 1}: ");
                list.Add(element);
            }
            Console.WriteLine("\nИсходный список:");
            PrintList(list);
            object elementToRemove = InputValidator.ReadValidElement("\nВведите элемент, который нужно удалить: ");
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].Equals(elementToRemove))
                    list.RemoveAt(i);
            }
            Console.WriteLine("\nСписок после удаления:");
            PrintList(list);
        }
        #endregion

        #region Задание 7. LinkedList.
        // Вывод в обратном порядке элементов LinkedList; список вводится вручную.
        public static void Task7_PrintLinkedListReverse()
        {
            Console.WriteLine("Task 7: Вывод элементов LinkedList в обратном порядке.");
            int n = InputValidator.ReadValidInt("Введите количество элементов в списке: ", 1, 100);
            LinkedList<object> linkedList = new LinkedList<object>();
            Console.WriteLine("Введите элементы списка (числа, строки и т.д.):");
            for (int i = 0; i < n; i++)
            {
                object element = InputValidator.ReadValidElement($"Элемент {i + 1}: ");
                linkedList.AddLast(element);
            }
            Console.WriteLine("\nИсходный список (прямой порядок):");
            foreach (object item in linkedList)
                Console.Write(item + " ");
            Console.WriteLine();
            Console.WriteLine("\nСписок в обратном порядке:");
            LinkedListNode<object> current = linkedList.Last;
            while (current != null)
            {
                Console.Write(current.Value + " ");
                current = current.Previous;
            }
            Console.WriteLine();
        }
        #endregion

        #region Задание 8. HashSet.
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
            Console.WriteLine("Фирмы, закупка в которых проводилась каждым из заведений:");
            foreach (string firm in intersection)
                Console.WriteLine("  " + firm);
            Console.WriteLine("\nФирмы, закупка в которых проводилась хотя бы одним из заведений:");
            foreach (string firm in union)
                Console.WriteLine("  " + firm);
            Console.WriteLine("\nФирмы, в которых ни одно заведение не закупало компьютеры:");
            foreach (string firm in notPurchased)
                Console.WriteLine("  " + firm);
        }
        #endregion

        #region Задание 9. HashSet.
        // Файл с текстом для задания 9 уже создан (не создаётся автоматически).
        public static void Task9_PrintVoicedConsonants(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Task 9: Файл с текстом не найден.");
                return;
            }
            string text = File.ReadAllText(filePath).ToLower();
            HashSet<char> voicedConsonants = new HashSet<char>() { 'б', 'в', 'г', 'д', 'ж', 'з', 'л', 'м', 'н', 'р' };
            HashSet<char> found = new HashSet<char>();
            foreach (char ch in text)
            {
                if (char.IsLetter(ch) && voicedConsonants.Contains(ch))
                    found.Add(ch);
            }
            List<char> sorted = new List<char>(found);
            sorted.Sort();
            Console.WriteLine("Task 9: Звонкие согласные (в алфавитном порядке):");
            foreach (char ch in sorted)
                Console.Write(ch + " ");
            Console.WriteLine();
        }
        #endregion

        #region Задание 10. Dictionary/SortedList.
        // Данные получаются из текстового файла и формируется уникальный логин для каждого ученика.
        public static void Task10_GenerateLoginsFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Task 10: Файл с данными не найден. Создайте файл " + filePath + " с данными.");
                Console.WriteLine("Пример содержимого файла:");
                Console.WriteLine("5");
                Console.WriteLine("Иванова Мария");
                Console.WriteLine("Петров Сергей");
                Console.WriteLine("Бойцова Екатерина");
                Console.WriteLine("Петров Иван");
                Console.WriteLine("Иванова Наташа");
                return;
            }
            string[] allLines = File.ReadAllLines(filePath);
            if (allLines.Length == 0)
            {
                Console.WriteLine("Task 10: Файл пуст.");
                return;
            }
            if (!int.TryParse(allLines[0], out int countStudents))
            {
                Console.WriteLine("Task 10: Первая строка файла должна содержать количество учеников.");
                return;
            }
            if (countStudents != (allLines.Length - 1))
            {
                Console.WriteLine("Task 10: Количество учеников, указанное в первой строке, не соответствует числу записей.");
                return;
            }
            Dictionary<string, int> surnameCounts = new Dictionary<string, int>();
            Console.WriteLine("Task 10: Сгенерированные логины:");
            for (int i = 1; i < allLines.Length; i++)
            {
                string entry = allLines[i];
                string[] tokens = entry.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length < 2)
                {
                    Console.WriteLine("Некорректная запись: " + entry);
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
        // Вспомогательный метод для вывода элементов списка (Task 6).
        private static void PrintList<T>(List<T> list)
        {
            foreach (T item in list)
                Console.Write(item + " ");
            Console.WriteLine();
        }

        // ---------------------------------------------------
        // Метод RunTask: выбор задания по номеру.
        public static void RunTask(int taskNumber)
        {
            switch (taskNumber)
            {
                case 1:
                    {
                        string file = "task1.txt";
                        int count = 10; // должно быть чётное число
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
                        // Файл с текстом уже существует для задания 3.
                        string src = "task3_source.txt";
                        string dest = "task3_dest.txt";
                        // Проверяем наличие исходного файла
                        if (!File.Exists(src))
                            Console.WriteLine("Task 3: Файл " + src + " не найден.");
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
                        // Файл с текстом уже существует для задания 9.
                        string file = "task9.txt";
                        if (!File.Exists(file))
                            Console.WriteLine("Task 9: Файл " + file + " не найден.");
                        else
                            Task9_PrintVoicedConsonants(file);
                        break;
                    }
                case 10:
                    {
                        // Файл "task10.txt" должен быть подготовлен с данными.
                        Task10_GenerateLoginsFromFile("task10.txt");
                        break;
                    }
                default:
                    Console.WriteLine("Неверный номер задания.");
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml.Serialization;


namespace TasksProject
{
    public static class Tasks
    {
        // Задание 1 – Текстовые файлы. Разность сумм половин.

        public static void ExecuteTask1()
        {
            Console.WriteLine("Задание 1: Файл с числами (по одному значению в строке).");
            string fileName = Validators.GetValidatedFileName("Введите имя файла для задания 1 (например, task1.txt): ");
            int count = Validators.GetValidatedEvenInt("Введите количество чисел (четное число): ");

            // Если файл существует, проверяем его корректность.
            if (File.Exists(fileName))
            {
                if (ValidateTask1File(fileName, count))
                {
                    Console.WriteLine($"Файл {fileName} существует и соответствует условиям. Будет использован существующий файл.");
                }
                else
                {
                    Console.WriteLine($"Файл {fileName} существует, но не соответствует условиям. Он будет перезаписан.");
                    Task1_FillFile(fileName, count);
                }
            }
            else
            {
                Console.WriteLine($"Файл {fileName} не найден. Он будет создан.");
                Task1_FillFile(fileName, count);
            }

            double difference = Task1_ComputeDifference(fileName);
            Console.WriteLine("Разность суммы первой и второй половины файла: " + difference);
        }

        private static bool ValidateTask1File(string fileName, int expectedCount)
        {
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                if (lines.Length != expectedCount || lines.Length % 2 != 0)
                    return false;
                foreach (string line in lines)
                {
                    if (!double.TryParse(line, out double num))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при проверке файла: " + ex.Message);
                return false;
            }
        }

        public static void Task1_FillFile(string fileName, int count)
        {
            Random rnd = new Random();
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                for (int i = 0; i < count; i++)
                {
                    sw.WriteLine(rnd.Next(1, 101)); // Числа от 1 до 100
                }
            }
        }

        public static double Task1_ComputeDifference(string fileName)
        {
            double[] numbers;
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                int count = lines.Length;
                if (count % 2 != 0)
                {
                    Console.WriteLine("В файле содержится нечётное количество элементов.");
                    return 0;
                }
                numbers = new double[count];
                for (int i = 0; i < count; i++)
                {
                    numbers[i] = double.Parse(lines[i]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при чтении файла: " + ex.Message);
                return 0;
            }
            int mid = numbers.Length / 2;
            double sumFirstHalf = 0, sumSecondHalf = 0;
            for (int i = 0; i < mid; i++)
                sumFirstHalf += numbers[i];
            for (int i = mid; i < numbers.Length; i++)
                sumSecondHalf += numbers[i];
            return sumFirstHalf - sumSecondHalf;
        }



        // Задание 2 – Текстовые файлы. Вычисление суммы элементов.

        public static void ExecuteTask2()
        {
            Console.WriteLine("Задание 2: Файл с числами (несколько элементов в строке) – вычисление суммы элементов.");
            string fileName = Validators.GetValidatedFileName("Введите имя файла для задания 2 (например, task2.txt): ");
            int rows = Validators.GetValidatedInt("Введите количество строк: ");
            int numbersPerRow = Validators.GetValidatedInt("Введите количество чисел в каждой строке: ");

            if (File.Exists(fileName))
            {
                if (ValidateTask2File(fileName, rows, numbersPerRow))
                {
                    Console.WriteLine($"Файл {fileName} существует и соответствует условиям. Будет использован существующий файл.");
                }
                else
                {
                    Console.WriteLine($"Файл {fileName} существует, но не соответствует условиям. Он будет перезаписан.");
                    Task2_FillFile(fileName, rows, numbersPerRow);
                }
            }
            else
            {
                Console.WriteLine($"Файл {fileName} не найден. Он будет создан.");
                Task2_FillFile(fileName, rows, numbersPerRow);
            }

            double sum = Task2_ComputeSum(fileName);
            Console.WriteLine("Сумма всех элементов файла: " + sum);
        }

        private static bool ValidateTask2File(string fileName, int rows, int numbersPerRow)
        {
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                if (lines.Length != rows)
                    return false;
                foreach (string line in lines)
                {
                    string[] tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens.Length != numbersPerRow)
                        return false;
                    foreach (string token in tokens)
                    {
                        if (!double.TryParse(token, out double num))
                            return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при проверке файла: " + ex.Message);
                return false;
            }
        }

        public static void Task2_FillFile(string fileName, int rows, int numbersPerRow)
        {
            Random rnd = new Random();
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                for (int i = 0; i < rows; i++)
                {
                    string line = "";
                    for (int j = 0; j < numbersPerRow; j++)
                    {
                        line += rnd.Next(1, 101).ToString();
                        if (j < numbersPerRow - 1)
                            line += " ";
                    }
                    sw.WriteLine(line);
                }
            }
        }

        public static double Task2_ComputeSum(string fileName)
        {
            double sum = 0;
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                foreach (string line in lines)
                {
                    string[] tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string token in tokens)
                    {
                        sum += double.Parse(token);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при вычислении суммы: " + ex.Message);
            }
            return sum;
        }



        // Задание 3 – Текстовые файлы. Переписать самую короткую и самую длинную строки.

        public static void ExecuteTask3()
        {
            Console.WriteLine("Задание 3: Переписать в другой файл самую короткую и самую длинную строки.");
            string inputFileName = Validators.GetValidatedFileName("Введите имя исходного текстового файла (например, task3.txt): ");

            if (!File.Exists(inputFileName))
            {
                Console.WriteLine("Исходный файл не существует. Создайте файл и повторите попытку.");
                return;
            }

            string[] lines = File.ReadAllLines(inputFileName);
            if (lines.Length == 0)
            {
                Console.WriteLine("Файл пустой. Завершение задания.");
                return;
            }

            string shortest = lines[0];
            string longest = lines[0];
            foreach (string line in lines)
            {
                if (line.Length < shortest.Length)
                    shortest = line;
                if (line.Length > longest.Length)
                    longest = line;
            }

            Console.WriteLine("Найденные строки:");
            Console.WriteLine("Самая короткая строка: " + shortest);
            Console.WriteLine("Самая длинная строка: " + longest);

            string outputFileName = Validators.GetValidatedFileName("Введите имя выходного файла (например, result_task3.txt): ");
            try
            {
                using (StreamWriter sw = new StreamWriter(outputFileName))
                {
                    sw.WriteLine("Самая короткая строка: " + shortest);
                    sw.WriteLine("Самая длинная строка: " + longest);
                }
                Console.WriteLine($"Результат записан в файл {outputFileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при записи файла: " + ex.Message);
            }
        }



        // Задание 4 – Бинарные файлы. Фильтрация четных чисел.

        public static void ExecuteTask4()
        {
            Console.WriteLine("Задание 4: Получить в новом файле те компоненты исходного файла, которые являются четными.");
            string inputFileName = Validators.GetValidatedFileName("Введите имя входного бинарного файла (например, task4.bin): ");
            int count = 0;
            if (!File.Exists(inputFileName))
            {
                Console.WriteLine($"Файл {inputFileName} не найден. Он будет создан.");
                count = Validators.GetValidatedInt("Введите количество чисел для генерации в исходном файле: ");
                Task4_FillBinaryFile(inputFileName, count);
            }
            else
            {
                if (!ValidateTask4File(inputFileName))
                {
                    Console.WriteLine($"Файл {inputFileName} существует, но не соответствует условиям. Он будет перезаписан.");
                    count = Validators.GetValidatedInt("Введите количество чисел для генерации в исходном файле: ");
                    Task4_FillBinaryFile(inputFileName, count);
                }
            }

            string outputFileName = Validators.GetValidatedFileName("Введите имя нового бинарного файла (например, even_task4.bin): ");
            Task4_FilterEvenNumbers(inputFileName, outputFileName);
            Console.WriteLine($"Новый файл {outputFileName} создан с четными числами из файла {inputFileName}.");
        }

        private static bool ValidateTask4File(string fileName)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(fileName);
                if (fileInfo.Length == 0 || fileInfo.Length % 4 != 0)
                    return false;
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    while (fs.Position < fs.Length)
                    {
                        int value = br.ReadInt32();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при проверке бинарного файла: " + ex.Message);
                return false;
            }
        }

        public static void Task4_FillBinaryFile(string fileName, int count)
        {
            Random rnd = new Random();
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                for (int i = 0; i < count; i++)
                {
                    writer.Write(rnd.Next(1, 101));
                }
            }
        }

        public static void Task4_FilterEvenNumbers(string inputFileName, string outputFileName)
        {
            try
            {
                List<int> evenNumbers = new List<int>();
                using (FileStream fs = new FileStream(inputFileName, FileMode.Open, FileAccess.Read))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    while (fs.Position < fs.Length)
                    {
                        int num = br.ReadInt32();
                        if (num % 2 == 0)
                            evenNumbers.Add(num);
                    }
                }
                using (FileStream fsOut = new FileStream(outputFileName, FileMode.Create))
                using (BinaryWriter bw = new BinaryWriter(fsOut))
                {
                    foreach (int even in evenNumbers)
                    {
                        bw.Write(even);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при фильтрации четных чисел: " + ex.Message);
            }
        }



        // Задание 5 – Бинарные файлы и структуры. Багаж пассажира с XML-сериализацией.

        public static void ExecuteTask5()
        {
            Console.WriteLine("Задание 5: Работа с данными о багаже пассажиров (с использованием XML-сериализации).");
            string inputFileName = Validators.GetValidatedFileName("Введите имя входного файла для багажа (например, baggage.xml): ");
            List<BaggageStruct> baggageList = new List<BaggageStruct>();

            if (!File.Exists(inputFileName))
            {
                Console.WriteLine($"Файл {inputFileName} не найден. Он будет создан.");
                int count = Validators.GetValidatedInt("Введите количество записей (единиц багажа) для генерации: ");
                baggageList = Task5_FillBaggageFile(inputFileName, count);
            }
            else
            {
                try
                {
                    baggageList = Task5_ReadBaggageFile(inputFileName);
                    if (baggageList == null || baggageList.Count == 0)
                    {
                        Console.WriteLine("Файл существует, но не содержит корректных данных. Он будет перезаписан.");
                        int count = Validators.GetValidatedInt("Введите количество записей для генерации: ");
                        baggageList = Task5_FillBaggageFile(inputFileName, count);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при чтении файла: " + ex.Message);
                    int count = Validators.GetValidatedInt("Введите количество записей для генерации: ");
                    baggageList = Task5_FillBaggageFile(inputFileName, count);
                }
            }

            double totalMass = 0;
            foreach (BaggageStruct bs in baggageList)
                totalMass += bs.Mass;
            double overallAverage = totalMass / baggageList.Count;
            Console.WriteLine("Общая средняя масса единицы багажа: " + overallAverage + " кг");

            Console.Write("Введите m (допустимое отклонение, в кг): ");
            double m;
            while (!double.TryParse(Console.ReadLine(), out m) || m < 0)
                Console.Write("Некорректный ввод для m. Повторите ввод: ");

            List<BaggageStruct> filteredList = new List<BaggageStruct>();
            foreach (BaggageStruct bs in baggageList)
            {
                if (Math.Abs(bs.Mass - overallAverage) <= m)
                    filteredList.Add(bs);
            }

            string outputFileName = Validators.GetValidatedFileName("Введите имя выходного файла (например, filtered_baggage.xml): ");
            Task5_WriteBaggageFile(outputFileName, filteredList);
            Console.WriteLine($"Отфильтрованные данные успешно записаны в файл {outputFileName}.");
        }

        [Serializable]
        public struct BaggageStruct
        {
            private string _itemName;
            private double _mass;

            public BaggageStruct(string itemName, double mass)
            {
                _itemName = itemName;
                _mass = mass;
            }

            public string ItemName
            {
                get { return _itemName; }
                set { _itemName = value; }
            }

            public double Mass
            {
                get { return _mass; }
                set { _mass = value; }
            }
        }

        private static List<BaggageStruct> Task5_FillBaggageFile(string fileName, int count)
        {
            string[] types = { "чемодан", "сумка", "коробка", "рюкзак", "портфель" };
            Random rnd = new Random();
            List<BaggageStruct> list = new List<BaggageStruct>();
            for (int i = 0; i < count; i++)
            {
                string type = types[rnd.Next(types.Length)];
                double mass = Math.Round(5 + rnd.NextDouble() * 25, 2);
                list.Add(new BaggageStruct(type, mass));
            }
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<BaggageStruct>));
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    serializer.Serialize(fs, list);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при сериализации исходного файла: " + ex.Message);
            }
            return list;
        }

        private static List<BaggageStruct> Task5_ReadBaggageFile(string fileName)
        {
            List<BaggageStruct> list = new List<BaggageStruct>();
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<BaggageStruct>));
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    list = (List<BaggageStruct>)serializer.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при десериализации входного файла: " + ex.Message);
            }
            return list;
        }

        private static void Task5_WriteBaggageFile(string fileName, List<BaggageStruct> list)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<BaggageStruct>));
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    serializer.Serialize(fs, list);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при записи выходного файла: " + ex.Message);
            }
        }



        // Задание 6 – List. Удаление всех вхождений элемента E.

        public static void ExecuteTask6()
        {
            Console.WriteLine("Задание 6: Удаление из списка всех элементов E.");
            Console.WriteLine("Выберите тип списка для ввода:");
            Console.WriteLine("1. int");
            Console.WriteLine("2. double");
            Console.WriteLine("3. string");

            string typeChoice = Console.ReadLine();
            switch (typeChoice)
            {
                case "1":
                    RemoveElementsFromList<int>();
                    break;
                case "2":
                    RemoveElementsFromList<double>();
                    break;
                case "3":
                    RemoveElementsFromList<string>();
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    ExecuteTask6();
                    break;
            }
        }

        public static void RemoveElementsFromList<T>()
        {
            List<T> list = CreateGenericList<T>();
            Console.WriteLine("Введённый список:");
            PrintGenericList(list);

            T elementToRemove = default(T);
            bool valid = false;
            while (!valid)
            {
                Console.Write("Введите значение E для удаления: ");
                string input = Console.ReadLine();
                try
                {
                    elementToRemove = (T)Convert.ChangeType(input, typeof(T));
                    valid = true;
                }
                catch
                {
                    Console.WriteLine("Неверный формат ввода для данного типа. Повторите попытку.");
                }
            }

            int removedCount = RemoveAllOccurrences(list, elementToRemove);
            if (removedCount > 0)
                Console.WriteLine($"Из списка удалено {removedCount} элемент(а), равных {elementToRemove}.");
            else
                Console.WriteLine("Значение не найдено в списке.");

            Console.WriteLine("Результирующий список:");
            PrintGenericList(list);
        }

        public static int RemoveAllOccurrences<T>(List<T> list, T element)
        {
            int countRemoved = 0;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].Equals(element))
                {
                    list.RemoveAt(i);
                    countRemoved++;
                }
            }
            return countRemoved;
        }



        // Задание 7 – LinkedList<T>. Вывод элементов в обратном порядке.

        public static void ExecuteTask7()
        {
            Console.WriteLine("Задание 7: Вывод элементов LinkedList<T> в обратном порядке.");
            Console.WriteLine("Выберите тип списка для ввода:");
            Console.WriteLine("1. int");
            Console.WriteLine("2. double");
            Console.WriteLine("3. string");

            string typeChoice = Console.ReadLine();
            switch (typeChoice)
            {
                case "1":
                    ProcessLinkedList<int>();
                    break;
                case "2":
                    ProcessLinkedList<double>();
                    break;
                case "3":
                    ProcessLinkedList<string>();
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    ExecuteTask7();
                    break;
            }
        }

        private static void ProcessLinkedList<T>()
        {
            LinkedList<T> linkedList = CreateGenericLinkedList<T>();
            if (linkedList.Count == 0)
            {
                Console.WriteLine("Список пустой. Нечего выводить.");
                return;
            }
            Console.WriteLine("Элементы списка в обратном порядке:");
            PrintLinkedListInReverse(linkedList);
        }

        private static LinkedList<T> CreateGenericLinkedList<T>()
        {
            LinkedList<T> ll = new LinkedList<T>();
            int count = Validators.GetValidatedInt("Введите количество элементов в списке: ");
            for (int i = 0; i < count; i++)
            {
                bool valid = false;
                T element = default(T);
                while (!valid)
                {
                    Console.Write($"Введите элемент {i + 1}: ");
                    string input = Console.ReadLine();
                    try
                    {
                        element = (T)Convert.ChangeType(input, typeof(T));
                        valid = true;
                    }
                    catch
                    {
                        Console.WriteLine("Неверный формат ввода. Попробуйте снова.");
                    }
                }
                ll.AddLast(element);
            }
            return ll;
        }

        private static void PrintLinkedListInReverse<T>(LinkedList<T> list)
        {
            LinkedListNode<T> current = list.Last;
            while (current != null)
            {
                Console.Write(current.Value + " ");
                current = current.Previous;
            }
            Console.WriteLine();
        }



        // Задание 8 – HashSet. Анализ закупок учебными заведениями.

        public static void ExecuteTask8()
        {
            Console.WriteLine("Задание 8: Анализ закупок учебными заведениями с использованием HashSet.");
            string fileName = Validators.GetValidatedFileName("Введите имя текстового файла с данными (например, procurement.txt): ");

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл не найден. Завершение задания.");
                return;
            }

            string[] lines = File.ReadAllLines(fileName);
            if (lines.Length < 3)
            {
                Console.WriteLine("Файл имеет недостаточное количество строк для обработки.");
                return;
            }

            if (!int.TryParse(lines[0].Trim(), out int n))
            {
                Console.WriteLine("Первая строка файла должна содержать целое число (количество учебных заведений).");
                return;
            }

            string[] allFirmsArr = lines[1].Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (allFirmsArr.Length == 0)
            {
                Console.WriteLine("Вторая строка файла должна содержать перечень фирм.");
                return;
            }
            HashSet<string> completeFirms = new HashSet<string>(allFirmsArr, StringComparer.OrdinalIgnoreCase);

            if (lines.Length - 2 != n)
            {
                Console.WriteLine($"Некорректное число строк для учебных заведений. Ожидалось {n}, обнаружено {lines.Length - 2}.");
                return;
            }

            HashSet<string> unionSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            HashSet<string> intersectionSet = null;

            for (int i = 2; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line))
                {
                    Console.WriteLine($"Строка {i + 1} пуста. Некорректные данные.");
                    return;
                }
                string[] tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length < 2)
                {
                    Console.WriteLine($"Строка {i + 1} должна содержать имя учебного заведения и хотя бы одну фирму закупки.");
                    return;
                }
                HashSet<string> purchasedSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                for (int j = 1; j < tokens.Length; j++)
                {
                    string firm = tokens[j].Trim();
                    if (!completeFirms.Contains(firm))
                    {
                        Console.WriteLine($"Фирма '{firm}' в строке {i + 1} не присутствует в перечне фирм из второй строки.");
                        return;
                    }
                    purchasedSet.Add(firm);
                }
                unionSet.UnionWith(purchasedSet);
                if (intersectionSet == null)
                    intersectionSet = new HashSet<string>(purchasedSet, StringComparer.OrdinalIgnoreCase);
                else
                    intersectionSet.IntersectWith(purchasedSet);
            }

            HashSet<string> notPurchasedSet = new HashSet<string>(completeFirms, StringComparer.OrdinalIgnoreCase);
            notPurchasedSet.ExceptWith(unionSet);

            Console.WriteLine("\n1) Фирмы, закупка в которых производилась каждым из заведений:");
            if (intersectionSet != null && intersectionSet.Count > 0)
            {
                foreach (var firm in intersectionSet)
                    Console.Write(firm + " ");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Нет фирм, закупка в которых была у всех заведений.");
            }

            Console.WriteLine("\n2) Фирмы, закупка в которых производилась хотя бы одним из заведений:");
            if (unionSet.Count > 0)
            {
                foreach (var firm in unionSet)
                    Console.Write(firm + " ");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Нет фирм, закупка в которых производилась.");
            }

            Console.WriteLine("\n3) Фирмы, в которых ни одно из заведений не закупало компьютеры:");
            if (notPurchasedSet.Count > 0)
            {
                foreach (var firm in notPurchasedSet)
                    Console.Write(firm + " ");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Все фирмы были задействованы в закупках.");
            }
        }



        // Задание 9 – HashSet. Вывод звонких согласных в алфавитном порядке.

        public static void ExecuteTask9()
        {
            Console.WriteLine("Задание 9: Напечатать в алфавитном порядке все звонкие согласные буквы, встречающиеся в тексте.");
            string fileName = Validators.GetValidatedFileName("Введите имя текстового файла с русским текстом: ");
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл не найден.");
                return;
            }
            string text = File.ReadAllText(fileName);
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Файл пуст.");
                return;
            }
            HashSet<char> voicedConsonants = new HashSet<char> { 'б', 'в', 'г', 'д', 'ж', 'з' };
            HashSet<char> foundLetters = new HashSet<char>();
            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    char lowerChar = char.ToLower(c);
                    if (voicedConsonants.Contains(lowerChar))
                        foundLetters.Add(lowerChar);
                }
            }
            if (foundLetters.Count > 0)
            {
                List<char> sortedLetters = new List<char>(foundLetters);
                sortedLetters.Sort();
                Console.WriteLine("Звонкие согласные буквы (в алфавитном порядке):");
                foreach (char letter in sortedLetters)
                    Console.Write(letter + " ");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("В тексте не найдены звонкие согласные буквы.");
            }
        }



        // Задание 10 – Dictionary/SortedList. Формирование уникальных логинов учеников.

        public static void ExecuteTask10()
        {
            Console.WriteLine("Задание 10: Формирование уникальных логинов для учеников.");
            string fileName = Validators.GetValidatedFileName("Введите имя входного файла (например, students.txt): ");

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл не найден. Завершение задания.");
                return;
            }

            string[] lines = File.ReadAllLines(fileName);
            if (lines.Length < 2)
            {
                Console.WriteLine("Файл имеет недостаточное количество строк. В первой строке должно быть число учеников, далее – данные.");
                return;
            }

            if (!int.TryParse(lines[0].Trim(), out int n))
            {
                Console.WriteLine("Первая строка файла должна содержать целое число - количество участников.");
                return;
            }
            if (n > 100)
            {
                Console.WriteLine("Количество учеников не должно превышать 100.");
                return;
            }
            if (lines.Length != n + 1)
            {
                Console.WriteLine($"Ожидалось {n} строк с данными, обнаружено {lines.Length - 1}.");
                return;
            }

            Dictionary<string, int> surnameCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            List<string> logins = new List<string>();

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line))
                {
                    Console.WriteLine($"Строка {i + 1} пуста. Некорректные данные.");
                    return;
                }
                string[] tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length != 2)
                {
                    Console.WriteLine($"Строка {i + 1} должна содержать фамилию и имя, разделённые пробелом.");
                    return;
                }
                string surname = tokens[0].Trim();
                string name = tokens[1].Trim();
                if (surname.Length > 20)
                {
                    Console.WriteLine($"Фамилия в строке {i + 1} превышает 20 символов.");
                    return;
                }
                if (name.Length > 15)
                {
                    Console.WriteLine($"Имя в строке {i + 1} превышает 15 символов.");
                    return;
                }
                if (surnameCounts.ContainsKey(surname))
                    surnameCounts[surname]++;
                else
                    surnameCounts[surname] = 1;
                string login = (surnameCounts[surname] == 1) ? surname : surname + surnameCounts[surname].ToString();
                logins.Add(login);
            }

            Console.WriteLine("\nСформированные логины:");
            foreach (string login in logins)
                Console.WriteLine(login);
        }



        // Обобщённые методы для работы со списками (для заданий 6 и 7)

        public static List<T> CreateGenericList<T>()
        {
            List<T> list = new List<T>();
            int count = Validators.GetValidatedInt("Введите количество элементов списка: ");
            for (int i = 0; i < count; i++)
            {
                bool valid = false;
                T element = default(T);
                while (!valid)
                {
                    Console.Write($"Введите элемент {i + 1}: ");
                    string input = Console.ReadLine();
                    try
                    {
                        element = (T)Convert.ChangeType(input, typeof(T));
                        valid = true;
                    }
                    catch
                    {
                        Console.WriteLine("Неверный формат ввода. Попробуйте снова.");
                    }
                }
                list.Add(element);
            }
            return list;
        }

        public static void PrintGenericList<T>(List<T> list)
        {
            foreach (T item in list)
                Console.Write(item + " ");
            Console.WriteLine();
        }


    }
}

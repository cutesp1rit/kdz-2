using System.Runtime.CompilerServices;
using System.Text;

namespace CSV_Library
{
    public static class DataProcessing
    {
        /// <summary>
        /// Поиск максимальной длины элемента для каждой выборки, чтобы позже выводить данные в читаемом табличном виде
        /// </summary>
        /// <param name="massivData">массив массивов</param>
        /// <returns></returns>
        public static int[] MaxLen(string[][] massivData)
        {
            int[] maxMassiv = new int[massivData[0].Length];
            for (int i = 0; i < massivData[0].Length; i++)
            {
                for (int j = 0; j < massivData.Length; j++)
                {
                    if (maxMassiv[i] < massivData[j][i].Length)
                    {
                        maxMassiv[i] = massivData[j][i].Length;
                    }
                }
            }
            return maxMassiv;
        }
        /// <summary>
        /// Поиск индекса с нужной выборкой
        /// </summary>
        /// <param name="massivData"></param>
        /// <param name="var"></param>
        public static int Search(string[][] massivData, string var)
        {
            int indexArea = -1;
            // ищу номер элемента с такой выборкой
            for (int i = 0; i < massivData[0].Length; i++)
            {
                if (massivData[0][i] == var)
                {
                    indexArea = i;
                    break;
                }
            }
            if (indexArea == -1)
            {
                Console.WriteLine("Ваш файл не соотвествует заявленным выборкам. Перезаргузите команду и выберите другой.");
                indexArea = 0;
            }
            return indexArea;
        }
        /// <summary>
        /// Вывод строки на экран
        /// </summary>
        public static void PrintString(string[][] massivData, int[] maxlen, int i)
        {
            for (int j = 0; j < massivData[i].Length; j++) // цикл для вывода массива
            {
                Console.Write($"{massivData[i][j]}");
                // это цикл для пробелов, если в ячейке еще осталось место после вывода элемента
                for (int f = 0; f < maxlen[j] - massivData[i][j].Length + 1; f++)
                // +1 пробел так как хотя бы какой-то разделитель между элементами должен быть
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
        /// <summary>
        /// Метод переводит массив элементов строки обратно в строку
        /// </summary>
        /// <param name="someString">Переданный массив элементов</param>
        public static string MyToString(string[] someString)
        {
            StringBuilder stringNew = new StringBuilder("");
            foreach (string el in someString)
            {
                stringNew.Append($"\"{el}\";");
            }
            stringNew.Append("\n");
            return stringNew.ToString();
        }
        /// <summary>
        /// Метод для вывода видоизмененных данных с помощью метода.
        /// Преобразование массива массивов, в массив строк.
        /// </summary>
        /// <param name="massivData">массив всех данных</param>
        /// <param name="indexValue">индекс значения</param>
        /// <param name="value">значение по выборке</param>
        /// <returns></returns>
        public static string[] PrintAndGroup(string[][] massivData, int indexValue, string value)
        {
            // массив для вывода данных на экран. в нем лежит информация о размере ячейки для каждого элемента
            // я ее ищу в методе, чтобы все данные выводились в табличном читаемом виде
            int[] maxlen = MaxLen(massivData);

            int kol = 0; // переменная для посчета строк, подходящих под выборку
            for (int i = 2; i < massivData.Length; i++) // i=2, так как первые 2 строки отвечают за заголовки выборок
            {
                if (massivData[i][indexValue].Contains(value))
                {

                    PrintString(massivData, maxlen, i);
                    kol++;
                }
            }

            // массив и индекс для его заполнения
            int indexSort; string[] sortMassiv;
            // значит, мы точно передаем массив строк, то есть первые 2 нам нужны
            if (kol > 1)
            {
                indexSort = 2;
                sortMassiv = new string[kol+2];
                sortMassiv[0] = MyToString(massivData[0]);
                sortMassiv[1] = MyToString(massivData[1]);
            }
            // значит, мы передаем либо 0 строк, либо 1, то есть первые 2 нам не нужны
            else {
                indexSort = 0;
                sortMassiv = new string[kol];
            }
            for (int i = 2; i < massivData.Length; i++) // i=2, так как первые 2 строки отвечают за заголовки выборок
            {
                if (massivData[i][indexValue].Contains(value))
                {
                    sortMassiv[indexSort] = MyToString(massivData[i]);
                    indexSort++;
                }
            }
            return sortMassiv;
        }
        /// <summary>
        /// Поиск по выборке CoverageArea и вывод его на экран
        /// </summary>
        /// <param name="massivData"></param>
        public static void CoverageArea(string[][] massivData)
        {
            int indexArea = Search(massivData, "CoverageArea");
            Console.Write("Введите значение для данной выборки: ");
            string area = Console.ReadLine();
            while (area == null || area.Length == 0)
            {
                Console.Write("Введите значение не null и не пустое: ");
                area = Console.ReadLine();
            }

            string[] sortMassiv = PrintAndGroup(massivData, indexArea, area);
            if (sortMassiv.Length == 1) {
                CsvProcessing.WriteFile(sortMassiv[0]);
            }
            if (sortMassiv.Length == 0)
            {
                Console.WriteLine("К сожалению, таких данных в базе нет :(");
            }
            if (sortMassiv.Length > 1)
            {
                CsvProcessing.WriteFile(sortMassiv);
            }
        }
        /// <summary>
        /// Поиск по выборке WifiName и вывод его на экран
        /// </summary>
        /// <param name="massivData"></param>
        public static void WiFiName(string[][] massivData)
        {
            int indexWifiName = Search(massivData, "WiFiName");
            Console.Write("Введите значение для данной выборки: ");
            string wifiName = Console.ReadLine();
            while (wifiName == null || wifiName.Length == 0)
            {
                Console.Write("Введите значение не null и не пустое: ");
                wifiName = Console.ReadLine();
            }

            string[] sortMassiv = PrintAndGroup(massivData, indexWifiName, wifiName);
            if (sortMassiv.Length == 1)
            {
                CsvProcessing.WriteFile(sortMassiv[0]);
            }
            if (sortMassiv.Length == 0)
            {
                Console.WriteLine("К сожалению, таких данных в базе нет :(");
            }
            if (sortMassiv.Length > 1)
            {
                CsvProcessing.WriteFile(sortMassiv);
            }
        }
        /// <summary>
        /// Поиск по выборке District и AccesFlag, а также вывод таких данных на экран
        /// </summary>
        /// <param name="massivData"></param>
        public static void DistrictAndAccessFlag(string[][] massivData)
        {
            int indexDistrict = Search(massivData, "District");
            Console.Write("Введите значение для выборки District: ");
            string district = Console.ReadLine();
            while (district == null || district.Length == 0)
            {
                Console.Write("Введите значение не null и не пустое: ");
                district = Console.ReadLine();
            }

            int indexAccessFlag = Search(massivData, "AccessFlag");
            Console.Write("Введите значение для выборки AccessFlag: ");
            string accessFlag = Console.ReadLine();
            while (accessFlag == null || accessFlag.Length == 0)
            {
                Console.Write("Введите значение не null и не пустое: ");
                accessFlag = Console.ReadLine();
            }

            // массив для вывода данных на экран. в нем лежит информация о размере ячейки для каждого элемента
            // я ее ищу в методе, чтобы все данные выводились в табличном читаемом виде
            int[] maxlen = MaxLen(massivData);
            // это прописал вызов метода для вывода первой строки, чтобы было понятно, где какие ячейки, но решил, что это не нужно
            // PrintString(massivData, maxlen, 0);
            int kol = 0; // переменная для посчета строк, подходящих под выборку
            for (int i = 2; i < massivData.Length; i++) // i=2, так как первые 2 строки отвечают за заголовки выборок
            {
                if (massivData[i][indexDistrict].Contains(district) && massivData[i][indexAccessFlag].Contains(accessFlag))
                {
                    PrintString(massivData, maxlen, i);
                    kol++;
                }
            }

            // массив и индекс для его заполнения
            int indexSort; string[] sortMassiv;
            // значит, мы точно передаем массив строк, то есть первые 2 нам нужны
            if (kol > 1)
            {
                indexSort = 2;
                sortMassiv = new string[kol + 2];
                sortMassiv[0] = MyToString(massivData[0]);
                sortMassiv[1] = MyToString(massivData[1]);
            }
            // значит, мы передаем либо 0 строк, либо 1, то есть первые 2 нам не нужны
            else
            {
                indexSort = 0;
                sortMassiv = new string[kol];
            }
            for (int i = 2; i < massivData.Length; i++) // i=2, так как первые 2 строки отвечают за заголовки выборок
            {
                if (massivData[i][indexDistrict].Contains(district) && massivData[i][indexAccessFlag].Contains(accessFlag))
                {
                    sortMassiv[indexSort] = MyToString(massivData[i]);
                    indexSort++;
                }
            }

            if (sortMassiv.Length == 1)
            {
                CsvProcessing.WriteFile(sortMassiv[0]);
            }
            if (sortMassiv.Length == 0)
            {
                Console.WriteLine("К сожалению, таких данных в базе нет :(");
            }
            if (sortMassiv.Length > 1)
            {
                CsvProcessing.WriteFile(sortMassiv);
            }
        }
        /// <summary>
        /// Метод сортировки массива по значению CulturalCenterName (по алфавиту)
        /// </summary>
        /// <param name="massivData">массив массивов</param>
        public static void CulturalCenterName(string[][] massivData)
        {
            int indexCulturalCenterName = Search(massivData, "CulturalCenterName");

            for (int i = massivData.Length - 1; i > 2; i--)
            {
                for (int j = 2; j < i; j++)
                {
                    if (String.Compare(massivData[j][indexCulturalCenterName], massivData[j + 1][indexCulturalCenterName]) > 0)
                    {
                        string[] tmp = massivData[j][..]; // [..] для копирование значений, а не ссылок
                        massivData[j] = massivData[j + 1][..];
                        massivData[j + 1] = tmp[..];
                    }
                }
            }
            // массив для вывода данных на экран. в нем лежит информация о размере ячейки для каждого элемента
            // я ее ищу в методе, чтобы все данные выводились в табличном читаемом виде
            int[] maxlen = MaxLen(massivData);
            for (int i = 2; i < massivData.Length; i++) // i=2, так как первые 2 строки отвечают за заголовки выборок
            {
                PrintString(massivData, maxlen, i);
            }

            // запись отсорированного массива массивов в массив строк
            string[] sortMassiv = new string[massivData.Length];
            for (int i = 0; i < massivData.Length; i++)
            {
                sortMassiv[i] = MyToString(massivData[i]);
            }

            CsvProcessing.WriteFile(sortMassiv);
        }
        /// <summary>
        /// Метод сортировки массива по значению NumberOfAccessPoints (по возрастанию)
        /// </summary>
        /// <param name="massivData">массив массивов</param>
        public static void NumberOfAccessPoints(string[][] massivData)
        {
            int indexNumberOfAccessPoints = Search(massivData, "NumberOfAccessPoints");
            for (int i = massivData.Length - 1; i > 2; i--)
            {
                for (int j = 2; j < i; j++) // первые две строки не учитываем, так как они отвечают за названия выборок
                {
                    if (int.Parse(massivData[j][indexNumberOfAccessPoints]) > int.Parse(massivData[j + 1][indexNumberOfAccessPoints]))
                    {
                        string[] tmp = massivData[j][..]; // [..] для копирование значений, а не ссылок
                        massivData[j] = massivData[j + 1][..]; 
                        massivData[j + 1] = tmp[..];
                    }
                }
            }
            // массив для вывода данных на экран. в нем лежит информация о размере ячейки для каждого элемента
            // я ее ищу в методе, чтобы все данные выводились в табличном читаемом виде
            int[] maxlen = MaxLen(massivData);
            for (int i = 2; i < massivData.Length; i++) // i=2, так как первые 2 строки отвечают за заголовки выборок
            {
                PrintString(massivData, maxlen, i);
            }

            string[] sortMassiv = new string[massivData.Length];
            for (int i = 0; i < massivData.Length; i++)
            {
                sortMassiv[i] = MyToString(massivData[i]);
            }

            CsvProcessing.WriteFile(sortMassiv);
        }
    }
}
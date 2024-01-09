using System.Runtime.InteropServices;
using System.Xml.Linq;


namespace CSV_MainProgram
{
    // Максимов Тимофей Степанович, БПИ236-1, Вариант: 18

    // Предисловие: так как файл мне был выдан, то я знаю разделитель данных, а также по первой строке могу высчитать
    // количество выборок. В данном коде я не пользовался методом Split, так как некоторые данные в файле заключены в кавычках
    // Для этого я написал отдельный метод, который позволяет сделать это без потери данных и исключений.
    // В нем я знаю заранее количество выборок, а потому задаю его сразу - 16, дабы избежать перегруженности кода. 
    // Если бы от меня конкретно требовалось также посчитать количество выборок, то делал я бы это с помощью первой строки
    // отдельным методом. Просто не заполнял бы массив и не создавал, а лишь создал еще один метод, который был видоизменен с помощью
    // вышеупомянутого метода только для подсчета выборок. Но так как я могу пользоваться выданным мне файлом, то я не создавал такой метод,
    // чтобы не перегружать код. Хочется также упомянуть, что по первой строке я знаю, что строка завершается ";", а потому
    // если в следующих строках в конце нет ";", то я считаю это за некорретную запись данных в файле и предупреждаю об этом пользователя 

    // В КДЗ требуется выводить в табличном читаемом виде. По коду можно заметить, что я пытаюсь соблюсти этот параметр, однако
    // размеры консоли не позволяет мне этого сделать, поэтому все равно выглядит немного криво, но получше, чем если совсем хаотично

    // при сортировках пустые элементы выводятся вначале 

    // C:\Users\lenovo\source\repos\KDZ-2\wifi-cult-centres.csv
    internal class Program
    {
        static void Main()
        {
            Console.Write("Введите абсолютный путь к файлу, у которого разделитель ';': ");
            string[] result;
            while (true)
            {
                try
                {
                    CSV_Library.CsvProcessing.FPath = Console.ReadLine(); // назначение пути
                    result = CSV_Library.CsvProcessing.Read(); // массив строк
                    Console.WriteLine("Файл успешно считан.");
                    break;
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("Файл отсутствует или его структура не соответствуют варианту. Повторите попытку: ");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Возникла ошибка при открытии файла, повторите попытку: ");
                }
                catch (IOException e)
                {
                    Console.WriteLine("Введено некорректное название файла, повторите попытку: ");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Возникла непредвиденная ошибка, повторите попытку: ");
                }
            }

            string[][] massivData; // массив массивов с элементами по каждой строке
            try
            {
                massivData = CSV_Library.CsvProcessing.SortOfInformation(result);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Данные в файлe записаны неверно, начните программу заново и выберите другой файл.");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine("Непредвиденная ошибка, пожалуйста, начните программу заново и выберите другой файл.");
                return;
            }

            bool flag = true;
            do
            {
                Console.WriteLine("Укажите номер пункта меню для запуска действия:");
                Console.WriteLine("\t1. Произвести выборку по значению CoverageArea");
                Console.WriteLine("\t2. Произвести выборку по значению WiFiName");
                Console.WriteLine("\t3. Произвести выборку по значению District и AccessFlag");
                Console.WriteLine("\t4. Отсортировать таблицу по значению CulturalCenterName (по алфавиту)");
                Console.WriteLine("\t5. Отсортировать таблицу по значению NumberOfAccessPoints (по возрастанию)");
                Console.WriteLine("\t6. Выйти из программы");
                Console.WriteLine("\t7. Выбрать другой файл");
                string numberOfPoint = Console.ReadLine();
                switch (numberOfPoint)
                {
                    case "1":
                        CSV_Library.DataProcessing.CoverageArea(massivData);
                        break;
                    case "2":
                        CSV_Library.DataProcessing.WiFiName(massivData);
                        break;
                    case "3":
                        CSV_Library.DataProcessing.DistrictAndAccessFlag(massivData);
                        break;
                    case "4":
                        CSV_Library.DataProcessing.CulturalCenterName(massivData);
                        break;
                    case "5":
                        try
                        {
                            CSV_Library.DataProcessing.NumberOfAccessPoints(massivData);
                        }
                        catch (Exception e)
                        {
                            // если там не число
                            Console.WriteLine("Данные в файле записаны некорректны, выберите другой.");
                        }
                        break;
                    case "6":
                        flag = false;
                        break;
                    case "7":
                        Main();
                        return;
                    default: 
                        Console.WriteLine("Введенное значение может быть от 1 до 7, как выбор пункта для запуска действия, повторите попытку.");
                        break;
                }
            } while (flag);
        }
    }
}
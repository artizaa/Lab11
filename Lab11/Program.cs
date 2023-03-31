using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLab10;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace Lab11
{

    //не забыть
    //1. Для печати объектов коллекции реализовано 2 способа: с помощью метода в классе InputData (перебором foreach) и с помощью класса Vector
    //2. 3 способа добавления элементов в массив через .ToDictionary и лямбда-функции
    //3. В словаре фильтрация с помощью Where через лямбда-функцию для поиска
    //4. В словаре сортировка через лямбда функцию
    //



    public class Program
    {
        [ExcludeFromCodeCoverage]
        public static Place[] InputObjects(int size = 1)
        {
            ConsoleKeyInfo key;
            Place[] places = new Place[size];
            InputData.PrintMenu("initArr");
            do //цикл не дающий ввести клавиши кроме предложенных
            {
                key = Console.ReadKey(true);
            } while (key.KeyChar != '1' && key.KeyChar != '2');
            switch (key.KeyChar)
            {
                case '1': //ввод объекта случайно
                    {
                        places = Requests.CreatePlaces(size);
                        break;
                    }
                case '2': //ввод объекта  с клавиатуры
                    {
                        for (int i = 0; i < size; i++)
                        {
                            InputData.PrintMenu("object", i + 1);

                            do //цикл не дающий ввести клавиши кроме предложенных
                            {
                                key = Console.ReadKey(true);
                            } while (key.KeyChar != '1' && key.KeyChar != '2' && key.KeyChar != '3' && key.KeyChar != '4' && key.KeyChar != '5');

                            switch (key.KeyChar) //выбор типа создаваемого объекта
                            {
                                case '1':
                                    {
                                        Place p = new Place();
                                        InputData.CreateObject(p, i);
                                        places[i] = p;
                                        break;
                                    }
                                case '2':
                                    {
                                        Region r = new Region();
                                        InputData.CreateObject(r, i);
                                        places[i] = r;
                                        break;
                                    }
                                case '3':
                                    {
                                        City c = new City();
                                        InputData.CreateObject(c, i);
                                        places[i] = c;
                                        break;
                                    }
                                case '4':
                                    {
                                        Megapolis m = new Megapolis();
                                        InputData.CreateObject(m, i);
                                        places[i] = m;
                                        break;
                                    }
                                case '5':
                                    {
                                        Adress a = new Adress();
                                        InputData.CreateObject(a, i);
                                        places[i] = a;
                                        break;
                                    }
                            }

                        };
                        break;
                    }
            };
            return places;
        }

        [ExcludeFromCodeCoverage]
        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            do
            {
                InputData.PrintMenu("part");
                do //цикл не дающий ввести клавиши кроме предложенных
                {
                    key = Console.ReadKey(true);
                } while (key.KeyChar != '1' && key.KeyChar != '2' && key.KeyChar != '3' && key.KeyChar != '4');
                switch (key.KeyChar) //перебор с помощью функций предложенных клавиш
                {
                    case '1': //1 часть лабы
                        {
                            int size = InputData.InputIntNumber("Введите количество объектов типа Place", 1);
                            
                            ArrayList array = new ArrayList();
                            array.AddRange(InputObjects(size));

                            new Vector<object>(array.ToArray()).PrintArray();
                            //InputData.PrintArrayList(array);

                            do
                            {
                                InputData.PrintMenu("actions");
                                do //цикл не дающий ввести клавиши кроме предложенных
                                {
                                    key = Console.ReadKey(true);
                                } while (key.KeyChar != '1' && key.KeyChar != '2' && key.KeyChar != '3' && key.KeyChar != '4' && key.KeyChar != '5' && key.KeyChar != '6' && key.KeyChar != '7');

                                switch (key.KeyChar)
                                {
                                    case '1': //добавление элемента в коллекцию
                                        {
                                            int kAdd = InputData.InputIntNumber("Введите количество добавляемых элементов", 1, 10);
                                            Place[] addPlace = InputObjects(kAdd);
                                            array.InsertRange(InputData.InputIntNumber("Введите позицию первого добавляемого элемента", 1, array.Count + 1) - 1, addPlace);
                                            new Vector<object>(array.ToArray()).PrintArray();
                                            break;
                                        }
                                    case '2': //удаление элемента из коллекции
                                        {
                                            if (array == null || array.Count == 0)
                                                Console.WriteLine("Массив пустой, удалять элементы нельзя");
                                            else
                                            {
                                                int nDelete = InputData.InputIntNumber("Введите позицию первого удаляемого элемента", 1, array.Count) - 1;
                                                array.RemoveRange(nDelete, InputData.InputIntNumber("Введите количество удаляемых элементов", 1, array.Count - nDelete));
                                                InputData.PrintArrayList(array);
                                            }
                                            break;
                                        }
                                    case '3': //реализация запросов
                                        {
                                            InputData.PrintMenu("request");

                                            do //цикл не дающий ввести клавиши кроме предложенных
                                            {
                                                key = Console.ReadKey(true);
                                            } while (key.KeyChar != '1' && key.KeyChar != '2' && key.KeyChar != '3' && key.KeyChar != '4' && key.KeyChar != '5');

                                            switch (key.KeyChar)
                                            {
                                                case '1': //Количество элементов определенного вида
                                                    {
                                                        string inputRegion = InputData.CheckInput(InputData.Regions);
                                                        string[] arr = Requests.CitiesInRegion(inputRegion, array);
                                                        Console.WriteLine($"Количество городов данной области = {arr.Length}");
                                                        if (arr.Length != 0)
                                                        {
                                                            Console.WriteLine("Список этих городов:");
                                                            for (int i = 0; i < arr.Length; i++)
                                                            {
                                                                Console.WriteLine(arr[i]);
                                                            }
                                                        }
                                                        Console.WriteLine();
                                                        //Console.WriteLine($"Количество элементов класса {input} = {Requests.NumberClasses(array, input)}");
                                                       
                                                        break;
                                                    }
                                                case '2':
                                                    {
                                                        string inputRegion = InputData.CheckInput(InputData.Regions);
                                                        Console.WriteLine($"Количество жителей данной области = {Requests.CitizienInRegion(inputRegion, array)}");
                                                        Console.WriteLine();
                                                        break;
                                                    }
                                                case '3':
                                                    {
                                                        string inputRegion = InputData.CheckInput(InputData.Regions);
                                                        Console.WriteLine($"Максимальное количество улиц = {Requests.MaxStreets(inputRegion, array)}");
                                                        Console.WriteLine();
                                                        break;
                                                    }
                                                case '4':
                                                    {
                                                        string inputRegion = InputData.CheckInput(InputData.Streets, "Введите улицу", "Такой улицы не найдено, повторите ввод");
                                                        Console.WriteLine($"Сумма номеров квартир = {Requests.SumFlats(inputRegion, array)}");
                                                        Console.WriteLine();
                                                        break;
                                                    }
                                                case '5':
                                                    {
                                                        string inputRegion = InputData.CheckInput(InputData.Regions);
                                                        Console.WriteLine($"Минимальная сумма координат мегаполиса = {Requests.MinMegapolisCoords(inputRegion, array)}");
                                                        Console.WriteLine();
                                                        break;
                                                    }

                                            }
                                            break;
                                        }
                                    case '4': //клонирование коллекции
                                        {
                                            if (array == null || array.Count == 0)
                                                Console.WriteLine("Массив пустой");
                                            else
                                            {
                                                ArrayList array2 = new ArrayList();
                                                array2.AddRange(array);
                                                InputData.PrintArrayList(array2, "Отклонированная коллекция");
                                            }
                                            break;
                                        }
                                    case '5': //сортировка коллекции
                                        {
                                            if (array == null || array.Count == 0)
                                                Console.WriteLine("Массив пустой");
                                            else
                                            {
                                                array.Sort();
                                                InputData.PrintArrayList(array, "Отсортированная коллекция:");

                                                Console.WriteLine("Введите объект для бинарного поиска:");
                                                Place p = InputObjects()[0];
                                                Console.WriteLine(p);
                                                if (array.BinarySearch(p) < 0) Console.WriteLine("Данный элемент не найден");
                                                else Console.WriteLine($"Индекс данного элемента {array.BinarySearch(p) + 1}");
                                                Console.WriteLine();
                                            }
                                            
                                            break;
                                        }
                                    case '6': //создание новой коллекции
                                        {
                                            size = InputData.InputIntNumber("Введите количество объектов типа Place", 1);
                                            array.Clear();
                                            array.AddRange(InputObjects(size));
                                            InputData.PrintArrayList(array);
                                            break;
                                        }
                                    case '7': //выход из коллекции
                                        {
                                            break;
                                        }
                                };
                            } while (key.KeyChar != '7');
                            

                            break;
                        }
                    case '2': //2 часть лабы
                        {
                            int size = InputData.InputIntNumber("Введите количество объектов типа Place", 1);

                            Dictionary <int, Place> array = new Dictionary <int, Place>();
                            Place[] p = InputObjects(size);

                            //3 способа добавления элементов в массив через .ToDictionary и лямбда-функции
                            array = Enumerable.Range(0, size).ToArray().Select((k, i) => (k, i)).ToDictionary(x => x.k, x => p[x.i]);
                            array.Clear();
                            array = Enumerable.Range(0, size).ToDictionary(i => Enumerable.Range(0, size).ToArray()[i], i => p[i]);
                            array.Clear();

                            for (int i = 0; i < size; i++)
                            {
                                array.Add(i, p[i]);
                            }

                            //InputData.PrintDictionary(array);
                            //альтернативный способ печати объекта
                            new Vector <object>(array.Values.ToArray()).PrintArray();
                            
                            do
                            {
                                InputData.PrintMenu("actions");
                                do //цикл не дающий ввести клавиши кроме предложенных
                                {
                                    key = Console.ReadKey(true);
                                } while (key.KeyChar != '1' && key.KeyChar != '2' && key.KeyChar != '3' && key.KeyChar != '4' && key.KeyChar != '5' && key.KeyChar != '6' && key.KeyChar != '7');

                                switch (key.KeyChar)
                                {
                                    case '1': //добавление элемента в коллекцию
                                        {
                                            int nAdd = InputData.InputIntNumber("Введите количество добавляемых элементов", 1, 20);
                                            Place[] addPlace = InputObjects(nAdd);
                                            int kAdd = InputData.InputIntNumber("Введите позицию первого добавляемого элемента", 1, array.Last().Key + 2) - 1;
                                            for (int i=0; i<nAdd; i++)
                                            {
                                                if (!array.ContainsKey(i+kAdd))
                                                {
                                                    array.Add(i + kAdd, addPlace[i]);
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"Элемент с номером {i + 1 + kAdd} уже есть в словаре");
                                                }    
                                            }
                                            Console.WriteLine();
                                            //for (int i = 0; i < nAdd; i++)
                                            //{
                                            //    array.Add(array.Count, addPlace[i]); //добавление в конец
                                            //}
                                            InputData.PrintDictionary(array);
                                            break;
                                        }
                                    case '2': //удаление элемента из коллекции
                                        {
                                            int kDelete = InputData.InputIntNumber("Введите номер первого удаляемого элемента", 1, array.Last().Key+1);
                                            int nDelete = InputData.InputIntNumber("Введите количество удаляемых элементов", 1, array.Last().Key - kDelete + 2);
                                            for (int i = 0; i < nDelete; i++)
                                            {
                                                if (!array.ContainsKey(kDelete + i - 1))
                                                {
                                                    Console.WriteLine($"Нет элемента с заданным номером {kDelete + i}");
                                                }
                                                else
                                                {
                                                    array.Remove(kDelete + i - 1);
                                                }
                                            }
                                            InputData.PrintDictionary(array);
                                            break;
                                        }
                                    case '3': //реализация запросов
                                        {
                                            InputData.PrintMenu("request");

                                            do //цикл не дающий ввести клавиши кроме предложенных
                                            {
                                                key = Console.ReadKey(true);
                                            } while (key.KeyChar != '1' && key.KeyChar != '2' && key.KeyChar != '3' && key.KeyChar != '4' && key.KeyChar != '5');

                                            switch (key.KeyChar)
                                            {
                                                case '1': //Количество элементов определенного вида
                                                    {
                                                        string inputRegion = InputData.CheckInput(InputData.Regions);
                                                        string[] arr = Requests.CitiesInRegion(inputRegion, array);
                                                        Console.WriteLine($"Количество городов данной области = {arr.Length}");
                                                        if (arr.Length != 0)
                                                        {
                                                            Console.WriteLine("Список этих городов:");
                                                            for (int i = 0; i < arr.Length; i++)
                                                            {
                                                                Console.WriteLine(arr[i]);
                                                            }
                                                        }
                                                        Console.WriteLine();
                                                        //Console.WriteLine($"Количество элементов класса {input} = {Requests.NumberClasses(array, input)}");

                                                        break;
                                                    }
                                                case '2':
                                                    {
                                                        string inputRegion = InputData.CheckInput(InputData.Regions);
                                                        Console.WriteLine($"Количество жителей данной области = {Requests.CitizienInRegion(inputRegion, array)}");
                                                        Console.WriteLine();
                                                        break;
                                                    }
                                                case '3':
                                                    {
                                                        string inputRegion = InputData.CheckInput(InputData.Regions);
                                                        Console.WriteLine($"Максимальное количество улиц = {Requests.MaxStreets(inputRegion, array)}");
                                                        Console.WriteLine();
                                                        break;
                                                    }
                                                case '4':
                                                    {
                                                        string inputRegion = InputData.CheckInput(InputData.Streets, "Введите улицу", "Такой улицы не найдено, повторите ввод");
                                                        Console.WriteLine($"Сумма номеров квартир = {Requests.SumFlats(inputRegion, array)}");
                                                        Console.WriteLine();
                                                        break;
                                                    }
                                                case '5':
                                                    {
                                                        string inputRegion = InputData.CheckInput(InputData.Regions);
                                                        Console.WriteLine($"Минимальная сумма координат мегаполиса = {Requests.MinMegapolisCoords(inputRegion, array)}");
                                                        Console.WriteLine();
                                                        break;
                                                    }

                                            }
                                            break;
                                        }
                                    case '4': //клонирование коллекции
                                        {
                                            Dictionary <int, Place> array2 = new Dictionary<int, Place>();
                                            for (int i = 0; i < array.Last().Key+1; i++)
                                            {
                                                if (array.ContainsKey(i))
                                                {
                                                    array2.Add(i, array[i]);
                                                }
                                            }
                                            InputData.PrintDictionary(array2, "Отклонированная коллекция");
                                            break;
                                        }
                                    case '5': //сортировка коллекции
                                        {
                                            array = array.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                                            InputData.PrintDictionary(array, "Отсортированная коллекция");
                                            Console.WriteLine();
                                            Console.WriteLine("Введите объект для поиска:");
                                            Place pFind = InputObjects()[0];
                                            Console.WriteLine(pFind);
                                            if (!array.ContainsValue(pFind)) Console.WriteLine("Данный элемент не найден");
                                            else
                                            {
                                                Console.WriteLine($"Индекс данного элемента {array.Where(x => x.Value.Equals(pFind)).First().Key + 1}");
                                            }
                                            Console.WriteLine();
                                            break;
                                        }
                                    case '6': //создание новой коллекции
                                        {
                                            p = InputObjects(size);
                                            for (int i = 0; i < array.Count; i++)
                                            {
                                                array[i] = p[i];
                                            }
                                            InputData.PrintDictionary(array);
                                            break;
                                        }
                                    case '7': //выход из коллекции
                                        {
                                            break;
                                        }
                                };
                            } while (key.KeyChar != '7');
                            break;
                        }
                    case '3': //3 часть лабы
                        {

                            break;
                        }
                    case '4':
                        {
                            //функция выхода из программы
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Для выхода из программы нажмите Escape");
                            //следующий цикл блокирует остальные кнопки, выход из программы только по клавише Escape
                            Console.ForegroundColor = ConsoleColor.Gray;
                            do
                            {
                                key = Console.ReadKey(true);
                            } while (key.Key != ConsoleKey.Escape);
                            break;
                        }
                }
            } while (key.Key != ConsoleKey.Escape);

        }

    }

}

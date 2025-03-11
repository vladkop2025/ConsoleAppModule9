using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    class Program
    {
        static void Main(string[] args)
        {
            //Модуль 9. Обработка исключений. Делегаты
            /* 9.6. Практикум

            --------------------------------------
            Задание 2
            -------------------------------------- 
            Создайте консольное приложение, в котором будет происходить сортировка списка фамилий из пяти человек. Сортировка должна происходить при помощи события. 
            Сортировка происходит при введении пользователем либо числа 1 (сортировка А-Я), либо числа 2 (сортировка Я-А).

            Дополнительно реализуйте проверку введённых данных от пользователя через конструкцию TryCatchFinally с использованием собственного типа исключения.

             */

            //--------------------------------------
            //Код для задания 2
            //--------------------------------------      

            // Список фамилий
            List<string> surnames = new List<string> { "Иванов", "Петров", "Сидоров", "Кузнецов", "Васильев" };

            // Создаем объект сортировщика
            SurnameSorter sorter = new SurnameSorter(surnames);

            // Подписываемся на событие сортировки
            sorter.SortEvent += ShowSortedSurnames;

            while (true)
            {
                try
                {
                    // Запрашиваем у пользователя тип сортировки
                    Console.WriteLine("Введите число 1 для сортировки А-Я или 2 для сортировки Я-А:");
                    int sortType = Convert.ToInt32(Console.ReadLine());

                    // Вызываем метод сортировки
                    sorter.Sort(sortType);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: введено нечисловое значение.");
                }
                catch (CustomSortException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                finally
                {                    
                    //Console.WriteLine("Программа завершена.");
                }
            }

        }

        // Метод для отображения отсортированного списка фамилий
        static void ShowSortedSurnames(List<string> surnames)
        {
            Console.WriteLine("Отсортированный список фамилий:");
            foreach (var surname in surnames)
            {
                Console.WriteLine(surname);
            }
        }

    }

    // Класс для сортировки фамилий
    class SurnameSorter
    {
        private List<string> _surnames;

        // Конструктор
        public SurnameSorter(List<string> surnames)
        {
            _surnames = surnames;
        }

        // Делегат для события сортировки
        public delegate void SortDelegate(List<string> surnames);

        // Событие сортировки
        public event SortDelegate SortEvent;

        // Метод для сортировки
        public void Sort(int sortType)
        {
            if (sortType != 1 && sortType != 2)
            {
                throw new CustomSortException("Введено недопустимое значение. Допустимые значения: 1 или 2.");
            }

            // Сортировка А-Я
            if (sortType == 1)
            {
                _surnames.Sort();
            }
            // Сортировка Я-А
            else if (sortType == 2)
            {
                _surnames.Sort();
                _surnames.Reverse();
            }

            // Вызов события
            SortEvent?.Invoke(_surnames);
        }
    }

    // Собственный тип исключения
    class CustomSortException : Exception
    {
        public CustomSortException(string message) : base(message) { }
    }

}

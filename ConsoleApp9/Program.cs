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
            Задание 1
            --------------------------------------           
            Создайте свой тип исключения.

            Сделайте массив из пяти различных видов исключений, включая собственный тип исключения. Реализуйте конструкцию TryCatchFinally, .
            в которой будет итерация на каждый тип исключения (блок finally по желанию).
            В блоке catch выведите в консольном сообщении текст исключения.

            --------------------------------------
            Задание 2
            -------------------------------------- 
            Создайте консольное приложение, в котором будет происходить сортировка списка фамилий из пяти человек. Сортировка должна происходить при помощи события. 
            Сортировка происходит при введении пользователем либо числа 1 (сортировка А-Я), либо числа 2 (сортировка Я-А).

            Дополнительно реализуйте проверку введённых данных от пользователя через конструкцию TryCatchFinally с использованием собственного типа исключения.

             */

            //--------------------------------------
            //Код для задания 1
            //--------------------------------------      

            // Создаем массив исключений
            Exception[] exceptions = new Exception[]
            {
                new FormatException("Ошибка формата данных"),
                new DivideByZeroException("Попытка деления на ноль"),
                new IndexOutOfRangeException("Выход за пределы массива"),
                new InvalidOperationException("Недопустимая операция"),
                new CustomException("Собственное исключение")
            };

            //В цикле foreach для каждого исключения выполняется блок try, где исключение генерируется с помощью throw
            foreach (var exception in exceptions)
            {
                try
                {
                    // Генерируем исключение
                    throw exception;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Поймано исключение: {ex.Message}");
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine($"Поймано исключение: {ex.Message}");
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine($"Поймано исключение: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Поймано исключение: {ex.Message}");
                }
                catch (CustomException ex)
                {
                    Console.WriteLine($"Поймано собственное исключение: {ex.Message}");
                }
                //выполняется после каждого блока try/catch, независимо от того, было ли поднято исключение
                finally
                {
                    Console.WriteLine("Блок finally выполнен.");
                }
            }

        }

        // Собственный тип исключения
        class CustomException : Exception
        {
            public CustomException(string message) : base(message) { }
        }

    }
}

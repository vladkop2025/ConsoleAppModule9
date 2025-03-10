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
            /* 9.2. Конструкция Try Catch Finally
 
            try: блок try инкапсулирует проверяемый на исключение регион кода. Если любая строка кода в этом блоке вызывает срабатывание исключения, 
            то исключение будет обработано соответствующим блоком catch.

            catch: когда происходит исключение, запускается блок кода catch. Это то место, где вы можете обработать исключения и предпринять адекватные 
            для него действия, например, записать событие ошибки в лог, прервать работу программы, или может просто игнорировать исключение (когда блок catch пустой).

            finally: блок finally позволяет вам выполнить какой-то определенный код приложения, если исключение сработало, или если оно не сработало. 
            Например, если вы открываете файл, он должен быть закрыт вне зависимости от того, возникло ли исключение. Часто блок finally опускается, 
            когда обработка исключения подразумевает нормальное дальнейшее выполнение программы, потому блок finally может быть просто заменен обычным 
            кодом, который следует за блоками try и catch.

            throw: ключевое слово throw используется для реального создания нового исключения (в юните 9.1 мы уже затрагивали данный блок в примере), 
            в результате чего выполнение кода попадет в соответствующие блоки catch и finally.

            
                Try	Обязательный блок
                Catch	Обязательный блок
                Finally	Необязательный блок

            Отметим, что блок finally необязательный. Например, функция нашего калькулятора, которая делит два числа, может быть записана вот так:

            public class Program
            {
            static int Division(int a, int b)
            {
                return a / b;
            }

            static void Main(string[] args)
            {

                try
                {
                    int result = Division(7, 0);

                    Console.WriteLine(result);
                }
                catch (System.DivideByZeroException)
                {
                    Console.WriteLine("На ноль делить нельзя!");
                }
                finally
                {
                    Console.WriteLine("Блок Finally сработал!");
                }

                Console.ReadKey();
            }
            }

            И может быть записана вот так, без блока finally:

        public class Program
        {
            static int Division(int a, int b)
            {
                return a / b;
            }

            static void Main(string[] args)
            {

                try
                {
                    int result = Division(7, 0);

                    Console.WriteLine(result);
                }

                catch (System.DivideByZeroException)
                {
                    Console.WriteLine("На ноль делить нельзя!");
                }

                Console.ReadKey();
            }
        }

            В коде выше видно особенности конструкции catch в нашем калькуляторе. Функция int Division находится в блоке try.

            Следовательно, если в данном блоке сработает исключение, начнётся выполнение блока catch. Но обратите внимание, что в блоке catch 
            находится параметр System.DivideByZeroException. Это означает, что блок будет срабатывать, только если сработало исключение System.DivideByZeroException.

            Но мы можем изменить наш блок catch и сделать вот так:

         public class Program
        {
            static int Division(int a, int b)
            {
                return a / b;
            }

            static void Main(string[] args)
            {

                try
                {
                    int result = Division(7, 0);

                    Console.WriteLine(result);
                }

                catch (Exception ex)
                {
                    if (ex is DivideByZeroException) Console.WriteLine("На ноль делить нельзя!");
                    else Console.WriteLine("Произошла непредвиденная ошибка в приложении.");
                }

                finally
                {
                    Console.WriteLine("Блок Finally сработал!");
                }

                Console.ReadKey();
            }
        }

            Теперь наш блок catch будет ловить все виды исключений. Но в самом блоке мы сделаем дополнительную проверку типа исключения на System.DivideByZeroException 
            для отображения соответствующего сообщения пользователю. Если исключение таковым не является, то отобразим для пользователя сообщение: 
            "Произошла непредвиденная ошибка в приложении".

            Задание 9.2.2
            Создайте консольное решение, в котором реализуйте конструкцию Try/Catch/Finally для обработки исключения ArgumentOutOfRangeException.
            В случае исключения отобразите в консоль сообщение об ошибке.

    namespace TryCatchPractices
    {
        class Program
        {
            static void Main(string[] args)
            {

                try
                {
                    throw new ArgumentOutOfRangeException("Сообщение об ошибке");
                }

                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                finally
                {
                    Console.Read();
                }
            }
        }
    }

            Задание 9.2.3
            Создайте консольное решение, в котором реализуйте конструкцию Try/Catch/Finally для обработки исключения RankException. В случае исключения 
            отобразите в консоль тип исключения (через метод GetType()).

      namespace TryCatchPractices
      {
        class Program
        {
            static void Main(string[] args)
            {

                try
                    throw new RankException("Сообщение об ошибке");
                }

                catch (RankException ex)
                {
                    Console.WriteLine(ex.GetType());
                }

                finally
                {
                    Console.Read();
                }
            }
        }
     }

            */

            try
            {
                throw new ArgumentOutOfRangeException("Сообщение об ошибке");
            }

            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                Console.Read();
            }

            try
            {
                throw new RankException("Сообщение об ошибке");
            }

            catch (RankException ex)
            {
                Console.WriteLine(ex.GetType());
            }

            finally
            {
                Console.Read();
            }

        }
    }
}

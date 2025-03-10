using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp9.Program;

namespace ConsoleApp9
{
    class Program
    {
        static void Main(string[] args)
        {
            //Модуль 9. Обработка исключений. Делегаты
            /* 9.3. Делегаты

            Обратимся к коду ниже:

    using System;
 
    namespace SkillFactoryExample
    {
        public class MyClass
        {
            public void Process()
            {
                Console.WriteLine("Процесс начат");
                Console.WriteLine("Процесс окончен");
            }
        }

        public class Test
        {
            static void Main(string[] args)
            {
                MyClass myClass = new MyClass();
                myClass.Process();
            }
    }

            В данном коде мы создали класс MyClass. В нём метод Process, который отображает в консоли два сообщения: "Процесс начат" и "Процесс окончен".

            В методе Main создаём экземпляр класса MyClass и вызываем у него метод Process.

            Всё прекрасно работает, и казалось бы, зачем всё усложнять. Однако, что если мы не хотим вызывать функцию напрямую? Мы хотели бы передать
            её кому-нибудь ещё, чтобы они могли её вызвать, то есть мы бы хотели делегировать её. Тут на помощь к нам и приходят делегаты.


                Делегат — указатель на функцию. Использование делегата позволяет нам инкапсулировать ссылку на метод внутри объекта делегата. Затем объект 
                делегата может быть передан в код, который может вызвать указанный метод без необходимости знать во время компиляции, какой метод будет вызван.

            Важные особенности делегатов:

                Предоставляет хороший способ инкапсулировать методы (привет инкапсуляция в ООП).
                Это типобезопасный указатель любого метода.
                Делегаты в основном используются при реализации методов и событий обратного вызова.
                Делегаты могут быть объединены в цепочку, так как два или более метода могут быть вызваны для одного события.
                Его не волнует класс объекта, на который он ссылается.
                Делегаты также могут использоваться при вызове «анонимных методов».

            --------------------------------------
            Объявление делегатов
            --------------------------------------

            Тип делегата объявляется при помощи ключевого слова delegate.

                // "public" это модификатор доступа
                // "int" это возвращаемый тип
                // "Summ" это название делегата
                // "(int a, int b, int c)" это входные параметры
                public delegate int SumDelegate(int a, int b, int c);

                Примечание. Делегат будет вызывать только метод, соответствующий его сигнатуре и типу возвращаемого значения.

            Это означает, что мы можем сделать вот так:
            Invoke в C# — это механизм вызова методов, делегатов, событий или других блоков кода.

         public class Program
        {
            public delegate int SumDelegate(int a, int b, int c);

            static void Main(string[] args)
            {
                SumDelegate sumDelegate = Sum;
                sumDelegate.Invoke(1, 10, 50);
                Console.ReadKey();
            }

            static int Sum(int a, int b, int c)
            {
                return a + b + c;
            }
        }
            Но не можем сделать вот так:
                
        public class Program
        {
            public delegate int SumDelegate(int a, int b, int c);
            static void Main(string[] args)
            {
                SumDelegate sumDelegate = Sum;
                sumDelegate.Invoke(1, 10, 50);
                Console.ReadKey();
            }

            static int Sum(int a, int b)
            {
                return a + b;
            }
        }

            Потому что функция int Sum на вход требует два числа int, а делегат int SumDelegate на вход требует три числа int.

            Задание 9.3.1

                static void Show(string _mess) { Console.WriteLine(_mess); }

            Какой из перечисленных делегатов подходит под сигнатуру указанного метода?

                delegate Show(string_mess);
                delegate Show();
                delegate void Show();
                delegate void Show(string _mess);   X

            --------------------------------------
            Вызов делегата
            --------------------------------------
            
            Вызов делегата можно произвести несколькими способами.

            Первый способ:

        public class Program
        {
            public delegate int SumDelegate(int a, int b, int c);

            static void Main(string[] args)
            {
                SumDelegate sumDelegate = Sum;
                int result = sumDelegate(1, 10, 50);
                Console.WriteLine(result);
                Console.ReadKey();
            }

            static int Sum(int a, int b, int c)
            {
                return a + b + c;
            }
        }

            Второй способ:

        public class Program
        {
            public delegate int SumDelegate(int a, int b, int c);

            static void Main(string[] args)
            {
                SumDelegate sumDelegate = Sum;
                int result = sumDelegate.Invoke(1, 10, 50);
                Console.WriteLine(result);
                Console.ReadKey();
            }

            static int Sum(int a, int b, int c)
            {
                return a + b + c;
            }
        }

            Запустим нашу программу и посмотрим на результат. И в том, и в другом случае в консоли выводится один результат, несмотря на то, 
            что вызывали делегат разными способами. Выводимый результат — сумма трёх чисел (1, 10 и 50):

                61

            Задание 9.3.3
            Используя ваше приложение из задания 9.3.2, реализуйте вызов делегата двумя разными способами.

    namespace DelegatePractices
    {
        class Program
        {
            delegate int CalculateDelegate(int a, int b);
            static void Main(string[] args)
            {
                CalculateDelegate calcDelegate = Calculate;

                int resultOne = calcDelegate.Invoke(100, 30);
                Console.WriteLine(resultOne);

                int resultTwo = calcDelegate(100, 30);
                Console.WriteLine(resultTwo);


                Console.Read();
            }

            static int Calculate(int a, int b)
            {
                return a - b;
            }
        }
    }

            --------------------------------------
            Добавление методов в делегат (multicast deligate или многоадресный делегат)
            --------------------------------------            

            В примере выше делегат указывает только на один метод Sum. Но есть ещё одна особенность у делегатов: в каждый делегат можно добавлять 
            огромное количество методов. Такой делегат будет называться мультикастовым (multicast deligate) или многоадресным.

            Видоизменяем пример для демонстрации мультикастового делегата:

        public class Program
        {
            public delegate void ShowDelegate();
            static void Main(string[] args)
            {

                ShowDelegate showDelegate = ShowMessage1;
                showDelegate += ShowMessage2;
                showDelegate += ShowMessage3;
                showDelegate += ShowMessage4;

                showDelegate.Invoke();

                Console.ReadKey();
            }

            static void ShowMessage1()
            {
                Console.WriteLine("Метод 1");
            }

            static void ShowMessage2()
            {
                Console.WriteLine("Метод 2");
            }

            static void ShowMessage3()
            {
                Console.WriteLine("Метод 3");
            }

            static void ShowMessage4()
            {
                Console.WriteLine("Метод 4");
            }
        }
            
            Как мы видим, добавление методов в делегат происходит через операцию +=.

            Запустим приложение и увидим результат:

                Метод 1
                Метод 2
                Метод 3
                Метод 4

            Задание 9.3.4
            Реализуйте консольное приложение, в котором существует две функции: первая функция вычитает второе число из первого и отображает результат в консольном сообщении, 
            вторая функция складывает два числа и отображает результат в консоли. Реализуйте вызов этих двух функций через многоадресный делегат.

    namespace DelegatePractices
    {
        class Program
        {
            delegate void CalculateDelegate(int a, int b);
            static void Main(string[] args)
            {
                CalculateDelegate calcDelegate = CalculateOne;

                calcDelegate += CalculateTwo;

                calcDelegate.Invoke(100, 30);

                Console.Read();
            }

            static void CalculateOne(int a, int b)
            {
                Console.WriteLine(a - b);
            }

            static void CalculateTwo(int a, int b)
            {
                Console.WriteLine(a + b);
            }
        }
    }

            --------------------------------------
            Удаление методов из делегата
            --------------------------------------        

            Удаление методов из делегата, происходит при помощи операции -=. Давайте удалим некоторые методы из нашего делегата:

        public class Program
        {
            public delegate void ShowDelegate();
            static void Main(string[] args)
            {

                ShowDelegate showDelegate = ShowMessage1;
                showDelegate += ShowMessage2;
                showDelegate += ShowMessage3;
                showDelegate += ShowMessage4;

                showDelegate -= ShowMessage4;
                showDelegate -= ShowMessage3;
                showDelegate -= ShowMessage2;

                showDelegate.Invoke();

                Console.ReadKey();
            }

            static void ShowMessage1()
            {
                Console.WriteLine("Метод 1");
            }

            static void ShowMessage2()
            {
                Console.WriteLine("Метод 2");
            }

            static void ShowMessage3()
            {
                Console.WriteLine("Метод 3");
            }

            static void ShowMessage4()
            {
                Console.WriteLine("Метод 4");
            }
        }
            И запустим программу. Результат будет следующий:

                 Метод 1

            --------------------------------------
            Объединение делегатов
            --------------------------------------    

            Делегаты имеют ещё одну отличную особенность — они могут между собой объединяться. Объединение происходит через операцию + между делегатами. 
            Снова видоизменяем наш код выше для объединения делегатов:

                        
        public class Program
        {
            public delegate void ShowDelegate();
            static void Main(string[] args)
            {

                ShowDelegate showDelegate1 = ShowMessage1;
                showDelegate1 += ShowMessage2;

                ShowDelegate showDelegate2 = ShowMessage3;
                showDelegate2 += ShowMessage4;

                ShowDelegate showDelegate3 = showDelegate1 + showDelegate2;

                showDelegate3.Invoke();

                Console.ReadKey();
            }

            static void ShowMessage1()
            {
                Console.WriteLine("Метод 1");
            }

            static void ShowMessage2()
            {
                Console.WriteLine("Метод 2");
            }

            static void ShowMessage3()
            {
                Console.WriteLine("Метод 3");
            }

            static void ShowMessage4()
            {
                Console.WriteLine("Метод 4");
            }
        }


            Задание 9.3.6
            В приложении существует две объекта делегата:

                CalculateDelegate calcDelegateOne = CalculateOne;
                CalculateDelegate calcDelegateTwo = CalculateTwo;

            Программист хочет создать третий объект делегата calcDelegateThree и поместить в него предыдущие два: calcDelegateOne и calcDelegateTwo.

                CalculateDelegate calcDelegateThree = calcDelegateOne += calcDelegateTwo;

                CalculateDelegate calcDelegateThree = calcDelegateOne + calcDelegateTwo;    X

                CalculateDelegate calcDelegateThree = calcDelegateThree;

                CalculateDelegate calcDelegateThree += calcDelegateOne + calcDelegateTwo;

            --------------------------------------
            Шаблонные, универсальные или встроенные делегаты
            -------------------------------------- 

            Шаблонные делегаты в C# были представлены как часть .NET Framework 3.5, которая не требует определения экземпляра делегата для вызова методов.

            C# предоставляет три встроенных универсальных делегата, это:

                Func;
                Действие;
                Предикат.

            Давайте разберёмся с необходимостью использования шаблонных делегатов на примере.

            ПРИМЕР

            Допустим, у нас есть следующие три метода, и мы хотим вызывать эти методы с помощью делегатов

        static void ShowMessage()
        {
            Console.WriteLine("Hello World!");
        }

        static int Sum(int a, int b, int c)
        {
            return a + b + c;
        }

        static bool CheckLength(string _row)
        {
            if (_row.Length > 3) return true;
            return false;
        }

            Для вызова данных методов при помощи делегатов создаём три соответствующих делегата.

                delegate void ShowMessageDelegate();
                delegate int SumDelegate(int a, int b, int c);
                delegate bool CheckLengthDelegate(string _row);

            Далее вызываем методы, используя соответствующие экземпляры делегата:

                ShowMessageDelegate showMessageDelegate = ShowMessage;
                showMessageDelegate.Invoke();

                SumDelegate sumDelegate = Sum;
                int result = sumDelegate.Invoke(1, 30, 120);
                Console.WriteLine(result);

                CheckLengthDelegate checkLengthDelegate = CheckLength;
                bool status = checkLengthDelegate.Invoke("skill_factory");
                Console.WriteLine(status);

            Задание 9.3.7
            Попробуйте самостоятельно собрать консольное решение из приведённых выше блоков кода.

                                    public class Program
        {
            delegate void ShowMessageDelegate();
            delegate int SumDelegate(int a, int b, int c);
            delegate bool CheckLengthDelegate(string row);

            static void Main(string[] args)
            {
                ShowMessageDelegate showMessageDelegate = ShowMessage;
                showMessageDelegate.Invoke();

                SumDelegate sumDelegate = Sum;
                int result = sumDelegate.Invoke(1, 30, 120);
                Console.WriteLine(result);

                CheckLengthDelegate checkLengthDelegate = CheckLength;
                bool status = checkLengthDelegate.Invoke("skill_factory");
                Console.WriteLine(status);

                Console.ReadLine();
            }

            static void ShowMessage()
            {
                Console.WriteLine("Hello World!");
            }

            static int Sum(int a, int b, int c)
            {
                return a + b + c;
            }

            static bool CheckLength(string _row)
            {
                if (_row.Length > 3) return true;
                return false;
            }
        }

            Запускаем только что созданное консольное приложение и смотрим на результат:

                Hello World!
                151
                True

            И тут появляется вопрос: обязательно ли нам создавать делегаты? Ответ: нет.

            C# предоставляет три шаблонных (универсальных) делегата, которые могут выполнять эту работу за нас: Func, Action и Predicate.

            
            Шаблонный делегат   Особенности	                                                                Шаблон применения в программе

            Func	            Этот делегат принимает один или несколько входных параметров и возвращает один выходной параметр. 
                                Последний параметр считается возвращаемым значением.
                                Func делегат в C# может принимать до 16 входных параметров различных типов. 
                                Он должен иметь один возвращаемый тип. Тип возврата является обязательным, а входной параметр — нет.

                                На практике: Func используется всякий раз, когда ваш делегат возвращает какое-либо значение, независимо от того, 
                                принимает ли он какой-либо входной параметр или нет.


            Action	            Он принимает один или несколько входных параметров и ничего не возвращает. Этот делегат может принимать до 16 
                                входных параметров разного или одного типа. На практике: Action используется, когда ваш делегат не возвращает никакого 
                                значения, независимо от того, принимает ли он какой-либо входной параметр или нет.

            Predicate	        Этот делегат используется для проверки определенных критериев метода и возвращает вывод как логическое значение True или False.
                                Он принимает один входной параметр и всегда возвращает логическое значение, которое является обязательным. Этот делегат может 
                                принимать максимум 1 входной параметр и всегда возвращает значение логического типа.

                                На практике: Predicate используется в тех случаях, если ваш делегат возвращает только логическое значение (true или false), принимая только один входной параметр.

            Пример Func:

class Program 
{
  static void Main(string[] args) 
  {
    Func < int,
    int,
    int > Addition = AddNumbers;
    int result = Addition(10, 20);
    Console.WriteLine(result);
  }

  private static int AddNumbers(int param1, int param2) 
  {
    return param1 + param2;
  }
}

            Пример Action:

static void Main(string[] args) 
{
  Action < string > action = new Action < string > (Display);
  action("Привет разработчик!");
  Console.Read();
}
static void Display(string message) 
{
  Console.WriteLine(message);
}

            Пример Predicate:

class Program 
{
  static void Main(string[] args) 
  {
    Predicate < string > CheckIfApple = IsApple;
    bool result = CheckIfApple("IPhone X");    if (result) Console.WriteLine("Это IPhone X");
  }

  private static bool IsApple(string modelName) 
  {
    if (modelName == "IPhone X") return true;
    else return false;
  }
}

            В шаблонных делегатах мы можем использовать уже ранее изученные модификаторы параметров in и out. Но особенность их применения в шаблонных 
            делегатах заключается в том, что они не инициируют передачу аргументов по ссылке.

            --------------------------------------
            Пример для закрепления шаблонных делегатов
            -------------------------------------- 
            
            Для понимания шаблонных делегатов вернёмся к нашему верхнему коду. Вспомним, какие методы там у нас были, и выполним применение шаблонных делегатов к ним:

                 Метод ShowMessage, который отображает в консоли текстовое сообщение. Здесь мы будем использовать делегат Action для достижения такого же результата.
    
                 Метод Sum принимает три параметра и возвращает значение типа int. Здесь мы будем использовать делегат Func для достижения такого же результата.

                 Метод CheckLength принимает один строковый параметр и возвращает логическое значение. Здесь мы будем использовать делегат Predicate для достижения такого же результата.

            --------------------------------------
             Action в деле
            -------------------------------------- 
           
            Заменяем данный блок кода:

                ShowMessageDelegate showMessageDelegate = ShowMessage;
                showMessageDelegate.Invoke();
            
            На такой:

                Action showMessageDelegate = ShowMessage;
                showMessageDelegate.Invoke();

            --------------------------------------
             Func в деле
            -------------------------------------- 
           
            Данный блок:

                SumDelegate sumDelegate = Sum;
                int result = sumDelegate.Invoke(1, 30, 120);
                Console.WriteLine(result);
            
            Меняем следующим образом:

                Func < int,int,int,int > sumDelegate = Sum;
                int result = sumDelegate.Invoke(1, 30, 120);
                Console.WriteLine(result);

            Обратите внимание, что в делегате Func<int, int, int, int> первые три параметра int являются входными параметрами, а последний int — возвращаемым значением.

                Это важно запомнить: Всегда последний параметр делегата Func является возвращаемым значением

            --------------------------------------
             Predicate в деле
            -------------------------------------- 

            Для этого заменяем данный блок:

                CheckLengthDelegate checkLengthDelegate = CheckLength;
                bool status = checkLengthDelegate.Invoke("skill_factory");
                Console.WriteLine(status);

            На следующий:

                Predicate < string > checkLengthDelegate = CheckLength;
                bool status = checkLengthDelegate.Invoke("skill_factory");
                Console.WriteLine(status);

                Это важно запомнить: делегат Predicate всегда возвращает только логическое значение (true или false) и имеет не более одного входного параметра.

            Задание 9.3.8
            Используя консольное решение из предыдущей задачи 9.3.7, реализуйте применение шаблонных делегатов, описанных выше.

                                public class Program
        {
            static void Main(string[] args)
            {
                Action showMessageDelegate = ShowMessage;
                showMessageDelegate.Invoke();

                Func<int,
                int,
                int,
                int> sumDelegate = Sum;
                int result = sumDelegate.Invoke(1, 30, 120);
                Console.WriteLine(result);

                Predicate<string> checkLengthDelegate = CheckLength;
                bool status = checkLengthDelegate.Invoke("skill_factory");
                Console.WriteLine(status);

                Console.ReadLine();
            }

            static void ShowMessage()
            {
                Console.WriteLine("Hello World!");
            }

            static int Sum(int a, int b, int c)
            {
                return a + b + c;
            }

            static bool CheckLength(string _row)
            {
                if (_row.Length > 3) return true;
                return false;
            }
        }

            Задание 9.3.9
            В приложении существует следующая функция:

                static void ShowMessage(string message) 
                {
                    Console.WriteLine(message);
                }

            Выберите верную реализацию шаблонного делегата Action к данной функции:

                Action<string> action = ShowMessage;                X

                Action action = ShowMessage;

                Action<string> action = ShowMessage();

                Action < string > action = ShowMessage("Hello");

            Задание 9.3.10
            В приложении существует следующая функция:

                static int Calculate(int a, int b) 
                {
                    return a + b;
                }
        
            Выберите верную реализацию шаблонного делегата Func к данной функции:

                Func<int, int> result = Calculate;

                Func<int, int, int> result = Calculate;         X

                Func result = Calculate(int a, int b);

                Func<int> result = Calculate;

            Задание 9.3.11
            В приложении существует следующая функция:

                static bool IsFive(int a) 
                {
                    if (a == 5) return true;
                    return false;
                }

                Predicate isFive = IsFive(10);

                Predicate<int> isFive = IsFive(10);

                Predicate isFive = IsFive;

                Predicate<int> isFive = IsFive;     X

            --------------------------------------
            Анонимные методы
            --------------------------------------

            Анонимные методы в C# могут быть определены с помощью ключевого слова delegate и могут быть назначены переменной типа делегата. 
            Другими словами, анонимный метод — это метод без имени. Вы наверное уже спрашиваете, для чего нужны анонимные методы? Рассмотрим на примерах.

            Предположим, у нас есть простой класс с некоторым делегатом:

     namespace DelegateDemo
    {
        public class AnonymousMethods
        {
            public delegate string GreetingsDelegate(string name);
            public static string Greetings(string name)
            {
                return "Привет @" + name + "! Добро пожаловать на SkillFactory!";
            }

            static void Main(string[] args)
            {
                GreetingsDelegate gd = new GreetingsDelegate(AnonymousMethods.Greetings);
                string GreetingsMessage = gd.Invoke("Будущий гуру");
                Console.WriteLine(GreetingsMessage);
                Console.ReadKey();
            }
        }
    }

            Алгоритм приведённого выше примера следующий:

                Создание делегата.
                Создание экземпляра делегата.
                Вызов делегата.

            Но что если я скажу вам, что можно с помощью анонимного метода сократить данный код?

    namespace DelegateDemo
    {
        public class AnonymousMethods
        {
            public delegate string GreetingsDelegate(string name);

            static void Main(string[] args)
            {
                GreetingsDelegate gd = delegate (string name)
                {
                    return "Привет @" + name + " и добро пожаловать на SkillFactory!";
                };
                string GreetingsMessage = gd.Invoke("Pranaya");
                Console.WriteLine(GreetingsMessage);
                Console.ReadKey();
            }
        }
    }

            Обратим внимание на данный участок кода:

            delegate(string name) 
            {
                 return "Привет @" + name + " и добро пожаловать на SkillFactory!";
            };

            Он не имеет имени и содержит только тело, а метод определяется с использованием ключевого слова delegate. Нам не требуется писать 
            какие-либо модификаторы доступа, такие как public, private, protected и т.д. Нам также не требуется писать какие-либо возвращаемые типы, 
            такие как void, int, double и т.д.

            Например, у нас есть делегат:

                delegate int CalculateDelegate(int a, int b);

            И есть функция, суммирующая два числа:

                static int Calculate(int a, int b) 
                {
                    return a + b;
                }

            Благодаря анонимному методу мы можем посчитать сумму двух чисел без применения функции Calculate метода. Её мы может отпустить и не использовать 
            в коде, тем самым сократив код приложения:

    namespace DelegatePractices
    {
        class Program
        {
            delegate int CalculateDelegate(int a, int b);
            static void Main(string[] args)
            {
                CalculateDelegate calculateDelegate = delegate (int a, int b)
                {
                    return a + b;
                };

                int result = calculateDelegate.Invoke(50, 10);
            }
        }
    }
            Каковы преимущества использования анонимных методов в C#?

            Банально, но меньше печатать. Как правило, анонимные методы используются, когда объем кода очень мал.

            Это важно: анонимные методы имеют доступ к переменным во внешней среде.
            Например, мы можем сделать так:


    namespace DelegateDemo
    {
        public class AnonymousMethods
        {
            public delegate string GreetingsDelegate(string name);

            static void Main(string[] args)
            {
                string Message = "добро пожаловать на SkillFactory!";
                GreetingsDelegate gd = delegate (string name)
                {
                    return "Привет @" + name + " " + Message;
                };
                string GreetingsMessage = gd.Invoke("Будущий гуру");
                Console.WriteLine(GreetingsMessage);
                Console.ReadKey();
            }
        }
    }

            Несмотря на то что, переменная string Message объявлена ранее анонимного делегата, анонимный делегат успешно имеет к ней доступ. 
            Это необходимо запомнить.

            Задание 9.3.12
            Существует следующее консольное решение, которое отображает сообщение Hello World в консольном сообщении:

    namespace DelegatePractices
    {
        class Program
        {
            delegate void ShowMessageDelegate(string _message);
            static void Main(string[] args)
            {
                ShowMessageDelegate showMessageDelegate = ShowMessage;
                showMessageDelegate.Invoke("Hello World!");
                Console.Read();
            }

            static void ShowMessage(string _message)
            {
                Console.WriteLine(_message);
            }
        }
    }

            Реализуйте в данном решении анонимный метод, не сломав логику приложения.

    namespace DelegatePractices
    {
        class Program
        {
            delegate void ShowMessageDelegate(string _message);
            static void Main(string[] args)
            {
                ShowMessageDelegate showMessageDelegate = delegate (string _message)
                {
                    Console.WriteLine(_message);
                };
                showMessageDelegate.Invoke("Hello World!");
                Console.Read();
            }
        }

            Задание 9.3.13
            Существует консольное решение, которое выводит случайное целое число в диапазоне от 0 до 100 и отображает результат в консольное сообщение:

    namespace DelegatePractices
    {
        class Program
        {
            delegate int RandomNumberDelegate();
            static void Main(string[] args)
            {
                RandomNumberDelegate randomNumberDelegate = RandomNumber;
                int result = randomNumberDelegate.Invoke();
                Console.WriteLine(result);
                Console.Read();
            }

            static int RandomNumber()
            {
                return new Random().Next(0, 100);
            }
        }
    }

            Реализуйте в данном решении анонимный метод, не сломав логику приложения.

    namespace DelegatePractices
    {
        class Program
        {
            delegate int RandomNumberDelegate();
            static void Main(string[] args)
            {
                RandomNumberDelegate randomNumberDelegate = delegate
                {
                    return new Random().Next(0, 100);
                };
                int result = randomNumberDelegate.Invoke();
                Console.WriteLine(result);
                Console.Read();
            }
        }
    }

            --------------------------------------
            Лямбда-выражения
            ------------------------------------            
            Лямбда-выражения в C# являются сокращением для записи анонимной функции. Таким образом, мы можем сказать, что лямбда-выражение 
            в C# — это не что иное, как упрощение анонимной функции.

            Правило оформления лямбда-выражения: Чтобы создать лямбда-выражение в C#, нам нужно указать входные параметры (если есть) в левой части лямбда-оператора =>, а в правой части нам нужно поместить блок выражения.
            Пример использования лямбда-оператора:

                delegate int Calculate(int a, int c);
                static void Main(string[] args) 
                {
                  Calculate calculation = (x, y) =>x + y;
                  Console.WriteLine(calculation(10, 20));
                  Console.WriteLine(calculation(40, 20));
                  Console.Read();
                }

            Ну вот и подошёл к концу юнит по делегатам. В скринкасте разберём ещё более интересное применение делегатов на практике:

            Задание 9.3.14
            Используя консольное решение из задачи 9.3.12, реализуйте лямбда-оператор во время вызова анонимного метода.

    namespace DelegatePractices
    {
        class Program
        {
            delegate void ShowMessageDelegate(string _message);
            static void Main(string[] args)
            {
                ShowMessageDelegate showMessageDelegate = (string _message) =>
                {
                    Console.WriteLine(_message);
                };
                showMessageDelegate.Invoke("Hello World!");
                Console.Read();
            }
        }
    }

            Задание 9.3.15
            Используя консольное решение из задачи 9.3.13, реализуйте лямбда-оператор во время вызова анонимного метода.

     namespace DelegatePractices
    {
        class Program
        {
            delegate int RandomNumberDelegate();
            static void Main(string[] args)
            {
                RandomNumberDelegate randomNumberDelegate = () =>
                {
                    return new Random().Next(0, 100);
                };
                int result = randomNumberDelegate.Invoke();
                Console.WriteLine(result);
                Console.Read();
            }
        }
    }
            */

            Employee empl1 = new Employee()
            {
                ID = 55,
                Name = "Алексей",
                Expirience = 5,
                Salary = 20000
            };

            Employee empl2 = new Employee()
            {
                ID = 56,
                Name = "Михаил",
                Expirience = 2,
                Salary = 10000
            };

            Employee empl3 = new Employee()
            {
                ID = 57,
                Name = "Николоай",
                Expirience = 4,
                Salary = 15000
            };

            List<Employee> IsEmployees = new List<Employee>();
            ListEmployees.Add(empl1);
            ListEmployees.Add(empl2);
            ListEmployees.Add(empl3);

            ELigibleToPromotion eligibleToPromotion = Promote;
            Employee.PromoteEmployee(k=listEmployees, eligibleToPromotion);


        }

        public delegate bool ELigibleToPromotion(Employee EmployeeToPromotion);

        public static bool Promote(Employee employee)
        {
            if (employee.Salary > 10000)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    public class Employee
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Expirience { get; set; }
            public int Salary { get; set; }

            public static void PromoteEmployee(List<Employee> listEmployees, eligibleToPromotion IsEmployeeEligible)
            {
                foreach (Employee in listEmployees)
                {
                    if (IsEmployeeEligible(employee))
                        Console.WriteLine("Employee {0} Promoted", employee.Name);
                }
            }
        }

    }

}



using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    class Program
    {
        static void Main(string[] args)
        {
            //Модуль 9. Обработка исключений. Делегаты
            /* 9.1.Исключения
            
            Исключение (англ. Exception) — событие, которое может стать причиной отказа программы.

            В языке программирования C# присутствует класс System.Exception. Именно данный класс будет будет содержать в себе все сведения 
            об исключении, если таковое произошло. Всевозможные исключения, определяемые на уровне пользователя и системы, в конечном итоге 
            наследуются только от System.Exception. 

            Ниже показано, как в целом выглядит этот класс. Некоторые члены класса являются виртуальными и могут переопределяться в производных классах. 

            }
            public class Exception : ISerializable, _Exception
            {
            // общедоступные конструкторы
            public Exception(string message, Exception innerException);
            public Exception(string message);
            public Exception();
            ...
            // методы
            public virtual Exception GetBaseException();
            public virtual void GetObjectData(Serializationlnfо info,
            StreamingContext context);
            // свойства
            public virtual IDictionary Data { get; }
            public virtual string HelpLink { get; set; }
            public Exception InnerException { get; }
            public virtual string Message { get; }
            public virtual string Source { get; set; }
            public virtual string StackTrace { get; }
            public MethodBase TargetSite { get; }
            ...

            Давайте рассмотрим более подробно конструкторы, свойства и методы класса Exception в таблице.

            Общедоступные конструкторы класса Exception

                Exception()	Инициализирует новый экземпляр класса Exception.

                Exception(SerializationInfo, StreamingContext)	Инициализирует новый экземпляр класса Exception с сериализованными данными.

                Exception(String)	Инициализирует новый экземпляр класса Exception с указанным сообщением об ошибке.

                Exception(String, Exception)	Инициализирует новый экземпляр класса Exception с указанным сообщением об ошибке и ссылкой 
                на внутреннее исключение, вызвавшее данное исключение.

            Свойства класса Exception

                Data	Возвращает коллекцию пар «ключ-значение», предоставляющую дополнительные сведения об исключении. 
                В частых случаях в данное свойство вставляем дату исключения.
               
                HelpLink	Получает или задает ссылку на файл справки, связанный с этим исключением.
                
                HResult	Возвращает или задает HRESULT — кодированное числовое значение, присвоенное определенному исключению.
                
                InnerException	Возвращает экземпляр класса Exception, который вызвал текущее исключение.
                
                Message	Возвращает сообщение, описывающее текущее исключение.
                
                Source	Возвращает или задает имя приложения или объекта, вызвавшего ошибку.
                
                StackTrace	Получает строковое представление непосредственных кадров в стеке вызова.
                
                TargetSite	Возвращает метод, создавший текущее исключение.

            Методы класса Exception

                Equals(Object)	Определяет, равен ли указанный объект текущему объекту.

                GetBaseException()	При переопределении в производном классе возвращает исключение Exception, которое является первопричиной 
                одного или нескольких последующих исключений.

                GetHashCode()	Служит в качестве хэш-функции по умолчанию.
                (Унаследовано от Object)

                GetObjectData(SerializationInfo, StreamingContext)	При переопределении в производном классе задает объект SerializationInfo со сведениями об исключении.

                GetType()	Возвращает тип среды выполнения текущего экземпляра.

                MemberwiseClone()	Создает неполную копию текущего объекта Object.
                (Унаследовано от Object)

                ToString()	Создает и возвращает строковое представление текущего исключения.

            Как мы видим, класс Exception не слишком сложен по структуре, но знания его свойств и особенностей намного сократят время на практике. 

            Но что если мы захотим создать свой вид исключения (Custom exception), унаследованный от класса Exception? Нам поможет одно из 
            свойств объектно-ориентированного-программирования…

            Задание 9.1.1
            Какое из свойств ООП нам поможет в создании собственного класса исключения, унаследованного от класса System.Exception?

                Полиморфизм
                Наследование    X
                Инкапсуляция

            public class MyException : Exception
            {
                 public MyException()
                 { }

                public MyException(string message)
                : base(message)
                { }
            }

            В данном коде мы создали класс MyException. Унаследовали его от класса Exception. В конструкторе MyException принимаем обязательный 
            параметр string message и передаем его в конструктор унаследованного класса Exception. 

            Теперь появляется вопрос: для чего нам создавать собственный класс исключения, если можно использовать стандартный класс Exception?

            Допустим, у нас есть веб-сервис, реализованный на Net.Core. В данном сервисе используется собственный класс исключения HumanException. 
            Он наследуется от класса ArgumentException по аналогии с примером выше.

                 public class HumanException: ArgumentException 
                {
                    public HumanException(string _exceptionMessage): base(_exceptionMessage) { }
                }

            В данном сервисе есть функция по аутентификации пользователя:

                 public AccountDTO Authenticate(string _userName, string _password) 
                {
                     UserEntity findUser = base.FindByUsername(_userName);
                     if (findUser is null) throw new HumanException("Пользователь не найден в системе.");
                     if (findUser.password != _password) throw new HumanException("Пароль не корректный.");
                     return new AccountDTO(findUser, _token.Generate(findUser.id), roleRepository.FindByUserId(findUser.id));
                }

            Данная функция в случае, если пользователь не найден или пароль не корректен, выбрасывает пользовательское исключение HumanException 
            при помощи оператора throw с сообщением об ошибке. Нам нужно, чтобы это сообщение отобразилось у пользователя на сайте. Оператор throw 
            мы подробно рассмотрим в следующем юните.

            В данном веб-сервисе используется глобальный перехватчик всех исключений в сервисе под названием ExceptionHandler:

         public class ExceptionHandler : ActionFilterAttribute, IExceptionFilter
        {
            readonly Error _error = new Error();
            public void OnException(ExceptionContext context)
            {
                _error.Write(context.Exception.ToString());
                string errorMessage = "Произошла непредвиденная ошибка в приложении. Администрация сайта уже бежит на помощь.";
                if (context.Exception is HumanException) errorMessage = context.Exception.Message;
                context.Result = new BadRequestObjectResult(errorMessage);
            }
        }

            И вот тут стоит обратить внимание на код и увидеть огромный плюс в использовании пользовательского исключения HumanException. В метод OnException 
            залетает класс ExceptionContext, который содержит в себе класс Exception. Данный класс мы можем проверить, является ли он объектом класса HumanException 
            или нет. Если является, сообщение об ошибке оставить как есть, если нет, то использовать сообщение об ошибке по умолчанию (errorMessage).

            Сообщение об ошибке по умолчанию для пользователя находится в строковой переменной типа string.

                string errorMessage = "Произошла непредвиденная ошибка в приложении. Администрация сайта уже бежит на помощь.";

            И благодаря сравнению ниже мы либо переопределяем сообщение об ошибке, либо оставляем по умолчанию. 

                if (context.Exception is HumanException) errorMessage = context.Exception.Message;

            Именно благодаря реализации класса HumanException мы решили одну из задач приложения и сделали это быстро и эффективно. 

                Исключение	                    Условие

                ArgumentException	            Непустой аргумент, передаваемый в метод, является недопустимым.
                ArgumentNullException	        Аргумент, передаваемый в метод — null.
                ArgumentOutOfRangeException	    Аргумент находится за пределами диапазона допустимых значений.
                DirectoryNotFoundException	    Недопустимая часть пути к каталогу.
                DivideByZeroException	        Знаменатель в операции деления или целого числа Decimal равен нулю.
                DriveNotFoundException	        Диск недоступен или не существует.
                FileNotFoundException	        Файл не существует.
                FormatException         	    Значение не находится в соответствующем формате для преобразования из строки методом преобразования, например Parse .
                IndexOutOfRangeException	    Индекс находится за пределами границ массива или коллекции.
                InvalidOperationException	    Вызов метода недопустим в текущем состоянии объекта.
                KeyNotFoundException	        Не удается найти указанный ключ для доступа к элементу в коллекции.
                NotImplementedException	        Метод или операция не реализованы.
                NotSupportedException	        Метод или операция не поддерживается.
                ObjectDisposedException	        Операция выполняется над объектом, который был ликвидирован.
                OverflowException	            Арифметическое, приведение или операция преобразования приводят к переполнению.
                PathTooLongException	        Длина пути или имени файла превышает максимальную длину, определенную системой
                PlatformNotSupportedException	Операция не поддерживается на текущей платформе.
                RankException	                В метод передается массив с неправильным числом измерений.
                TimeoutException	            Срок действия интервала времени, выделенного для операции, истек.
                UriFormatException	            Используется недопустимый универсальный код ресурса (URI).

            Например, мы заметили в таблице уже знакомое нам условие — попытка деления числа на ноль. Такое исключение называется DivideByZeroException

            Данная таблица нам ещё понадобится в следующем юните, в котором мы будем обрабатывать исключения в соответствующем блоке.

            В скринкасте я отмечу наиболее часто встречающиеся на практике свойства Exception, которые нужно обязательно использовать, и покажу, для чего это делать:

            Задание 9.1.2
            Используя таблицу с видами исключений, решите следующие задачи:

            1. Какое исключение сработает, если пользователь пытается записать данные в файл, которого нет?

                FileException
                FileNullException
                FileNotFoundException       X
                FileReferenceException

            2. Какое исключение сработает, если диск недоступен или защищён от записи?

                DriveNotFoundException      X
                DiskNotFoundException
                DriverFullException
                DriverException

            Какое исключение сработает, если истёк срок действия интервала времени, выделенного для операции?

                TimeException
                TimeoutException            X
                PathTooLongException
                ObjectDisposedException

            Задание 9.1.3
            Создайте экземпляр класса исключения Exception и добавьте в свойство Data дату создания исключения.

    namespace TryCatchPractices
    {
        class Program
        {
            static void Main(string[] args)
            {

                Exception exception = new Exception();
                exception.Data.Add("Дата создания исключения : ", DateTime.Now);
            }
        }
    }

            Задание 9.1.4
            Создайте класс исключения Exception и переопределите его свойство Message, а также свойство HelpLink, добавив в него ссылку на внешний ресурс.

    namespace TryCatchPractices
    {
        class Program
        {
            static void Main(string[] args)
            {

                Exception exception = new Exception("Собственное исключения");
                exception.HelpLink = "www.google.ru";
            }
        }
    }
             */

            Exception exception = new Exception("Произошло исключение в БД");
            exception.Data.Add("Дата создания исключения", DateTime.Now);
            exception.HelpLink = "www.google.com";        }
    }
}

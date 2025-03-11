using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    class Program
    {
        static void Main(string[] args)
        {
            //Модуль 9. Обработка исключений. Делегаты
            /* 9.4. Ковариантность и контравариантность делегатов
             
            Делегаты становятся ещё более гибкими средствами программирования благодаря двум свойствам: ковариантности и контравариантности.

            Как правило метод, передаваемый делегату, должен иметь такой же возвращаемый тип и сигнатуру, как и делегат.Но в отношении производных типов это правило 
            оказывается не таким строгим благодаря ковариантности и контравариантности. В частности, ковариантность позволяет присвоить делегату метод, возвращаемым 
            типом которого служит класс, производный от класса, указываемого в возвращаемом типе делегата. А контравариантность позволяет присвоить делегату метод, 
            типом параметра которого служит класс, являющийся базовым для класса, указываемого в объявлении делегата.

            --------------------------------------
            Ковариация в делегатах
            --------------------------------------

            Ковариация — это когда мы можем объявлять делегат и назначать ему методы другой сигнатуры, но которые являются производными от основного метода.
            Рассмотрим пример, который показывает ковариацию делегатов.

            Мы создали класс Car и класс BMW, унаследованный от класса Car:

                class Car
                {
                    public string Model { get; set; }
                }
                class BMW : Car { }

            Не смотря на то, что сигнатура делегата не соответствует сигнатуре метода BuildBWM, мы можем сделать вот так, благодаря ковариации делегатов:

        delegate Car CarDelegate(string name);
        static void Main(string[] args)
        {
            CarDelegate carDelegate;
            carDelegate = BuildBMW; // ковариантность
            Car c = carDelegate("X6");
            Console.WriteLine(c.Model);
            Console.Read();
        }
        private static BMW BuildBMW(string model)
        {
            return new BMW { Model = model };
        }

            --------------------------------------
            Контравариантность в делегатах
            --------------------------------------

                Контравариантность — это когда мы можем объявлять делегат и назначать ему методы другой сигнатуры, но которые являются более 
                универсальными по отношению к типу параметра делегата.

            Несмотря на то, что делегат в качестве параметра принимает объект BMW, ему можно присвоить метод, принимающий в качестве параметра 
            объект базового типа Car. Может показаться на первый взгляд, что здесь есть некоторое противоречие, то есть использование более 
            универсального типа вместо более производного.

            Однако, в реальности в делегат при его вызове мы всё равно можем передать только объекты типа Car, а любой объект типа Car 
            является объектом типа BMW , который используется в методе.

        public class Program
        {
            delegate void BwmInfo(BMW bwm);
            static void Main(string[] args)
            {
                BwmInfo bmwInfo = GetCarInfo; // контравариантность
                BMW bwm = new BMW
                {
                    Model = "X6"
                };
                bmwInfo(bwm);
                Console.Read();
            }

            private static void GetCarInfo(Car p)
            {
                Console.WriteLine(p.Model);
            }
        }

            Задание 9.4.1
            Какой принцип демонстрирует данный код?

        class Animal
        {
            public string Name
            {
                get;
                set;
            }
        }
        class Penguin : Animal { }

        public class Program
        {

            delegate Animal AnimalDelegate(string name);
            static void Main(string[] args)
            {
                AnimalDelegate animalDelegate;
                animalDelegate = BuildPeguin;
                Animal animal = animalDelegate("Josh");
                Console.WriteLine(animal.Name);
                Console.Read();
            }
            private static Penguin BuildPeguin(string name)
            {
                return new Penguin
                {
                    Name = name
                };
            }
        }

            Ковариантность          X
            Контравариантность  

            Задание 9.4.2
            На основе скринкаста создайте консольное приложение, в котором реализуйте демонстрацию ковариантности делегатов при помощи следующей модели классов:

                class Car {}
                class Lexus: Car {}

    namespace CovAndContrPractices
    {
        class Program
        {
            public delegate Car HandlerMethod();

            public static Car CarHandler()
            {
                return null;
            }

            public static Lexus LexusHandler()
            {
                return null;
            }

            static void Main(string[] args)
            {
                HandlerMethod handlerLexus = LexusHandler;

                Console.Read();
            }
        }

        class Car { }

        class Lexus : Car { }
    }
            
            Задание 9.4.3
            На основе скринкаста создайте консольное приложение, в котором реализуйте демонстрацию контравариантности делегатов при помощи следующей модели классов:

                class Parent {}
                class Child: Parent {}

    namespace CovAndContrPractices
    {
        class Program
        {
            public delegate Parent HandlerMethod();
            delegate void ChildInfo(Child child);

            public static Parent ParentHandler()
            {
                return null;
            }


            public static Child ChildHandler()
            {
                return null;
            }

            static void Main(string[] args)
            {
                ChildInfo childInfo = GetParentInfo;

                childInfo.Invoke(new Child());

                Console.Read();

            }

            public static void GetParentInfo(Parent p)
            {
                Console.WriteLine(p.GetType());
            }
        }

        class Parent { }

        class Child : Parent { }
    }

            */

            //Пример ковариации в делегатах - это работает, т.к. класс Dog унаследован из класса Animal
            HandlerMethod handlerMethod = AnimalHandler;
            HandlerMethod handlerDog = DogHandler;

            //показ контрвариантности в делегатах
            DogInfo doginfo = GetAnimalInfo;
            doginfo.Invoke(new Dog());
            Console.Read();

        }

        public delegate Animal HandlerMethod();
        //Данные для показа контрвариантности в делегатах
        delegate void DogInfo(Dog dog);
        //
        

        public static Dog DogHandler()
        {
            return null;
        }

        public static Animal AnimalHandler()
        {
            return null;
        }

        //Данные для показа контрвариантности в делегатах
        public static void GetAnimalInfo(Animal p)
        {
            Console.WriteLine(p.GetType());
        }
        //

    }

    class Animal { }
    class Dog : Animal { }

}

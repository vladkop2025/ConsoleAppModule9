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
            /* 9.5. События

            Событие — это уведомление, отправляемое объектом, чтобы сигнализировать о возникновении действия.

            Класс, который вызывает события, называется издателем, а класс, который получает уведомление, называется подписчиком. На одно событие может 
            быть несколько подписчиков. Обычно издатель вызывает событие, когда произошло какое-то действие. Подписчики, которые заинтересованы в получении 
            уведомления о совершении действия, должны зарегистрироваться в событии и обработать его.

            Событие объявляется в два этапа:

                Объявление делегата.
                Объявление переменной делегата с ключевым словом Event.

            В следующем примере показано, как объявить событие:

                public delegate void Notify();  // делегат                
                public class ProcessBusinessLogic
                {
                    public event Notify ProcessCompleted; // событие
                }

            В приведенном выше примере мы объявили делегат Notify (от англ. уведомление), а затем объявили событие ProcessCompleted делегата Notify, 
            используя ключевое слово event. Таким образом, ProcessBusinessLogic класс называется издателем. Notify делегат определяет подписку под 
            названием ProcessCompleted. Он указывает, что метод обработчика событий в классе подписчика должен иметь тип возврата void и никаких параметров.

            Теперь посмотрим, как заюзать ProcessCompleted событие на примере. Рассмотрим следующую реализацию:


        public delegate void Notify();  // делегат
        public class ProcessBusinessLogic
        {
            public event Notify ProcessCompleted; // событие

            public void StartProcess()
            {
                Console.WriteLine("Процесс начат!");
                OnProcessCompleted();
            }

            protected virtual void OnProcessCompleted() //protected virtual method
            {
                ProcessCompleted?.Invoke();
            }
        }

            Выше StartProcess() метод вызывает метод onProcessCompleted() в конце, который вызывает событие.

            OnProcessCompleted() метод вызывает делегат, используя ProcessCompleted?.Invoke(). Это вызовет все методы обработчика событий, зарегистрированные 
            в ProcessCompleted событии.

            Класс подписчика должен зарегистрироваться для ProcessCompleted события и обработать его с помощью метода, подпись которого соответствует
            Notify делегату, как показано ниже:

        class Program
        {
            public static void Main()
            {
                ProcessBusinessLogic bl = new ProcessBusinessLogic();
                bl.ProcessCompleted += bl_ProcessCompleted; // регистрируем событие
                bl.StartProcess();
            }

            // перехватчик событий
            public static void bl_ProcessCompleted()
            {
                Console.WriteLine("Процесс завершён!");
            }
        }

            Program класс является подписчиком ProcessCompleted события. Он регистрируется в событии с помощью оператора +=. Помните, что таким же образом 
            мы добавляем методы в список вызовов многоадресного делегата. bl_ProcessCompleted() метод обрабатывает событие, поскольку он совпадает с подписью Notify делегата.

            Задание 9.5.1
            Какой оператор используется для регистрации событий?

                +=      X
                -=
                +
                -

            Задание 9.5.2
            В программе определён делегат:

                public delegate string MessageDelegate();

            Как правильно определить событие для данного делегата?

                public event MessageDelegate MessageEvent;              X

                public event MessageDelegate MessageEvent();

                public event string MessageDelegate MessageEvent()

                public event MessageDelegate<string> MessageEvent;

            Задание 9.5.3
            В программе определён делегат:

                public delegate int CalculateDelegate(int a, int b);

            Как правильно определить событие для данного делегата?

                public event CalculateDelegate(int a, int b) CalculateEvent;

                public event CalculateDelegate<int> CalculateEvent();

                public event CalculateDelegate CalculateEvent();

                public event CalculateDelegate CalculateEvent;              X

            */

        }
    }
}
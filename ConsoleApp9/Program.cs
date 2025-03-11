using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            //Пример обработки события
            NumberReader numberReader = new NumberReader();
            numberReader.NumberEnteredEvent += ShowNumber;

            while (true) 
            {
                try
                {
                    numberReader.Read();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введено некорректное значение");
                }
            }
        }

        static void ShowNumber(int number)
        {
            switch (number)
            {
                case 1: Console.WriteLine("Введено значение: 1"); break;
                case 2: Console.WriteLine("Введено значение: 2"); break;
            }
        }
    }

    class NumberReader
    {
        //Объявление делегата
        public delegate void NumberEnteredDelegate(int number);
        //Объявление события
        public event NumberEnteredDelegate NumberEnteredEvent;

        //если public не объявить, то по умолчанию метлд private и не доступен в Main
        public void Read()
        {

            Console.WriteLine();
            Console.WriteLine("Необходимо ввести значение: либо 1, либо 2");

            int number = Convert.ToInt32(Console.ReadLine());

            //если введено число не то, сами поднимаем исключение
            if (number != 1 && number != 2) throw new FormatException();

            //Если введено правильное значение, обрабатываем событие
            NumberEntered(number);

        }

        //Метод для вызова события NumberEnteredEvent
        protected virtual void NumberEntered(int number)
        {
            NumberEnteredEvent?.Invoke(number);
        }

    }
}

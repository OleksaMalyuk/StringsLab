using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StringsLab

{
    class StringBase : IComparable              // базовый класс Строка
    {
        protected string value;                 // поле содержит символы строки
        public StringBase()                     // конструктор по умолчанию
        {
            this.value = "";
        }
        public StringBase(char ch)              // конструктор из символа
        {
            this.value = Convert.ToString(ch);
        }
        public StringBase(string s)             // конструктор из строкового литерала
        {
            this.value = s;
        }

        public int Length()                     // возвращает длину строки
        {
            return value.Length;
        }
        public void Clear()                     // очищает строку
        {
            this.value = "";
        }

        override public bool Equals(object obj)             // проверяет равенство строк по значению
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            StringBase temp = (StringBase)obj;
            return this.value == temp.value;
        }
        override public int GetHashCode()                   // надо для того чтобы работало :)
        {
            return value.GetHashCode();
        }
        override public string ToString()                   // Возвращает строку, представляющую данный объект
        {
            return this.value;
        }
        virtual public int CompareTo(object obj)            // Сравнивает строку по значению с другой в алфавитном порядке
        {
            StringBase temp = (StringBase)obj;
            return this.value.CompareTo(temp.value);
        }
        virtual public object Addition(object obj)          // Прибавляет к строке другую
        {
            StringBase temp = (StringBase)obj;
            return new StringBase(this.value + temp.value);
        }
        virtual public object Subtraction(object obj)       // Вычитание (не имеет смысла для строк, должен быть перекрыт в классе-потомке)
        {
            return null;
        }

        // перегрузка операторов +- <>
        public static StringBase operator +(StringBase s1, StringBase s2)
        {
            return (StringBase)s1.Addition(s2);
        }
        public static StringBase operator -(StringBase s1, StringBase s2)
        {
            return (StringBase)s1.Subtraction(s2);
        }
        public static bool operator >(StringBase s1, StringBase s2)
        {
            return s1.CompareTo(s2) > 0;
        }
        public static bool operator <(StringBase s1, StringBase s2)
        {
            return s1.CompareTo(s2) < 0;
        }
    }

    class StringDecimal : StringBase                        // производный класс "Десятичная строка" от класса "Строка"
    {
        protected int number_value;                         // содержит числовое выражение строки (для совершения арифм. операций)
        public StringDecimal()                              // конструктор по умолчанию
        {
            this.number_value = 0;
            this.value = "0";
        }
        public StringDecimal(char ch)                       // конструктор из символа                       
        {
            this.number_value = (int)Char.GetNumericValue(ch);
            if (this.number_value == -1)
                this.number_value = 0;
            this.value = Convert.ToString(this.number_value);
        }
        public StringDecimal(string s)                      // конструктор из строки
        {
            try
            {
                this.number_value = Convert.ToInt32(s);
            }
            catch
            {
                this.number_value = 0;
            }
            this.value = Convert.ToString(number_value);
        }
        public StringDecimal(int number)                    // конструктор из числа
        {
            this.number_value = number;
            this.value = Convert.ToString(number);
        }

        override public bool Equals(object obj)             // проверяет арифметическое равенство строк (как два числа)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            StringDecimal temp = (StringDecimal)obj;
            return number_value == temp.number_value;
        }
        override public int GetHashCode()
        {
            return number_value.GetHashCode();
        }
        override public int CompareTo(object obj)           // Сравнивает строку по значению с другой как два числа
        {
            StringDecimal temp = (StringDecimal)obj;
            if (this.number_value > temp.number_value) return 1;
            if (this.number_value < temp.number_value) return -1;
            return 0;
        }
        override public object Addition(object obj)         // Арифметическое сложение
        {
            try
            {
                StringDecimal temp = (StringDecimal)obj;
                return new StringDecimal(this.number_value + temp.number_value);
            }
            catch
            {
                return base.Addition(obj);                  // если не удалось сложить как числа - складываем как строки
            }
        }
        override public object Subtraction(object obj)      // Арифметическое вычитание
        {
            try
            {
                StringDecimal temp = (StringDecimal)obj;
                return new StringDecimal(this.number_value - temp.number_value);
            }
            catch
            {
                return base.Subtraction(obj);                  // если не удалось сложить как числа - складываем как строки
            }
        }
    }

    class Classl
    {

        public static void PrintArray(string header, Array a)   // выводит массив
        {
            Console.WriteLine(header);
            foreach (object x in a)
            {
                Console.Write("\"{0}\"\t", x);
            }
            Console.WriteLine();
        }

        static void Main()
        {
            const int n = 7;
            StringBase[] mas = new StringBase[n];           // Создание массива объектов базового типа Строка

            mas[0] = new StringDecimal(150);                // Заполнение вперемешку объектами как базового так и производного типов
            mas[1] = new StringDecimal(" +125 ");             // тут заполняем объектами типа Десятичная строка
            mas[2] = new StringDecimal("-25");
            mas[3] = new StringDecimal('2');
            mas[4] = new StringDecimal("qwerty");
            mas[5] = new StringBase("qwerty");              // тут добавим несколько объектов типа Строка
            mas[6] = new StringBase("abc");

            Console.WriteLine("\nДемонстрация операций над строками:");
            Console.WriteLine("\"{0}\" = \"{1}\"  \t is {2}", mas[0], mas[1], mas[0] == mas[1]);
            Console.WriteLine("\"{0}\" != \"{1}\"  \t is {2}", mas[2], mas[4], mas[2] != mas[4]);
            Console.WriteLine("\"{0}\" > \"{1}\"  \t is {2}", mas[0], mas[1], mas[0] > mas[1]);
            Console.WriteLine("\"{0}\" < \"{1}\"  \t is {2}", mas[2], mas[3], mas[2] < mas[3]);
            Console.WriteLine("\"{0}\" + \"{1}\"  \t = {2}", mas[0], mas[1], (mas[0] + mas[1]));
            Console.WriteLine("\"{0}\" - \"{1}\"  \t = {2}", mas[0], mas[1], (mas[0] - mas[1]));
            Console.WriteLine("\"{0}\" + \"{1}\"  \t = {2}", mas[5], mas[6], (mas[5] + mas[6]));
            Console.WriteLine("\"{0}\" + \"{1}\"  \t = {2}", mas[0], mas[6], (mas[0] + mas[6]));

            Console.WriteLine("\nДлина строки \"{0}\" = {1}", mas[1], mas[1].Length());
            Console.WriteLine("Cтрока до очистки \"{0}\"", mas[6]);
            mas[6].Clear();
            Console.WriteLine("Cтрока после очистки \"{0}\"", mas[6]);

            PrintArray("\nИсходный массив", mas);
            Array.Sort(mas);                                // сортировка массива (демонстрация работы метода CompareTo)
            PrintArray("Отсортированный массив", mas);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringsLab
{

    class StringBase                            // базовый класс Строка
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
         public int CompareTo(StringDecimal s2)           // Сравнивает строку по значению с другой как два числа
        {
            if (this.number_value > s2.number_value) return 1;
            if (this.number_value < s2.number_value) return -1;
            return 0;
        }
        public StringDecimal Subtraction(StringDecimal s2)      // Арифметическое вычитание
        {
            return new StringDecimal(this.number_value - s2.number_value);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            StringDecimal s1 = new StringDecimal(30);
            StringDecimal s2 = new StringDecimal("-20");
            StringDecimal s3 = s1.Subtraction(s2);
            Console.WriteLine("\"{0}\" - \"{1}\" = \"{2}\"", s1, s2, s3);

        }
    }
}

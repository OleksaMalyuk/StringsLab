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



    class Program
    {
        static void Main(string[] args)
        {
            StringBase s1 = new StringBase("30");
            StringBase s2 = new StringBase("20");

        }
    }
}

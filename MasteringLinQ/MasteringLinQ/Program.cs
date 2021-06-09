using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteringLinQ
{
    public class Params : IEnumerable<int>
    {
        private int a, b, c;

        public Params(int a, int b, int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public IEnumerator<int> GetEnumerator()
        {
            yield return a;
            yield return b;
            yield return c;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class Person
    {
        private string firstName, middleName, lastName;

        public Person(string firstName, string middleName, string lastName)
        {
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
        }

        public IEnumerable<string> Names
        {
            get
            {
                yield return firstName;
                yield return middleName;
                yield return lastName;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            Merge(new[] { 1, 2, 3, 4, 5 }, new[] { 1, 3, 5, 7, 9 });

            LengthOfPositive(new[] { -3, -1, 3, 7, 1, -3, 7 });

            IEnumerable<int> result = MyFilter(Enumerable.Range(1,10));


            var r = Poly(2, new[] { 3, 4, 5 });

            var p = new Params(1, 2, 3);

            foreach (var x in p)
            {
                Console.WriteLine(x);
            }

            var person = new Person("Vladimir", "Ilyitch", "Lenin");

            foreach (var name in person.Names)
            {
                Console.WriteLine(name);
            }


            // Display powers of 2 up to the exponent of 8:
            foreach (int i in Power(2, 10))
            {
                Console.Write("{0} ", i);
            }

        }

        public static System.Collections.Generic.IEnumerable<int> Power(int number, int exponent)
        {
            int result = 1;

            for (int i = 0; i < exponent; i++)
            {
                result = result * number;
                yield return result;
            }
        }

        public static IEnumerable<int> MyFilter(IEnumerable<int> input)
        {
            // todo
            var pares = input.Where(x => x % 2 == 0);
            var squaresmenor50 = pares.Where(x => x * x < 50);
            return squaresmenor50.Select(x=>x*x);
        }

        public static IEnumerable<int> Merge(IEnumerable<int> a, IEnumerable<int> b)
        {
            // todo 
            var c = a.Except(b).Union(b.Except(a));
            return c;

        }
        public static int LengthOfPositive(IEnumerable<int> input)
        {
            // todo

            var c = input.SkipWhile(x => x < 0).TakeWhile(y => y > 0);

            return c.Count();
        }
        public static int Poly(int x, IEnumerable<int> coeffs)
        {
            // todo

            //IEnumerable<int> coefficients = coeffs.Reverse(); //we reverse the coefficients' order for simplicity, in real cases this might not be possible, since IEnumerable does not guarantee it is "replayable"

            var rank = 2;
            return coeffs.Aggregate(0,(sofar, current) => {
                var result = sofar +  current * (int)Math.Pow(x, rank);
                rank--;
                return result;
                });

        }
    }
}
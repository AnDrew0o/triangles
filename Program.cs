using System;
using System.Numerics;

namespace triangles
{
    class Program
    {
        static void Main(string[] args)
        {
            Triangle triangle1 = new Triangle(4.0, 30f, 60f);
            triangle1.GetParams();

            Triangle triangle2 = new Triangle(4.0, 4.0, 60f);
            triangle2.GetParams();

            Triangle triangle3 = new Triangle(10.0, 6.0, 5.0);
            triangle3.GetParams();
            triangle3.Altitude(triangle3.A);
        }
    }

    class Triangle
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }

        float alpha, beta, gamma;

        //Конструктори для створення трикутників за 3 сторонами, за стороною і 2-ма кутами, за кутом і двома сторонами. Решта кутів і сторін дораховуються
        public Triangle(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;

            if (IsExist())
            {
                alpha = (float)Math.Round(ToDegree(Math.Acos((b * b + c * c - a * a) / (2 * b * c))));
                beta = (float)Math.Round(ToDegree(Math.Acos((a * a + c * c - b * b) / (2 * a * c))));
                gamma = 180 - alpha - beta;
            }
            else
            {
                Console.WriteLine("Такий трикутник не iснує");
            }
        }
        public Triangle(double a, double b, float gamma)
        {
            A = a;
            B = b;
            this.gamma = gamma;
            C = Math.Round(Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2) - 2 * a * b * Math.Cos(ToRad(gamma))), 5);
            alpha = (float)Math.Round(ToDegree(Math.Asin(a * Math.Sin(ToRad(gamma)) / C)));
            beta = (float)Math.Round(ToDegree(Math.Asin(b * Math.Sin(ToRad(gamma)) / C)));
        }
        public Triangle(double a, float beta, float gamma)
        {
            A = a;
            this.beta = beta;
            this.gamma = gamma;
            alpha = 180 - beta - gamma;
            B = Math.Round(a * Math.Sin(ToRad(beta)) / Math.Sin(ToRad(alpha)), 5);
            C = Math.Round(a * Math.Sin(ToRad(gamma)) / Math.Sin(ToRad(alpha)), 5);
        }
        public double Square() //площа
        {
            double p = HalfPerimeter();
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }

        public double Perimeter() //периметер
        {
            return (A + B + C);
        }

        public double HalfPerimeter() //півпериметер
        {
            return Perimeter() / 2;
        }
        public double OutsideR() //радіус описаного кола
        {
            return (A * B * C) / (4 * Square());

        }
        public double InsideR() //радіус вписаного кола
        {
            return Square() / HalfPerimeter();
        }

        public double Altitude(double a) //висота
        {
            return 2 / a * Square();
        }

        public double Median(double a, double b, double c) //медіана
        {
            return 0.5 * Math.Sqrt(2 * a * a + 2 * b * b - c * c);
        }

        public double Bisector(double a, double b, double c) //бісектриса
        {
            double p = HalfPerimeter();

            return 2 * Math.Sqrt((a * b * p * (p - c))/(a + b));
        }

        public void GetParams()
        {
            Console.WriteLine($"Сторони: {A} {B} {C}\nКути: {alpha} {beta} {gamma}");
        }

        public bool IsExist() //перевірка, чи існує трикутник
        {
            return (A + B > C) && (A + C > B) && (B + C > A);
        }

        public bool IsRightTriangle() //перевірка, чи прямокутний трикутник
        {
            return A == 90 || B == 90 || C == 90;
        }

        public bool IsIsosceles() //перевірка, чи рівнобедрений трикутник
        {
            return A == B || B == C || C == A;
        }

        private static double ToRad(double angle) //перетворення з градусів в радіани
        {
            return (angle * Math.PI / 180);
        }

        private static double ToDegree(double angle) //перетворення з радіанів в градуси
        {
            return (angle * 180 / Math.PI);
        }
    }
}

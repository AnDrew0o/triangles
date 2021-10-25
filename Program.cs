using System;

namespace triangles
{
    class Program
    {
        static void Main(string[] args)
        {
            Triangle triangle1 = new Triangle(4.0, 30f, 60f);
            triangle1.GetParams();
            WriteAll(triangle1);

            Triangle triangle2 = new Triangle(4.0, 4.0, 60f);
            triangle2.GetParams();
            WriteAll(triangle2);

            Triangle triangle3 = new Triangle(10.0, 6.0, 5.0);
            triangle3.GetParams();
            WriteAll(triangle3);
        }

        static void WriteAll(Triangle triangle)
        {
            Console.WriteLine();
            Console.WriteLine($"Площа: {triangle.Square()}");
            Console.WriteLine($"Периметер: {triangle.Perimeter()}");
            Console.WriteLine($"Зовнiшнiй радiус: {triangle.OutsideR()}");
            Console.WriteLine($"Внутрiшнiй радiус: {triangle.InsideR()}");
            Console.WriteLine($"Рiвностороннiй: {triangle.IsEquilateral()}");
            Console.WriteLine($"Рiвнобедренний: {triangle.IsIsosceles()}");
            Console.WriteLine($"Прямокутний: {triangle.IsRightTriangle()}");

            Console.WriteLine($"Висота до A: {triangle.Altitude(triangle.A)}");
            Console.WriteLine($"Висота до B: {triangle.Altitude(triangle.B)}");
            Console.WriteLine($"Висота до C: {triangle.Altitude(triangle.C)}");

            Console.WriteLine($"Медiана до A: {triangle.Median(triangle.B, triangle.C, triangle.A)}");
            // Console.WriteLine($"Медiана до B: {triangle.Median(triangle.A, triangle.C, triangle.B)}");
            Console.WriteLine($"Медiана до B: {triangle.Median(triangle.C, triangle.A, triangle.B)}");
            Console.WriteLine($"Медiана до C: {triangle.Median(triangle.A, triangle.B, triangle.C)}");

            Console.WriteLine($"Бiсектриса до А: {triangle.Bisector(triangle.B, triangle.C, triangle.A)}");
            Console.WriteLine($"Бiсектриса до B: {triangle.Bisector(triangle.C, triangle.A, triangle.B)}");
            Console.WriteLine($"Бiсектриса до C: {triangle.Bisector(triangle.A, triangle.B, triangle.C)}");



            Console.WriteLine();
        }
    }

    class Triangle
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }

        float alpha, beta, gamma;

        //Конструктори для створення трикутникiв за 3 сторонами, за стороною i 2-ма кутами, за кутом i двома сторонами. Решта кутiв i сторiн дораховуються
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

        private double HalfPerimeter() //пiвпериметер
        {
            return Perimeter() / 2;
        }
        public double OutsideR() //радiус описаного кола
        {
            return (A * B * C) / (4 * Square());

        }
        public double InsideR() //радiус вписаного кола
        {
            return Square() / HalfPerimeter();
        }

        public double Altitude(double a) //висота
        {
            return 2 / a * Square();
        }

        public double Median(double a, double b, double c) //медiана
        {
            return 0.5 * Math.Sqrt(2 * a * a + 2 * b * b - c * c);
        }

        public double Bisector(double a, double b, double c) //бiсектриса
        {
            double p = HalfPerimeter();

            return 2 * Math.Sqrt((a * b * p * (p - c))) / (a + b);
        }

        public void GetParams()
        {
            Console.WriteLine($"Сторони: {A} {B} {C}\nКути: {alpha} {beta} {gamma}");
        }

        public bool IsExist() //перевiрка, чи iснує трикутник
        {
            return (A + B > C) && (A + C > B) && (B + C > A);
        }

        public bool IsRightTriangle() //перевiрка, чи прямокутний трикутник
        {
            return A == 90 || B == 90 || C == 90;
        }

        public bool IsIsosceles() //перевiрка, чи рiвнобедрений трикутник
        {
            return A == B || B == C || C == A;
        }

        public bool IsEquilateral() //перевiрка, чи рiвностороннiй трикутник
        {
            return A == B && B == C && C == A;
        }

        private static double ToRad(double angle) //перетворення з градусiв в радiани
        {
            return (angle * Math.PI / 180);
        }

        private static double ToDegree(double angle) //перетворення з радiанiв в градуси
        {
            return (angle * 180 / Math.PI);
        }
    }
}

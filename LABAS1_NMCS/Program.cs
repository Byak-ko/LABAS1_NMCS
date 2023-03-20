namespace LABAS1_NMCS
{
    internal class Program
    {
        static List<double> roots = new List<double>();
        static double F(double x)
        {
            return 1.5 - Math.Pow(x, 1 - Math.Cos(x));
            //  return Math.Sin(2*x) + Math.Cos(x);
        }
        static double DF(double x)
        {
            return (F(x + 0.05) - F(x))/ 0.05;
        }
        static void Main(string[] args)
        {
            Separate(10, 0, 5 * Math.PI);

            foreach (var root in roots)
            {
                Console.WriteLine(root);
            }

            Console.WriteLine(roots.Count);
        }

        static void Separate(int iterations, double leftBorder, double rightBorder)
        {
            double step = (rightBorder - leftBorder) / iterations;

            for (int i = 0; i < iterations; i++)
            {
                double leftSectionX = leftBorder + i * step;
                double rightSectionX = leftBorder + (i + 1) * step;

                double LeftSectionY = F(leftSectionX);
                double RightSectionY = F(rightSectionX);

                if (IsChangedSign(10, leftSectionX, rightSectionX)) // чи змінився знак похідної
                {

                    if (Math.Abs(rightSectionX - leftSectionX) > 0.1) // умова закінчення рекурсії
                    {
                        Separate(10, leftSectionX, rightSectionX);
                    }
                }
                else // якщо знак не змінився знак у похідної
                {
                    if (Math.Sign(LeftSectionY) != Math.Sign(RightSectionY)) // змінився один зі знаків
                    {
                        NewtonsMethod(rightSectionX, 0.01);
                    }
                }
            }
        }

        static bool IsChangedSign(int iterations, double leftBoundX, double rightBoundX)
        {
            bool IsChangeSign = false;
            double sign = Math.Sign(DF(leftBoundX));

            double step = (rightBoundX - leftBoundX) / iterations;

            for (int i = 0; i < iterations; i++)
            {
                double rightFuncDeriv = DF(leftBoundX + (i + 1) * step);

                if (sign == 0) sign = Math.Sign(rightFuncDeriv);

                else
                {
                    if (Math.Sign(rightFuncDeriv) != sign)
                    {
                        IsChangeSign = true;
                        break;
                    }
                }
            }
            return IsChangeSign;
        }

        static void NewtonsMethod(double rightBoundX, double epsilon)
        {
            double x0 = rightBoundX;
            double x1 = x0 - (F(x0) / DF(x0));
            double y = Math.Abs(F(x1));
            x0 = x1;
            while (y > epsilon)
            {
                x1 = x0 - (F(x0) / DF(x0));
                x0 = x1;
                y = Math.Abs(F(x1));
            }
            roots.Add(x1);
        }
    }
}
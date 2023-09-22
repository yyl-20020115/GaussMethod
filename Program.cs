namespace GaussMethod;

public class Program
{
    public static void Main(string[] _)
    {
        /* double[,] xArray = new double[,]
         {

                 { 2.000000 ,-1.000000 , 3.000000,  1.000000},
                 { 4.000000 , 2.000000 , 5.000000,  4.000000},
                 { 1.000000 , 2.000000 , 0.000000 , 7.000000}
         };*/

        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        double[] y = new double[] { 29152.3, 47025.3, 86852.3, 132450.6, 200302.3, 284688.1, 396988.3 };
        double[] x = new double[] { 1.24, 2.37, 5.12, 8.12, 12.19, 17.97, 24.99 };

        // double[,] xArray;

        sw.Start();
        var ratio = FittingFunctions.Linear(y, x);
        sw.Stop();

        foreach (double num in ratio)
        {
            Console.WriteLine(num);
        }
        Console.WriteLine($"一次拟合计算时间：{sw.ElapsedMilliseconds} ms");

        sw.Start();
        ratio = FittingFunctions.LOGEST(y, x);
        sw.Stop();

        foreach (double num in ratio)
        {
            Console.WriteLine(num);
        }
        Console.WriteLine($"对数拟合计算时间：{sw.ElapsedMilliseconds} ms");

        sw.Start();
        ratio = FittingFunctions.PowEST(y, x);
        sw.Stop();

        foreach (double num in ratio)
        {
            Console.WriteLine(num);
        }
        Console.WriteLine($"指数拟合计算时间：{sw.ElapsedMilliseconds} ms");

        sw.Start();
        ratio = FittingFunctions.IndexEST(y, x);
        sw.Stop();
        foreach (double num in ratio)
        {
            Console.WriteLine(num);
        }
        Console.WriteLine($"幂级数拟合计算时间：{sw.ElapsedMilliseconds} ms");
        Console.ReadKey();

    }
}

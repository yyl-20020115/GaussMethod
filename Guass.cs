namespace GaussMethod;
public class Guass
{
    public static void Cal_Guass(double[,] guass, int count)
    {
        double t;
        double[] x_value;

        for (int j = 0; j < count; j++)
        {
            int k = j;
            double min = guass[j, j];

            for (int i = j; i < count; i++)
            {
                if (Math.Abs(guass[i, j]) < min)
                {
                    min = guass[i, j];
                    k = i;
                }
            }

            if (k != j)
            {
                for (int x = j; x <= count; x++)
                {
                    t = guass[k, x];
                    guass[k, x] = guass[j, x];
                    guass[j, x] = t;
                }
            }

            for (int m = j + 1; m < count; m++)
            {
                double div = guass[m, j] / guass[j, j];
                for (int n = j; n <= count; n++)
                {
                    guass[m, n] = guass[m, n] - guass[j, n] * div;
                }
            }

            System.Console.WriteLine("初等行变换：");
            for (int i = 0; i < count; i++)
            {
                for (int m = 0; m < count + 1; m++)
                {
                    System.Console.Write("{0,10:F6}", guass[i, m]);
                }
                Console.WriteLine();
            }
        }
        x_value = Get_Value(guass, count);

        if (x_value == null)
            Console.WriteLine("方程组无解或多解！");
        else
        {
            foreach (double x in x_value)
            {
                Console.WriteLine("{0:F6}", x);
            }
        }
    }
    #region 回带计算X值
    public static double[] Get_Value(double[,] guass, int count)
    {
        double[] x = new double[count];
        double[,] X_Array = new double[count, count];
        int rank = guass.Rank;//秩是从0开始的

        for (int i = 0; i < count; i++)
            for (int j = 0; j < count; j++)
                X_Array[i, j] = guass[i, j];

        if (X_Array.Rank < guass.Rank)//表示无解
        {
            return null;
        }

        if (X_Array.Rank < count - 1)//表示有多解
        {
            return null;
        }
        //回带计算x值
        x[count - 1] = guass[count - 1, count] / guass[count - 1, count - 1];
        for (int i = count - 2; i >= 0; i--)
        {
            double temp = 0;
            for (int j = i; j < count; j++)
            {
                temp += x[j] * guass[i, j];
            }
            x[i] = (guass[i, count] - temp) / guass[i, i];
        }

        return x;
    }
}
#endregion

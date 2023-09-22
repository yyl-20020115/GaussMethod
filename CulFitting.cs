namespace GaussMethod;

public class CulFitting
{
    #region  得到数据的法矩阵,输出为发矩阵的增广矩阵
    public static double[,] BuildArray(double[] y, double[] x, int n)
    {
        var result = new double[n + 1, n + 2];

        if (y.Length != x.Length)
        {
            throw (new Exception("Different input length"));
            //return null;
        }

        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= n; j++)
            {
                result[i, j] = SumInput(x, i + j);
            }
            result[i, n + 1] = SumInputPow(y, x, i);
        }

        return result;
    }

    #endregion

    #region 累加的计算
    public static double SumInput(double[] input, int order)
    {
        double result = 0;
        int length = input.Length;

        for (int i = 0; i < length; i++)
        {
            result += Math.Pow(input[i], order);
        }

        return result;
    }
    #endregion

    #region 计算∑(x^j)*y
    public static double SumInputPow(double[] y, double[] x, int order)
    {
        double result = 0;

        int length = x.Length;

        for (int i = 0; i < length; i++)
        {
            result += Math.Pow(x[i], order) * y[i];
        }

        return result;
    }
    #endregion
}

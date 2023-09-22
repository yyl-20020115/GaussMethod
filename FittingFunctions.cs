using System;

namespace GaussMethod;

public class FittingFunctions
{
    #region 多项式拟合函数,输出系数是y=a0+a1*x+a2*x*x+.........，按a0,a1,a2输出
    public static double[] Polyfit(double[] y, double[] x, int order)
    {
        var guass = Get_Array(y, x, order);

        var ratio = Cal_Guass(guass, order + 1);

        return ratio;
    }
    #endregion

    #region 一次拟合函数，y=a0+a1*x,输出次序是a0,a1
    public static double[] Linear(double[] y, double[] x)
    {
        var ratio = Polyfit(y, x, 1);
        return ratio;
    }
    #endregion

    #region 对数拟合函数,.y= c*(ln x)+b,输出为b,c
    public static double[] LOGEST(double[] y, double[] x)
    {
        var lnX = new double[x.Length];

        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] == 0 || x[i] < 0)
            {
                throw (new Exception("Exp for negative"));
                return null;
            }
            lnX[i] = Math.Log(x[i]);
        }

        return Linear(y, lnX);
    }
    #endregion

    #region 幂函数拟合模型, y=c*x^b,输出为c,b
    public static double[] PowEST(double[] y, double[] x)
    {
        var lnX = new double[x.Length];
        var lnY = new double[y.Length];

        for (int i = 0; i < x.Length; i++)
        {
            lnX[i] = Math.Log(x[i]);
            lnY[i] = Math.Log(y[i]);
        }

        var dlinestRet = Linear(lnY, lnX);

        dlinestRet[0] = Math.Exp(dlinestRet[0]);

        return dlinestRet;
    }
    #endregion

    #region 指数函数拟合函数模型，公式为 y=c*m^x;输出为 c,m
    public static double[] IndexEST(double[] y, double[] x)
    {
        var lnY = new double[y.Length];
        for (int i = 0; i < y.Length; i++)
        {
            lnY[i] = Math.Log(y[i]);
        }

        var ratio = Linear(lnY, x);

        for (int i = 0; i < ratio.Length; i++)
        {
            ratio[i] = Math.Exp(ratio[i]);
        }
        return ratio;
    }
    #endregion

    #region 最小二乘法部分

    #region 计算增广矩阵
    static private double[] Cal_Guass(double[,] guass, int count)
    {
        double temp;
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
                    temp = guass[k, x];
                    guass[k, x] = guass[j, x];
                    guass[j, x] = temp;
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

            /* System.Console.WriteLine("初等行变换：");
             for (int i = 0; i < count; i++)
             {
                 for (int m = 0; m < count + 1; m++)
                 {
                     System.Console.Write("{0,10:F6}", guass[i, m]);
                 }
                 Console.WriteLine();
             }*/
        }
        x_value = Get_Value(guass, count);

        return x_value;

        /*if (x_value == null)
            Console.WriteLine("方程组无解或多解！");
        else
        {
            foreach (double x in x_value)
            {
                Console.WriteLine("{0:F6}", x);
            }
        }*/
    }

    #endregion

    #region 回带计算X值
    static private double[] Get_Value(double[,] guass, int count)
    {
        var x = new double[count];
        var X_Array = new double[count, count];
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
    #endregion

    #region  得到数据的法矩阵,输出为发矩阵的增广矩阵
    static private double[,] Get_Array(double[] y, double[] x, int n)
    {
        var result = new double[n + 1, n + 2];

        if (y.Length != x.Length)
        {
            throw (new Exception("两个输入数组长度不一！"));
            //return null;
        }

        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= n; j++)
            {
                result[i, j] = Cal_sum(x, i + j);
            }
            result[i, n + 1] = Cal_multi(y, x, i);
        }

        return result;
    }

    #endregion

    #region 累加的计算
    static private double Cal_sum(double[] input, int order)
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
    static private double Cal_multi(double[] y, double[] x, int order)
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

    #endregion
}

/*
// MAX VALUE
int MaxValue (int x, int y)
{
    if (x > y)
        return x;
    else
        return y;
}

Console.WriteLine("Max value is: " + MaxValue(3, 5));
*/

/*
//MIN VALUE
int MinValue(int x, int y)
{
    if (x > y)
        return y;
    else
        return x;
}

Console.WriteLine("Max value is: " + MinValue(3, 5));
*/

/*
//TrySumIfOdd
bool TrySumIfOdd (int x, int y, out int sum)
{
    sum = x + y;
    return sum % 2 != 0;

}

int x = 4;
int y = 5;
int sumOfValues = 0;
Console.WriteLine("Is sum odd? " + TrySumIfOdd(x, y, out sumOfValues) + " sum is: " + sumOfValues);
*/

/*
// MAX VALUE WITH 3 and 4 PARAMETERS
int MaxValue3(int x, int y, int z)
{
    if (x > y && x > z)
        return x;
    else if (y > x && y > z)
        return y;
    else
        return z;
}

Console.WriteLine("Max value is: " + MaxValue3(3, 5, 2));

int MaxValue4(int x, int y, int z, int a)
{
    int firstPairMax = x > y ? x : y;
    int secondPairMax = z > a ? z : a;
    return firstPairMax > secondPairMax ? firstPairMax : secondPairMax;
}

Console.WriteLine("Max value is: " + MaxValue4(3, 5, 2, 7));
*/

/*
// MIN VALUE WITH 3 and 4 PARAMETERS
int MinValue3(int x, int y, int z)
{
    if (x < y && x < z)
        return x;
    else if (y < x && y < z)
        return y;
    else
        return z;
}

Console.WriteLine("Max value is: " + MinValue3(3, 5, 2));

int MinValue4(int x, int y, int z, int a)
{
    int firstPairMax = x < y ? x : y;
    int secondPairMax = z < a ? z : a;
    return firstPairMax < secondPairMax ? firstPairMax : secondPairMax;
}

Console.WriteLine("Max value is: " + MinValue4(3, 5, 2, 7));
*/

/*
//METHOD REPEAT
string Repeat (string str, int n)
{
    string longStr = "";
    for (int i = 0; i < n; i++)
        longStr += str;
    return longStr;
}

Console.WriteLine(Repeat("str", 4));
*/

//FIBONACCI
int FibonacciSum(int n)
{
    if (n <= 1) return 0;
    if (n == 2) return 1;
    else
        return FibonacciSum(n - 1) + FibonacciSum(n - 2) + 1;

}
int x = 5;
Console.WriteLine("Sum of " + x + " numbers of Fibonacci line is " + FibonacciSum(x));
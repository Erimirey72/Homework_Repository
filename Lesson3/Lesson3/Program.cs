/*
int x = 10;
int y = 5;
*/
int x = 0;
int y = 0;
int sum = 0;

Console.Write("First number is: ");
bool firstNumber = int.TryParse(Console.ReadLine(), out x);
Console.Write("Second number is: ");
bool secondNumber = int.TryParse(Console.ReadLine(), out y);

if (!firstNumber || !secondNumber)
    Console.WriteLine("Input is in incorrect format!");
else
{
    if (x < y)
    {
        for (int i = x; i <= y; i++) sum += i;
    }
    else if (x >= y)
    {
        for (int i = y; i <= x; i++) sum += i;
    }
    Console.WriteLine("Sum of numbers between: " + sum);
}
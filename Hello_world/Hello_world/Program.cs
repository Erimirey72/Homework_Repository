/*
int a = 10;
short b = 11;
long c = 12;
double d = 13.1;

Console.WriteLine(a + a);
Console.WriteLine(b - a);
Console.WriteLine(c * b);
Console.WriteLine(d % a); 
*/

double x = 10;
double y = 15;

Console.WriteLine("First math example: " + (-6 * Math.Pow(x, 3) + 5 * Math.Pow(x, 2) - 10 * x + 15));

Console.WriteLine("Second math example: " + (Math.Abs(x) * Math.Sin(x)));

Console.WriteLine("Third math example: " + (2 * Math.PI * x));

Console.WriteLine("Fourth math example: " + Math.Max(x, y));

DateTime currentDate = DateTime.Now;
DateTime nextNewYear = new DateTime(currentDate.Year + 1, 1, 1);
int daysToNewYearLeft = (nextNewYear - currentDate).Days;
Console.WriteLine(daysToNewYearLeft + " days left to New Year");
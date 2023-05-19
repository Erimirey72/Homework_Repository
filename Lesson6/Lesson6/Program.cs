using System.Text;

static bool Compare(string str1, string str2)
{
        return str1 == str2;
}

static int Analyze(string str)
{
    int stringLenght = str.Length;
    int numberOfChars = 0;
    int i = 0;
    while (i < stringLenght)
    {
        if ((str[i] >= 'a' && str[i] <= 'z') || (str[i] >= 'A' && str[i] <= 'Z'))
        {
            numberOfChars++;
        }
        i++;
    }
    return numberOfChars;
}

static char[] Sort(string str)
{
    char[] charArray = str.ToLower().ToCharArray();
    char c;
    for (int i = 1; i < charArray.Length; i++)
    {
        for (int j = 0; j < charArray.Length - 1; j++)
        {
            if (charArray[j] > charArray[j + 1])
            {
                c = charArray[j];
                charArray[j] = charArray[j + 1];
                charArray[j + 1] = c;
            }
        }
    }
    return charArray;
}
/*
static string Duplicate(string str)
{
    string newSrting = str.ToLower();
    string newStr = "";
    for (int i = 0; i < newSrting.Length; i++)
    {
        for (int j = i + 1; j < newSrting.Length - 1; j++)
        {
            if (newSrting[i] == newSrting[j])
            {
                newStr += newSrting[j];
            }
        }
    }
    return newStr;
}
*/

static char[] Duplicate(string str)
{
    string lowerString = str.ToLower();
    StringBuilder duplicates = new StringBuilder();

    for (int i = 0; i < lowerString.Length; i++)
    {
        char currentChar = lowerString[i];
        if (currentChar == '\0')
            continue;
        for (int j = i + 1; j < lowerString.Length; j++)
        {
            if (currentChar == lowerString[j])
            {
                duplicates.Append(currentChar);
                lowerString = lowerString.Remove(j, 1).Insert(j, "\0");
                lowerString = lowerString.Remove(i, 1).Insert(i, "\0");

                break;
            }
        }
    }
    char[] result = new char[duplicates.Length];
    duplicates.CopyTo(0, result, 0, duplicates.Length);

    return result;
}

Console.WriteLine(Compare("a", "aa"));
Console.WriteLine(Analyze("aa aa AA"));
Console.WriteLine(Sort("Hello"));
Console.WriteLine(Duplicate("Hello and hi"));
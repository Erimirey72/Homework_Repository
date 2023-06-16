public enum SortAlgorithmType
{
    Selection,
    Bubble,
    Insertion
}
public enum OrderBy
{
    Asc,
    Desc
}
class Program
{
    static void Main(string[] args)
    { 
        static void SelectionSort(int[] arr, bool asc)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int sortedValue = i;
                if (asc)
                {
                    for (int j = i + 1; j < arr.Length; j++)
                        if (arr[j] < arr[sortedValue])
                            sortedValue = j;
                }
                else
                {
                    for (int j = i + 1; j < arr.Length; j++)
                        if (arr[j] > arr[sortedValue])
                            sortedValue = j;
                }
                int x = arr[sortedValue];
                arr[sortedValue] = arr[i];
                arr[i] = x;
            }
        }

        static void BubbleSort(int[] arr, bool asc)
        {
            int temp = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (asc)
                {
                    for (int j = 0; j < arr.Length - 1; j++)
                    {
                        if (arr[j] > arr[j + 1])
                        {
                            temp = arr[j + 1];
                            arr[j + 1] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < arr.Length - 1; j++)
                    {
                        if (arr[j] < arr[j + 1])
                        {
                            temp = arr[j + 1];
                            arr[j + 1] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
            }
        }

        static void InsertionSort(int[] arr, bool asc)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (asc)
                {
                    for (int j = i + 1; j > 0; j--)
                    {
                        if (arr[j - 1] > arr[j])
                        {
                            int temp = arr[j - 1];
                            arr[j - 1] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
                else
                {
                    for (int j = i + 1; j > 0; j--)
                    {
                        if (arr[j - 1] < arr[j])
                        {
                            int temp = arr[j - 1];
                            arr[j - 1] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
            }
        }

        static void PrintArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; ++i)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }

        static void Sort(int[] arr, SortAlgorithmType type, OrderBy order)
        {
            switch (order)
            {
                case OrderBy.Asc:
                    switch (type)
                    {
                        case SortAlgorithmType.Selection:
                            SelectionSort(arr, true);
                            Console.WriteLine("Sorted array with selection:");
                            PrintArray(arr);
                            break;
                        case SortAlgorithmType.Bubble:
                            BubbleSort(arr, true);
                            Console.WriteLine("Sorted array with bubble:");
                            PrintArray(arr);
                            break;
                        case SortAlgorithmType.Insertion:
                            InsertionSort(arr, true);
                            Console.WriteLine("Sorted array with insertion:");
                            PrintArray(arr);
                            break;
                    }
                    break;
                case OrderBy.Desc:
                    switch (type)
                    {
                        case SortAlgorithmType.Selection:
                            SelectionSort(arr, false);
                            Console.WriteLine("Sorted array with selection:");
                            PrintArray(arr);
                            break;
                        case SortAlgorithmType.Bubble:
                            BubbleSort(arr, false);
                            Console.WriteLine("Sorted array with bubble:");
                            PrintArray(arr);
                            break;
                        case SortAlgorithmType.Insertion:
                            InsertionSort(arr, false);
                            Console.WriteLine("Sorted array with insertion:");
                            PrintArray(arr);
                            break;
                    }
                    break;
            }
        }



        int[] arr = { 800, 11, 50, 771, 649, 770, 240, 9 };
        SortAlgorithmType selection = SortAlgorithmType.Selection;
        SortAlgorithmType bubble = SortAlgorithmType.Bubble;
        SortAlgorithmType insertion = SortAlgorithmType.Insertion;
        OrderBy order = OrderBy.Desc;
        Sort(arr, selection, order);
        Sort(arr, bubble, order);
        Sort(arr, insertion, order);
    }
}

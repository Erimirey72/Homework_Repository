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
        static void selectionSort(int[] arr, bool asc)
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

        static void bubbleSort(int[] arr, bool asc)
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

        static void insertionSort(int[] arr, bool asc)
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

        static void printArray(int[] arr)
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
                            selectionSort(arr, true);
                            Console.WriteLine("Sorted array with selection:");
                            printArray(arr);
                            break;
                        case SortAlgorithmType.Bubble:
                            bubbleSort(arr, true);
                            Console.WriteLine("Sorted array with bubble:");
                            printArray(arr);
                            break;
                        case SortAlgorithmType.Insertion:
                            insertionSort(arr, true);
                            Console.WriteLine("Sorted array with insertion:");
                            printArray(arr);
                            break;
                    }
                    break;
                case OrderBy.Desc:
                    switch (type)
                    {
                        case SortAlgorithmType.Selection:
                            selectionSort(arr, false);
                            Console.WriteLine("Sorted array with selection:");
                            printArray(arr);
                            break;
                        case SortAlgorithmType.Bubble:
                            bubbleSort(arr, false);
                            Console.WriteLine("Sorted array with bubble:");
                            printArray(arr);
                            break;
                        case SortAlgorithmType.Insertion:
                            insertionSort(arr, false);
                            Console.WriteLine("Sorted array with insertion:");
                            printArray(arr);
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
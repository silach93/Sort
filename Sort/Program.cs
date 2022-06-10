using System.Diagnostics;

namespace Sort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int threadCount = 5;// хотел чисто по фану 11, потому что счастливое число, я тогда родився, но меня послали в жопу
            Testing(GenerateRandom(500000), threadCount);
        }

        static bool IspbltanieNaGaySex(int n)//проверяет простое число или нет
        {
            if (n == 1 || n == 0)
                return false;
            for (int i = 2; i <= n / 2; i++)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }
        static int SystemTargetGay(int[] arr)//Первое попавшееся ПРОСТОЕ число в массиве
        {
            foreach (int i in arr)
            {
                if (IspbltanieNaGaySex(i))
                    return i;
            }
            return 0;
        }
        static int FindPrimeInList(List<int> arr)//Та же фигня, что сверху, только для списка
        {
            foreach (int i in arr)
            {
                if (IspbltanieNaGaySex(i))
                    return i;
            }
            return 0;
        }

        static List<int[]> MnogochlenNaOdnochlen(int[] arr, int threadCount)//Эта хрень делит массив на части
        {
            List<int[]> list = new();//C# 9.0 лучший
            int LengthMicroPenises = arr.Length / threadCount;
            int LastMicroPenis = LengthMicroPenises + arr.Length % threadCount;
            for (int i = 0; i < threadCount - 1; i++)
            {
                list.Add(new int[LengthMicroPenises]);
                Array.Copy(arr, LengthMicroPenises * i, list[i], 0, LengthMicroPenises);
            }

            list.Add(new int[LastMicroPenis]);
            Array.Copy(arr, LengthMicroPenises * (threadCount - 1), list[threadCount - 1], 0, LastMicroPenis);
            return list;
        }

        static string SimpleSearchGay(int[] arr)//Поиск простово(однопоточный)
        {
            int result = 0;
            result = SystemTargetGay(arr);
            return (result == 0) ? "No primes" : result.ToString();
        }

        static string ParallelSearchGay(int[] arr)//тоже говно, что сверху, только параллельно
        {
            int result = 0;
            Parallel.ForEach(arr, (i, state) =>
            {
                if (IspbltanieNaGaySex(i))
                {
                    result = i;
                    state.Stop();
                }
            });
            return (result == 0) ? "No primes" : result.ToString();
        }

        static string MultiSearchGay(int[] arr, int threadCount)//Поиск простого, но во много потоков
        {
            int result = 0;
            List<int> results = new(threadCount);
            List<Thread> threads = new(threadCount);
            List<int[]> arrayList = MnogochlenNaOdnochlen(arr, threadCount);

            foreach (int[] array in arrayList)//Создаёт поток для каждого массива, как в Баскетболе Куроко
            {
                threads.Add(new Thread(() =>
                {
                    results.Add(SystemTargetGay(array));
                }));
            }

            foreach (Thread thread in threads)//Запуск всех потоков(как Куроко,только круче)
            {
                thread.Start();
                thread.Join();//эта хрень заставляет ждать конца потока
            }

            result = FindPrimeInList(results);

            return (result == 0) ? "No primes" : result.ToString();
        }

        static void Testing(int[] arr, int threadCount)//Список пидорасов ,как-либо насоливших мне
        {
            Console.WriteLine("Погнали");
            Stopwatch timer = new Stopwatch();//честно списано с документации шарпов и метанита
            string result;

            Console.WriteLine("Обычная сортировка");
            timer.Restart();
            result = SimpleSearchGay(arr);
            timer.Stop();
            Console.WriteLine($"Гей вычислен обычным методом: {result}, за {timer.Elapsed}");

            Console.WriteLine("Параллельная сортировка");
            timer.Restart();
            result = ParallelSearchGay(arr);
            timer.Stop();
            Console.WriteLine($"Гей вычеслен в росгвардии: {result}, за {timer.Elapsed}");

            Console.WriteLine("Многопоточная сортировка");
            timer.Restart();
            result = MultiSearchGay(arr, threadCount);
            timer.Stop();
            Console.WriteLine($"Гей вычеслен в ВКИ: {result}, за {timer.Elapsed}");

            Console.WriteLine("Конец");
            Console.WriteLine();
        }

        static int[] GenerateRandom(int len)
        {
            var rand = new Random();
            int[] arr = new int[len];
            for (int i = 0; i < len; i++)
                    arr[i] = rand.Next(0, 2000);
            return arr;
        }

    }
}

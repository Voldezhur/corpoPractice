using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

namespace HelloWorld
{
    class Program
    {        
        // Переделать 5, 7, 9, 11 через LINQ

        static void number1(string[] input) {
            // Переводим строчный массив в массив чисел
            int[] nums = Array.ConvertAll(input, int.Parse);

            // Первый положительный элемент
            foreach (int i in nums) {
                if (i > 0) {
                    Console.WriteLine(i);
                    break;
                }
            }

            // Последний отрицательный элемент
            foreach (int i in nums.Reverse()) {
                if (i < 0) {
                    Console.WriteLine(i);
                    break;
                }
            }
        }

        static void number2(string[] input) {
            // Переводим строчный массив в массив чисел
            int[] nums = Array.ConvertAll(input, int.Parse);
            
            int lastNum = nums[0];
            nums = nums.Skip(1).ToArray();

            foreach (int i in nums) {
                if (i % 10 == lastNum) {
                    Console.WriteLine(i);
                    break;
                }
            }
        }
        
        static void number3(string[] nums) {
            string result = "Not found";

            int L = nums[0][0] - '0';  // Первый элемент - искомая длина
            nums = nums.Skip(1).ToArray();  // Пропускаем первый элемент

            foreach (string i in nums) {
                if (Char.IsDigit(i[0]) && i.Length == L) {
                    result = i;
                }
            }

            Console.WriteLine(result);
        }

        static void number4(string[] input) {
            // Переводим строчный массив в массив чисел
            int[] nums = Array.ConvertAll(input, int.Parse);
            
            int D = nums[0];  // Первое число, с которым сравниваем
            
            // Поиск первого подходящего числа
            int counter = 1;
            while (nums[counter] <= D) {
                counter++;
            }

            // Вывод чисел после первого подходящего
            while (counter < nums.Length) {
                Console.WriteLine(nums[counter]);
                counter++;
            }
        }

        static void number5(string[] input) {
            int K = input[0][0] - '0';  // Первый элемент - искомая длина
            input = input.Skip(1).ToArray();  // Пропускаем первый элемент

            List<string> strings = input.ToList();
            strings = strings.GetRange(0, K);  // Оставляем только элементы после K по индексу
            strings.Reverse();  // Переворачиваем список

            // В новый список будем записывать подходящие строки
            List<string> newStrings = new List<string>();
            // Ищем подходящие строки
            foreach (string i in strings) {
                if (i.Length % 2 != 0) {
                    newStrings.Add(i);
                }
            }

            foreach (string i in newStrings) {
                Console.WriteLine(i);
            }
        }

        static void number5LINQ(string[] input) {
            int K = input[0][0] - '0';  // Первый элемент - искомая длина
            List<string> strings = input.Skip(1).ToList();  // Пропускаем первый элемент

            char[] alpha = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',  'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};

            // Из элементов до K берем только подходящие по условию
            IEnumerable<string> selectedStrings = from t in strings.Take(K)
                                where t.Length % 2 == 1 && alpha.Contains(t[0])
                                select t;

            // Вывод
            foreach (string i in selectedStrings) {
                Console.WriteLine(i);
            }
        }

        static void number6(string[] input) {
            // Переводим строчный массив в массив чисел
            int[] nums = Array.ConvertAll(input, int.Parse);
            
            int D = nums[0];
            int K = nums[1];

            nums = nums.Skip(2).ToArray();
            
            // Строим первый фрагмент
            int i = 0;  // Находим первый элемент > D
            while (nums[i] <= D) {
                i++;
            }
            
            int[] first = new int[nums.Length - i];
            
            // Строим массив
            Console.WriteLine("Первый массив:");
            
            for (int j = i; j < nums.Length; j++) {
                first[j - i] = nums[j];
                Console.WriteLine(nums[j]);
            }

            // Строим второй массив
            Console.WriteLine("Второй массив:");

            int[] second = new int[nums.Length - K - 1];
            K++;  // Прибавляем у индексу 1 т.к. не включаем К-тый элемент
            int index = 0;

            while (K < nums.Length) {
                second[index] = nums[K];
                Console.WriteLine(nums[K]);
                
                index++;
                K++;
            }

            // Пересечение
            Console.WriteLine("Пересечение:");

            IEnumerable<int> joined = first.Union(second);

            foreach (int k in joined) {
                Console.WriteLine(k);
            }
        }

        static void number7(string[] input) {
            // Переводим строчный массив в массив чисел
            int[] nums = Array.ConvertAll(input, int.Parse);
            
            int K = nums[0];

            nums = nums.Skip(1).ToArray();  // Пропускаем первый элемент

            // Первый фрагмент
            List<int> first = new List<int>();
            Console.WriteLine("Первый фрагмент:");

            foreach (int i in nums) {
                if (i % 2 == 0) {
                    first.Add(i);
                    Console.WriteLine(i);
                }
            }

            // Второй фрагмент
            List<int> second = new List<int>();
            second = nums.Skip(K + 1).ToList();
            Console.WriteLine("Второй фрагмент:");
            
            foreach (int i in second) {
                Console.WriteLine(i);
            }

            // Разность
            Console.WriteLine("Разность:");

            IEnumerable<int> arr = first.Except(second);

            foreach (int k in arr) {
                Console.WriteLine(k);
            }
        }

        static void number7LINQ(string[] input) {
            // Переводим строчный массив в массив чисел
            List<int> nums = Array.ConvertAll(input, int.Parse).ToList();
            
            int K = nums[0];

            nums = nums.Skip(1).ToList();  // Пропускаем первый элемент

            // Первый фрагмент - четные элементы
            IEnumerable<int> A = from num in nums
                                where num % 2 == 0
                                select num;

            Console.WriteLine("Первый фрагмент");
            foreach (int i in A) {
                Console.Write(i.ToString() + ' ');
            }


            // Второй фрагмент - числа после K-ого индекса
            IEnumerable<int> B = from num in nums.Skip(K)
                                select num;

            Console.WriteLine("\n\nВторой фрагмент");
            foreach( int i in B) {
                Console.Write(i.ToString() + ' ');
            }

            // Разность
            IEnumerable<int> arr = A.Except(B);

            Console.WriteLine("\n\nРазность");
            foreach (int k in arr) {
                Console.Write(k.ToString() + ' ');
            }
        }

        static void number8(string[] input) {
            int K = input[0][0] - '0';
            input = input.Skip(1).ToArray();  // Пропускаем первый элемент

            List<string> elements = new List<string>(input);

            // Первый фрагмент
            List<string> first = elements.GetRange(0, K);
            
            Console.WriteLine("Первый фрагмент:");

            foreach (string i in first) {
                Console.WriteLine(i);
            }

            // Второй фрагмент
            // Ищем индекс последнего элемента, оканчивающегося цифрой
            int tempIndex = 0;
            int index = 0;

            while (tempIndex < input.Length) {
                if (Char.IsDigit(input[tempIndex].Last())) {
                    index = tempIndex;
                }

                tempIndex++;
            }
            
            // Убираем все элементы до и включая последний элемент, оканчивающийся на цифру
            List<string> second = elements.Skip(index + 1).ToList();

            Console.WriteLine("Второй фрагмент:");

            foreach (string i in second) {
                Console.WriteLine(i);
            }
            
            // Пересечение
            Console.WriteLine("Пересечение:");

            List<string> joined = first.Union(second).ToList();

            joined.Sort();
            joined = joined.OrderBy(x => x.Length).ToList();

            // Вывод
            foreach (string i in joined) {
                Console.WriteLine(i);
            }
        }

        // Класс студентов для заданий 9 - 10
        public class Student {
            public string school;
            public string year;
            public string name;
            
            public Student(string _school, string _year, string _name)  {
                school = _school;
                year = _year;
                name = _name;
            }
        }
        static void number9(string[] input) {
            List<Student> students = new List<Student>();
            List<string> years = new List<string>();

            // Заносим студентов в список
            foreach (string i in input) {
                string[] studentInfo = i.Split('-');

                // Добавляем года поступления в список, чтобы потом смотреть по годам
                if (!years.Contains(studentInfo[1])) {
                    years.Add(studentInfo[1]);
                }

                Student newStudent = new Student(studentInfo[0], studentInfo[1], studentInfo[2]);

                students.Add(newStudent);
            }

            years.Sort();

            // Идем по годам и смотрим номера школ студентов, поступивших в этот год
            foreach (string year in years){
                List<string> schools = new List<string>();

                // Добавляем все уникальные школы в список
                foreach (Student student in students) {
                    if (student.year == year) {
                        if (!schools.Contains(student.school)) {
                            schools.Add(student.school);
                        }
                    }
                }

                // Выводим список школ за этот год
                Console.WriteLine("Год - " + year);

                foreach (string i in schools) {
                    Console.Write(i + ' ');
                }

                Console.Write('\n');
            }            
        }

        static void number9LINQ(string[] input) {
            // Пример вводной строки:
            // 1-2022-Zhuravlev 1-2022-Pupkin 1-2023-Ivanov
            
            // Создаем список всех студентов
            IEnumerable<Student> students = from i in input
                                select new Student(
                                    i.Split('-')[0],
                                    i.Split('-')[1],
                                    i.Split('-')[2]
                                );
            
            // Группируем студентов по годам
            var groupByYear = 
                from student in students
                group student by student.year into yearGroup
                orderby yearGroup.Key
                select yearGroup;

            // Для каждого года выводим студентов
            foreach (var yearGroup in groupByYear) {
                Console.WriteLine("\n\nГод: " + yearGroup.Key);
                
                // Группируем студентов по школам для каждого года
                var groupBySchool =
                    from student in yearGroup
                    group student by student.school into schoolGroup
                    orderby schoolGroup.Key
                    select schoolGroup;

                // Брать количество уникальных школ
                // Строка ниже ломает код
                Console.WriteLine("Уникальных школ - " + groupBySchool.Count() + '\n');

                foreach (var schoolGroup in groupBySchool) {
                    Console.WriteLine("Школа № " + schoolGroup.Key);
                    Console.WriteLine("Количество студентов - " + schoolGroup.Count());
                }
            }
        }

        static void number10(string[] input) {
            List<Student> students = new List<Student>();
            List<string> years = new List<string>();

            // Заносим студентов в список
            foreach (string i in input) {
                string[] studentInfo = i.Split('-');

                // Добавляем года поступления в список, чтобы потом смотреть по годам
                if (!years.Contains(studentInfo[1])) {
                    years.Add(studentInfo[1]);
                }

                Student newStudent = new Student(studentInfo[0], studentInfo[1], studentInfo[2]);

                students.Add(newStudent);
            }

            years.Sort();
            
            // Первое число - количество абитуриентов, второе - год
            int[] mostStudents = {0, 0};
            int[] leastStudents = {-1, 0};

            // Идем по годам и смотрим номера школ студентов, поступивших в этот год
            foreach (string year in years){
                List<string> schools = new List<string>();

                int studentCount = 0;
                // Добавляем все уникальные школы в список
                foreach (Student student in students) {
                    if (student.year == year) {
                        if (!schools.Contains(student.school)) {
                            schools.Add(student.school);
                        }

                        studentCount++;
                    }
                }

                // Обновляем наименьшее и наибольшее количество абитуриентов
                if (studentCount > mostStudents[0]) {
                    mostStudents[0] = studentCount;
                    mostStudents[1] = int.Parse(year);
                }

                if (leastStudents[0] == -1) {
                    leastStudents[0] = studentCount;
                    leastStudents[1] = int.Parse(year);
                }

                else if (studentCount < leastStudents[0]) {
                    leastStudents[0] = studentCount;
                    leastStudents[1] = int.Parse(year);
                }

                // Выводим список школ за этот год
                Console.WriteLine("Год - " + year);

                foreach (string i in schools) {
                    Console.Write(i + ' ');
                }

                Console.Write('\n');
            }

            Console.WriteLine("Наибольшее количество абитуриентов: " + mostStudents[0].ToString() + " в " + mostStudents[1].ToString() + " году");
            Console.WriteLine("Наименьшее количество абитуриентов: " + leastStudents[0].ToString() + " в " + leastStudents[1].ToString() + " году");
        }

        // Класс клиентов для задания 11
        public class Client {
            public string code;
            public List<int[]> months = new List<int[]>();  // Будут записываться месяцы клиента с годом и количеством часов

            public Client(string _code) {
                code = _code;
            }
            
            // Функция ищет наименее продуктивный месяц клиента
            public string leastProductiveMonth() {
                int[] leastHours = {-1, 0, 0};

                // Среди всех месяцев ищется месяц с наименьшим количеством часов
                
                // Объяснение индексов: 
                // leastHours[0] - Количество часов
                // leastHours[1] - Номер года
                // leastHours[2] - Номер месяца
                // month[0] - Номер года
                // month[1] - Номер месяца
                // month[2] - Количество часов

                foreach (int[] month in months) {
                    if (leastHours[0] == -1) {
                        leastHours[0] = month[2];
                        leastHours[2] = month[1];
                        leastHours[1] = month[0];
                    }

                    if (month[2] < leastHours[0]) {
                        leastHours[0] = month[2];
                        leastHours[2] = month[1];
                        leastHours[1] = month[0];
                    }
                }

                return "Кол-во часов - " + leastHours[0] + ", год - " + leastHours[1] + ", месяц - " + leastHours[2];
            }
        }
        
        static void number11(string[] input) {
            List<Client> clients = new List<Client>();
            List<string> months = new List<string>();

            List<string> checkedClients = new List<string>();

            // Заносим клиентов в список
            foreach (string i in input) {
                string[] clientInfo = i.Split('-');
                string clientCode = clientInfo[0];

                Client newClient = new Client(clientCode);

                // Заносим в список всех уникальных клиентов (по коду)
                if (!checkedClients.Contains(clientCode)) {
                    checkedClients.Add(clientCode);
                    clients.Add(newClient);
                }
            }

            // Добавляем месяцы в профиль клиента
            foreach (Client client in clients) {
                foreach (string i in input) {
                    string[] clientInfo = i.Split('-');
                    string clientCode = clientInfo[0];

                    int year = int.Parse(clientInfo[1]);
                    int month = int.Parse(clientInfo[2]);
                    int workoutHours = int.Parse(clientInfo[3]);

                    // Добавляем месяц к списку месяцев клиента
                    if (clientCode == client.code) {
                        client.months.Add(new int[] {year, month, workoutHours});
                    }
                }
            }

            foreach (Client client in clients) {
                Console.WriteLine("Клиент с кодом " + client.code);
                
                // Вывод всех месяцев клиента
                // foreach (int[] i in client.months) {
                //     Console.WriteLine("Год: " + i[0].ToString() + "\tМесяц: " + i[1].ToString() + "\tЧасов: " + i[2].ToString());
                // }

                // Вывод самого непродуктивного месяца
                Console.WriteLine("Самый непродуктивный месяц: " + client.leastProductiveMonth() + '\n');
            }
        }

        public class Client2 {
            public string code;
            public string year;
            public string month;
            public string workoutHours;

            public Client2(string _code, string _year, string _month, string _workoutHours) {
                code = _code;
                year = _year;
                month = _month;
                workoutHours = _workoutHours;
            }
        }
        static void number11LINQ(string[] input) {
            // Пример вводной строки:
            // 123-2022-6-50 123-2022-7-20 123-2023-1-10 124-2022-6-5 124-2022-7-80

            // Создаем список клиентов - есть дубликаты
            IEnumerable<Client2> clients = 
                from i in input
                select new Client2(
                    i.Split('-')[0],
                    i.Split('-')[1],
                    i.Split('-')[2],
                    i.Split('-')[3]
                );

            // Создаем список с группами по коду клиента
            var clientGroups = 
                from client in clients
                group client by client.code into clientGroup
                select clientGroup;

            // Для каждого клиента группируем по годам
            foreach (var clientGroup in clientGroups) {
                Console.WriteLine("\n\nКлиент с кодом " + clientGroup.Key);
                
                var years = 
                    from client in clientGroup
                    group client by client.year into yearGroup
                    select yearGroup;

                // Выводим наименее продуктивный месяц каждого года
                foreach (var yearGroup in years) {
                    Console.WriteLine("Год - " + yearGroup.Key);

                    // Ищем наименьшее количество часов в группе
                    var minValue = yearGroup.Min(x => x.workoutHours);
                    // Ищем запись по часам
                    var minMonth = yearGroup.Where(x => x.workoutHours == minValue).ToList();

                    Console.WriteLine("Наименее продуктивный месяц - " + minMonth[0].month + "\tКоличество часов - " + minMonth[0].workoutHours);
                }
            }
        }

        static string[] stringInput() {
            string input = Console.ReadLine();

            // Переводим строку в массив
            string[] arr = input.Split(' ');

            return arr;
        }


        static void Main(string[] args)
        {            
            string[] input = stringInput();

            // Вызов методов задач
            // number1(input);
            // number2(input);
            // number3(input);
            // number4(input);
            // number5(input);
            // number5LINQ(input);
            // number6(input);
            // number7(input);
            // number7LINQ(input);
            // number8(input);
            // number9(input);
            // number9LINQ(input);
            // number10(input);
            // number11(input);  // Выводит только один самый непродуктивный месяц, а не по годам
            // number11LINQ(input);
        }
    }
}

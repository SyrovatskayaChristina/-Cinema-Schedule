using System;
using System.Collections.Generic;

namespace Cinema
{
    #region Задание "Кинотеатр"
    //    Основное задание:

    //На любом языке программирования напишите консольную программу, составляющую расписание сеансов для кинотеатра.
    //При запуске программа получает на вход число - количество залов кинотеатра.
    //После этого пользователь вводит еще одно число - количество фильмов в прокате и поочередно вводит название и продолжительность(в минутах) для каждого фильма.
    //В ответ на полученную информацию программа формирует и выводит оптимальное  расписание сеансов каждого зала в формате:

    //Зал 1:
    //10:00 - 11:30 Король лев
    //11:30 - 14:00 Терминатор
    //14:00 - 15:30 Король лев

    //Оптимальным считается расписание, в котором залы простаивают без показов минимальное время.
    //Кинотеатр работает с 8:00 до 22:00.
    //Временем между сеансами можно пренебречь.

    //Дополнительные задания:

    //* Измените программу таким образом, чтобы, если это возможно, каждый прокатный фильм встречался как минимум 1 раз (достаточно присутствия фильма в одном из залов).

    //* Измените программу таким образом, чтобы расписания сеансов в различных залах не повторялись.
    //Достаточно отличия в один сеанс или в их порядке.Гарантированно программа будет получать на вход миниму два прокатных фильма.


    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            List<Film> userFilmList = new List<Film>();

            int hallCount;//кол-во залов, например 5
            int filmCount; //кол-во фильмов
            Console.WriteLine("Добро пожаловать в программу составления расписания для кинотеатра!\nКинотеатр работает с 8:00\n");
            DateTime startTime = new DateTime(2020, 10, 10, 8, 00, 00);
            Console.WriteLine("Введите длительность работы кинотеатра в минутах:");
            int cinemaWorkingMinutes = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите количество залов в кинотеатре:");
            hallCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите количество фильмов в прокате:");
            filmCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Для каждого фильма из проката необходимо предоставить более подробную информацию о нем (название и продолжительности фильма в минутах):");
            for (int i = 1; i <= filmCount; i++)
            {
                Console.WriteLine($"Введите название фильма № {i}:");
                string fName = Console.ReadLine();
                Console.WriteLine($"Введите длительность фильма №{i} (в минутах):");
                int fDuration = Convert.ToInt32(Console.ReadLine());
                userFilmList.Add(new Film { Duration = fDuration, Name = fName });
            }

            //создание дерева решений
            GraphTree cinemaTable = new GraphTree(userFilmList, cinemaWorkingMinutes);
            cinemaTable.CreateTree();

            //получнеие оптимального расписания для hallCount количества залов
            List<TableWithFreeTime> optimalTable = cinemaTable.CreateOptimalTable(hallCount);
            //вывод оптимального расписания на экран
            PrintTable(optimalTable, startTime);

            ////дополнительное задание задание
            ////Измените программу таким образом, чтобы, если это возможно, каждый прокатный фильм встречался как минимум 1 раз (достаточно присутствия фильма в одном из залов).
            //List<TableWithFreeTime> zopa1Table = cinemaTable.zopa1(hallCount);
            ////вывод расписания на экран
            //PrintTable(zopa1Table, startTime);
        }

        public static void PrintTable(List<TableWithFreeTime> allTablesWithFreeTime, DateTime startTime)
        {
            Console.WriteLine("\nОптимальное расписание работы залов кинотеатра:\n");
            int roomNumber = 1;
            //вывод расписания на экран
            foreach (var curTable in allTablesWithFreeTime)
            {
                DateTime startFilmTime = new DateTime();
                startFilmTime = startTime;
                Console.WriteLine($"\nРасписание работы зала № {roomNumber}:\n");
                foreach (var curFiml in curTable.Table)
                {
                    DateTime endFilmTime = startFilmTime.AddMinutes(curFiml.Duration);
                    Console.WriteLine($"{startTime.ToShortTimeString()}-{endFilmTime.ToShortTimeString()}  {curFiml.Name}, продолжительность:{curFiml.Duration} минут");
                    startFilmTime = endFilmTime;
                }
                Console.WriteLine($"Оставшееся свободное время в зале: {curTable.FreeTime} минут");
                Console.WriteLine("\n********************\n");
                roomNumber++;
            }
        }
    }
}

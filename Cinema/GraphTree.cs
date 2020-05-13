using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema
{
    public class GraphTree
    {
        List<Film> filmList; //список фильмов с их продолжительностью, которые вводит пользователь
        Node root;

        public List<TableWithFreeTime> allTablesWithFreeTime = new List<TableWithFreeTime>();

        /// <summary>
        /// Конструктор для дерева кинотератра
        /// </summary>
        /// <param name="list">Список фильмов для составлеия расписания</param>
        /// <param name="cinamaWorkingDuration">Сколько минут в день работает кинотеатр</param>
        public GraphTree(List<Film> list, int cinamaWorkingDuration)
        {
            filmList = list;
            root = new Node(cinamaWorkingDuration, new List<Film>());
        }


        /// <summary>
        /// Формирвоание дерева
        /// </summary>
        public void CreateTree()
        {
            addBranch(root);
        }

        /// <summary>
        /// Составление оптимального расписания
        /// </summary>
        /// <param name="count">Количество залов кинотеатра</param>
        /// <returns>Список - оптимальное расписание</returns>
        public List<TableWithFreeTime> CreateOptimalTable(int count)
        {
            List<TableWithFreeTime> result = new List<TableWithFreeTime>(allTablesWithFreeTime);
            //сортировка результатов по возрастанию
            result.Sort(compareTalbles);
            if (count < allTablesWithFreeTime.Count) //если кол-в залов < чем кол-во вариантов расписания
            {
                for (int i = allTablesWithFreeTime.Count - 1; i >= count; i--)
                {
                    result.RemoveAt(i);
                }
                //дописать если cont > allTablesWithFreeTime.Count
            }
            else if (count > allTablesWithFreeTime.Count) //если кол-в залов > чем кол-во вариантов расписания (Расписания в залах будут повторяться)
            {
                List<TableWithFreeTime> addingList = new List<TableWithFreeTime>(result);
                for (int i = 0, j = 0; i < count - allTablesWithFreeTime.Count; i++, j++)
                {
                    if (j == allTablesWithFreeTime.Count) j = 0;
                    result.Add(result[j]);
                }

            }
            return result;
        }

        /// <summary>
        /// Каждый прокатный фильм встревается хотя бы 1 раз
        /// </summary>
        /// <param name="count">Количество залов</param>
        /// <returns></returns>
        //public List<TableWithFreeTime>? zopa1(int count)
        //{

        //    List<Film> fList = new List<Film>(filmList);
        //    List<TableWithFreeTime> result = new List<TableWithFreeTime>(allTablesWithFreeTime);
        //    result.Sort(compareTalbles);

        //    for (int i = 0; i < Math.Min(result.Count, count); i++)
        //    {
        //        // foreach (var value in fList)
        //        for (int j = 0; j < fList.Count; j++)
        //        {
        //            if (result[i].Table.Find(x => x.Name == fList[j].Name) != null)
        //            {
        //                fList.Remove(fList[j]);
        //                j--;
        //            }
        //        }
        //    }
        //    if (fList.Count == 0) return null;
        //    //parts.Find(x => x.PartName.Contains("seat")));
        //    return result;
        //}
        private static int compareTalbles(TableWithFreeTime a, TableWithFreeTime b)
        {
            if (a.FreeTime == b.FreeTime) return 0;
            if (a.FreeTime > b.FreeTime) return 1;
            if (a.FreeTime < b.FreeTime) return -1;
            return 0;
        }

        private void addBranch(Node node)
        {
            foreach (var itemSub in filmList)
            {
                if (node.Value >= itemSub.Duration)
                {
                    List<Film> newList = new List<Film>(node.pathToNode);//копирование пути родителя
                    newList.Add(itemSub);//добавления к пути родителя текущего шага
                    Node newNode = new Node(node.Value - itemSub.Duration, newList);//создание ноды с новым путем и новым значением
                    node.AddChildren(newNode);//добавление в дерево новой ноды
                    addBranch(newNode);
                }
            }

            bool b = true;
            foreach (var itemSub in filmList)
            {
                if (node.Value >= itemSub.Duration)
                { b = false; }
            }
            if (b)
            {
                TableWithFreeTime curentTable = new TableWithFreeTime();
                curentTable.FreeTime = node.Value;
                curentTable.Table = new List<Film>();
                //вывод полного пути  - выделать в отдельный метод, чтобы он возвращал List<Films>, т.к.Console.WriteLine можно делать только в Program.cs !
                foreach (var value in node.pathToNode)
                {
                    curentTable.Table.Add(new Film { Name = value.Name, Duration = value.Duration });
                    //    Console.WriteLine($"{value.Name}, длительность:  {value.Duration} минут");
                }
                allTablesWithFreeTime.Add(curentTable);
                // Console.WriteLine("***********");

            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema
{
    public class TableWithFreeTime
    {
        /// <summary>
        /// время, оствшееся свободным в текущем зале
        /// </summary>
        public int FreeTime { get; set; }
        /// <summary>
        /// Список сеансов в текущем зале (название и продолжительность в минутах)
        /// </summary>
        public List<Film> Table { get; set; }

      
    }
}

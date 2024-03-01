using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FinalProjectFR
{
    public class Openings
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string PGNMoves { get; set; }
        public string Moves { get; set; }
        public string Description { get; set; }
        public string NameEn { get; set; }
        public string DescriptionEn { get; set; }
    }
}

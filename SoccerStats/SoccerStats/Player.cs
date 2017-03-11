using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerStats
{

    public class Rootobject
    {
        public Player[] Players { get; set; }
    }

    public class Player
    {
        public string first_name { get; set; }
        public int id { get; set; }
        public string points_per_game { get; set; }
        public string second_name { get; set; }
        public string team_name { get; set; }
    }

}

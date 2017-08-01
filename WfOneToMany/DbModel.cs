using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace WfOneToMany
{
    class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }

    class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Coach { get; set; }

        public virtual ICollection<Player> Players { get; set; }


        public Team(){
            Players = new List<Player>();

            }
        public override string ToString()
        {
            return Name;
        }


    }

    class DbConnected : DbContext
    {
        public DbConnected():base("SoccerDB")
        {}

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace SoccerStats
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDircetory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDircetory);
            var fileName = Path.Combine(directory.FullName, "SoccerGameResults.csv");
            var fileContents = ReadSoccerResults(fileName);
            fileName = Path.Combine(directory.FullName, "players.json");
            var players = DeserializePlayers(fileName);
            var topTenPlayers = GetTopTenPlayers(players);


            foreach (var player in players)
            {
                Console.WriteLine(
                    String.Format(
                    "Team: {0}, ID: {1}, First name: {2}, Last name: {3}, Points per game: {4}", 
                    
                    player.TeamName,
                    player.Id, 
                    player.firstName, 
                    player.LastName, 
                    player.PointsPerGame
                    )
                );
            }

            Console.WriteLine("\n The top ten players of the season are: \n");

            foreach (var player in topTenPlayers)
            {
                Console.WriteLine(
                    String.Format(
                        "Name: {0} {1}, Points Per Game: {2}",
                        player.firstName,
                        player.LastName,
                        player.PointsPerGame
                        )
                    );
            }
            fileName = Path.Combine(directory.FullName, "topTenPlayers.json");
            SerialzePlayersToFile(topTenPlayers, fileName);
        }

        public static string ReadFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            } 
        }

        public static List<GameResult> ReadSoccerResults(string fileName)
        {
            var soccerResults = new List<GameResult>();
            using (var reader = new StreamReader(fileName))
            {
                string line = "";
                while((line = reader.ReadLine()) != null)
                {
                    var gameResult = new GameResult();
                    string[] values = line.Split(',');
                    DateTime gameDate;
                    HomeOrAway homeOrAway;
                    int parseInt;
                    double possesionPercent;
                    if (DateTime.TryParse(values[0], out gameDate))
                    {
                        gameResult.GameDate = gameDate;
                    }
                    gameResult.TeamName = values[1];
                    if (Enum.TryParse(values[2], out homeOrAway))
                    {
                        gameResult.HomeOrAway = homeOrAway;
                    }
                    if (int.TryParse(values[3], out parseInt))
                    {
                        gameResult.Goals = parseInt;
                    }
                    if (int.TryParse(values[4], out parseInt))
                    {
                        gameResult.GoalAttempts = parseInt;
                    }
                    if (int.TryParse(values[5], out parseInt))
                    {
                        gameResult.ShotsOnGoal = parseInt;
                    }
                    if (int.TryParse(values[6], out parseInt))
                    {
                        gameResult.ShotsOffGoal = parseInt;
                    }
                    if (double.TryParse(values[7], out possesionPercent))
                    {
                        gameResult.PossesionPercent = possesionPercent;
                    }
                    soccerResults.Add(gameResult);
                }
            }
            return soccerResults;
        }

        public static List<Player> DeserializePlayers(string fileName)
        {
            var players = new List<Player>();
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(fileName))
            using(var jsonReader = new JsonTextReader(reader))
            {
                players = serializer.Deserialize<List<Player>>(jsonReader);
            }
            return players; 
        }

        public static List<Player> GetTopTenPlayers(List<Player> players)
        {
            var topTenPlayers = new List<Player>();
            players.Sort(new PlayerComparer());
            for (int i = 0; i < 9; i++)
            {
                topTenPlayers.Add(players[i]);
            }

            return topTenPlayers;
        }

        public static void SerialzePlayersToFile(List<Player> players, String fileName)
        {
            var serializer = new JsonSerializer();
            using (var writer = new StreamWriter(fileName))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, players);
            }
        }
    }
}

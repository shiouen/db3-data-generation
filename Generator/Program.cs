using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using Generator.Extensions;
using Generator.Model;
using System.Net.Http;

namespace Generator {
    public class Program {
        public string Path { get; set; }
        public Random Random { get; set; }

        public List<String> Places;
        public List<int> PostalCodes;
        public List<String> StreetNames;

        public List<Club> Clubs { get; set; }
        public List<ClubHouse> ClubHouses { get; set; }
        public List<Contest> Contests { get; set; }
        public List<ContestWeek> ContestWeeks { get; set; }
        public List<Game> Games { get; set; }
        public List<Player> Players { get; set; }
        public List<Season> Seasons { get; set; }
        public List<Selection> Selections { get; set; }
        public List<Set> Sets { get; set; }
        public List<TeamPlayer> TeamPlayers { get; set; }
        public List<Team> Teams { get; set; }

        public Program() {
            this.Clubs = new List<Club>();
            this.ClubHouses = new List<ClubHouse>();
            this.Contests = new List<Contest>();
            this.ContestWeeks = new List<ContestWeek>();
            this.Games = new List<Game>();
            this.Players = new List<Player>();
            this.Seasons = new List<Season>();
            this.Selections = new List<Selection>();
            this.TeamPlayers = new List<TeamPlayer>();
            this.Teams = new List<Team>();

            // Places
            this.Places = new List<String>(
                new String[] {
                    "Antwerpen", "Antwerpen", "Antwerpen", "Antwerpen", "Antwerpen", "Antwerpen", "Antwerpen",
                    "Deurne", "Wijnegem", "Borgerhout", "Merksem", "Ekeren", "Berchem", "Wilrijk", "Hoboken", "Aartselaar"
                });

            this.PostalCodes = new List<int>(new int[] { 2000, 2018, 2020, 2030, 2040, 2050, 2060, 2100, 2110, 2140, 2170, 2180, 2600, 2610, 2660, 2630 });

            this.StreetNames = new List<String>(
                new String[] {
                    "Arlington Avenue", "Jefferson Court", "Route 10", "Lafayette Street", "Hawthorne Lane", "Surrey Lane", "Laurel Street",
                    "Beech Street", "Virginia Street", "Street Road", "North Avenue", "Monroe Street", "Magnolia Court", "Cambridge Court", "Hanover Court",
                }
            );

            using (var client = new HttpClient()) {
                Uri uri = new Uri("http://api.randomuser.me/?results=5000");

                HttpResponseMessage result = client.GetAsync(uri).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(resultContent);
            }

            this.Random = new Random();
        }

        private Program GenerateClubs(int clubAmount = 40) {
            for (int clubId = 1; clubId <= clubAmount; ++clubId) {
                int clubHouseId = this.Random.Next(1, this.ClubHouses.Count);
                this.Clubs.Add(Club.Generate(clubId, clubHouseId));
            }

            return this;
        }

        private Program GenerateClubHouses(int clubHouseAmount = 15) {
            for (int clubHouseId = 1; clubHouseId <= clubHouseAmount; ++clubHouseId) {
                ClubHouse clubHouse = ClubHouse.Generate(
                    clubHouseId,
                    this.PostalCodes[clubHouseId - 1],
                    this.Places[clubHouseId - 1],
                    this.StreetNames[clubHouseId - 1],
                    this.Random
                );

                this.ClubHouses.Add(clubHouse);
            }

            return this;
        }

        private Program GenerateContestWeeks(int contestWeekAmountPerSeason = 22) {
            int contestWeekId = 1;

            foreach (Season season in this.Seasons) {
                for (int contestWeekNumber = 1; contestWeekNumber <= contestWeekAmountPerSeason; ++contestWeekNumber) {
                    this.ContestWeeks.Add(ContestWeek.Generate(contestWeekId, contestWeekNumber, season.Id, season.Start));

                    ++contestWeekId;
                }
            }

            return this;
        }

        private Program GenerateGames() {
            return this;
        }

        private Program GenerateSeasons(int amount = 5) {
            int seasonId = 1;

            for (seasonId = 1; seasonId <= amount; ++seasonId) {
                Season season = Season.Generate(seasonId);

                this.Seasons.Add(season);
            }

            return this;
        }

        public Program GenerateDatabase() {
            /* order is IMPORTANT */
            this.GenerateClubHouses()
                .GenerateClubs()
                .GenerateSeasons()
                .GenerateContestWeeks();

            return this;
        }

        public void WriteInserts(bool cleanUp) {
            StringBuilder builder = new StringBuilder();
            this.Path = String.Format("{0}../../{1}", AppDomain.CurrentDomain.BaseDirectory, "Files/inserts.sql");

            this.ClubHouses.BuildAsQuery(builder, "club_houses");
            this.Clubs.BuildAsQuery(builder, "clubs");

            this.Seasons.BuildAsQuery(builder, "seasons");
            this.ContestWeeks.BuildAsQuery(builder, "contest_weeks");

            using (StreamWriter writer = new StreamWriter(this.Path)) {
                writer.Write(builder.ToString());
            }
        }

        public static void Main(string[] args) {
            Stopwatch stopwatch = Stopwatch.StartNew();

            new Program()
                .GenerateDatabase()
                .WriteInserts(cleanUp: false);

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.Read();
        }
    }
}

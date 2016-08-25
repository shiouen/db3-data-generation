using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;

using Newtonsoft.Json;

using MySQL.Extensions;
using MySQL.Model;
using MySQL.Utilities;

namespace MySQL {
    public class Program {
        public string Path { get; set; }
        public Random Random { get; set; }

        public List<String> Places { get; set; }
        public List<int> PostalCodes { get; set; }
        public List<String> StreetNames { get; set; }
        public List<String> DepartmentNames { get; set; }

        public List<Club> Clubs { get; set; }
        public List<ClubHouse> ClubHouses { get; set; }
        public List<Contest> Contests { get; set; }
        public List<ContestWeek> ContestWeeks { get; set; }
        public List<Department> Departments { get; set; }
        public List<Game> Games { get; set; }
        public List<Player> Players { get; set; }
        public List<Season> Seasons { get; set; }
        public List<Set> Sets { get; set; }
        public List<TeamPlayer> TeamPlayers { get; set; }
        public List<Team> Teams { get; set; }

        public Dictionary<int, Ranking> RankingByTeamPlayerId { get; set; }
        public Dictionary<int, List<Contest>> ContestsByContestWeekId { get; set; }
        public Dictionary<int, List<Game>> GamesByContestId { get; set; }
        public Dictionary<int, List<TeamPlayer>> TeamPlayersByTeamId { get; set; }
        public Dictionary<int, List<Team>> TeamsByDepartmentId { get; set; }
        public Dictionary<int, int> AgeByTeamPlayerId { get; set; }

        public Program() {
            this.Clubs = new List<Club>();
            this.ClubHouses = new List<ClubHouse>();
            this.Contests = new List<Contest>();
            this.ContestWeeks = new List<ContestWeek>();
            this.Departments = new List<Department>();
            this.Games = new List<Game>();
            this.Players = new List<Player>();
            this.Seasons = new List<Season>();
            this.Sets = new List<Set>();
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

            // Departments
            this.DepartmentNames = new List<String>(new String[] {
                "F5", "E5", "D5", "C5", "B5", "A5", "D4", "C4", "B4", "A4", "C3", "B3", "A3", "B2", "A2", "A1"
            });

            // Dicitonaries
            this.RankingByTeamPlayerId = new Dictionary<int, Ranking>();
            this.ContestsByContestWeekId = new Dictionary<int, List<Contest>>();
            this.GamesByContestId = new Dictionary<int, List<Game>>();
            this.TeamPlayersByTeamId = new Dictionary<int, List<TeamPlayer>>();
            this.TeamsByDepartmentId = new Dictionary<int, List<Team>>();
            this.AgeByTeamPlayerId= new Dictionary<int, int>();

            this.Random = new Random();
        }

        private Program CompleteContests() {
            foreach (ContestWeek contestWeek in this.ContestWeeks) {
                List<Contest> contests = this.ContestsByContestWeekId[contestWeek.Id]
                    .OrderBy(contest => contest.Date)
                    .ToList();

                int contestNumber = 1;

                foreach (Contest contest in contests) {
                    contest.ContestNumber = contestNumber++;

                    if (contest.Surrender) {
                        // 1 / 2 chance each side to surrender
                        bool homeSurrenders = (this.Random.Next() % 2 == 0) && contest.Surrender;
                        bool guestSurrenders = contest.Surrender && !homeSurrenders;

                        contest.HomeScore = (guestSurrenders) ? 1 : 0;
                        contest.GuestScore = (homeSurrenders) ? 1 : 0;

                        continue;
                    }

                    List<Game> games = this.GamesByContestId[contest.Id];

                    contest.GuestScore = games.FindAll(game => game.GuestSetsWon > game.HomeSetsWon).Count();
                    contest.HomeScore = games.FindAll(game => game.GuestSetsWon < game.HomeSetsWon).Count();
                }
            }

            return this;
        }

        private Program GenerateClubs(int clubAmount = 40) {
            for (int clubId = 1; clubId <= clubAmount; ++clubId) {
                int clubHouseId = this.Random.Next(1, this.ClubHouses.Count + 1);
                this.Clubs.Add(Club.Generate(clubId, clubHouseId));
            }

            return this;
        }

        private Program GenerateContests() {
            int contestId = 1;

            int guestCaptainTeamPlayerId,
                guestFirstTeamPlayerId,
                guestSecondTeamPlayerId,
                guestThirdTeamPlayerId,
                guestFourthTeamPlayerId,
                homeCaptainTeamPlayerId,
                homeFirstTeamPlayerId,
                homeSecondTeamPlayerId,
                homeThirdTeamPlayerId,
                homeFourthTeamPlayerId,
                refereeTeamPlayerId = 0;

            List<Team> departmentTeams = null;

            List<TeamPlayer> guestTeamPlayersByRank = new List<TeamPlayer>();
            List<TeamPlayer> homeTeamPlayersByRank = new List<TeamPlayer>();

            ContestWeek contestWeek = null;
            int contestWeekNumber = 1;
            Contest contest = null;

            RankingComparer rankingComparer = new RankingComparer();

            Dictionary<int, HashSet<int>> teamAgendas = new Dictionary<int, HashSet<int>>();

            foreach (Department department in this.Departments) {
                departmentTeams = this.TeamsByDepartmentId[department.Id];

                foreach (Team homeTeam in departmentTeams) {
                    foreach (Team guestTeam in departmentTeams) {
                        if (homeTeam.Id == guestTeam.Id) { continue; }

                        if (!teamAgendas.ContainsKey(guestTeam.Id)) { teamAgendas[guestTeam.Id] = new HashSet<int>(); }
                        if (!teamAgendas.ContainsKey(homeTeam.Id)) { teamAgendas[homeTeam.Id] = new HashSet<int>(); }

                        guestTeamPlayersByRank = this.TeamPlayersByTeamId[guestTeam.Id]
                            .OrderByDescending(teamPlayer => this.RankingByTeamPlayerId[teamPlayer.Id], rankingComparer)
                            .ToList();

                        homeTeamPlayersByRank = this.TeamPlayersByTeamId[homeTeam.Id]
                            .OrderBy(teamPlayer => this.RankingByTeamPlayerId[teamPlayer.Id], rankingComparer)
                            .ToList();

                        guestCaptainTeamPlayerId = guestTeamPlayersByRank[this.Random.Next(0, 2)].Id;
                        guestFirstTeamPlayerId = guestCaptainTeamPlayerId;
                        guestSecondTeamPlayerId = guestTeamPlayersByRank[this.Random.Next(2, 5)].Id;
                        guestThirdTeamPlayerId = guestTeamPlayersByRank[this.Random.Next(5, 8)].Id;
                        guestFourthTeamPlayerId = guestTeamPlayersByRank[this.Random.Next(8, 9)].Id;

                        homeCaptainTeamPlayerId = homeTeamPlayersByRank[this.Random.Next(0, 2)].Id;
                        homeFirstTeamPlayerId = homeCaptainTeamPlayerId;
                        homeSecondTeamPlayerId = homeTeamPlayersByRank[this.Random.Next(2, 5)].Id;
                        homeThirdTeamPlayerId = homeTeamPlayersByRank[this.Random.Next(5, 8)].Id;
                        homeFourthTeamPlayerId = homeTeamPlayersByRank[this.Random.Next(8, 10)].Id;

                        refereeTeamPlayerId = guestTeamPlayersByRank[9].Id;

                        contestWeekNumber = (from n in Enumerable.Range(int.MinValue, int.MaxValue)
                                             let number = this.Random.Next(1, 23)
                                             where !teamAgendas[guestTeam.Id].Contains(number) || !teamAgendas[homeTeam.Id].Contains(number)
                                             select number).First();

                        contestWeek = this.ContestWeeks.Find(cw => cw.DepartmentId == department.Id && cw.WeekNumber == contestWeekNumber);

                        int days = this.Random.Next(0, 7);
                        DateTime date = contestWeek.Start.AddDays(days);

                        // 1 / 200 chance for surrender
                        bool surrender = (this.Random.Next() % 200 == 0);

                        contest = Contest.Generate(
                            contestId++, 0, date, 0, 0, surrender, contestWeek.Id,
                            guestTeam.Id, guestCaptainTeamPlayerId, guestFirstTeamPlayerId, guestSecondTeamPlayerId, guestThirdTeamPlayerId, guestFourthTeamPlayerId,
                            homeTeam.Id, homeCaptainTeamPlayerId, homeFirstTeamPlayerId, homeSecondTeamPlayerId, homeThirdTeamPlayerId, homeFourthTeamPlayerId,
                            refereeTeamPlayerId
                        );

                        teamAgendas[guestTeam.Id].Add(contestWeekNumber);
                        teamAgendas[homeTeam.Id].Add(contestWeekNumber);

                        this.ContestsByContestWeekId[contestWeek.Id].Add(contest);

                        this.Contests.Add(contest);
                    }
                }
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

        private Program GenerateContestWeeks(int amountPerDepartmentPerSeason = 22) {
            int contestWeekId = 1;

            foreach (Department department in this.Departments) {
                DateTime seasonStart = this.Seasons.Find(season => season.Id == department.SeasonId).Start;

                for (int contestWeekNumber = 1; contestWeekNumber <= amountPerDepartmentPerSeason; ++contestWeekNumber) {
                    ContestWeek contestWeek = ContestWeek.Generate(contestWeekId++, contestWeekNumber, department.Id, seasonStart);

                    this.ContestsByContestWeekId[contestWeek.Id] = new List<Contest>();

                    this.ContestWeeks.Add(contestWeek);
                }
            }

            return this;
        }

        private Program GenerateDepartments() {
            int departmentId = 1;

            foreach (Season season in this.Seasons) {
                foreach (string departmentName in this.DepartmentNames) {
                    Department department = Department.Generate(departmentId++, departmentName, "Antwerpen", season.Id);
                    this.Departments.Add(department);
                }
            }

            return this;
        }

        private Program GenerateGames() {
            Game game = null;

            int gameId = 1;

            foreach (Contest contest in this.Contests) {
                int gameNumber = 1;

                if (contest.Surrender) { continue; }

                this.GamesByContestId[contest.Id] = new List<Game>();

                //  4-2
                game = Game.Generate(gameId++, gameNumber++, 0, 0, contest.Id, contest.HomeFourthTeamPlayerId, contest.GuestSecondTeamPlayerId);
                this.Games.Add(game);
                this.GamesByContestId[contest.Id].Add(game);

                //  3-1
                game = Game.Generate(gameId++, gameNumber++, 0, 0, contest.Id, contest.HomeThirdTeamPlayerId, contest.GuestFirstTeamPlayerId);
                this.Games.Add(game);
                this.GamesByContestId[contest.Id].Add(game);

                //  2-4
                game = Game.Generate(gameId++, gameNumber++, 0, 0, contest.Id, contest.HomeSecondTeamPlayerId, contest.GuestFourthTeamPlayerId);
                this.Games.Add(game);
                this.GamesByContestId[contest.Id].Add(game);

                //  1-3
                game = Game.Generate(gameId++, gameNumber++, 0, 0, contest.Id, contest.HomeFirstTeamPlayerId, contest.GuestThirdTeamPlayerId);
                this.Games.Add(game);
                this.GamesByContestId[contest.Id].Add(game);

                //  4-1
                game = Game.Generate(gameId++, gameNumber++, 0, 0, contest.Id, contest.HomeFourthTeamPlayerId, contest.GuestFirstTeamPlayerId);
                this.Games.Add(game);
                this.GamesByContestId[contest.Id].Add(game);

                //  3-2
                game = Game.Generate(gameId++, gameNumber++, 0, 0, contest.Id, contest.HomeThirdTeamPlayerId, contest.GuestSecondTeamPlayerId);
                this.Games.Add(game);
                this.GamesByContestId[contest.Id].Add(game);

                //  2-3
                game = Game.Generate(gameId++, gameNumber++, 0, 0, contest.Id, contest.HomeSecondTeamPlayerId, contest.GuestThirdTeamPlayerId);
                this.Games.Add(game);
                this.GamesByContestId[contest.Id].Add(game);

                //  1-4
                game = Game.Generate(gameId++, gameNumber++, 0, 0, contest.Id, contest.HomeFirstTeamPlayerId, contest.GuestFourthTeamPlayerId);
                this.Games.Add(game);
                this.GamesByContestId[contest.Id].Add(game);

                //  4-3
                game = Game.Generate(gameId++, gameNumber++, 0, 0, contest.Id, contest.HomeFourthTeamPlayerId, contest.GuestThirdTeamPlayerId);
                this.Games.Add(game);
                this.GamesByContestId[contest.Id].Add(game);

                //  3-4
                game = Game.Generate(gameId++, gameNumber++, 0, 0, contest.Id, contest.HomeThirdTeamPlayerId, contest.GuestFourthTeamPlayerId);
                this.Games.Add(game);
                this.GamesByContestId[contest.Id].Add(game);

                //  2-1
                game = Game.Generate(gameId++, gameNumber++, 0, 0, contest.Id, contest.HomeSecondTeamPlayerId, contest.GuestFirstTeamPlayerId);
                this.Games.Add(game);
                this.GamesByContestId[contest.Id].Add(game);

                //  1-2
                game = Game.Generate(gameId++, gameNumber++, 0, 0, contest.Id, contest.HomeFirstTeamPlayerId, contest.GuestSecondTeamPlayerId);
                this.Games.Add(game);
                this.GamesByContestId[contest.Id].Add(game);

                //  4-4
                game = Game.Generate(gameId++, gameNumber++, 0, 0, contest.Id, contest.HomeFourthTeamPlayerId, contest.GuestFourthTeamPlayerId);
                this.Games.Add(game);
                this.GamesByContestId[contest.Id].Add(game);

                //  3-3
                game = Game.Generate(gameId++, gameNumber++, 0, 0, contest.Id, contest.HomeThirdTeamPlayerId, contest.GuestThirdTeamPlayerId);
                this.Games.Add(game);
                this.GamesByContestId[contest.Id].Add(game);

                //  2-2
                game = Game.Generate(gameId++, gameNumber++, 0, 0, contest.Id, contest.HomeSecondTeamPlayerId, contest.GuestSecondTeamPlayerId);
                this.Games.Add(game);
                this.GamesByContestId[contest.Id].Add(game);

                //  1-1
                game = Game.Generate(gameId++, gameNumber++, 0, 0, contest.Id, contest.HomeFirstTeamPlayerId, contest.GuestFirstTeamPlayerId);
                this.Games.Add(game);
                this.GamesByContestId[contest.Id].Add(game);
            }

            return this;
        }

        private Program GeneratePlayers() {
            List<PlayerMock> playerMocks = new List<PlayerMock>();

            using (StreamReader r = new StreamReader(System.IO.Path.GetFullPath("Files/players_mock_data.json"))) {
                string json = r.ReadToEnd();
                playerMocks = JsonConvert.DeserializeObject<List<PlayerMock>>(json);
            }

            Array rankings = Enum.GetValues(typeof(Ranking));

            int playerId = 1;

            foreach (PlayerMock mock in playerMocks) {
                int randomIndex = this.Random.Next(0, this.PostalCodes.Count);
                string place = this.Places[randomIndex];
                int postalCode = this.PostalCodes[randomIndex];

                DateTime dateOfBirth = Convert.ToDateTime(mock.date_of_birth);

                Ranking ranking = (Ranking) rankings.GetValue(this.Random.Next(rankings.Length));

                string streetName = string.Format("{0} Street", mock.street_name);
                
                Player player = Player.Generate(playerId++, dateOfBirth, mock.first_name, mock.last_name, postalCode, place, ranking, streetName, mock.street_number);

                this.Players.Add(player);
            }

            return this;
        }

        private Program GenerateSeasons(int amount = 5) {
            for (int seasonId = 1; seasonId <= amount; ++seasonId) {
                Season season = Season.Generate(seasonId);

                this.Seasons.Add(season);
            }

            return this;
        }

        private Program GenerateSets() {
            int setId = 1;

            SetUtilities setUtils = new SetUtilities();

            foreach (Game game in this.Games) {
                int setNumber = 1;

                Ranking guestRanking = this.RankingByTeamPlayerId[game.GuestTeamPlayerId];
                Ranking homeRanking = this.RankingByTeamPlayerId[game.HomeTeamPlayerId];

                while (game.HomeSetsWon != 3 && game.GuestSetsWon != 3) {
                    bool homeWins = setUtils.DetermineSetByRankAndAge(
                        homeRanking, this.AgeByTeamPlayerId[game.HomeTeamPlayerId],
                        guestRanking, this.AgeByTeamPlayerId[game.GuestTeamPlayerId],
                        this.Random);

                    int winningScore = this.Random.Next(11, 50);
                    int losingScore = 0;

                    switch (winningScore) {
                        case 11:
                            losingScore = this.Random.Next(0, 10);
                            break;
                        default:
                            losingScore = winningScore - 2;
                            break;
                    }

                    if (homeWins) { game.HomeSetsWon++; }
                    else { game.GuestSetsWon++; }

                    int duration = this.Random.Next(30, 7201);

                    Set set = Set.Generate(setId++, duration, (homeWins) ? losingScore : winningScore, (homeWins) ? winningScore : losingScore, setNumber++, game.Id);
                    this.Sets.Add(set);
                }
            }

            return this;
        }

        private Program GenerateTeams(int amountPerDepartment = 12) {
            int teamId = 1;

            foreach (Department department in this.Departments) {
                this.TeamsByDepartmentId[department.Id] = new List<Team>();

                for (int i = 0; i < amountPerDepartment; ++i) {
                    int clubId = this.Clubs[this.Random.Next(0, this.Clubs.Count)].Id;
                    Team team = Team.Generate(teamId++, clubId, department.Id);

                    this.TeamsByDepartmentId[department.Id].Add(team);

                    this.Teams.Add(team);
                }
            }

            return this;
        }

        private Program GenerateTeamPlayers(int amountPerTeam = 10) {
            int teamPlayerId = 1;

            foreach (Team team in this.Teams) {
                List<Player> players = new List<Player>();

                this.TeamPlayersByTeamId[team.Id] = new List<TeamPlayer>();

                while (players.Count < amountPerTeam) {
                    Player player = this.Players[this.Random.Next(0, this.Players.Count)];

                    if (!players.Contains(player)) {
                        players.Add(player);
                    }
                }

                foreach (Player player in players) {
                    TeamPlayer teamPlayer = TeamPlayer.Generate(teamPlayerId++, player.Id, team.Id);

                    this.RankingByTeamPlayerId[teamPlayer.Id] = player.Ranking;
                    this.TeamPlayersByTeamId[team.Id].Add(teamPlayer);
                    this.AgeByTeamPlayerId[teamPlayer.Id] = DateTime.Now.Year - player.DateOfBirth.Year;

                    this.TeamPlayers.Add(teamPlayer);
                }
            }

            return this;
        }

        public Program GenerateDatabase() {
            /* order is IMPORTANT */
            this.GenerateClubHouses()
                .GenerateClubs()
                .GenerateSeasons()
                .GenerateDepartments()
                .GenerateContestWeeks()
                .GeneratePlayers()
                .GenerateTeams()
                .GenerateTeamPlayers()
                .GenerateContests()
                .GenerateGames()
                .GenerateSets()
                .CompleteContests();

            return this;
        }

        public void WriteInserts(bool cleanUp) {
            StringBuilder builder = new StringBuilder();
            this.Path = String.Format("{0}../../{1}", AppDomain.CurrentDomain.BaseDirectory, "Files/inserts.sql");

            this.ClubHouses.BuildAsQuery(builder, "club_houses");
            this.Clubs.BuildAsQuery(builder, "clubs");

            this.Seasons.BuildAsQuery(builder, "seasons");
            this.Departments.BuildAsQuery(builder, "departments");
            this.ContestWeeks.BuildAsQuery(builder, "contest_weeks");

            this.Players.BuildAsQuery(builder, "players");
            this.Teams.BuildAsQuery(builder, "teams");
            this.TeamPlayers.BuildAsQuery(builder, "team_players");

            this.Contests.BuildAsQuery(builder, "contests");
            this.Games.BuildAsQuery(builder, "games");
            this.Sets.BuildAsQuery(builder, "sets");

            using (StreamWriter writer = new StreamWriter(this.Path)) {
                writer.Write(builder.ToString());
                writer.Flush();
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

        private class PlayerMock {
            public virtual string first_name { get; set; }
            public virtual string last_name { get; set; }
            public virtual string date_of_birth { get; set; }
            public virtual int postal_code { get; set; }
            public virtual string street_name { get; set; }
            public virtual int street_number { get; set; }
        }
    }
}

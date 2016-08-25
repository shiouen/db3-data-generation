using System;

namespace MySQL.Model {
    public class Game {
        public int Id { get; set; }
        public int GameNumber { get; set; }
        public int GuestSetsWon { get; set; }
        public int HomeSetsWon { get; set; }
        public int ContestId { get; set; }
        public int GuestTeamPlayerId { get; set; }
        public int HomeTeamPlayerId { get; set; }

        public static Game Generate(int index, int gameNumber, int guestSetsWon, int homeSetsWon, int contestId, int guestTeamPlayerId, int homeTeamPlayerId) {
            return new Game {
                Id = index,
                GameNumber = gameNumber,
                GuestSetsWon = guestSetsWon,
                HomeSetsWon = homeSetsWon,
                ContestId = contestId,
                GuestTeamPlayerId = guestTeamPlayerId,
                HomeTeamPlayerId = homeTeamPlayerId
            };
        }

        public override string ToString() {
            return String.Format(
                "({0}, {1}, {2}, {3}, {4}, {5}, {6})",
                this.Id,
                this.GameNumber,
                this.GuestSetsWon,
                this.HomeSetsWon,
                this.ContestId,
                this.GuestTeamPlayerId,
                this.HomeTeamPlayerId
            );
        }
    }
}

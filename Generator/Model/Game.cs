using System;

namespace Generator.Model {
    public class Game {
        public int Id { get; set; }
        public Winner Winner { get; set; }
        public int ContestId { get; set; }
        public int GuestTeamPlayerId { get; set; }
        public int HomeTeamPlayerId { get; set; }

        public static Game Generate(int index, Winner winner, int contestId, int guestTeamPlayerId, int homeTeamPlayerId) {
            return new Game {
                Id = index,
                Winner = winner,
                ContestId = contestId,
                GuestTeamPlayerId = guestTeamPlayerId,
                HomeTeamPlayerId = homeTeamPlayerId
            };
        }

        public override string ToString() {
            return String.Format(
                "({0}, '{1}', {2}, {3}, {4})",
                this.Id,
                this.Winner.ToString(),
                this.ContestId,
                this.GuestTeamPlayerId,
                this.HomeTeamPlayerId
            );
        }
    }
}

using System;

namespace Generator.Model {
    public class Contest {
        public int Id { get; set; }
        public int GuestScore { get; set; }
        public int HomeScore { get; set; }
        public bool Surrender { get; set; }
        public int ContestWeekId { get; set; }
        public int GuestSelectionId { get; set; }
        public int HomeSelectionId { get; set; }
        public int RefereeTeamPlayerId { get; set; }
        public int WinningTeamId { get; set; }

        public static Contest Generate(int index, int guestScore, int homeScore, bool surrender, int contestWeekId, int guestSelectionId, int homeSelectionId, int refereeTeamPlayerId, int winningTeamId) {
            return new Contest {
                Id = index,
                GuestScore = guestScore,
                HomeScore = homeScore,
                Surrender = surrender,
                ContestWeekId = contestWeekId,
                GuestSelectionId = guestSelectionId,
                HomeSelectionId = homeSelectionId,
                RefereeTeamPlayerId = refereeTeamPlayerId,
                WinningTeamId = winningTeamId
            };
        }

        public override string ToString() {
            return String.Format(
                "({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})",
                this.Id,
                this.GuestScore,
                this.HomeScore,
                this.Surrender.ToString().ToUpper(),
                this.ContestWeekId,
                this.GuestSelectionId,
                this.HomeSelectionId,
                this.RefereeTeamPlayerId,
                this.WinningTeamId
            );
        }
    }
}

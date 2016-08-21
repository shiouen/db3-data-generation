using System;

namespace Generator.Model {
    public class Set {
        public int Id { get; set; }
        public int Duration { get; set; }
        public int GuestScore { get; set; }
        public int HomeScore { get; set; }
        public int SetNumber { get; set; }
        public int GameId { get; set; }

        public static Set Generate(int index, int duration, int guestScore, int homeScore, int setNumber, int gameId) {
            return new Set {
                Id = index,
                Duration = duration,
                GuestScore = guestScore,
                HomeScore = homeScore,
                SetNumber = setNumber,
                GameId = gameId
            };
        }

        public override string ToString() {
            return String.Format(
                "({0}, {1}, {2}, {3}, {4}, {5})",
                this.Id,
                this.Duration,
                this.GuestScore,
                this.HomeScore,
                this.SetNumber,
                this.GameId
            );
        }
    }
}

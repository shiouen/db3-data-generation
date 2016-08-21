using System;

namespace Generator.Model {
    public class TeamPlayer {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }

        public static TeamPlayer Generate(int index, int playerId, int teamId) {
            return new TeamPlayer {
                Id = index,
                PlayerId = playerId,
                TeamId = teamId
            };
        }

        public override string ToString() {
            return String.Format(
                "({0}, {1}, {2})",
                this.Id,
                this.PlayerId,
                this.TeamId
            );
        }
    }
}

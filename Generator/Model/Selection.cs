using System;

namespace Generator.Model {
    public class Selection {
        public int Id { get; set; }
        public int CaptainTeamPlayerId { get; set; }
        public int ContestWeekId { get; set; }
        public int TeamId { get; set; }
        public int FirstTeamPlayerId { get; set; }
        public int SecondTeamPlayerId { get; set; }
        public int ThirdTeamPlayerId { get; set; }
        public int FourthTeamPlayerId { get; set; }

        public static Selection Generate(int index, int contestWeekId, int teamId, int firstTeamPlayerId, int secondTeamPlayerId, int thirdTeamPlayerId, int fourthTeamPlayerId) {
            return new Selection {
                Id = index,
                CaptainTeamPlayerId = firstTeamPlayerId,
                ContestWeekId = contestWeekId,
                TeamId = teamId,
                FirstTeamPlayerId = firstTeamPlayerId,
                SecondTeamPlayerId = secondTeamPlayerId,
                ThirdTeamPlayerId = thirdTeamPlayerId,
                FourthTeamPlayerId = fourthTeamPlayerId
            };
        }

        public override string ToString() {
            return String.Format(
                "({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8})",
                this.Id,
                this.CaptainTeamPlayerId,
                this.ContestWeekId,
                this.TeamId,
                this.FirstTeamPlayerId,
                this.SecondTeamPlayerId,
                this.ThirdTeamPlayerId,
                this.FourthTeamPlayerId
            );
        }
    }
}

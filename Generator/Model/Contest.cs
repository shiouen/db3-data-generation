using System;

namespace MySQL.Model {
    public class Contest {
        public int Id { get; set; }
        public int ContestNumber { get; set; }
        public DateTime Date { get; set; }
        public int GuestScore { get; set; }
        public int HomeScore { get; set; }
        public bool Surrender { get; set; }
        public int ContestWeekId { get; set; }

        public int GuestTeamId { get; set; }
        public int GuestCaptainTeamPlayerId { get; set; }
        public int GuestFirstTeamPlayerId { get; set; }
        public int GuestSecondTeamPlayerId { get; set; }
        public int GuestThirdTeamPlayerId { get; set; }
        public int GuestFourthTeamPlayerId { get; set; }

        public int HomeTeamId { get; set; }
        public int HomeCaptainTeamPlayerId { get; set; }
        public int HomeFirstTeamPlayerId { get; set; }
        public int HomeSecondTeamPlayerId { get; set; }
        public int HomeThirdTeamPlayerId { get; set; }
        public int HomeFourthTeamPlayerId { get; set; }
        public int RefereeTeamPlayerId { get; set; }

    
        public static Contest Generate(
           int index, int contestNumber, DateTime date, int guestScore, int homeScore, bool surrender, int contestWeekId,
           int guestTeamId, int guestCaptainTeamPlayerId, int guestFirstTeamPlayerId, int guestSecondTeamPlayerId, int guestThirdTeamPlayerId, int guestFourthTeamPlayerId,
           int homeTeamId, int homeCaptainTeamPlayerId, int homeFirstTeamPlayerId, int homeSecondTeamPlayerId, int homeThirdTeamPlayerId, int homeFourthTeamPlayerId,
           int refereeTeamPlayerId) {
            return new Contest {
                Id = index,
                ContestNumber = contestNumber,
                Date = date,
                GuestScore = guestScore,
                HomeScore = homeScore,
                Surrender = surrender,
                ContestWeekId = contestWeekId,
                GuestTeamId = guestTeamId,
                GuestCaptainTeamPlayerId = guestCaptainTeamPlayerId,
                GuestFirstTeamPlayerId = guestFirstTeamPlayerId,
                GuestSecondTeamPlayerId = guestSecondTeamPlayerId,
                GuestThirdTeamPlayerId = guestThirdTeamPlayerId,
                GuestFourthTeamPlayerId = guestFourthTeamPlayerId,
                HomeTeamId = homeTeamId,
                HomeCaptainTeamPlayerId = homeCaptainTeamPlayerId,
                HomeFirstTeamPlayerId = homeFirstTeamPlayerId,
                HomeSecondTeamPlayerId = homeSecondTeamPlayerId,
                HomeThirdTeamPlayerId = homeThirdTeamPlayerId,
                HomeFourthTeamPlayerId = homeFourthTeamPlayerId,
                RefereeTeamPlayerId = refereeTeamPlayerId
            };
        }

        public override string ToString() {
            return String.Format(
                "({0}, {1}, '{2}', {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19})",
                this.Id,
                this.ContestNumber,
                this.Date.ToString("yyyy-MM-dd"),
                this.GuestScore,
                this.HomeScore,
                this.Surrender.ToString().ToUpper(),
                this.ContestWeekId,
                this.GuestTeamId,
                this.GuestCaptainTeamPlayerId,
                this.GuestFirstTeamPlayerId,
                this.GuestSecondTeamPlayerId,
                this.GuestThirdTeamPlayerId,
                this.GuestFourthTeamPlayerId,
                this.HomeTeamId,
                this.HomeCaptainTeamPlayerId,
                this.HomeFirstTeamPlayerId,
                this.HomeSecondTeamPlayerId,
                this.HomeThirdTeamPlayerId,
                this.HomeFourthTeamPlayerId,
                this.RefereeTeamPlayerId
            );
        }
    }
}

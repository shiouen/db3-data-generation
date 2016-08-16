using System;

namespace Generator.Model {
    public class ContestWeek {
        public int Id { get; set; }
        public DateTime End { get; set; }
        public DateTime Start { get; set; }
        public int WeekNumber { get; set; }
        public int SeasonId { get; set; }

        public static ContestWeek Generate(int index, int weekNumber, int seasonId, DateTime seasonStart) {
            return new ContestWeek {
                Id = index,
                End = seasonStart.AddDays((weekNumber * 7) - 1),
                Start = seasonStart.AddDays((weekNumber - 1) * 7),
                WeekNumber = weekNumber,
                SeasonId = seasonId
            };
        }

        public override string ToString() {
            string s = "({0}, '{1}', '{2}', {3}, {4})";
            return String.Format(s, this.Id, this.End.ToString("yyyy-MM-dd"), this.Start.ToString("yyyy-MM-dd"), this.WeekNumber, this.SeasonId);
        }
    }
}

using System;

namespace Generator.Model {
    public class Season {
        public int Id { get; set; }
        public DateTime End { get; set; }
        public DateTime Start { get; set; }

        public static Season Generate(int index) {
            DateTime start = new DateTime((DateTime.Now.Year + index - 1), 1, 1);

            return new Season {
                Id = index,
                End = start.AddDays(22 * 7),
                Start = start
            };
        }

        public override string ToString() {
            return String.Format(
                "({0}, '{1}', '{2}')",
                this.Id,
                this.End.ToString("yyyy-MM-dd"),
                this.Start.ToString("yyyy-MM-dd")
            );
        }
    }
}

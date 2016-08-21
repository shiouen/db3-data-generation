using System.Collections.Generic;

namespace Generator.Model {
    public enum Ranking {
        NG,
        E6, E4, E2, E0,
        D6, D4, D2, D0,
        C6, C4, C2, C0,
        B6, B4, B2, B0,
        A
    }

    public class RankingComparer : IComparer<Ranking> {
        protected IList<Ranking> orderedTypes { get; set; }

        public RankingComparer() {
            // you can reorder it's all as you want
            orderedTypes = new List<Ranking>() {
                Ranking.NG,
                Ranking.E6, Ranking.E4, Ranking.E2, Ranking.E0,
                Ranking.D6, Ranking.D4, Ranking.D2, Ranking.D0,
                Ranking.C6, Ranking.C4, Ranking.C2, Ranking.C0,
                Ranking.B6, Ranking.B4, Ranking.B2, Ranking.B0,
                Ranking.A
            };
        }

        public int Compare(Ranking x, Ranking y) {
            var xIndex = orderedTypes.IndexOf(x);
            var yIndex = orderedTypes.IndexOf(y);

            return xIndex.CompareTo(yIndex);
        }
    };
}

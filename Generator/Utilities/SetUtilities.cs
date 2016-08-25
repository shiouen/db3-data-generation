using System;
using MySQL.Model;

namespace MySQL.Utilities {
    public class SetUtilities {
        public RankingComparer RankingComparer { get; set; }

        public SetUtilities() {
            this.RankingComparer = new RankingComparer();
        }

        public bool DetermineSetByRankAndAge(Ranking playerRanking, int playerAge, Ranking opponentRanking, int opponentAge, Random random) {
            bool playerWins;

            int rankingComparison = this.RankingComparer.Compare(playerRanking, opponentRanking);

            int rankingValueDifference = Math.Abs(this.RankingComparer.RankingToInt(playerRanking) - this.RankingComparer.RankingToInt(opponentRanking));

            bool playerBetterAndOlder = rankingComparison > 0 && playerAge > opponentAge;
            bool playerBetterAndSameAge = rankingComparison > 0 && playerAge == opponentAge;
            bool playerBetterAndYounger = rankingComparison > 0 && playerAge < opponentAge;
            bool opponentBetterAndYounger = rankingComparison < 0 && playerAge > opponentAge;
            bool opponentBetterAndSameAge = rankingComparison < 0 && playerAge == opponentAge;
            bool opponentBetterAndOlder = rankingComparison < 0 && playerAge < opponentAge;

            if (playerBetterAndOlder) { playerWins = true; }
            else if (playerBetterAndSameAge) { playerWins = !(random.Next() % rankingValueDifference == 0) ? true : false; }
            else if (playerBetterAndYounger) { playerWins = !(random.Next() % (rankingValueDifference * 2) == 0) ? true : false; }

            else if (opponentBetterAndYounger) { playerWins = !(random.Next() % (rankingValueDifference * 2 ) == 0) ? false : true; }
            else if (opponentBetterAndSameAge) { playerWins = !(random.Next() % rankingValueDifference == 0) ? false : true; }
            else if (opponentBetterAndOlder) { playerWins = false; }

            else { playerWins = (random.Next() % 2 == 0) ? true : false; }

            return playerWins;

        }
    }
}

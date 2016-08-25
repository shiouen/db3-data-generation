using NoSQL.Model;
using System;

namespace NoSQL.Extensions {
    public static class RandomExtensions {
        private static double racketMinimumLatitude = -0.0000085;
        private static double racketMaximumLatitude = 0.0000085;

        private static double racketMinimumLongitude = -0.0000075;
        private static double racketMaximumLongitude = 0.0000075;

        private static double tableMinimumLatitude = -0.0002000;
        private static double tableMaximumLatitude = 0.0002000;

        private static double tableMinimumLongitude = -0.0002800;
        private static double tableMaximumLongitude = 0.0002800;

        public static Coordinate NextRacketCoordinate(this Random random) {
            return new Coordinate(random.NextRacketLatitude(), random.NextRacketLongitude());
        }

        public static double NextRacketLatitude(this Random random) {
            return Math.Round(random.NextDouble() * (racketMaximumLatitude - racketMinimumLatitude) + racketMinimumLatitude, 7);
        }

        public static double NextRacketLongitude(this Random random) {
            return Math.Round(random.NextDouble() * (racketMaximumLongitude - racketMinimumLongitude) + racketMinimumLongitude, 7);
        }

        public static Coordinate NextTableCoordinate(this Random random) {
            return new Coordinate(random.NextTableLatitude(), random.NextTableLongitude());
        }

        public static double NextTableLatitude(this Random random) {
            return Math.Round(random.NextDouble() * (tableMaximumLatitude - tableMinimumLatitude) + tableMinimumLatitude, 7);
        }

        public static double NextTableLongitude(this Random random) {
            return Math.Round(random.NextDouble() * (tableMaximumLongitude - tableMinimumLongitude) + tableMinimumLongitude, 7);
        }

        public static HitType NextHitType(this Random random) {
            Array values = Enum.GetValues(typeof(HitType));
            return (HitType) values.GetValue(random.Next(values.Length));
        }
    }
}
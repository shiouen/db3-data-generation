using System;

namespace MySQL.Extensions {
    public static class RandomExtensions {
        private static double minimumLatitude = -89.999999;
        private static double maximumLatitude = 89.999999;

        private static double minimumLongitude = -179.999999;
        private static double maximumLongitude = 179.999999;

        public static double NextLatitude(this Random random) {
            return random.NextDouble() * (maximumLatitude - minimumLatitude) + minimumLatitude;
        }

        public static double NextLongitude(this Random random) {
            return random.NextDouble() * (maximumLongitude - minimumLongitude) + minimumLongitude;
        }
    }
}

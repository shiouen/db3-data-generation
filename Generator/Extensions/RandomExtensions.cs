using System;

namespace Generator.Extensions {
    public static class RandomExtensions {
        private static double minimumLatitude = -89.999999;
        private static double maximumLatitude = 89.999999;

        private static double minimumLongitude = -179.999999;
        private static double maximumLongitude = 179.999999;

        public static double nextLatitude(this Random random) {
            return random.NextDouble() * (maximumLatitude - minimumLatitude) + minimumLatitude;
        }

        public static double nextLongitude(this Random random) {
            return random.NextDouble() * (maximumLongitude - minimumLongitude) + minimumLongitude;
        }
    }
}

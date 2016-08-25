namespace NoSQL.Model {
    public class Coordinate {
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }

        public Coordinate(double latitude, double longitude) {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}

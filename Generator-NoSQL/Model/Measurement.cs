using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSQL.Model {
    public class Measurement {
        public virtual int ContestId { get; set; }
        public virtual int GameId { get; set; }
        public virtual int PlayerId { get; set; }
        public virtual Sensor Sensor { get; set; }
        public virtual DateTime Timestamp { get; set; }
        public virtual Coordinate Coordinate { get; set; }

        public Measurement(int contestId, int gameId, int playerId, Sensor sensor, DateTime timestamp, Coordinate coordinate) {
            this.ContestId = contestId;
            this.GameId = gameId;
            this.PlayerId = playerId;
            this.Sensor = sensor;
            this.Timestamp = timestamp;
            this.Coordinate = coordinate;
        }
    }
}

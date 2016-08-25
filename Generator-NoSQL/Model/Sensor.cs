using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSQL.Model {
    public class Sensor {
        public virtual HitType HitType { get; set; }
        public virtual int TopSpin { get; set; }
        public virtual int Power { get; set; }
        public virtual Coordinate Coordinate { get; set; } 

        public Sensor(HitType hitType, int topSpin, int power, Coordinate coordinate) {
            this.HitType = hitType;
            this.TopSpin = topSpin;
            this.Power = power;
            this.Coordinate = coordinate;
        }
    }
}

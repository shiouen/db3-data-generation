using System;

namespace MySQL.Model {
    public class Club {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MembershipNumber { get; set; }
        public int ClubHouseId { get; set; }

        public static Club Generate(int index, int clubHouseId) {
            return new Club {
                Id = index,
                Name = String.Format("club{0}", index),
                MembershipNumber = index,
                ClubHouseId = clubHouseId
            };
        }

        public override string ToString() {
            return String.Format(
                "({0}, '{1}', {2}, {3})",
                this.Id,
                this.Name,
                this.MembershipNumber,
                this.ClubHouseId
            );
        }
    }
}

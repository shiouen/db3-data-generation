using System;

namespace Generator.Model {
    public class Club {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MembershipNumber { get; set; }
        public int ClubHouseId { get; set; }

        public static Club Generate(int clubId, int clubHouseId) {
            return new Club {
                Id = clubId,
                Name = String.Format("club{0}", clubId),
                MembershipNumber = clubId,
                ClubHouseId = clubHouseId
            };
        }

        public override string ToString() {
            string s = "({0}, '{1}', {2}, {3})";
            return String.Format(s, this.Id, this.Name, this.MembershipNumber, this.ClubHouseId);
        }
    }
}

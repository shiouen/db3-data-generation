using System;

namespace MySQL.Model {
    public class Team {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClubId { get; set; }
        public int DepartmentId { get; set; }

        public static Team Generate(int index, int clubId, int departmentId) {
            return new Team {
                Id = index,
                Name = string.Format("team{0}", index),
                ClubId = clubId,
                DepartmentId = departmentId
            };
        }

        public override string ToString() {
            return String.Format(
                "({0}, '{1}', {2}, {3})",
                this.Id,
                this.Name,
                this.ClubId,
                this.DepartmentId
            );
        }
    }
}

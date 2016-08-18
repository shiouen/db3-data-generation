using System;

namespace Generator.Model {
    public class Team {
        public int Id { get; set; }
        public Department Department { get; set; }
        public string Name { get; set; }
        public int ClubId { get; set; }

        public static Team Generate(int index, Department department, string name, int clubId) {
            return new Team {
                Id = index,
                Department = department,
                Name = name,
                ClubId = clubId
            };
        }

        public override string ToString() {
            return String.Format(
                "({0}, '{1}', '{2}', {3})",
                this.Id,
                this.Department.ToString(),
                this.Name,
                this.ClubId
            );
        }
    }
}

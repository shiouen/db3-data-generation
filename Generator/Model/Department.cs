namespace Generator.Model {
    public class Department {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public int SeasonId { get; set; }

        public static Department Generate(int index, string name, string province, int seasonId) {
            return new Department {
                Id = index,
                Name = name,
                Province = province,
                SeasonId = seasonId
            };
        }

        public override string ToString() {
            return string.Format(
                "({0}, '{1}', '{2}', {3})",
                this.Id,
                this.Name,
                this.Province,
                this.SeasonId
            );
        }
    }
}

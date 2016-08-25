using System;

namespace MySQL.Model {
    public class Player {
        public int Id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int MembershipNumber { get; set; }
        public int PostalCode { get; set; }
        public Ranking Ranking { get; set; }
        public string Place { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }

        public static Player Generate(int index, DateTime dateOfBirth, string firstName, string lastName, int postalCode, string place, Ranking ranking, string streetName, int streetNumber) {
            return new Player {
                Id = index,
                DateOfBirth = dateOfBirth,
                FirstName = firstName,
                LastName = lastName,
                MembershipNumber = index,
                PostalCode = postalCode,
                Place = place,
                Ranking = ranking,
                StreetName = streetName,
                StreetNumber = streetNumber
            };
        }

        public override string ToString() {
            return String.Format(
                "({0}, '{1}', {2}, '{3}', '{4}', {5}, '{6}', '{7}', '{8}', {9})",
                this.Id,
                this.DateOfBirth.ToString("yyyy-MM-dd"),
                this.MembershipNumber,
                this.FirstName,
                this.LastName,
                this.PostalCode,
                this.Place,
                this.Ranking.ToString(),
                this.StreetName,
                this.StreetNumber
            );
        }
    }
}

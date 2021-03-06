﻿using System;

using MySQL.Extensions;

namespace MySQL.Model {
    public class ClubHouse {
        public int Id { get; set; }
        public int AmountOfTables { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public int PostalCode { get; set; }
        public string Place { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }

        public static ClubHouse Generate(int index, int postalCode, String place, String streeName, Random random) {
            return new ClubHouse {
                Id = index,
                AmountOfTables = 120,
                Latitude = random.NextLatitude(),
                Longitude = random.NextLongitude(),
                Name = String.Format("club_house{0}", index),
                PostalCode = postalCode,
                Place = place,
                StreetName = streeName,
                StreetNumber = 1
            };
        }

        public override string ToString() {
            return String.Format(
                "({0}, {1}, {2:0.000000}, {3:0.000000}, '{4}', {5}, '{6}', '{7}', {8})",
                this.Id,
                this.AmountOfTables,
                this.Latitude,
                this.Longitude,
                this.Name,
                this.PostalCode,
                this.Place,
                this.StreetName,
                this.StreetNumber
            );
        }
    }
}

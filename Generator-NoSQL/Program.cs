using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using NoSQL.Extensions;
using NoSQL.Model;

namespace NoSQL {
    public class Program {
        public string Path { get; set; }
        public Random Random { get; set; }

        public List<Measurement> Measurements { get; set; }
        
        public Program() {
            this.Random = new Random();

            this.Measurements = new List<Measurement>();
        }

        public Program GenerateDatabase(int contestAmount = 10, int gamePerContestAmount = 16, int measurementsPerGame = 1000) {
            /* order is IMPORTANT */

            // rofl
            //int gameId = 1;

            for (int contestId = 1; contestId <= contestAmount; contestId++) {
                for (int i = 0; i < gamePerContestAmount; i++) {
                    int playerId = this.Random.Next(1, 5);
                    int opponentId = this.Random.Next(5, 9);

                    for (int gameId = 1; gameId <= measurementsPerGame; gameId++) {
                        int currentPlayerId = (this.Random.Next() % 2 == 0) ? playerId : opponentId;

                        HitType hitType = this.Random.NextHitType();
                        int topSpin = this.Random.Next(0, 101);
                        int power = this.Random.Next(0, 501);
                        Coordinate sesnsorCoordinate = this.Random.NextRacketCoordinate();

                        Sensor sensor = new Sensor(hitType, topSpin, power, sesnsorCoordinate);
                        Coordinate measurementCoordinate = this.Random.NextTableCoordinate();

                        Measurement measurement = new Measurement(contestId, gameId, currentPlayerId, sensor, DateTime.Now, measurementCoordinate);

                        this.Measurements.Add(measurement);
                    }
                }
            }

            return this;
        }

        public void WriteJSON(bool cleanUp) {
            string json = this.Measurements.ToJson();

            this.Path = String.Format("{0}../../{1}", AppDomain.CurrentDomain.BaseDirectory, "Files/measurements.json");

            using (StreamWriter writer = new StreamWriter(this.Path)) {
                writer.Write(json);
            }

        }

        public static void Main(string[] args) {
            Stopwatch stopwatch = Stopwatch.StartNew();

            new Program()
                .GenerateDatabase()
                .WriteJSON(cleanUp: false);

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.Read();
        }
    }
}

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NoSQL.Model;

namespace NoSQL.Extensions {
    public static class JsonExtensions {
        public static string ToJson(this List<Measurement> measurements) {
            string json = "";

            using (StringWriter sw = new StringWriter()) {
                using (JsonTextWriter writer = new JsonTextWriter(sw)) {
                    // [
                    writer.WriteStartArray();

                    foreach (Measurement measurement in measurements) {
                        writer.WriteRawValue(measurement.ToJson());
                    }

                    // ]
                    writer.WriteEndArray();
                }

                json = sw.ToString();
            }
        
            return json;
        }

        public static string ToJson(this Measurement measurement) {
            string json = "";

            using (StringWriter sw = new StringWriter()) {
                using (JsonTextWriter writer = new JsonTextWriter(sw)) {
                    // {
                    writer.WriteStartObject();

                    writer.WritePropertyName("contest_id");
                    writer.WriteValue(measurement.ContestId);

                    writer.WritePropertyName("game_id");
                    writer.WriteValue(measurement.GameId);

                    writer.WritePropertyName("player_id");
                    writer.WriteValue(measurement.PlayerId);

                    writer.WritePropertyName("sensor");
                    writer.WriteRawValue(measurement.Sensor.ToJson());

                    writer.WritePropertyName("timestamp");
                    writer.WriteValue(measurement.Timestamp);

                    writer.WritePropertyName("coordinate");
                    writer.WriteRawValue(measurement.Coordinate.ToJson());

                    // }
                    writer.WriteEndObject();
                }

                json = sw.ToString();
            }

            return json;
        }

        public static string ToJson(this Sensor sensor) {
            string json = "";

            using (StringWriter sw = new StringWriter()) {
                using (JsonTextWriter writer = new JsonTextWriter(sw)) {
                    // {
                    writer.WriteStartObject();

                    writer.WritePropertyName("hit_type");
                    writer.WriteValue(sensor.HitType.ToString());

                    writer.WritePropertyName("top_spin");
                    writer.WriteValue(sensor.TopSpin);

                    writer.WritePropertyName("power");
                    writer.WriteValue(sensor.Power);

                    writer.WritePropertyName("coordinate");
                    writer.WriteRawValue(sensor.Coordinate.ToJson());

                    // }
                    writer.WriteEndObject();
                }

                json = sw.ToString();
            }

            return json;
        }

        public static string ToJson(this Coordinate coordinate, bool valuesAsString = false) {
            string json = "";

            using (StringWriter sw = new StringWriter()) {
                using (JsonTextWriter writer = new JsonTextWriter(sw)) {
                    // {
                    writer.WriteStartObject();

                    writer.WritePropertyName("latitude");
                    if (valuesAsString) {
                        writer.WriteValue(string.Format("{0:0.0000000}", coordinate.Latitude));
                    } else {
                        writer.WriteValue(coordinate.Latitude);
                    }

                    writer.WritePropertyName("longitude");
                    if (valuesAsString) {
                        writer.WriteValue(string.Format("{0:0.0000000}", coordinate.Longitude));
                    } else {
                        writer.WriteValue(coordinate.Longitude);
                    }

                    // }
                    writer.WriteEndObject();
                }

                json = sw.ToString();
            }

            return json;
        }
    }
}

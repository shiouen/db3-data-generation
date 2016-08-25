using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySQL.Extensions {
    public static class ListExtensions {
        public static void BuildAsQuery<T>(this ICollection<T> collection, StringBuilder builder, string tableName) {
            if (collection.Count == 0) { return; }

            int id = 1;

            string insert = string.Format("insert into {0} values", tableName);
            builder.AppendLine(insert);

            var lastElement = collection.Last();

            foreach (var element in collection) {
                builder.Append(element.ToString());

                if (element.Equals(lastElement)) {
                    builder.AppendLine(";");
                    builder.AppendLine("commit;");
                } else if (id % 20000 == 0) {
                    builder.AppendLine(";");
                    builder.AppendLine("commit;");
                    builder.AppendLine(insert);
                } else {
                    builder.AppendLine(",");
                }

                ++id;
            }
            builder.AppendLine();
        }
    }
}

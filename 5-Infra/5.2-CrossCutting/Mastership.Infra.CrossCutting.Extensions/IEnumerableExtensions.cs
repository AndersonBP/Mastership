using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Mastership.Infra.CrossCutting.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> data, Action<T> action)
        {
            foreach (T d in data)
                action(d);
        }

        public static byte[] ToCsv<T>(this IEnumerable<T> list, string separator = ";")
        {
            var t = typeof(T);
            var newLine = Environment.NewLine;

            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(mem))
            {
                var obj = Activator.CreateInstance(t);
                var props = obj.GetType().GetProperties();

                writer.Write(string.Join(separator, props.Select(d => d.Name).ToArray()) + newLine);

                if (list != null)
                    foreach (T item in list)
                    {
                        var values = props.Select(x =>
                        {
                            var val = item.GetType().GetProperty(x.Name).GetValue(item, null);
                            return val != null ? val.ToString() : string.Empty;
                        }).ToArray();

                        var row = string.Join(separator, values);
                        writer.Write(row + newLine);
                    }

                writer.Flush();
                return mem.ToArray();
            }
        }

        public static byte[] ToExcel<T>(this IEnumerable<T> collection, string dateTimeFormat = null)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Report");

                var properties = typeof(T).GetProperties()
                    .Where(x => Attribute.IsDefined(x, typeof(DataMemberAttribute)))
                    .OrderBy(x => ((DataMemberAttribute)x
                            .GetCustomAttributes(typeof(DataMemberAttribute), false)
                            .Single()).Order)
                    .ToList();

                properties.AddRange(typeof(T).GetProperties()
                    .Where(x => !Attribute.IsDefined(x, typeof(DataMemberAttribute))));

                worksheet.Cells["A1"].LoadFromCollection(collection, true, TableStyles.None, BindingFlags.Instance | BindingFlags.Public, properties.ToArray());

                var columnIndex = 0;

                var typesToFormatAsDateTime = new Type[] { typeof(DateTime), typeof(DateTime?) };
                foreach (var property in properties)
                {
                    columnIndex++;
                    if (typesToFormatAsDateTime.Contains(property.PropertyType))
                        worksheet.Column(columnIndex).Style.Numberformat.Format = dateTimeFormat ?? "dd/MM/yyyy HH:mm:ss";
                }

                return package.GetAsByteArray();
            }
        }
    }
}

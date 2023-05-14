using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;

namespace Common.Helper
{
    /// <summary>
    ///     Hilfsklasse zum Umgehen mit Csv's.
    /// </summary>
    public class CsvHandlingHelper
    {
        private static readonly CsvConfiguration _configComma = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            NewLine = Environment.NewLine,
            Delimiter = ",",
            HeaderValidated = null
        };

        private static readonly CsvConfiguration _configSemiColon = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            NewLine = Environment.NewLine,
            Delimiter= ";",
            HeaderValidated = null
        };

        /// <summary>
        ///     Lies Daten aus CSV Byte Array.
        /// </summary>
        /// <typeparam name="T">Erwartete Daten.</typeparam>
        /// <param name="bytes">Bytes.</param>
        /// <returns>Ausgelesene Daten.</returns>
        public static IEnumerable<T> GetDataFromCsvByteArray<T>(byte[] bytes)
        {
            if(bytes == null)
            {
                return Enumerable.Empty<T>();
            }

            if(bytes.Length == 0)
            {
                return Enumerable.Empty<T>();
            }


            var list = ReadData<T>(bytes, _configComma);
            
            if (!list.Any())
            {
                list = ReadData<T>(bytes, _configSemiColon);
            }


            return list;
        }

        private static IEnumerable<T> ReadData<T>(byte[] data, CsvConfiguration config)
        {
            var list = Enumerable.Empty<T>();
            var stream = new MemoryStream(data);
            using (var streamreader = new StreamReader(stream,Encoding.Latin1))
            using (var csv = new CsvReader(streamreader, config))
            {
                try
                {
                    list = csv.GetRecords<T>().ToList();
                }
                catch(Exception)
                {
                    return Enumerable.Empty<T>();
                }
            }
            return list;
        }
    }
}

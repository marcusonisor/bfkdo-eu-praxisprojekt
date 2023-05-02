using Common.Helper;
using System.Text;

namespace ProjectUnitTests.HelperTests
{
    public class CsvReaderTests
    {
        [Test]
        public void Test_EmptyIfBytesAreNull()
        {
            var result = CsvHandlingHelper.GetDataFromCsvByteArray<DemoData>(null!);
            Assert.That(result.Count() == 0);
        }

        [Test]
        public void Test_EmptyIfBytesAreEmpty()
        {
            var result = CsvHandlingHelper.GetDataFromCsvByteArray<DemoData>(new byte[0]);
            Assert.That(result.Count() == 0);
        }

        [Test]
        public void Test_EmptyIfInvalidDelimiter()
        {
            var csvstring = "Id_Name\r\n1_one\r\n2_two";
            var bytes = Encoding.UTF8.GetBytes(csvstring);
            var result = CsvHandlingHelper.GetDataFromCsvByteArray<DemoData>(bytes);
            Assert.That(result.Count() == 0);
        }

        [Test]
        public void Test_EmptyIfWrongModel()
        {
            var csvstring = "Id,Name\r\n1,one\r\n2,two";
            var bytes = Encoding.UTF8.GetBytes(csvstring);
            var result = CsvHandlingHelper.GetDataFromCsvByteArray<string>(bytes);
            Assert.That(result.Count() == 0);
        }

        [Test]
        public void Test_ReadDataIfCommaDelimiter()
        {
            var csvstring = "Id,Name\r\n1,one\r\n2,two";
            var bytes = Encoding.UTF8.GetBytes(csvstring);
            var result = CsvHandlingHelper.GetDataFromCsvByteArray<DemoData>(bytes);
            Assert.That(result.Any());
        }

        [Test]
        public void Test_ReadDataIfSemiColonDelimiter()
        {
            var csvstring = "Id;Name\r\n1;one\r\n2;two";
            var bytes = Encoding.UTF8.GetBytes(csvstring);
            var result = CsvHandlingHelper.GetDataFromCsvByteArray<DemoData>(bytes);
            Assert.That(result.Any());
        }
    }

    /// <summary>
    ///     Demo Daten Modell für Test.
    /// </summary>
    public class DemoData
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
    }
}

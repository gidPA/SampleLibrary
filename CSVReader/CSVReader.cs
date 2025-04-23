using Microsoft.VisualBasic.FileIO;

public class CSVReader
{
    public CSVReader(string fileLocation, string[] expectedHeaders)
    {
        if (string.IsNullOrWhiteSpace(fileLocation))
            throw new ArgumentNullException(nameof(expectedHeaders));
        if (expectedHeaders == null)
            throw new ArgumentNullException(nameof(expectedHeaders));

        if (expectedHeaders.Any(h => string.IsNullOrWhiteSpace(h)))
            throw new ArgumentException("ExpectedHeaders must not contain null or whitespace elements.", nameof(expectedHeaders));
        
        FileLocation = fileLocation;
        ExpectedHeaders = (string[])expectedHeaders.Clone();
    }
    private string[] ExpectedHeaders;
    private string FileLocation;

    public IEnumerable<Dictionary<string, string>> ReadCSVFile()
    {
        using (TextFieldParser parser = new TextFieldParser(FileLocation))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(","); // for CSV
            parser.HasFieldsEnclosedInQuotes = true;

            List<Dictionary<string, string>> rows = new();

            string[]? headers = parser.ReadFields();

            if (!headers!.SequenceEqual(ExpectedHeaders))
            {
                throw new InvalidDataException("CSV headers do not match expected schema.");
            }

            while (!parser.EndOfData)
            {
                string[]? fields = parser.ReadFields();

                if (fields!.Any(h => string.IsNullOrWhiteSpace(h)))
                    throw new ArgumentException("Data must not contain null or whitespace elements.", nameof(fields));

                var row = new Dictionary<string, string>();

                for (int i = 0; i < headers!.Length; i++)
                {
                    row[headers[i]] = i < fields!.Length ? fields[i] : "";
                }

                rows.Add(row);
            }

            return rows;
        }
    }
}
using System.Text.RegularExpressions;

string filepath = "files_quarantine.txt";

if (File.Exists(filepath))
{
    string fileContent = File.ReadAllText(filepath);

    var validFiles = fileContent.Trim().Split("\n").Where(isValidChecksum).Select((x,i) => new {
        Position = i + 1,
        Value = x
    });

    foreach (var file in validFiles)
    {
        Console.WriteLine($"Position: {file.Position}, Value: {file.Value}");
    }
}

static bool isValidChecksum(string fileName)
{
    var parts = fileName.Split('-');

    if (parts.Length != 2)
    {
        throw new ArgumentException("Invalid file name format");
    }

    string firstPart = parts[0];
    string checksum = parts[1];

    var nonRepeatedCharacters = firstPart
        .GroupBy(c => c)
        .Where(g => g.Count() == 1)
        .Select(g => g.Key)
        .ToArray();

    string correctChecksum = new(nonRepeatedCharacters);

    return checksum == correctChecksum;
}
string pathToMessageFile = "encryption_policies.txt";

if (File.Exists(pathToMessageFile))
{
    using var fileStream = new FileStream(pathToMessageFile, FileMode.Open);
    using var streamReader = new StreamReader(fileStream);
    var fileContent = streamReader.ReadToEnd();

    var invalidKeys = fileContent.Split('\n').Select(row =>
    {
        string policy = row.Split(':')[0].Trim();
        string key = row.Split(':')[1].Trim();

        IValidationInput validationInput = new ValidationInput
        {
            Min = int.Parse(policy.Split(' ')[0].Split('-')[0]),
            Max = int.Parse(policy.Split(' ')[0].Split('-')[1]),
            Symbol = policy.Split(' ')[1][0],
            Value = key
        };

        return new
        {
            validationInput.Value,
            IsValid = ValidateEncryption(validationInput)
        };
    }).Where(n => n.IsValid == false).Select((x,index) => new {
        Position = index + 1,
        x.Value
    });

    foreach (var invalidKey in invalidKeys)
    {
        Console.WriteLine($"Position: {invalidKey.Position}, Value: {invalidKey.Value}");
    }
}

static bool ValidateEncryption(IValidationInput item)
{
    var quantity = item.Value.Count(x => x == item.Symbol);

    return quantity >= item.Min && quantity <= item.Max;
}

interface IValidationInput
{
    public int Min { get; set; }
    public int Max { get; set; }
    public char Symbol { get; set; }
    public string Value { get; set; }
}

class ValidationInput : IValidationInput
{
    public int Min { get; set; }
    public int Max { get; set; }
    public char Symbol { get; set; }
    public string Value { get; set; } = string.Empty;
}

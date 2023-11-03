string filepath = "message_01.txt";

if (File.Exists(filepath))
{
    string fileContent = File.ReadAllText(filepath);

    var output = fileContent.Trim().ToLower().Split(" ")
    .GroupBy(word => word)
    .Select(group => new {
        Word = group.Key,
        Count = group.Count()
    })
    .ToDictionary(item => item.Word, item => item.Count)
    .Aggregate("",(accum, current) => $"{accum}{current.Key}{current.Value}");

    Console.WriteLine(output);
}
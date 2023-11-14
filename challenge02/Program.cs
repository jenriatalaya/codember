using System.Text;

string pathToMessageFile = "message_02.txt";

if (File.Exists(pathToMessageFile))
{
    using var fileStream = new FileStream(pathToMessageFile, FileMode.Open);
    using var streamReader = new StreamReader(fileStream);
    var fileContent = streamReader.ReadToEnd();

    int lastValue = 0;
    var sb = new StringBuilder();

    foreach (var item in fileContent.ToCharArray())
    {
        if (item == '&')
        {
            sb.Append(lastValue);
        }
        else
        {
            lastValue = RunSymbol(item, lastValue);
        }
    }

    Console.WriteLine(sb.ToString());
}

static int RunSymbol(char symbol, int lastValue)
{
    return symbol switch
    {
        '#' => lastValue + 1,
        '@' => lastValue - 1,
        '*' => lastValue * lastValue,
        _ => 0
    };
}

using System.Text;

Console.OutputEncoding = Encoding.Unicode;
Console.InputEncoding = Encoding.Unicode;

var isFileCreated = true;
const string path = "data";
var keyPath = Path.Combine(path, "key.txt");
var inputPath = Path.Combine(path, "input.txt");
var stepsPath = Path.Combine(path, "steps.txt");
var outputPath = Path.Combine(path, "output.txt");
var alphabetPath = Path.Combine(path, "alphabet.txt");

try
{
  if (!Directory.Exists(path))
  {
    Directory.CreateDirectory(path);
  }

  TryCreateFile(keyPath, "ANGEL");
  TryCreateFile(inputPath, "Ucv rmom zjossax");
  TryCreateFile(stepsPath, "");
  TryCreateFile(outputPath, "");
  TryCreateFile(alphabetPath, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
}
catch (Exception)
{
  Console.WriteLine("File creation error.");
  isFileCreated = false;
}

var alphabet = "";
var lines = new string[0];
var keyLetters = new char[0];

if (isFileCreated)
{
  alphabet = File.ReadAllText(alphabetPath).Trim().ToUpper();
  lines = File.ReadAllLines(inputPath);
  keyLetters = File.ReadAllText(keyPath).Trim().ToUpper().ToCharArray();
}
else
{
  Console.Write("Enter the alphabet: ");
  alphabet = Console.ReadLine()?.Trim().ToUpper() ?? "";
  Console.Write("Enter line for encrypt: ");
  lines = new string[] { Console.ReadLine() ?? "" };
  Console.Write("Enter the key: ");
  keyLetters = Console.ReadLine()?.Trim().ToUpper().ToCharArray() ?? new char[0];
}

var alphabetMap = new Dictionary<char, int>();
for (var i = 0; i < alphabet.Length; i++)
{
  alphabetMap.Add(alphabet[i], i + 1);
  alphabetMap.Add(char.ToLower(alphabet[i]), i + 1);
}
var codeLines = new string[lines.Length];
var resultLines = new string[lines.Length];
var codeId = 0;
var stepsOutput = "";
var resultOutput = "";
for (var i = 0; i < lines.Length; i++)
{
  var inputLine = lines[i];
  var keyLine = new char[inputLine.Length];
  var resultLine = new char[inputLine.Length];

  for (var ii = 0; ii < inputLine.Length; ii++)
  {
    var inputLetter = inputLine[ii];
    if (!alphabetMap.ContainsKey(inputLetter))
    {
      keyLine[ii] = resultLine[ii] = inputLetter;
      stepsOutput += $"{inputLetter}\n";
      continue;
    }

    var keyLetter = char.IsUpper(inputLetter) ? keyLetters[codeId] : char.ToLower(keyLetters[codeId]);
    var resultLetter = setLetter(inputLetter, keyLetter);
    codeId = (codeId + 1) % keyLetters.Length;

    keyLine[ii] = keyLetter;
    resultLine[ii] = resultLetter;

    stepsOutput +=
      $"{inputLetter} + {keyLetter} = {resultLetter} " +
      $"({alphabetMap[inputLetter],2:0} + {alphabetMap[keyLetter],2:0} = {(alphabetMap[inputLetter] + alphabetMap[keyLetter]) % alphabet.Length,3: 0;-0})\n";
  }

  codeLines[i] = new string(keyLine);
  resultLines[i] = new string(resultLine);

  var input = $" input - {inputLine}";
  var key = $"   key - {codeLines[i]}";
  var output = $"output - {resultLines[i]}";
  resultOutput += $"{input}\n";
  resultOutput += $"{key}\n";
  resultOutput += $"{output}\n";
  resultOutput += '\n';

  Console.WriteLine(input);
  Console.ForegroundColor = ConsoleColor.Red;
  Console.WriteLine(key);
  Console.ForegroundColor = ConsoleColor.Green;
  Console.WriteLine(output);
  Console.ResetColor();
}

if (isFileCreated)
{
  File.WriteAllLines(outputPath, resultLines);
  File.WriteAllText(stepsPath, resultOutput + '\n' + stepsOutput);
  var fullPath = Path.GetFullPath(path);
  Console.WriteLine(
    $"Data contains in \"{fullPath}\":\n" +
    $"Input: \n" +
    $" 1. alphabet in \"{Path.GetFullPath(alphabetPath)}\"\n" +
    $" 2.    input in \"{Path.GetFullPath(inputPath)}\"\n" +
    $" 3.      key in \"{Path.GetFullPath(keyPath)}\"\n" +
    $"Output: \n" +
    $" 1.   output in \"{Path.GetFullPath(outputPath)}\"\n" +
    $" 2.    steps in \"{Path.GetFullPath(stepsPath)}\"\n"
  );
}

Console.ReadLine();

char setLetter(char inputLetter, char keyLetter)
{
  var isUpper = char.IsUpper(inputLetter);
  var letterNumber = (alphabetMap[inputLetter] + alphabetMap[keyLetter]) % alphabet.Length;
  var letter =
    letterNumber > 0 ?
    alphabet[letterNumber - 1] :
    alphabet[alphabet.Length + letterNumber - 1];
  return isUpper ? letter : char.ToLower(letter);
}

void TryCreateFile(string path, string defaultContent)
{
  if (File.Exists(path)) return;

  using (var fs = File.Create(path))
  {
    var value = defaultContent.ToCharArray();
    fs.Write(Encoding.UTF8.GetBytes(value), 0, value.Length);
  }
}
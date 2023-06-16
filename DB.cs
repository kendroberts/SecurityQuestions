using System.Text.Json;

namespace SecurityQuestions;

/// <summary>
/// Loads/Stores answers in DB.json file, located in same directory as .exe.
/// Creates if needed.
/// </summary>
public class DB
{
    private readonly Dictionary<string, List<Answer>> _names;

    private const string _fileName = "DB.json";

    public DB()
    {
        Dictionary<string, List<Answer>>? names = null;
        try
        {
            var options = new JsonSerializerOptions();
            options.IncludeFields = true;
            names = JsonSerializer.Deserialize<Dictionary<string, List<Answer>>>(File.ReadAllText(_fileName), options);
        }
        catch (FileNotFoundException)
        {
            //normal
        }
        catch (Exception e)
        {
            Program.Log(e.ToString());
        }
        _names = names ?? new Dictionary<string, List<Answer>>();
    }

    public List<Answer>? Get(string name)
    {
        if (_names.TryGetValue(name, out var answers))
        {
            return answers;
        }
        return null;
    }

    public void Save(string name, List<Answer> answers)
    {
        _names[name] = answers; //replaces if exists

        try
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.IncludeFields = true;
            File.WriteAllText(_fileName, JsonSerializer.Serialize(_names, options));
        }
        catch (Exception e)
        {
            Program.Log(e.ToString());
        }
    }
}

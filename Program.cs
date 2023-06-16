namespace SecurityQuestions;

internal static class Program
{
    private static void Main()
    {
        try
        {
            new SecurityQuestions().Run();
        }
        catch (Exception e)
        {
            Log(e.ToString());
        }
    }

    public static void Log(string message)
    {
        try
        {
            File.AppendAllText("Log.txt", message + System.Environment.NewLine);
        }
        catch (Exception)
        {
            //eat
        }
    }
}
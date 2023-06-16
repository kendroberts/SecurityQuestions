namespace SecurityQuestions;

/// <summary>
/// NOTE: we store answers in plain text.  Possibly we should encrypt them with a one way hash.
/// NOTE: only 1 instance of this exe should be run at a time (they would erase each others answers.)
/// </summary>
public class SecurityQuestions
{
    //NOTE: a question's array index will be saved in DB.json, 
    //so you can add questions to the end but don't remove/change questions.
    private readonly string[] _questions = new[] {
        "In what city were you born? ",              //0
        "What is the name of your favorite pet? ",   //1
        "What is your mother's maiden name? ",       //2
        "What high school did you attend? ",         //3
        "What was the mascot of your high school? ", //4
        "What was the make of your first car? ",     //5
        "What was your favorite toy as a child? ",   //6
        "Where did you meet your spouse? ",          //7
        "What is your favorite meal? ",              //8
        "Who is your favorite actor / actress? "     //9
    };

    /// <summary>
    /// Never ending loop prompting user for name.
    /// If name's first time, prompts for the initial answers to 3 security questions.
    /// Otherwise asks them to re-answer 1 of the 3 previously entered questions correctly.
    /// If they don't want to do that, then let them give new answers to 3 questions.
    /// </summary>
    public void Run()
    {
        var db = new DB();

        while (true)
        {
            var name = PromptForName();
            var answers = db.Get(name);
            if (answers != null)
            {
                if (YesNo("Do you want to answer a security question?"))
                {
                    QuizAnswers(answers);
                }
                else
                {
                    //re-ask to get initial below
                    answers = null;
                }
            }

            if (answers == null && YesNo("Would you like to store answers to security questions?"))
            {
                answers = GetInitialAnswers();
                //Note how there is no way to "remove" your answers without answering 3 more questions
                if (answers != null)
                {
                    db.Save(name, answers);
                }
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Won't return without a name.
    /// Name will be trimmed and lowercase.
    /// </summary>
    private static string PromptForName()
    {
        string? name;
        do
        {
            Console.Write("Hi, what is your name? ");
            name = Console.ReadLine()?.Trim().ToLower();
        } while (string.IsNullOrEmpty(name));
        return name;
    }

    /// <summary>
    /// Loops through _questions and prompts them for answers.
    /// Out of 10 total questions the user has to choose 3.
    /// Answers will be trimmed and lowercase.
    /// NOTE: we just run through the questions 1 time.
    /// If they fail to answer 3 we return null.
    /// Maybe we should instead keep cycling through unanswered questions?
    /// Maybe we should tell them we are not saving their answers?
    /// </summary>
    /// <returns>null or list of 3 answers</returns>
    private List<Answer>? GetInitialAnswers()
    {
        var answers = new List<Answer>();
        var count = 0;
        for (var questionID = 0; questionID < _questions.Length; questionID++)
        {
            Console.Write(_questions[questionID]);
            //making answers lower case so we can quiz them ignoring case.
            var answer = Console.ReadLine()?.Trim().ToLower();
            if (answer?.Length > 0)
            {
                answers.Add(new Answer(questionID, answer));
                count++;
                if (count == 3)
                {
                    return answers;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Presents questions until either the user runs out of questions or answers one correctly
    /// </summary>
    private void QuizAnswers(List<Answer> answers)
    {
        foreach (var answer in answers)
        {
            if (answer.QuestionID < _questions.Length)
            {
                Console.Write(_questions[answer.QuestionID]);
                var value = Console.ReadLine()?.Trim().ToLower();
                if (value == answer.Value)
                {
                    Console.WriteLine("Congratulations!");
                    return;
                }
            }
            else
            {
                Program.Log("Bad QuestionID: " + answer.QuestionID);
            }
        }
        Console.WriteLine("Fail - ran out of questions.");
    }

    /// <summary>
    /// Shows prompt and then waits for them to press Y or N.
    /// </summary>
    /// <returns>True if Y is the first non-modifier key they press, otherwise false.</returns>
    private static bool YesNo(string prompt)
    {
        Console.Write(prompt + " ");
        var keyInfo = Console.ReadKey();
        Console.WriteLine();
        return keyInfo.Key == ConsoleKey.Y;
    }
}

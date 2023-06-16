namespace SecurityQuestions;

public class Answer
{
    public int QuestionID;
    public string Value;

    public Answer(int questionID, string value)
    {
        QuestionID = questionID;
        Value = value;
    }
}

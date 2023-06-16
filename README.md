# SecurityQuestions

An exploration of Ken Roberts' coding acumen.

A C# console application.

To use, build solution and run.

## Examples

If your first time you are prompted with up to 10 questions until you answer 3
```console
Hi, what is your name? Mary
Would you like to store answers to security questions? y
In what city were you born?
What is the name of your favorite pet? Spot
What is your mother's maiden name?
What high school did you attend? East Meck
What was the mascot of your high school? Eagle

Hi, what is your name?
```
Your second time you are prompted to re-enter your responses.

After the first correct response you are congratulated.  It is not case sensitive.
```console
Hi, what is your name? Mary
Do you want to answer a security question? y
What is the name of your favorite pet? I forget
What high school did you attend?
What was the mascot of your high school? eagle
Congratulations!

Hi, what is your name?
```
If you fail you are scolded
```console
Hi, what is your name? Mary
Do you want to answer a security question? y
What is the name of your favorite pet?
What high school did you attend?
What was the mascot of your high school?
Fail - ran out of questions.

Hi, what is your name?
```
You can re-enter your responses by answering 'n' to 'Do you want...'
Notice that when you don't answer 3 of the 10 questions it does not save your responses and it does not warn you.
```console
Hi, what is your name? Mary
Do you want to answer a security question? n
Would you like to store answers to security questions? y
In what city were you born? Gotham
What is the name of your favorite pet? Rex
What is your mother's maiden name?
What high school did you attend?
What was the mascot of your high school?
What was the make of your first car?
What was your favorite toy as a child?
Where did you meet your spouse?
What is your favorite meal?
Who is your favorite actor / actress?

Hi, what is your name? Mary
Do you want to answer a security question? y
What is the name of your favorite pet? Rex
What high school did you attend?
What was the mascot of your high school? eagle
Congratulations!

Hi, what is your name?
```
The system will handle multiple names.

The system saves responses between runs to the file DB.json.

The system logs errors to Log.txt

## Remarks
We store answers in plain text.  Possibly we should encrypt them with a one way hash.

Only 1 instance of this exe should be run at a time (or they will erase each other's answers.)

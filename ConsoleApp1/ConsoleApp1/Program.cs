// See https://aka.ms/new-console-template for more information
/******************************************************************************

Assignment No.1

*******************************************************************************/
using System;


public class Number
{
    public int value;
    public void GenerateTable()
    {
        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine($"{value} multiplied by {i} = {value * i}");
        }
    }
    public void EvenOddChecker()
    {
        if (value % 2 == 0)
        {
            Console.WriteLine($"The number {value} is even");
        }
        else
        {
            Console.WriteLine($"The number {value} is odd");
        }
    }
    public static float MaxOfThree(float a, float b, float c)
    {
        float max = 0;
        if (a >= b)
        {
            if (a >= c)
            {
                max = a;
            }
            else
            {
                max = c;
            }

        }
        if (b >= a)
        {
            if (b >= c)
            {
                max = b;
            }
            else
            {
                max = c;
            }
        }
        return max;
    }
    public static int SumOfNumbers(int N)
    {
        int sum = 0;
        for (int i = 1; i <= N; i++)
        {
            sum += i;
        }
        return sum;
    }
    public static int ReverseTheNumber(int num)
    {
        string Numstr = num.ToString();
        string reversed = "";
        for (int i = Numstr.Length - 1; i >= 0; i--)
        {
            reversed += Numstr[i];
        }
        return Convert.ToInt32(reversed);
    }
    public static int factorial(int number)
    {
        int f = 1;
        for (int n = number; n >= 1; n--)
        {
            f = f * n;
        }
        return f;
    }

    public static bool LeapYearChecker(int year)
    {
        bool leap = false;
        if (year % 4 == 0)
        {
            if (year % 100 == 0 && year % 400 != 0)
            {
                leap = false;
            }
            else
            {
                leap = true;
            }
        }
        return leap;
    }
    public int adder(int a, int b)
    {
        return a + b;
    }
    public static void FibonacciSequence(int N)
    {
        int[] sequence = { 0, 1 };
        int term = 0;
        for (int i = 0; i <= N; i++)
        {
            Console.WriteLine($"{sequence[0]}");
            term = sequence[0] + sequence[1];
            sequence[0] = sequence[1];
            sequence[1] = term;
        }

    }
    public static bool PrimeChecker(int num)
    {
        bool status = true;
        if (num <= 1)
        {
            status = false;
        }
        for (int i = 2; i < num; i++)
        {
            if (num % i == 0)
            {
                status = false;
            }
        }
        return status;
    }
    public static int GCDFinder(int num1, int num2)
    {
        int limit;
        int GCD = 0;
        if (num1 >= num2)
        {
            limit = num1;
        }
        else
        {
            limit = num2;
        }
        for (int i = 1; i < limit; i++)
        {
            if (num1 % i == 0 && num2 % i == 0)
            {
                GCD = i;
            }
        }
        return GCD;
    }

    public static void SimpleCalculator()
    {
        double num1, num2, result = 0;
        char operation;

        // Input two numbers
        Console.Write("Enter first number: ");
        num1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter second number: ");
        num2 = Convert.ToDouble(Console.ReadLine());

        // Input operation
        Console.Write("Enter operation (+, -, *, /, %): ");
        operation = Convert.ToChar(Console.ReadLine());

        // Switch case to perform operation
        switch (operation)
        {
            case '+':
                result = num1 + num2;
                Console.WriteLine("Result = " + result);
                break;

            case '-':
                result = num1 - num2;
                Console.WriteLine("Result = " + result);
                break;

            case '*':
                result = num1 * num2;
                Console.WriteLine("Result = " + result);
                break;

            case '/':
                if (num2 != 0)
                {
                    result = num1 / num2;
                    Console.WriteLine("Result = " + result);
                }
                else
                {
                    Console.WriteLine("Error: Division by zero.");
                }
                break;

            case '%':
                if (num2 != 0)
                {
                    result = num1 % num2;
                    Console.WriteLine("Result = " + result);
                }
                else
                {
                    Console.WriteLine("Error: Modulus by zero.");
                }
                break;

            default:
                Console.WriteLine("Invalid operation.");
                break;
        }
    }
    public static int DigitCounter(int num)
    {
        return num.ToString().Length;
    }
    public static bool PalindromeNumCheck(int num)
    {
        string original = num.ToString();
        string reversed = "";
        for (int i = original.Length - 1; i >= 0; i--)
        {
            reversed += original[i].ToString();
        }
        return original == reversed;
    }
    public static int SumOfDigits(int num)
    {
        string strNum = num.ToString();
        int sum = 0;
        for (int i = 0; i < strNum.Length; i++)
        {
            sum += Convert.ToInt32(strNum[i].ToString());
        }
        return sum;
    }
    public static bool checkArmstrong(int num)
    {
        string strNum = num.ToString();
        int NumLen = strNum.Length;
        double Armstrong = Math.Pow(Convert.ToInt32(strNum[0].ToString()), NumLen) + Math.Pow(Convert.ToInt32(strNum[1].ToString()), NumLen) + Math.Pow(Convert.ToInt32(strNum[2].ToString()), NumLen);
        return Armstrong == num;

    }

    public static (double max, double min) FindMaxMin(double[] arr)
    {
        double min = double.PositiveInfinity;
        double max = double.NegativeInfinity;
        for (int i = 0; i < arr.Length; i++)
        {
            if (min >= arr[i])
            {
                min = arr[i];
            }
            if (max <= arr[i])
            {
                max = arr[i];
            }
        }
        return (max, min);
    }
    public static int LinearSearch(int x, int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (x == arr[i])
            {
                return i;
            }
        }
        return -1;
    }
    public static void EvenOddCounter(int[] arr)
    {
        int even = 0, odd = 0;
        foreach (int num in arr)
        {
            if (num % 2 == 0)
                even++;
            else
                odd++;
        }
        Console.WriteLine($"Even: {even}, Odd: {odd}");
    }


}
public class Program
{
    public static void SortNames(string[] names)
    {
        Array.Sort(names);
        foreach (string name in names)
        {
            Console.WriteLine(name);
        }
    }


    public static void FrequencyCounter(int[] arr)
    {
        Dictionary<int, int> freq = new Dictionary<int, int>();
        foreach (int num in arr)
        {
            if (freq.ContainsKey(num))
                freq[num]++;
            else
                freq[num] = 1;
        }

        foreach (var kvp in freq)
        {
            Console.WriteLine($"{kvp.Key} -> {kvp.Value} times");
        }
    }

    public static int[,] AddMatrices(int[,] A, int[,] B)
    {
        int rows = A.GetLength(0);
        int cols = A.GetLength(1);
        int[,] result = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = A[i, j] + B[i, j];
            }
        }
        return result;
    }
    public static int CountVowels(string sentence)
    {
        int count = 0;
        foreach (char c in sentence.ToLower())
        {
            if ("aeiou".Contains(c))
                count++;
        }
        return count;
    }
    public static bool IsStringPalindrome(string input)
    {
        input = input.ToLower().Replace(" ", "");  // ignores case and spaces
        string reversed = "";

        for (int i = input.Length - 1; i >= 0; i--)
        {
            reversed += input[i];
        }

        return input == reversed;
    }
    public static string ReverseWords(string sentence)
    {
        string[] words = sentence.Split(' ');
        Array.Reverse(words);
        return string.Join(" ", words);
    }
    public static List<int> RemoveDuplicates(int[] arr)
    {
        HashSet<int> unique = new HashSet<int>(arr);
        return unique.ToList();
    }

    public static void WordFrequency(string paragraph)
    {
        string[] words = paragraph.ToLower().Split(new[] { ' ', '.', ',', '?', '!', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
        Dictionary<string, int> freq = new Dictionary<string, int>();

        foreach (var word in words)
        {
            if (freq.ContainsKey(word))
                freq[word]++;
            else
                freq[word] = 1;
        }

        foreach (var kvp in freq)
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
    }


    public static string GeneratePassword(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random rand = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[rand.Next(s.Length)]).ToArray());
    }



}

public class StudentMarksManager
{
    static Dictionary<string, int> marks = new Dictionary<string, int>();

    public static void AddStudent(string name, int mark)
    {
        marks[name] = mark;
        Console.WriteLine($"Added {name} with marks {mark}.");
    }

    public static void SearchStudent(string name)
    {
        if (marks.ContainsKey(name))
            Console.WriteLine($"{name} has {marks[name]} marks.");
        else
            Console.WriteLine($"{name} not found.");
    }

    public static void UpdateMarks(string name, int newMark)
    {
        if (marks.ContainsKey(name))
        {
            marks[name] = newMark;
            Console.WriteLine($"{name}'s marks updated to {newMark}.");
        }
        else
            Console.WriteLine($"{name} not found.");
    }
}

public class Patient
{
    public string Name;
    public string VisitReason;

    public Patient(string name, string reason)
    {
        Name = name;
        VisitReason = reason;
    }

    static List<Patient> patientList = new List<Patient>();

    public static void Add(string name, string reason)
    {
        patientList.Add(new Patient(name, reason));
        Console.WriteLine("Patient added.");
    }

    public static void Search(string name)
    {
        foreach (var p in patientList)
        {
            if (p.Name == name)
            {
                Console.WriteLine("Found: " + p.Name + " - " + p.VisitReason);
                return;
            }
        }
        Console.WriteLine("Patient not found.");
    }

    public static void Update(string name, string newReason)
    {
        foreach (var p in patientList)
        {
            if (p.Name == name)
            {
                p.VisitReason = newReason;
                Console.WriteLine("Visit reason updated.");
                return;
            }
        }
        Console.WriteLine("Patient not found.");
    }

    public static void Delete(string name)
    {
        for (int i = 0; i < patientList.Count; i++)
        {
            if (patientList[i].Name == name)
            {
                patientList.RemoveAt(i);
                Console.WriteLine("Patient deleted.");
                return;
            }
        }
        Console.WriteLine("Patient not found.");
    }
}




public class MainClass
{
    static void Main()
    {
        Number number = new Number();
        Console.WriteLine("Please Enter any number");
        number.value = Convert.ToInt32(Console.ReadLine());

        // 1- use the function |GenerateTable| to generate the table
        Console.WriteLine("1- Multiplication Table Generator: Input a number and print its multiplication table up to 10 using loops.");
        number.GenerateTable();


        // 2- use the function |EvenOddChecker| from the Class Number
        Console.WriteLine("2- The Program of Even Odd Checker");
        number.EvenOddChecker();


        // 3- use the function |MaxOfThree| from the class Number
        Console.WriteLine("3- The Program for finding the maximum of three numbers");
        float max_num = Number.MaxOfThree(45, 32, 102);
        Console.WriteLine($"The max number is: {max_num}");

        // 4- Sum of N numbers use |SumOfNumbers()| function from the Number class
        Console.WriteLine("4- The Program for finding the sum of first N");
        int sumof_N = Number.SumOfNumbers(10);
        Console.WriteLine($"Sum of {10} numbers is: {sumof_N}");

        // 5- Reverse the Number
        Console.WriteLine("5- The Program for rversing the number");
        int reversed_number = Number.ReverseTheNumber(125678);
        Console.WriteLine($"The reversed number is: {reversed_number}");

        // 6- Use the |factorial| function from the Number class
        Console.WriteLine("6- The Program for finding the factorial");
        int fac = Number.factorial(5);
        Console.WriteLine($"The factorial of number is: {fac}");

        // 7- leap year calculator code: -
        Console.WriteLine("7- leap year calculator code");
        Console.WriteLine(Number.LeapYearChecker(2002));

        // 7- Fibonacci sequence
        Console.WriteLine("8- Fibonacci Sequence");
        Console.WriteLine("Fibonacci Sequence: -");
        Number.FibonacciSequence(10);

        // 9- Prime Number Checker code;
        Console.WriteLine("Prime Number Checker: -");
        bool isPrime = Number.PrimeChecker(4);
        Console.WriteLine($"The isPrime status is: {isPrime}");

        // 10- GCD GCDFinder
        Console.WriteLine("10- GCD Finder: -");
        int GCD = Number.GCDFinder(18, 48);
        Console.WriteLine($"GCD= {GCD}");

        // 11- Simple calculator
        Console.WriteLine("11- Simple Calculator: -");
        Number.SimpleCalculator();

        // 12- Digit Counter
        Console.WriteLine("12- Digit COunter: -");
        int count = Number.DigitCounter(1000230);
        Console.WriteLine($"No. of Digits= {count}");

        // 13- Palindrome Number Checker
        Console.WriteLine("13- Palindrome Num: -");
        bool isPalindrome = Number.PalindromeNumCheck(1244321);
        Console.WriteLine($"isPalindrome= {isPalindrome}");

        // 14- Sum of Digits
        Console.WriteLine("14- Sum of Digits: -");
        int sod = Number.SumOfDigits(23332);
        Console.WriteLine($"Sum of Digits= {sod}");

        // 15- Armstrong Number
        Console.WriteLine("15- Armstrong code: -");
        bool isArmStrong = Number.checkArmstrong(1523);
        Console.WriteLine($"isArmStrong= {isArmStrong}");

        // 16- max and min in an array
        Console.WriteLine("16- Max and Min in Array");
        double[] arr = { -13, 20, 340, 3, 100, 32 };
        var result = Number.FindMaxMin(arr);
        Console.WriteLine($"Max= {result.max} Min= {result.min}");

        // 17- Linear Array Search
        Console.WriteLine("17- Linear Array Search");
        int[] search_arr = { 1, 8, 9, 2, 10 };
        int x = 2;
        int index = Number.LinearSearch(x, search_arr);
        Console.WriteLine($"{x} is found at index {index}");

        // 18- Sorting an Array
        Console.WriteLine("18- Sorting an array");
        int[] sample_array = { 1, 43, 2, 5, 11, 32 };
        Array.Sort(sample_array);
        for (int i = 0; i < sample_array.Length; i++)
        {
            Console.WriteLine($"{sample_array[i]}");
        }

        // 19- Even Odd Counter Code. It will print the number of even and odd numbers in the array
        Console.WriteLine("19- Even Odd Counter");
        Number.EvenOddCounter(sample_array);

        // 20- Sorting the Names in Alphabetic Order
        Console.WriteLine("20- Sorting Names Alphabeticaly");
        string[] names = { "Ali", "Sharjeel", "Tayyaba", "Abida" };
        Program.SortNames(names);

        // 21- FrequencyCounter Function counts the repition of elements of an array
        Console.WriteLine("21- Frequency counter in array");
        int[] myArr = { 11, 1, 2, 3, 3, 3, 5, 5, 7, 9, 9, 7 };
        Program.FrequencyCounter(myArr);

        // 22- Adding two matrices and showing the result
        Console.WriteLine("22- Addition of Matrices");
        int[,] A = { { 1, 3, 4, 5 }, { 2, 4, 6, 8 }, { 3, 2, 34, 5 } };
        int[,] B = { { 2, 3, 4, 5 }, { 1, 2, 3, 4 }, { 7, 8, 9, 9 } };
        Program.AddMatrices(A, B);

        // 23- Counting Vowels in a sentence
        Console.WriteLine("23- Counting Vowels");
        string sentence_with_vowels = "I am a good Boy. I am taking the Bootcamp at CureMD";
        int vowel_count = Program.CountVowels(sentence_with_vowels);
        Console.WriteLine($"The number of vowels in the sentence are: {vowel_count}");

        // 24- Checking a Palindrome string
        Console.WriteLine("24- Palindrom String");
        string palindromeStr = "race car";
        bool isPalindromeStr = Program.IsStringPalindrome(palindromeStr);
        Console.WriteLine($"isPalindrome status of the string: {isPalindromeStr}");

        // 25- Reverse Words in a string
        Console.WriteLine("25- Reversing words in a string");
        string original_str = "Hi I am here!";
        string word_reversed_str = Program.ReverseWords(original_str);
        Console.WriteLine($"The word-reversed version of the original string is: {word_reversed_str}");

        // 26- Remove reptitions from an array using Hash<set> in C#
        int[] arr_with_duplicates = { 2, 2, 3, 44, 44, 21 };
        List<int> list_without_duplicates = Program.RemoveDuplicates(arr_with_duplicates);
        Console.WriteLine("The following list is without duplicates: -");
        foreach (int item in list_without_duplicates)
        {
            Console.WriteLine(item);
        }
        // 27- Students Marks Manager: Adding Searching and finding students: -
        Console.WriteLine("27- Student Mangeer System");
        StudentMarksManager.AddStudent("Ali", 88);
        StudentMarksManager.AddStudent("Sara", 92);
        StudentMarksManager.SearchStudent("Ali");
        StudentMarksManager.UpdateMarks("Ali", 95);
        StudentMarksManager.SearchStudent("Ali");

        // 28- Patient Visit Console App
        Console.WriteLine("28- Patient Console App");
        Patient.Add("Ali", "Fever");
        Patient.Add("Sara", "Cough");
        Patient.Search("Ali");
        Patient.Update("Ali", "Headache");
        Patient.Delete("Sara");
        Patient.Search("Sara");

        // 29- Word counter in a paragraph
        Console.WriteLine("29- Word Counter: -");
        string paragraph = "Hello there! I am a student at NUST and I am very delighted to meet you all. I just want to say Hello!";
        Program.WordFrequency(paragraph);

        // 30- Random Password Generator
        Console.WriteLine("30- Random Password genrator");
        string random_password = Program.GeneratePassword(10);
        Console.WriteLine($"The random password is: {random_password}");





    }

}
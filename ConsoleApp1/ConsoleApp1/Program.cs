// See https://aka.ms/new-console-template for more information
// program no.1 
using System.ComponentModel.DataAnnotations;

public class Number
{
    public int value;
    public void GenerateTable()
    {
        for(int i=1; i<=10; i++)
        {
            Console.WriteLine($"{value} multiplied by {i} = {value*i}");
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
        float max=0;
        if( a >= b)
        {
            if (a >= c)
            {
               max=a;
            }
            else
            {
                max=c;
            }
            
        }
        if (b >= a)
        {
            if (b >= c)
            {
                max=b;
            }
            else
            {
                max=c;
            }
        }
        return max;
    }
    public static int SumOfNumbers(int N)
    {
        int sum=0;
        for (int i=0; i<=N;)
        return sum;
    }
}
public class MainClass
{
    static void Main()
    {
        Number number=new Number();
        Console.WriteLine("Please Enter any number");
        string num = Console.ReadLine();
        number.value = Convert.ToInt32(num);
        
        // use the function |GenerateTable| to generate the table
        number.GenerateTable();
        // use the function |EvenOddChecker| from the Class Number
        number.EvenOddChecker();
        // use the function |MaxOfThree| from the class Number
        float max_num = Number.MaxOfThree(45, 32, 102);
        Console.WriteLine($"The max number is: {max_num}");
    }
}
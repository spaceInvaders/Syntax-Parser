﻿using System;

namespace Syntax_Pars
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Enter your phrase for calculation: ");
            string input = Console.ReadLine();
            if (String.IsNullOrEmpty(input))
            {
                Console.WriteLine("pointer should move to the next string");
                #warning: Put event here;
            }
            else
            {
                Node<CalculationElement> myNode = input.GrowNodeTree();
                if (myNode != null)
                {
                    try
                    {
                        decimal result = myNode.Calculate();
                        Console.WriteLine(result);
                    }
                    catch (DivideByZeroException)
                    {
                        Console.WriteLine("Error: Divide by Zero");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Calculation failed");
                    }
                }
            }
        }
    }
}
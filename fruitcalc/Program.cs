using System;
using System.Collections.Generic;

/*
 * 

Question:  Is "Input :" part of the actual input?

Input : Oranges – $10; Apples- $5 ; Promotions: No; Basket: Oranges - 5, Apples 1

Output: Total price= 55

Example 2:

Input : Oranges – $10; Apples- $5 ; Promotions: Oranges – 0.5;  Basket: Oranges - 5, Apples 1

Output: Total price= 30
*/

namespace fruitcalc
{
    class Program
    {
        
        static void Main(string[] args)
        {
            fruitcalc.store.StoreFront storeFront = new store.StoreFront();
            //Assuming "Input :" is not intended to be part of the input string, properties for this project has debugger settings with 2 inputs wrapped in quotes 
            if(args == null || args.Length == 0){
                args = new[] { "Oranges – $10; Apples- $5 ; Promotions: No; Basket: Oranges - 5, Apples 1" };
            }

            foreach(string arg in args)
            {
                storeFront.ParseArgs(arg);
            }

            Console.WriteLine("Press any key to close...");
            Console.Read();

        }

    }
}

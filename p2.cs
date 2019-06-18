// Author: Jeremiah Kalmus
// File: p2.cs
// Date: April 17th, 2019
// Version: 1.0

/*
 * The flow of the driver class is to first test the range class and then test the multiQ class. I start by allocating an array
 * that holds address space for 10 range objects in an array called range_array followed by the allocation of an array that holds
 * address space for 4 multiQ objects in an array called multiQ_array. The thought was to create 10 range objects for adequate
 * testing of the range class, however, I chose to only make 4 multiQ objects since they were more complicated classes that 
 * perform many operations. I have fewer multiQ objects since each object would be put through more tests than the range objects and
 * the testing for each would be more involved. Therefore I deemed it unneccessary to test more than 4 object. 
 * ----------------------------------------------------------------------------------------------------------------------------------
 * RANGE TESTS:
 * 
 * Initialize_RangeObj():
 *      
 *      This method initializes the range_array by inserting range objects into it. The first 3 objects were initialized without any
 *      input parameters to test the constructer in its deafult state without user input. The next 4 objects were initialized with
 *      only one factor value passed in. The last 3 objects were initialized with two factor values passed in by the user. This was
 *      done since it is uncertain what the user would decide to do. The factor values used to input were created using a rendom number
 *      generator with values ranging from 1 to 30. The values must be non-negative integers, therefore I entered only positive numbers.
 *      Zero is not included since in the range class it was not recommended to use the value zero. I make sure to output what the 
 *      factor values that enter the range objects are. 
 *      
 *  Test_Ping():
 *  
 *      This method tests the Ping() method. I tested each of the 10 objects in the range_array with 3 ping values that were all
 *      randomized by the random number generator. I output the values entering the ping function and whether or not it was a
 *      successful ping. The values entered ranged anywhere from 1 to 1000 giving a large range that the client was most likely
 *      to choose from.
 *      
 *  Show_Range_Stats():
 *      
 *      This method simply displays all the statistics that are kept track of in the range objects. These stats include Max, Min,
 *      Mean, and Ping_Count. I insert two values into the Ping() method into object 0 that I know are pinged values essentially
 *      to ensure that at least one range object has stats that are not all zeroes.
 * 
 * ----------------------------------------------------------------------------------------------------------------------------------
 * MULTIQ TESTS:
 * 
 * 
 * 
 * 
 * ----------------------------------------------------------------------------------------------------------------------------------
 */

using System;
class p2
{
    const uint RANGEOBJSIZE = 10;
    const uint MULTIQDEFAULTTABLESIZE = 20;
    const uint MULTIQDELETETESTSIZE = 12;
    const uint MULTIQOBJSIZE = 4;
    const uint RANGEOBJTHIRD = 3;
    const uint RANGEOBJTWOTHIRDS = 7;
    const uint DEFAULTFACTORS = 2;
    static Random rand = new Random();
    static void Initialize_RangeObj(range[] range_array)
    {
        Console.WriteLine("INITIALIZING AN ARRAY OF RANGE OBJECTS FOR TESTING");
        Console.WriteLine();

        uint random_value1;
        uint random_value2;

        for (uint i = 0; i < RANGEOBJSIZE; i++)
        {
            random_value1 = (uint)rand.Next(1, 20);
            random_value2 = (uint)rand.Next(1, 30);

            if (i < RANGEOBJTHIRD)
            {
                range_array[i] = new range();
                Console.WriteLine("Factor values entered into range object " + i + " is: " + DEFAULTFACTORS + " and " + RANGEOBJTHIRD);
            }
            else if (RANGEOBJTHIRD <= i  && i < RANGEOBJTWOTHIRDS)
            {
                range_array[i] = new range(random_value1);
                Console.WriteLine("Factor values entered into range object " + i + " is: " + random_value1 + " and " + RANGEOBJTHIRD);
            }
            else
            {
                range_array[i] = new range(random_value1, random_value2);
                Console.WriteLine("Factor values entered into range object " + i + " is: " + random_value1 + " and " + random_value2);
            }

        }
        Console.WriteLine();
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }
    static void Test_Ping(range[] range_array)
    {
        Console.WriteLine("ENTERING PING TEST VALUES");
        Console.WriteLine();

        bool ping_return;
        uint random;

        for (uint i = 0; i < RANGEOBJSIZE; i++)
        {
            for (uint j = 0; j < RANGEOBJTHIRD; j++)
            {
                random = (uint)rand.Next(1, 1000);
                ping_return = range_array[i].Ping(random);
                Console.WriteLine("Ping test value for range object " + i + " is: " + random);
                Console.WriteLine("Was it a Ping?: " + ping_return);
            }
            Console.WriteLine();
        }
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }
    static void Show_Range_Stats(range[] range_array)
    {
        Console.WriteLine("DISPLAYING ACCUMULATED STATS FROM RANGE OBJECTS");
        Console.WriteLine();
        range_array[0].Ping(MULTIQDELETETESTSIZE);
        range_array[0].Ping(MULTIQDELETETESTSIZE*RANGEOBJSIZE);
        Console.WriteLine("Passed in " + MULTIQDELETETESTSIZE + " & " + MULTIQDELETETESTSIZE * RANGEOBJSIZE +
            " to object 0 as ping values");
        Console.WriteLine();

        for (uint i = 0; i < RANGEOBJSIZE; i++)
        {
            Console.WriteLine("Range object " + i + " largest successful ping: " + range_array[i].Max);
            Console.WriteLine("Range object " + i + " smallest successful ping: " + range_array[i].Min);
            Console.WriteLine("Range object " + i + " number of successful pings: " + range_array[i].Ping_Num);
            Console.WriteLine("Range object " + i + " average successful ping value: " + range_array[i].Mean);
            Console.WriteLine();
        }
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }
    static void Initialize_MultiQObj(multiQ[] multiQ_array)
    {
        Console.WriteLine("INITIALIZING AN ARRAY OF MULTIQ OBJECTS FOR TESTING");
        Console.WriteLine();

        uint random_value;

        for (uint i = 0; i < MULTIQOBJSIZE; i++)
        {
            random_value = (uint)rand.Next(10, 50);

            if (i < DEFAULTFACTORS)
            {
                multiQ_array[i] = new multiQ();
                Console.WriteLine("Table size in multiQ object " + i + " is: " + MULTIQDEFAULTTABLESIZE);
            }
            else
            {
                multiQ_array[i] = new multiQ();
                Console.WriteLine("Table size in multiQ object " + i + " is: " + random_value);
            }

        }
        Console.WriteLine();
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }
    static void Test_Add(multiQ[] multiQ_array)
    {
        Console.WriteLine("TESTING ADD FOR MULTIQ FACTOR OBJECTS");
        Console.WriteLine();

        uint random_value;
        uint loop_iterations;

        for (uint i = 0; i < MULTIQOBJSIZE; i++)
        {
            Console.WriteLine("Number of factor values held in object " + i + " is: " + multiQ_array[i].Factor_Count);
            loop_iterations = (uint)rand.Next(2, 10);
            for (int j = 0; j < loop_iterations; j++)
            {
                random_value = (uint)rand.Next(2, 20);
                multiQ_array[i].Add(random_value);
                Console.WriteLine("Factor value added into multiQ object " + i + " is: " + random_value);
            }

            Console.WriteLine("Number of factor values held in object " + i + " is: " + multiQ_array[i].Factor_Count);
            Console.WriteLine();
        }
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }
    static void Test_Query(multiQ[] multiQ_array)
    {
        Console.WriteLine("TESTING QUERY FOR MULTIQ FACTOR OBJECTS");
        Console.WriteLine();

        uint random_value;
        for (uint i = 0; i < MULTIQOBJSIZE; i++)
        {
            random_value = (uint)rand.Next(20, 100);
            Console.WriteLine("Value used for query for multiQ obj " + i + ": " + random_value);
            Console.WriteLine("Number of factors for queried number: " + multiQ_array[i].Query(random_value));
            Console.WriteLine();
        }
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }
    static void Test_Active(multiQ[] multiQ_array)
    {
        Console.WriteLine("TESTING ACTIVE STATUS FOR MULTIQ FACTOR OBJECTS");
        Console.WriteLine();

        uint query_return;
        uint random_value = (uint)rand.Next(1, 10);
        for (uint i = 0; i < MULTIQOBJSIZE; i++)
        {
            Console.WriteLine("Is multiQ object " + i + " active?: " + multiQ_array[i].isActive());
            for (uint j = 0; j < MULTIQOBJSIZE; j++)
            {
                query_return = multiQ_array[i].Query(random_value);
                Console.WriteLine("Queried " + random_value + " to MultiQ object " + i);
                Console.WriteLine("Is multiQ object " + i + " active?: " + query_return);
                Console.WriteLine("Is multiQ object " + i + " active?: " + multiQ_array[i].isActive());
            }
            Console.WriteLine();
        }
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }
    static void Test_Reset(multiQ[] multiQ_array)
    {
        Console.WriteLine("TESTING RESET FOR MULTIQ FACTOR OBJECTS");
        Console.WriteLine();

        for (uint i = 0; i < MULTIQOBJSIZE; i++)
        {
            Console.WriteLine("MultiQ object " + i + " is active?: " + multiQ_array[i].isActive());
            Console.WriteLine("MultiQ object " + i + " has " + multiQ_array[i].Factor_Count + " stored values");
            Console.WriteLine("Resetting object " + i);
            multiQ_array[i].Reset();
            Console.WriteLine("MultiQ object " + i + " is active?: " + multiQ_array[i].isActive());
            Console.WriteLine("MultiQ object " + i + " has " + multiQ_array[i].Factor_Count + " stored values");
            Console.WriteLine();
        }
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }
    static void Test_Full(multiQ[] multiQ_array)
    {
        Console.WriteLine("TESTING ISFULL FOR MULTIQ FACTOR OBJECTS");
        Console.WriteLine();

        uint random_value;
        for (uint i = 0; i < MULTIQDEFAULTTABLESIZE; i++)
        {
            random_value = (uint)rand.Next(20, 100);
            if (!multiQ_array[0].isFull)
            {
                multiQ_array[0].Add(random_value);
                Console.WriteLine("Adding value " + random_value + " to MultiQ object 0 table");
            }
            else
            {
                Console.WriteLine("MultiQ object 0 is full!");
            }
        }
        Console.WriteLine();
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }
    static void Test_Delete(multiQ[] multiQ_array)
    {
        Console.WriteLine("TESTING DELETE FOR MULTIQ FACTOR OBJECTS");
        Console.WriteLine("WE SHOULD EXPECT FOR EACH OBJECT, THE SAME AMOUNT OF ITEMS DELETED AS ADDED");
        Console.WriteLine();
        for (uint i = 0; i < MULTIQOBJSIZE; i++)
        {
            Console.WriteLine("Number of items in table is: " + multiQ_array[i].Factor_Count);
            for (uint j = 0; j < MULTIQDELETETESTSIZE; j++)
            {
                if (multiQ_array[i].Factor_Count != 0)
                {
                    multiQ_array[i].Delete();
                    Console.WriteLine("Deleted item from MultiQ");
                    Console.WriteLine("Number of items in table is: " + multiQ_array[i].Factor_Count);
                }
                else
                {
                    Console.WriteLine("The MultiQ is empty!");
                }
            }

            Console.WriteLine();
        }
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }
    static void Show_MultiQ_Stats(multiQ[] multiQ_array)
    {
        Console.WriteLine("DISPLAYING ACCUMULATED STATS FROM MULTIQ OBJECTS");
        Console.WriteLine();

        for (uint i = 0; i < MULTIQOBJSIZE; i++)
        {
            Console.WriteLine("MultiQ object " + i + " most successful active query: " + multiQ_array[i].Max);
            Console.WriteLine("MultiQ object " + i + " least successful active query: " + multiQ_array[i].Min);
            Console.WriteLine("MultiQ object " + i + " number of active queries: " + multiQ_array[i].Query_Count);
            Console.WriteLine("MultiQ object " + i + " average successful query value: " + multiQ_array[i].Mean);
            Console.WriteLine("MultiQ object " + i + " number of resets: " + multiQ_array[i].Reset_Count);
            Console.WriteLine();
        }
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }
    static void Main(string[] args)
    {
        range[] range_array = new range[RANGEOBJSIZE];
        multiQ[] multiQ_array = new multiQ[MULTIQOBJSIZE];

        Initialize_RangeObj(range_array);
        Test_Ping(range_array);
        Show_Range_Stats(range_array);

        Initialize_MultiQObj(multiQ_array);
        Test_Add(multiQ_array);
        Test_Query(multiQ_array);
        Test_Active(multiQ_array);
        Test_Reset(multiQ_array);
        Test_Add(multiQ_array);
        Test_Full(multiQ_array);
        Test_Query(multiQ_array);
        Test_Delete(multiQ_array);
        Show_MultiQ_Stats(multiQ_array);

        Console.WriteLine("Press enter to end program...");
        Console.ReadLine();
    }
}


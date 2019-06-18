// Author: Jeremiah Kalmus
// File: range.cs
// Date: April 17th, 2019
// Version: 1.0


/* 
 * OVERVIEW:
 *  The range class initializes with two factor objects. Zero, one, or two of these can be passed in by the client. These factors are 
 *  then compared to a pinged value that the client enters into Ping(). This pinged value will be compared to the two factor objects
 *  and checked to see if it is a ping. A Ping only occurs when both factor objects are factors of the pinged value entered by the
 *  client. For example, the two factor objects have factor values of 2 and 3 and the client passes in a pinged value of 3. This is
 *  not a ping, however, the pinged value of 6 is a ping.
 * ---------------------------------------------------------------------------------------------------------------------------------
 * DESIGN DECISIONS AND ASSUMPTIONS:
 *  1 - Client must enter non-negative numbers for the factor objects upon construction, and for the pinged value.
 *  
 *  2 - Reset() is called during every Ping() the client does to ensure all range objects remain active indefinitely.
 * ---------------------------------------------------------------------------------------------------------------------------------
 * INTERFACE INVARIANTS:
 *  1 - range objects are always active.
 *  
 *  2 - Client should not pass in more than 2 factor values into the constructor. These values are to be positive numbers and 
 *      preferably not zero since zero is a factor to all other numbers.
 *  
 *  3 - Ping():
 *  
 *      The client can input a non-negative number and it will compare that input value to the factor object values. This will return
 *      whether or not the value was a ping. An input value is a ping if it is a multiple of both factor values.
 *      
 *  4 - Statistics - Max, Min, Mean, & Ping_Num:
 *  
 *      The statistics have been implemented such that the client can access the information via an accessor at any point. They may use
 *      Max to determine the largest value that was a ping. Min will determine the smallest value that was a ping. Mean will return the
 *      average of all the values that were succefful pings. Ping_Num will return the number of pings that have occured. All the stats
 *      will be 0 if the object never receives a ping value from the client or if there are no successful pings.
 * ---------------------------------------------------------------------------------------------------------------------------------
 * IMPLEMENTATION INVARIANTS:
 *  1 - All objects are kept active internally by calling reset after every pinged value comparison.
 *  
 *  2 - The factor objects are initialized either through client input, or through the default values already assigned.
 *  
 *  3 - The Ping() method calls methods Compare_Factor1, Compare_Factor2, and isPing to determine if the input value was a ping.
 * ---------------------------------------------------------------------------------------------------------------------------------
 * CLASS INVARIANTS:
 *  1 - Constructor initializes two factor objects with factor values as well as all other data members to keep track
 *      of statistics.
 * ---------------------------------------------------------------------------------------------------------------------------------
 */
class range
{
    const uint DEFAULTMIN = 1000;
    private factor factorObj1 = new factor();
    private factor factorObj2 = new factor();
    private uint max;
    private uint min;
    private uint mean;
    private uint sum_of_pings;
    private uint num_of_pings;
    public range(uint initial_val1 = 2, uint initial_val2 = 3)
    {
        factorObj1 = new factor(initial_val1);
        factorObj2 = new factor(initial_val2);
        max = 0;
        min = DEFAULTMIN;
        mean = 0;
        sum_of_pings = 0;
        num_of_pings = 0;
    }
    public uint Max
    {
        get
        {
            return max;
        }
    }
    //POST: Data member min may be set to 0.
    public uint Min
    {
        get
        {
            if (min == DEFAULTMIN)
            {
                min = 0;
            }
            return min;
        }
    }
    public uint Ping_Num
    {
        get
        {
            return num_of_pings;
        }
    }
    //POST: data member mean will be calculated in accessor.
    public uint Mean
    {
        get
        {
            if (num_of_pings != 0)
            {
                mean = sum_of_pings / num_of_pings;
            }
            return mean;
        }
    }
    //PRE: Ping must be provided with a non-negative input.
    public bool Ping(uint ping_value)
    {
        int result1 = Compare_Factor1(ping_value);
        int result2 = Compare_Factor2(ping_value);
        return isPing(result1, result2, ping_value);
    }
    //PRE: Input must be non-negative integer
    private int Compare_Factor1(uint ping_value)
    {
        int comparison_result = factorObj1.Factor_Comparison(ping_value);
        factorObj1.Reset();
        return comparison_result;
    }
    //PRE: Input must be non-negative integer
    private int Compare_Factor2(uint ping_value)
    {
        int comparison_result = factorObj2.Factor_Comparison(ping_value);
        factorObj2.Reset();
        return comparison_result;
    }
    //PRE: Must receive results of Compare_Factor 1 and 2 from Ping(), and ping_value must be non-negative.
    //POST: max, min, sum_of_pings, and num_of_pings may change.
    private bool isPing(int result1, int result2, uint ping_value)
    {
        if (result1 == 1 && result2 == 1)
        {
            if (ping_value > max)
            {
                max = ping_value;
            }
            if (ping_value < min)
            {
                min = ping_value;
            }
            sum_of_pings = sum_of_pings + ping_value;
            num_of_pings++;
            return true;
        }
        else
        {
            return false;
       
		}
}


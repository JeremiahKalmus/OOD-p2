// Author: Jeremiah Kalmus
// File: multiQ.cs
// Date: April 17th, 2019
// Version: 1.0

/* 
 * OVERVIEW:
 *  MultiQ.cs takes values in from the client and creates an array of factor
 *  objectsfrom the size gievn by the client. The client can add and remove 
 *  factor values which will result in factor objects being created and removed
 *  from the multiQ object.The client can enter a value into 
 *  Query() and find out how many factor objects are a factor to the queried value.
 *  An example being, if a multiQ object held an array with factor objects of values
 *  2, 3, and 5, and 6 was passes into Query(), the result of that method would return
 *  2. If 11 was passed into Query() it would return 0.
 * ---------------------------------------------------------------------------------------------------------------------------------
 * DESIGN DECISIONS AND ASSUMPTIONS:
 *  1 - Statistics that are kept track of include the largest successful query, the
 *      smallest successful query, the number of times the multiQ object was queried,
 *      the average of amount of factors returned from successful queries, and the
 *      number of times the object has been reset.
 *      
 *  2 - Active status of multiQ objects depend on the active status of the encapsulated
 *      array of factor objects. If any factor object becomes unactive, the entire multiQ
 *      object then becomes inactive until someone resets the multiQ object.
 *      
 *  3 - Can only use unsigned integers for adding a factor value and for Query since factor.cs
 *      can only take in positive integers.
 *      
 *  4 - A choice was to prohibit the user from handling the factor objects themselves, therefore, 
 *      the multiQ class deals with the memory management of the factor objects. The client is only
 *      concerned about the factor values themselves. 
 *      
 *  5 - The constructors will create an array to hold factor objects between the sizes of 10 and 50.
 *      The user will decide the size as long as the size remains within those bounds, if they 
 *      exceed those bounds then the array will be set to a default size of 20.
 * ---------------------------------------------------------------------------------------------------------------------------------
 * INTERFACE INVARIANTS:
 *  1 - MultiQ objects remain active for as long as the client does not Query() the same value twice in succession.
 *  
 *  2 - Reset():
 *  
 *      At any time, the client can Reset() a multiQ object and it will reset the object to the state it was in
 *      immediately after construction.
 *      
 *  3 - isFull:
 *  
 *      At any time, the client can check to see if the array of factors is full using isFull. 
 *      
 *  4 - Factor_Count:
 *  
 *      At any time, the client can check to see how many factor objects are within the multiQ storage array by using Factor_Count.  
 *      
 *  5 - Query:
 *  
 *      When using Query() the client must pass in a positive integer. Query() will only work effectively if the state of the object
 *      is active. It will compare the input value (queried value) with the array of holding factor objects. It will compare to see
 *      how many of the factor values are factors of the queried value. Query() will return the number of values that are factors 
 *      of the queried value.
 *  
 *  6 - Statistics - Max, Min, Mean, Query_Count, Reset_Count:
 *  
 *      The user can access any statistic value they would like to know by the means of its accessor. The stats are kept track only
 *      when the object is active. Stats include the Max which returns the largest number of factors of any single Query(). Min, which
 *      returns the smallest number of factors of any single Query(). Mean, which returns the average number of factors that return from
 *      a Query() call. Query_Count which returns how many times Query() has been called while the object was active. Lastly, there is
 *      Reset_Count which returns the number of times an object has been reset. Stats will be all 0 if there were no queries or if the
 *      queries resulted in the queried values having no factors in the internal factor object array.
 *      
 *  7 - isActive:
 *  
 *      The client can check at any time if an object is active or inactive. They should use the isActive Accessor.
 *      
 *  8 - Add():
 *  
 *      This method allows the client to add a positive factor value to the back of the array held by a multiQ object. When adding 
 *      a factor value to the array, the client must be mindful if they are adding to a full array for Add() will not add a 
 *      value to the array if it is full, it will simply ignore the request. Recommended that 0 is not entered since 0 is a factor
 *      to every other number.
 *      
 *  9 - Delete():
 *  
 *      This method allows the client to delete a factor value from the front of the array held by a multiQ object. 
 *      From this, the client is responsible for checking whether the array is empty. The client cannot Delete() from an empty factor 
 *      array since Delete() will simply ignore the request.
 * ---------------------------------------------------------------------------------------------------------------------------------
 * IMPLEMENTATION INVARIANTS:
 *  1 - Using an array to contain factor objects with factor values. The client will not have to deal with resource management since
 *      they will only pass in the factor values. The multiQ class will handle instantiating an array of factor objects, each holding
 *      a factor value that the client has entered using Add().
 *      
 *  2 - Add() and Query() only accept unsigned integers.
 *  
 *  3 - Object is deemed inactive if any of the factor objects it holds in the array is inactive.
 *  
 * ---------------------------------------------------------------------------------------------------------------------------------
 * CLASS INVARIANTS:
 *  1 - The constructor will create an array size between 10 and 50 to hold factor objects. The constructor will recieve a size
 *      of array from the client and create an array filled with empty factor objects. The private data members tracking statistics
 *      will be initialized and the size of the array holding factor objects from that point onward cannot change.
 *      
 *  2 - Object is deemed inactive if any of the factor objects it holds in the array is inactive.
 * ---------------------------------------------------------------------------------------------------------------------------------
 */
class multiQ
{
    private const uint DEFAULTSIZE = 20;
    private const uint DEFAULTMIN = 1000;
    private factor[] factor_objArray;
    private uint index;
    private readonly uint max_array_size;
    private bool full;
    private bool active;
    private uint max;
    private uint min;
    private uint mean;
    private uint sum_of_queries;
    private uint num_of_queries;
    private uint num_of_resets;
    public multiQ(uint array_size = DEFAULTSIZE)
    {
        if (array_size < 10 || array_size > 50)
        {
            max_array_size = DEFAULTSIZE;
        }
        else
        {
            max_array_size = array_size;
        }
        factor_objArray = new factor[max_array_size];
        index = 0;
        full = false;
        active = true;
        max = 0;
        min = DEFAULTMIN;
        mean = 0; ;
        sum_of_queries = 0;
        num_of_queries = 0;
        num_of_resets = 0;
}
    public bool isFull
    {
        get
        {
            return full;
        }
    }
    public uint Factor_Count
    {
        get
        {
            return index;
        }
    }
    public uint Max
    {
        get
        {
            return max;
        }
    }
    // POST: data member min may change to 0
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
    //POST: data member mean will be calculated within the accessor and change value.
    public uint Mean
    {
        get
        {
            if (num_of_queries != 0)
            {
                mean = sum_of_queries / num_of_queries;
            }
            return mean;
        }
    }
    public uint Query_Count
    {
        get
        {
            return num_of_queries;
        }
    }
    public uint Reset_Count
    {
        get
        {
            return num_of_resets;
        }
    }
    //PRE: Add will work properly if factor_value is a positive number greater than 0 and the object is active.
    //POST: May add a factor object to the array, increase the index of the array, and 
    //      may also set the array status to full.
    public void Add(uint factor_value)
    {
        if (isActive())
        {
            if (factor_value == 0)
            {
                return;
            }
            if (index < max_array_size)
            {
                factor_objArray[index] = new factor(factor_value);
                index++;
                if (index == max_array_size)
                {
                    full = true;
                }
            }
        }
    }
    //POST: Will remove a factor object from the array and decrement the index if active.
    public void Delete()
    {
        if (isActive())
        {
            if (index == 1)
            {
                factor_objArray[index - 1] = null;
                index--;
            }
            else if (index > 1)
            {
                for (uint i = 1; i < index; i++)
                {
                    factor_objArray[i - 1] = factor_objArray[i];
                }
                factor_objArray[index - 1] = null;
                index--;
            }
        }
    }
    public bool isActive()
    {
        return active;
    }
    //POST: All elements of the factor object array will be returned to null. All data members will be reset to default state.
    public void Reset()
    {
        for (uint i = 0; i < max_array_size; i++)
        {
            factor_objArray[i] = null;
        }
        index = 0;
        full = false;
        active = true;
        max = 0;
        min = DEFAULTMIN;
        mean = 0; ;
        sum_of_queries = 0;
        num_of_queries = 0;
        num_of_resets++;
    }
    //PRE: Value passed in from client must be a non-negative integer.
    //POST: data mambers max, min, sum_of_queries, and active may change.
    public uint Query(uint comparison_value)
    {
        int compare_result = 0;
        uint multiple_counter = 0;
        if (active)
        {
            num_of_queries++;
            for (uint i = 0; i < index; i++)
            {
                compare_result = factor_objArray[i].Factor_Comparison(comparison_value);
                if (compare_result == 1)
                {
                    multiple_counter++;
                }
                if (compare_result == -1)
                {
                    active = false;
                }
            }
            if (multiple_counter > max)
            {
                max = multiple_counter;
            }
            if (multiple_counter < min)
            {
                min = multiple_counter;
            }
            sum_of_queries = sum_of_queries + multiple_counter;
        }
        return multiple_counter;
    }
}


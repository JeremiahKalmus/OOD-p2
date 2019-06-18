// Author: Jeremiah Kalmus
// File: factor.cs
// Date: April 17th, 2019
// Version: 2.0

/* OVERVIEW:
 *  factor.cs is to create an object which contains a factor value and to have the
 *  client compare other values to it to see if the value entered is a multiple of 
 *  the factor objects factor value. 
 * ---------------------------------------------------------------------------------------------------------------------------------
 * DESIGN DECISIONS AND ASSUMPTIONS:
 *  1 - The factor class will only work with positive integers since the 
 *  
 *  2 - accessor isActive is given so that the client can check whether the
 *      object is active or not. Factor_Comparison() will do nothing but return -1
 *      if the object is inactive so it is up to the client to check to ensure the
 *      state is active.
 * ---------------------------------------------------------------------------------------------------------------------------------
 * INTERFACE INVARIANTS:
 *  1 - Construction:
 *  
 *      The client can pass in a positive integer for the factor value
 *      or they can choose to not pass in any factor value and a default factor
 *      value will be set for them. Client is advised not to enter a 0 for the
 *      factor value. 0 can cause problems since it is a factor of every number
 *      and there is no protection against entering it in the factor.cs.
 *  
 *  2 - Factor_Comparison():
 *  
 *      Factor objects will only operate as intended if they are in an active state.
 *      If the client passes in 2 of the same values into the Factor_Comparison()
 *      method, then the object will become inactive. If the client wishes to restore
 *      the object to an active state, they must call the Reset() method. The only
 *      values that can be passed into Factor_Comparison() are positive integers.
 *      Factor_Comparison() will take in a positive integer and determine if the 
 *      input value from the client is a multiple of the factor stored in the object.
 *      If the object is inactive, the Factor_Comparison() will not operate and merely
 *      return -1.
 *      
 *  3 - Reset():
 *      
 *      Reset() can be called when the object is active or inactive and it will return
 *      the object back to its original state immediately after construction. All private
 *      data members will be reset except for the factor number. Once set, the factor
 *      number can not be changed.
 * 
 *  4 - Accessors - isActive & Counter:
 *  
 *      Counter, when called, will provide the client with the amount of multiples the
 *      factor object has processed. The isActive accessor will allow the client to check
 *      if the object is currently active or not.
 * ---------------------------------------------------------------------------------------------------------------------------------     
 * IMPLEMENTATION INVARIANTS:
 *  1 - factor.cs is only supported to handle factor values and comparison values that are
 *      unsigned integers.
 *    
 *  2 - The Factor_Comparison() method will return 1 if the value compared to the factor value
 *      is a multiple, it will return 0 if the value compared is not a multiple of the factor
 *      value, and it will return -1 if the object is inactive.
 * 
 * ---------------------------------------------------------------------------------------------------------------------------------
 * CLASS INVARIANTS:
 *  1 - After every construction, a factor value is set in the object, the status of the object
 *      is set to active and all data members associated are intialized.
 *      The client can pass in a positive integer for the factor value
 *      or they can choose to not pass in any factor value and a default factor
 *      value will be set for them. The factor value cannot be changed once set.
 *      
 *  2 - Factor object is set to inactive if the same value is passed in twice to the 
 *      Factor_Comparison() method.
 * ---------------------------------------------------------------------------------------------------------------------------------
 */

class factor
{
    private const uint DEFAULTFACTOR = 4;
    private readonly uint factor_number;
    private bool active;
    private uint multiple_counter;
    private uint previous_input;
    
    public factor()
    {
        factor_number = DEFAULTFACTOR;
        active = true;
        multiple_counter = 0;
        previous_input = 0;
    }

    public factor(uint user_input)
    {
        factor_number = user_input;
        active = true;
        multiple_counter = 0;
        previous_input = 0;
    }

    public uint Counter
    {
        get
        {
            return multiple_counter;
        }
    }
    public bool isActive
    {
        get
        {
            return active;
        }
    }
    // PRE: To operate effectively, the object must be in an active state.
    // POST: The previous_input may change and the state of the object may become inactive.
    public int Factor_Comparison(uint comparison_value)
    {
        if (active)
        {
            if ((comparison_value % factor_number == 0) && (previous_input != comparison_value))
            {
                multiple_counter++;
                previous_input = comparison_value;
                return 1;
            }
            else if ((comparison_value % factor_number != 0) && (previous_input != comparison_value))
            {
                previous_input = comparison_value;
                return 0;
            }
            else
            {
                active = false;
                return -1;
            }
        }
        return -1;
    }
    // POST: Multiple_Counter and previous_input are returned to their default state
    //       upon intialization and the state of the object is set to active.
    public void Reset()
    {
        multiple_counter = 0;
        previous_input = 0;
        active = true;
    }
}


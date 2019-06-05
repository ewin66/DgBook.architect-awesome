using System;
using System.Collections.Generic;
using System.Text;

namespace RQCore.Restaurants
{

    #region Coo_LaoMeat

    public class Coo_LaoMeat :Sauce,Ingredients
    {

        //Meat m_Meat = new Pork();
        Meat m_Beef = new Beef();
        //Vegetable m_Vegetablet = new Pear();
        Vegetable m_Pineapple = new Pineapple();
    }

    public interface Sauce
    {
    }
    public interface Ingredients
    {

    }


    #endregion


    #region Vegetable



    public class Vegetable
    { 
        //Tomato, Pear, Peach, Pineapple
        public void Washing() { }//煎炸
    }

    public class Pear : Vegetable
    { }
    public class Pineapple : Vegetable
    { }

    #endregion 


    #region Meat

    public class Meat
    {
       public void Boiled() { }//焯水
        public void Frying() { }//煎炸
        //Beef =Pork chicken

    }
    public class Beef:Meat
    { }
    public class Pork : Meat
    { }
    public class Chicken : Meat
    { } 
    #endregion
}

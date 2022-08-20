using System;

namespace Fork
{
    public class CateogryModel : CategoryViewModel
    {
        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static CateogryModel Instance => new();


        public CateogryModel() : base()
        {
            Random rand = new Random();

            int number = rand.Next(0,2);
            if (number == 0)
            {
                Name = "Indian";
                RGB = "#ee8f16";
            }
            else if (number == 1)
            {
                Name = "Korean";
                RGB = "#2259f6";
            }
            else if (number == 2)
            {
                Name = "Mexican";
                RGB = "#ebba10";
            }


        }
    }
}

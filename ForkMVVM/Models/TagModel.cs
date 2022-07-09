using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fork
{
    public class TagModel : TagViewModel
    {
        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static TagModel Instance => new TagModel();


        public TagModel()
        {
            Random rand = new Random();

            int number = rand.Next(0,2);
            if (number == 0)
            {
                Tag = "Indian";
                RGB = "#ee8f16";
            }
            else if (number == 1)
            {
                Tag = "Korean";
                RGB = "#2259f6";
            }
            else if (number == 2)
            {
                Tag = "Mexican";
                RGB = " #ebba10 ";
            }


        }
    }
}

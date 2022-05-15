using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class TagDesignModel : TagViewModel
    {
        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static TagDesignModel Instance => new TagDesignModel();


        public TagDesignModel()
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

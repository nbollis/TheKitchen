﻿using System;
using System.Linq;
using ForkDataHandling;

namespace Fork
{
    public class CategoryModel : CategoryViewModel
    {
        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static CategoryModel Instance => new CategoryModel();


        public CategoryModel() : base(ForkGlobalData.AllCategories.First())
        {

        }
    }
}

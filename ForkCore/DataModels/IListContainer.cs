﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkCore
{
    public interface IListContainer
    {
        void ListItemSelected(object obj, ListItemSelectedEventArgs e);

        void ListItemSelected(object obj);
    }
}

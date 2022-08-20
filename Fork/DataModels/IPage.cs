using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fork
{
    public interface IPage
    {
        public ApplicationPage ApplicationPage { get; }
        public void OnClosing();
    }
}

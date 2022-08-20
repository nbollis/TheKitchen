using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fork
{
    public class ApplicationModel : ApplicationViewModel
    {
        public static ApplicationModel Instance => new ApplicationModel();

        public ApplicationModel() : base()
        {

        }
    }
}

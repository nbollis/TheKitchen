using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fork
{
    public class TechniquesPageViewModel : BaseViewModel, IPage
    {
        #region Private Members

        #endregion

        #region Public Properties
        public ApplicationPage ApplicationPage => ApplicationPage.Techniques;

        #endregion

        #region Commands

        #endregion

        #region Constructor

        #endregion

        #region Helpers
        public void OnClosing()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

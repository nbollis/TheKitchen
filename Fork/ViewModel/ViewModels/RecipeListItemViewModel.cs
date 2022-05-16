using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fork
{
    public class RecipeListItemViewModel : BaseViewModel
    {
        #region Public Properties
        public string Name { get; set; } 
        public ObservableCollection<TagViewModel> Tags { get; set; }

        //public picture Picture
        public bool IsSelected { get; set; }   
        public bool NewContentAvailable { get; set; }

        #endregion

        #region Public Commands

        #endregion

        #region Constructor

        public RecipeListItemViewModel()
        {
            Tags = new ObservableCollection<TagViewModel>();
        }

        #endregion

        #region Command Methods 

        #endregion
    }

}

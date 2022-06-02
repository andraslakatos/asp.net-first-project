using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Desktop.ViewModel
{
    public class NewItemViewModel : ViewModelBase
    {
        private int _id;
        public int Id
        {
            get { return _id;}
            set { _id = value; OnPropertyChanged(); }
        }

        private string _name;
        public string Name
        {
            get { return _name;}
            set { _name = value; OnPropertyChanged(); }
        }
        
        private int _price;
        public int Price
        {
            get { return _price;}
            set { _price = value; OnPropertyChanged(); }
        }

        private string? _description;
        public string Description
        {
            get { return _description;}
            set { _description = value; OnPropertyChanged(); }
        }

        private bool _spicy;
        public bool Spicy
        {
            get { return _spicy;}
            set { _spicy = value; OnPropertyChanged(); }
        }

        private bool _vegan;
        public bool Vegan
        {
            get { return _vegan;}
            set { _vegan = value; OnPropertyChanged(); }
        }

        private int _orderedCount;
        public int OrderedCount
        {
            get { return _orderedCount;}
            set { _orderedCount = value; OnPropertyChanged(); }
        }

        private CategoryDTO _category;
        public CategoryDTO Category
        {
            get { return _category;}
            set { _category = value; OnPropertyChanged();
            }
        }

        private int _caregoryId;

        public int CategoryId
        {
            get { return _caregoryId; }
            set { _caregoryId = value; OnPropertyChanged(); }
        }
    }
}

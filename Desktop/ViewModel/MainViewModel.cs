using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Printing;
using System.Runtime.InteropServices;
using Desktop.Model;
using Data;

namespace Desktop.ViewModel
{
    public enum FilterModeEnum
    {
        Completed,
        NotCompleted,
        All
    }

    public class MainViewModel : ViewModelBase
    {
        private readonly FoodOrderApiService _service;
        private ObservableCollection<OrderDTO> _orders;
        private ObservableCollection<CategoryDTO> _categories;
        private ObservableCollection<CartItemDTO> _cartItems;
        private OrderDTO _selectedOrder;
        private ItemDTO _currentItem;
        private FilterModeEnum _filterMode;
        private string _filterName;
        private string _filterAddress;
        private NewItemViewModel _newItem;

        public ObservableCollection<OrderDTO> Orders
        {
            get { return _orders; }
            set { _orders = value; OnPropertyChanged(); }
        }

        public ObservableCollection<CategoryDTO> Categories
        {
            get { return _categories; }
            set { _categories = value; OnPropertyChanged(); }
        }

        public ObservableCollection<CartItemDTO> CartItems
        {
            get { return _cartItems; }
            set { _cartItems = value; OnPropertyChanged(); }
        }

        public OrderDTO SelectedOrder
        {
            get { return _selectedOrder; }
            set { _selectedOrder = value; OnPropertyChanged(); }
        }

        public ItemDTO CurrentItem
        {
            get { return _currentItem; }
            set { _currentItem = value; OnPropertyChanged(); }
        }

        public FilterModeEnum FilterMode { get; set; } = FilterModeEnum.All;

        public string FilterName
        {
            get { return _filterName; }
            set { _filterName = value; OnPropertyChanged(); }
        }

        public string FilterAddress
        {
            get { return _filterAddress; }
            set { _filterAddress = value; OnPropertyChanged(); }
        }

        public NewItemViewModel NewItem
        {
            get { return _newItem; }
            set { _newItem = value; OnPropertyChanged(); }
        }

        public DelegateCommand RefreshCommand { get; private set; }
        public DelegateCommand SelectCommand { get; private set; }
        public DelegateCommand FilterCommand { get; private set; }
        public DelegateCommand FilterModeCommand { get; private set; }
        public DelegateCommand CompleteOrderCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }
        public DelegateCommand NewItemCommand { get; private set; }
        public DelegateCommand CancelNewItemCommand { get; private set; }
        public DelegateCommand SaveNewItemCommand { get; private set; }

        public event EventHandler LogoutSucceeded;
        public event EventHandler FinishingNewItem;
        public event EventHandler StartingNewItem;
        public MainViewModel(FoodOrderApiService service)
        {
            _service = service;
            RefreshCommand = new DelegateCommand(_ => RefreshAsync());
            SelectCommand = new DelegateCommand(_ => LoadCartItemsAsync(SelectedOrder));
            FilterCommand = new DelegateCommand(_ => FilterButton_Clicked());
            FilterModeCommand = new DelegateCommand(param => FilterModeChanged(param));
            CompleteOrderCommand = new DelegateCommand(_ => CompleteOrder());
            NewItemCommand = new DelegateCommand(_ => StartNewItem());
            SaveNewItemCommand = new DelegateCommand(_ => SaveNewItem(NewItem));
            CancelNewItemCommand = new DelegateCommand(_ => CancelNewItem());
            LogoutCommand = new DelegateCommand(_ => LogoutAsync());
            RefreshCommand.Execute(null!);
        }

        public void RefreshAsync()
        {
            LoadCategoriesAsync();
            LoadOrderAsync();
        }

        private async void LogoutAsync()
        {
            try
            {
                await _service.LogoutAsync();
                LogoutSucceeded?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }

        }

        private async void LoadOrderAsync()
        {
            try
            {
                Orders = new ObservableCollection<OrderDTO>(await _service.LoadOrdersAsync());
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Váratlan hiba történt! ({ex.Message})");
            }
        }

        private async void LoadCategoriesAsync()
        {
            try
            {
                Categories = new ObservableCollection<CategoryDTO>(await _service.LoadCategoriesAsync());
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Váratlan hiba történt! ({ex.Message})");
            }
        }

        private async void LoadCartItemsAsync(OrderDTO orderDto)
        {
            if (orderDto is null)
            {
                CartItems = new ObservableCollection<CartItemDTO>();
                return;
            }

            try
            {
                SelectedOrder = orderDto;
                CartItems = new ObservableCollection<CartItemDTO>(await _service.LoadCartItemsAsync(orderDto.Id));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        public async void CompleteOrder()
        {
            try
            {
                if (SelectedOrder.TimeOfCompletion == null)
                {
                    SelectedOrder.TimeOfCompletion = DateTime.Now;
                    await _service.UpdateOrderAsync((OrderDTO) SelectedOrder);
                    RefreshAsync();
                }
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        public async void FilterButton_Clicked()
        {
            var orders = await _service.LoadOrdersAsync();

            orders = FilterMode switch
            {
                FilterModeEnum.All => orders,
                FilterModeEnum.Completed => orders.Where(o => o.TimeOfCompletion != null).ToList(),
                FilterModeEnum.NotCompleted => orders.Where(o => o.TimeOfCompletion == null).ToList(),
            };

            if (FilterName != null && FilterName != "")
            {
                orders = orders.Where(o => o.Name.Contains(FilterName));
            }

            if (FilterAddress != null && FilterAddress != "")
            {
                orders = orders.Where(o => o.Address.Contains(FilterAddress));
            }

            Orders = new ObservableCollection<OrderDTO>(orders);
        }

        public void FilterModeChanged(object? sender)
        {
            string str = sender!.ToString()!;
            switch (str)
            {
                case "1":
                    FilterMode = FilterModeEnum.All;
                    break;
                case "2":
                    FilterMode = FilterModeEnum.Completed;
                    break;
                case "3":
                    FilterMode = FilterModeEnum.NotCompleted;
                    break;
            }
        }
        private void StartNewItem()
        {
            NewItem = new NewItemViewModel();
            StartingNewItem?.Invoke(this, EventArgs.Empty);
        }
        private void CancelNewItem()
        {
            NewItem = null;
            FinishingNewItem?.Invoke(this, EventArgs.Empty);
        }

        private async void SaveNewItem(NewItemViewModel newItemViewModel)
        {
            var itemDto = new ItemDTO()
            {
                Id = newItemViewModel.Id,
                Name = newItemViewModel.Name,
                Price = newItemViewModel.Price,
                Description = newItemViewModel.Description ?? "",
                Spicy = newItemViewModel.Spicy,
                Vegan = newItemViewModel.Vegan,
                OrderedCnt = newItemViewModel.OrderedCount,
                Category = Categories.FirstOrDefault(c => c.Id == newItemViewModel.CategoryId)
            };

            if (itemDto.Name.Length > 30 || itemDto.Name.Length == 0)
            {
                OnMessageApplication($"A név 1-30 betű hossz lehet csak!");
            }
            if (itemDto.Price < 0)
            {
                OnMessageApplication($"Az ár nem lehet kissebb mint 0!");
            } else if (itemDto.OrderedCnt < 0)
            {
                OnMessageApplication($"A rendelt db szám nem lehet kissebb mint 0!");
            } else {
                try
                {
                    await _service.CreateItemAsync(itemDto);
                    FinishingNewItem?.Invoke(this, EventArgs.Empty);
                }

                catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
                {
                    OnMessageApplication($"Váratlan hiba! ({ex.Message})");
                }
            }
            
        }
    }
}

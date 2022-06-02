using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Desktop.Model;

namespace Desktop.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly FoodOrderApiService _service;
        private Boolean _isLoading;
        public DelegateCommand LoginCommand { get; private set; }
        public String UserName { get; set; }
        public Boolean IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler LoginSucceeded;
        public event EventHandler LoginFailed;

        public LoginViewModel(FoodOrderApiService service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            _service = service;
            UserName = String.Empty;
            IsLoading = false;

            LoginCommand = new DelegateCommand(_ => !IsLoading, param => LoginAsync(param as PasswordBox));
        }

        private async void LoginAsync(PasswordBox passwordBox)
        {
            if (passwordBox == null)
                return;

            try
            {
                IsLoading = true;
                bool result = await _service.LoginAsync(UserName, passwordBox.Password);
                IsLoading = false;

                if (result)
                    OnLoginSuccess();
                else
                    OnLoginFailed();
            }
            catch (HttpRequestException ex)
            {
                OnMessageApplication($"Server error occurred: ({ex.Message})");
            }
            catch (NetworkException ex)
            {
                OnMessageApplication($"Unexpected error occurred: ({ex.Message})");
            }
        }

        private void OnLoginSuccess()
        {
            LoginSucceeded?.Invoke(this, EventArgs.Empty);
        }

        private void OnLoginFailed()
        {
            LoginFailed?.Invoke(this, EventArgs.Empty);
        }
    }
}

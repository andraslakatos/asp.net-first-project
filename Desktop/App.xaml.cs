using System;
using System.Configuration;
using System.Windows;
using Desktop.Model;
using Desktop.ViewModel;
using Desktop.View;

namespace Desktop
{
    public partial class App : Application
    {

        private FoodOrderApiService _service;
        private MainViewModel _mainViewModel;
        private LoginViewModel _loginViewModel;
        private MainWindow _view;
        private LoginWindow _loginView;
        private NewItemWindow _newItemView;

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _service = new FoodOrderApiService(ConfigurationManager.AppSettings["baseAddress"]);

            _loginViewModel = new LoginViewModel(_service);

            _loginViewModel.LoginSucceeded += ViewModel_LoginSucceeded;
            _loginViewModel.LoginFailed += ViewModel_LoginFailed;
            _loginViewModel.MessageApplication += ViewModel_MessageApplication;

            _loginView = new LoginWindow
            {
                DataContext = _loginViewModel
            };

            _mainViewModel = new MainViewModel(_service);
            _mainViewModel.MessageApplication += ViewModel_MessageApplication;
            _mainViewModel.LogoutSucceeded += ViewModel_LogoutSucceeded;

            _mainViewModel.StartingNewItem += ViewModel_StartingNewItem;
            _mainViewModel.FinishingNewItem += ViewModel_FinishingNewItem;

            _view = new MainWindow
            {
                DataContext = _mainViewModel
            };

            MainWindow = _view;
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            _loginView.Closed += LoginView_Closed; // bejelentkezési ablak bezárásakor is leállítás

            _loginView.Show();
        }

        private void LoginView_Closed(object sender, EventArgs e)
        {
            Shutdown();
        }

        private void ViewModel_LoginSucceeded(object sender, EventArgs e)
        {
            _loginView.Hide();
            _view.Show();
        }

        private void ViewModel_LoginFailed(object sender, EventArgs e)
        {
            MessageBox.Show("Login unsuccessful!", "TodoList", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void ViewModel_LogoutSucceeded(object sender, EventArgs e)
        {
            _view.Hide();
            _loginView.Show();
        }
        private void ViewModel_MessageApplication(object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "FoodOrder", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void ViewModel_StartingNewItem(object sender, EventArgs e)
        {
            _newItemView = new NewItemWindow()
            {
                DataContext = _mainViewModel,
                Owner = _view
            };
            _newItemView.ShowDialog();
            _mainViewModel.RefreshAsync();
        }

        private void ViewModel_FinishingNewItem(object sender, EventArgs e)
        {
            if (_newItemView.IsActive)
                _newItemView.Close();
        }

    }
}

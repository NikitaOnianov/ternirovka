using Avalonia.Controls;
using Onianov.Models;
using System.Linq;

namespace Onianov
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Auth_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (Login.Text != null && password.Text != null)
            {
                try
                {
                    using (PostgresContext db = new PostgresContext())
                    {
                        User? user = db.Users.FirstOrDefault(it=>it.UserLogin == Login.Text && it.UserPassword == password.Text);
                        if (user != null)
                        {
                            entry(user);
                        }
                        else
                        {
                            Mess.Text = "Пользователь отсутствует в системе";
                        }
                    }
                }
                catch
                {
                    Mess.Text = "Ошибкаы";
                }
            }
            else
            {
                Mess.Text = "Введите все данные";
            }
        }

        private void Pass_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            new ClientPass().Show(); Close();
        }
        void entry(User user)
        {
            if (user.UserType == 1) {
                new Admin(user).Show(); Close(); }
            else if (user.UserType == 2) {
                new Meneger(user).Show(); Close(); }
            else
                new Client(user).Show(); Close();
        }
    }
}
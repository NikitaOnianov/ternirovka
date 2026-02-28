using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using Onianov.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Onianov;

public partial class Admin : Window
{
    User user1 { get; set; }
    public Admin()
    {
        InitializeComponent();
    }
    public Admin(User user)
    {
        InitializeComponent();
        user1 = user;
        name.Text = user.UserName;
        ListTovars.ItemsSource = allTovars();
    }

    ObservableCollection<TovarDTO> allTovars()
    {
        using (PostgresContext db = new PostgresContext())
        {
            var all = db.Tovars.
                Include(it => it.TovarDelivaryNavigation).
                Include(it => it.TovarProducerNavigation).
                Include(it => it.TovarTypeNavigation).
                Select(it => new TovarDTO()
                {
                    TovarCount = it.TovarCount,
                    TovarDelivary = it.TovarDelivaryNavigation.DelivereName,
                    TovarDesription = it.TovarDesription,
                    TovarId = it.TovarId,
                    TovarName = it.TovarName,
                    TovarPhoto = Image(it.TovarPhoto),
                    TovarPrice = it.TovarPrice,
                    TovarProducer = it.TovarProducerNavigation.ProducerName,
                    TovarSkidka = it.TovarSkidka,
                    TovarType = it.TovarTypeNavigation.TovarName,
                }).ToList();
            return new ObservableCollection<TovarDTO>(all);
        }
    }
    static Bitmap Image(string? name)
    {
        try
        {
            return new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "Assets/Images/" + name);
        }
        catch
        {
            return new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "Assets/picture.png");
        }
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }

    private void Add_tovat_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new AddTovar(user1).Show();
        Close();
    }

    private void ListTovars_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ListTovars.SelectedItem != null)
        {
            string tovarDTO_id = (ListTovars.SelectedItem as TovarDTO).TovarId;
            using (PostgresContext db = new PostgresContext())
            {
                Tovar tovar = db.Tovars.First(it=>it.TovarId == tovarDTO_id);
                new AddTovar(user1, tovar).Show();
                Close();
            }
        }
    }
}
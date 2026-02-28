using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using Onianov.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Onianov;

public partial class Client : Window
{
    User user1 { get; set; }
    ObservableCollection<TovarDTO> tovars { get; set; }
    public Client()
    {
        InitializeComponent();
    }
    public Client(User user)
    {
        InitializeComponent();
        user1 = user;
        name.Text = user.UserName;
        tovars = allTovars();
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
}
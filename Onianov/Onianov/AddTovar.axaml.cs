using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Onianov.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace Onianov;

public partial class AddTovar : Window
{
    User user;
    Tovar tovar1;
    ObservableCollection<Delivere> deliveres;
    ObservableCollection<TovarType> tovarTypes;
    ObservableCollection<Producer> producers;
    public AddTovar()
    {
        InitializeComponent();
    }
    public AddTovar(User user1) // если добавлять
    {
        InitializeComponent();
        user = user1;
        using (PostgresContext db = new PostgresContext())
        {
            deliveres = new ObservableCollection<Delivere>(db.Deliveres.ToList());
            tovarTypes = new ObservableCollection<TovarType>(db.TovarTypes.ToList());
            producers = new ObservableCollection<Producer>(db.Producers.ToList());
        }
        ListDeliver.ItemsSource = deliveres;
        ListProduser.ItemsSource = producers;
        ListType.ItemsSource = tovarTypes;
    }
    public AddTovar(User user1, Tovar tovar) // если редактировать
    {
        InitializeComponent();
        user = user1;
        tovar1 = tovar;


        using (PostgresContext db = new PostgresContext())
        {
            deliveres = new ObservableCollection<Delivere>(db.Deliveres.ToList());
            tovarTypes = new ObservableCollection<TovarType>(db.TovarTypes.ToList());
            producers = new ObservableCollection<Producer>(db.Producers.ToList());

            ListDeliver.ItemsSource = deliveres;
            ListProduser.ItemsSource = producers;
            ListType.ItemsSource = tovarTypes;

            name.Text = tovar1.TovarName;
            idname.Text = tovar1.TovarId;
            price.Text = (tovar1.TovarPrice).ToString();
            ListDeliver.SelectedItem = db.Deliveres.First(it=>it.DelivereId == tovar1.TovarDelivary);
            ListProduser.SelectedItem = db.Producers.First(it => it.ProducerId == tovar1.TovarProducer);
            ListType.SelectedItem = db.TovarTypes.First(it => it.TovarId == tovar1.TovarType);
        }
    }

    private void Button_Click_Add(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (name.Text != null && price.Text != null && ListDeliver.SelectedItem != null && ListProduser.SelectedItem != null && ListType.SelectedItem != null && idname.Text != null)
        {
            try
            {
                decimal i = decimal.Parse(price.Text);
                add(name.Text, (ListDeliver.SelectedItem as Delivere).DelivereId, (ListProduser.SelectedItem as Producer).ProducerId, i, (ListType.SelectedItem as TovarType).TovarId, idname.Text);
                new Admin(user).Show();
                Close();
            }
            catch
            {
                Mess.Text = "Ошибка";
            }
        }
        else
        {
            Mess.Text = "Ошибка";
        }
    }

    private void Button_Click_Red(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        try
        {
            if (name.Text != null && price.Text != null && ListDeliver.SelectedItem != null && ListProduser.SelectedItem != null && ListType.SelectedItem != null && idname.Text != null)
            {

                decimal i = decimal.Parse(price.Text);
                red(name.Text, (ListDeliver.SelectedItem as Delivere).DelivereId, (ListProduser.SelectedItem as Producer).ProducerId, i, (ListType.SelectedItem as TovarType).TovarId);
                new Admin(user).Show();
                Close();
            }
        }
        catch
        {
            Mess.Text = "Ошибка";
        }
    }
    void add(string name, int Delivary, int producre, decimal price, int type, string art)
    {
        try
        {
            using (PostgresContext db = new PostgresContext())
            {
                Tovar tovar = new Tovar()
                {
                    TovarCount = 0,
                    TovarDelivary = Delivary,
                    TovarDesription = "",
                    TovarName = name,
                    TovarPhoto = null,
                    TovarPrice = price,
                    TovarProducer = producre,
                    TovarSkidka = null,
                    TovarType = type,
                    TovarId = art
                };
                db.Tovars.Add(tovar);
                db.SaveChanges();
            }
        }
        catch
        {
            Mess.Text = "Ошибка добавления";
        }
    }
    void red(string name, int Delivary, int producre, decimal price, int type)
    {
        try
        {
            using (PostgresContext db = new PostgresContext())
            {
                Tovar tovar = db.Tovars.First(it => it.TovarId == tovar1.TovarId);
                tovar.TovarCount = 0;
                tovar.TovarDelivary = Delivary;
                tovar.TovarDesription = "";
                tovar.TovarName = name;
                tovar.TovarPhoto = null;
                tovar.TovarPrice = price;
                tovar.TovarProducer = producre;
                tovar.TovarSkidka = null;
                tovar.TovarType = type;
                db.SaveChanges();
            }
        }
        catch
        {
            Mess.Text = "Ошибка изменения";
        }
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new Admin(user).Show();
        Close();
    }
}
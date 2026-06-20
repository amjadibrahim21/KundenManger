using System;
using System.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Converters;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace AvaloniaApplication5.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

    }

    private void click_hier(object? sender, RoutedEventArgs e)
    {
        string name = NameBox.Text ?? "";
        string email = EmailBox.Text ?? "";
        string nummer = NummerBox.Text ?? "";
        if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(nummer))
        {
            DataBase.AddKunde(name, email, nummer);
            NameBox.Text = EmailBox.Text = NummerBox.Text = "";
            
        }
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var kunden = DataBase.kundenListe();
        KundenListe.Items.Clear();
        KundenListe.Items.Add( new TextBlock{
             Text = String.Format("{0,-5} {1,-20} {2,-30} {3,-15}",
                "ID", "Name", "Email", "Nummer"),
            FontSize = 16, FontWeight = FontWeight.Bold
      }  );

        // Kunden
        foreach (var kunde in kunden)
        {
            KundenListe.Items.Add(
                String.Format("{0,-5} {1,-20} {2,-30} {3,-15}",
                    kunde.Id, kunde.Name, kunde.Email, kunde.tel)
            );
        }
    }

    private void kundeLoschen(object? sender, RoutedEventArgs e)
    {
        if (int.TryParse(IdTextBox.Text, out int id))
        {
            DataBase.kundenloschen(id);
            IdTextBox.Text = "";
        }
        else
        {
            var dlg = new Window
            {
                Content = new TextBlock{Text = "Ungültige ID!"}
            };
            dlg.ShowDialog(this);
        }
    }
}

    
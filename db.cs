using System;
using System.Collections.Generic;
using Avalonia.Controls;
using MySql.Data.MySqlClient;
namespace AvaloniaApplication5;

public class DataBase
{
    public static void CreatDatabase()
    {
        string connectionString = "server=localhost;port=3306;user=root;password=";
        MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();
        Console.WriteLine("connection opened");
        string sql = "CREATE DATABASE IF NOT EXISTS kk";
        MySqlCommand command = new MySqlCommand(sql, connection);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public static void CreateTables()
    {
        string connectionString = "server=localhost;port=3306;user=root;database=kk;password=";
        MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();
        Console.WriteLine("connection opened");
        string sql = @"
CREATE TABLE IF NOT EXISTS kunde(
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    tel VARCHAR(50) NOT NULL
);
   ";
        MySqlCommand command = new MySqlCommand(sql, connection);
        command.ExecuteNonQuery();
        connection.Close();
        Console.WriteLine("Die Tabelle würde erstellt: ");
    }

    static string connectionString = "server=localhost;port=3306;user=root;database=kk;password=;";

    public static void AddKunde(string name, string email, string tel)
    {
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();
        Console.WriteLine("connection opened");

        string sql = "INSERT INTO kunde (Name, Email, tel) VALUES (@name, @email, @nummer)";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@email", email);
        command.Parameters.AddWithValue("@nummer", tel);
        command.ExecuteNonQuery();
        connection.Close();
    }







    public static List<kunde> kundenListe()
    {
        var kunnde = new List<kunde>();
        try
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string sql = "SELECT * FROM kunde";
            using MySqlCommand command = new MySqlCommand(sql, connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var kunde = new kunde
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Email = reader.GetString("email"),
                    tel = reader.GetString("tel")
                };
                kunnde.Add(kunde);

            }
        }catch(MySqlException ex)
        {
            Console.WriteLine(ex.Message + "Fehler");
        }
        
        return kunnde;
        
    }

    public static void kundenloschen(int id)
    {
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();
        Console.WriteLine("connection opened");
        string sql = "DELETE FROM kunde WHERE id = @id";
        using MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
        connection.Close();
    }

   
}
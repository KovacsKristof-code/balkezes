using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace balsmthin
{
    internal class Adatbazis
    {
        private string connectionString;
        private List<Balkezesek> lista;

        public Adatbazis()
        {
            this.connectionString = "Server=localhost;Database=bal;Uid=root;Pwd='';";
            this.lista = new List<Balkezesek>();
            Kapcsolodas();
            Feladatok();
        }

        private void Kapcsolodas()
        {
            Console.WriteLine("Kapcsolódás az adatbázishoz...");

            string query = "SELECT * FROM balkezesek";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this.connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        Console.WriteLine("Sikeres kapcsolódás az adatbázishoz.");
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Balkezesek balkezes = new Balkezesek(
                                    reader.GetString("nev"),
                                    DateOnly.FromDateTime(reader.GetDateTime("elso")),
                                    DateOnly.FromDateTime(reader.GetDateTime("utolso")),
                                    reader.GetInt32("suly"),
                                    reader.GetInt32("magassag")
                                );
                                lista.Add(balkezes);
                            }
                            Console.WriteLine("Adatok betöltve az adatbázisból.");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Hiba történt az adatbázis lekérdezés során: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt: " + ex.Message);
                Environment.Exit(1);
            }
        }

        private void Feladatok()
        {
            Console.WriteLine("-----------------------------------------------------------------");
            foreach (var balkezes in lista)
            {
                Console.WriteLine(balkezes.ToString());
            }
            Console.WriteLine("-----------------------------------------------------------------");

            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("3. feladat: " + lista.Count);

            var okt99 = lista.Where(b => b.Utolso.Year == 1999 && b.Utolso.Month == 10);

            Console.WriteLine("4. feladat:");
            foreach (var balkezes in okt99)
            {
                Console.WriteLine("\t" + balkezes.Nev+", "+ balkezes.Magassag*2.54+ " cm");
            }

            Console.WriteLine("5. feladat:");
            Console.Write("Kérek egy évszámot [1990-1999]: ");
            int ev = int.Parse(Console.ReadLine());
            while (ev < 1990 || ev > 1999)
            {
                Console.Write("Hibás adat, kérek egy 1990 és 1999 közötti évszámot! ");
                ev = int.Parse(Console.ReadLine());
            }
            Console.Write("5. feladat:");
            var atlsuly = lista.Where(b => b.Elso.Year == ev).Average(b => b.Suly);
            Console.WriteLine($"{atlsuly:F2} font");
        }
    }
}

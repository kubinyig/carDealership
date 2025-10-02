using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carDealership
{
    class Program
    {
        public static List<Car> cars = new List<Car>();
        public static List<Brand> brands = new List<Brand>();
        public static List<Owner> owners = new List<Owner>();
        static serverConnection connection = new serverConnection("http://localhost:3000");
        static async Task Main(string[] args)
        {
            cars = await connection.GetCars();
            brands = await connection.Getbrands();
            owners = await connection.GetOwners();
            while (true)
            {
                menu();
                getcommand();
            }
        }
        static void menu()
        {
            Console.WriteLine("Autók / Márkák / tulajdpnosok kilistázása: -list:c / m / t");
            Console.WriteLine("Adat hozzáadása: -c/m/t:add");
            Console.WriteLine("Egy elem törlése: -remove:típus:elem");
            Console.WriteLine("Kilépés: -exit");
        }
        static async Task getcommand()
        {
            string command = Console.ReadLine();
            if (command.Split(';')[1].Length == 1)
            {
                list(char.Parse(command.Split(';')[1]));
            }
            else if (command.Split(':')[0].Length == 1)
            {
                await add(char.Parse(command.Split(':')[0]));
            }
            else if (command.Split(':')[0] == "-remove")
            {
                await Remove(char.Parse(command.Split(':')[1]), int.Parse(command.Split(':')[2]));
            }
            else if (command == "-exit")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Hibás parancs!");
            }


        }
        static async Task Remove(char type, int id) {
        
            if(type == 'c')
            {
                await connection.delete("car", id);
                cars.Clear();
                cars = connection.GetCars().Result;
            }
            else if (type == 'm')
            {
                await connection.delete("brand", id);
                brands.Clear();
                brands = connection.Getbrands().Result;
            }
            else if (type == 't')
            {
                await connection.delete("owner", id);
                owners.Clear();
                owners = connection.GetOwners().Result;
            }
        }
        static async Task add(char type) {
            if (type == 'c')
            {
                Console.WriteLine("Márka ID:");
                int brandid = int.Parse(Console.ReadLine());
                Console.WriteLine("Modell:");
                string model = Console.ReadLine();
                Console.WriteLine("Teljesítmény (LE):");
                int performance = int.Parse(Console.ReadLine());
                Console.WriteLine("Gyártási év:");
                int manufacturingyear = int.Parse(Console.ReadLine());
                Console.WriteLine("Kerék szélesség (mm):");
                int wheelwidth = int.Parse(Console.ReadLine());
                await connection.addCar(brandid, model, performance, manufacturingyear, wheelwidth);
                cars.Clear();
                cars = await connection.GetCars();
            }
            else if (type == 'm')
            {
                Console.WriteLine("Márka név:");
                string name = Console.ReadLine();
                Console.WriteLine("Alapítás éve:");
                int founded = int.Parse(Console.ReadLine());
                Console.WriteLine("Ország:");
                string country = Console.ReadLine();
                Console.WriteLine("Gyártási év:");
                int manufacturingyear =int.Parse( Console.ReadLine());
                await connection.PostBrand(name, founded, country, manufacturingyear);
                brands.Clear();
                brands = await connection.Getbrands();
            }
            else if (type == 't')
            {
                Console.WriteLine("Név:");
                string name = Console.ReadLine();
                Console.WriteLine("Cím:");
                string address = Console.ReadLine();
                Console.WriteLine("Születési év:");
                int birthyear = int.Parse(Console.ReadLine());
                Console.WriteLine("Autó ID:");
                int carid = int.Parse(Console.ReadLine());
                await connection.addOwner(carid, name, address, birthyear );
                owners.Clear();
                owners = await connection.GetOwners();
            }
        }
        static void list(char type)
        {
            if (type == 'c')
            {
                string brandname = "";
                foreach (var item in cars)
                {
                    foreach (var brand in brands)
                    {
                        if (item.brandid == brand.id)
                        {
                            brandname = brand.name;
                        }
                    }
                    Console.WriteLine($"{item.id}; {brandname}, {item.model}; {item.performance}hp; {item.wheelwidth}mm");
                }
            }
            if (type == 'm')
            {
                foreach (var item in brands)
                {
                    Console.WriteLine($"{item.id}; {item.name}; {item.country}; {item.founded}");
                }
            }
            if (type == 't')
            {
                foreach (var item in owners)
                {
                    foreach (var car in cars)
                    {
                        if (item.carid == car.id)
                        {
                            Console.WriteLine($"{item.id}; {item.name}; {item.address}; {item.birthyear}; {car.model}");
                        }
                    }
                }
            }
        }
    }
}

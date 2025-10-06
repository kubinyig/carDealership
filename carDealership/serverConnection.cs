using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace carDealership
{
    class serverConnection
    {
        HttpClient client = new HttpClient();
        public serverConnection(string url)
        {
            if (!url.StartsWith("http://")) throw new ArgumentException("Hibas URL, http:// megadása kötelező");
            client.BaseAddress = new Uri(url);
        }
        public async Task<List<Brand>> Getbrands()
        {
            List<Brand> brands = new List<Brand>();
            try
            {
                HttpResponseMessage response = await client.GetAsync("/getbrands");
                response.EnsureSuccessStatusCode();
                brands = JsonConvert.DeserializeObject<List<Brand>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return brands;
        }
        public async Task<message> PostBrand(
            string Name,
            int Founded,
            string Country,
            int Manufacturingyear)
        {
            message Message = new message();
            try
            {
                var jsonData = new
                {
                    name = Name,
                    founded = Founded,
                    country = Country,
                    manufacturingyear = Manufacturingyear
                };
                string jsonstring = JsonConvert.SerializeObject(jsonData);
                HttpContent json = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PostAsync("/addbrand", json);
                res.EnsureSuccessStatusCode();
                Message = JsonConvert.DeserializeObject<message>(await res.Content.ReadAsStringAsync());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Message;
        }
        public async Task<message> delete(string type, int id)
        {
            message Message = new message();
            try
            {
                HttpResponseMessage res = await client.DeleteAsync("/" + type + "/" + id);
                res.EnsureSuccessStatusCode();
                Message = JsonConvert.DeserializeObject<message>(await res.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return new message() { Message = e.Message };
            }
            return Message;
        }
        public async Task<List<Car>> GetCars()
        {
            List<Car> cars = new List<Car>();
            try
            {
                HttpResponseMessage response = await client.GetAsync("/getcars");
                response.EnsureSuccessStatusCode();
                cars = JsonConvert.DeserializeObject<List<Car>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return cars;
        }
        public async Task<message> addCar(
    int Brandid,
    string Model,
    int Perfprmance,
    int Manufacturingyear,
    int Wheelwidth)
        {
            message Message = new message();
            try
            {
                var jsonData = new
                {
                    brandid = Brandid,
                    model = Model,
                    performance = Perfprmance,
                    manufacturingyear = Manufacturingyear,
                    wheelwidth = Wheelwidth
                };
                string jsonstring = JsonConvert.SerializeObject(jsonData);
                HttpContent json = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PostAsync("/addcar", json);
                res.EnsureSuccessStatusCode();
                Message = JsonConvert.DeserializeObject<message>(await res.Content.ReadAsStringAsync());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Message;
        }
        public async Task<List<Owner>> GetOwners()
        {
            List<Owner> owners = new List<Owner>();
            try
            {
                HttpResponseMessage response = await client.GetAsync("/getowners");
                response.EnsureSuccessStatusCode();
                owners = JsonConvert.DeserializeObject<List<Owner>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return owners;
        }
        public async Task<message> addOwner(
            int Carid,
             string Name,
            string Address,
            int Birthyear)
        {
            message Message = new message();
            try
            {
                var jsonData = new
                {
                    carid = Carid,
                    name = Name,
                    address = Address,
                    birthyear = Birthyear
                };
                string jsonstring = JsonConvert.SerializeObject(jsonData);
                HttpContent json = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PostAsync("/addowner", json);
                res.EnsureSuccessStatusCode();
                Message = JsonConvert.DeserializeObject<message>(await res.Content.ReadAsStringAsync());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Message;
        }
        public async Task<message> putCar(
            int Perfprmance,
            int Wheelwidth = -1)
        { 
            message Message = new message();
            dynamic jsonData;
            if (Wheelwidth <=0 && Perfprmance > 0)
            {
                 jsonData = new
                {
                    performance = Perfprmance,
                };
            }
            else if (Perfprmance <= 0 && Wheelwidth > 0)
            {
                jsonData = new
                {
                    wheelwidth = Wheelwidth
                };
            }
            else if(Perfprmance> 0 && Wheelwidth > 0)
            {
                jsonData = new
                {
                    performance = Perfprmance,
                    wheelwidth = Wheelwidth
                };
            }
            else
            {
                return new message() { Message = "semmi sem változott" };
            }
            try
                {
                    string jsonstring = JsonConvert.SerializeObject(jsonData);
                    HttpContent json = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await client.PutAsync("/addcar", json);
                    res.EnsureSuccessStatusCode();
                    Message = JsonConvert.DeserializeObject<message>(await res.Content.ReadAsStringAsync());

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            return Message;
        }
    }
}


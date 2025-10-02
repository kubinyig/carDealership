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
        string baseurl = "";
        public serverConnection(string url)
        {
            if (!url.StartsWith("http://")) throw new ArgumentException("Hibas URL, http:// megadása kötelező");
            baseurl = url;
        }
        public async Task<List<Brand>> Getbrands()
        {
            List<Brand> brands = new List<Brand>();
            string url = baseurl + "/getbrands";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
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
            string url = baseurl + "/addbrand";
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
                HttpResponseMessage res = await client.PostAsync(url, json);
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
            string url = baseurl + "/"+type+"/" + id;
            try
            {
                HttpResponseMessage res = await client.DeleteAsync(url);
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
            string url = baseurl + "/getcars";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
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
            string url = baseurl + "/addcar";
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
                HttpResponseMessage res = await client.PostAsync(url, json);
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
            string url = baseurl + "/getowners";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
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
            string url = baseurl + "/addowner";
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
                HttpResponseMessage res = await client.PostAsync(url, json);
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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject2Server
{
    class DataStorage
    {
        private List<Car> carList = new List<Car>()
        {
                new Car("Audi", new DateTime(1996, 11, 22),"red", 35000),
                new Car("Audi", new DateTime(1996, 11, 22),"black", 19000),
                new Car("Audi", new DateTime(2019, 11, 12),"red", 35000),
                new Car("Audi", new DateTime(2019, 11, 20),"blue", 42000),
                new Car("Audi", new DateTime(2019, 11, 15),"yellow", 38000),
                new Car("Audi", new DateTime(2019, 11, 29),"black", 30000),
                new Car("Toyota", new DateTime(2019, 11, 24),"red", 36000),
                new Car("Toyota", new DateTime(2019, 11, 22),"green", 39000),
                new Car("Toyota", new DateTime(2019, 11, 30),"orange", 32000),
                new Car("Toyota", new DateTime(2019, 11, 21),"blue", 23000),
                new Car("Toyota", new DateTime(2019, 11, 25),"blue", 32000),
                new Car("Toyota", new DateTime(2019, 11, 18),"gray", 45000),
                new Car("BMW", new DateTime(2019, 11, 13),"black", 45000),
                new Car("BMW", new DateTime(2019, 11, 14),"gray", 28000),
                new Car("BMW", new DateTime(2019, 11, 22),"blue", 69000),
                new Car("BMW", new DateTime(2019, 11, 27),"orange", 50000),
                new Car("Peugeot", new DateTime(2019, 11, 16),"red", 11000),
                new Car("Peugeot", new DateTime(2019, 11, 18),"blue", 18000),
                new Car("Peugeot", new DateTime(2019, 11, 29),"gray", 9000),
                new Car("Peugeot", new DateTime(2019, 11, 27),"black", 11000),
                new Car("Peugeot", new DateTime(2019, 11, 12),"green", 25000),
                new Car("Peugeot", new DateTime(2019, 11, 24),"yellow", 33000),
                new Car("Ferrari", new DateTime(2019, 11, 22),"red", 120000),
                new Car("Ferrari", new DateTime(2019, 11, 28),"black", 200000),
                new Car("Ferrari", new DateTime(2019, 11, 28),"black", 200000),
                new Car("lamborghini", new DateTime(2019, 11, 14),"yellow", 300000),
                new Car("lamborghini", new DateTime(2019, 11, 19),"green", 350000),
                new Car("Fiat", new DateTime(2019, 11, 18),"yellow", 8000),
                new Car("Fiat", new DateTime(2019, 11, 26),"gray", 9000),
                new Car("Fiat", new DateTime(2019, 11, 22),"blue", 6000)
        };

        public List<Car> CarList
        {
            get { return carList; }
        }
    }
}

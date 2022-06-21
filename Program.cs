using System.Text.Json;

List<Helicopter> Helicopters = new List<Helicopter>();
List<Plane> Planes = new List<Plane>();
List<Train> Trains = new List<Train>();
List<Car> Cars = new List<Car>();

using (StreamReader sr = new StreamReader(@"E:\Yaromyr\VisStudio\files\transports.txt"))
{
    string line;
    byte key = 0;

    while ((line = sr.ReadLine()) != null)
    {
        if (line == "Helicopters:") key = 1;
        else if (line == "Planes:") key = 2;
        else if (line == "Trains:") key = 3;
        else if (line == "Cars:") key = 4;
        else if (key == 1) Helicopters.Add(JsonSerializer.Deserialize<Helicopter>(line));
        else if (key == 2) Planes.Add(JsonSerializer.Deserialize<Plane>(line));
        else if (key == 3) Trains.Add(JsonSerializer.Deserialize<Train>(line));
        else if (key == 4) Cars.Add(JsonSerializer.Deserialize<Car>(line));
    }
}

Console.WriteLine("Який шлях пройти?");
double Way = Convert.ToDouble(Console.ReadLine());
Console.WriteLine("Який вид транспорту?");
string transporting = Console.ReadLine();
Console.WriteLine("який транспорт вибрати по рахунку?");
int Whichone = Convert.ToInt32(Console.ReadLine());

switch (transporting)
{
    case "Helicopter":
        Helicopters[Whichone].Travel(Way);
        break;
    case "Plane":
        Planes[Whichone].Travel(Way);
        break;
    case "Train":
        Trains[Whichone].Travel(Way);
        break;
    case "Car":
        Cars[Whichone].Travel(Way);
        break;
    default:
        break;
}

abstract class Transport
{
    public string Name { get; set; }
    public double Speed { get; set; }
    public int Price { get; set; }
    public int NumOfPersons { get; set; }
    public double MaxSpeed { get; set; }
    public double MaxPath { get; set; }
    public double RefuelTime { get; set; }

    public Transport()
    {
        Name = "zero";
        Speed = 0;
        Price = 0;
        NumOfPersons = 0;
        MaxPath = 0;
        RefuelTime = 0;
    }
    public Transport(string name, double speed, int price, int numOfPersons, double maxPath, double refuelTime)
    {
        Name = name;
        Speed = speed;
        Price = price;
        NumOfPersons = numOfPersons;
        MaxPath = maxPath;
        RefuelTime = refuelTime;
    }

    public virtual void Travel(double path)
    {
        double currenttime = 0;
        int refueltimes = (int)Math.Floor((double)path / this.MaxPath);
        for (int i =0; i<= refueltimes; i++) Refuel(currenttime);
        currenttime += path / this.Speed;
    }
    public virtual double Refuel(double pathtime)
    {
        pathtime += this.RefuelTime;
        return pathtime;
    }
}

abstract class AirTransport : Transport
{

    public double GetUpTime { get; set; }
    public double SitDownTime { get; set; }

    override public double Refuel(double pathtime)
    {
        SitDown(pathtime);
        base.Refuel(pathtime);
        GetUp(pathtime);
        return pathtime;
    }

    public virtual double GetUp(double pathtime)
    {
        pathtime += this.GetUpTime;
        return pathtime;
    }
    public virtual double SitDown(double pathtime)
    {
        pathtime += this.SitDownTime;
        return pathtime;
    }

    public AirTransport()
    {
        Name = "zero";
        Speed = 0;
        Price = 0;
        NumOfPersons = 0;
        MaxPath = 0;
        RefuelTime = 0;
        GetUpTime = 0;
        SitDownTime = 0;
    }
    public AirTransport(string name, double speed, int price, int numOfPersons, double maxPath, double refuelTime, double getUpTime, double sitDownTime)
        : base(name, speed, price, numOfPersons, maxPath, refuelTime)
    {
        GetUpTime = getUpTime;
        SitDownTime = sitDownTime;
    }
}
class Helicopter : AirTransport
    {
        public Helicopter()
        {
            Name = "zero";
            Speed = 0;
            Price = 0;
            NumOfPersons = 0;
            MaxPath = 0;
            RefuelTime = 0;
            GetUpTime = 0;
            SitDownTime = 0;
        }
        public Helicopter(string name,double speed, int price, int numOfPersons, double maxPath, double refuelTime, double getUpTime, double sitDownTime) 
        : base(name, speed, price, numOfPersons, maxPath, refuelTime, getUpTime, sitDownTime) { }
        override public double GetUp(double pathtime)
        {
            base.GetUp(pathtime);
            Console.WriteLine("Через {0} хвилин вертоліт піднявся в повітря",this.GetUpTime);
            Console.Read();
            return pathtime;
        }
        override public double SitDown(double pathtime)
        {
            base.SitDown(pathtime);
            Console.WriteLine("Через {0} хвилин вертоліт сів на землю", this.SitDownTime);
            Console.Read();
        return pathtime;
    }
        override public double Refuel(double pathtime)
        {
        pathtime += SitDown(pathtime);
            pathtime += this.RefuelTime;
            Console.WriteLine("за {0} хвилин вертоліт заправили", RefuelTime);
            Console.Read();
        pathtime += GetUp(pathtime);
        return pathtime;
    }
        public virtual void Travel(double path)
        {
            Console.WriteLine("Ви направились до вертольоту");
            Console.Read();
            double currenttime= 0;

        currenttime += this.GetUp(currenttime);
        Console.WriteLine("вертоліт летить");
            int refueltimes = (int)Math.Floor((double)path / this.MaxPath);
        for (int i = 0; i < refueltimes; i++)
        {
            currenttime = Refuel(currenttime);
            Console.WriteLine("вертоліт летить");
            Console.ReadKey();
        }
            currenttime += path / this.Speed;
        currenttime += this.SitDown(currenttime);
        Console.WriteLine("за {0} хвилин вертоліт добрався до місця призначення",currenttime );
            Console.Read();
        }
    }
class Plane : AirTransport
{
    public Plane()
     {
        Name = "zero";
        Speed = 0;
        Price = 0;
        NumOfPersons = 0;
        MaxPath = 0;
        RefuelTime = 0;
        GetUpTime = 0;
        SitDownTime = 0;
    }
    public Plane(string name, double speed, int price, int numOfPersons,  double maxPath, double refuelTime, double getUpTime, double sitDownTime) 
        : base(name, speed, price, numOfPersons, maxPath, refuelTime, getUpTime, sitDownTime) { }

    override public double GetUp(double pathtime)
    {
        base.GetUp(pathtime);
        Console.WriteLine("Через {0} хвилин літак піднявся в повітря", this.GetUpTime);
        Console.Read();
        return pathtime;
    }
    override public double SitDown(double pathtime)
        {
            base.SitDown(pathtime);
            Console.WriteLine("Через {0} хвилин літак сів на землю", this.SitDownTime);
            Console.Read();
        return pathtime;
    }
    override public double Refuel(double pathtime)
    {
        pathtime += SitDown(pathtime);
        pathtime += this.RefuelTime;
        Console.WriteLine("за {0} хвилин літак заправили", RefuelTime);
        Console.Read();
        pathtime+= GetUp(pathtime);
        return pathtime;
    }
    public virtual void Travel(double path)
    {
        Console.WriteLine("Ви направились до літака");
        Console.Read();
        double currenttime = 0;
        currenttime += this.GetUp(currenttime);
        Console.WriteLine("Літак летить");
        int refueltimes = (int)Math.Floor((double)path / this.MaxPath);
        for (int i = 0; i < refueltimes; i++)
        {
            currenttime = Refuel(currenttime);
            Console.WriteLine("Літак летить");
            Console.ReadKey();
        }
        currenttime += path / this.Speed;
        currenttime += this.SitDown(currenttime);
        Console.WriteLine("за {0} хвилин літак добрався до місця призначення", currenttime);
        Console.Read();
    }
}
class Train : Transport
    {
        public Train()
        {
        Name = "zero";
            Speed = 0;
            Price = 0;
            NumOfPersons = 0;
            MaxPath = 0;
            RefuelTime = 0;
        }
        public Train(string name, double speed, int price, int numOfPersons,  double maxPath, double refuelTime) 
        : base(name, speed, price, numOfPersons, maxPath, refuelTime) { }
        public virtual void Travel(double path)
        {
            Console.WriteLine("Ви напрвилися до потяга");
            Console.Read();
            double currenttime = 0;
        Console.WriteLine("Потяг їде");
        int refueltimes = (int)Math.Floor((double)path / this.MaxPath);
            for (int i = 0; i < refueltimes; i++)
        {
            currenttime = Refuel(currenttime);
            Console.WriteLine("Потяг їде");
            Console.ReadKey();
        }
        currenttime += path / this.Speed;
            Console.WriteLine("за {0} хвилин потяг добрався до місця призначення", currenttime);
            Console.Read();
        }
        public virtual double Refuel(double pathtime)
        {
        Console.WriteLine("Потяг зупинився щоб заправитися");
        Console.Read();
        pathtime += this.RefuelTime;
            Console.WriteLine("за {0} хвилин потяг заправили", RefuelTime);
            Console.Read();
        return pathtime;
        }
    }
class Car : Transport
    {
        public Car()
        {
        Name = "zero";
            Speed = 0;
            Price = 0;
            NumOfPersons = 0;
            MaxPath = 0;
            RefuelTime = 0;
        }
        public Car(string name, double speed, int price, int numOfPersons, double maxPath, double refuelTime) 
        : base(name, speed, price, numOfPersons, maxPath, refuelTime) { }
    public virtual void Travel(double path)
    {
        Console.WriteLine("Ви направилися до авто");
        Console.Read();
        double currenttime = 0;
        Console.WriteLine("Авто їде");
        int refueltimes = (int)Math.Floor((double)path / this.MaxPath);
        for (int i = 0; i < refueltimes; i++)
        {
            currenttime = Refuel(currenttime);
            Console.WriteLine("Авто їде");
            Console.ReadKey();
        }
        currenttime += path / this.Speed;
        Console.WriteLine("за {0} хвилин авто добралося до місця призначення", currenttime);
        Console.Read();
    }
    public virtual double Refuel(double pathtime)
    {
        Console.WriteLine("авто зупинилося щоб заправитися");
        Console.Read();
        pathtime += this.RefuelTime;
        Console.WriteLine("за {0} хвилин авто заправили", RefuelTime);
        Console.Read();
        return pathtime;
    }
}

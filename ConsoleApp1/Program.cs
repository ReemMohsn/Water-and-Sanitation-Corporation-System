using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace مشروع_مؤسسة__المياة
{
    class Program
    {
        static void Main(string[] args)
        {
            int num;
            do
            {
                Console.WriteLine("----------------Water and Sanitation------------------------");
                Console.WriteLine("(1) Add new subscriber");
                Console.WriteLine("(2) Add new counter");
                Console.WriteLine("(3) Print the invoice");
                Console.WriteLine("(4) Paying off");
                Console.WriteLine("(5) search for a subscriber");
                Console.WriteLine("(6) search for a counter");
                Console.WriteLine("(7) Delete a subscriber");
                Console.WriteLine("(8) Delete a counter");
                Console.WriteLine("(9) Print names of subscribers");
                Console.WriteLine("(10) Print Numbers of counters");
                Console.WriteLine("(11) Counter status");
                Console.WriteLine("(12) Exit");
                Console.Write("Enter number: ");
                num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        Institution.Addsubscriber();
                        break;
                    case 2:
                        Institution.Addcounter();
                        break;
                    case 3:
                        Institution.Printinvoice();
                        break;
                    case 4:
                        Institution.Paying();
                        break;
                    case 5:
                        Institution.SearchSubscriber();
                        break;
                    case 6:
                        Institution.SearchCounter();
                        break;
                    case 7:
                        Institution.DeleteSubscriber();
                        break;
                    case 8:
                        Institution.DeleteCounter();
                        break;
                    case 9:
                        Institution.ShowNamesSubscriber();
                        break;
                    case 10:
                        Institution.ShowNumbersCounters();
                        break;
                    case 11:
                        Institution.CounterStatus();
                        break;
                    case 12:
                        break;

                }
            } while (num != 12);











        }
    }
    class Institution
    {
        static public List<Subscriber> Subscribers;
        static public List<Homecounter> Homecounters;
        static public List<CommercialCounter> CommercialCounters;
        static public long CounterNumber;
        static Institution()
        {
            Subscribers = new List<Subscriber>();
            Homecounters = new List<Homecounter>();
            CommercialCounters = new List<CommercialCounter>();
            CounterNumber = 100000;
        }

        static public void Addsubscriber()
        {
            if (Convert.ToString(CounterNumber).Length == 6)
            {
                bool isfound = false;
                Console.WriteLine("enter the name of Subscriber:");
                string name = Console.ReadLine();
                foreach (Subscriber Su in Subscribers)
                {
                    if (Su.Name == name)
                    {
                        Console.WriteLine("You are already subscribed!");
                        isfound = true;
                    }
                }
                if (isfound == false)
                {
                    Console.WriteLine("enter the phone number of Subscriber:");
                    string phonnumber = Console.ReadLine();
                    Console.WriteLine("enter the type of counter you want (commercial) (home) : ");
                    string counter = Console.ReadLine();
                    while (true)
                    {
                        if (counter == "home")
                        {
                            Homecounter HC = new Homecounter();
                            Homecounter HC1 = HC.AddHomeCounter(CounterNumber);
                            Homecounters.Add(HC1);
                            Subscriber Sb = new Subscriber(name, phonnumber);
                            Subscribers.Add(Sb);
                            Console.WriteLine($"name:{name}\nCounterNumber:{CounterNumber}\nType:{counter}\n");
                            Sb.counters_of_Subscriber.Add(CounterNumber, "home");
                            CounterNumber++;
                            Sb.NumberCounters++;
                            break;
                        }
                        else if (counter == "commercial")
                        {
                            CommercialCounter Cc = new CommercialCounter();
                            CommercialCounter Cc1 = Cc.AddCommercialCounter(CounterNumber);
                            CommercialCounters.Add(Cc1);
                            Subscriber Sb = new Subscriber(name, phonnumber);
                            Subscribers.Add(Sb);
                            Console.WriteLine($"name:{name}\nCounterNumber:{CounterNumber}\nType:{counter}\n");
                            Sb.counters_of_Subscriber.Add(CounterNumber, "home");
                            CounterNumber++;
                            Sb.NumberCounters++;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Wrong input,try agin (commercial) (home) : ");
                            counter = Console.ReadLine();
                        }

                    }



                }

            }
            else
            {
                Console.WriteLine("we can't make new subscriber,sore");
            }

        }
        static public void Addcounter()
        {
            if (Convert.ToString(CounterNumber).Length == 6)
            {
                Console.WriteLine("enter the name of subscriber: ");
                string name = Console.ReadLine();
                bool isfound = false;
                foreach (Subscriber s in Subscribers)
                {
                    if (s.Name == name)
                    {
                        isfound = true;
                    o:
                        Console.WriteLine("enter the type of counter that you want (home) (commercial): ");
                        string type = Console.ReadLine();
                        if (type == "home")
                        {
                            Homecounter HC = new Homecounter();
                            Homecounter Hc = HC.AddHomeCounter(CounterNumber);
                            Homecounters.Add(Hc);
                            Console.WriteLine($"name:{name}\nCounterNumber:{CounterNumber}\nType:{type}\n");
                            s.counters_of_Subscriber.Add(CounterNumber, "home");
                            s.NumberCounters++;
                            CounterNumber++;
                        }
                        else if (type == "commercial")
                        {
                            CommercialCounter CC = new CommercialCounter();
                            CommercialCounter Cc = CC.AddCommercialCounter(CounterNumber);
                            CommercialCounters.Add(Cc);
                            Console.WriteLine($"name:{name}\nCounterNumber:{CounterNumber}\nType:{type}\n");
                            s.counters_of_Subscriber.Add(CounterNumber, "Commercial");
                            s.NumberCounters++;
                            CounterNumber++;
                        }
                        else
                        {
                            Console.WriteLine("the type iis not corect,try again ");
                            goto o;
                        }
                    }
                }
                if (isfound == false)
                {
                    Console.WriteLine("you are not registered as a subscriber, you should registered as a subscriber first.");
                }
            }
            else
            {
                Console.WriteLine("we can't make new counter,sore");
            }

        }
        static public void Printinvoice()
        {
            Console.WriteLine("Enter the type of Counter (home) (commercial):");
            string type = Console.ReadLine();
            Console.WriteLine("Enter the Number of Counter:");
            int num = int.Parse(Console.ReadLine());
            if (type == "home")
            {
                bool isfound = false;
                foreach (Homecounter h in Homecounters)
                {
                    if (num == h.GetNumber())
                    {
                        isfound = true;
                        int dayes;
                        DateTime now = DateTime.Now;
                        if (h.DtListRead.Month == now.Month)
                        {
                            int year = 12 * (now.Year - h.DtListRead.Year);
                            dayes = now.Day - h.DtListRead.Day + 30 * year;
                        }
                        else if (h.DtListRead.Month < now.Month)
                        {
                            int year = 12 * (now.Year - h.DtListRead.Year);
                            int Farg = now.Month - h.DtListRead.Month;
                            dayes = 30 * (Farg - 1) + (30 - h.DtListRead.Day) + now.Day + 30 * year;
                        }
                        else
                        {
                            int year = 12 * ((now.Year - h.DtListRead.Year) - 1);
                            int Farg = (12 - h.DtListRead.Month) + now.Month;
                            dayes = 30 * (Farg - 1) + (30 - h.DtListRead.Day) + now.Day + 30 * year;
                        }
                        if (dayes < 30)
                        {
                            Console.WriteLine("you can't get the invoice,because the mount is not finshed yet");
                        }
                        else
                        {
                            for (int i = 1; i <= dayes / 30; i++)
                            {
                                h.Printinvoice(h);
                            }
                        }


                    }
                }
                if (isfound == false)
                {
                    Console.WriteLine($"there is no counter with number:{num}");
                }
            }
            else if (type == "commercial")
            {
                bool isfound = false;
                foreach (CommercialCounter h in CommercialCounters)
                {
                    if (num == h.GetNumber())
                    {
                        isfound = true;
                        int dayes;
                        DateTime now = DateTime.Now;
                        if (h.DtListRead.Month == now.Month)
                        {
                            int year = 12 * (now.Year - h.DtListRead.Year);
                            dayes = now.Day - h.DtListRead.Day + 30 * year;
                        }
                        else if (h.DtListRead.Month < now.Month)
                        {
                            int year = 12 * (now.Year - h.DtListRead.Year);
                            int Farg = now.Month - h.DtListRead.Month;
                            dayes = 30 * (Farg - 1) + (30 - h.DtListRead.Day) + now.Day + 30 * year;
                        }
                        else
                        {
                            int year = 12 * ((now.Year - h.DtListRead.Year) - 1);
                            int Farg = (12 - h.DtListRead.Month) + now.Month;
                            dayes = 30 * (Farg - 1) + (30 - h.DtListRead.Day) + now.Day + 30 * year;
                        }
                        if (dayes < 30)
                        {
                            Console.WriteLine("you can't get the invoice,because the mont is not finshed yet");
                        }
                        else
                        {
                            for (int i = 1; i <= dayes / 30; i++)
                            {
                                h.Printinvoice(h);
                            }
                        }


                    }
                }
                if (isfound == false)
                {
                    Console.WriteLine($"there is no counter with number:{num}");
                }

            }
        }
        static public void Paying()
        {
            Console.WriteLine("Enter the type of Counter (home) (commercial):");
            string type = Console.ReadLine();
            Console.WriteLine("Enter the Number of Counter:");
            int num = int.Parse(Console.ReadLine());
            if (type == "home")
            {
                bool isfound = false;
                foreach (Homecounter h in Homecounters)
                {
                    if (num == h.GetNumber())
                    {
                        h.Paying();
                        isfound = true;
                    }
                }
                if (isfound == false)
                {
                    Console.WriteLine($"there is no counter with number:{num}");
                }
            }
            else if (type == "commercial")
            {
                bool isfound = false;
                foreach (CommercialCounter C in CommercialCounters)
                {
                    if (num == C.GetNumber())
                    {
                        C.Paying();
                        isfound = true;
                    }
                }
                if (isfound == false)
                {
                    Console.WriteLine($"there is no counter with number:{num}");
                }
            }
            else
            {
                Console.WriteLine($"Wrong (the type of counter), try again:");
            }

        }
        static public void SearchSubscriber()
        {
            Console.WriteLine("enter the name of subscriber: ");
            string name = Console.ReadLine();
            bool isfound = false;
            foreach (Subscriber S in Subscribers)
            {
                if (name == S.Name)
                {
                    Console.WriteLine("IS found");
                    S.printInformation();
                    isfound = true;
                }
            }
            if (isfound == false)
            {
                Console.WriteLine($"there is no subscriber with name:{name}");
            }

        }
        static public void SearchCounter()
        {
            Console.WriteLine("Enter the type of Counter (home) (commercial):");
            string type = Console.ReadLine();
            if (type == "home")
            {
                Homecounter H = new Homecounter();
                H.SearchHomeCounter(Homecounters);
            }
            else if (type == "commercial")
            {
                CommercialCounter C = new CommercialCounter();
                C.SearchCommercialCounter(CommercialCounters);
            }
            else
            {
                Console.WriteLine("The Type is not corect,try again.");
            }
        }
        static public void DeleteSubscriber()
        {
            Console.WriteLine("enter the name of Subscriber:");
            string name = Console.ReadLine();
            Subscriber SS = null;
            bool isNameFou = false;
            foreach (Subscriber S in Subscribers)
            {
                if (name == S.Name)
                {
                    isNameFou = true;
                    if (S.NumberCounters == 0)
                    {
                        SS = S;
                    }
                }

            }
            if (isNameFou == true)
            {
                if (SS != null)
                {
                    Subscribers.Remove(SS);
                }
                else
                {
                    Console.WriteLine("You have counters,delet the counters  first,and then you can delet the Subscrib.");
                }
            }
            else
            {
                Console.WriteLine($"there is no Subscriber with {name} name");
            }
        }
        static public void DeleteCounter()
        {
            Console.WriteLine("Enter the name of Subscriber:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the type of counter:");
            string type = Console.ReadLine();
            Console.WriteLine("Enter the number of counter:");
            int number = int.Parse(Console.ReadLine());
            bool isnamefound = false;
            foreach (Subscriber S in Subscribers)
            {
                if (S.Name == name)
                {
                    isnamefound = true;
                    if (type == "home")
                    {
                        bool isNumFou = false;
                        Homecounter SS = null;
                        foreach (Homecounter H in Homecounters)
                        {
                            if (H.GetNumber() == number)
                            {
                                isNumFou = true;
                                int dayes;
                                DateTime now = DateTime.Now;
                                if (H.DtListRead.Month == now.Month)
                                {
                                    int year = 12 * (now.Year - H.DtListRead.Year);
                                    dayes = now.Day - H.DtListRead.Day + 30 * year;
                                }
                                else if (H.DtListRead.Month < now.Month)
                                {
                                    int year = 12 * (now.Year - H.DtListRead.Year);
                                    int Farg = now.Month - H.DtListRead.Month;
                                    dayes = 30 * (Farg - 1) + (30 - H.DtListRead.Day) + now.Day + 30 * year;
                                }
                                else
                                {
                                    int year = 12 * ((now.Year - H.DtListRead.Year) - 1);
                                    int Farg = (12 - H.DtListRead.Month) + now.Month;
                                    dayes = 30 * (Farg - 1) + (30 - H.DtListRead.Day) + now.Day + 30 * year;
                                }
                                if (dayes < 30)
                                {
                                    if (H.PreviousBalance == 0)
                                    {
                                        SS = H;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"you have a Previous Balance = {H.PreviousBalance},so you should pay that first.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("You have the invoice you didn't print them yet, print them first please .");
                                }
                            }
                        }
                        if (isNumFou == true)
                        {
                            if (SS != null)
                            {
                                Homecounters.Remove(SS);
                                S.counters_of_Subscriber.Remove(number);
                                S.NumberCounters--;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"there is no Counter with {number} Number !");
                        }

                    }
                    else if (type == "commercial")
                    {
                        bool isNumFou = false;
                        CommercialCounter SS = null;
                        foreach (CommercialCounter H in CommercialCounters)
                        {
                            if (H.GetNumber() == number)
                            {
                                isNumFou = true;
                                int dayes;
                                DateTime now = DateTime.Now;
                                if (H.DtListRead.Month == now.Month)
                                {
                                    int year = 12 * (now.Year - H.DtListRead.Year);
                                    dayes = now.Day - H.DtListRead.Day + 30 * year;
                                }
                                else if (H.DtListRead.Month < now.Month)
                                {
                                    int year = 12 * (now.Year - H.DtListRead.Year);
                                    int Farg = now.Month - H.DtListRead.Month;
                                    dayes = 30 * (Farg - 1) + (30 - H.DtListRead.Day) + now.Day + 30 * year;
                                }
                                else
                                {
                                    int year = 12 * ((now.Year - H.DtListRead.Year) - 1);
                                    int Farg = (12 - H.DtListRead.Month) + now.Month;
                                    dayes = 30 * (Farg - 1) + (30 - H.DtListRead.Day) + now.Day + 30 * year;
                                }
                                if (dayes < 30)
                                {
                                    if (H.PreviousBalance == 0)
                                    {
                                        SS = H;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"you have a Previous Balance = {H.PreviousBalance},so you should pay that first.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("You have the invoice you didn't print them yet, print them first please .");
                                }
                            }
                        }
                        if (isNumFou == true)
                        {
                            if (SS != null)
                            {
                                CommercialCounters.Remove(SS);
                                S.counters_of_Subscriber.Remove(number);
                                S.NumberCounters--;

                            }
                        }
                        else
                        {
                            Console.WriteLine($"there is no Counter with {number} Number !");
                        }

                    }
                    else
                    {
                        Console.WriteLine($"the type of counter is  not  corect !");
                    }

                }
            }
            if (isnamefound == false)
            {
                Console.WriteLine($"there is no Subscriber with {name} name,So we can't delet a counter.");
            }
            {

            }
        }
        static public void ShowNamesSubscriber()
        {
            foreach (Subscriber S in Subscribers)
            {
                Console.WriteLine("Name: " + S.Name);
            }
        }
        static public void ShowNumbersCounters()
        {
        n:
            Console.WriteLine("Enter the type of Counter (home) (commercial):");
            string type = Console.ReadLine();
            if (type == "home")
            {
                foreach (Homecounter H in Homecounters)
                {
                    Console.WriteLine("Number of Counter: " + H.GetNumber());
                }
            }
            else if (type == "commercial")
            {
                foreach (CommercialCounter C in CommercialCounters)
                {
                    Console.WriteLine("Number of Counter: " + C.GetNumber());
                }
            }
            else
            {
                Console.WriteLine("The Type is not corect,try again.");
                goto n;
            }


        }
        static public void CounterStatus()
        {

        }

    }
    abstract class Counter
    {
        protected long CounterNumber;
        protected string address;
        public decimal PreviousBalance = 0;               //
        public DateTime DtListRead;
        public string CounterStatus;
        public long GetNumber()
        {
            return CounterNumber;
        }
        public void printInformation()
        {
            Console.WriteLine($"The counter Number={CounterNumber}");
            Console.WriteLine($"The counter Address={address}");
            Console.WriteLine($"The Previous Balance ={PreviousBalance}");
            Console.WriteLine($"The List read Date ={DtListRead}");
            Console.WriteLine($"The Counter Status={CounterStatus}");
        }
        public Counter(long CounterNumber, string Address)
        {
            this.CounterNumber = CounterNumber;
            address = Address;
            CounterStatus = "continuous";
            DtListRead = DateTime.Now;
        }
        public Counter() { }
        public void Paying()
        {
            int dayes;
            DateTime now = DateTime.Now;
            if (DtListRead.Month == now.Month)
            {
                int year = 12 * (now.Year - DtListRead.Year);
                dayes = now.Day - DtListRead.Day + 30 * year;
            }
            else if (DtListRead.Month < now.Month)
            {
                int year = 12 * (now.Year - DtListRead.Year);
                int Farg = now.Month - DtListRead.Month;
                dayes = 30 * (Farg - 1) + (30 - DtListRead.Day) + now.Day + 30 * year;
            }
            else
            {
                int year = 12 * ((now.Year - DtListRead.Year) - 1);
                int Farg = (12 - DtListRead.Month) + now.Month;
                dayes = 30 * (Farg - 1) + (30 - DtListRead.Day) + now.Day + 30 * year;
            }
            if (dayes < 30)
            {
                if (PreviousBalance != 0)
                {
                    Console.WriteLine($"your PreviousBalance ={PreviousBalance} from last date: {DtListRead}");
                o:
                    Console.WriteLine("enter the money :");
                    decimal money = decimal.Parse(Console.ReadLine());
                    if (money > PreviousBalance)
                    {
                        Console.WriteLine($"you shoud pay {PreviousBalance} or less than that.");
                        goto o;
                    }
                    PreviousBalance -= money;
                }
                else
                {
                    Console.WriteLine($"your PreviousBalance is paied from last date: {DtListRead}\nyour PreviousBalance ={PreviousBalance}");
                }

            }
            else
            {
                Console.WriteLine("You have the invoice you didn't print them yet, print them first please.");
            }



        }
    }
    class Homecounter : Counter
    {
        public void SearchHomeCounter(List<Homecounter> Homecounters)
        {
        pp:
            Console.WriteLine("enter the number of counter:");
            string num = Console.ReadLine();
            if (num.Length == 6)
            {
                bool isfound = false;
                foreach (Homecounter H in Homecounters)
                {
                    if (H.CounterNumber == int.Parse(num))
                    {
                        Console.WriteLine("is found");
                        H.printInformation();
                        isfound = true;
                    }
                }
                if (isfound == false)
                {
                    Console.WriteLine($"there is no counter with {num} number");
                }
            }
            else
            {
                Console.WriteLine("The number of counter not corect, try again.");
                goto pp;
            }
        }
        public Homecounter(long CounterNumber, string Address) : base(CounterNumber, Address)
        {

        }
        public Homecounter() { }
        public void Printinvoice(Homecounter h)
        {
            Console.WriteLine("Enter your full name of Subscriber:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the Previous Reading of Counter:");
            int PreviousMeterReading = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the Current Reading of Counter:");
            int CurrentMeterReading = int.Parse(Console.ReadLine());
            int MonthlyConsumption = CurrentMeterReading - PreviousMeterReading;
            int WaterValue = 0;
            int SanitationFees = 0;
            int num = 1;
            while (MonthlyConsumption != 0)
            {
                if (MonthlyConsumption >= 10)
                {
                    MonthlyConsumption -= 10;
                    switch (num)
                    {
                        case 1:
                            WaterValue += (10 * 100);
                            SanitationFees += (10 * 70);
                            break;
                        case 2:
                            WaterValue += (10 * 120);
                            SanitationFees += (10 * 84);
                            break;
                        case 3:
                            WaterValue += (10 * 140);
                            SanitationFees += (10 * 98);
                            break;
                        case 4:
                            WaterValue += (10 * 160);
                            SanitationFees += (10 * 112);
                            break;
                    }

                }
                else if (MonthlyConsumption < 10)
                {
                    int s = MonthlyConsumption;
                    MonthlyConsumption = -MonthlyConsumption;
                    switch (num)
                    {
                        case 1:
                            WaterValue += (s * 100);
                            SanitationFees += (10 * 70);
                            break;
                        case 2:
                            WaterValue += (s * 120);
                            SanitationFees += (10 * 84);
                            break;
                        case 3:
                            WaterValue += (s * 140);
                            SanitationFees += (10 * 98);
                            break;
                        case 4:
                            WaterValue += (s * 160);
                            SanitationFees += (10 * 112);
                            break;
                    }
                }
                num++;
            }
            decimal Previous = h.PreviousBalance;
            int MeterRent = 500;
            int LocalCouncils = 48;
            decimal DeservedAmount = WaterValue + SanitationFees + Previous + MeterRent + LocalCouncils;
            Console.WriteLine("            WATER BILL          ");
            Console.WriteLine($"Invoice\n\nLocation {h.address}\nWanted By Brother {name}\nHome counter:\n Number:{h.GetNumber()}\n Cycle from:{h.DtListRead} to:{h.DtListRead.AddMonths(1)}");
            Console.WriteLine("==================================================================");
            Console.WriteLine($"WaterValue   SanitationFees   Previous     MeterRent     LocalCouncils");
            Console.WriteLine($"  {WaterValue}   {SanitationFees}   {Previous}    {MeterRent}    {LocalCouncils}");
            Console.WriteLine($"DeservedAmount = {DeservedAmount}");
            h.DtListRead = h.DtListRead.AddMonths(1);



        }
        public Homecounter AddHomeCounter(long Counternumber)
        {
            Console.WriteLine("enter the address/location :");
            string address = Console.ReadLine();
            Homecounter H = new Homecounter(Counternumber, address);
            return H;
        }
    }
    class CommercialCounter : Counter
    {
        public CommercialCounter(long CounterNumber, string Address) : base(CounterNumber, Address)
        {

        }
        public CommercialCounter() { }
        public void Printinvoice(CommercialCounter C)
        {
            Console.WriteLine("Enter your full name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the Previous Reading of Counter:");
            int PreviousMeterReading = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the Current Reading of Counter:");
            int CurrentMeterReading = int.Parse(Console.ReadLine());
            int MonthlyConsumption = CurrentMeterReading - PreviousMeterReading;
            int WaterValue = 500 * MonthlyConsumption;
            int SanitationFees = WaterValue * (70 / 100);
            decimal Previous = C.PreviousBalance;
            int MeterRent = 1000;
            int LocalCouncils = 100;
            decimal DeservedAmount = WaterValue + SanitationFees + Previous + MeterRent + LocalCouncils;
            Console.WriteLine("            WATER BILL          ");
            Console.WriteLine($"Invoice\n\nLocation {C.address}\nWanted By Brother {name}\nCommercial Counter\\n Number:{C.GetNumber()}\n Cycle from:{C.DtListRead} to:{C.DtListRead.AddMonths(1)}");
            Console.WriteLine("==================================================================");
            Console.WriteLine($"WaterValue   SanitationFees   Previous     MeterRent     LocalCouncils");
            Console.WriteLine($"  {WaterValue}   {SanitationFees}   {Previous}    {MeterRent}    {LocalCouncils}");
            Console.WriteLine($"DeservedAmount = {DeservedAmount}");
            C.DtListRead = C.DtListRead.AddMonths(1);

        }
        public CommercialCounter AddCommercialCounter(long Counternumber)
        {
            Console.WriteLine("enter the address/location :");
            string address = Console.ReadLine();
            CommercialCounter H = new CommercialCounter(Counternumber, address);
            return H;
        }
        public void SearchCommercialCounter(List<CommercialCounter> CommercialCounters)
        {
        po:
            Console.WriteLine("enter the number of counter:");
            string num = Console.ReadLine();
            if (num.Length >= 6)
            {
                bool isfound = false;
                foreach (CommercialCounter H in CommercialCounters)
                {
                    if (H.CounterNumber == int.Parse(num))
                    {
                        Console.WriteLine("is found");
                        H.printInformation();
                        isfound = true;
                    }
                }
                if (isfound == false)
                {
                    Console.WriteLine($"there is no counter with {num} number");
                }
            }
            else
            {
                Console.WriteLine("The number of counter not corect, try again.");
                goto po;
            }
        }
    }
    class Subscriber
    {
        public string Name;
        string phonNumber;
        public string PhonNumber
        {
            get { return phonNumber; }
            set
            {
                while (value.Length != 9)
                {
                    Console.Write("wrong number try agin: ");
                    value = Console.ReadLine();
                }
                phonNumber = value;
            }
        }
        public int NumberCounters;
        public Dictionary<long, string> counters_of_Subscriber = new Dictionary<long, string>();
        public Subscriber() { }
        public Subscriber(string name, string phonNumber)
        {
            Name = name;
            PhonNumber = phonNumber;
        }
        public void printInformation()
        {
            Console.WriteLine($"The Name of Subscriber: {Name}");
            Console.WriteLine($"The Phon number of Subscriber: {PhonNumber}");
            Console.WriteLine($"The number of counters: {NumberCounters}");
            Console.WriteLine($"The Owner counters:");
            Console.WriteLine($"counter type:          counter number:");
            foreach (var pair in counters_of_Subscriber)
            {
                Console.WriteLine(pair.Value + "              " + pair.Key);
            }


        }

    }
}



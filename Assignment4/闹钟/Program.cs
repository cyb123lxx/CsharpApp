using System;

class Clock
{
    public event EventHandler Tick;
    public event EventHandler Alarm;

    public void Start()
    {
        while (true)
        {
            System.Threading.Thread.Sleep(1000);
            Tick?.Invoke(this, EventArgs.Empty);

            if (DateTime.Now.Hour == 7 && DateTime.Now.Minute == 0)
            {
                Alarm?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Clock clock = new Clock();

        clock.Tick += (sender, e) => Console.WriteLine("Tick");
        clock.Alarm += (sender, e) => Console.WriteLine("Alarm");

        clock.Start();
    }
}

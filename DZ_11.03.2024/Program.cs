/* Реализовать программу “Строительство дома”
 Реализовать:
	 Классы
		■	House(Дом), Basement(Фундамент), Walls(Стены), Door(Дверь), Window(Окно), Roof(Крыша);
		■	Team(Бригада строителей), Worker(Строитель), TeamLeader(Бригадир).
   Интерфейсы:
		■	IWorker, IPart.
	Все части дома должны реализовать интерфейс IPart (Часть дома), для рабочих и бригадира предоставляется
	базовый интерфейс IWorker (Рабочий). Бригада строителей (Team) строит дом (House). Дом состоит из
	фундамента (Basement), стен (Wall), окон (Window), дверей (Door), крыши (Part).
	Согласно проекту, в доме должно быть 1 фундамент,  4 стены, 1 дверь, 4 окна, 1 крыша.
  Бригада начинает работу, и строители последовательно "строят" дом, начиная с фундамента. Каждый строитель
  не знает заранее, на чём завершился предыдущий этап строительства, поэтому он "проверяет", что уже построено
 и продолжает работу. Если в игру вступает бригадир (TeamLeader), он не строит, а формирует отчёт,
  что уже построено и какая часть работы выполнена.
	В конечном итоге на консоль выводится сообщение, что строительство дома завершено
	и отображается "рисунок дома" (вариант отображения выбрать самостоятельно).*/

using static System.Console;

interface IPart
{
    string Name { get; set; }
}

interface IWorker
{
    void Build(IPart part);
}

class Basement : IPart
{
    public string Name { get; set; } = "Фундамент";
}

class Walls : IPart
{
    public string Name { get; set; } = "Стена";
}

class Door : IPart
{
    public string Name { get; set; } = "Дверь";
}

class Window : IPart
{
    public string Name { get; set; } = "Окно";
}

class Roof : IPart
{
    public string Name { get; set; } = "Крыша";
}

class Team
{
    private List<IWorker> workers = new List<IWorker>();

    public void AddWorker(IWorker worker)
    {
        workers.Add(worker);
    }

    public void BuildHouse()
    {
        House house = new House();
        foreach (var worker in workers)
        {
            worker.Build(house.GetNextPart());
        }
        house.ShowHouse();
    }
}

class Worker : IWorker
{
    public void Build(IPart part)
    {
        WriteLine($"Строитель строит {part.Name}");
    }
}

class TeamLeader : IWorker
{
    public void Build(IPart part)
    {
        WriteLine($"Бригадир проверяет работу. {part.Name} уже построено.");
    }
}

class House
{
    private Queue<IPart> parts = new Queue<IPart>();

    public House()
    {
        parts.Enqueue(new Basement());
        parts.Enqueue(new Walls());
        parts.Enqueue(new Walls());
        parts.Enqueue(new Walls());
        parts.Enqueue(new Walls());
        parts.Enqueue(new Door());
        parts.Enqueue(new Window());
        parts.Enqueue(new Window());
        parts.Enqueue(new Window());
        parts.Enqueue(new Window());
        parts.Enqueue(new Roof());
    }

    public IPart GetNextPart()
    {
        return parts.Dequeue();
    }

    public void ShowHouse()
    {
        WriteLine("\nДом построен!");
        WriteLine("  _____");
        WriteLine(" /     \\");
        WriteLine("/_______\\");
        WriteLine(" |  _  |");
        WriteLine(" | | | |");
        WriteLine(" | | | |");
        WriteLine(" | | | |");
        WriteLine(" |_|_|_|");
        WriteLine(" |_____|");
    }
}

class Program
{
    static void Main()
    {
        Team team = new Team();
        team.AddWorker(new Worker());

        team.AddWorker(new Worker());
        team.AddWorker(new Worker());
        team.AddWorker(new Worker());

        team.AddWorker(new TeamLeader());
        team.AddWorker(new Worker());

        team.AddWorker(new Worker());
        team.AddWorker(new Worker());
        team.AddWorker(new Worker());
        team.AddWorker(new TeamLeader());

        team.AddWorker(new Worker());

        team.BuildHouse();
    }
}
// See https://aka.ms/new-console-template for more information
using YieldReturn;

Console.WriteLine("Hello, World!");
ProcessPeople();


static void ProcessPeople()
{
    var people = GetPeople(1_000_000);

    // Only when people is being accessed GetPeople() will be called
    // We lazily iterate over the GetPeople() method
    // Thereby we only need to generate 1000 ppl instead of 1 million
    foreach (var person in people)
    {
        if (person.Id < 1000)
        {
            Console.WriteLine(person.Name);
        } else
        {
            break;
        }
    }
}

static IEnumerable<Person> GetPeople(int count)
{
    for (int i = 0; i < count; i++)
    {
        yield return new Person { Id = i,  Name = $"Person {i}" };
    }
}   
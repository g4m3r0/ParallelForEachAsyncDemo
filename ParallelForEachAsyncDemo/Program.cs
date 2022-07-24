// ForEachAsync
// https://peterdaugaardrasmussen.com/2021/11/13/csharp-how-to-use-parallel-foreachasync/

// How to: Cancel a Parallel.For or ForEach Loop
// https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-cancel-a-parallel-for-or-foreach-loop

var list = new List<(string name, int waitTime)>{
    ("Item1",200),
    ("Item2",500),
    ("Item3",1000),
    ("Item4",1500),
    ("Item5",5000),
    ("Item6",20),
    ("Item7",50),
    ("Item8",100),
    ("Item9",150),
    ("Item10",500),
};

CancellationTokenSource cts = new CancellationTokenSource();

// Use ParallelOptions instance to store the CancellationToken
ParallelOptions po = new ParallelOptions();
po.CancellationToken = cts.Token;
po.MaxDegreeOfParallelism = System.Environment.ProcessorCount;


Task.Run(() =>
{
    Thread.Sleep(2000);
    cts.Cancel();
});


try
{
    Parallel.ForEach(list, po, (item, cancellationToken) =>
    {
        Console.WriteLine($"Called for {item.name}, will wait {item.waitTime} ms");
        //Task.Delay(item.waitTime * 10);
        Thread.Sleep(item.waitTime);
        Console.WriteLine($"Done handling {item.name}");
    });

    //await Parallel.ForEachAsync(list, po, async (item, cancellationToken) =>
    //{
    //    Console.WriteLine($"Called for {item.name}, will wait {item.waitTime} ms");
    //    await Task.Delay(item.waitTime);
    //    Console.WriteLine($"Done handling {item.name}");
    //});
}
catch (OperationCanceledException e)
{
    Console.WriteLine(e.Message);
}
finally
{
    cts.Dispose();
}
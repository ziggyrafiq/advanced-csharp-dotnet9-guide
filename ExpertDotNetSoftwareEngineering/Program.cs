using System.Collections.Immutable;

Console.WriteLine("Hello, from Ziggy Rafiq");

var numbers = ImmutableArray.CreateRange(Enumerable.Range(1, 1000000));

// Use parallel processing to calculate squares without modifying the original array
var squares = numbers.AsParallel().Select(x => x * x).ToImmutableArray();

// Output first ten squares
Console.WriteLine(string.Join(", ", squares.Take(10)));

using ChipsDistribution;

var chipsCounts = InputValidation.GetValidatedInput().ToList();
var result = Distrubutor.DistributeChips(chipsCounts);

Console.WriteLine($"Total iterations of distribution: {result}");
namespace FluentTesting.Sample.PrimeService;

public class PrimeService
{
    public static bool IsPrime(int candidate)
    {
        if (candidate < 2)
        {
            return false;
        }

        for (var divisor = 2; divisor <= Math.Sqrt(candidate); divisor++)
        {
            if (candidate % divisor == 0)
            {
                return false;
            }
        }
        return true;
    }
}
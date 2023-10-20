
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Meal.Infrastructure.ExecutionStrategies
{
    public class EFCoreCustomRetryExecutionStrategy : ExecutionStrategy
    {
        int retryCount = 0;

        public EFCoreCustomRetryExecutionStrategy(DbContext context, int maxRetryCount, TimeSpan maxRetryDelay) : base(context, maxRetryCount, maxRetryDelay)
        {
        }

        public EFCoreCustomRetryExecutionStrategy(ExecutionStrategyDependencies dependencies, int maxRetryCount, TimeSpan maxRetryDelay) : base(dependencies, maxRetryCount, maxRetryDelay)
        {
        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            Console.WriteLine($"{++retryCount}. Try to Connect...");
            return true;
        }
    }
}

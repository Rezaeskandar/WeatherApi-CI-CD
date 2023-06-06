using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApi_XUTest
{
    public class APICounter
    {
        private int apiCallCount;

        public APICounter(int initialCount)
        {
            apiCallCount = initialCount;
        }

        public async Task IncrementAPICallAsync()
        {
            await Task.Run(() => Interlocked.Increment(ref apiCallCount));
        }

        public async Task<string> GetStatisticsAsync()
        {
            return await Task.Run(() => $"Number of API calls: {apiCallCount}");
        }

        public int GetAPICallCount()
        {
            return apiCallCount;
        }
    }
}

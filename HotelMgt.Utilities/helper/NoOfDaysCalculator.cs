using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Utilities.helper
{
    public static class NoOfDaysCalculator
    {
        public static int GetNumberOfDays(this DateTime dateTimeOffset)
        {
            var currentDate = DateTime.UtcNow;
            int days = currentDate.Day - dateTimeOffset.Day;

            if (currentDate < dateTimeOffset.AddDays(days))
            {
                days--;
            }

            return days;
        }
    }
}

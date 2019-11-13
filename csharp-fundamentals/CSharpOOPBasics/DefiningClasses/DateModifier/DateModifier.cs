using System;
using System.Collections.Generic;
using System.Text;

namespace DefiningClasses
{
    public class DateModifier
    {
        public double Difference { get; set; }

        public void CalculateDifference(string date1, string date2)
        {
            DateTime dateOne = DateTime.Parse(date1);
            DateTime dateTwo = DateTime.Parse(date2);

            if (dateOne > dateTwo)
            {
                this.Difference = (dateOne - dateTwo).TotalDays;
            }
            else
            {
                this.Difference = (dateTwo - dateOne).TotalDays;
            }
        }
    }
}

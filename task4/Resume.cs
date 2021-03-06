using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    class Resume
    {
      public string Name;
      public string Surname;
      public int NumberOfYearsOfExperience;
      public string City;
      public int salaryRequirement;

        public override string ToString()
        {
            return $"Name: {Name} Surname: {Surname} City: {City} Years Of Experience: {NumberOfYearsOfExperience} salary Requirement: {salaryRequirement}";
        }
    }
}

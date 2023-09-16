using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TimeManagementApp
{
    public class Module
    {
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public int NumberOfCredits { get; set; }
        public int ClassHoursPerWeek { get; set; }
        public int SelfStudyHoursPerWeek { get; set; }

        public Module(string moduleCode, string moduleName, int numberOfCredits, int classHoursPerWeek, int selfStudyHoursPerWeek)
        {
            ModuleCode = moduleCode;
            ModuleName = moduleName;
            NumberOfCredits = numberOfCredits;
            ClassHoursPerWeek = classHoursPerWeek;
            SelfStudyHoursPerWeek = selfStudyHoursPerWeek;
        }
        public override string ToString()
        {
            return $"Module Code: {ModuleCode}\nModule Name: {ModuleName}\nNumber of Credits: {NumberOfCredits}\nClass Hours Per Week: {ClassHoursPerWeek}\n";
        }
    }
}

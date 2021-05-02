using System;

namespace DissertationArtefact.Shared
{

    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class Income
    {
        public Income()
        {

        }

        public Guid? IncomeID { get; set; }
        public string LabelName { get; set; }
        public double Amount { get; set; }
        public string SenderName { get; set; }
        public DateTime PaymentDate { get; set; }
        public IncomeFrequencies Frequency { get; set; }

    }

    public enum IncomeFrequencies {
        OneTime,
        Weekly,
        Monthly,
        Annually

    }
}
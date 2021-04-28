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

        public string LabelName { get; set; }
        public string Amount { get; set; }
        public string SenderName { get; set; }
        public string PaymentDate { get; set; }
        public Occurances Occurance { get; set; }

    }

    public enum Occurances { 
    oneTime,
    weekly,
    monthly,
    yearly

    }
}
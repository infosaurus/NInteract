using System;

namespace NInteract.Tests.Collaboration
{
    public interface IAssistant
    {
        void PrepareADrink();
        void ServeDrink();
        void CallACab();
        void BuySandwiches(int number);
        object GetVegasPlaneSchedule(object timeFrame);
        void BookPlaneToVegas();
        object GiveNextAppointment();
        void MakeCoffee(Coffee coffee, int nbSugars, bool withMilk);
        void PrintAutographs(int number);
        object GiveCreditCard();
        DateTime GetMasseurNextAvailability(int duration);
        decimal GiveTotalFee(decimal hourlyRate, bool includesAccommodation, CalculationPolicy calculationPolicy);
        void AddAddressBookEntry(ContactInfo contactInfo);

        string Goo { get; set; }
    }

    public class Star
    {
        private readonly IAssistant _assistant;

        public Star(IAssistant assistant)
        {
            _assistant = assistant;
        }

        public void Thirsty()
        {
            _assistant.PrepareADrink();
            _assistant.ServeDrink();
        }

        public void Hungry()
        {
            _assistant.BuySandwiches(3);
        }

        public void Tired()
        {
            _assistant.GetVegasPlaneSchedule(null);
        }

        public void WokeUp()
        {
            _assistant.MakeCoffee(Expresso.Instance, 2, false);
        }

        public void SignAutographs(int number)
        {
            _assistant.PrintAutographs(number);
        }

        public void Bored()
        {
            _assistant.GiveNextAppointment();
        }

        public void BackAches()
        {
            _assistant.GetMasseurNextAvailability(2);
        }

        public void NeedsSomeFun()
        {
            _assistant.GetVegasPlaneSchedule(null);
            _assistant.BookPlaneToVegas();
        }

        public void SignContract()
        {
            _assistant.GiveTotalFee(1000, false, VatInclusiveCalculationPolicy.Instance);
        }

        public void FriendWantsCoffee(Coffee coffee, int nbSugars, bool addMilk)
        {
            _assistant.MakeCoffee(coffee, nbSugars, addMilk);
        }

        public void JustMet(string name, int phoneNumber)
        {
            _assistant.AddAddressBookEntry(new ContactInfo(name, phoneNumber));
        }
    }

    public abstract class Coffee { }

    public class Expresso : Coffee
    {
      private static readonly Lazy<Expresso> _lazy =
        new Lazy<Expresso>(() => new Expresso());
    
        public static Expresso Instance { get { return _lazy.Value; } }

        private Expresso()
        {
        }
    }

    public abstract class CalculationPolicy
    {
    }

    public class VatInclusiveCalculationPolicy : CalculationPolicy
    {
        private static readonly Lazy<VatInclusiveCalculationPolicy> _lazy =
        new Lazy<VatInclusiveCalculationPolicy>(() => new VatInclusiveCalculationPolicy());
    
        public static VatInclusiveCalculationPolicy Instance { get { return _lazy.Value; } }

        private VatInclusiveCalculationPolicy()
        {
        }
    }

    public class ContactInfo
    {
        public string Name { get; set; }
        public int PhoneNumber { get; set; }

        public ContactInfo(string name, int phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}

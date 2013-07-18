using System;

namespace Ninteract.Tests.Collaboration
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
        DateTime? GetMasseurNextAvailability(int duration);
        decimal GiveTotalFee(decimal hourlyRate, bool includesAccommodation, CalculationPolicy calculationPolicy);
        void AddAddressBookEntry(ContactInfo contactInfo);
        void PayBill(Bill bill);
        int WithdrawCash(int amount);
        void BuyPen();
        void Fired();
        void BuySomeSugar();

        object Pen { get; set; }
        bool IsHungry { get; set; }
        decimal Salary { get; set; }
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
            var assistantHungryToo = _assistant.IsHungry;
            try
            {
                _assistant.BuySandwiches(3);
            }
            catch (OutOfCashException)
            {
                _assistant.WithdrawCash(50);
            }
            try
            {
                _assistant.IsHungry = false;
            }
            catch (StillHungryException)
            {
                _assistant.BuySandwiches(1);
            }
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
            object pen = null;
            try
            {
                pen = _assistant.Pen;
            }
            catch (OutOfInkException)
            {
                _assistant.BuyPen();
            }
            if (pen == null)
                _assistant.PrintAutographs(number);
        }

        public void Bored()
        {
            if (_assistant.GiveNextAppointment() == null)
                if (_assistant.GetMasseurNextAvailability(2) == null)
                    _assistant.PrepareADrink();
        }

        public void BackAches()
        {
            if (_assistant.GetMasseurNextAvailability(2) > DateTime.Now.AddDays(2))
            {
                throw new OutOfControlException();
            }
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
            try
            {
                _assistant.MakeCoffee(coffee, nbSugars, addMilk);
            }
            catch (OutOfSugarException)
            {
                _assistant.BuySomeSugar();
            }
        }

        public void JustMet(string name, int phoneNumber)
        {
            _assistant.AddAddressBookEntry(new ContactInfo(name, phoneNumber));
        }

        public void PayBill(Bill bill)
        {
            try
            {
                _assistant.PayBill(bill);
            }
            catch (OutOfCashException)
            {
                try
                {
                    _assistant.WithdrawCash(500);
                }
                catch (BankruptException)
                {
                    _assistant.Fired();
                }
            }
        }

        public void Generous()
        {
            _assistant.Salary = _assistant.Salary * 1.05m;
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

    public class Bill
    {
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

    public class OutOfControlException : Exception
    {
    }

    public class OutOfCashException : Exception
    {
    }

    public class BankruptException : Exception
    {
    }   
    
    public class OutOfInkException : Exception
    {
    }

    public class StillHungryException : Exception
    {
    }

    public class OutOfSugarException : Exception
    {
    }

    public class Assistant : IAssistant
    {
        private readonly INoteBook _noteBook;

        public Assistant(INoteBook noteBook)
        {
            _noteBook = noteBook;
        }

        public void PrepareADrink()
        {
            throw new NotImplementedException();
        }

        public void ServeDrink()
        {
            throw new NotImplementedException();
        }

        public void CallACab()
        {
            throw new NotImplementedException();
        }

        public void BuySandwiches(int number)
        {
            throw new NotImplementedException();
        }

        public object GetVegasPlaneSchedule(object timeFrame)
        {
            throw new NotImplementedException();
        }

        public void BookPlaneToVegas()
        {
            throw new NotImplementedException();
        }

        public object GiveNextAppointment()
        {
            return _noteBook.NextAppointment();
        }

        public void MakeCoffee(Coffee coffee, int nbSugars, bool withMilk)
        {
            throw new NotImplementedException();
        }

        public void PrintAutographs(int number)
        {
            throw new NotImplementedException();
        }

        public object GiveCreditCard()
        {
            throw new NotImplementedException();
        }

        public DateTime? GetMasseurNextAvailability(int duration)
        {
            throw new NotImplementedException();
        }

        public decimal GiveTotalFee(decimal hourlyRate, bool includesAccommodation, CalculationPolicy calculationPolicy)
        {
            throw new NotImplementedException();
        }

        public void AddAddressBookEntry(ContactInfo contactInfo)
        {
            throw new NotImplementedException();
        }

        public void PayBill(Bill bill)
        {
            throw new NotImplementedException();
        }

        public int WithdrawCash(int amount)
        {
            throw new NotImplementedException();
        }

        public void BuyPen()
        {
            throw new NotImplementedException();
        }

        public void Fired()
        {
            throw new NotImplementedException();
        }

        public void BuySomeSugar()
        {
            throw new NotImplementedException();
        }

        public object Pen { get; set; }
        public bool IsHungry { get; set; }
        public decimal Salary { get; set; }
    }

    public interface INoteBook
    {
        object NextAppointment();
    }
}

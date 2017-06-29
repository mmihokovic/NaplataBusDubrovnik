using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
   public static class SubscriberController
    {
       public static void AddSubscriber(string licencePlates, DateTime validTo)
       {

           int months = 0;

           if (((validTo - DateTime.Now).Days / 31.0) == 0)
           {
               months = 1;
               validTo = DateTime.Now;
           }
           else
           {
               months = Convert.ToInt32(Math.Ceiling((validTo - DateTime.Now).Days / 31.0));
               validTo = DateTime.Now;
           }
           DateTime newValidTo = validTo.AddMonths(months);

           Database.Subscribers.AddSubscriber(licencePlates, newValidTo);

           if (months > 0)
           {
               ChargeSubscriberController.ChargeSubscription(licencePlates, months, newValidTo);

           }
       }

       public static Subscriber GetSubscriber(string licencePlates)
       {
           return Database.Subscribers.GetSubscriber(licencePlates);
       }

       public static void UpdateSubscriber(Subscriber subscriber)
       {
           Subscriber currentSubscriber = GetSubscriber(subscriber.LicencePlates);
           DateTime oldSubscription = currentSubscriber.ValidTo;
           int months = Convert.ToInt32(Math.Ceiling((subscriber.ValidTo - oldSubscription).Days / 31.0));
           DateTime validTo = currentSubscriber.ValidTo.AddMonths(months);
           currentSubscriber.ValidTo = validTo;

           if (months > 0)
           {
               ChargeSubscriberController.ChargeSubscription(subscriber.LicencePlates, months, validTo);
               Database.Subscribers.UpdateSubscriber(currentSubscriber);
               
           }
           else
           {
               throw new Exception("Pretplata se ne može ukinuti.");
           }
       }

       public static void RemoveSubscriber(string licencePlates)
       {
           Database.Subscribers.RemoveSubscriber(licencePlates);
       }

       public static List<Subscriber> GetAllSubscribers()
       {
           return Database.Subscribers.GetAllSubscribers();
       }

       public static List<Subscriber> GetAllValidSubscribers()
       {
           List<Subscriber> subscribers = GetAllSubscribers();
           DateTime now = DateTime.Now;
           List<Subscriber> validSubscribers = new List<Subscriber>();
           foreach(Subscriber s in subscribers){
               if(s.ValidTo >= now){
                   validSubscribers.Add(s);
               }
           }
           return validSubscribers;
       }
    }
}

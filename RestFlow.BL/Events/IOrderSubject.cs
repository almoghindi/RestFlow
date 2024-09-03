using RestFlow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.BL.Observer
{
    public interface IOrderSubject
    {
        void RegisterObserver(IOrderObserver observer);
        void RemoveObserver(IOrderObserver observer);
        void NotifyObservers(Order order);
    }
}

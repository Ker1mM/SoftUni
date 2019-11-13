using Heroes.Contracts;

namespace Observer.Contracts
{
    public interface ISubject
    {
        void Register(IObserver observer);
        void Unregister(IObserver observer);
        void NotifyObservers();
    }
}

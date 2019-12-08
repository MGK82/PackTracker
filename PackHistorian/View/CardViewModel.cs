using System.ComponentModel;
using System.Runtime.CompilerServices;
using HDTCard = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace PackTracker.View
{
    public class CardViewModel : INotifyPropertyChanged
    {
        HDTCard _card;
        bool _premium;

        public HDTCard HDTCard
        {
            get { return _card; }
            set
            {
                _card = value;
                OnPropertyChanged();
            }
        }

        public bool Premium
        {
            get { return _premium; }
            set
            {
                _premium = value;
                OnPropertyChanged();
            }
        }

        public CardViewModel(HDTCard Card, bool premium)
        {
            _card = Card;
            _premium = premium;
        }

        public CardViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Hearthstone_Deck_Tracker.Utility;
using PackTracker.Entity;
using PackTracker.Storage;
using HDTCard = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace PackTracker.View
{
    class ManualPackInsert : INotifyPropertyChanged
    {
        private readonly History _history;
        private readonly IHistoryStorage _historyStorage;
        DateTime? _selectedDateTime;
        private List<int> _sets;
        private int _selectedSet;
        private ObservableCollection<HDTCard> _cardsInCurrentSet = new ObservableCollection<HDTCard>();

        private ObservableCollection<CardViewModel> _packCards = new ObservableCollection<CardViewModel>();

        public DateTime? SelectedDateTime
        {
            get => _selectedDateTime;
            set
            {
                _selectedDateTime = value;
                OnPropertyChanged("SelectedDateTime");
            }
        }

        public int SelectedSet
        {
            get => _selectedSet;
            set
            {
                _selectedSet = value;
                RefreshCardsInCurrentSet();
                OnPropertyChanged("SelectedSet");
            }
        }

        public List<int> Sets
        {
            get => _sets;
            set
            {
                _sets = value;
                OnPropertyChanged("Sets");
            }
        }

        public ObservableCollection<HDTCard> CardsInCurrentSet
        {
            get { return _cardsInCurrentSet; }
            set
            {
                _cardsInCurrentSet = value;
                OnPropertyChanged("CardsInCurrentSet");
            }
        }

        public ObservableCollection<CardViewModel> PackCards
        {
            get { return _packCards; }
            set
            {
                _packCards = value;
                OnPropertyChanged("PackCards");
            }
        }

        public bool AddNewPackEnabled
        {
            get =>
                SelectedDateTime != null && PackCards.All(c => c.HDTCard != null);
        }

        readonly Dictionary<int, List<HDTCard>> _setsCache = new Dictionary<int, List<HDTCard>>();

        public ManualPackInsert()
        {
            _selectedDateTime = DateTime.Now;

            _sets = PackNameConverter.PackNames.Select(set => set.Key).ToList();

            SelectedSet = _sets.FirstOrDefault();

            ResetPackCards();
        }

        public ManualPackInsert(History History) : this()
        {
            _history = History;
            _historyStorage = new XmlHistory();
        }

        private void RefreshCardsInCurrentSet()
        {
            CardsInCurrentSet.Clear();

            if (!_setsCache.ContainsKey(SelectedSet))
            {
                _setsCache[SelectedSet] = HearthDb.Cards.Collectible.Values
                    .Where(card => card.Set == SetProvider.PackSets[SelectedSet])
                    .OrderBy(card => card.Rarity)
                    .ThenBy(card => card.Cost)
                    .ThenBy(card => card.Name)
                    .Select(card => new HDTCard(card))
                    .ToList();
            }

            _setsCache[SelectedSet].ForEach(c => CardsInCurrentSet.Add(c));
        }

        private ICommand _addNewPackCommand;

        public ICommand AddNewPackCommand
        {
            get
            {
                if (_addNewPackCommand == null)
                {
                    _addNewPackCommand = new Command(AddNewPack);
                }
                return _addNewPackCommand;
            }
        }

        private void AddNewPack()
        {
            List<Entity.Card> Cards = new List<Entity.Card>();

            if (SelectedDateTime != null && PackCards.All(c => c.HDTCard != null))
            {
                PackCards.ToList().ForEach(c => Cards.Add(new Entity.Card(c.HDTCard, c.Premium)));

                var newPack = new Pack(SelectedSet, (DateTime)SelectedDateTime, Cards);

                _history.Add(newPack);
                _historyStorage.Store(_history.Ascending);

                ClearData();
            }
        }

        private void ResetPackCards()
        {
            PackCards.Clear();

            Enumerable.Range(1, 5).ToList().ForEach(i => PackCards.Add(new CardViewModel()));

            foreach (CardViewModel cardViewModel in PackCards)
            {
                cardViewModel.PropertyChanged += CardViewModel_PropertyChanged;
            }
        }

        private void CardViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CardViewModel.HDTCard))
            {
                OnPropertyChanged(nameof(AddNewPackEnabled));
            }
        }

        private void ClearData()
        {
            SelectedDateTime = DateTime.Now;

            ResetPackCards();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
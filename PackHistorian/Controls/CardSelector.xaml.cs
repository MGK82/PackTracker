using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.Hearthstone;
using HDTCard = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace PackTracker.Controls
{
    /// <summary>
    /// Logica di interazione per CardSelector.xaml
    /// </summary>
    public partial class CardSelector : UserControl
    {
        private Entity.Card _selectedCard;

        public Entity.Card SelectedCard
        {
            get { return _selectedCard; }
            set { _selectedCard = value; }
        }


        public ObservableCollection<HDTCard> SetHdtCards
        {
            get { return (ObservableCollection<HDTCard>)GetValue(SetHdtCardsProperty); }
            set { SetValue(SetHdtCardsProperty, value); }
        }

        public static readonly DependencyProperty SetHdtCardsProperty = DependencyProperty.Register("SetHdtCards", typeof(ObservableCollection<HDTCard>), typeof(CardSelector), null);


        public CardSelector()
        {
            InitializeComponent();
        }
    }
}

using Bingo.Model;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Bingo.ViewModel
{
    public class BingoViewModel : ObservableObject
    {
        public delegate void IsNBingoEventHandelr();
        public event IsNBingoEventHandelr OnIsBingo;

        #region BingoCase Test
        private bool BingoCaseOne = false;
        private bool BingoCaseTwo = false;
        private bool BingoCaseThree = false;
        private bool BingoCaseFour = false;
        private bool BingoCaseFive = false;
        private bool BingoCaseSix = false;
        private bool BingoCaseSeven = false;
        private bool BingoCaseEight = false;
        #endregion

        #region Properties
        private ObservableCollection<BingoModel> _bingoItems;
        public ObservableCollection<BingoModel> BingoItems
        {
            get => _bingoItems;
            set
            {
                _bingoItems = value;
                OnPropertyChanged();
            }
        }

        private BingoModel _selectedBingo;
        public BingoModel SelectedBingo
        {
            get => _selectedBingo;
            set
            {
                _selectedBingo = value;
                OnPropertyChanged();
            }
        }

        private int _inputBingoCount;
        public int InputBingoCount
        {
            get => _inputBingoCount;
            set
            {
                _inputBingoCount = value;
                OnPropertyChanged();
            }
        }

        private int _bingoCount = 0;
        public int BingoCount
        {
            get => _bingoCount;
            set
            {
                _bingoCount = value;
                OnPropertyChanged();
            }
        }

        private Dictionary<List<int>, bool> BingoCaseDictionary;
        private List<List<int>> BingoDirectionItems;
        #endregion

        #region Commands
        public ICommand CanPlayGameCommand { get; set; }
        #endregion

        #region Constructor
        public BingoViewModel()
        {
            InitVariables();
            InitCommands();
            SetBingoItems();
            SetBingoCaseItems();
            SetBingoCaseDictionaries();
        }
        #endregion

        #region Init
        private void InitVariables()
        {
            BingoItems = new ObservableCollection<BingoModel>();
            BingoCaseDictionary = new Dictionary<List<int>, bool>();
            BingoDirectionItems = new List<List<int>>();
        }

        private void InitCommands()
        {
            CanPlayGameCommand = new DelegateCommand(OnBingoSelectionChange, CanBingoSelectionChange);
        }

        private void SetBingoItems()
        {
            for(int i = 1; i <= 9; i++)
            {
                BingoItems.Add(new BingoModel { BingoIdx = i, IsSelected = false });
            }
        }

        private void SetBingoCaseItems()
        {
            BingoDirectionItems.Add(new List<int>() { 0, 1, 2 });
            BingoDirectionItems.Add(new List<int>() { 3, 4, 5 });
            BingoDirectionItems.Add(new List<int>() { 6, 7, 8 });
            BingoDirectionItems.Add(new List<int>() { 0, 3, 6 });
            BingoDirectionItems.Add(new List<int>() { 1, 4, 7 });
            BingoDirectionItems.Add(new List<int>() { 2, 5, 8 });
            BingoDirectionItems.Add(new List<int>() { 0, 4, 8 });
            BingoDirectionItems.Add(new List<int>() { 2, 4, 6 });
        }
        #endregion

        #region Command Methods
        private bool CanBingoSelectionChange()
        {
            return InputBingoCount != 0 ? true : false;
        }

        private void OnBingoSelectionChange()
        {
            SelectedBingo.IsSelected = true;
            IsMatchBingo();
        }
        #endregion

        private void SetBingoCaseDictionaries()
        {
            for(int i = 1; i < 9; i++)
            {
                BingoCaseDictionary.Add(BingoDirectionItems[i - 1], false);
            }
        }

        private void IsMatchBingo()
        {
            foreach(var BingoCase in BingoCaseDictionary.ToList())
            {
                if (BingoCase.Value != true)
                {
                    if(BingoItems[BingoCase.Key[0]].IsSelected && BingoItems[BingoCase.Key[1]].IsSelected && BingoItems[BingoCase.Key[2]].IsSelected)
                    {
                        IncreaseBingoCount();
                        IsNBingo();
                        BingoCaseDictionary[BingoCase.Key] = true;
                    }
                }
            }
        }

        private void IncreaseBingoCount()
        {
            BingoCount += 1;
        }

        private void IsNBingo()
        {
            if (BingoCount == InputBingoCount)
            {
                OnIsBingo?.Invoke();
            }
        }
    }
}

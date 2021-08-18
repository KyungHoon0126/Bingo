using Bingo.Model;
using Prism.Commands;
using System;
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

        private int MAX_BINGO_COUNT = 9;
        private Random random;
        private List<string> BingoContents = new List<string>();
        private Dictionary<List<int>, bool> BingoCaseDictionary;
        private List<List<int>> BingoDirectionItems;
        #endregion

        #region Commands
        public ICommand CanPlayGameCommand { get; set; }
        public ICommand ShuffleBingoItemsCommand { get; set; }
        #endregion

        #region Constructor
        public BingoViewModel()
        {
            InitVariables();
            InitCommands();
            SetBingoContents();
            SetBingoItems();
            SetBingoCaseItems();
            SetBingoCaseDictionaries();
        }
        #endregion

        #region Init
        private void InitVariables()
        {
            BingoItems = new ObservableCollection<BingoModel>();
            random = new Random();
            BingoContents = new List<string>();
            BingoCaseDictionary = new Dictionary<List<int>, bool>();
            BingoDirectionItems = new List<List<int>>();
        }

        private void InitCommands()
        {
            CanPlayGameCommand = new DelegateCommand(OnBingoSelectionChange, CanBingoSelectionChange);
            ShuffleBingoItemsCommand = new DelegateCommand(OnShuffleBingoItems);
        }

        private void SetBingoContents()
        {
            #region Set Dummy BingoContents
            BingoContents.Add("내 책상에는 항상 카페인 음료가 있다");
            BingoContents.Add("주변 사람들로부터 컴퓨터 관련된 질문을 받아본 적있다");
            BingoContents.Add("영타 400타 이상이다");
            BingoContents.Add("노트북에 스티커가 붙어 있다");
            BingoContents.Add("어? 라는 말을 많이 한다");
            BingoContents.Add("기계식 키보드를 갖고 있다");
            BingoContents.Add("할 줄 아는 언어가 3개 이상이다");
            BingoContents.Add("허리가 좋지 않다");
            BingoContents.Add("개발과 게임, 둘 중 선택하라 하면 개발이다");
            BingoContents.Add("큰 백팩을 자주 맨다");
            BingoContents.Add("기능이 없어 만들어 쓴 적 있다");
            BingoContents.Add("버그 잡다가 새로운 버그를 발견한 적 있다");
            BingoContents.Add("후드티를 많이 갖고 있다");
            BingoContents.Add("불금에는 코딩이지");
            BingoContents.Add("개발 개그를 보면 공감간다");
            BingoContents.Add("Ctrl+S를 습관적으로 누른다");
            BingoContents.Add("칼퇴보다 야근이 더 많다");
            BingoContents.Add("일을 시작하고 나서 손목터널증후군이 생겼다");
            BingoContents.Add("적성에 맞는지 진지하게 생각해본 적 있다");
            BingoContents.Add("화려하지만 심플하게라는 말을 들어봤다");
            BingoContents.Add("외주를 받아봤다");
            BingoContents.Add("이름만 부른 메신저가 오면 방어태세부터 갖춘다");
            BingoContents.Add("이상한 폰트보면 기분 나빠진다");
            BingoContents.Add("가끔 친구들이 내가 하는 말을 이해하지 못한다");
            BingoContents.Add("자격증이 3개 이상이다");
            BingoContents.Add("이력서 100개 이상 뿌려 봤다");
            BingoContents.Add("낮과 밤이 바뀌었다");
            BingoContents.Add("코드 리펙토링이 필요하지만 못 본 척 지나친적 있다");
            BingoContents.Add("Pull Request 날렸다가 퇴짜 맞은적이 있다");
            BingoContents.Add("자주 사용하는 개발툴이 있다");
            #endregion
        }

        private void SetBingoItems()
        {
            while(MAX_BINGO_COUNT != 0)
            {
                int randomBingoIdx = random.Next(0, 30);
                if (BingoItems.Where(x => x.BingoContent == BingoContents[randomBingoIdx]).FirstOrDefault() == null)
                {
                    BingoItems.Add(new BingoModel { BingoContent = BingoContents[randomBingoIdx], IsSelected = false });
                    MAX_BINGO_COUNT -= 1;
                }
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

        private void OnShuffleBingoItems()
        {
            if(BingoItems.Count > 0)
            {
                BingoItems.Clear();
            }
            MAX_BINGO_COUNT = 9;
            SetBingoItems();
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

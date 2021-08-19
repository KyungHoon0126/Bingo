using Bingo.Common;
using Bingo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Prism.Commands;

namespace Bingo.ViewModel
{
    public class BingoViewModel : ObservableObject
    {
        public delegate void IsNBingoEventHandelr();
        public event IsNBingoEventHandelr OnIsNBingo;

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

        private int _checkBingoCount = 0;
        public int CheckBingoCount
        {
            get => _checkBingoCount;
            set
            {
                _checkBingoCount = value;
                OnPropertyChanged();
            }
        }

        private Random random;
        private int MAX_BINGO_COUNT = BingoProperties.MAX_BINGO_COUNT;
        private List<string> BingoContents;
        private Dictionary<List<int>, bool> BingoCaseDictionaries;
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

        #region Initialize
        private void InitVariables()
        {
            BingoItems = new ObservableCollection<BingoModel>();
            random = new Random();
            BingoContents = new List<string>();
            BingoCaseDictionaries = new Dictionary<List<int>, bool>();
            BingoDirectionItems = new List<List<int>>();
        }

        private void InitCommands()
        {
            CanPlayGameCommand = new DelegateCommand(OnBingoSelectionChange, CanBingoSelectionChange);
            ShuffleBingoItemsCommand = new DelegateCommand(OnShuffleBingoItems);
        }

        private void SetBingoContents()
        {
            string[] bingoContentItems = new string[] { 
               "내 책상에는 항상 카페인 음료가 있다", "주변 사람들로부터 컴퓨터 관련된 질문을 받아본 적있다", "영타 400타 이상이다", "노트북에 스티커가 붙어 있다", "어? 라는 말을 많이 한다",
               "기계식 키보드를 갖고 있다", "할 줄 아는 언어가 3개 이상이다", "허리가 좋지 않다", "개발과 게임, 둘 중 선택하라 하면 개발이다", "큰 백팩을 자주 맨다", "기능이 없어 만들어 쓴 적 있다", "버그 잡다가 새로운 버그를 발견한 적 있다",
               "후드티를 많이 갖고 있다", "불금에는 코딩이지", "개발 개그를 보면 공감간다", "Ctrl+S를 습관적으로 누른다", "칼퇴보다 야근이 더 많다", "일을 시작하고 나서 손목터널증후군이 생겼다", "적성에 맞는지 진지하게 생각해본 적 있다",
               "화려하지만 심플하게라는 말을 들어봤다", "외주를 받아봤다", "이름만 부른 메신저가 오면 방어태세부터 갖춘다", "이상한 폰트보면 기분 나빠진다", "가끔 친구들이 내가 하는 말을 이해하지 못한다", "자격증이 3개 이상이다", "이력서 100개 이상 뿌려 봤다",
               "낮과 밤이 바뀌었다", "코드 리펙토링이 필요하지만 못 본 척 지나친적 있다", "Pull Request 날렸다가 퇴짜 맞은적이 있다", "자주 사용하는 개발툴이 있다"
            };

            for(int i = 0; i < bingoContentItems.Length; i++)
            {
                BingoContents.Add(bingoContentItems[i]);
            }
        }

        /// <summary>
        /// DummyBingoContents에서 랜덤으로 3 X 3 크기 만큼의 Content 설정.
        /// </summary>
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
            List<int[]> bingoDirectionItems = new List<int[]>
            {
                #region 3 X 3 Bingo Cases
                new int[] { 0, 1, 2 },
                new int[] { 3, 4, 5 },
                new int[] { 6, 7, 8 },
                new int[] { 0, 3, 6 },
                new int[] { 1, 4, 7 },
                new int[] { 2, 5, 8 },
                new int[] { 0, 4, 8 },
                new int[] { 2, 4, 6 },
                #endregion
            };
            
            foreach(var items in bingoDirectionItems)
            {
                BingoDirectionItems.Add(items.ToList());
            }
        }

        private void SetBingoCaseDictionaries()
        {
            for (int i = 1; i < 9; i++)
            {
                BingoCaseDictionaries.Add(BingoDirectionItems[i - 1], false);
            }
        }
        #endregion

        #region Command Methods
        private bool CanBingoSelectionChange()
        {
            return InputBingoCount != 0 ? true : false;
        }

        private void OnBingoSelectionChange()
        {
            if(BingoItems.Count > 0)
            {
                SelectedBingo.IsSelected = true;
                IsMatchBingo();
            }
        }

        private void OnShuffleBingoItems()
        {
            InitializeBingoGame();
            SetBingoItems();
        }
        #endregion

        private void IsMatchBingo()
        {
            foreach(var bingoCase in GetBingoCaseDictionaries())
            {
                if (bingoCase.Value != true)
                {
                    if(BingoItems[bingoCase.Key[0]].IsSelected && BingoItems[bingoCase.Key[1]].IsSelected && BingoItems[bingoCase.Key[2]].IsSelected)
                    {
                        IncreaseBingoCount();
                        IsNBingo();
                        ChangeBingoCaseDictionaryValue(bingoCase, true);
                    }
                }
            }
        }

        private List<KeyValuePair<List<int>, bool>> GetBingoCaseDictionaries()
        {
            return BingoCaseDictionaries.ToList();
        }

        private void IncreaseBingoCount()
        {
            CheckBingoCount += 1;
        }

        private void IsNBingo()
        {
            if (CheckBingoCount == InputBingoCount)
            {
                OnIsNBingo?.Invoke();
            }
        }

        private void ChangeBingoCaseDictionaryValue(KeyValuePair<List<int>, bool> bingoCase, bool isSelected)
        {
            BingoCaseDictionaries[bingoCase.Key] = isSelected;
        }

        private void InitializeBingoGame()
        {
            BingoItems.Clear();
            CheckBingoCount = 0;
            MAX_BINGO_COUNT = BingoProperties.MAX_BINGO_COUNT;
            foreach(var bingoCase in GetBingoCaseDictionaries())
            {
                ChangeBingoCaseDictionaryValue(bingoCase, false);
            }
        }
    }
}
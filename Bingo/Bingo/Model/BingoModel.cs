namespace Bingo.Model
{
    public class BingoModel : ObservableObject
    {
        private int _bingoIdx;
        public int BingoIdx
        {
            get => _bingoIdx;
            set
            {
                _bingoIdx = value;
                OnPropertyChanged();
            }
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
    }
}

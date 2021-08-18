namespace Bingo.Model
{
    public class BingoModel : ObservableObject
    {
        private string _bingoContent;
        public string BingoContent
        {
            get => _bingoContent;
            set
            {
                _bingoContent = value;
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

using Unity.Mathematics;

namespace UnityEngine.UI
{
    [ExecuteAlways]
    public class FitterGridLayoutGroup : LayoutGroup
    {
        [SerializeField] private Vector2 _targetCellSize = new Vector2(100, 100);
        [SerializeField, Range(0.1f, 2f)] private float _minScale = 0.5f;
        [SerializeField, Range(0.1f, 2f)] private float _maxScale = 2f;
        [SerializeField] private int _minColumns = 3;
        [SerializeField] private float _spacing = 5f;

        private Vector2 _currentCellSize;
        private int _currentColumns;
        private int _currentRows;

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            UpdateGridLayout();
            SetLayoutInputForAxis(_currentCellSize.x * _currentColumns + _spacing * (_currentColumns - 1) + padding.left + padding.right, 0, -1, 0);
        }
        public override void CalculateLayoutInputVertical()
        {
            int count = rectChildren.Count;
            _currentRows = (int)math.ceil(count / (float)_currentColumns);

            float totalHeight = _currentCellSize.y * _currentRows + _spacing * (_currentRows - 1) + padding.top + padding.bottom;
            SetLayoutInputForAxis(totalHeight, totalHeight, -1, 1);
        }

        public override void SetLayoutHorizontal() => SetCells();
        public override void SetLayoutVertical() => SetCells();

        private void UpdateGridLayout()
        {
            float width = rectTransform.rect.width;
            float availableWidth = width - padding.left - padding.right;

            float minCellWidth = _targetCellSize.x * _minScale;
            float maxCellWidth = _targetCellSize.x * _maxScale;

            int columns = _minColumns;
            float cellWidth;

            while (true)
            {
                cellWidth = (availableWidth - (_spacing * (columns - 1))) / columns;
                if (cellWidth < minCellWidth) { columns = math.max(_minColumns, columns - 1); break; }
                if (cellWidth > maxCellWidth) columns++; else break;
            }


            float aspect = _targetCellSize.y / _targetCellSize.x;
            float cellHeight = cellWidth * aspect;

            float minCellHeight = _targetCellSize.y * _minScale;
            float maxCellHeight = _targetCellSize.y * _maxScale;

            // Clamp cell size
            cellWidth = math.clamp(cellWidth, minCellWidth, maxCellWidth);
            cellHeight = math.clamp(cellHeight, minCellHeight, maxCellHeight);
            columns = math.max(columns, _minColumns);

            _currentCellSize = new(cellWidth, cellHeight);
            _currentColumns = columns;
        }
        private void SetCells()
        {
            int count = rectChildren.Count;
            for (int i = 0; i < count; i++)
            {
                int row = i / _currentColumns;
                int column = i % _currentColumns;

                float x = padding.left + (_currentCellSize.x + _spacing) * column;
                float y = padding.top + (_currentCellSize.y + _spacing) * row;

                SetChildAlongAxis(rectChildren[i], 0, x, _currentCellSize.x);
                SetChildAlongAxis(rectChildren[i], 1, y, _currentCellSize.y);
            }
        }
    }
}
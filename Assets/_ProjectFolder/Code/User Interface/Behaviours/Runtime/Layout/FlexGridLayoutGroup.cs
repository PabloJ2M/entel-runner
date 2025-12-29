using System.Collections.Generic;
using Unity.Mathematics;

namespace UnityEngine.UI
{
    [AddComponentMenu("Layout/Grid Layout Group Flexible")]
    public class FlexGridLayoutGroup : LayoutGroup
    {
        [SerializeField] private Vector2 _size = new Vector2(100, 100);
        [SerializeField] private Vector2 _spacing;

        private struct CellPosition
        {
            public int column;
            public int row;
        }

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            LayoutChildren();
        }
        public override void CalculateLayoutInputVertical() => LayoutChildren();
        public override void SetLayoutHorizontal() => LayoutChildren();
        public override void SetLayoutVertical() => LayoutChildren();

        private void LayoutChildren()
        {
            TextAnchor alignment = childAlignment;
            float fullCellWidth = _size.x + _spacing.x;
            int availableWidth = (int)math.floor(rectTransform.rect.width - padding.left - padding.right);
            int columns = math.max(1, (int)math.floor((availableWidth + _spacing.x) / fullCellWidth));

            int[,] gridMap = new int[100, 100];

            Dictionary<RectTransform, CellPosition> itemPositions = new();
            Dictionary<RectTransform, Vector2Int> itemSpans = new();

            //detect cell spans per child
            foreach (RectTransform child in rectChildren)
            {
                Vector2Int span = Vector2Int.one;

                if (child.TryGetComponent(out LayoutElement layout))
                {
                    if (layout.flexibleWidth > 1f) span.x = math.clamp((int)layout.flexibleWidth, 1, columns);
                    if (layout.flexibleHeight > 1f) span.y = math.max((int)layout.flexibleHeight, 1);
                }

                itemSpans[child] = span;
            }

            //place items in grid
            int maxRow = 0;
            foreach (RectTransform child in rectChildren)
            {
                Vector2Int span = itemSpans[child];

                bool placed = false;
                for (int row = 0; !placed; row++)
                {
                    for (int col = 0; col < columns; col++)
                    {
                        if (CanPlace(gridMap, col, row, span.x, span.y, columns))
                        {
                            MarkOccupied(gridMap, col, row, span.x, span.y);
                            itemPositions[child] = new CellPosition { column = col, row = row };
                            maxRow = math.max(maxRow, row + span.y);
                            placed = true;
                            break;
                        }
                    }
                }
            }

            // Total grid size
            float totalWidth = columns * _size.x + (columns - 1) * _spacing.x + padding.left + padding.right;
            float totalHeight = maxRow * _size.y + (maxRow - 1) * _spacing.y + padding.top + padding.bottom;

            // Alignments
            bool midH = alignment == TextAnchor.UpperCenter || alignment == TextAnchor.MiddleCenter || alignment == TextAnchor.LowerCenter;
            bool cornerH = alignment == TextAnchor.UpperRight || alignment == TextAnchor.MiddleRight || alignment == TextAnchor.LowerRight;
            bool midV = alignment == TextAnchor.MiddleLeft || alignment == TextAnchor.MiddleCenter || alignment == TextAnchor.MiddleRight;
            bool cornerV = alignment == TextAnchor.LowerLeft || alignment == TextAnchor.LowerCenter || alignment == TextAnchor.LowerRight;

            float offsetX = 0f;
            float offsetY = 0f;

            if (midH) offsetX = (rectTransform.rect.width - totalWidth) / 2f;
            else if (cornerH) offsetX = rectTransform.rect.width - totalWidth;

            if (midV) offsetY = (rectTransform.rect.height - totalHeight) / 2f;
            else if (cornerV) offsetY = rectTransform.rect.height - totalHeight;

            // Position children
            foreach (RectTransform child in rectChildren)
            {
                CellPosition pos = itemPositions[child];
                Vector2Int span = itemSpans[child];

                LayoutElement le = child.GetComponent<LayoutElement>();

                float prefW = le != null && le.preferredWidth > 0f ? le.preferredWidth : _size.x * span.x + _spacing.x * (span.x - 1);
                float prefH = le != null && le.preferredHeight > 0f ? le.preferredHeight : _size.y * span.y + _spacing.y * (span.y - 1);

                float cellAreaWidth = _size.x * span.x + _spacing.x * (span.x - 1);
                float cellAreaHeight = _size.y * span.y + _spacing.y * (span.y - 1);

                prefW = math.min(prefW, cellAreaWidth);
                prefH = math.min(prefH, cellAreaHeight);

                float innerOffsetX = 0f;
                float innerOffsetY = 0f;

                if (midH) innerOffsetX = (cellAreaWidth - prefW) / 2f;
                else if (cornerH) innerOffsetX = (cellAreaWidth - prefW);

                if (midV) innerOffsetY = (cellAreaHeight - prefH) / 2f;
                else if (cornerV) innerOffsetY = (cellAreaHeight - prefH);

                float x = padding.left + pos.column * (_size.x + _spacing.x) + offsetX + innerOffsetX;
                float y = padding.top + pos.row * (_size.y + _spacing.y) + offsetY + innerOffsetY;

                SetChildAlongAxis(child, 0, x, prefW);
                SetChildAlongAxis(child, 1, y, prefH);
            }

            SetLayoutInputForAxis(totalWidth, totalWidth, -1, 0);
            SetLayoutInputForAxis(totalHeight, totalHeight, -1, 1);
        }

        private bool CanPlace(int[,] map, int col, int row, int spanX, int spanY, int maxCols)
        {
            if (col + spanX > maxCols) return false;

            for (int x = col; x < col + spanX; x++)
            {
                for (int y = row; y < row + spanY; y++)
                {
                    if (map[x, y] != 0) return false;
                }
            }
            return true;
        }
        private void MarkOccupied(int[,] map, int col, int row, int spanX, int spanY)
        {
            for (int x = col; x < col + spanX; x++)
            {
                for (int y = row; y < row + spanY; y++)
                    map[x, y] = 1;
            }
        }
    }
}
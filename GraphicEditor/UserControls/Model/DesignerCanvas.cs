using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;

namespace GraphicEditor.UserControls.Model
{
    public class DesignerCanvas : Canvas
    {
        private Point? f_dragStartPoint = null;

        public IEnumerable<DesignerItem> SelectedItems
        {
            get
            {
                var selectedItems = from item in this.Children.OfType<DesignerItem>()
                                    where item.IsSelected == true
                                    select item;

                return selectedItems;
            }
        }

        public void DeselectAll()
        {
            foreach (DesignerItem item in this.SelectedItems)
            {
                item.IsSelected = false;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e == null)
                return;
            base.OnMouseDown(e);
            if (e.Source == this)
            {
                this.f_dragStartPoint = new Point?(e.GetPosition(this));
                this.DeselectAll();
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e == null)
                return;

            base.OnMouseMove(e);

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                this.f_dragStartPoint = null;
            }

            if (this.f_dragStartPoint.HasValue)
            {
                // AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                // if (adornerLayer != null)
                // {
                //    RubberbandAdorner adorner = new RubberbandAdorner(this, this.dragStartPoint);
                //    if (adorner != null)
                //    {
                //        adornerLayer.Add(adorner);
                //    }
                // }
                e.Handled = true;
            }
        }

        protected override void OnDrop(DragEventArgs e)
        {
            if (e == null)
                return;
            base.OnDrop(e);
            string xamlString = e.Data.GetData("DESIGNER_ITEM") as string;
            if (!string.IsNullOrEmpty(xamlString))
            {
                DesignerItem newItem = null;
                FrameworkElement content = new FrameworkElement();

                using (StringReader stringReader = new StringReader(xamlString))
                {
                    content = XamlReader.Load(XmlReader.Create(stringReader)) as FrameworkElement;
                }

                if (content != null)
                {
                    newItem = new DesignerItem();
                    newItem.Content = content;

                    Point position = e.GetPosition(this);
                    if (content.MinHeight != 0 && content.MinWidth != 0)
                    {
                        newItem.Width = content.MinWidth * 2;
                        newItem.Height = content.MinHeight * 2;
                    }
                    else
                    {
                        newItem.Width = 65;
                        newItem.Height = 65;
                    }

                    DesignerCanvas.SetLeft(newItem, Math.Max(0, position.X - (newItem.Width / 2)));
                    DesignerCanvas.SetTop(newItem, Math.Max(0, position.Y - (newItem.Height / 2)));
                    Children.Add(newItem);

                    this.DeselectAll();
                    newItem.IsSelected = true;
                }

                e.Handled = true;
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size size = new Size();
            foreach (UIElement element in this.Children)
            {
                double left = Canvas.GetLeft(element);
                double top = Canvas.GetTop(element);
                left = double.IsNaN(left) ? 0 : left;
                top = double.IsNaN(top) ? 0 : top;

                element.Measure(constraint);

                Size desiredSize = element.DesiredSize;
                if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
                {
                    size.Width = Math.Max(size.Width, left + desiredSize.Width);
                    size.Height = Math.Max(size.Height, top + desiredSize.Height);
                }
            }

            // add some extra margin
            size.Width += 10;
            size.Height += 10;
            return size;
        }
    }
}

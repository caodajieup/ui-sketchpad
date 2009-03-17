#region Header
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
#endregion

namespace Sketchpad.UI.Services
{
    public interface IUndoHandler
    {
        bool EnableUndo
        {
            get;
        }

        bool EnableRedo
        {
            get;
        }

        void Undo();
        void Redo();
    }

    public class FormsDesignerUndoEngine : UndoEngine, IUndoHandler
    {
        Stack<UndoEngine.UndoUnit> undoStack = new Stack<UndoEngine.UndoUnit>();
        Stack<UndoEngine.UndoUnit> redoStack = new Stack<UndoEngine.UndoUnit>();

        public FormsDesignerUndoEngine(IServiceProvider provider)
            : base(provider)
        {
        }

        #region IUndoHandler
        public bool EnableUndo
        {
            get
            {
                return undoStack.Count > 0;
            }
        }

        public bool EnableRedo
        {
            get
            {
                return redoStack.Count > 0;
            }
        }

        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                UndoEngine.UndoUnit unit = undoStack.Pop();
                unit.Undo();
                redoStack.Push(unit);
            }
        }

        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                UndoEngine.UndoUnit unit = redoStack.Pop();
                unit.Undo();
                undoStack.Push(unit);
            }
        }
        #endregion

        protected override void AddUndoUnit(UndoEngine.UndoUnit unit)
        {
            undoStack.Push(unit);
        }
    }
}
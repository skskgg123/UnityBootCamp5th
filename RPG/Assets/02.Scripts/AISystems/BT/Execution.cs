using System;

namespace RPG.AISystems.BT
{
    public class Excution : Node
    {
        public Excution(Tree tree, Func<Result> excute) : base(tree)
        {
            _excute = excute;
        }

        private Func<Result> _excute;

        public override Result Invoke()
        {
            return _excute.Invoke();
        }

    }
}
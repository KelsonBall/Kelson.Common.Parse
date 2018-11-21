using System;
using System.Collections.Generic;

namespace Lua
{
    public class State
    {
        private readonly SortedList<string, dynamic> values = new SortedList<string, dynamic>();
        private readonly Stack<dynamic> stack = new Stack<dynamic>();

        public int StackHeight => stack.Count;

        private readonly State root;

        private readonly State parent;

        public State(State parent) => (this.parent, this.root) = (parent, parent.root);

        public State() => root = this;

        public dynamic this[string key]
        {
            get
            {
                if (values.ContainsKey(key))
                    return values[key];
                else if (parent != null)
                    return parent[key];
                else
                    return null;
            }

            protected set
            {
                if (values.ContainsKey(key))
                    values[key] = value;
                else if (parent != null)
                    parent[key] = value;
                else
                    values[key] = value;
            }
        }

        public void SetLocal(string key, dynamic value) => values[key] = value;

        public void Set(string key, dynamic value) => this[key] = value;

        public void Push(dynamic value)
        {
            throw new NotImplementedException();
        }
            //=> stack.Push(value);

        public dynamic Pop() => stack.Pop();
    }
}

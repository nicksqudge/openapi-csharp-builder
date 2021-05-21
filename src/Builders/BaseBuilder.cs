using System;
using System.Collections.Generic;

namespace Builders
{
    public abstract class BaseBuilder<T>
        where T : class, new()
    {
        protected T _result;

        public BaseBuilder()
        {
            _result = new T();
        }

        public virtual T Build()
        {
            return _result;
        }
    }

    public abstract class BaseListBuilder<T> : BaseBuilder<List<T>>
        where T : class, new()
    {
        protected void Add(T input)
        {
            _result.Add(input);
        }

    }
}

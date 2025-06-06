using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{

    Expression<Func<T, bool>>? Criteria { get; }  // This is a property. has to give a criteria function in the implementation.
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    bool isDistinct { get; }

}

public interface ISpecification<T, TResult> : ISpecification<T>
{
    Expression<Func<T, TResult>>? Select { get; }
}

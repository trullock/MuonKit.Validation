using System;
using System.Collections;

// ReSharper disable once CheckNamespace
namespace MuonLab.Validation
{
	public static class BetweenExtensions
	{
		/// <summary>
		/// Ensure the property is between some values (inclusive)
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="lower">The lower bound to compare to</param>
		/// <param name="upper">The upper bound to compare to</param>
		/// <returns></returns>
		public static ICondition<TValue?> IsBetween<TValue>(this TValue? self, TValue? lower, TValue? upper) where TValue : struct, IComparable
		{
			return self.IsBetween(lower, upper, "Between");
		}

		/// <summary>
		/// Ensure the property is between some values (inclusive)
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="lower">The lower bound to compare to</param>
		/// <param name="upper">The upper bound to compare to</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<TValue?> IsBetween<TValue>(this TValue? self, TValue? lower, TValue? upper, string errorKey) where TValue : struct, IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, lower) >= 0 && Comparer.Default.Compare(x, upper) <= 0, errorKey);
		}

		/// <summary>
		/// Ensure the property is between some values (inclusive)
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="lower">The lower bound to compare to</param>
		/// <param name="upper">The upper bound to compare to</param>
		/// <returns></returns>
		public static ICondition<TValue> IsBetween<TValue>(this TValue self, TValue? lower, TValue? upper) where TValue : struct, IComparable
		{
			return self.IsBetween(lower, upper, "Between");
		}

		/// <summary>
		/// Ensure the property is between some values (inclusive)
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="lower">The lower bound to compare to</param>
		/// <param name="upper">The upper bound to compare to</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<TValue> IsBetween<TValue>(this TValue self, TValue? lower, TValue? upper, string errorKey) where TValue : struct, IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, lower) >= 0 && Comparer.Default.Compare(x, upper) <= 0, errorKey);
		}

		/// <summary>
		/// Ensure the property is between some values (inclusive)
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="lower">The lower bound to compare to</param>
		/// <param name="upper">The upper bound to compare to</param>
		/// <returns></returns>
		public static ICondition<TValue> IsBetween<TValue>(this TValue self, TValue lower, TValue upper) where TValue : IComparable
		{
			return self.IsBetween(lower, upper, "Between");
		}

		/// <summary>
		/// Ensure the property is between some values (inclusive)
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="lower">The lower bound to compare to</param>
		/// <param name="upper">The upper bound to compare to</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<TValue> IsBetween<TValue>(this TValue self, TValue lower, TValue upper, string errorKey) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, lower) >= 0 && Comparer.Default.Compare(x, upper) <= 0, errorKey);
		}
	}
}
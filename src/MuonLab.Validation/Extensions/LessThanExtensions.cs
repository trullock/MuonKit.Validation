using System;
using System.Collections;

// ReSharper disable once CheckNamespace
namespace MuonLab.Validation
{
	public static class LessThanExtensions
	{
		/// <summary>
		/// Ensure the property is less than some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be less than</param>
		/// <returns></returns>
		public static ICondition<TValue?> IsLessThan<TValue>(this TValue? self, TValue? comparison) where TValue : struct, IComparable
		{
			return self.IsLessThan(comparison, "LessThan");
		}

		/// <summary>
		/// Ensure the property is less than some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be less than</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<TValue?> IsLessThan<TValue>(this TValue? self, TValue? comparison, string errorKey) where TValue : struct, IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, comparison) < 0, errorKey);
		}

		/// <summary>
		/// Ensure the property is less than some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be less than</param>
		/// <returns></returns>
		public static ICondition<TValue> IsLessThan<TValue>(this TValue self, TValue? comparison) where TValue : struct, IComparable
		{
			return self.IsLessThan(comparison, "LessThan");
		}

		/// <summary>
		/// Ensure the property is less than some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be less than</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<TValue> IsLessThan<TValue>(this TValue self, TValue? comparison, string errorKey) where TValue : struct, IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, comparison) < 0, errorKey);
		}

		/// <summary>
		/// Ensure the property is less than some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be less than</param>
		/// <returns></returns>
		public static ICondition<TValue> IsLessThan<TValue>(this TValue self, TValue comparison) where TValue : IComparable
		{
			return self.IsLessThan(comparison, "LessThan");
		}

		/// <summary>
		/// Ensure the property is less than some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be less than</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<TValue> IsLessThan<TValue>(this TValue self, TValue comparison, string errorKey) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, comparison) < 0, errorKey);
		}
	}
}
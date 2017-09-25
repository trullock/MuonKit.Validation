using System;
using System.Collections;

// ReSharper disable once CheckNamespace
namespace MuonLab.Validation
{
	public static class InequalityExtensions
	{
		/// <summary>
		/// Ensure the property is not equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be not equal to</param>
		/// <returns></returns>
		public static ICondition<TValue> IsNotEqualTo<TValue>(this TValue self, TValue comparison) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, comparison) != 0, "NotEqualTo");
		}

		/// <summary>
		/// Ensure the property is not equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be not equal to</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<TValue> IsNotEqualTo<TValue>(this TValue self, TValue comparison, string errorKey = "NotEqualTo") where TValue : IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, comparison) != 0, errorKey);
		}

		/// <summary>
		/// Ensure the property is not equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be not equal to</param>
		/// <returns></returns>
		public static ICondition<TValue> IsNotEqualTo<TValue>(this TValue self, TValue? comparison) where TValue : struct, IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, comparison) != 0, "NotEqualTo");
		}


		/// <summary>
		/// Ensure the property is not equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be not equal to</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<TValue> IsNotEqualTo<TValue>(this TValue self, TValue? comparison, string errorKey) where TValue : struct, IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, comparison) != 0, errorKey);
		}

		/// <summary>
		/// Ensure the property is not equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be not equal to</param>
		/// <returns></returns>
		public static ICondition<TValue?> IsNotEqualTo<TValue>(this TValue? self, TValue? comparison) where TValue : struct, IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, comparison) != 0, "NotEqualTo");
		}

		/// <summary>
		/// Ensure the property is not equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be not equal to</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<TValue?> IsNotEqualTo<TValue>(this TValue? self, TValue? comparison, string errorKey) where TValue : struct, IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, comparison) != 0, errorKey);
		}
	}
}
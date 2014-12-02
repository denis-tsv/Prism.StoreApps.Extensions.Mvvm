using System;
using Microsoft.Practices.Unity;

namespace Prism.StoreApps.Extensions.Common.Extensions
{
	public static class UnityExtensions
	{
		public static IUnityContainer RegisterTypeAs<TFrom, TTo>(this IUnityContainer container, ResolvingMode mode = ResolvingMode.None, params InjectionMember[] injectionMembers) where TTo : TFrom
		{
			return container.RegisterType<TFrom, TTo>(GetLifetimeManager(mode), injectionMembers);
		}

		public static IUnityContainer RegisterTypeAs<TFrom, TTo>(this IUnityContainer container, string name, ResolvingMode mode = ResolvingMode.None, params InjectionMember[] injectionMembers) where TTo : TFrom
		{
			return container.RegisterType<TFrom, TTo>(name, GetLifetimeManager(mode), injectionMembers);
		}

		public static IUnityContainer RegisterTypeAs(this IUnityContainer container, Type from, Type to, ResolvingMode mode = ResolvingMode.None, params InjectionMember[] injectionMembers)
		{
			return container.RegisterType(from, to, GetLifetimeManager(mode), injectionMembers);
		}

		public static IUnityContainer RegisterTypeAs(this IUnityContainer container, Type from, Type to, string name, ResolvingMode mode = ResolvingMode.None, params InjectionMember[] injectionMembers)
		{
			return container.RegisterType(from, to, name, GetLifetimeManager(mode), injectionMembers);
		}

		public static IUnityContainer RegisterTypeAs<T>(this IUnityContainer container, ResolvingMode resolvingMode = ResolvingMode.None)
		{
			return container.RegisterType<T>(GetLifetimeManager(resolvingMode));
		}

		public static IUnityContainer RegisterTypeAs<T>(this IUnityContainer container, string name, ResolvingMode resolvingMode = ResolvingMode.None)
		{
			return container.RegisterType<T>(name, GetLifetimeManager(resolvingMode));
		}

		private static LifetimeManager GetLifetimeManager(ResolvingMode resolvingMode)
		{
			switch (resolvingMode)
			{
				case ResolvingMode.Singleton:
					return new ContainerControlledLifetimeManager();

				case ResolvingMode.None:
					return new TransientLifetimeManager();

				default:
					throw new ArgumentOutOfRangeException("resolvingMode");
			}
		}
	}

	public enum ResolvingMode
	{
		None = 1,
		Singleton = 2,
	}
}

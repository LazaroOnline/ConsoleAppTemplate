using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests;

public static class ServiceCollectionExtensions
{

	/// <summary>
	/// Removes all registered service registrations of <see cref="TService"/>.
	/// </summary>
	public static void RemoveServices<TService>(this IServiceCollection services)
	{
		var serviceDescriptors = services.Where(x => x.ServiceType == typeof(TService)).ToList();
		if (serviceDescriptors.Any())
		{
			foreach (var serviceDescriptor in serviceDescriptors)
			{
				services.Remove(serviceDescriptor);
			}
		}
	}

}

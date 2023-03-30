﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NSE.WebApi.Core.Extensions
{
	public static class HttpExtensions
	{
		public static IHttpClientBuilder AllowSelfSignedCertificate(this IHttpClientBuilder builder)
		{
			if(builder == null)
			{
				throw new ArgumentNullException(nameof(builder));
			}

			return builder.ConfigureHttpMessageHandlerBuilder(b =>
			{
				b.PrimaryHandler = new HttpClientHandler
				{
					ServerCertificateCustomValidationCallback = HttpClientHandler
					.DangerousAcceptAnyServerCertificateValidator
				};
			});
		}
	}
}

﻿using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace rivne.booking.Core.Entities.Specification;
public static class RefreshTokenSpecifications
{
	public static class RefreshTokenSpecification
	{
		public class GetRefreshToken : Specification<RefreshToken>
		{
			public GetRefreshToken(string refreshToken)
			{
				Query.Where(t => t.Token == refreshToken);
			}
		}

		public class GetTokensDyUserId : Specification<RefreshToken>
		{
			public GetTokensDyUserId(string userId)
			{
				Query.Where(t => t.UserId == userId);
			}
		}
	}
}

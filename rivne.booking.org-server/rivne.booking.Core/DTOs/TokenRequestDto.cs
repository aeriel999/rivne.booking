﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rivne.booking.Core.DTOs;
public class TokenRequestDto
{
	public string Token { get; set; } = string.Empty;
	public string RefreshToken { get; set; } = string.Empty;
}

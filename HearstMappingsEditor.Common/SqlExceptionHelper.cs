using System;
using System.Data.SqlClient;

namespace HearstMappingsEditor.Common
{
	public static class SqlExceptionHelper
	{
		public const int UniqueConstraintErrorCode = 2601;
		public const int UniqueIndexErrorCode = 2627;

		public static bool IsUniqueConstraintError(Exception exc)
		{
			if (exc == null)
			{
				return false;
			}

			while (exc != null && !(exc is SqlException))
			{
				exc = exc.InnerException;
			}

			if (exc != null)
			{
				var sqlExc = (SqlException) exc;
				if (sqlExc.Number == UniqueConstraintErrorCode || sqlExc.Number == UniqueIndexErrorCode)
				{
					return true;
				}
			}

			return false;
		}
	}
}

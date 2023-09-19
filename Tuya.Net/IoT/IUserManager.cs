﻿using Tuya.Net.Data;

namespace Tuya.Net.IoT
{
    /// <summary>
    /// User Manager interface.
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>A <see cref="User"/> containing user information.</returns>
        public Task<User?> GetUserByIdAsync(string userId, CancellationToken ct = default);
    }
}

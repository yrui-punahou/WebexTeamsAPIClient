﻿/* 
 * MIT License
 * 
 * Copyright(c) 2018 thrzn41
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thrzn41.Util;

namespace Thrzn41.WebexTeams.Version1.GuestIssuer
{

    /// <summary>
    /// Cisco Webex Teams Guest Issuer Client for API version 1.
    /// </summary>
    public class TeamsGuestIssuerClient : IDisposable
    {


        /// <summary>
        /// Teams Guest Issuer API Path.
        /// </summary>
        protected static readonly string TEAMS_GUEST_ISSUER_API_PATH = TeamsAPIClient.GetAPIPath("jwt/login");


        /// <summary>
        /// Teams Guest Issuer API Uri.
        /// </summary>
        protected static readonly Uri TEAMS_GUEST_ISSUER_API_URI = new Uri(TEAMS_GUEST_ISSUER_API_PATH);


        /// <summary>
        /// Internal Encoding of this class.
        /// </summary>
        private static readonly Encoding ENCODING = UTF8Utils.UTF8_WITHOUT_BOM;

        /// <summary>
        /// base64url encoded JWT Header.
        /// </summary>
        private static readonly string TEAMS_GUEST_ISSUER_ENCODED_JWT_HEADER = encodeBase64Url( (new JWTHeader()).ToJsonString() );


        /// <summary>
        /// Unix time base.
        /// </summary>
        private static readonly DateTime UNIX_TIME_BASE = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


        /// <summary>
        /// HttpClient for Guest Issuer API.
        /// </summary>
        protected readonly TeamsHttpClient teamsHttpClient;


        /// <summary>
        /// <see cref="HashAlgorithm"/> to sign JWT Header and Payload.
        /// </summary>
        private readonly HashAlgorithm jwtSigner;

        /// <summary>
        /// Guest Issuer Id.
        /// </summary>
        private readonly string guestIssuerId;




        /// <summary>
        /// Constuctor of TeamsGuestIssuerClient.
        /// </summary>
        /// <param name="secret">Secret of the Guest Issuer.</param>
        /// <param name="guestIssuerId">Guest Issuer ID.</param>
        internal TeamsGuestIssuerClient(string secret, string guestIssuerId)
        {
            this.teamsHttpClient = new TeamsHttpClient(null, TeamsAPIClient.TEAMS_API_URI_PATTERN, true);

            // For now, Webex Teams Guest Issuer uses HMAC-SHA256 for JWT signature.
            this.jwtSigner = new HMACSHA256( Convert.FromBase64String(secret) );

            this.guestIssuerId = guestIssuerId;
        }


        /// <summary>
        /// Encodes to base64url string.
        /// </summary>
        /// <param name="data">data to be encoded.</param>
        /// <returns>base64url encoded string.</returns>
        private static string encodeBase64Url(byte[] data)
        {
#if (DOTNETSTANDARD1_3 || DOTNETCORE1_0)
            string base64Str = Convert.ToBase64String(data);
#else
            string base64Str = Convert.ToBase64String(data, Base64FormattingOptions.None);
#endif

            return base64Str.TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        /// <summary>
        /// Encodes to base64url string.
        /// </summary>
        /// <param name="str"><see cref="string"/> to be encoded.</param>
        /// <returns>base64url encoded string.</returns>
        private static string encodeBase64Url(string str)
        {
            return encodeBase64Url( ENCODING.GetBytes(str) );
        }



        /// <summary>
        /// Converts Guest Issuer token result to Teams token result.
        /// </summary>
        /// <param name="source">Source result.</param>
        /// <param name="refreshedAt"><see cref="DateTime"/> when the token refreshed.</param>
        /// <returns><see cref="TeamsResult{TTeamsObject}"/> to get result.</returns>
        private static TeamsResult<GuestTokenInfo> convert(TeamsResult<GuestTokenInternalInfo> source, DateTime refreshedAt)
        {
            var result = new TeamsResult<GuestTokenInfo>();

            result.IsSuccessStatus = source.IsSuccessStatus;
            result.HttpStatusCode  = source.HttpStatusCode;
            result.RetryAfter      = source.RetryAfter;
            result.TrackingId      = source.TrackingId;


            var tokenInfo = new GuestTokenInfo();

            var data = source.Data;

            if (result.IsSuccessStatus)
            {
                tokenInfo.RefreshedAt = refreshedAt;

                tokenInfo.AccessToken = data.Token;

                if (data.ExpiresIn.HasValue)
                {
                    tokenInfo.AccessTokenExpiresIn = TimeSpan.FromSeconds(data.ExpiresIn.Value);
                    tokenInfo.AccessTokenExpiresAt = refreshedAt + tokenInfo.AccessTokenExpiresIn;
                }
            }

            if (data.HasExtensionData)
            {
                tokenInfo.JsonExtensionData = data.JsonExtensionData;
            }

            tokenInfo.HasValues = data.HasValues;

            result.Data = tokenInfo;

            return result;
        }


        /// <summary>
        /// Gets Guest Token info.
        /// </summary>
        /// <param name="subject">The subject of the token. A unique, public identifier for the end-user of the token. This claim may contain only letters, numbers, and hyphens.</param>
        /// <param name="name">The display name of the guest user. This will be the name shown in Webex Teams clients.</param>
        /// <param name="expiresAt">The expiration time of the token. Use the lowest practical value for the use of the token.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to be used for cancellation.</param>
        /// <returns><see cref="TeamsResult{TTeamsObject}"/> to get result.</returns>
        private async Task< TeamsResult<GuestTokenInfo> > getGuestTokenInfoAsync(string subject, string name, DateTime expiresAt, CancellationToken? cancellationToken = null)
        {
            var expirationTime = expiresAt.ToUniversalTime() - UNIX_TIME_BASE;

            var payload = new JWTPayload
                            {
                                Subject        = subject,
                                Name           = name,
                                Issuer         = this.guestIssuerId,
                                ExpirationTime = Convert.ToInt64(expirationTime.TotalSeconds),
                            };

            var jwtToken = new StringBuilder(TEAMS_GUEST_ISSUER_ENCODED_JWT_HEADER);

            jwtToken.Append('.').Append( encodeBase64Url(payload.ToJsonString()) );

            var data = this.jwtSigner.ComputeHash( ENCODING.GetBytes(jwtToken.ToString()) );

            jwtToken.Append('.').Append( encodeBase64Url(data) );



            DateTime refreshedAt = DateTime.UtcNow;

            var result = await this.teamsHttpClient.RequestJsonWithBearerTokenAsync<TeamsResult<GuestTokenInternalInfo>, GuestTokenInternalInfo>(
                                    HttpMethod.Post,
                                    TEAMS_GUEST_ISSUER_API_URI,
                                    null,
                                    null,
                                    jwtToken.ToString(),
                                    cancellationToken);

            result.IsSuccessStatus = (result.IsSuccessStatus && (result.HttpStatusCode == System.Net.HttpStatusCode.OK));

            return convert(result, refreshedAt);
        }


        /// <summary>
        /// Gets Guest Token info.
        /// </summary>
        /// <param name="subject">The subject of the token. A unique, public identifier for the end-user of the token. This claim may contain only letters, numbers, and hyphens.</param>
        /// <param name="name">The display name of the guest user. This will be the name shown in Webex Teams clients.</param>
        /// <param name="expiresIn">The expiration time of the token. Use the lowest practical value for the use of the token.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to be used for cancellation.</param>
        /// <returns><see cref="TeamsResult{TTeamsObject}"/> to get result.</returns>
        public Task< TeamsResult<GuestTokenInfo> > GetGuestTokenInfoAsync(string subject, string name, TimeSpan expiresIn, CancellationToken? cancellationToken = null)
        {
            return getGuestTokenInfoAsync(subject, name, (DateTime.UtcNow + expiresIn), cancellationToken);
        }

        /// <summary>
        /// Gets Guest Token info.
        /// </summary>
        /// <param name="subject">The subject of the token. A unique, public identifier for the end-user of the token. This claim may contain only letters, numbers, and hyphens.</param>
        /// <param name="name">The display name of the guest user. This will be the name shown in Webex Teams clients.</param>
        /// <param name="expiresAt">The expiration time of the token. Use the lowest practical value for the use of the token.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to be used for cancellation.</param>
        /// <returns><see cref="TeamsResult{TTeamsObject}"/> to get result.</returns>
        public Task< TeamsResult<GuestTokenInfo> > GetGuestTokenInfoAsync(string subject, string name, DateTime expiresAt, CancellationToken? cancellationToken = null)
        {
            return getGuestTokenInfoAsync(subject, name, expiresAt, cancellationToken);
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls


        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    using (this.teamsHttpClient)
                    using (this.jwtSigner)
                    {

                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TeamsOAuth2Client() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


    }

}
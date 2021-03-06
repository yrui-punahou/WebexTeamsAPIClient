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
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thrzn41.WebexTeams.Version1.OAuth2
{

    /// <summary>
    /// Cisco Webex Teams token info object.
    /// </summary>
    public class TeamsOAuth2Client : IDisposable
    {

        /// <summary>
        /// Teams access token API Path.
        /// </summary>
        protected static readonly string TEAMS_ACCESS_TOKEN_API_PATH = TeamsAPIClient.GetAPIPath("access_token");


        /// <summary>
        /// Teams access token API Uri.
        /// </summary>
        protected static readonly Uri TEAMS_ACCESS_TOKEN_API_URI = new Uri(TEAMS_ACCESS_TOKEN_API_PATH);


        /// <summary>
        /// HttpClient for Teams API.
        /// </summary>
        protected readonly TeamsHttpClient teamsHttpClient;


        /// <summary>
        /// Client secret.
        /// </summary>
        private string clientSecret;

        /// <summary>
        /// Client id.
        /// </summary>
        private string clientId;




        /// <summary>
        /// Constuctor of TeamsOAuth2Client.
        /// </summary>
        /// <param name="clientSecret">Client secret key.</param>
        /// <param name="clientId">Client id.</param>
        /// <param name="retryExecutor">Executor for retry.</param>
        /// <param name="retryNotificationFunc">Notification func for retry.</param>
        internal TeamsOAuth2Client(string clientSecret, string clientId, TeamsRetry retryExecutor = null, Func<TeamsResultInfo, int, bool> retryNotificationFunc = null)
        {
            this.teamsHttpClient = new TeamsHttpClient(null, TeamsAPIClient.TEAMS_API_URI_PATTERN, retryExecutor, retryNotificationFunc);

            this.clientId     = clientId;
            this.clientSecret = clientSecret;
        }


        /// <summary>
        /// Converts OAuth2 result to Teams token result.
        /// </summary>
        /// <param name="source">Source result.</param>
        /// <param name="refreshedAt"><see cref="DateTime"/> when the token refreshed.</param>
        /// <returns><see cref="TeamsResult{TTeamsObject}"/> to get result.</returns>
        private static TeamsResult<TokenInfo> convert(TeamsResult<Oauth2TokenInternalInfo> source, DateTime refreshedAt)
        {
            var result = new TeamsResult<TokenInfo>();

            source.CopyInfoTo(result);


            var tokenInfo = new TokenInfo();

            var data = source.Data;

            if (result.IsSuccessStatus)
            {
                tokenInfo.RefreshedAt = refreshedAt;

                tokenInfo.AccessToken = data.AccessToken;

                if(data.ExpiresIn.HasValue)
                {
                    tokenInfo.AccessTokenExpiresIn = TimeSpan.FromSeconds(data.ExpiresIn.Value);
                    tokenInfo.AccessTokenExpiresAt = refreshedAt + tokenInfo.AccessTokenExpiresIn;
                }

                tokenInfo.RefreshToken = data.RefreshToken;

                if (data.RefreshTokenExpiresIn.HasValue)
                {
                    tokenInfo.RefreshTokenExpiresIn = TimeSpan.FromSeconds(data.RefreshTokenExpiresIn.Value);
                    tokenInfo.RefreshTokenExpiresAt = refreshedAt + tokenInfo.RefreshTokenExpiresIn;
                }
            }

            if(data.HasExtensionData)
            {
                tokenInfo.JsonExtensionData = data.JsonExtensionData;
            }

            tokenInfo.HasValues = data.HasValues;

            result.Data = tokenInfo;

            return result;
        }


        /// <summary>
        /// Gets token info.
        /// </summary>
        /// <param name="parameters">Query parameters.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to be used for cancellation.</param>
        /// <returns><see cref="TeamsResult{TTeamsObject}"/> to get result.</returns>
        private async Task< TeamsResult<TokenInfo> > getTokenInfoAsync(NameValueCollection parameters, CancellationToken? cancellationToken = null)
        {
            DateTime refreshedAt = DateTime.UtcNow;

            var result = await this.teamsHttpClient.RequestFormDataAsync<TeamsResult<Oauth2TokenInternalInfo>, Oauth2TokenInternalInfo>(
                                    HttpMethod.Post,
                                    TEAMS_ACCESS_TOKEN_API_URI,
                                    null,
                                    parameters,
                                    cancellationToken);

            result.IsSuccessStatus = (result.IsSuccessStatus && (result.HttpStatusCode == System.Net.HttpStatusCode.OK));

            return convert(result, refreshedAt);
        }


        /// <summary>
        /// Gets token info.
        /// </summary>
        /// <param name="code">Authorization Code.</param>
        /// <param name="redirectUri">Redirect uri.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to be used for cancellation.</param>
        /// <returns><see cref="TeamsResult{TTeamsObject}"/> to get result.</returns>
        public Task< TeamsResult<TokenInfo> > GetTokenInfoAsync(string code, string redirectUri, CancellationToken? cancellationToken = null)
        {
            var parameters = new NameValueCollection();

            parameters.Add("grant_type",    "authorization_code");
            parameters.Add("client_id",     this.clientId);
            parameters.Add("client_secret", this.clientSecret);
            parameters.Add("code",          code);
            parameters.Add("redirect_uri",  redirectUri);

            return (getTokenInfoAsync(parameters, cancellationToken));
        }


        /// <summary>
        /// Refreshed token info.
        /// </summary>
        /// <param name="refreshToken">Refresh Code.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to be used for cancellation.</param>
        /// <returns><see cref="TeamsResult{TTeamsObject}"/> to get result.</returns>
        public Task< TeamsResult<TokenInfo> > RefreshTokenInfoAsync(string refreshToken, CancellationToken? cancellationToken = null)
        {
            var parameters = new NameValueCollection();

            parameters.Add("grant_type",   "refresh_token");
            parameters.Add("client_id",     this.clientId);
            parameters.Add("client_secret", this.clientSecret);
            parameters.Add("refresh_token", refreshToken);

            return (getTokenInfoAsync(parameters, cancellationToken));
        }

        /// <summary>
        /// Refreshed token info.
        /// </summary>
        /// <param name="tokenInfo">Refresh Code.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to be used for cancellation.</param>
        /// <returns><see cref="TeamsResult{TTeamsObject}"/> to get result.</returns>
        public Task< TeamsResult<TokenInfo> > RefreshTokenInfoAsync(TokenInfo tokenInfo, CancellationToken? cancellationToken = null)
        {
            return (RefreshTokenInfoAsync(tokenInfo.RefreshToken, cancellationToken));
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

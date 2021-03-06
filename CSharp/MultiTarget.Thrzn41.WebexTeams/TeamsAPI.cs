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
using System.Text;
using Thrzn41.Util;

namespace Thrzn41.WebexTeams
{

    /// <summary>
    /// Class to create Teams API clients.
    /// </summary>
    public static class TeamsAPI
    {




        #region Teams v1 API Client

        /// <summary>
        /// Creates Teams API client for v1 API.
        /// </summary>
        /// <param name="tokenProtected">Teams API token of <see cref="ProtectedString"/></param>
        /// <returns>Teams API client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.TeamsAPIClient CreateVersion1Client(ProtectedString tokenProtected)
        {
            return CreateVersion1Client( tokenProtected.DecryptToString() );
        }

        /// <summary>
        /// Creates Teams API client for v1 API.
        /// </summary>
        /// <param name="tokenChars">Teams API token of char array.</param>
        /// <returns>Teams API client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.TeamsAPIClient CreateVersion1Client(char[] tokenChars)
        {
            return CreateVersion1Client( new String(tokenChars) );
        }

        /// <summary>
        /// Creates Teams API client for v1 API.
        /// </summary>
        /// <param name="tokenString">Teams API token of string.</param>
        /// <returns>Teams API client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.TeamsAPIClient CreateVersion1Client(string tokenString)
        {
            return new Thrzn41.WebexTeams.Version1.TeamsAPIClient(tokenString);
        }

        /// <summary>
        /// Creates Teams API client for v1 API.
        /// </summary>
        /// <param name="tokenInfo">Teams API token info.</param>
        /// <returns>Teams API client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.TeamsAPIClient CreateVersion1Client(Thrzn41.WebexTeams.Version1.AccessTokenInfo tokenInfo)
        {
            return new Thrzn41.WebexTeams.Version1.TeamsAPIClient(tokenInfo.AccessToken);
        }

        #endregion


        #region Teams v1 API Client with Retry

        /// <summary>
        /// Creates Teams API client for v1 API with retry feature.
        /// <see cref="TeamsRetryHandler"/> or <see cref="TeamsRetryOnErrorHandler"/> can be specified so that the client can retry on HTTP 429 and optinally 500, 502, 503 and 504 response.
        /// </summary>
        /// <param name="tokenProtected">Teams API token of <see cref="ProtectedString"/></param>
        /// <param name="retryHandler">Handler for retry.</param>
        /// <param name="retryNotificationFunc">Notification func where is notified before a retry. The func receives <see cref="TeamsResultInfo"/> for response info and int for trial counter. If the func returns false, the retry is cancelled.</param>
        /// <returns>Teams API client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.TeamsAPIClient CreateVersion1Client(ProtectedString tokenProtected, TeamsRetry retryHandler, Func<TeamsResultInfo, int, bool> retryNotificationFunc = null)
        {
            return CreateVersion1Client(tokenProtected.DecryptToString(), retryHandler, retryNotificationFunc);
        }

        /// <summary>
        /// Creates Teams API client for v1 API with retry feature.
        /// <see cref="TeamsRetryHandler"/> or <see cref="TeamsRetryOnErrorHandler"/> can be specified so that the client can retry on HTTP 429 and optinally 500, 502, 503 and 504 response.
        /// </summary>
        /// <param name="tokenChars">Teams API token of char array.</param>
        /// <param name="retryHandler">Handler for retry.</param>
        /// <param name="retryNotificationFunc">Notification func where is notified before a retry. The func receives <see cref="TeamsResultInfo"/> for response info and int for trial counter. If the func returns false, the retry is cancelled.</param>
        /// <returns>Teams API client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.TeamsAPIClient CreateVersion1Client(char[] tokenChars, TeamsRetry retryHandler, Func<TeamsResultInfo, int, bool> retryNotificationFunc = null)
        {
            return CreateVersion1Client(new String(tokenChars), retryHandler, retryNotificationFunc);
        }

        /// <summary>
        /// Creates Teams API client for v1 API with retry feature.
        /// <see cref="TeamsRetryHandler"/> or <see cref="TeamsRetryOnErrorHandler"/> can be specified so that the client can retry on HTTP 429 and optinally 500, 502, 503 and 504 response.
        /// </summary>
        /// <param name="tokenString">Teams API token of string.</param>
        /// <param name="retryHandler">Handler for retry.</param>
        /// <param name="retryNotificationFunc">Notification func where is notified before a retry. The func receives <see cref="TeamsResultInfo"/> for response info and int for trial counter. If the func returns false, the retry is cancelled.</param>
        /// <returns>Teams API client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.TeamsAPIClient CreateVersion1Client(string tokenString, TeamsRetry retryHandler, Func<TeamsResultInfo, int, bool> retryNotificationFunc = null)
        {
            return new Thrzn41.WebexTeams.Version1.TeamsAPIClient(tokenString, retryHandler, retryNotificationFunc);
        }

        /// <summary>
        /// Creates Teams API client for v1 API with retry feature.
        /// <see cref="TeamsRetryHandler"/> or <see cref="TeamsRetryOnErrorHandler"/> can be specified so that the client can retry on HTTP 429 and optinally 500, 502, 503 and 504 response.
        /// </summary>
        /// <param name="tokenInfo">Teams API token info.</param>
        /// <param name="retryHandler">Handler for retry.</param>
        /// <param name="retryNotificationFunc">Notification func where is notified before a retry. The func receives <see cref="TeamsResultInfo"/> for response info and int for trial counter. If the func returns false, the retry is cancelled.</param>
        /// <returns>Teams API client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.TeamsAPIClient CreateVersion1Client(Thrzn41.WebexTeams.Version1.AccessTokenInfo tokenInfo, TeamsRetry retryHandler, Func<TeamsResultInfo, int, bool> retryNotificationFunc = null)
        {
            return new Thrzn41.WebexTeams.Version1.TeamsAPIClient(tokenInfo.AccessToken, retryHandler, retryNotificationFunc);
        }

        #endregion




        #region Teams v1 Admin API Client

        /// <summary>
        /// Creates Teams Admin API client for v1 API.
        /// </summary>
        /// <param name="tokenProtected">Teams API token of <see cref="ProtectedString"/></param>
        /// <returns>Teams Admin API client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.Admin.TeamsAdminAPIClient CreateVersion1AdminClient(ProtectedString tokenProtected)
        {
            return CreateVersion1AdminClient(tokenProtected.DecryptToString());
        }

        /// <summary>
        /// Creates Teams Admin API client for v1 API.
        /// </summary>
        /// <param name="tokenChars">Teams API token of char array.</param>
        /// <returns>Teams Admin API client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.Admin.TeamsAdminAPIClient CreateVersion1AdminClient(char[] tokenChars)
        {
            return CreateVersion1AdminClient(new String(tokenChars));
        }

        /// <summary>
        /// Creates Teams Admin API client for v1 API.
        /// </summary>
        /// <param name="tokenString">Teams API token of string.</param>
        /// <returns>Teams Admin API client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.Admin.TeamsAdminAPIClient CreateVersion1AdminClient(string tokenString)
        {
            return new Thrzn41.WebexTeams.Version1.Admin.TeamsAdminAPIClient(tokenString);
        }

        /// <summary>
        /// Creates Teams Admin API client for v1 API.
        /// </summary>
        /// <param name="tokenInfo">Teams API token info.</param>
        /// <returns>Teams Admin API client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.Admin.TeamsAdminAPIClient CreateVersion1AdminClient(Thrzn41.WebexTeams.Version1.AccessTokenInfo tokenInfo)
        {
            return new Thrzn41.WebexTeams.Version1.Admin.TeamsAdminAPIClient(tokenInfo.AccessToken);
        }

        #endregion


        #region Teams v1 Admin API Client with Retry

        /// <summary>
        /// Creates Teams Admin API client for v1 API with retry feature.
        /// <see cref="TeamsRetryHandler"/> or <see cref="TeamsRetryOnErrorHandler"/> can be specified so that the client can retry on HTTP 429 and optinally 500, 502, 503 and 504 response.
        /// </summary>
        /// <param name="tokenProtected">Teams API token of <see cref="ProtectedString"/></param>
        /// <param name="retryHandler">Handler for retry.</param>
        /// <param name="retryNotificationFunc">Notification func where is notified before a retry. The func receives <see cref="TeamsResultInfo"/> for response info and int for trial counter. If the func returns false, the retry is cancelled.</param>
        /// <returns>Teams Admin API client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.Admin.TeamsAdminAPIClient CreateVersion1AdminClient(ProtectedString tokenProtected, TeamsRetry retryHandler, Func<TeamsResultInfo, int, bool> retryNotificationFunc = null)
        {
            return CreateVersion1AdminClient(tokenProtected.DecryptToString(), retryHandler, retryNotificationFunc);
        }

        /// <summary>
        /// Creates Teams Admin API client for v1 API with retry feature.
        /// <see cref="TeamsRetryHandler"/> or <see cref="TeamsRetryOnErrorHandler"/> can be specified so that the client can retry on HTTP 429 and optinally 500, 502, 503 and 504 response.
        /// </summary>
        /// <param name="tokenChars">Teams API token of char array.</param>
        /// <param name="retryHandler">Handler for retry.</param>
        /// <param name="retryNotificationFunc">Notification func where is notified before a retry. The func receives <see cref="TeamsResultInfo"/> for response info and int for trial counter. If the func returns false, the retry is cancelled.</param>
        /// <returns>Teams Admin API client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.Admin.TeamsAdminAPIClient CreateVersion1AdminClient(char[] tokenChars, TeamsRetry retryHandler, Func<TeamsResultInfo, int, bool> retryNotificationFunc = null)
        {
            return CreateVersion1AdminClient(new String(tokenChars), retryHandler, retryNotificationFunc);
        }

        /// <summary>
        /// Creates Teams Admin API client for v1 API with retry feature.
        /// <see cref="TeamsRetryHandler"/> or <see cref="TeamsRetryOnErrorHandler"/> can be specified so that the client can retry on HTTP 429 and optinally 500, 502, 503 and 504 response.
        /// </summary>
        /// <param name="tokenString">Teams API token of string.</param>
        /// <param name="retryHandler">Handler for retry.</param>
        /// <param name="retryNotificationFunc">Notification func where is notified before a retry. The func receives <see cref="TeamsResultInfo"/> for response info and int for trial counter. If the func returns false, the retry is cancelled.</param>
        /// <returns>Teams Admin API client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.Admin.TeamsAdminAPIClient CreateVersion1AdminClient(string tokenString, TeamsRetry retryHandler, Func<TeamsResultInfo, int, bool> retryNotificationFunc = null)
        {
            return new Thrzn41.WebexTeams.Version1.Admin.TeamsAdminAPIClient(tokenString, retryHandler, retryNotificationFunc);
        }

        /// <summary>
        /// Creates Teams Admin API client for v1 API with retry feature.
        /// <see cref="TeamsRetryHandler"/> or <see cref="TeamsRetryOnErrorHandler"/> can be specified so that the client can retry on HTTP 429 and optinally 500, 502, 503 and 504 response.
        /// </summary>
        /// <param name="tokenInfo">Teams API token info.</param>
        /// <param name="retryHandler">Handler for retry.</param>
        /// <param name="retryNotificationFunc">Notification func where is notified before a retry. The func receives <see cref="TeamsResultInfo"/> for response info and int for trial counter. If the func returns false, the retry is cancelled.</param>
        /// <returns>Teams Admin API client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.Admin.TeamsAdminAPIClient CreateVersion1AdminClient(Thrzn41.WebexTeams.Version1.AccessTokenInfo tokenInfo, TeamsRetry retryHandler, Func<TeamsResultInfo, int, bool> retryNotificationFunc = null)
        {
            return new Thrzn41.WebexTeams.Version1.Admin.TeamsAdminAPIClient(tokenInfo.AccessToken, retryHandler, retryNotificationFunc);
        }

        #endregion




        #region Teams v1 OAuth2 Client

        /// <summary>
        /// Creates Teams OAuth2 client for v1 API.
        /// </summary>
        /// <param name="clientSecretProtected">Client secret of <see cref="ProtectedString"/>.</param>
        /// <param name="clientId">Client id.</param>
        /// <returns>Teams OAuth2 client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.OAuth2.TeamsOAuth2Client CreateVersion1OAuth2Client(ProtectedString clientSecretProtected, string clientId)
        {
            return new Thrzn41.WebexTeams.Version1.OAuth2.TeamsOAuth2Client(clientSecretProtected.DecryptToString(), clientId);
        }

        /// <summary>
        /// Creates Teams OAuth2 client for v1 API.
        /// </summary>
        /// <param name="clientSecretChars">Client secret of char array.</param>
        /// <param name="clientId">Client id.</param>
        /// <returns>Teams OAuth2 client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.OAuth2.TeamsOAuth2Client CreateVersion1OAuth2Client(char[] clientSecretChars, string clientId)
        {
            return new Thrzn41.WebexTeams.Version1.OAuth2.TeamsOAuth2Client(new string(clientSecretChars), clientId);
        }

        /// <summary>
        /// Creates Teams OAuth2 client for v1 API.
        /// </summary>
        /// <param name="clientSecretString">Client secret of string.</param>
        /// <param name="clientId">Client id.</param>
        /// <returns>Teams OAuth2 client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.OAuth2.TeamsOAuth2Client CreateVersion1OAuth2Client(string clientSecretString, string clientId)
        {
            return new Thrzn41.WebexTeams.Version1.OAuth2.TeamsOAuth2Client(clientSecretString, clientId);
        }

        #endregion


        #region Teams v1 OAuth2 Client with Retry

        /// <summary>
        /// Creates Teams OAuth2 client for v1 API with retry feature.
        /// <see cref="TeamsRetryHandler"/> or <see cref="TeamsRetryOnErrorHandler"/> can be specified so that the client can retry on HTTP 429 and optinally 500, 502, 503 and 504 response.
        /// </summary>
        /// <param name="clientSecretProtected">Client secret of <see cref="ProtectedString"/>.</param>
        /// <param name="clientId">Client id.</param>
        /// <param name="retryHandler">Handler for retry.</param>
        /// <param name="retryNotificationFunc">Notification func where is notified before a retry. The func receives <see cref="TeamsResultInfo"/> for response info and int for trial counter. If the func returns false, the retry is cancelled.</param>
        /// <returns>Teams OAuth2 client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.OAuth2.TeamsOAuth2Client CreateVersion1OAuth2Client(ProtectedString clientSecretProtected, string clientId, TeamsRetry retryHandler, Func<TeamsResultInfo, int, bool> retryNotificationFunc = null)
        {
            return new Thrzn41.WebexTeams.Version1.OAuth2.TeamsOAuth2Client(clientSecretProtected.DecryptToString(), clientId, retryHandler, retryNotificationFunc);
        }

        /// <summary>
        /// Creates Teams OAuth2 client for v1 API with retry feature.
        /// <see cref="TeamsRetryHandler"/> or <see cref="TeamsRetryOnErrorHandler"/> can be specified so that the client can retry on HTTP 429 and optinally 500, 502, 503 and 504 response.
        /// </summary>
        /// <param name="clientSecretChars">Client secret of char array.</param>
        /// <param name="clientId">Client id.</param>
        /// <param name="retryHandler">Handler for retry.</param>
        /// <param name="retryNotificationFunc">Notification func where is notified before a retry. The func receives <see cref="TeamsResultInfo"/> for response info and int for trial counter. If the func returns false, the retry is cancelled.</param>
        /// <returns>Teams OAuth2 client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.OAuth2.TeamsOAuth2Client CreateVersion1OAuth2Client(char[] clientSecretChars, string clientId, TeamsRetry retryHandler, Func<TeamsResultInfo, int, bool> retryNotificationFunc = null)
        {
            return new Thrzn41.WebexTeams.Version1.OAuth2.TeamsOAuth2Client(new string(clientSecretChars), clientId, retryHandler, retryNotificationFunc);
        }

        /// <summary>
        /// Creates Teams OAuth2 client for v1 API with retry feature.
        /// <see cref="TeamsRetryHandler"/> or <see cref="TeamsRetryOnErrorHandler"/> can be specified so that the client can retry on HTTP 429 and optinally 500, 502, 503 and 504 response.
        /// </summary>
        /// <param name="clientSecretString">Client secret of string.</param>
        /// <param name="clientId">Client id.</param>
        /// <param name="retryHandler">Handler for retry.</param>
        /// <param name="retryNotificationFunc">Notification func where is notified before a retry. The func receives <see cref="TeamsResultInfo"/> for response info and int for trial counter. If the func returns false, the retry is cancelled.</param>
        /// <returns>Teams OAuth2 client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.OAuth2.TeamsOAuth2Client CreateVersion1OAuth2Client(string clientSecretString, string clientId, TeamsRetry retryHandler, Func<TeamsResultInfo, int, bool> retryNotificationFunc = null)
        {
            return new Thrzn41.WebexTeams.Version1.OAuth2.TeamsOAuth2Client(clientSecretString, clientId, retryHandler, retryNotificationFunc);
        }

        #endregion





        #region Teams v1 Guest Issuer Client

        /// <summary>
        /// Creates Teams Guest Issuer client for v1 API.
        /// </summary>
        /// <param name="secretProtected">Secret of <see cref="ProtectedString"/>.</param>
        /// <param name="guestIssuerId">Guest Issuer Id.</param>
        /// <returns>Teams Guest Issuer client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.GuestIssuer.TeamsGuestIssuerClient CreateVersion1GuestIssuerClient(ProtectedString secretProtected, string guestIssuerId)
        {
            return new Thrzn41.WebexTeams.Version1.GuestIssuer.TeamsGuestIssuerClient(secretProtected.DecryptToString(), guestIssuerId);
        }

        /// <summary>
        /// Creates Teams Guest Issuer client for v1 API.
        /// </summary>
        /// <param name="secretChars">Secret of char array.</param>
        /// <param name="guestIssuerId">Guest Issuer Id.</param>
        /// <returns>Teams Guest Issuer client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.GuestIssuer.TeamsGuestIssuerClient CreateVersion1GuestIssuerClient(char[] secretChars, string guestIssuerId)
        {
            return new Thrzn41.WebexTeams.Version1.GuestIssuer.TeamsGuestIssuerClient(new string(secretChars), guestIssuerId);
        }

        /// <summary>
        /// Creates Teams Guest Issuer client for v1 API.
        /// </summary>
        /// <param name="secretString">Secret of string.</param>
        /// <param name="guestIssuerId">Guest Issuer Id.</param>
        /// <returns>Teams Guest Issuer client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.GuestIssuer.TeamsGuestIssuerClient CreateVersion1GuestIssuerClient(string secretString, string guestIssuerId)
        {
            return new Thrzn41.WebexTeams.Version1.GuestIssuer.TeamsGuestIssuerClient(secretString, guestIssuerId);
        }

        #endregion


        #region Teams v1 Guest Issuer Client with Retry

        /// <summary>
        /// Creates Teams Guest Issuer client for v1 API with retry feature.
        /// <see cref="TeamsRetryHandler"/> or <see cref="TeamsRetryOnErrorHandler"/> can be specified so that the client can retry on HTTP 429 and optinally 500, 502, 503 and 504 response.
        /// </summary>
        /// <param name="secretProtected">Secret of <see cref="ProtectedString"/>.</param>
        /// <param name="guestIssuerId">Guest Issuer Id.</param>
        /// <param name="retryHandler">Handler for retry.</param>
        /// <param name="retryNotificationFunc">Notification func where is notified before a retry. The func receives <see cref="TeamsResultInfo"/> for response info and int for trial counter. If the func returns false, the retry is cancelled.</param>
        /// <returns>Teams Guest Issuer client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.GuestIssuer.TeamsGuestIssuerClient CreateVersion1GuestIssuerClient(ProtectedString secretProtected, string guestIssuerId, TeamsRetry retryHandler, Func<TeamsResultInfo, int, bool> retryNotificationFunc = null)
        {
            return new Thrzn41.WebexTeams.Version1.GuestIssuer.TeamsGuestIssuerClient(secretProtected.DecryptToString(), guestIssuerId, retryHandler, retryNotificationFunc);
        }

        /// <summary>
        /// Creates Teams Guest Issuer client for v1 API with retry feature.
        /// <see cref="TeamsRetryHandler"/> or <see cref="TeamsRetryOnErrorHandler"/> can be specified so that the client can retry on HTTP 429 and optinally 500, 502, 503 and 504 response.
        /// </summary>
        /// <param name="secretChars">Secret of char array.</param>
        /// <param name="guestIssuerId">Guest Issuer Id.</param>
        /// <param name="retryHandler">Handler for retry.</param>
        /// <param name="retryNotificationFunc">Notification func where is notified before a retry. The func receives <see cref="TeamsResultInfo"/> for response info and int for trial counter. If the func returns false, the retry is cancelled.</param>
        /// <returns>Teams Guest Issuer client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.GuestIssuer.TeamsGuestIssuerClient CreateVersion1GuestIssuerClient(char[] secretChars, string guestIssuerId, TeamsRetry retryHandler, Func<TeamsResultInfo, int, bool> retryNotificationFunc = null)
        {
            return new Thrzn41.WebexTeams.Version1.GuestIssuer.TeamsGuestIssuerClient(new string(secretChars), guestIssuerId, retryHandler, retryNotificationFunc);
        }

        /// <summary>
        /// Creates Teams Guest Issuer client for v1 API with retry feature.
        /// <see cref="TeamsRetryHandler"/> or <see cref="TeamsRetryOnErrorHandler"/> can be specified so that the client can retry on HTTP 429 and optinally 500, 502, 503 and 504 response.
        /// </summary>
        /// <param name="secretString">Secret of string.</param>
        /// <param name="guestIssuerId">Guest Issuer Id.</param>
        /// <param name="retryHandler">Handler for retry.</param>
        /// <param name="retryNotificationFunc">Notification func where is notified before a retry. The func receives <see cref="TeamsResultInfo"/> for response info and int for trial counter. If the func returns false, the retry is cancelled.</param>
        /// <returns>Teams Guest Issuer client for v1 API.</returns>
        public static Thrzn41.WebexTeams.Version1.GuestIssuer.TeamsGuestIssuerClient CreateVersion1GuestIssuerClient(string secretString, string guestIssuerId, TeamsRetry retryHandler, Func<TeamsResultInfo, int, bool> retryNotificationFunc = null)
        {
            return new Thrzn41.WebexTeams.Version1.GuestIssuer.TeamsGuestIssuerClient(secretString, guestIssuerId, retryHandler, retryNotificationFunc);
        }

        #endregion


    }

}

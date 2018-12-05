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
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Thrzn41.WebexTeams
{


    /// <summary>
    /// The result info of Teams API request.
    /// </summary>
    public class TeamsResultInfo
    {


        /// <summary>
        /// Creates TeamsResultInfo.
        /// </summary>
        public TeamsResultInfo()
        {
            this.TransactionId = Guid.NewGuid();
        }


        /// <summary>
        /// Transaction id for this result.
        /// </summary>
        public Guid TransactionId { get; internal set; }

        /// <summary>
        /// Indicats the request has been succeeded or not.
        /// </summary>
        public bool IsSuccessStatus { get; internal set; }

        /// <summary>
        /// Http status code returned on the API request.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; internal set; }

        /// <summary>
        /// Tracking id of the request.
        /// This id can be used for technical support.
        /// </summary>
        public string TrackingId { get; internal set; }

        /// <summary>
        /// Indicates whether the result has tracking id or not.
        /// </summary>
        public bool HasTrackingId
        {
            get
            {
                return (!String.IsNullOrEmpty(this.TrackingId));
            }
        }

        /// <summary>
        /// Retry-After header value.
        /// </summary>
        public RetryConditionHeaderValue RetryAfter { get; internal set; }

        /// <summary>
        /// Indicates the request has Retry-After header value.
        /// </summary>
        public bool HasRetryAfter
        {
            get
            {
                return (this.RetryAfter != null);
            }
        }


        /// <summary>
        /// <see cref="TeamsRequestInfo"/> for this request.
        /// </summary>
        internal TeamsRequestInfo RequestInfo { get; set; }


        /// <summary>
        /// Request line of this request.
        /// </summary>
        public string RequestLine
        {
            get
            {
                if(this.RequestInfo == null)
                {
                    return "UNKNOWN * HTTP/1.1";
                }

                return this.RequestInfo.GetRequestLine();
            }
        }


    }
}

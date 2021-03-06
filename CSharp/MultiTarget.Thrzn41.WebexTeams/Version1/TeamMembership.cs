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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thrzn41.WebexTeams.Version1
{

    /// <summary>
    /// Cisco Webex Teams TeamMembership object.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class TeamMembership : TeamsData
    {

        /// <summary>
        /// Id of the membership.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; internal set; }

        /// <summary>
        /// Team id of the membership.
        /// </summary>
        [JsonProperty(PropertyName = "teamId")]
        public string TeamId { get; internal set; }

        /// <summary>
        /// Person id of the membership.
        /// </summary>
        [JsonProperty(PropertyName = "personId")]
        public string PersonId { get; internal set; }

        /// <summary>
        /// Person email of the membership.
        /// </summary>
        [JsonProperty(PropertyName = "personEmail")]
        public string PersonEmail { get; internal set; }

        /// <summary>
        /// Person display name of the membership.
        /// </summary>
        [JsonProperty(PropertyName = "personDisplayName")]
        public string PersonDisplayName { get; internal set; }

        /// <summary>
        /// Person organization of the membership.
        /// </summary>
        [JsonProperty(PropertyName = "personOrgId")]
        public string PersonOrganizationId { get; internal set; }

        /// <summary>
        /// Indicates the membership person is moderator or not.
        /// </summary>
        [JsonProperty(PropertyName = "isModerator")]
        public bool? IsModerator { get; internal set; }

        /// <summary>
        /// <see cref="DateTime"/> when the membership was created.
        /// </summary>
        [JsonProperty(PropertyName = "created")]
        public DateTime? Created { get; internal set; }

    }

}

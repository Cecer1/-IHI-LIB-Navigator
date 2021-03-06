﻿#region GPLv3

// 
// Copyright (C) 2012  Chris Chenery
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 

#endregion

#region Usings

using System.Collections.Generic;

#endregion

namespace IHI.Server.Libraries.Cecer1.Navigator

{
    public abstract class Listing
    {
        #region Events
        public event ListingEventHandler OnMove;
        public static event ListingEventHandler OnMoveAny;
        #endregion

        #region Fields

        private Category _primaryCategory;

        #endregion

        #region Properties
        internal Navigator Navigator { get; set; }

        /// <summary>
        /// The ID that will be sent to the client.
        /// </summary>
        public int ID { get; set; }

        public Category PrimaryCategory
        {
            set
            {
                if (PrimaryCategory == value)
                    return;

                if (_primaryCategory != null)
                    _primaryCategory.RemoveListing(this);

                _primaryCategory = value;
                if (_primaryCategory != null)
                    _primaryCategory.AddListing(this);

                if (OnMove != null)
                    OnMove.Invoke(this, new ListingEventArgs
                                    {
                                        Navigator = Navigator
                                    });

                if (OnMoveAny != null)
                    OnMoveAny.Invoke(this, new ListingEventArgs
                                            {
                                                Navigator = Navigator
                                            });
            }
            get { return _primaryCategory; }
        }

        /// <summary>
        ///   The name of the room.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///   The current population of the room.
        /// </summary>
        public byte Population { get; set; }

        /// <summary>
        ///   The maximum population of the room.
        /// </summary>
        public byte Capacity { get; set; }

        #endregion
    }
}
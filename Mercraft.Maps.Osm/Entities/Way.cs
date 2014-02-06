﻿using System.Collections.Generic;
using Mercraft.Maps.Core;

namespace Mercraft.Maps.Osm.Entities
{
    /// <summary>
    /// Represents a simple way.
    /// </summary>
    public class Way : Element
    {
        /// <summary>
        /// Creates a new simple way.
        /// </summary>
        public Way()
        {
            this.Type = ElementType.Way;
        }

        /// <summary>
        /// Holds the list of nodes.
        /// </summary>
        public List<long>  NodeIds { get; set; }

        public List<Node> Nodes { get; set; }


        /// <summary>
        /// Returns all the coordinates in this way in the same order as the nodes.
        /// </summary>
        /// <returns></returns>
        public List<GeoCoordinate> GetCoordinates()
        {
            var coordinates = new List<GeoCoordinate>();

            for (int idx = 0; idx < this.Nodes.Count; idx++)
            {
                coordinates.Add(this.Nodes[idx].Coordinate);
            }

            return coordinates;
        }

        /// <summary>
        /// Returns true if this way is closed (firstnode == lastnode).
        /// </summary>
        /// <returns></returns>
        public bool IsClosed()
        {
            return this.Nodes != null &&
                this.Nodes.Count > 1 &&
                this.Nodes[0].Id == this.Nodes[this.Nodes.Count - 1].Id;
        }

        /// <summary>
        /// Returns a description of this object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string tags = "{no tags}";
            if (this.Tags != null && this.Tags.Count > 0)
            {
                tags = this.Tags.ToString();
            }
            if (!this.Id.HasValue)
            {
                return string.Format("Way[null]{0}", tags);
            }
            return string.Format("Way[{0}]{1}", this.Id.Value, tags);
        }

        /// <summary>
        /// Creates a new way.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static Way Create(long id, params long[] nodes)
        {
            Way way = new Way();
            way.Id = id;
            way.NodeIds = new List<long>(nodes);
            return way;
        }

        /// <summary>
        /// Creates a new way.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nodes"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public static Way Create(long id, ICollection<Tag> tags, params long[] nodes)
        {
            Way way = new Way();
            way.Id = id;
            way.NodeIds = new List<long>(nodes);
            way.Tags = tags;
            return way;
        }
    }
}

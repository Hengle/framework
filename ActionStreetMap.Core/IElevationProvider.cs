﻿using ActionStreetMap.Infrastructure.Reactive;

namespace ActionStreetMap.Core
{
    /// <summary> Defines behavior of elevation provider. </summary>
    public interface IElevationProvider
    {
        /// <summary> Checks whether elevation data for given bounding box is present in map data. </summary>
        /// <returns>True, if data is here.</returns>
        bool HasElevation(BoundingBox bbox);

        /// <summary> Download elevation data from server. </summary>
        /// <param name="bbox">Bounding box.</param>
        /// <returns>IObservable.</returns>
        IObservable<Unit> Download(BoundingBox bbox);

        /// <summary> Gets elevation for given latitude and longitude. </summary>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitude">Longitude.</param>
        /// <returns>Elevation.</returns>
        float GetElevation(double latitude, double longitude);

        /// <summary> Gets elevation for given map point. </summary>
        float GetElevation(MapPoint point);

        /// <summary> Sets coordinate correspongin for (0,0). </summary>
        void SetNullPoint(GeoCoordinate coordinate);
    }
}

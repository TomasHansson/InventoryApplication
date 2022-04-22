namespace AfryAnalytics.Capture.Helpers
{
    public static class LocationHelpers
    {
        public static double CalculateDistance(double longitudeA, double latitudeA, double longitudeB, double latitudeB)
        {
            var longitudeDifference = Math.Abs(longitudeA - longitudeB);
            var latitudeDifference = Math.Abs(latitudeA - latitudeB);
            if (longitudeDifference == 0 && latitudeDifference == 0)
            {
                return 0;
            }

            if (longitudeDifference == 0)
            {
                return latitudeDifference;
            }

            if (latitudeDifference == 0)
            {
                return longitudeDifference;
            }

            var squaredResults = longitudeDifference * longitudeDifference + latitudeDifference * latitudeDifference;
            return Math.Sqrt(squaredResults);
        }
    }
}

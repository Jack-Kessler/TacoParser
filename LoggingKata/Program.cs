using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            string[] lines = File.ReadAllLines(csvPath);

            // Log an error if you get 0 lines and a warning if you get 1 line
            if (lines.Length == 0)
            {
                logger.LogError("Error: 0 Lines.");
            }
            else if (lines.Length == 1)
            {
                logger.LogError("Error: 0 Lines.");
                logger.LogWarning("Warning: 1 line.");
            }

            //Loop necessary here?
            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            TacoParser parserInstance = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            ITrackable[] locations = lines.Select(x => parserInstance.Parse(x)).ToArray();

            // DON'T FORGET TO LOG YOUR STEPS

            // Now that your Parse method is completed, START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // Create a `double` variable to store the distance
            ITrackable tB1 = null;
            ITrackable tB2 = null;
            double distance = 0;

            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            //HINT NESTED LOOPS SECTION---------------------
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)
            for (int i = 0; i < locations.Length -1; i++)
            {
                ITrackable locA = locations[i];

                // Create a new corA Coordinate with your locA's lat and long

                GeoCoordinate corA = new GeoCoordinate();
                corA.Latitude = locA.Location.Latitude;
                corA.Longitude = locA.Location.Longitude;

                for (int j = i+1; j <locations.Length; j++)
                {
                    // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)
                    ITrackable locB = locations[j];

                    // Create a new Coordinate with your locB's lat and long
                    GeoCoordinate corB = new GeoCoordinate();
                    corB.Latitude = locB.Location.Latitude;
                    corB.Longitude = locB.Location.Longitude;

                    // Now, compare the two using `.GetDistanceTo()`, which returns a double
                    double currentDist = corA.GetDistanceTo(corB);
                    // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above
                    if (currentDist > distance)
                    {
                        distance = currentDist;
                        tB1 = locA;
                        tB2 = locB;
                    }
                }
            }
            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.
            Console.WriteLine("*****************************************************************************************\n\n");
            Console.WriteLine($"The two Taco Bell locations farthest away from eachother are {tB1.Name} and {tB2.Name}.\n They are located {distance} meters apart!\n\n");
        }
    }
}

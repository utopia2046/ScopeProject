using Microsoft.SCOPE.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;

public static class Utils
{

    public static string MVGetFlightNum(String flightInfo, String fligthLine)
    {
        if (String.IsNullOrEmpty(flightInfo))
        {
            return String.Empty;
        }

        //split the string to different flight lines
        String[] flights = flightInfo.Split(';');
        if (flights == null || flights.Length == 0)
        {
            return String.Empty;
        }

        foreach (String flight in flights)
        {
            String[] infos = flight.Split('|');
            if (infos == null || infos.Length != 4)
            {
                continue;
            }

            //AdsFLN7
            if (infos[0].Equals(fligthLine, StringComparison.InvariantCultureIgnoreCase))
            {
                //return the flight Id
                return infos[1];
            }
        }
        return String.Empty;
    }
}

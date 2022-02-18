using Microsoft.SCOPE.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ScopeRuntime;
using System.Linq;

public static class Tools
{
    public static bool IsFraudItem(string pagePosition, bool isFraud, int fraudQualityType, byte[] fraudReason)
    {
        // BT ads that are duplicates of ML have IsFraud = true and FraudQualityType = 250
        // In this case to detect if the ad is actually fraud, use FraudReason check (if any bits are set then it is Fraud)
        if (pagePosition != null && pagePosition.StartsWith("BT") && fraudQualityType == 250)
        {
            // Check if any bit is set
            return fraudReason.Any(b => b != 0);
        }

        return isFraud;
    }

    public static string ParseFlightInfoRecordList(string FlightInfoRecordList, string LineID)
    {
        if (string.IsNullOrEmpty(FlightInfoRecordList))
            return (string)null;
        string str1 = "";
        int num = FlightInfoRecordList.IndexOf(LineID + "|");
        if (num > -1)
        {
            string str2 = FlightInfoRecordList.Substring(num + LineID.Length + 1);
            if (str2.IndexOf("|") > -1)
                str1 = str2.Substring(0, str2.IndexOf("|"));
        }
        return str1;
    }

    public static int? ParseFlightNumber(string FlightInfoRecordList, string LineID)
    {
        string flightInfoRecordList = Tools.ParseFlightInfoRecordList(FlightInfoRecordList, LineID);
        int result = -1;
        return int.TryParse(flightInfoRecordList, out result) ? new int?(result) : new int?();
    }

    public static bool IsRelatedToYahoo(int? relatedAccountId, sbyte? marketId, int mediumId, int distribId)
    {
        return relatedAccountId == 1004 && marketId == 1 && (mediumId == 1 || mediumId == 3) && distribId == 1;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketListener
{
    /// <summary>
    /// http://www.iana.org/assignments/icmp-parameters/icmp-parameters.xhtml
    /// https://en.wikipedia.org/wiki/Internet_Control_Message_Protocol
    /// </summary>
    public enum IcmpType : int
    {
        EchoReply = 0,
        Unassigned1 = 1,
        Unassigned2 = 2,
        DestinationUnreachable = 3,
        SourceQuench = 4,
        Redirect = 5,
        AlternateHostAddress = 6,
        Unassigned = 7,
        Echo = 8,
        RouterAdvertisement = 9,
        RouterSelection = 10,
        TimeExceeded = 11,
        ParameterProblem = 12,
        Timestamp = 13,
        TimestampReply = 14,
        InformationRequest = 15,
        InformationReply = 16,
        AddressMaskRequest = 17,
        AddressMaskReply = 18,
        ReservedForSecurity = 19,

        ReservedForRobustnessExperiment1 = 20,
        ReservedForRobustnessExperiment2 = 21,
        ReservedForRobustnessExperiment3 = 22,
        ReservedForRobustnessExperiment4 = 23,
        ReservedForRobustnessExperiment5 = 24,
        ReservedForRobustnessExperiment6 = 25,
        ReservedForRobustnessExperiment7 = 26,
        ReservedForRobustnessExperiment8 = 27,
        ReservedForRobustnessExperiment9 = 28,
        ReservedForRobustnessExperiment10 = 29,

        Traceroute = 30,

        DatagramConversionError = 31,

        MobileHostRedirect = 32,

        IPv6WhereAreYou = 33,
        IPv6IAmHere = 34,

        MobileRegistrationRequest = 35,
        MobileRegistrationReply = 36,

        DomainNameRequest = 37,
        DomainNameReply = 38,

        SKIP = 39,

        Photuris = 40

        // 42 - 255 = Reserved

    }
}

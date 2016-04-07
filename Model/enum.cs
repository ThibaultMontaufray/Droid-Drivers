
namespace RobotLibrary
{
    /// <summary>
    /// Direction of rotation
    /// </summary>
    public enum DIRECTION
    {
        NONE = 0,
        HORAIRE = 1,
        TRIGO = -1
    }
    /// <summary>
    /// Design the phase of the game we are
    /// </summary>
    public enum GAME_PHASE
    {
        UNKNOW,
        WAIT_COLOR_SELECTION,
        INIT_POSITION,
        WAIT_GO_GAME,
        IN_GAME,
        GAME_OVER
    }
    /// <summary>
    /// List of available color for the robot team
    /// </summary>
    public enum TEAM_COLOR
    {
        NONE,
        RED,
        YELLOW
    }
    /// <summary>
    /// List of activation status
    /// </summary>
    public enum STATUS
    {
        UNKNOWN,
        ACTIVATED,
        DISABLED
    }
    /// <summary>
    /// List of byte for serial communication.
    /// </summary>
    public enum TYPE_PACKET : byte
    {
        ERREUR_COMMUNICATION = 250,

        NONE,
        ACK = 1,
        NACK = 2,
        WHOIS = 3,
        CardAnimator = 4,
        CardPropulsor = 5,
        CardArms = 6,

        MODE_LOCK = 10,
        MODE_PENDING = 11,
        MODE_GETINSTRUCTION = 12,
        MODE_SHUTDOWN = 13,
        MODE_MATCH = 14,

        RequestIRALL = 50,
        RequestIR1 = 51,
        RequestIR2 = 52,
        RequestIR3 = 53,
        RequestIR4 = 54,
        RequestIR5 = 55,
        RequestIR6 = 56,
        RequestIR7 = 57,
        RequestIR8 = 58,

        RequestCompas = 60,
        RequestUS1 = 61,
        RequestUS2 = 62,
        RequestUS3 = 63,
        RequestUS4 = 64,
        RequestStart = 65,
        RequestColorChoosen = 66,
        RequestStarterReady = 67,

        BaliseModeWait = 70,
        BaliseModeRed = 71,
        BaliseModeYellow = 72,
        BaliseModeLocked = 73,
        BaliseModeFree = 74,
        BaliseAnimation = 75,

        ArmLeftLoad = 80,
        ArmLeftFire = 81,
        ArmRightLoad = 82,
        ArmRightFire = 83,
        ArmLeftFocus = 84,
        ArmRightFocus = 85,
        ArmLeftReset = 86,
        ArmRightReset = 87,

        AnswerIRALL = 150,
        AnswerIR0 = 151,
        AnswerIR1 = 152,
        AnswerIR2 = 153,
        AnswerIR3 = 154,
        AnswerIR4 = 155,
        AnswerIR5 = 156,
        AnswerIR6 = 157,
        AnswerIR7 = 158,

        AnswerCompas = 160,
        AnswerUS1 = 161,
        AnswerUS2 = 162,
        AnswerUS3 = 163,
        AnswerUS4 = 164,
        AnswerStart = 165,
        AnswerColorChoosen = 166,
        AnswerStarterReady = 167,
        
        GoLeft = 170,
        GoRight = 171,
        BackLeft = 172,
        BackRight = 173
    }
}
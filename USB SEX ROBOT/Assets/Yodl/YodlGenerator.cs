using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class YodlGenerator : MonoBehaviour
{

    public string createdPost;

    public bool createNewPost = false;

    public Sprite[] avatar;

    public Sprite POTUSPortrait;
    public Sprite GovPortrait;
    public Sprite SNNPortrait;

    private void Awake()
    {
        NewGenerateMessage();  
    }

    public void NewGenerateMessage()
    {
        Text messageText = gameObject.GetComponentInChildren<Text>();
        messageText.text = GenerateMessage();

        StartCoroutine(KillMyself());
    }

    IEnumerator KillMyself()
    {
        yield return new WaitForSeconds(4);
        Debug.LogWarning("KILLING MYSELF");
        DestroyObject(gameObject);
    }

    public string[] fname =
    {
        "Harold ",
        "Rarity ",
        "Betsy ",
        "Berdy ",
        "Bennie ",
        "Anton ",
        "Michael ",
        "Danial ",
        "Jordi ",
        "Stef ",
        "Jan ",
        "Mounim ",
        "Merry ",
        "Marijn ",
        "Jeroen ",
        "Martin ",
        "Hodor ",
        "Max ",
        "July ",
        "Gerda ",
        "Ingrid ",
        "Uniqua ",
        "Ledasha ",
        "Diamond ",
        "Ruby ",
        "Saphire ",
        "Keano ",
        "Mikkel ",
        "Jane ",
        "Donald ",
        "Saphire ",
        "Peter ",
        "Trevor ",
        "Charlie ",
        "Bernie ",
        "Kevin ",
        "Carl ",
        "Jim ",
        "Stefan ",
        "Johnny ",
        "Arnold ",
        "Robbie ",
        "Maxwell ",
        "Gerald ",
        "Andy ",
        "Jack ",
        "Stefano ",
        "Will ",
        "Ann ",
        "Anna ",
        "Klaus ",
        "Bart ",
        "Werner ",
        "Charles",
        "Jason ",
        "Keith ",
        "Matthijs ",
        "Mannu ",
        "Yorick ",
        "Rik ",
        "Ilona ",
        "Randy ",
        "Dastina ",
        "Reginald ",
        "Denny ",
        "Manuel ",
        "Reman ",
        "Veronica ",
        "Nicholas ",
        "Daniel ",
        "Varis ",
        "Bob ",
        "Leslie",
        "Carolin ",
        "Alex ",
        "Ryan ",
        "Mila "

    };

    public string[] twitterHandle =
    {
        "BlackSamurai",
        "DankSteven",
        "AverageArnold",
        "billyBam",
        "joeStar",
        "gregGor",
        "petePoep",
        "killerFiller",
        "pancakehead",
        "antioch",
        "joeman",
        "kiffresh",
        "moumeme",
        "warriorjan",
        "xArcky",
        "darkangel291",
        "tyler777",
        "theStorm",
        "bavariafan",
        "tylerswifter",
        "mrBondJames",
        "monstarr",
        "maanster",
        "henkdevries",
        "kamehameha",
        "gohansama",
        "diowryyyy",
        "ArnoldJames",
        "Gibson",
        "chocolateboy",
        "darkoman",
        "ilama",
        "blommenman",
        "NickLamb",
        "MichelWarrun",
        "AlexCamilleri",
        "DazzleDan",
        "Jedi_Mocro",
        "Twirlbound",
        "Lordbound",
        "PineIsFine",
        "Mvdlaar",
        "ikaros",
        "KojimaKun",
        "FalloutMan69",
        "Skyrimfan21",
        "rockNeverDies",
        "RatchetAudio",
        "punchespears",
        "Vlambe",
        "TomNopper",
        "JohnWicker",
        "JoJoJoStar",
        "memelord",
        "chronenberg",
        "heisenberg",
        "leslieman",
        "white_knight",
        "weeaboom",
        "loliconcert",
        "Superstarrr",
        "PhazerBlazer",
        "Aceria_",
        "NinjaGinja",
        "SamuraiPizzaCat",
        "Blackbird",
        "JimiHendrix",
        "SlashFan211",
        "HanzoMain",
        "GenjiMain",
        "TheRealBestGirl",
        "Gokamehamea",
        "MudaMudaMuda",
        "OraOraOra",
        "queenofblood",
        "420blazer",
        "cuteboybigboy",
        "chocolateboyyy",
        "chocolategarcon",
        "TaxMaster",
        "Tyker",
        "BikerMan9271",
        "LadyBloodwind",
        "MasterOfDisaster",
        "Sniper777",
        "quickscope666",
        "famousPotato",
        "DUTCHLIONGOD",
        "GodOfGamers",
        "MisterBrostar",
        "Pikablue",
        "RickRollingston",
        "gamergirl1337"
    };

    public string[] message =
    {
        "GOODBYE MY LOVER",
        "OH MY GOD",
        "WHAT A MANIAC",
        "I DON'T WANNA DIE",
        "GOODBYE CRUEL WORLD",
        "BY THE EIGHT DIVINES",
        "NO NO NO",
        "WHAT THE FRICK",
        "AAAAAAAAAH",
        "AAAAAAAAAAAAAAAAAAH",
        "WAAAAAAAAAAAAA",
        "WAAAAARGHH",
        "WOOP",
        "FINALLY THE SWEET RELEASE OF DEATH",
        "I'M TOO YOUNG TO DIE",
        "COPS ARE KILLING EVERYONE",
        "FRACK THE POLICE",
        "THIS CAN'T BE HAPPENING!",
        "LEAVING GOODBYE",
        "RUN FOR YOUR LIVES",
        "RUN RUUUUN",
        "GET OUT OF THE WAY",
        "IT'S A CRAZY COP",
        "THE POLICE HAS GONE ROGUE",
        "WOAH",
        "DEATH CANNOT STOP ME",
        "NEVER SHOULD'VE COME HERE",
        "RRREEEEEEEEEEEEEEEEEEEE",
        "FROCK THIS SHIZNIT",
        "EVERYONE SAID I WAS CRAZY GUESS WHO'S CRAZY NOW",
        "THE RAPTURE IS UPON US",
        "OMG LIKE WHAT'S GOING ON EVEN",
        "IT'S A TERRORIST ATTACK",
        "MOVE DITCH, GET OUT THE WAY",
        "LEARN TO DRIVE, CREEP",
        "WHAT ARE YOU 12",
        "THIS IS DUMB AND STUPID",
        "WAKE ME UP",
        "HELP",
        "HELP HEEEELP",
        "OH NO",
        "I HATE MY LIFE",
        "GOODBYE CRUEL LIFE",
        "DRIVE ON THE ROAD",
        "THIS IS BULLCRAP",
        "I'M GOING TO DIE",
        "CAN'T WAKE UP",
        "I JUST GOT A PROMOTION",
        "MY WIFE IS PREGNANT",
        "GOSH DARNIT",
        "N'WAH",
        "YOU HANZO MAIN",
        "DEJIMA CHAAAAAAN",
        "SENPAI NO",
        "NANI",
        "OH GOLLY",
        "OH GEE WIZ",
        "MAXIMUM DRIFT",
        "DO YOU EVEN DRIFT",
        "CUT MY LIFE INTO PIECES",
        "JUST KILL ME ALREADY",
        "I WANNA DIE",
        "DAMN THOSE IMMIGRANTS",
        "GREAT START TO MY DAY",
        "WHAT A WAY TO DIE",    
        "PROPER DRIVING MATE",
        "BABY NO",
        "PLEASE I HAVE KIDS",
        "NOT THE FACE NOT THE FACE",
        "FINALLY DEATH",
        "WHAT A NICE CAR",
        "WATCH IT",
        "I LIKE LIVING THANK YOU VERY MUCH",
        "NO I LIKE MY LIFE",
        "STOP THIS MADNESS",
        "MANIAC",
        "FREAK",
        "ARE YOU CRAZY",
        "YOU ABSOLUTE MADMAN",
        "I NEED MY COFFEE",
        "GO OUTSIDE THEY SAID",
        "I AM NEVER GOING OUTSIDE AGAIN",
        "MY LIFE SUCKS",
        "THE GOVERNMENT IS COMING TO GET ME",
        "BUILD THAT WALL",
        "YOU SUCK AT KILLING PEOPLE",
        "LEARN TO DRIVE",
        "THINK OF THE CHILDREN",
        "OH GOD THE AGONY",
        "I WISH I WAS NEVER BORN",
        "PLEASE DO NOT KILL ME",
        "LOL",
        "HAHA FINALLY DEATH",
        "I WELCOME DEATH",
        "THE SYSTEM IS RIGGED",
        "CHECKING OUT",
        "OMG",
        "ROFLMAO",
        "THIS IS LIT",
        "DAB TO PAY RESPECTS",
        "TERRORISTS TERRORISTS",
    };

    public string[] deathMessage = new string[]
    {
        "End of an Era: Cats No Longer Favorite Internet Mammal",
        "Protestors Rally: Legalization of Heroine takes Dramerica by storm",
        "You WON'T BELIEVE these 5 ways of eliminating secularism!",
        "A Dark Day in Symerian History: Patroller dies fleeing for <x> miles from Dramericans",
    };

    public string[] hashtag =
    {
        "DramericaLameAgain",
        "SyriaBestria",
        "RUINED",
        "NeverGiveUp",
        "FuckTheSystem",
        "GoodByeDramerica",
        "NOTGREAT",
        "WEW",
        "LOL",
        "ByeBye",
        "CHECKINGOUT",
        "DestinationAnywhere",
        "FuckThePOTUS",
        "OldPeopleSuck",
        "BlameTheGlobalists",
        "BURNINHELL",
        "Dramericant",
        "KeepOnDreaming",

        "Burnie2020",
        "ToSymeria",
        "BoardTheBoats",
        "FollowTheWave",
        "WaveOfGreatness",
        "DramericanNightmare",
        "WTF",
        "OMG",
        "HOMELESS",
        "AMAZING",
        "SARCASM",
        "nopenopenope",
        "Polidicks",
        "GodPls",
        "GGDramerica",
        "calledit",

        "NewLifeNoProblems"
    };



    public string[] lname =
    {
        "Abdul",
        "Pinky Pie",
        "Dildoking",
        "Assad",
        "Simpson",
        "Cunty",
        "Vaporwave",
        "Watson",
        "White",
        "Ketchum",
        "Dogson",
        "McKenzie",
        "Cupson",
        "Cuckold",
        "de Vries",
        "de Koning",
        "Banksy",
        "Neiman",
        "Botman",
        "Anderson",
        "Bloomberg",
        "Cosner",
        "Smith",
        "de Boer",
        "Smid",
        "Crystal",
        "Maluku",
        "Dutchman",
        "York",
        "Mikkelson",
        "Bertrand",
        "Sweatshop",
        "Jones",
        "Williams",
        "Taylor",
        "Johnson",
        "Walker",
        "de Jong",
        "Jansen",
        "Bakker",
        "de Wit",
        "Kok",
        "Jacobs",
        "Vos",
        "Visser",
        "Fisher",
        "Maas",
        "Post",
        "Kuiper",
        "de Leeuw"
    };

    /*public string[] myPosts = new string[]
	{
		"My", "just died",
		"I hate my boss, he is such a",
		"MMM, I'm going to have", "for lunch",
		"Did anybody see my",
		"Frikking", "invading my country and stealing our",
		"I just bought a",
		"I painted my room in a", "colour",
		"This music sounds like", "music. I wished they played REAL music",
		"I tripped over a", "AGAIN! Stupid",
		"are the best",
		"I love my", "19-6-2010 never forget",
		"Wow check out this"
	};*/

    public string[] myHalfPosts = new string[]
    {
        "Wow check out this ",
        "I cannot believe ",
        "I watched a",
        "I'm walking my ",
        "Ugh, I have to make a",
        "Why stay ",
        "I need some ",
        "I hate it when ",
        "I'm rooting for my home team the ",
        "I'm the man of the ",
        "I'm dancing with my ",
        "I fell in love with a ",
        "Keano scratched his hairy back with his ",
        "I kissed a ",
        "Never again shall I lick this crazy ",
        "I just completed hugging my ",
        "Cleaning my ",
        "Ha! I won an internet argument against ",
        "My fursona is a ",
        "Just go this ",
        "Only 90's kids will remember ",
        "Like if you want to have sex with a ",
        "I just got kicked in the groin by a full-blown ",
        "Man, I wanna get shitfaced and hump ",
        "I could enjoy some ",
        "Just fucked my "
    };

    public string[] POTUSMessage = new string[]
    {
        "This is a disgrace! Fake Dramericans leaving the country! SHAME!",
        "Totally biased reporters are slandering my name - do not trust them!",
        "Ungrateful people are leaving our country! Absolutely UN-DRAMERICAN!",
        "Symeria is WORSE than Dramerica! Don't believe the press! Dramerica First!",
        "We will make our country GREAT AGAIN!",
    };

    public string[] mySecondHalfPosts = new string[]
    {
        " it's so crazy",
        " this made me unconfortable",
        " for 12 hours",
        " what a disgrace",
        " I could never be happier",
        " I'm getting too old for this",
        " I feel enligthend",
        " I guess it's accepted in some cultures...",
        " I'm never drinking again",
        " and I liked it",
        " It's just not right",
        " I wish I could get a degree in this...",
        " thorough",
        " makes me feel euphoric",
        "kin",
        " I will write a book about this",
        " I wish my life was more exciting",
        " this is why i voet trump!",
        " for every like I will do it again",
        " I wish I could die already",
        " Linkin Park sang about this"
    };

    public string[] mySubjects = new string[]
    {
        "customer",
        "drama",
        "gender",
        "bleep",
        "rain",
        "stepson",
        "television",
        "toothbrush",
        "wasp",
        "activity",
        "aluminium",
        "bottle",
        "deodorant",
        "freeze",
        "South-America",
        "toy",
        "typhoon",
        "yodler",
        "white-supremacist",
        "hockysticks",
        "hammer",
        "white women",
        "crazy boy",
        "Keano",
        "lubed jellyjar",
        "coconut oil",
        "banana",
        "bradwurst",
        "microphone",
        "dildo",
        "Meatswipe",
        "Yodlbirdz",
        "banker",
        "Jeroen's baby",
        "South-African folk singers",
        "nature",
        "spikey apples",
        "My Little Pony hentai doushinji",
        "your moms buttabplog",
        "weird hentai written by O.J. Simpson",
        "Occulus Rift",
        "goats",
        "Pokemon",
        "Pickachu",
        "Global Game Jam 2016"
    };


    void Start()
    {
        //GenerateName();
        //GenerateMessage();
        NewGenerateMessage();
    }

    /*
    // Update is called once per frame
    void Update()
    {

        if (!createNewPost)
            return;

        GenerateName();
        GenerateMessage();

        createNewPost = false;
    }
    */


    public string CreatePost()
    {

        createdPost = fname[Random.Range(0, fname.Length)] +
                      lname[Random.Range(0, lname.Length)] +
                      myHalfPosts[Random.Range(0, myHalfPosts.Length)] +
                      mySubjects[Random.Range(0, mySubjects.Length)] +
                      mySecondHalfPosts[Random.Range(0, mySecondHalfPosts.Length)];

        return createdPost;
    }

    public Sprite GenerateAvatar()
    {
        Sprite generatedAvatar = avatar[Random.Range(0, avatar.Length)];

        return generatedAvatar;
    }

    public string GenerateName()
    {

        string generatedName;
        int handleGen = Random.Range(0, 1);

        if (handleGen == 0)
        {
            //Twitter Handle = internet name
            //Re-add this for last name:  + lname[Random.Range(0, lname.Length)]
            generatedName = fname[Random.Range(0, fname.Length)] + " @" + twitterHandle[Random.Range(0, lname.Length)];
        }

        else
        {
            //Twitter Handle = Last name
            generatedName = fname[Random.Range(0, fname.Length)] + " @" + lname[Random.Range(0, lname.Length)];
        }


        return generatedName;
    }

    public string GenerateMessage()
    {

        string generatedMessage = message[Random.Range(0, message.Length)];

        return generatedMessage;
    }

}

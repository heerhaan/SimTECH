﻿using System.ComponentModel;

namespace SimTECH
{
    public enum State
    {
        Concept = 0,
        Active,
        Closed,
        Archived,
        // NOTE: additional states are found underneath, used for some entities to control specific behaviour
        Advanced,
    }

    public enum StateFilter { All, Default, Closed, Archived }

    public enum Entrant
    {
        Unknown = 0,
        Driver,
        Team,
        Track,
        Engine,
    }

    public enum RangeType
    {
        Unknown = 0,
        Skill,
        Age,
        Team,
        Engine
    }

    public enum TargetDevelop { Main, Reliability }

    public enum Gender { All, Male, Female, Other }

    [Flags]
    public enum LeagueOptions
    {
        None = 0,
        UsePenalty = 1, EnableFatality = 2,
    }

    #region racing

    public enum Weather
    {
        Unknown = 0,
        Sunny, Overcast, Rain, Storm
    }

    public enum TeamRole
    {
        None = 0,
        Main, Support
    }

    public enum RaceStatus
    {
        Unknown = 0,
        Racing, Dnf, Dsq, Dnq, Fatal,
    }

    // TODO: You might want to rethink this enum
    public enum Incident
    {
        None = 0,
        Damage, Collision, Accident, Puncture,
        Engine,
        Electrics, Exhaust, Clutch, Hydraulics, Wheel, Brakes,
        Illegal, Fuel, Dangerous,
        Death
    }

    [Flags]
    public enum RacerEvent
    {
        Unknown = 0,
        Racing = 1,
        DriverDnf = 2, CarDnf = 4, EngineDnf = 8,
        Mistake = 16, Pitstop = 32, Swap = 64,
        Death = 128,
    }

    #endregion racing

    #region countries

    public enum FlagSize
    {
        Normal = 0,
        Tiny, Small, Large
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1154:Sort enum members.", Justification = "I don't care")]
    public enum Country
    {
        [Description("Afghanistan")] AF = 1,
        [Description("Åland Islands")] AX = 2,
        [Description("Albania")] AL = 3,
        [Description("Algeria")] DZ = 4,
        [Description("American Samoa")] AS = 5,
        [Description("Andorra")] AD = 6,
        [Description("Angola")] AO = 7,
        [Description("Anguilla")] AI = 8,
        [Description("Antarctica")] AQ = 9,
        [Description("Antigua and Barbuda")] AG = 10,
        [Description("Argentina")] AR = 11,
        [Description("Armenia")] AM = 12,
        [Description("Aruba")] AW = 13,
        [Description("Australia")] AU = 14,
        [Description("Austria")] AT = 15,
        [Description("Azerbaijan")] AZ = 16,
        [Description("Bahamas")] BS = 17,
        [Description("Bahrain")] BH = 18,
        [Description("Bangladesh")] BD = 19,
        [Description("Barbados")] BB = 20,
        [Description("Belarus")] BY = 21,
        [Description("Belgium")] BE = 22,
        [Description("Belize")] BZ = 23,
        [Description("Benin")] BJ = 24,
        [Description("Bermuda")] BM = 25,
        [Description("Bhutan")] BT = 26,
        [Description("Bolivia")] BO = 27,
        [Description("Bonaire, Sint Eustatius and Saba")] BQ = 28,
        [Description("Bosnia and Herzegovina")] BA = 29,
        [Description("Botswana")] BW = 30,
        [Description("Bouvet Island")] BV = 31,
        [Description("Brazil")] BR = 32,
        [Description("British Indian Ocean Territory")] IO = 33,
        [Description("Brunei Darussalam")] BN = 34,
        [Description("Bulgaria")] BG = 35,
        [Description("Burkina Faso")] BF = 36,
        [Description("Burundi")] BI = 37,
        [Description("Cabo Verde")] CV = 38,
        [Description("Cambodia")] KH = 39,
        [Description("Cameroon")] CM = 40,
        [Description("Canada")] CA = 41,
        [Description("Cayman Islands")] KY = 42,
        [Description("Central African Republic")] CF = 43,
        [Description("Chad")] TD = 44,
        [Description("Chile")] CL = 45,
        [Description("China")] CN = 46,
        [Description("Christmas Island")] CX = 47,
        [Description("Cocos Islands")] CC = 48,
        [Description("Colombia")] CO = 49,
        [Description("Comoros")] KM = 50,
        [Description("Congo")] CG = 51,
        [Description("Democratic Republic of Congo")] CD = 52,
        [Description("Cook Islands")] CK = 53,
        [Description("Costa Rica")] CR = 54,
        [Description("Côte d'Ivoire")] CI = 55,
        [Description("Croatia")] HR = 56,
        [Description("Cuba")] CU = 57,
        [Description("Curaçao")] CW = 58,
        [Description("Cyprus")] CY = 59,
        [Description("Czechia")] CZ = 60,
        [Description("Denmark")] DK = 61,
        [Description("Djibouti")] DJ = 62,
        [Description("Dominica")] DM = 63,
        [Description("Dominican Republic")] DO = 64,
        [Description("Ecuador")] EC = 65,
        [Description("Egypt")] EG = 66,
        [Description("El Salvador")] SV = 67,
        [Description("Equatorial Guinea")] GQ = 68,
        [Description("Eritrea")] ER = 69,
        [Description("Estonia")] EE = 70,
        [Description("Ethiopia")] ET = 71,
        [Description("European Union")] EU = 250,
        [Description("Falkland Islands")] FK = 72,
        [Description("Faroe Islands")] FO = 73,
        [Description("Fiji")] FJ = 74,
        [Description("Finland")] FI = 75,
        [Description("France")] FR = 76,
        [Description("French Guiana")] GF = 77,
        [Description("French Polynesia")] PF = 78,
        [Description("French Southern Territories")] TF = 79,
        [Description("Gabon")] GA = 80,
        [Description("Gambia")] GM = 81,
        [Description("Georgia")] GE = 82,
        [Description("Germany")] DE = 83,
        [Description("Ghana")] GH = 84,
        [Description("Gibraltar")] GI = 85,
        [Description("Greece")] GR = 86,
        [Description("Greenland")] GL = 87,
        [Description("Grenada")] GD = 88,
        [Description("Guadeloupe")] GP = 89,
        [Description("Guam")] GU = 90,
        [Description("Guatemala")] GT = 91,
        [Description("Guernsey")] GG = 92,
        [Description("Guinea")] GN = 93,
        [Description("Guinea-Bissau")] GW = 94,
        [Description("Guyana")] GY = 95,
        [Description("Haiti")] HT = 96,
        [Description("Heard Island and McDonald Islands")] HM = 97,
        [Description("Holy See")] VA = 98,
        [Description("Honduras")] HN = 99,
        [Description("Hong Kong")] HK = 100,
        [Description("Hungary")] HU = 101,
        [Description("Iceland")] IS = 102,
        [Description("India")] IN = 103,
        [Description("Indonesia")] ID = 104,
        [Description("Iran")] IR = 105,
        [Description("Iraq")] IQ = 106,
        [Description("Ireland")] IE = 107,
        [Description("Isle of Man")] IM = 108,
        [Description("Israel")] IL = 109,
        [Description("Italy")] IT = 110,
        [Description("Jamaica")] JM = 111,
        [Description("Japan")] JP = 112,
        [Description("Jersey")] JE = 113,
        [Description("Jordan")] JO = 114,
        [Description("Kazakhstan")] KZ = 115,
        [Description("Kenya")] KE = 116,
        [Description("Kiribati")] KI = 117,
        [Description("Kosovo")] XK = 251,
        [Description("North-Korea")] KP = 118,
        [Description("South-Korea")] KR = 119,
        [Description("Kuwait")] KW = 120,
        [Description("Kyrgyzstan")] KG = 121,
        [Description("Lao People's Democratic Republic")] LA = 122,
        [Description("Latvia")] LV = 123,
        [Description("Lebanon")] LB = 124,
        [Description("Lesotho")] LS = 125,
        [Description("Liberia")] LR = 126,
        [Description("Libya")] LY = 127,
        [Description("Liechtenstein")] LI = 128,
        [Description("Lithuania")] LT = 129,
        [Description("Luxembourg")] LU = 130,
        [Description("Macao")] MO = 131,
        [Description("Macedonia")] MK = 132,
        [Description("Madagascar")] MG = 133,
        [Description("Malawi")] MW = 134,
        [Description("Malaysia")] MY = 135,
        [Description("Maldives")] MV = 136,
        [Description("Mali")] ML = 137,
        [Description("Malta")] MT = 138,
        [Description("Marshall Islands")] MH = 139,
        [Description("Martinique")] MQ = 140,
        [Description("Mauritania")] MR = 141,
        [Description("Mauritius")] MU = 142,
        [Description("Mayotte")] YT = 143,
        [Description("Mexico")] MX = 144,
        [Description("Micronesia")] FM = 145,
        [Description("Moldova")] MD = 146,
        [Description("Monaco")] MC = 147,
        [Description("Mongolia")] MN = 148,
        [Description("Montenegro")] ME = 149,
        [Description("Montserrat")] MS = 150,
        [Description("Morocco")] MA = 151,
        [Description("Mozambique")] MZ = 152,
        [Description("Myanmar")] MM = 153,
        [Description("Namibia")] NA = 154,
        [Description("Nauru")] NR = 155,
        [Description("Nepal")] NP = 156,
        [Description("Netherlands")] NL = 157,
        [Description("New Caledonia")] NC = 158,
        [Description("New Zealand")] NZ = 159,
        [Description("Nicaragua")] NI = 160,
        [Description("Niger")] NE = 161,
        [Description("Nigeria")] NG = 162,
        [Description("Niue")] NU = 163,
        [Description("Norfolk Island")] NF = 164,
        [Description("Northern Mariana Islands")] MP = 165,
        [Description("Norway")] NO = 166,
        [Description("Oman")] OM = 167,
        [Description("Pakistan")] PK = 168,
        [Description("Palau")] PW = 169,
        [Description("Palestine, State of")] PS = 170,
        [Description("Panama")] PA = 171,
        [Description("Papua New Guinea")] PG = 172,
        [Description("Paraguay")] PY = 173,
        [Description("Peru")] PE = 174,
        [Description("Philippines")] PH = 175,
        [Description("Pitcairn")] PN = 176,
        [Description("Poland")] PL = 177,
        [Description("Portugal")] PT = 178,
        [Description("Puerto Rico")] PR = 179,
        [Description("Qatar")] QA = 180,
        [Description("Réunion")] RE = 181,
        [Description("Romania")] RO = 182,
        [Description("Russian Federation")] RU = 183,
        [Description("Rwanda")] RW = 184,
        [Description("Saint Barthélemy")] BL = 185,
        [Description("Saint Helena, Ascension and Tristan da Cunha")] SH = 186,
        [Description("Saint Kitts and Nevis")] KN = 187,
        [Description("Saint Lucia")] LC = 188,
        [Description("Saint Martin (French part)")] MF = 189,
        [Description("Saint Pierre and Miquelon")] PM = 190,
        [Description("Saint Vincent and the Grenadines")] VC = 191,
        [Description("Samoa")] WS = 192,
        [Description("San Marino")] SM = 193,
        [Description("Sao Tome and Principe")] ST = 194,
        [Description("Saudi Arabia")] SA = 195,
        [Description("Senegal")] SN = 196,
        [Description("Serbia")] RS = 197,
        [Description("Seychelles")] SC = 198,
        [Description("Sierra Leone")] SL = 199,
        [Description("Singapore")] SG = 200,
        [Description("Sint Maarten")] SX = 201,
        [Description("Slovakia")] SK = 202,
        [Description("Slovenia")] SI = 203,
        [Description("Solomon Islands")] SB = 204,
        [Description("Somalia")] SO = 205,
        [Description("South Africa")] ZA = 206,
        [Description("South Georgia")] GS = 207,
        [Description("South Sudan")] SS = 208,
        [Description("Spain")] ES = 209,
        [Description("Sri Lanka")] LK = 210,
        [Description("Sudan")] SD = 211,
        [Description("Suriname")] SR = 212,
        [Description("Svalbard and Jan Mayen")] SJ = 213,
        [Description("Swaziland")] SZ = 214,
        [Description("Sweden")] SE = 215,
        [Description("Switzerland")] CH = 216,
        [Description("Syrian Arab Republic")] SY = 217,
        [Description("Taiwan, Province of China[a]")] TW = 218,
        [Description("Tajikistan")] TJ = 219,
        [Description("Tanzania, United Republic of")] TZ = 220,
        [Description("Thailand")] TH = 221,
        [Description("Timor-Leste")] TL = 222,
        [Description("Togo")] TG = 223,
        [Description("Tokelau")] TK = 224,
        [Description("Tonga")] TO = 225,
        [Description("Trinidad and Tobago")] TT = 226,
        [Description("Tunisia")] TN = 227,
        [Description("Turkey")] TR = 228,
        [Description("Turkmenistan")] TM = 229,
        [Description("Turks and Caicos Islands")] TC = 230,
        [Description("Tuvalu")] TV = 231,
        [Description("Uganda")] UG = 232,
        [Description("Ukraine")] UA = 233,
        [Description("United Arab Emirates")] AE = 234,
        [Description("United Kingdom")] GB = 235,
        [Description("United Nations")] UN = 252,
        [Description("United States of America")] US = 236,
        [Description("United States Minor Outlying Islands")] UM = 237,
        [Description("Uruguay")] UY = 238,
        [Description("Uzbekistan")] UZ = 239,
        [Description("Vanuatu")] VU = 240,
        [Description("Venezuela")] VE = 241,
        [Description("Vietnam")] VN = 242,
        [Description("British Virgin Islands")] VG = 243,
        [Description("US Virgin Islands")] VI = 244,
        [Description("Wallis and Futuna")] WF = 245,
        [Description("Western Sahara")] EH = 246,
        [Description("Yemen")] YE = 247,
        [Description("Zambia")] ZM = 248,
        [Description("Zimbabwe")] ZW = 249,

        // Last index is 252 (European Union)
    }

    #endregion countries
}

# TLV Website Automation Tests

פרויקט בדיקות אוטומציה לאתר עיריית תל אביב-יפו באמצעות C#, Selenium WebDriver, NUnit ו-ReportPortal.

## 📋 דרישות מקדימות

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0) או גבוה יותר
- [Google Chrome](https://www.google.com/chrome/) (גרסה אחרונה)
- [ReportPortal](http://localhost:8082) (אופציונלי - לצפייה בדוחות)

## 🚀 התקנה

1. **Clone או הורדת הפרויקט**

2. **שחזור חבילות NuGet:**
   ```bash
   cd c:\Users\Asher_Sh\Documents\ReportPortal-Demo
   dotnet restore
   ```

3. **בנייה:**
   ```bash
   dotnet build
   ```

## 🖥️ הגדרת סביבת היישום (IDE)

### Visual Studio User? (המומלץ)
ה-Test Explorer מובנה ב-Visual Studio.
1. בתפריט העליון, לחץ על **Test**.
2. בחר **Test Explorer**.
3. אם החלון לא מופיע, ודא שהתקנת את ה-Workload של **.NET desktop development**.

### VS Code User?
כדי לראות את ה-Test Explorer ב-VS Code:
1. לחץ על סמל הקוביות (Extensions) בצד שמאל.
2. חפש את המזהה המדויק: **formulahendry.dotnet-test-explorer**
   *(שם התוסף: .NET Core Test Explorer, מאת Jun Han)*.
3. לאחר ההתקנה, יופיע אייקון של "מבחנה" (Testing) בצד שמאל.
4. לחץ עליו כדי לראות ולהריץ את הטסטים.

## 🧪 הרצת הבדיקות

### הרצת כל הבדיקות:
```bash
dotnet test
```

### הרצת בדיקות ספציפיות לפי קטגוריה:
```bash
# בדיקות דף הבית
dotnet test --filter "Category=HomePage"

# בדיקות ניווט
dotnet test --filter "Category=Navigation"

# בדיקות דף יצירת קשר
dotnet test --filter "Category=ContactPage"

# בדיקות חיפוש
dotnet test --filter "Category=Search"

# בדיקות החלפת שפה
dotnet test --filter "Category=Language"
```

### הרצה עם פלט מפורט:
```bash
dotnet test --logger "console;verbosity=detailed"
```

## 📁 מבנה הפרויקט

```
TlvWebSite/
├── Pages/                      # Page Object Model classes
│   ├── BasePage.cs             # Base class with common functionality
│   ├── HomePage.cs             # Homepage POM
│   ├── ContactPage.cs          # Contact page POM
│   ├── ServicesPage.cs         # Services page POM
│   └── SearchResultsPage.cs    # Search results POM
├── Tests/                      # Test classes
│   ├── TestBase.cs             # Base test with setup/teardown
│   ├── HomePageTests.cs        # Test 1: Homepage tests
│   ├── NavigationTests.cs      # Test 2: Navigation tests
│   ├── ContactPageTests.cs     # Test 3: Contact page tests
│   ├── SearchTests.cs          # Test 4: Search tests
│   └── LanguageSwitchTests.cs  # Test 5: Language switch tests
├── Utilities/                  # Helper classes
│   └── DriverFactory.cs        # WebDriver factory
├── ReportPortal.json           # ReportPortal configuration
└── TlvWebSite.csproj           # Project file
```

## 🧩 5 הבדיקות העיקריות

| # | שם הבדיקה | תיאור |
|---|-----------|--------|
| 1 | `VerifyHomePageLoads` | בדיקת טעינת דף הבית עם כותרת, לוגו וניווט |
| 2 | `VerifyServicesNavigation` | בדיקת ניווט לדף השירותים |
| 3 | `VerifyContactPageLoads` | בדיקת טעינת דף יצירת קשר |
| 4 | `VerifySearchFunctionality` | בדיקת פונקציית חיפוש עם "ארנונה" |
| 5 | `VerifyLanguageSwitch` | בדיקת החלפת שפה מעברית לאנגלית |

## 🔐 אבטחת מידע וניהול סודות (API Key)

כדי למנוע חשיפה של ה-API Key, הפרויקט תומך בטעינת המפתח דרך משתני סביבה (Environment Variables).
קובץ הקונפיגורציה `ReportPortal.json` אינו נכלל בגיט (מוחרג ב-`.gitignore`).

### הגדרה לוקאלית (Local Setup)

1. **אפשרות א' - קובץ קונפיגורציה (לא מומלץ ל-Commit):**
   - העתק את `ReportPortal.template.json` לקובץ בשם `ReportPortal.json`.
   - ערוך את `ReportPortal.json` והדבק את ה-API Key שלך בשדה `apiKey`.
   - הקובץ `ReportPortal.json` כבר מוחרג ב-`.gitignore` ולא יעלה לגיט.

2. **אפשרות ב' - משתנה סביבה (מומלץ):**
   - הגדר משתנה סביבה במערכת ההפעלה או ב-IDE:
     - שם: `ReportPortal_Server_ApiKey`
     - ערך: מפתח ה-API שלך.

### הגדרה ב-GitHub Actions (CI/CD)

הפרויקט כולל תהליך אוטומטי (`.github/workflows/test.yml`) שמריץ את הבדיקות בכל דחיפה (Push).
כדי שהבדיקות יעבדו ב-GitHub, יש להגדיר את הסודות:

1. היכנס ל-GitHub Repository שלך.
2. עבור ל-**Settings** > **Secrets and variables** > **Actions**.
3. לחץ על **New repository secret**.
4. צור סוד חדש בשם `REPORTPORTAL_API_KEY` והדבק את המפתח שלך בתוכו.

## 📊 ReportPortal Integration

הפרויקט מוגדר לשלוח תוצאות ל-ReportPortal:
- **URL:** http://localhost:8082 (לוקאלי) או כתובת השרת המרוחק ב-CI.
- **Project:** tlv-web-site

לצפייה בתוצאות, ודא ש-ReportPortal פועל והרץ את הבדיקות.

## 🛠️ טכנולוגיות

- **Selenium WebDriver 4.39** - אוטומציה של דפדפן
- **NUnit 3** - Framework לבדיקות
- **ChromeDriver** - דרייבר ל-Google Chrome
- **ReportPortal.NUnit** - אינטגרציה עם ReportPortal

## 📝 הערות

- הבדיקות מריצות דפדפן Chrome במצב רגיל (לא headless)
- לשינוי למצב headless, ערוך את `TestBase.cs` ושנה את הפרמטר ל-`true`:
  ```csharp
  Driver = DriverFactory.CreateChromeDriver(headless: true);
  ```
- צילומי מסך נשמרים אוטומטית במקרה של כשלון

## 👤 מחבר

נוצר על ידי Antigravity AI Assistant

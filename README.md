# TLV Website Automation Framework

A robust test automation solution for the Tel Aviv Municipality website (www.tel-aviv.gov.il), engineered with C#, NUnit, and Selenium WebDriver. This project demonstrates best practices in test automation, including Page Object Matrix (POM) design, real-time reporting via ReportPortal, and secure configuration management.

## 🛠 Technology Stack

- **Core:** .NET 7.0 (C#)
- **Web Automation:** Selenium WebDriver 4.39 (Chrome)
- **Testing Framework:** NUnit 3
- **Reporting:** ReportPortal Integration
- **Logging:** Log4Net
- **CI/CD:** GitHub Actions

## 🚀 Getting Started

### Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0) or higher
- Google Chrome (latest version)
- A running instance of ReportPortal (optional)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Captain-Automation/ReportPortal-Demo.git
   cd ReportPortal-Demo
   ```

2. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the Project**
   ```bash
   dotnet build
   ```

## 🔐 Configuration & Security

To ensure security, sensitive credentials (API Keys) are not stored in version control.

### Option 1: Environment Variables (Recommended for CI/CD)
The framework automatically reads the API key from environment variables.
- **Variable Name:** `ReportPortal_Server_ApiKey`
- **Value:** Your personal access token

### Option 2: Local Configuration (Development)
1. Copy the template file: `ReportPortal.template.json` → `ReportPortal.json`.
2. Paste your API Key into the `apiKey` field in `ReportPortal.json`.
3. **Note:** `ReportPortal.json` is securely ignored by `.gitignore` and will not be pushed to the repository.

## 🧪 Executing Tests

You can run tests using the `dotnet test` command CLI or the Test Explorer in Visual Studio / VS Code.

### Run All Tests
```bash
dotnet test
```

### Run Specific Modules
```bash
# Homepage Verification
dotnet test --filter "Category=HomePage"

# Search Functionality
dotnet test --filter "Category=Search"

# Contact Page
dotnet test --filter "Category=ContactPage"

# Language Localization
dotnet test --filter "Category=Language"
```

## 📂 Project Structure

```text
TlvWebSite/
├── Pages/                  # Page Object Models (POM) - UI Logic
│   ├── HomePage.cs
│   ├── SearchResultsPage.cs
│   └── ...
├── Tests/                  # NUnit Test Classes - Test Logic
│   ├── TestBase.cs         # Setup/Teardown & Driver Management
│   ├── SearchTests.cs
│   └── ...
├── Utilities/              # Shared Helpers
│   └── DriverFactory.cs    # WebDriver Initialization
├── ReportPortal.json       # Local Configuration (Ignored)
└── log4net.config          # Logging Configuration
```

## 📊 CI/CD Pipeline

This project is configured with **GitHub Actions**.
- **Workflow:** `.github/workflows/test.yml`
- **Trigger:** Pushes to `main` branch.
- **Secrets:** Uses `REPORTPORTAL_API_KEY` stored in GitHub Repository Secrets.

---

## 👤 Author

**Asher M Chiff**

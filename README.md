# Quiz ASP.NET Project

## Project Overview
This project is a dynamic web application for creating and taking quizzes, built using ASP.NET. It allows administrators to create, edit, and delete quizzes, while users can participate in quizzes, view their scores, and track their progress.

---

## Features

### User Features:
- **Sign Up/Login**: Secure authentication using ASP.NET Identity.
- **Take Quizzes**: Attempt quizzes created by administrators.
- **View Scores**: Track quiz results and performance over time.

### Admin Features:
- **Quiz Management**: Create, update, and delete quizzes and questions.
- **User Management**: Monitor user activity and manage accounts.

---

## Technology Stack

- **Frontend**:
  - HTML/CSS/JavaScript
  - Razor Pages

- **Backend**:
  - ASP.NET Core
  - Entity Framework Core (for database management)

- **Database**:
  - SQL Server

- **Authentication**:
  - ASP.NET Identity

---

## Installation Guide

### Prerequisites:
1. Visual Studio (latest version recommended).
2. .NET Core SDK.
3. SQL Server (LocalDB or full version).

### Steps to Run Locally:
1. Clone the repository:
   ```bash
   git clone <repository-url>
   ```
2. Open the project in Visual Studio.
3. Update the `appsettings.json` file with your database connection string.
4. Run database migrations:
   ```bash
   dotnet ef database update
   ```
5. Build and run the project:
   - Press `Ctrl + F5` or navigate to **Debug > Start Without Debugging** in Visual Studio.
6. Access the application in your browser at `http://localhost:<port>/`.

---

## Usage Instructions

1. **User Sign-Up/Login**:
   - Create an account or log in with an existing account.
2. **Taking a Quiz**:
   - Navigate to the "Quizzes" section, choose a quiz, and start answering questions.
3. **Admin Panel**:
   - Log in with an admin account to access the admin dashboard for managing quizzes and users.

---

## Contribution Guidelines

1. Fork the repository and create a new branch for your feature/bug fix.
2. Commit your changes with clear messages.
3. Create a pull request describing your changes.

---

## Future Enhancements

- Add support for multimedia quiz questions.
- Implement real-time leaderboards.
- Add multilingual support.
- Mobile-friendly responsive design.

---

## License
This project is licensed under the MIT License. See the LICENSE file for more details.

---

## Contact
For any inquiries or issues, please contact:
- **Name**: Vishal Baraiya
- **Email**: baraiyavishalbhai32@gmail.com
- **GitHub**: https://github.com/mr-baraiya

---


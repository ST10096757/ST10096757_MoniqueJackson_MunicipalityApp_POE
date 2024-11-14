ST10096757
Monique Jackson
PROG7312
Part 2

-- Overview
The POE Part 2 adds to the Report Issues Application with an Events Announcement feature. This WPF-based desktop application now allows users to view and search upcoming community events. The application provides an interface for managing event announcements and enables users to explore events by category or date.


-- Features
Location Input: Users can specify the location of the reported issue.
Category Selection: A dropdown menu allows users to select the category of the issue (e.g., sanitation, roads, utilities).
Description Box: A RichTextBox is for users to provide a detailed description of the issue.
Media Attachment: Users can attach images or documents related to the reported issue using the file dialog.
Progress Tracking: The progress bar updates based on the completion of the report form.

-- Part 2 Features
Event Display: Users can view a list of upcoming events, with details like title, date, category, and description.
Search Functionality: Users can search for events by selecting a category or date from dropdown menus.
Recent Searches: Users can view a list of their recent searches for events.
Recommendations: The application recommends events based on user preferences.
Clear Filters: Users can clear their search selections to view all events again.

-- Added Menu Feature
Theme Toggle: Users can switch between light and dark themes.
Emergency Contacts: Access emergency contact information easily.
Bill of Responsibilities: Users can read about their responsibilities as a South African


-- Navigation
Users are greeted with the Main Window interface, which displays the apps 3 functions.
Only "Report Issues" button is enabled.
Users can choose to exit the app by selecting the "Exit" button, which will ask the user to confirm that they would like to leave the app.

Once the user selects the "Report Issues" button, they will be navigated to the Report Issues Interface.
Here, they will be prompted to enter all the necessary information to report the issue.
All fields need to be filled for the user to be submit the issue.
Should fields empty, an error message will display upon Submission.

Once fields have been filled, the user must select the "Submit" button and a message box displaying the data they have entred will be displayed.

Users can return to the main menu at anytime by selecting the "Back to Main Menu" button.
If the user has entered data but haven't submitted the data, a warning message will display when the select the "Back to Main Menu" button.

Users can also view all data saved in the list by selecting the "View Issues" button, which will navigate them to the View Issues Interface. (Function Under Construction)

-- Part 2 Navigation

Now when launching the application, users are greeted with the main window interface that provides options to:

View Events: Navigate to the Events Announcement interface to see upcoming events.
Report Issues: Access the issue reporting feature in a new window.
Emergency Contacts: Open a dialog to view emergency contact information.

PopupBox:
Exit: Close the application with a confirmation prompt
Toggle: For themes
Bill of Responsibilities

In the Events Announcement interface, users can:

View all upcoming events.
Search for specific events based on selected criteria.
View recent searches.
Users can return to the main menu at any time using the "Back" button.

-- Getting Started

-- Prerequisites
.NET Framework 4.8 or later
Visual Studio 2019 

-- Set Up
Open the Project: Open the solution file in Visual Studio.
Build the Project: Build the project by selecting Build > Build Solution in the Visual Studio menu.
Run the Application: Start the application by pressing F5 or selecting Debug > Start Debugging in Visual Studio.


-- Code Structure
ReportIssues_UC.xaml: Defines the user interface for the issue reporting control.
ReportIssues_UC.xaml.cs: Contains the logic for handling user interactions, including form submission, media attachment, and progress tracking.

MainWindow.xaml: The main window that hosts the ReportIssues_UC user control.
MainWindow.xaml.cs: Manages navigation and other main window functionalities.

ViewIssue_UC.xaml: Defines the user interface for viewing reported issues.
ViewIssue_UC.xaml.cs: Contains logic for displaying and interacting with the list of reported issues.

IssuesClass: The data model for reported issues.
IssuesViewModel: The view model for the application, managing the reported issues and allowing data binding between the UI and the data model.

-- Part 2 Code Structure:
Code Structure
MainWindow.xaml: The main window that has user controls and manages navigation.

MainWindow.xaml.cs: Contains the logic for theme management, event handling, and user control navigation.

Events_Announce_UC.xaml: Defines the user interface for the events announcement control.

Events_Announce_UC.xaml.cs: Contains the logic for handling event display, search functionality, and recommendations.

Models:

EventsClass: Has the data model for events.
EventManager: Manages events and provides methods for adding and retrieving events.
EventSearchManager: Handles recent searches.
NotificationManager: Manages notifications related to new events. (Under Construction)
RecommendationManager: Gives event recommendations based on user interactions.
IssuesViewModel: Manages data and logic for reporting issues.


-- Author/s
Monique Jackson and a Robot-Overlord with a better eye for interfaces/designs than my own.

Thank you for "READ"ing ME.


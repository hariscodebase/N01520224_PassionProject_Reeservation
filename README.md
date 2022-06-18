# N01520224_PassionProject_Reeservation

This is a passion project to develop a Reservation Management Studio using ASP dotNet MVC and Entity Framework.

# Design

- This project has 3 major controllers Reservation, Guest and Unit controller.
- Similarly it has 3 Database tables connected using 1 to M relational db structure.
- For each controller, its resepctive view has the html code for user interactions.
- Added CSS stylesheet to make the website look attractive.

# Setting this project 
- Make sure there is an App_Data folder in the project (Right click solution > View in File Explorer)
- Tools > Nuget Package Manager > Package Manage Console > Update-Database
- Check that the database is created using (View > SQL Server Object Explorer > MSSQLLocalDb > ..)
- Run API commands through CURL to create new animals

# API calls available
Get a List of Reservations - GET
https://localhost:44324/api/ReservationData/ListReservations

Get a Single Reservation by passing Reservation Id - GET
https://localhost:44324/api/ReservationData/GetReservation/{id}

Get a list of Reservations by Guest Id - GET
https://localhost:44324/api/ReservationData/ListReservationByGuestId/{id}

Get a list of Reservations by Unit Id - GET
https://localhost:44324/api/ReservationData/ListReservationByUnitId/{id}

Add a new Reservation - POST
https://localhost:44324/api/ReservationData/AddReservation

Update Reservation (update the created\existing reservations) - POST
https://localhost:44324/api/ReservationData/UpdateReservation/{id}

# Running the Application
- Open this project in Visual Studio
- Run the solution
- Navigate to (https://localhost:44365/)
- Click on Reservation tab button.
- In this page you can add, view and edit reservation.
- Click on Add Guest page to add new guests.

# Features
- Create\Edit Reservation
- Add\Edit Guests

# Upcoming Features
- Add search and find feature for Guests\Units dropdown.
- Create Views for showing reservation lists based on Units, Guests.
- Add a Page to show the status progress or history of reservation.

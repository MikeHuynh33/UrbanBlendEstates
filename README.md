# Real Estate and Restaurant Review Application

Welcome to our creative collaboration project that merges a real estate application with a restaurant review application, combining the best of both worlds. This README provides an overview of the project, the steps taken for integration, and instructions for getting started.

## Overview

This project is a creative fusion of a real estate application and a restaurant review application, creating a comprehensive platform that allows users to explore properties and discover local dining options. By merging these two domains, we aim to provide users with a versatile and engaging experience to explore local community before they make a purchase.

## Getting Started

Follow these steps to set up the development environment and get the project running on your machine:

1. **Clone the Repository:**
   - Clone the repository of the ASP.NET project to your local machine using a Git client or the command line.

2. **Open the Project in Visual Studio:**
   - Open Visual Studio.
   - Go to `File` > `Open` > `Project/Solution` and navigate to the cloned project's directory to open the solution file (usually ending in `.sln`).

3. **Open Package Manager Console:**
   - In Visual Studio, go to `Tools` > `NuGet Package Manager` > `Package Manager Console`.

4. **Select Default Project:**
   - At the top of the NuGet Package Manager Console, make sure you select the correct project (the one with your Entity Framework DbContext) from the "Default Project" dropdown list.

5. **Run Migrations and Update Database:**
   - Run the migration command to create and apply migrations:
     ```
     Add-Migration InitialCreate
     ```
   - This command will create a migration based on the changes detected in your DbContext.
   - After creating the migration, run the update database command to apply the migration and update the database schema:
     ```
     Update-Database
     ```
   - This command applies any pending migrations to the database.

6. **Run the Project:**
   - Build and run the project in Visual Studio to see the changes reflected in your application.

Remember to have a solid understanding of your existing database schema and the changes you're making before running migrations. Communication with your team is important, especially in a collaborative environment, to avoid conflicts and ensure everyone is aware of database changes.


## Database Integration

   - PropertyDetail and RealEstate: These entities have a many-to-many relationship, allowing properties to be associated with multiple real estate entities.
   - Restaurants: Each property and restaurant shares a one-to-many relationship with the Neighbourhood table, connecting them to specific neighborhoods.
   - Booking: The Booking table stores appointments and reservations for both properties and restaurants.
     
## UI Integration

   Our application's interface seamlessly combines real estate and restaurant features, ensuring a consistent look and feel across different devices.

   ### Home Screen and Property Search
   - The homepage offers a list of properties stored in our database. Users can easily search for properties using a user-friendly search bar. We also showcase top agents for easy access.

   ### Property Details
   - Clicking on a property provides detailed information about it, helping users understand its unique features.

   ### Booking Appointments
   - Users can schedule property meetings with owners using the "Meeting" button. Our system intelligently suggests nearby restaurants in the same neighborhood, enhancing the user experience.

## Features

   ### Property Search

   Easily search for properties of interest using our user-friendly search bar. Discover a variety of property listings from our extensive database.

   ### Property Details

   Click on any property to access detailed information, including descriptions, images, and key features.

   ### Hassle-Free Meetings

   Schedule meetings with agents effortlessly using the provided "Meeting" button. You can also choose a nearby restaurant as your meeting spot, enhancing your overall experience.

   ### Neighborhood Restaurants

   Before finalizing your meeting, explore restaurants located in the same neighborhood as the property. This feature enables you to make informed decisions and plan your visit more effectively.


## Deployment
   [Check out how to deploy ASP.NET in AWS](https://docs.aws.amazon.com/elasticbeanstalk/latest/dg/create_deploy_NET.quickstart.html)
To deploy the application to your localhost environment:

1. Update the database connection settings in the `config.js` file to match your localhost configuration.
2. Run the database migration scripts to ensure the database schema is up to date: `npm run migrate`.
3. Launch the application using `npm start`.

## Localhost Migration

Migrating the application to your localhost involves updating the necessary configurations to match your local environment. Ensure that connection strings, API endpoints, and database settings are correctly configured for your localhost setup.

## Documentation

We've documented the integration process, configuration changes, and database setup in the project. For more detailed information, refer to the documentation provided in the `docs` directory.


## License

This project is licensed under the [MIT License](LICENSE).

## Acknowledgments

We'd like to express our gratitude to the open-source community for their inspiring contributions, as well as the teams behind the original real estate and restaurant review applications.

---

Enjoy exploring properties and discovering delightful dining experiences with our merged Real Estate and Restaurant Review Application!

For inquiries and support, contact us at contact@example.com.

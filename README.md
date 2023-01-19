# Take A Seat

TakeASeat is an online ticket distribution platform. The application allows the user to create event,event-related shows, and specific audiences for each show instance. 

The application has been created to learn ASP.NET Core 6 WEB API, Entity Framework and Prime React Framework. 

## Table of content

[Technologies](#technologies)

[Description](#description)

[To do](#to-do)

[Contributing](#contributing)

[License](#license)

## Technologies

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

![DOTNET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

![Nuget](https://img.shields.io/badge/NuGet-004880?style=for-the-badge&logo=nuget&logoColor=white)

![Docker](https://img.shields.io/badge/Docker-2CA5E0?style=for-the-badge&logo=docker&logoColor=white)

![JWT](https://img.shields.io/badge/JWT-black?style=for-the-badge&logo=JSON%20web%20tokens)

![MSSQL](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)

![HTML5](https://img.shields.io/badge/html5-%23E34F26.svg?style=for-the-badge&logo=html5&logoColor=white)

![CSS3](https://img.shields.io/badge/css3-%231572B6.svg?style=for-the-badge&logo=css3&logoColor=white)

![JavaScript](https://img.shields.io/badge/javascript-%23323330.svg?style=for-the-badge&logo=javascript&logoColor=%23F7DF1E)

![React](https://img.shields.io/badge/react-%2320232a.svg?style=for-the-badge&logo=react&logoColor=%2361DAFB)

![React Router](https://img.shields.io/badge/React_Router-CA4245?style=for-the-badge&logo=react-router&logoColor=white)

## Description

The application is intended for use by registered users only. Each registered user is assigned the "User" role. The user can buy tickets for shows. There are also "Organizer" and "Administrator" roles. Organizers can create events, shows, and audiences. Administrators can manage other user roles.

In order to sell tickets, you need to follow three steps:
1. The organizer creates a show and enables the purchase of tickets:
```mermaid
graph LR
A[Create Event] --> B((circle))
B[Create Show] --> C[Create Audience]
```
2. The user can reserve seats for each show. However, the user must pay for each reserved seat within five minutes. Otherwise, the reservation is released.

3. Once the payment is accepted by both the payment provider and the TakeASeat app, the tickets are created and emailed to the user.

# RestFlow

**Tired from using multiple platforms in order to manage your restaurant? RestFlow is for you.**

RestFlow is an all-in-one platform designed to streamline restaurant management, allowing you to handle everything from supply tracking to order creation. With an intuitive interface and secure backend, RestFlow brings your restaurant operations under one roof.

## Features

### 1. **Secure Authentication**
RestFlow ensures that all data is protected with secure authentication. Only authorized users can access the platform, with role-based access to manage specific tasks such as waiter functions, menu management, and supply control.

### 2. **Supply Management**
Easily track your ingredients and supplies in real-time. With features to update quantities, add new ingredients, and check availability, you’ll never run out of stock again. Keep your kitchen running smoothly with precise inventory management.

### 3. **Menu Management**
RestFlow allows you to manage your restaurant's menu seamlessly. Add or remove dishes, update prices, and track ingredient availability directly from the platform. Whether it’s a seasonal special or a permanent menu item, RestFlow helps you stay flexible.

### 4. **Waiter Management**
Simplify waiter management by adding waiters to the system with secure authentication. Each waiter gets personalized access to their tables, allowing them to easily manage orders, billings, and customer requests.

### 5. **Waiter Platform for Order Creation**
Waiters can use RestFlow’s dedicated order management platform to create and modify customer orders. From taking an order to closing a bill, the process is streamlined and efficient, improving the customer experience and reducing errors.

## Why RestFlow?

With **RestFlow**, you get everything you need to manage your restaurant efficiently, without switching between multiple platforms. It's the all-in-one solution that covers every aspect of your restaurant’s operations—giving you more time to focus on what matters most: your customers.

## Technology Stack

### Client-Side

- **Next.js & TypeScript**  
  The client side of RestFlow is built using Next.js, providing a powerful framework for server-side rendering and optimized performance. TypeScript is used to enhance the development process by adding type safety.

- **Tailwind CSS & shadcn**  
  For styling, RestFlow utilizes Tailwind CSS combined with `shadcn` to create a modern, responsive, and visually appealing interface. This ensures that the user experience is both clean and efficient across all devices.

- **Redux for State Management**  
  RestFlow leverages Redux to handle the application's global state, ensuring seamless state management across different components and ensuring a smooth user experience.

### Server-Side

- **ASP.NET Core Web API**  
  The backend is powered by ASP.NET Core Web API, providing a robust and scalable architecture for handling business logic, secure authentication, and API endpoints.

- **PostgreSQL**  
  For database management, RestFlow uses PostgreSQL, a powerful, open-source relational database system, to store and retrieve restaurant data efficiently.

- **Redis for Caching**  
  Redis is integrated to handle caching, improving response times and performance by reducing load on the database for frequently accessed data.

- **Docker for Containerization**  
  The entire server-side infrastructure is containerized using Docker, making it easy to deploy, scale, and manage the services across different environments.


# Design Patterns Used in RestFlow

1. **Repository Pattern**  
   Abstractions for data access that decouple the business logic from data persistence.  
   Implemented through repositories that handle CRUD operations for entities like ingredients and orders.

2. **Service Pattern**  
   Encapsulation of business logic in a dedicated service layer.  
   Implemented via services that manage operations related to ingredients, orders, and waiters.

3. **Factory Pattern**  
   A method for creating objects without specifying the exact class of object that will be created.  
   Implemented using a `ModelFactory` to create domain entities like ingredients and orders.

4. **Unit of Work Pattern**  
   Maintains a list of changes to entities for committing in a single transaction.  
   Implemented to ensure that all related repository operations are done atomically.

5. **Observer Pattern**  
   A subscription mechanism that allows objects to be notified of state changes.  
   Implemented by allowing the `OrderService` to notify registered observers when orders are updated.

6. **Singleton Pattern**  
   Ensures that a class has only one instance and provides a global access point.  
   Implemented for classes that require a single shared instance, like logging services.

7. **Decorator Pattern**  
   A design pattern that adds new behavior to objects dynamically.  
   Implemented to extend ingredient functionalities without altering the original class structure.

8. **Builder Pattern**  
   A pattern for constructing complex objects step by step.  
   Implemented to facilitate the creation of complex orders or dish combinations with flexible configurations.

9. **Facade Pattern**  
   Provides a simplified interface to a complex subsystem.  
   Implemented to offer a single point of interaction for managing various restaurant functionalities.

10. **Adapter Pattern**  
    Allows incompatible interfaces to work together.  
    Implemented to integrate third-party services or systems without modifying existing code.


## Architectures Used

### Domain-Driven Design (DDD)
Domain-Driven Design focuses on modeling complex software solutions based on the underlying business domain. In RestFlow, DDD was employed to create a clear separation between the domain logic and the application logic, ensuring that the software reflects the business needs and requirements accurately. This approach helps in maintaining a rich domain model that is both understandable and maintainable.

### Three-Layered Architecture
The Three-Layered Architecture divides the application into three distinct layers: Presentation, Business Logic, and Data Access. 
- **Presentation Layer**: This layer is responsible for handling user interface interactions and displaying data to users. In RestFlow, this is implemented using Next.js for the client-side.
- **Business Logic Layer**: This layer contains the core functionality and business rules of the application. It uses services to process data and enforce business rules.
- **Data Access Layer**: This layer is responsible for interacting with the database and performing CRUD operations. In RestFlow, this is facilitated by the Repository pattern, allowing for clean separation between data access and business logic.

### CQRS (Command Query Responsibility Segregation)
CQRS is an architectural pattern that separates the reading and writing of data into distinct models. In RestFlow, this approach allows for optimized queries and commands, improving performance and scalability by allowing each model to be tailored for its specific purpose.

### Dependency Injection (DI)
Dependency Injection is a design pattern that implements Inversion of Control, allowing for greater modularity and testing capabilities. In RestFlow, DI is used extensively to manage dependencies, making the application more flexible and easier to maintain.


## Data Structures Used

### Trees
Trees were implemented for ingredient management in RestFlow. This hierarchical data structure allows for efficient organization and retrieval of ingredients, enabling easy access to related ingredients and enhancing the overall management process.

### Linked List
A linked list was utilized for managing dishes within ingredients. This structure allows for dynamic memory allocation and efficient insertion and deletion of dishes, ensuring smooth handling of the menu items associated with each ingredient.

### Dictionary
Dictionaries were employed for managing dishes, providing a fast and efficient way to store and retrieve dish information based on unique identifiers. This key-value pair structure allows for quick lookups, enhancing performance when accessing dish data.

### Queues
Queues were implemented for managing orders in RestFlow. This FIFO (First In, First Out) structure allows for orderly processing of orders, ensuring that they are handled in the sequence they were received, which is crucial for maintaining a smooth workflow in the restaurant.

### Graph
A graph structure was used to represent tables within the restaurant. This allows for efficient management of table relationships and connections, making it easier to visualize and handle reservations, seating arrangements, and customer flow within the restaurant.

## Advanced Concepts Used

### Multithreading
Multithreading was implemented to enhance the performance of the RestFlow application by allowing multiple tasks to be executed concurrently. This enables efficient processing of user requests and background operations, improving the overall responsiveness and scalability of the system.

### Logging
A comprehensive logging system was integrated into the application to facilitate monitoring and debugging. This includes the use of structured logging to capture important events and errors throughout the application, providing insights into application behavior and helping identify issues promptly.

### Caching
Caching mechanisms were employed using Redis to improve the performance of the application by storing frequently accessed data in memory. This reduces the load on the database and speeds up data retrieval times, resulting in a more efficient and responsive user experience.


## Media

### Home Page
![Screenshot 2024-09-24 191610](https://github.com/user-attachments/assets/28603342-7df3-4345-88fa-9c9e43e3f109)

### Main Menu
![Screenshot 2024-09-24 190539](https://github.com/user-attachments/assets/4c289090-6f1a-440a-84ae-fd5549d83a12)

### Managment
![Screenshot 2024-09-24 191538](https://github.com/user-attachments/assets/da4b3d59-12ee-48fc-9c28-c37d204ee9f5)

### Waiter Platform
![Screenshot 2024-09-24 202335](https://github.com/user-attachments/assets/2963c7f1-a02b-44dd-9f51-cecce41f75a9)

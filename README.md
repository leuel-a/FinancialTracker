# Financial Tracking and Analysis Tool

## Overview

The Financial Tracking and Analysis Tool is designed to streamline financial management for businesses. It leverages modern web technologies and best practices in software architecture to provide an intuitive and powerful platform for financial oversight.

## Features

- **User-Friendly Dashboard:** Provides an overview of financial health with key performance indicators (KPIs) and recent transactions.
- **Transaction Management:** Allows easy recording, updating, and deletion of financial transactions.
- **Financial Data Visualization:** Visual representations of financial data through charts and graphs.
- **Detailed Financial Analysis:** Tools for deep financial analysis to uncover insights into sales patterns, expense trends, and other critical business metrics.
- **Enhanced Decision Making:** Facilitates informed decisions regarding budget allocation, cost reduction, and investment opportunities.

## Technologies Used

### Frontend
- **Next.js**
- **Shadcn UI Component Library**

### Backend
- **Microservices Architecture:**
  - Transaction Management Service: .NET Core
  - User Management Service: .NET Core
  - Employee Management Service: Node.js

### Deployment
- **Docker**
- **Docker Compose**

## Architectural Patterns
- **Clean Architecture**
- **SOLID Principles**
- **CQRS (Command Query Responsibility Segregation)**

## Setup Instructions

### Prerequisites
- Docker and Docker Compose installed on your machine.

### Steps
1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/financial-tracking-tool.git
   cd financial-tracking-tool
2. Build and run the Docker containers:
```sh
docker-compose up --build
```
3. Access the application:
    - The frontend will be available at http://localhost:3000
    - The backend services will be running on their respective ports as defined in docker-compose.yml

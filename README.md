# Developer Evaluation Project

## About the Project
This project is a sales management API for **DeveloperStore**, built using **.NET 8**, **Entity Framework Core**, and **PostgreSQL**.

The API allows **sales control**, including **customers, products, branches, and discount rules** based on the number of items purchased.

## Technologies Used
- .NET 8
- Entity Framework Core
- PostgreSQL
- Docker & Docker Compose
- FluentAssertions (Unit Testing)
- Moq (Mocking for Tests)
- xUnit (Test Framework)
- Swagger (API Documentation)

## Project Setup

### 1. Clone the Repository
```sh
git clone [https://github.com/thiagobrunoalexandre/Ambev.DeveloperEvaluation.git]
cd your-repository
```

### 2. Set Up the Database with Docker
```sh
docker-compose up -d
```
This will create a PostgreSQL database running on **localhost:5432**.

### 3. Configure the Application
Before running the application, check the **`appsettings.json`** file and edit the **ConnectionString** if necessary:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=DeveloperEvaluation;User Id=sa;Password=Pass@word;TrustServerCertificate=True"
}
```

### 4. Run the Application
```sh
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```
The API will be available at **http://localhost:5119**.

### 5. Run Tests
```sh
dotnet test
```
All tests should pass successfully ✅.

## Business Rules
✅ Purchases with **4+ items** get **10% discount**  
✅ Purchases between **10 and 20 items** get **20% discount**  
❌ **It is not possible to sell more than 20 identical items**  
❌ **No discounts allowed for purchases below 4 items**  

These business rules define quantity-based discounting tiers and limitations:

1. **Discount Tiers:**
   - **4+ items**: *10% discount*
   - **10-20 items**: *20% discount*

2. **Restrictions:**
   - **Maximum limit**: *20 items per product*
   - **No discounts allowed** for purchases below 4 items

## API Endpoints (Swagger)
The API documentation can be accessed via **Swagger**:

📌 [http://localhost:5119/swagger](http://localhost:5119/swagger)

## Running the Application with Docker
If you want to run the application using Docker, follow these steps:

1. Build the Docker image
```sh
docker build -t ambev-developer-api .
```

2. Run the application with Docker
```sh
docker run -p 8080:80 ambev-developer-api
```

3. Access the API
Now, the API will be available at:
[http://localhost:8080/swagger](http://localhost:8080/swagger)

## Author
👨‍💻 Developed by **Thiago Alexandre**  
📧 thiagobrunoalexandre@hotmail.com  
🌎 [LinkedIn](https://www.linkedin.com/in/thiago-alexandre-01705a14b)


# SQL CRUD Application

A modern ASP.NET Core MVC application demonstrating CRUD operations with Entity Framework Core and SQL Server LocalDB. This project features an attractive Bootstrap-based frontend and comprehensive product management functionality.

## 🚀 Features

- **Full CRUD Operations**: Create, Read, Update, and Delete products
- **Modern UI**: Responsive design with Bootstrap 5 and Font Awesome icons
- **Search & Filter**: Search products by name, description, or category
- **Data Validation**: Client and server-side validation
- **Entity Framework Core**: Code-first approach with migrations
- **SQL Server LocalDB**: Built-in SQL Server instance
- **Seed Data**: Pre-populated with sample products
- **Swagger/OpenAPI**: Complete API documentation with interactive testing
- **RESTful API**: Dedicated API endpoints for programmatic access

## 🛠️ Technologies Used

- **.NET 8.0**
- **ASP.NET Core MVC**
- **Entity Framework Core 8.0**
- **SQL Server LocalDB**
- **Bootstrap 5.3.2**
- **Font Awesome 6.4.0**
- **jQuery & jQuery Validation**
- **Swagger/OpenAPI 6.5.0**
- **Newtonsoft.Json**

## 📋 Prerequisites

Before running this application, make sure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) or [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) (usually comes with Visual Studio)

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone <your-repository-url>
cd SQLCRUD
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Install Client-Side Libraries

```bash
libman restore
```

### 4. Create and Update Database

```bash
# Add migration (if needed)
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run
```

The application will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:7000`
- **Swagger API Documentation**: `http://localhost:5000/api-docs` or `https://localhost:7000/api-docs`

## 📁 Project Structure

```
SQLCRUD/
├── Controllers/           # MVC Controllers
│   ├── HomeController.cs
│   ├── ProductsController.cs
│   └── ProductsApiController.cs
├── Data/                 # Entity Framework DbContext
│   └── ApplicationDbContext.cs
├── Models/               # Data Models
│   └── Product.cs
├── Filters/              # Swagger Filters
│   └── ExampleSchemaFilter.cs
├── Views/                # Razor Views
│   ├── Home/
│   ├── Products/
│   └── Shared/
├── wwwroot/              # Static Files
│   ├── css/
│   ├── js/
│   └── lib/
├── Properties/
│   └── launchSettings.json
├── Program.cs            # Application Entry Point
├── appsettings.json      # Configuration
└── README.md
```

## 🎯 Key Features Explained

### Product Management
- **Create**: Add new products with validation
- **Read**: View all products with search and filtering
- **Update**: Edit existing product information
- **Delete**: Remove products with confirmation

### Search & Filtering
- Search by product name, description, or category
- Filter by category dropdown
- Clear filters functionality

### Responsive Design
- Mobile-first approach with Bootstrap
- Card-based layout for product display
- Interactive buttons with hover effects
- Font Awesome icons throughout

### Data Validation
- Required field validation
- Range validation for prices and quantities
- String length validation
- Client-side and server-side validation

### API Documentation (Swagger)
- **Interactive API Testing**: Test all endpoints directly from the browser
- **Comprehensive Documentation**: Detailed descriptions for all API endpoints
- **Request/Response Examples**: Sample data for all operations
- **Schema Definitions**: Complete data models with validation rules
- **HTTP Status Codes**: Clear documentation of all possible responses

### RESTful API Endpoints
- `GET /api/products` - Get all products with optional filtering
- `GET /api/products/{id}` - Get specific product by ID
- `GET /api/products/categories` - Get all available categories
- `GET /api/products/statistics` - Get product statistics summary
- `POST /api/products` - Create new product
- `PUT /api/products/{id}` - Update existing product
- `DELETE /api/products/{id}` - Delete product

## 🗄️ Database Schema

The `Product` model includes:
- `Id` (Primary Key)
- `Name` (Required, Max 100 chars)
- `Description` (Optional, Max 500 chars)
- `Price` (Required, Decimal)
- `Category` (Required, Max 50 chars)
- `StockQuantity` (Integer, Min 0)
- `DateCreated` (DateTime)
- `IsActive` (Boolean)

## 🎨 UI Components

### Home Page
- Statistics dashboard
- Recent products display
- Quick action buttons

### Products Index
- Grid layout with product cards
- Search and filter functionality
- Action buttons for each product

### Product Forms
- Clean, validated input forms
- Icon-enhanced input fields
- Success/error messaging

## 🔧 Configuration

### Connection String
The application uses SQL Server LocalDB by default:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SQLCRUDDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### Environment Settings
- Development: Uses `SQLCRUDDb_Dev` database
- Production: Uses `SQLCRUDDb` database

## 🔧 Using the API

### Swagger UI
1. Navigate to `http://localhost:5000/api-docs` in your browser
2. Explore the interactive API documentation
3. Click "Try it out" on any endpoint to test it
4. Use the provided examples or create your own requests

### Example API Calls

#### Get All Products
```bash
curl -X GET "http://localhost:5000/api/products" -H "accept: application/json"
```

#### Create a New Product
```bash
curl -X POST "http://localhost:5000/api/products" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "New Product",
    "description": "Product description",
    "price": 99.99,
    "category": "Electronics",
    "stockQuantity": 10,
    "isActive": true
  }'
```

#### Search Products
```bash
curl -X GET "http://localhost:5000/api/products?searchString=laptop&categoryFilter=Electronics"
```

#### Get Product Statistics
```bash
curl -X GET "http://localhost:5000/api/products/statistics"
```

## 🚀 Deployment

### For GitHub Pages (Static Hosting)
This is an ASP.NET Core application and requires a server environment. For static hosting alternatives, consider:
- Azure App Service
- AWS Elastic Beanstalk
- Heroku
- DigitalOcean App Platform

### Docker Support
To containerize the application:

1. Create a `Dockerfile`:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SQLCRUD.csproj", "."]
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SQLCRUD.dll"]
```

2. Build and run:
```bash
docker build -t sqlcrud .
docker run -p 8080:80 sqlcrud
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- Bootstrap team for the amazing CSS framework
- Font Awesome for the beautiful icons
- Microsoft for ASP.NET Core and Entity Framework
- The .NET community for excellent documentation

## 📞 Support

If you encounter any issues or have questions:

1. Check the [Issues](https://github.com/yourusername/SQLCRUD/issues) page
2. Create a new issue with detailed information
3. Include your environment details (.NET version, OS, etc.)

---

**Happy Coding! 🎉**
